namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somefixes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "Reservations_ID", c => c.Int());
            AddColumn("dbo.Rooms", "Reservations_ID1", c => c.Int());
            CreateIndex("dbo.Rooms", "Reservations_ID");
            CreateIndex("dbo.Rooms", "Reservations_ID1");
            AddForeignKey("dbo.Rooms", "Reservations_ID", "dbo.Reservations", "ID");
            AddForeignKey("dbo.Rooms", "Reservations_ID1", "dbo.Reservations", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Reservations_ID1", "dbo.Reservations");
            DropForeignKey("dbo.Rooms", "Reservations_ID", "dbo.Reservations");
            DropIndex("dbo.Rooms", new[] { "Reservations_ID1" });
            DropIndex("dbo.Rooms", new[] { "Reservations_ID" });
            DropColumn("dbo.Rooms", "Reservations_ID1");
            DropColumn("dbo.Rooms", "Reservations_ID");
        }
    }
}
