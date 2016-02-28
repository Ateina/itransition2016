using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using School.Entities;

namespace School.Data.Configurations
{
    public class MedalConfiguration : EntityBaseConfiguration<Medal>
    {
        public MedalConfiguration()
        {
            Property(m => m.Name).IsRequired().HasMaxLength(100);
            Property(m => m.ImageSRC).IsRequired().HasMaxLength(100);
        }
    }
}
