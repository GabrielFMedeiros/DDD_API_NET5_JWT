using Application.Interfaces;
using Domain.Interfaces;
using Entities.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application
{
    public class UserApplication : IUserApplication
    {
        IUser _IUser;

        public UserApplication(IUser IUser)
        {
            _IUser = IUser;
        }
        public async Task<bool> AddUser(String name, String cpfCnpj, String email, String password, String age, String ddd, String phone, String cellphone, int role)
        {
            return await _IUser.AddUser(name, cpfCnpj, email, password, age, ddd, phone, cellphone, (RoleEnum)role);
        }

        public async Task<bool> ExistUser(String email, String passowrd)
        {
            return await _IUser.ExistUser(email, passowrd);
        }

        public async Task<User> GetUser(String login)
        {
            return await _IUser.GetUser(login);
        }
    }
}
