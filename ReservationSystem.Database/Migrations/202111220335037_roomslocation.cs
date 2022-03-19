namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roomslocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomLocation", c => c.String());
            AlterColumn("dbo.Rooms", "RoomName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "RoomName", c => c.String());
            DropColumn("dbo.Rooms", "RoomLocation");
        }
    }
}
