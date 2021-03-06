﻿using System;
using System.Collections.Generic;
using System.Text;
using SIS.Framework.ActionResults;
using SIS.Framework.ActionResults.Interfaces;
using SIS.Framework.Attributes.Method;
using SIS.Framework.Security;
using Torshia.Controllers.Base;
using Torshia.Services.Contracts;
using Torshia.ViewModels;
using Torshia.ViewModels.User;

namespace Torshia.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersSevice usersService;

        public UsersController(IUsersSevice usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Login() => this.View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var userExists = this.usersService.UserExistsByUsernameAndPassword(model.Username, model.Password);
            if (!userExists)
            {
               return RedirectToAction("/users/register");
            }
            this.SignIn(new IdentityUser { Username = model.Username });
            return RedirectToAction("/");
        }

        public IActionResult Register() => this.View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            this.usersService.RegisterUser(model.Username, model.Password, model.Email);
            this.SignIn(new IdentityUser
            {
                Email = model.Email,
                Username = model.Username
            });
            return RedirectToAction("/");
        }

        public IActionResult LogOut()
        {
            this.SignOut();
            return this.RedirectToAction("/");
        }
    }
}
