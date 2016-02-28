using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using School.Entities;

namespace School.Data.Configurations
{
    public class RoleConfiguration : EntityBaseConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(ur => ur.Name).IsRequired().HasMaxLength(50);
        }
    }
}
