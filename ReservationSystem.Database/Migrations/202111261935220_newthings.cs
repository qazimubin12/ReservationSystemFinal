namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newthings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "MeetingEndTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rooms", "AddedOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "AddedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservations", "MeetingEndTime");
        }
    }
}
