using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHUSHKA.Data;
using CHUSHKA.Models;
using CHUSHKA.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SIS.Framework.ActionResults;

namespace CHUSHKA.Services
{
    public class OrdersService:IOrdersService
    {
        private readonly ChuskaDbContext context;

        public OrdersService(ChuskaDbContext context)
        {
            this.context = context;
        }

        public ICollection<Order> GetAllOrdres()
        {
            return this.context.Orders
                .Include(x=>x.Product)
                .Include(x=>x.Client)
                .ToList();
        }
    }
}
