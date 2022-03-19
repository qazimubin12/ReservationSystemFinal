namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesrevert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "Reservations_ID", "dbo.Reservations");
            DropForeignKey("dbo.Rooms", "Reservations_ID1", "dbo.Reservations");
            DropIndex("dbo.Rooms", new[] { "Reservations_ID" });
            DropIndex("dbo.Rooms", new[] { "Reservations_ID1" });
            DropColumn("dbo.Rooms", "Reservations_ID");
            DropColumn("dbo.Rooms", "Reservations_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rooms", "Reservations_ID1", c => c.Int());
            AddColumn("dbo.Rooms", "Reservations_ID", c => c.Int());
            CreateIndex("dbo.Rooms", "Reservations_ID1");
            CreateIndex("dbo.Rooms", "Reservations_ID");
            AddForeignKey("dbo.Rooms", "Reservations_ID1", "dbo.Reservations", "ID");
            AddForeignKey("dbo.Rooms", "Reservations_ID", "dbo.Reservations", "ID");
        }
    }
}
