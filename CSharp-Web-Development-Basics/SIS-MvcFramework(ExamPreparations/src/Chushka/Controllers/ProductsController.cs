using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chushka.Models;
using Chushka.ViewModels;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using Type = Chushka.Models.Enum.Type;

namespace Chushka.Controllers
{
    public class ProductsController:BaseController
    {
        [Authorize]
        public IHttpResponse Order(string id)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (user == null)
            {
                return this.BadRequestError("Invalid user.");
            }

            var order = new Order
            {
                OrderedOn = DateTime.UtcNow,
                ProductId = id,
                ClientId = user.Id,
            };

            this.Db.Orders.Add(order);
            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        public IHttpResponse Details(string id)
        {
            var viewModel = this.Db.Products
                .Select(x => new ProductDetailsViewModel
                {
                    Type = x.Type.ToString(),
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price.ToString("F2"),
                    Description = x.Description,
                })
                .FirstOrDefault(x => x.Id == id);
            if (viewModel == null)
            {
                return this.BadRequestError("Invalid product id.");
            }

            return this.View(viewModel);
        }

        public IHttpResponse Create() => this.View();

        [HttpPost]
        public IHttpResponse Create(CreateProductViewModel model)
        {
            if (!Enum.TryParse(model.Type, out Type type))
            {
                return this.BadRequestErrorWithView("Invalid type.");
            }

            var product = new Product
            {
                Description = model.Description,
                Name = model.Name,
                Price = model.Price,
                Type = type,
            };

            this.Db.Products.Add(product);
            this.Db.SaveChanges();

            return this.Redirect("/Products/Details?id=" + product.Id);
        }
    }
}
