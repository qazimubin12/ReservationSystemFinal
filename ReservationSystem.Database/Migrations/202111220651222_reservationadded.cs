namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReservationTime = c.DateTime(nullable: false),
                        ReservationAgenda = c.String(),
                        Room_ID = c.Int(),
                        RoomSpecification_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rooms", t => t.Room_ID)
                .ForeignKey("dbo.RoomSpecifications", t => t.RoomSpecification_ID)
                .Index(t => t.Room_ID)
                .Index(t => t.RoomSpecification_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomSpecification_ID", "dbo.RoomSpecifications");
            DropForeignKey("dbo.Reservations", "Room_ID", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "RoomSpecification_ID" });
            DropIndex("dbo.Reservations", new[] { "Room_ID" });
            DropTable("dbo.Reservations");
        }
    }
}
