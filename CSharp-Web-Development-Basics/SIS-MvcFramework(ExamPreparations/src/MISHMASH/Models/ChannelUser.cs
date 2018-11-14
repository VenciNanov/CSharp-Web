using System;
using System.Collections.Generic;
using System.Text;

namespace MISHMASH.Models
{
   public class ChannelUser
    {
        public int Id { get; set; }

        public int ChannelId { get; set; }
        public virtual Channel Channel { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
