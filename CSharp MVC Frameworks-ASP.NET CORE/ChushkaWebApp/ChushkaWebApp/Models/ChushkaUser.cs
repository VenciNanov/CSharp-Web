using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ChushkaWebApp.Models
{
    public class ChushkaUser:IdentityUser<string>
    {
        public ChushkaUser()
        {
            this.Orders = new HashSet<Order>();
        }

        public string FullName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
