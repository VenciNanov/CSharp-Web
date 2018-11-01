using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHUSHKA.Controllers.Base;
using CHUSHKA.Services;
using CHUSHKA.Services.Contracts;
using CHUSHKA.ViewModels;
using SIS.Framework.ActionResults;

namespace CHUSHKA.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            if (this.Identity==null)
            {
                return this.View();
            }

            List<IndexProductViewModel> productViewModels = this.productsService.GetAllNotDeletedProducts().Select(x =>
                new IndexProductViewModel
                {
                    Id =x.Id.ToString(),
                    Description = x.Description,
                    Name = x.Name,
                    Price = x.Price.ToString("F2")

                }).ToList();

            List<IndexProductsRowViewModel> productsRowViewModels = new List<IndexProductsRowViewModel>();

            for (int i = 0; i < productViewModels.Count; i++)
            {
                if (i % 5 == 0)
                {
                    productsRowViewModels.Add(new IndexProductsRowViewModel());
                }

                productsRowViewModels[productsRowViewModels.Count-1].Products.Add(productViewModels[i]);
            }

            this.Model["ProductRows"] = productsRowViewModels;

            return this.View();
        }

       
    }
}
