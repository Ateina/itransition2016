using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Entities
{
    public class Category : IEntityBase
    {
        public Category()
        {
            Materials = new List<Material>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
    }
}
