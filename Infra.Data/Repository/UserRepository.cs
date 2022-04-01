using Domain.Interfaces;
using Entities.Entities;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUser
    {
        private readonly DbContextOptions<Context.DbContext> _options;
        public UserRepository()
        {
            _options = new DbContextOptions<Context.DbContext>();
        }

        public async Task<bool> AddUser(String name, String cpfCnpj, String email, String password, String age, String ddd, String phone, String cellphone, RoleEnum role)
        {
            try
            {
                using (var data = new Context.DbContext(_options))
                {
                    await data.User.AddAsync(
                        new User
                        {
                            NameCompany = name,
                            CpfCnpj = cpfCnpj,
                            Email = email,
                            PasswordHash = password,
                            Age = age,
                            DDD = ddd,
                            Phone = phone,
                            CellPhone = cellphone,
                            Role = role

                        });
                    await data.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ExistUser(string email, string passowrd)
        {
            try
            {
                using (var data = new Context.DbContext(_options))
                {
                    await data.User.Where(
                        u => u.Email.Equals(email) && u.PasswordHash.Equals(passowrd))
                        .AsNoTracking()
                        .AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<User> GetUser(string email)
        {
            try
            {
                using (var data = new Context.DbContext(_options))
                {
                    var entity = await data.User.Where(u => u.Email.Equals(email)).AsNoTracking().FirstOrDefaultAsync();

                    return entity;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
