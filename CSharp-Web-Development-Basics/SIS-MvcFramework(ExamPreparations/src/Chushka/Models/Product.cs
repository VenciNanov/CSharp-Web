using System.Collections.Generic;
using Type = Chushka.Models.Enum.Type;

namespace Chushka.Models
{
    public class Product
    {
        public Product()
        {
            Orders=new HashSet<Order>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
