using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChushkaWebApp.Models;
using ChushkaWebApp.Services.Contracts;
using ChushkaWebApp.ViewModels.Home;

namespace ChushkaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var products = this.productsService.GetAll<ProductInfoViewModel>();

            return View(products);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
