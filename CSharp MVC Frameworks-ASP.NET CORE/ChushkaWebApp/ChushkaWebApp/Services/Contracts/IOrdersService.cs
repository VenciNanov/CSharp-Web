using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChushkaWebApp.Services.Contracts
{
    public interface IOrdersService
    {
        ICollection<T> All<T>();

        bool Order(string productId, string username);
    }
}
