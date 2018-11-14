using System;
using System.Collections.Generic;
using System.Text;
using MISHMASH.Models.Enums;

namespace MISHMASH.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Channel> FollowedChanes{ get; set; }

        public Role Role { get; set; }
    }
}
