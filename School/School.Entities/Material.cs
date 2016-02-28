using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Entities
{
    public class Material : IEntityBase
    {
        public Material()
        {
            Tags = new List<Tag>();
            Comments = new List<Comment>();
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }//содержание
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Author { get; set; }
        public DateTime CreateDate { get; set; }
        public byte Rating { get; set; }
        public string TrailerURI { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
