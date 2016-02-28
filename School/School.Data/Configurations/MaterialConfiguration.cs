using School.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Configurations
{
    public class MaterialConfiguration : EntityBaseConfiguration<Material>
    {
        public MaterialConfiguration()
        {
            Property(m => m.Title).IsRequired().HasMaxLength(100);
            Property(m => m.CategoryId).IsRequired();
            Property(m => m.Author).IsRequired().HasMaxLength(100);
            Property(m => m.Rating).IsRequired();
            Property(m => m.Description).IsRequired().HasMaxLength(800);
            Property(m => m.Content).IsRequired();
            Property(m => m.TrailerURI).HasMaxLength(200);
            HasMany(m => m.Tags).WithRequired().HasForeignKey(s => s.MaterialId);
            HasMany(m => m.Comments).WithRequired().HasForeignKey(s => s.MaterialId);
        }
    }
}
