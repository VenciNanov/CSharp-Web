using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHUSHKA.Data;
using CHUSHKA.Models;
using CHUSHKA.Services.Contracts;
using CHUSHKA.ViewModels;
using Microsoft.EntityFrameworkCore;
using SIS.Framework.ActionResults.Implementations;
using SIS.Framework.Attributes.Method;

namespace CHUSHKA.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ChuskaDbContext context;

        public ProductsService(ChuskaDbContext context)
        {
            this.context = context;
        }

        public ICollection<Product> GetAllNotDeletedProducts()
        {
            return this.context
                .Products
                .Where(x => x.IsDeleted == false)
                .ToList();
        }

        public Product GetProductById(int id)
        {
            return this.context.Products.FirstOrDefault(x => x.Id == id);
        }

        public void CreateOrder(Order order)
        {
            this.context.Orders.Add(order);
            this.context.SaveChanges();
        }

        public Product CreateProduct(ProductCreateViewModel model)
        {
            if (!Enum.TryParse(model.Type, out Models.Enums.Type type))
            {
                throw new InvalidCastException("Invalid Type!");
            }

            var product = new Product
            {
                Description = model.Description,
                Name = model.Name,
                Price = decimal.Parse(model.Price),
                Type = type
            };

            this.context.Products.Add(product);
            this.context.SaveChanges();

            return product;
        }

        public ProductEditDeleteViewModel EditViewModel(Product product)
        {
            var checkedHtmlButton = @"checked=""checked""";

            var productTypes = new List<Models.Enums.Type>
            {
                Models.Enums.Type.Food,
                Models.Enums.Type.Domestic,
                Models.Enums.Type.Health,
                Models.Enums.Type.Cosmetic,
                Models.Enums.Type.Other
            };
            var viewModel = new ProductEditDeleteViewModel
            {
                Description = product.Description,
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price.ToString("F2"),

            };
            //for (int i = 0; i < 5; i++)

            //{
            //    if (viewModel.Food==product.Type.ToString())
            //    {
            //        viewModel.Food= checkedHtmlButton;

            //    }
            //    else if (viewModel.Domestic == product.Type.ToString())
            //    {
            //        viewModel.Domestic = checkedHtmlButton;

            //    }
            //    else if (viewModel.Health == product.Type.ToString())
            //    {
            //        viewModel.Health = checkedHtmlButton;

            //    }
            //    else if (viewModel.Cosmetic == product.Type.ToString())
            //    {
            //        viewModel.Cosmetic = checkedHtmlButton;

            //    }
            //    else if (viewModel.Other == product.Type.ToString())
            //    {
            //        viewModel.Food = checkedHtmlButton;

            //    }
            //}

            return viewModel;

        }

        public Product UpdateProduct(UpdateProductViewModel model, Product product)
        {

            if (!Enum.TryParse(model.Type, out Models.Enums.Type type))
            {
                throw new InvalidCastException("Invalid Type!");
            }

            product.Type = type;
            product.Description = model.Description;
            product.Name = model.Name;
            product.Price = decimal.Parse(model.Price);

            this.context.SaveChanges();

            return product;
        }

        public ProductDeleteViewModel DeleteProduct(Product product)
        {
            var viewModel = new ProductDeleteViewModel
            {
                Description = product.Description,
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.Price.ToString("F2"),

            };

            return viewModel;
        }



        public void DeleteProduct(UpdateProductViewModel model, Product product)
        {
            this.context.Products.Remove(product);

            this.context.SaveChanges();
        }
    }
}
