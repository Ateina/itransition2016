using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Entities;

namespace School.Data.Configurations
{
    public class CategoryConfiguration : EntityBaseConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(c => c.Name).IsRequired().HasMaxLength(50);
        }
    }
}
