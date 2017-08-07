namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletionStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderEntities", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderEntities", "Status");
        }
    }
}
