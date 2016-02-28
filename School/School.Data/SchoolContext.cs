using School.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Data.Configurations;

namespace School.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(): base("School")
        {
            Database.SetInitializer<SchoolContext>(null);
        }

        #region Entity Sets
        public IDbSet<User> UserSet { get; set; }
        public IDbSet<Role> RoleSet { get; set; }
        public IDbSet<UserRole> UserRoleSet { get; set; }
        public IDbSet<Material> MaterialSet { get; set; }
        public IDbSet<Category> CategorySet { get; set; }
        public IDbSet<Tag> TagSet { get; set; }
        public IDbSet<Medal> MedalSet { get; set; }
        public IDbSet<Error> ErrorSet { get; set; }
        public IDbSet<Comment> CommentSet { get; set; }
        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new MaterialConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new MedalConfiguration());
        }
    }
}
