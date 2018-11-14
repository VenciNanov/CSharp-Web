using System;
using System.Collections.Generic;
using System.Text;
using Chushka.Models.Enum;

namespace Chushka.Models
{
    public class User
    {
        public User()
        {
            Orders=new List<Order>();
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
