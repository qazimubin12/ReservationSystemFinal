namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class status_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "MeetingStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "MeetingStatus");
        }
    }
}
