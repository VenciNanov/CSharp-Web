using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChushkaWebApp.Services.Contracts
{
    public interface IAccountService
    {
        Task<bool> Register(string username,
            string password,
            string confirmPassword,
            string email,
            string fullName);

        Task<bool> Login(string username,
            string password);

        void Logout();
    }
}
