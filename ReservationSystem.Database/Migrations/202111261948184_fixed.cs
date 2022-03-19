namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _fixed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reservations", "MeetingEndTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "MeetingEndTime", c => c.DateTime(nullable: false));
        }
    }
}
