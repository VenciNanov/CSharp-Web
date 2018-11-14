using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chushka.ViewModels;
using SIS.HTTP.Responses;

namespace Chushka.Controllers
{
    public class HomeController:BaseController
    {
        public IHttpResponse Index()
        {
            if (this.User.Username!=null)
            {
                var products = this.Db.Products.Select(
                    x => new ProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description,
                    }).ToList();
                var model = new IndexViewModel
                {
                    Products = products,
                };

                return this.View("Home/IndexLoggedIn", model);
            }

            return this.View();
        }
    }
}
