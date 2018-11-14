using System.Collections.Generic;
using Type = MISHMASH.Models.Enums.Type;

namespace MISHMASH.Models
{
    public class Channel
    {
        public Channel()
        {
            Tags = new List<ChannelTag>();
            Followers = new List<ChannelUser>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public virtual ICollection<ChannelTag> Tags { get; set; }

        public virtual ICollection<ChannelUser> Followers { get; set; }
    }
}
