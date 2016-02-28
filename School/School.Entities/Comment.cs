using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Entities
{
    public class Comment : IEntityBase
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public int MaterialId { get; set; }
        public DateTime Date { get; set; }
        public int countOfMinus { get; set; }
        public int countOfPlus { get; set; }
    }
}
