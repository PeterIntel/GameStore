namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserEntity_and_RoleEntity : DbMigration
    {
        public override void Up()
        {
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
                "dbo.UserEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Login = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDay = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsSqlEntity = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserEntityRoleEntities",
                c => new
                    {
                        UserEntity_Id = c.String(nullable: false, maxLength: 128),
                        RoleEntity_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserEntity_Id, t.RoleEntity_Id })
                .ForeignKey("dbo.UserEntities", t => t.UserEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.RoleEntities", t => t.RoleEntity_Id, cascadeDelete: true)
                .Index(t => t.UserEntity_Id)
                .Index(t => t.RoleEntity_Id);
            
            AddColumn("dbo.OrderEntities", "UserEntity_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.OrderEntities", "UserEntity_Id");
            AddForeignKey("dbo.OrderEntities", "UserEntity_Id", "dbo.UserEntities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEntityRoleEntities", "RoleEntity_Id", "dbo.RoleEntities");
            DropForeignKey("dbo.UserEntityRoleEntities", "UserEntity_Id", "dbo.UserEntities");
            DropForeignKey("dbo.OrderEntities", "UserEntity_Id", "dbo.UserEntities");
            DropIndex("dbo.UserEntityRoleEntities", new[] { "RoleEntity_Id" });
            DropIndex("dbo.UserEntityRoleEntities", new[] { "UserEntity_Id" });
            DropIndex("dbo.OrderEntities", new[] { "UserEntity_Id" });
            DropColumn("dbo.OrderEntities", "UserEntity_Id");
            DropTable("dbo.UserEntityRoleEntities");
            DropTable("dbo.UserEntities");
            DropTable("dbo.RoleEntities");
        }
    }
}
