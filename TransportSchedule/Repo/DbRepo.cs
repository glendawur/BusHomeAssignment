using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Authorization;
using TransportSchedule.Schedule.Repo;
using System.Data.Entity;
using TransportSchedule.Schedule.Modules;

namespace TransportSchedule.Repo
{
    public abstract class DbRepository<T> : IRepository
    {
        protected List<T> _items;

        public abstract void GenerateData();

        public List<T> Items
        {
            get { return _items; }
        }

        public abstract void Add(T item);
        public abstract void Remove(T item);

        public DbRepository()
        {
            this.GenerateData();
        }

    }

    public class DbStations : DbRepository<Station>
    {
        public override void GenerateData()
        {
            using (var context = new Context())
            {
                _items = context.Stations.ToList();
            }
        }

        public override void Add(Station item)
        {
            using (var context = new Context())
            {
               context.Stations.Add(item);
               context.SaveChanges();
            }
        }

        public override void Remove(Station item)
        {
            using (var context = new Context())
            {
                context.Stations.Remove(item);
                context.SaveChanges();
            }
        }
    }

    public class DbRoutes : DbRepository<Route>
    {
        public override void GenerateData()
        {
            using (var context = new Context())
            {
                _items = context.Routes.Include(x => x.Stations
                .Select(y => y.Station))
                .ToList();
            }
        }

        public override void Add(Route item)
        {
            using (var context = new Context())
            {
                context.Routes.Add(item);
                context.SaveChanges();
            }
        }

        public override void Remove(Route item)
        {
            using (var context = new Context())
            {
                context.Routes.Remove(item);
                context.SaveChanges();
            }
        }
    }

    public class DbUsers : DbRepository<User>
    {
        public DbUsers()
        {
            this.GenerateData();
        }

        public override void GenerateData()
        {
            using (var context = new Context())
            {
                _items = context.Users
                    .Include(x => x.FavouriteStations
                    .Select(y => y.Station))
                    .ToList();
            }
        }

        public override void Add(User user)
        {
            using (var context = new Context())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public override void Remove(User user)
        {
            using (var context = new Context())
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
