using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using School.Entities;

namespace School.Data.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username).IsRequired().HasMaxLength(100);
            Property(u => u.Email).IsRequired().HasMaxLength(200);
            Property(u => u.Hobby).IsRequired().HasMaxLength(400);
            Property(u => u.City).IsRequired().HasMaxLength(20);
            Property(u => u.HashedPassword).IsRequired().HasMaxLength(200);
            Property(u => u.Salt).IsRequired().HasMaxLength(200);
            Property(u => u.IsLocked).IsRequired();
            Property(u => u.DateOfBirth);
            Property(u => u.RegistrationDate);
        }
    }
}
