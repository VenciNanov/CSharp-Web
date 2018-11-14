using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MISHMASH.Models;
using MISHMASH.ViewModels;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using Type = MISHMASH.Models.Enums.Type;

namespace MISHMASH.Controllers
{
    public class ChannelsController : BaseController
    {
        public IHttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public IHttpResponse Create(ChannelCreateViewModel model)
        {
            if (!Enum.TryParse(model.Type, true, out Type type))
            {
                return this.BadRequestErrorWithView("Invalid channelType");
            }

            var channel = new Channel
            {
                Name = model.Name,
                Description = model.Description,
                Type = type
            };
             
            if (!string.IsNullOrWhiteSpace(model.Tags))
            {
                var tags = model.Tags.Split(',', ';', StringSplitOptions.RemoveEmptyEntries);

                foreach (var tag in tags)
                {

                    var dbTag = this.Db.Tags.FirstOrDefault(x => x.Name == tag.Trim());
                    if (dbTag == null)
                    {
                        dbTag = new Tag { Name = tag.Trim() };
                        this.Db.Tags.Add(dbTag);
                        this.Db.SaveChanges();
                    }

                    channel.Tags.Add(new ChannelTag
                    {
                        TagId = dbTag.Id
                    });
                }

                Db.Channels.Add(channel);
                Db.SaveChanges();
            }

            return this.Redirect("/");
        }
    }
}
