using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CHUSHKA.Data;
using CHUSHKA.Models.Enums;
using CHUSHKA.Services.Contracts;
using SIS.Framework.ActionResults;
using SIS.Framework.Controllers;

namespace CHUSHKA.Controllers.Base
{
   public class BaseController:Controller
   {
       private readonly ChuskaDbContext db;
        //TODO: Authorization, Checkboxes fix
       public BaseController()
       {
           this.db = new ChuskaDbContext();
       }

       protected override IViewable View([CallerMemberName] string actionName = "")
        {
            
            if (this.Identity!=null)
            {
                var user = this.Identity.Username;
                var role = this.db.Users.First(x => x.Username == user).Role;
                if (role==Role.User)
                {
                    this.Model.Data["Guest"] = "none";
                    this.Model.Data["LoggedIn"] = "block";
                    this.Model.Data["Admin"] = "none";
                    this.Model.Data["Username"] = user;

                }
                else if (role==Role.Admin)
                {
                    this.Model.Data["Guest"] = "none";
                    this.Model.Data["Admin"] = "block";
                    this.Model.Data["LoggedIn"] = "none";
                    this.Model.Data["Username"] = user;
                }
            }
            else
            {
                this.Model.Data["LoggedIn"] = "none";
                this.Model.Data["Guest"] = "block";
                this.Model.Data["Admin"] = "none";
            }
            return base.View(actionName);
        }

       protected bool AdminAuthorization()
       {
           if (this.Identity != null)
           {
               var user = this.Identity.Username;
               var role = this.db.Users.First(u => u.Username == user).Role;
               if (role == Role.Admin)
               {
                   return true;
               }

               return false;
           }

           return false;
       }

       protected bool UserAuthorization()
       {
           if (this.Identity != null)
           {
               var user = this.Identity.Username;
               var role = this.db.Users.First(u => u.Username == user).Role;
               if (role == Role.User)
               {
                   return true;
               }

               return false;
           }
           return false;
       }
    }
}
