using System;
using System.Collections.Generic;
using System.Text;
using CHUSHKA.Models;

namespace CHUSHKA.Services.Contracts
{
    public interface IOrdersService
    {
        ICollection<Order> GetAllOrdres();
    }
}
