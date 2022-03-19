namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vacantbyadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "VacantBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rooms", "VacantBy");
        }
    }
}
