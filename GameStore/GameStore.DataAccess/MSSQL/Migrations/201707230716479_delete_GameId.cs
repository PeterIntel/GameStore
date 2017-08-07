namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_GameId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GameInfoEntities", "GameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameInfoEntities", "GameId", c => c.Int(nullable: false));
        }
    }
}
