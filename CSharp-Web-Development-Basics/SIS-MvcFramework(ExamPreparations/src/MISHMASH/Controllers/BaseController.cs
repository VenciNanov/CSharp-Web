using System;
using System.Collections.Generic;
using System.Text;
using MISHMASH.Data;
using SIS.MvcFramework;

namespace MISHMASH.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
            this.Db = new MishMashDbContext();

        }

        public MishMashDbContext Db { get; set; }
    }
}
