using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Entities
{
    public class User : IEntityBase
    {
        public User()
        {
            UserRoles = new List<UserRole>();
        }
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Hobby { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public bool IsLocked { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
