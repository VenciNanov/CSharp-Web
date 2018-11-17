using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChushkaWebApp.ViewModels.Product
{
    public class DetailsProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
