using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string ProductName { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
