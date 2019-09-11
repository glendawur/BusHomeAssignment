using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Authorization;
using TransportSchedule.Schedule.Modules;

namespace TransportSchedule.Schedule.Repo
{
    public class Context: DbContext
    {
        public Context() : base("DefaultConnection")
        {}

        static Context()
        {
            Database.SetInitializer(new Initializer());
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FavouriteStation> FavouriteStations { get; set; }
    }

    public class Initializer: CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            DataRepository<Route> routes = new RouteRepository();
            DataRepository<Station> stations = new StationRepository();
            List<User> users = WorkinhWithUserMethods.GetUsers();
            foreach (var item in stations.Items)
                context.Stations.Add(item);
            foreach (var item in routes.Items)
                context.Routes.Add(item);
            foreach (var item in users)
                context.Users.Add(item);
            context.SaveChanges();
        }
    }
}
