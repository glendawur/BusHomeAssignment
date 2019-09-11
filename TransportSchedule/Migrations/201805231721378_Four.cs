namespace TransportSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Four : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RouteStations", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.FavouriteStations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FavouriteStations", "Station_Id", "dbo.Stations");
            DropIndex("dbo.RouteStations", new[] { "Station_Id" });
            DropIndex("dbo.FavouriteStations", new[] { "Station_Id" });
            DropIndex("dbo.FavouriteStations", new[] { "User_Id" });
            RenameColumn(table: "dbo.RouteStations", name: "Station_Id", newName: "StationID");
            RenameColumn(table: "dbo.FavouriteStations", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.FavouriteStations", name: "Station_Id", newName: "StationID");
            AddColumn("dbo.Users", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.RouteStations", "StationID", c => c.Int(nullable: false));
            AlterColumn("dbo.FavouriteStations", "StationID", c => c.Int(nullable: false));
            AlterColumn("dbo.FavouriteStations", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.RouteStations", "StationID");
            CreateIndex("dbo.FavouriteStations", "UserId");
            CreateIndex("dbo.FavouriteStations", "StationID");
            AddForeignKey("dbo.RouteStations", "StationID", "dbo.Stations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FavouriteStations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FavouriteStations", "StationID", "dbo.Stations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteStations", "StationID", "dbo.Stations");
            DropForeignKey("dbo.FavouriteStations", "UserId", "dbo.Users");
            DropForeignKey("dbo.RouteStations", "StationID", "dbo.Stations");
            DropIndex("dbo.FavouriteStations", new[] { "StationID" });
            DropIndex("dbo.FavouriteStations", new[] { "UserId" });
            DropIndex("dbo.RouteStations", new[] { "StationID" });
            AlterColumn("dbo.FavouriteStations", "UserId", c => c.Int());
            AlterColumn("dbo.FavouriteStations", "StationID", c => c.Int());
            AlterColumn("dbo.RouteStations", "StationID", c => c.Int());
            DropColumn("dbo.Users", "UserId");
            RenameColumn(table: "dbo.FavouriteStations", name: "StationID", newName: "Station_Id");
            RenameColumn(table: "dbo.FavouriteStations", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.RouteStations", name: "StationID", newName: "Station_Id");
            CreateIndex("dbo.FavouriteStations", "User_Id");
            CreateIndex("dbo.FavouriteStations", "Station_Id");
            CreateIndex("dbo.RouteStations", "Station_Id");
            AddForeignKey("dbo.FavouriteStations", "Station_Id", "dbo.Stations", "Id");
            AddForeignKey("dbo.FavouriteStations", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.RouteStations", "Station_Id", "dbo.Stations", "Id");
        }
    }
}
