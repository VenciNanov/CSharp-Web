using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CHUSHKA.Controllers.Base;
using CHUSHKA.Services.Contracts;
using CHUSHKA.ViewModels;
using SIS.Framework.ActionResults;

namespace CHUSHKA.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrdersService service;

        public OrdersController(IOrdersService service)
        {
            this.service = service;
        }

        public IActionResult All()
        {
            if (!this.AdminAuthorization())
            {
                return RedirectToAction("/");
            }
            List<AllOrderViewModel> orderViewModels =
                this.service.GetAllOrdres().Select(x => new AllOrderViewModel
                {
                    Id = x.Id.ToString(),
                    Customer = x.Client.Username,
                    OrderedOn = x.OrderedOn.ToString("dd-MM-yyyy",CultureInfo.InvariantCulture),
                    ProductName = x.Product.Name

                }).ToList();

            for (int i = 0; i < orderViewModels.Count; i++)
            {
                orderViewModels[i].Index = i + 1;
            }

            this.Model["AllOrders"] = orderViewModels;
            return this.View();
        }
    }
}
