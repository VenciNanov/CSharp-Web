using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChushkaWebApp.Services.Contracts
{
   public interface IProductsService
   {
        string Create(string name, decimal price, string description, string type);

       T Details<T>(string id);

       void Delete(string id);

       bool Edit(string id, string name, decimal price, string description, string type);

       ICollection<T> GetAll<T>();

   }
}
