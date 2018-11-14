using System;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ClientId { get; set; }
        public virtual User Client { get; set; }

        public DateTime OrderedOn { get; set; } = DateTime.UnixEpoch;
    }
}
