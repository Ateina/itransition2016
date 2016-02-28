using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Entities;

namespace School.Data.Configurations
{
    public class CommentConfiguration : EntityBaseConfiguration<Comment>
    {
        public CommentConfiguration()
        {
            Property(c => c.Text).IsRequired().HasMaxLength(100);
            Property(c => c.Author).IsRequired().HasMaxLength(100);
            Property(u => u.Date);
            Property(m => m.countOfMinus).IsRequired();
            Property(m => m.countOfPlus).IsRequired();
            Property(s => s.MaterialId).IsRequired();
        }
    }
}
