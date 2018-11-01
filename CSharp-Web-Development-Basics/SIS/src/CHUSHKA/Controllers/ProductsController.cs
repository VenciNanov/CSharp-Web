using System;
using System.Collections.Generic;
using System.Text;
using CHUSHKA.Controllers.Base;
using CHUSHKA.Models;
using CHUSHKA.Services.Contracts;
using CHUSHKA.ViewModels;
using SIS.Framework.ActionResults;
using SIS.Framework.Attributes.Method;

namespace CHUSHKA.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductsService service;
        private readonly IUsersService usersService;

        public ProductsController(IProductsService service, IUsersService usersService)
        {
            this.service = service;
            this.usersService = usersService;
        }

        public IActionResult Details()
        {
            if (this.Identity == null)
            {
                return RedirectToAction("/");
            }
            var id = this.Request.QueryData["id"].ToString();

            Product product = this.service.GetProductById(int.Parse(id));

            if (product == null)
            {
                return this.RedirectToAction("/");
            }

            ProductDetailsViewModel viewModel = new ProductDetailsViewModel
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString("F2"),
                Type = product.Type.ToString()
            };

            this.Model["Product"] = viewModel;
            return this.View();
        }



        public IActionResult Order()
        {
            if (this.Identity == null)
            {
                return RedirectToAction("/");
            }
            var id = this.Request.QueryData["id"].ToString();
            Product product = this.service.GetProductById(int.Parse(id));

            var user = this.usersService.GetUserByUsername(this.Identity.Username);

            if (user == null)
            {
                return this.RedirectToAction("/");
            }

            var order = new Order
            {
                OrderedOn = DateTime.UtcNow,
                ProductId = product.Id,
                ClientId = user.Id
            };

            this.service.CreateOrder(order);

            return this.RedirectToAction("/");
        }

        public IActionResult Create()
        {
            if (!this.AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (!this.AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            Product product = service.CreateProduct(model);

            return this.RedirectToAction("/Products/Details?=id=" + product.Id);
        }

        public IActionResult Edit()
        {
            if (!this.AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            Product product = GetProductById();

            var viewModel = service.EditViewModel(product);

            this.Model["ProductEditDelete"] = viewModel;

            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(UpdateProductViewModel model)
        {
            if (!this.AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            Product product = GetProductById();

            service.UpdateProduct(model, product);

            return this.RedirectToAction("/Products/Details?=id=" + product.Id);
        }

        public IActionResult Delete()
        {
            if (!this.AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            var product = GetProductById();

            var viewModel = service.DeleteProduct(product);

            this.Model["ProductDelete"] = viewModel;


            return this.View();
        }

        [HttpPost]
        public IActionResult Delete(UpdateProductViewModel model)
        {
            if (!this.AdminAuthorization())
            {
                return RedirectToAction("/");
            }

            var product = GetProductById();

            service.DeleteProduct(model, product);

            return RedirectToAction("/");
        }

        private Product GetProductById()
        {
            var id = this.Request.QueryData["id"].ToString();

            Product product = this.service.GetProductById(int.Parse(id));
            return product;
        }
    }
}
