namespace TransportSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Five : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FavouriteStations", "UserId", "dbo.Users");
            DropForeignKey("dbo.FavouriteStations", "StationID", "dbo.Stations");
            DropIndex("dbo.FavouriteStations", new[] { "UserId" });
            DropIndex("dbo.FavouriteStations", new[] { "StationID" });
            AlterColumn("dbo.Users", "UserId", c => c.Int());
            AlterColumn("dbo.FavouriteStations", "UserId", c => c.Int());
            AlterColumn("dbo.FavouriteStations", "StationID", c => c.Int());
            CreateIndex("dbo.FavouriteStations", "UserId");
            CreateIndex("dbo.FavouriteStations", "StationID");
            AddForeignKey("dbo.FavouriteStations", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.FavouriteStations", "StationID", "dbo.Stations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteStations", "StationID", "dbo.Stations");
            DropForeignKey("dbo.FavouriteStations", "UserId", "dbo.Users");
            DropIndex("dbo.FavouriteStations", new[] { "StationID" });
            DropIndex("dbo.FavouriteStations", new[] { "UserId" });
            AlterColumn("dbo.FavouriteStations", "StationID", c => c.Int(nullable: false));
            AlterColumn("dbo.FavouriteStations", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.FavouriteStations", "StationID");
            CreateIndex("dbo.FavouriteStations", "UserId");
            AddForeignKey("dbo.FavouriteStations", "StationID", "dbo.Stations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FavouriteStations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
