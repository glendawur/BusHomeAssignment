using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Authorization;
using TransportSchedule.Repo;

namespace TransportSchedule {
    public class Program {

        public static List<ScheduleItem> ProgramResult(Station station)
        {
            var context = new Context();
            //DataRepository<Route> routes = new RouteRepository(); 
            DbRepository<Route> routes = new DbRoutes();
            List<ScheduleItem> result = new List<ScheduleItem>();
            DateTime currentDt = DateTime.Now;

            foreach (var route in routes.Items)
            {

                var routeStation = route.Stations.FirstOrDefault(st => st.Station.Equals(station));

                if (routeStation != null)
                {

                    if (routeStation != route.Stations.Last())
                    {
                        int left = route.TimeToNextDeparture(routeStation, currentDt, route.FirstDepartureFromOrigin(routeStation), route.LastDepartureFromOrigin(routeStation));
                        result.Add(new ScheduleItem
                        {
                            RouteName = route.Name,
                            Destination = route.Stations.Last().Station.Name,
                            MinutesLeft = left
                        });
                    }
                    if (routeStation != route.Stations.First())
                    {
                        int left = route.TimeToNextDeparture(routeStation, currentDt, route.FirstDepartureFromDest(routeStation), route.LastDepartureFromDest(routeStation));
                        result.Add(new ScheduleItem
                        {
                            RouteName = route.Name,
                            Destination = route.Stations.First().Station.Name,
                            MinutesLeft = left
                        });
                    }
                }
            }

            return result;

        }

        public static List<ScheduleItem> Sort(List<ScheduleItem> _list, Route _route)
        {
            var result = new List<ScheduleItem>();
            foreach(var item in _list)
            {
                if (item.RouteName == _route.Name)
                    result.Add(item);
            }
            return result;
        }
    }
}
