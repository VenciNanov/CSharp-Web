using System;
using System.Collections.Generic;
using System.Text;
using MISHMASH.ViewModels;
using SIS.HTTP.Responses;

namespace MISHMASH.Controllers
{
   public class HomeController:BaseController
    {
        public IHttpResponse Index()
        {
            if (this.User.Username!=null)
            {
                var model = new IndexViewModel
                {
                    Username = this.User.Username
                };

                return this.View("Home/IndexLoggedIn",model);
            }

            return this.View();
        }

    }
}
