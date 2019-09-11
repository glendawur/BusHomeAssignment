namespace TransportSchedule.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class One : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartTime = c.Int(nullable: false),
                        EndTime = c.Int(nullable: false),
                        Interval = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TimeFromOrigin = c.Int(nullable: false),
                        TimeFromDest = c.Int(nullable: false),
                        Station_Id = c.Int(),
                        Route_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.Station_Id)
                .ForeignKey("dbo.Routes", t => t.Route_Id)
                .Index(t => t.Station_Id)
                .Index(t => t.Route_Id);
            
            CreateTable(
                "dbo.Stations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FavouriteStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Station_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stations", t => t.Station_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Station_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavouriteStations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FavouriteStations", "Station_Id", "dbo.Stations");
            DropForeignKey("dbo.RouteStations", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.RouteStations", "Station_Id", "dbo.Stations");
            DropIndex("dbo.FavouriteStations", new[] { "User_Id" });
            DropIndex("dbo.FavouriteStations", new[] { "Station_Id" });
            DropIndex("dbo.RouteStations", new[] { "Route_Id" });
            DropIndex("dbo.RouteStations", new[] { "Station_Id" });
            DropTable("dbo.FavouriteStations");
            DropTable("dbo.Users");
            DropTable("dbo.Stations");
            DropTable("dbo.RouteStations");
            DropTable("dbo.Routes");
        }
    }
}
