namespace School.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Material",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 400),
                        Content = c.String(nullable: false, maxLength: 2000),
                        Image = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Author = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                        Rating = c.Byte(nullable: false),
                        TrailerURI = c.String(maxLength: 200),
                        Tag_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.Tag_ID)
                .Index(t => t.CategoryId)
                .Index(t => t.Tag_ID);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 100),
                        Author = c.String(nullable: false, maxLength: 100),
                        MaterialId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        countOfMinus = c.Int(nullable: false),
                        countOfPlus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Material", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        MaterialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Material", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Medal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ImageSRC = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 200),
                        Hobby = c.String(nullable: false, maxLength: 400),
                        City = c.String(nullable: false, maxLength: 20),
                        DateOfBirth = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        HashedPassword = c.String(nullable: false, maxLength: 200),
                        Salt = c.String(nullable: false, maxLength: 200),
                        IsLocked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Tag", "MaterialId", "dbo.Material");
            DropForeignKey("dbo.Material", "Tag_ID", "dbo.Tag");
            DropForeignKey("dbo.Comment", "MaterialId", "dbo.Material");
            DropForeignKey("dbo.Material", "CategoryId", "dbo.Category");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Tag", new[] { "MaterialId" });
            DropIndex("dbo.Comment", new[] { "MaterialId" });
            DropIndex("dbo.Material", new[] { "Tag_ID" });
            DropIndex("dbo.Material", new[] { "CategoryId" });
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
            DropTable("dbo.Medal");
            DropTable("dbo.Error");
            DropTable("dbo.Tag");
            DropTable("dbo.Comment");
            DropTable("dbo.Material");
            DropTable("dbo.Category");
        }
    }
}
