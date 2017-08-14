namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_User_Reference_to_OrderEntity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserEntityRoleEntities", newName: "RoleEntityUserEntities");
            DropIndex("dbo.OrderEntities", new[] { "UserEntity_Id" });
            DropColumn("dbo.OrderEntities", "CustomerId");
            RenameColumn(table: "dbo.OrderEntities", name: "UserEntity_Id", newName: "CustomerId");
            DropPrimaryKey("dbo.RoleEntityUserEntities");
            AlterColumn("dbo.OrderEntities", "CustomerId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.RoleEntityUserEntities", new[] { "RoleEntity_Id", "UserEntity_Id" });
            CreateIndex("dbo.OrderEntities", "CustomerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderEntities", new[] { "CustomerId" });
            DropPrimaryKey("dbo.RoleEntityUserEntities");
            AlterColumn("dbo.OrderEntities", "CustomerId", c => c.String());
            AddPrimaryKey("dbo.RoleEntityUserEntities", new[] { "UserEntity_Id", "RoleEntity_Id" });
            RenameColumn(table: "dbo.OrderEntities", name: "CustomerId", newName: "UserEntity_Id");
            AddColumn("dbo.OrderEntities", "CustomerId", c => c.String());
            CreateIndex("dbo.OrderEntities", "UserEntity_Id");
            RenameTable(name: "dbo.RoleEntityUserEntities", newName: "UserEntityRoleEntities");
        }
    }
}
