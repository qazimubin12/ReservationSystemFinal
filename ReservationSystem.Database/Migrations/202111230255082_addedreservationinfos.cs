namespace ReservationSystem.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedreservationinfos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "VacantBy", c => c.String());
            AddColumn("dbo.Reservations", "BookedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "BookedBy");
            DropColumn("dbo.Reservations", "VacantBy");
        }
    }
}
