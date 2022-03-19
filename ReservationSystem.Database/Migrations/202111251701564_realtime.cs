namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class realtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "AddedOn", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "AddedOn");
        }
    }
}
