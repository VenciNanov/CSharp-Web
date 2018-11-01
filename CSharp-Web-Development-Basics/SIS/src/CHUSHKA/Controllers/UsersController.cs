using System;
using System.Collections.Generic;
using System.Text;
using CHUSHKA.Controllers.Base;
using CHUSHKA.Models;
using CHUSHKA.Services.Contracts;
using CHUSHKA.ViewModels;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Method;
using SIS.Framework.Security;

namespace CHUSHKA.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        public IActionResult Login() => this.View();

        [HttpPost]
        public IActionResult Login(LoginViewModl model)
        {
            User user = service.GetUserByUsername(model);

            this.SignIn(new IdentityUser
            {
                Username = user.Username
            });

            return this.RedirectToAction("/Home/Index");
        }

        public IActionResult Register() => this.View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            this.service.CreateUser(model);
            return this.RedirectToAction("/Users/Login");
        }

        public IActionResult Logout()
        {
            this.SignOut();
            return this.RedirectToAction("/");
        }
    }
}
