using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChushkaWebApp.Models;
using ChushkaWebApp.Services.Contracts;
using ChushkaWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ChushkaWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _Service;

        public AccountController(IAccountService accountService)
        {
            this._Service = accountService;
        }

        [Authorize]
        public IActionResult Logout()
        {
            this._Service.Logout();

            return this.Redirect("/");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var result = this._Service.Login(model.Username, model.Password);
            if (!result.Result)
            {
                return this.View();
            }

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Register()
        {
            return this.View();
        }
        
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var result = this._Service.Register(model.Username, model.Password, model.ConfirmPassword, model.Email,
                model.FullName);

            if (!result.Result)
            {
                return this.View();
            }

            return this.RedirectToAction("Login");

        }
    }
}
