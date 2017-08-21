namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Update_to_UserEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEntities", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserEntities", "Email");
        }
    }
}
