﻿using System;
using System.Collections.Generic;
using System.Text;
using TORSHIA.Models.Enums;

namespace TORSHIA.Models
{
    public class User
    {
        public User()
        {
            Reports=new List<Report>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public List<Report> Reports { get; set; }
    }
}
