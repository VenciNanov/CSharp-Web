using System;
using System.Collections.Generic;
using System.Text;
using CHUSHKA.Models;
using CHUSHKA.ViewModels;

namespace CHUSHKA.Services.Contracts
{
    public interface IUsersService
    {
        void CreateUser(RegisterViewModel model);

        User GetUserByUsername(LoginViewModl model);

        User GetUserByUsername(string model);
    }
}
