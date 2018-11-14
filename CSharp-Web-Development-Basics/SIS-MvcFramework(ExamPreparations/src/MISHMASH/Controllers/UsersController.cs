using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MISHMASH.Models;
using MISHMASH.Models.Enums;
using MISHMASH.ViewModels;
using SIS.HTTP.Cookies;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace MISHMASH.Controllers
{
    public class UsersController : BaseController
    {
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
          return  this.View();
        }
        [HttpPost]
        public IHttpResponse Login(LoginViewModel model)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user==null)
            {
                return this.BadRequestErrorWithView("Invalid username or password");
            }

            var mvcUser = new MvcUserInfo
            {
                Username = user.Username,
                Info = user.Email,
                Role = user.Role.ToString()
            };

            var cookieContent = this.UserCookieService.GetUserCookie(mvcUser);

            var cookie = new HttpCookie(".auth-cakes", cookieContent, 7) { HttpOnly = true };
            this.Response.Cookies.Add(cookie);
            return this.Redirect("/");
        }



        public IHttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public IHttpResponse Register(RegisterViewModel model)
        {
            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                Role = !this.Db.Users.Any()? Role.Admin:Role.User
            };

            this.Db.Users.Add(user);
            this.Db.SaveChanges();

            return this.Redirect("/Users/Login");
        }
    }
}
