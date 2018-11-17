using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ChushkaWebApp.Data;
using ChushkaWebApp.Models;
using ChushkaWebApp.Services.Contracts;

namespace ChushkaWebApp.Services
{
    public class OrdersService:IOrdersService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public OrdersService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ICollection<T> All<T>()
        {
            var orders = this.context.Orders.ToList()
                .OrderByDescending(x => x.OrderedOn)
                .Select(x => this.mapper.Map<T>(x))
                .ToList();

            return orders;
        }

        public bool Order(string productId, string username)
        {
            var product = this.context.Products.Find(productId);
            if (product == null) return false;

            var user = this.context.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null) return false;

            var order = new Order
            {
                Product = product,
                Client = user,
                OrderedOn = DateTime.UtcNow
            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();

            return true;
        }
    }
}
