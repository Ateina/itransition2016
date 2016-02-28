using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Entities;
using School.Data.Repositories;

namespace School.Data.Extensions
{
    public static class UserExtensions
    {
        public static User GetSingleByUsername(this IEntityBaseRepository<User> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Username == username);
        }
    }
}
