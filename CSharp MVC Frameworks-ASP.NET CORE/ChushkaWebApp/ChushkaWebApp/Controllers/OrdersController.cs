using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChushkaWebApp.Services.Contracts;
using ChushkaWebApp.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace ChushkaWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService service;

        public OrdersController(IOrdersService service)
        {
            this.service = service;
        }

        public IActionResult Order(string id)
        {
            var username = this.User.Identity.Name;

            var isOrdered = this.service.Order(id, username);

            if (!isOrdered)
                return this.Redirect("/");

            if (this.User.IsInRole("Admin"))
            {
                return this.RedirectToAction(nameof(All));
            }

            return this.Redirect("/");

        }

        public IActionResult All()
        {
            var orders = this.service.All<DetailsOrderViewModel>();

            return this.View(orders);
        }
    }
}