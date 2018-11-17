using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChushkaWebApp.Models;
using ChushkaWebApp.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace ChushkaWebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ChushkaUser> signInManager;
        private readonly UserManager<ChushkaUser> usernManager;

        public AccountService(UserManager<ChushkaUser> usernManager, SignInManager<ChushkaUser> signInManager)
        {
            this.usernManager = usernManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> Register(string username, string password, string confirmPassword, string email, string fullName)
        {
            if (username == null ||
                password == null ||
                confirmPassword == null ||
                email == null ||
                fullName == null)
                return false;

            if (password != confirmPassword)
            {
                return false;
            }

            var user = new ChushkaUser
            {
                UserName = username,
                Email = email,
                FullName = fullName
            };

            var userCreateResult = await this.usernManager.CreateAsync(user, password);

            if (!userCreateResult.Succeeded)
            {
                return false;
            }

            IdentityResult addRoleResult = null;

            if (this.usernManager.Users.Count() == 1)
            {
                addRoleResult = await this.usernManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                addRoleResult = await this.usernManager.AddToRoleAsync(user, "User");
            }

            if (!addRoleResult.Succeeded) return false;

            return true;
        }

        public async Task<bool> Login(string username, string password)
        {
            var user = this.GetUser(username).Result;

            if (user == null)
            {
                return false;
            }

            var result =
                await this.signInManager.PasswordSignInAsync(user, password, isPersistent: false,
                    lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task<ChushkaUser> GetUser(string username)
        {
            var user = await this.usernManager.FindByNameAsync(username);

            return user;
        }

        public async void Logout()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}
