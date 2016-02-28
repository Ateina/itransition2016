namespace School.Data.Migrations
{
    using School.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<School.Data.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(School.Data.SchoolContext context)
        {
            /*
            //  create genres
            context.CategorySet.AddOrUpdate(g => g.Name, GenerateCategories());

            // create movies
            //context.MaterialSet.AddOrUpdate(m => m.Title, GenerateMaterials());

            //// create stocks
            //context.StockSet.AddOrUpdate(GenerateStocks());

            // create customers
            //context.CustomerSet.AddOrUpdate(GenerateCustomers());

            // create roles
            //context.RoleSet.AddOrUpdate(r => r.Name, GenerateRoles());

            // username: chsakell, password: homecinema
            context.UserSet.AddOrUpdate(u => u.Email, new User[]{
                new User()
                {
                    Email="chsakells.blog@gmail.com",
                    Username="chsakell",
                    HashedPassword ="XwAQoiq84p1RUzhAyPfaMDKVgSwnn80NCtsE8dNv3XI=",
                    Salt = "mNKLRbEFCH8y1xIyTXP4qA==",
                    IsLocked = false,
                    RegistrationDate = DateTime.Now
                }
            });

            // // create user-admin for chsakell
            context.UserRoleSet.AddOrUpdate(new UserRole[] {
                new UserRole() {
                    RoleId = 1, // admin
                    UserId = 1  // chsakell
                }
            });
             */
        }
        /*
        private Category[] GenerateCategories()
        {
            Category[] categories = new Category[] {
                new Category() { Name = "Bio" },
                new Category() { Name = "Math" },
                new Category() { Name = "Chem" },
                new Category() { Name = "Lan" },
            };

            return categories;
        }*/
    }
}
