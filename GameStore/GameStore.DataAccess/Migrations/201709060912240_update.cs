namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Login = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDay = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsSqlEntity = c.Boolean(nullable: false),
                        Publisher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PublisherEntities", t => t.Publisher_Id)
                .Index(t => t.Publisher_Id);
            
            CreateTable(
                "dbo.RoleEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Role = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsSqlEntity = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleEntityUserEntities",
                c => new
                    {
                        RoleEntity_Id = c.String(nullable: false, maxLength: 128),
                        UserEntity_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleEntity_Id, t.UserEntity_Id })
                .ForeignKey("dbo.RoleEntities", t => t.RoleEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserEntities", t => t.UserEntity_Id, cascadeDelete: true)
                .Index(t => t.RoleEntity_Id)
                .Index(t => t.UserEntity_Id);
            
            AddColumn("dbo.CommentEntities", "IsDisabled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.OrderEntities", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.OrderEntities", "CustomerId");
            AddForeignKey("dbo.OrderEntities", "CustomerId", "dbo.UserEntities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderEntities", "CustomerId", "dbo.UserEntities");
            DropForeignKey("dbo.RoleEntityUserEntities", "UserEntity_Id", "dbo.UserEntities");
            DropForeignKey("dbo.RoleEntityUserEntities", "RoleEntity_Id", "dbo.RoleEntities");
            DropForeignKey("dbo.UserEntities", "Publisher_Id", "dbo.PublisherEntities");
            DropIndex("dbo.RoleEntityUserEntities", new[] { "UserEntity_Id" });
            DropIndex("dbo.RoleEntityUserEntities", new[] { "RoleEntity_Id" });
            DropIndex("dbo.UserEntities", new[] { "Publisher_Id" });
            DropIndex("dbo.OrderEntities", new[] { "CustomerId" });
            AlterColumn("dbo.OrderEntities", "CustomerId", c => c.String());
            DropColumn("dbo.CommentEntities", "IsDisabled");
            DropTable("dbo.RoleEntityUserEntities");
            DropTable("dbo.RoleEntities");
            DropTable("dbo.UserEntities");
        }
    }
}