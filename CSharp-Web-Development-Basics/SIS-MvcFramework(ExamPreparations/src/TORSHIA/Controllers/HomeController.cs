using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Responses;

namespace TORSHIA.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            if (this.User.Username==null)
            {
                return this.View("/Home/GuestIndex");
            }

            var task = Db.Tasks.Include(x => x.AffectedSectors).Where(x => x.IsReported == false).ToArray();

            return this.View(task);
        }
    }
}
