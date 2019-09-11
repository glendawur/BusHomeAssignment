namespace TransportSchedule.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TransportSchedule.Authorization;
    using TransportSchedule.Schedule.Modules;

    internal sealed class Configuration : DbMigrationsConfiguration<TransportSchedule.Schedule.Repo.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TransportSchedule.Schedule.Repo.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            DataRepository<Route> routes = new RouteRepository();
            DataRepository<Station> stations = new StationRepository();
            List<User> users = WorkinhWithUserMethods.GetUsers();
            foreach (var item in stations.Items)
                context.Stations.AddOrUpdate(c => c.Id, item);
            foreach (var item in routes.Items)
            {
                foreach (var station in item.Stations)
                    station.Station = context.Stations.SingleOrDefault(x => x.Id == station.StationID);
                context.Routes.AddOrUpdate(c => c.Name,item);
            }
            foreach (var item in users)
            {
                foreach (var station in item.FavouriteStations)
                {
                    station.Station = context.Stations.SingleOrDefault(x => 
                    x.Id == station.StationID);
                    station.User = item;
                }
                context.Users.AddOrUpdate(c => c.UserId,item);
            }
            context.SaveChanges();
        }
    }
}
