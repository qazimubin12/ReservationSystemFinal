namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initalizeddatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomName = c.String(),
                        HaveWifi = c.Boolean(nullable: false),
                        Vacant = c.Boolean(nullable: false),
                        RoomSpecification_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RoomSpecifications", t => t.RoomSpecification_ID)
                .Index(t => t.RoomSpecification_ID);
            
            CreateTable(
                "dbo.RoomSpecifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoomProjectionSpecs = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "RoomSpecification_ID", "dbo.RoomSpecifications");
            DropIndex("dbo.Rooms", new[] { "RoomSpecification_ID" });
            DropTable("dbo.RoomSpecifications");
            DropTable("dbo.Rooms");
        }
    }
}
