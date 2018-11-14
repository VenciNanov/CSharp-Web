using System;
using System.Collections.Generic;
using System.Text;

namespace MISHMASH.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Channels=new List<ChannelTag>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ChannelTag> Channels { get; set; }
    }
}
