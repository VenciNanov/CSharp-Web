using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Torshia.Data;
using Torshia.Models;
using Torshia.Models.Enums;
using Torshia.Services.Contracts;

namespace Torshia.Services
{
   public class UsersService:IUsersSevice
   {
       private readonly TorshiaDbContext context;

       public UsersService(TorshiaDbContext context)
       {
           this.context = context;
       }

       public void RegisterUser(string usename, string password, string email)
       {
           var role = !this.context.Users.Any() ? Role.Admin : Role.User;

           var user = new User
           {
               Username = usename,
               Email = email,
               Password = password,
               Role = role
           };
           this.context.Users.Add(user);
           this.context.SaveChanges();
       }

       public bool UserExistsByUsernameAndPassword(string username, string password) =>
           this.context.Users
               .Any(x => x.Username == username &&
                         x.Password == password);
   }
}
