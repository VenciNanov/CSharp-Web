using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.HTTP.Cookies;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Services;
using TORSHIA.Models;
using TORSHIA.Models.Enums;
using TORSHIA.ViewModels.Users;

namespace TORSHIA.Controllers
{
    public class UsersController:BaseController
    {
        private readonly IHashService hashService;

        public UsersController(IHashService hashService)
        {
            this.hashService = hashService;
        }

        public IHttpResponse Logout()
        {
            if (!this.Request.Cookies.ContainsCookie(".auth-cakes"))
            {
                return this.Redirect("/");
            }

            var cookie = this.Request.Cookies.GetCookie(".auth-cakes");
            cookie.Delete();
            this.Response.Cookies.Add(cookie);
            return this.Redirect("/");
        }

        public IHttpResponse Login()
        {
            if (this.User.IsLoggedIn)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public IHttpResponse Login(LoginViewModel model)
        {
            var hashedPassword = this.hashService.Hash(model.Password);

            var user = this.Db.Users.FirstOrDefault(x =>
                x.Username == model.Username.Trim() &&
                x.Password == hashedPassword);
            if (user == null)
            {
                return this.BadRequestErrorWithView("Invalid username or password.");
            }

            var mvcUser = new MvcUserInfo
            {
                Username = user.Username,
                Role = user.Role.ToString(),
                Info = user.Email,
            };

            var cookieContent = this.UserCookieService.GetUserCookie(mvcUser);

            var cookie = new HttpCookie(".auth-cakes", cookieContent, 7) { HttpOnly = true };
            this.Response.Cookies.Add(cookie);
            return this.Redirect("/");
        }


        public IHttpResponse Register()
        {
            if (this.User.IsLoggedIn)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public IHttpResponse Register(RegisterViewModel model)
        {
            var hashedPassword = this.hashService.Hash(model.Password);

            var role = Role.User;
            if (!this.Db.Users.Any())
            {
                role = Role.Admin;
            }

            var user = new User
            {
                Email = model.Email,
                Password = hashedPassword,
                Role = role,
                Username = model.Username
            };

            this.Db.Users.Add(user);

            try
            {
                this.Db.SaveChanges();

            }
            catch (Exception e)
            {
                return this.BadRequestErrorWithView(e.Message);
            }

            return this.Redirect("/Users/Login");
        }
    }
}
