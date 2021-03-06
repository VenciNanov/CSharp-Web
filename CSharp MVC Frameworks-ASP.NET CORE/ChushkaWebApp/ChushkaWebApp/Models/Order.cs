﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChushkaWebApp.Models
{
    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public virtual ChushkaUser Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
