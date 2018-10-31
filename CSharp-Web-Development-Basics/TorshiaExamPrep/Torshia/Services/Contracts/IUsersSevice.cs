using System;
using System.Collections.Generic;
using System.Text;

namespace Torshia.Services.Contracts
{
   public interface IUsersSevice
   {
       void RegisterUser(string usename, string password, string email);

       bool UserExistsByUsernameAndPassword(string username, string password);
   }
}
