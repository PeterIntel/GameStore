namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_columns_to_games : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameEntities", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.GameEntities", "UnitsInStock", c => c.Short(nullable: false));
            AddColumn("dbo.GameEntities", "Discontinued", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameEntities", "Discontinued");
            DropColumn("dbo.GameEntities", "UnitsInStock");
            DropColumn("dbo.GameEntities", "Price");
        }
    }
}
