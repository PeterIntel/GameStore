using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Authorization;
using GameStore.Web.ViewModels;
using GameStore.Domain.ServicesInterfaces;
using AutoMapper;
using GameStore.Domain.BusinessObjects;

namespace GameStore.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IAuthentication auth, IMapper mapper) : base(auth)
        {
            _accountService = accountService;
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
                _accountService.Add(user);

                Auth.Login(model.Login, model.Password);

                return RedirectToAction("GetUsers");
            }

            return View(model);
        }

        private void CheckLoginAndEmail(UserViewModel model)
        {
            if (_accountService.Any(x => x.Login == model.Login))
            {
                ModelState.AddModelError("Login", $"Login {model.Login} already exists!");
            }

            if (_accountService.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", $"E-mail {model.Email} already exists!");
            }
        }

        public ActionResult LogOff(int id = 0)
        {
            Auth.Logout();
            return RedirectToAction("GetGames", "Game");
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
            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("GetGames", "Game");
        }

        public ActionResult GetUsers()
        {
            return View(_mapper.Map<IEnumerable<User>, IList<UserViewModel>>(_accountService.Get()));
        }

        public ActionResult UserDetails(string id)
        {
            User user = _accountService.First(x => x.Login == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(_mapper.Map<User, UserViewModel>(user));
        }

        public ActionResult Create()
        {
            return View(new UserViewModel() {Roles = _mapper.Map<IEnumerable<Role>, IList<RoleViewModel>>(_accountService.GetRoles()) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            CheckLoginAndEmail(userViewModel);

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserViewModel, User>(userViewModel);
                _accountService.Add(user);

                return RedirectToAction("GetUsers");
            }

            userViewModel.Roles = _mapper.Map<IEnumerable<Role>, IList<RoleViewModel>>(_accountService.GetAllRolesAndMarkSelected(userViewModel.IdRoles));

            return View(userViewModel);
        }
    }
}