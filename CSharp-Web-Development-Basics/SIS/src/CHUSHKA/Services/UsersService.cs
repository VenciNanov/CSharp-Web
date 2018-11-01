using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHUSHKA.Data;
using CHUSHKA.Models;
using CHUSHKA.Models.Enums;
using CHUSHKA.Services.Contracts;
using CHUSHKA.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CHUSHKA.Services
{
    public class UsersService:IUsersService
    {
        private readonly ChuskaDbContext _db;

        public UsersService(ChuskaDbContext db)
        {
            _db = db;
        }

        public void CreateUser(RegisterViewModel model)
        {
            User user = new User
            {
                Username = model.Username,
                Password = model.Password,
                FullName = model.FullName,
                Email = model.Email,
                Role = !this._db.Users.Any() ? Role.Admin : Role.User
            };

            this._db.Users.Add(user);
            this._db.SaveChanges();
        }

        public User GetUserByUsername(LoginViewModl model)
        {
            return this._db.Users.FirstOrDefault(x => x.Username == model.Username&&x.Password==model.Password);
          
        }

        public User GetUserByUsername(string username)
        {
            return this._db.Users.FirstOrDefault(x => x.Username == username);
        }

       
    }
}
