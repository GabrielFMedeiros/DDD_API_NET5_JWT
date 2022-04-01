using Entities.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUser
    {
        Task<bool> AddUser(String name, String cpfCnpj, String email, String password, String age, String ddd, String phone, String cellphone, RoleEnum role);
        Task<bool> ExistUser(String email, String passowrd);
        Task<User> GetUser(String email);
    }
}
