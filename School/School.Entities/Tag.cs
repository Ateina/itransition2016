using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Entities
{
    public class Tag : IEntityBase
    {
        public Tag()
        {
            Materials = new List<Material>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public int MaterialId { get; set; }
    }
}
