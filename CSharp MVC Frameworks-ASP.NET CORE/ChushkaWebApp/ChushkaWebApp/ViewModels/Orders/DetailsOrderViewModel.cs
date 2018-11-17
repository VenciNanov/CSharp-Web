using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChushkaWebApp.ViewModels.Orders
{
    public class DetailsOrderViewModel
    {
        public string Id { get; set; }

        public string Client { get; set; }

        public string Product { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
