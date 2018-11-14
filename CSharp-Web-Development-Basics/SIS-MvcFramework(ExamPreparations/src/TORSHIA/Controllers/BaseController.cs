using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
using TORSHIA.Data;

namespace TORSHIA.Controllers
{
   public abstract class BaseController:Controller
    {
        protected BaseController()
        {
            this.Db = new TorshiaDbContext();
        }

        public TorshiaDbContext Db { get; }
    }
}
