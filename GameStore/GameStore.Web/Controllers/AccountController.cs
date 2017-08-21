using System;
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
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (_accountService.Any(x => x.Login == model.Login))
            {
                ModelState.AddModelError("Login", $"Login {model.Login} already exists!");
            }

            if (_accountService.Any(x => x.Email == model.Email))
            {
                ModelState.AddModelError("Email", $"E-mail {model.Email} already exists!");
            }

            if (ModelState.IsValid)
            {
                var user = _mapper.Map<RegisterViewModel, User>(model);
                _accountService.Add(user);

                Auth.Login(model.Login, model.Password);

                return RedirectToAction("GetGames", "Game");
            }

            return View(model);
        }


    }
}