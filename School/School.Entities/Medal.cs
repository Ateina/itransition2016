using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Entities
{
    public class Medal : IEntityBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageSRC { get; set; }
    }
}
