namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "User_ID", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "User_ID" });
            AddColumn("dbo.Users", "Role", c => c.String());
            DropTable("dbo.UserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Users", "Role");
            CreateIndex("dbo.UserRoles", "User_ID");
            AddForeignKey("dbo.UserRoles", "User_ID", "dbo.Users", "ID");
        }
    }
}
