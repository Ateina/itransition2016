﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using School.Entities;

namespace School.Data.Configurations
{
    public class TagConfiguration : EntityBaseConfiguration<Tag>
    {
        public TagConfiguration()
        {
            Property(s => s.MaterialId).IsRequired();
            Property(g => g.Name).IsRequired().HasMaxLength(50);
        }
    }
}
