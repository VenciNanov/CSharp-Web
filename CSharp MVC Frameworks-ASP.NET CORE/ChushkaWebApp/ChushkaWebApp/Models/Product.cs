using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChushkaWebApp.Models.Enums;

namespace ChushkaWebApp.Models
{
    public class Product
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
