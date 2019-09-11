using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using TransportSchedule.Schedule.Modules;
using TransportSchedule.Schedule.Repo;
using System.Data.Entity;

namespace TransportSchedule.Authorization
{
    public class User
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<FavouriteStation> FavouriteStations { get; set; }

        public User AddFavourite(Station station)
        {
            User user = this;
            using (var context = new Context())
            {
                user = context.Users.Include(x => x.FavouriteStations
                .Select(y => y.Station))
                .SingleOrDefault(x => x.Login == user.Login);
                station = context.Stations.SingleOrDefault(x => x.Name == station.Name);

                user.FavouriteStations.Add(new FavouriteStation()
                {
                    Station = station,
                    StationID = station.Id,
                    User = user,
                    UserId = user.UserId,
                    Comment = ""
                });
                context.FavouriteStations.Add(user.FavouriteStations.Single(x => x.Station.Name == station.Name));
                context.SaveChanges();
                return context.Users.Single(x => x.Login == user.Login);
            }
        }

        public User RemoveFavourite(Station station)
        {
            User user = this;
            using (var context = new Context())
            {
                user = context.Users.Include(x => x.FavouriteStations
                .Select(y => y.Station))
                .SingleOrDefault(x => x.Login == user.Login);
                var temp = user.FavouriteStations.Single(x => x.Station.Name == station.Name);
                user.FavouriteStations.Remove(temp);
                context.FavouriteStations.Remove(context.FavouriteStations.Single(x => x.Id == temp.Id));
                context.SaveChanges();
                return context.Users.Single(x => x.Login == user.Login);
            }

        }

        public User EditComment(FavouriteStation station, string comment)
        {
            User user = this;
            using (var context = new Context())
            {
                user = context.Users.Include(x => x.FavouriteStations
                .Select(y => y.Station))
                .SingleOrDefault(x => x.Login == user.Login);
                station = user.FavouriteStations.SingleOrDefault(x => x.Station.Name == station.Station.Name);
                station.Comment = comment;
                context.SaveChanges();
                return user;
            }
        }
    }
}
