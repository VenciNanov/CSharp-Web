using System.Collections.Generic;
using CHUSHKA.Models;
using CHUSHKA.ViewModels;

namespace CHUSHKA.Services.Contracts
{
    public interface IProductsService
    {
        ICollection<Product> GetAllNotDeletedProducts();

        Product GetProductById(int id);

        void CreateOrder(Order order);

        Product CreateProduct(ProductCreateViewModel model);

        ProductEditDeleteViewModel EditViewModel(Product product);

        Product UpdateProduct(UpdateProductViewModel model, Product product);

        void DeleteProduct(UpdateProductViewModel model,Product product);

        ProductDeleteViewModel DeleteProduct(Product product);
    }
}
