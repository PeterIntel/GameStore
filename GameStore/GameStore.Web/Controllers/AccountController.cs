using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Authorization.Interfaces;
using GameStore.Domain.BusinessObjects;
using GameStore.Domain.ServicesInterfaces;
using GameStore.Web.Attributes;
using GameStore.Web.ViewModels;
using GameStore.Web.App_LocalResources;

namespace GameStore.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IAuthentication auth, IMapper mapper,
            IPublisherService publisherService) : base(auth)
        {
            _accountService = accountService;
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserViewModel model)
        {
            CheckLoginAndEmail(model);

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserViewModel, User>(model);

                ((IList<Role>)user.Roles).Add(new Role() {RoleEnum = RoleEnum.User});
                _accountService.Add(user, CurrentLanguageCode);

                Auth.Login(model.Login, model.Password);

                return RedirectToAction("GetUsers");
            }

            return View(model);
        }

        [CustomAuthorize(RoleEnum.Administrator, RoleEnum.Manager, RoleEnum.Moderator, RoleEnum.User, RoleEnum.Publisher)]
        public ActionResult LogOff()
        {
            Auth.Logout();

            return RedirectToAction("games", "Game");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Auth.Login(model.UserName, model.Password, model.RememberMe);

                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", Resources.IncorrectLogin);

            return View(model);
        }

        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult GetUsers()
        {
            return View(_mapper.Map<IEnumerable<User>, IList<UserViewModel>>(_accountService.Get(CurrentLanguageCode)));
        }

        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult UserDetails(string key)
        {
            User user = _accountService.First(x => x.Login == key, CurrentLanguageCode);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<User, UserViewModel>(user));
        }

        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult Create()
        {
            return View(new UserViewModel
            {
                Roles = _mapper.Map<IEnumerable<Role>, IList<RoleViewModel>>(_accountService.GetRoles()),
                Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get(CurrentLanguageCode))
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult Create(UserViewModel userViewModel)
        {
            CheckLoginAndEmail(userViewModel);

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserViewModel, User>(userViewModel);
                _accountService.Add(user, CurrentLanguageCode);

                return RedirectToAction("GetUsers");
            }

            userViewModel.Roles = _mapper.Map<IEnumerable<Role>, IList<RoleViewModel>>(_accountService.GetAllRolesAndMarkSelected(userViewModel.IdRoles));
            userViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get(CurrentLanguageCode));

            return View(userViewModel);
        }

        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult Edit(string key)
        {
            User user = _accountService.First(x => x.Login == key, CurrentLanguageCode);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userViewModel = _mapper.Map<User, UserViewModel>(user);
            userViewModel.Roles = _mapper.Map<IEnumerable<Role>, IList<RoleViewModel>>(_accountService.GetAllRolesAndMarkSelected(userViewModel.Roles.Select(x => x.Role.ToString())));
            userViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get(CurrentLanguageCode));

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _accountService.Update(_mapper.Map<UserViewModel, User>(userViewModel), CurrentLanguageCode);
                return RedirectToLocal("GetUsers");
            }

            userViewModel.Roles = _mapper.Map<IEnumerable<Role>, IList<RoleViewModel>>(_accountService.GetAllRolesAndMarkSelected(userViewModel.IdRoles));
            userViewModel.Publishers = _mapper.Map<IEnumerable<Publisher>, IList<PublisherViewModel>>(_publisherService.Get(CurrentLanguageCode));

            return View(userViewModel);
        }

        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult Delete(string key)
        {
            var user = _accountService.First(x => x.Login == key, CurrentLanguageCode);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<User, UserViewModel>(user));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(RoleEnum.Administrator)]
        public ActionResult ConfirmDelete(string id)
        {
            var user = _accountService.First(x => x.Id == id, CurrentLanguageCode);

            if (user.IsInRole(RoleEnum.Administrator) && _accountService.GetCountAdministrators() <= 1)
            {
                ModelState.AddModelError("", @"You are the last administrator and you can not delete yourself!");
            }

            if (ModelState.IsValid)
            {
                _accountService.Remove(id);

                return RedirectToAction("GetUsers");
            }

            return View(_mapper.Map<User, UserViewModel>(user));
        }

        private void CheckLoginAndEmail(UserViewModel model)
        {
            if (_accountService.Any(x => x.Login == model.Login))
            {
                ModelState.AddModelError("Login", Resources.ExistLogin);
            }

            if (_accountService.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", Resources.ExistEmail);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Games", "Game");
        }
    }
}