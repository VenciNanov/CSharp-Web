using System;
using System.Collections.Generic;
using System.Text;
using Type = CHUSHKA.Models.Enums.Type;

namespace CHUSHKA.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
