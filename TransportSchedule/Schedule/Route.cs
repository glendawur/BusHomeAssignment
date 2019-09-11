using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule {
    public class Route {
        public const int MaxInterval = 20;

        public string Name { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public int Interval { get; set; }

        public Route(string name, int startTime, int endTime, int interval) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Route name cannot be empty");
            if (startTime < 0 || startTime >= TimeHelpers.MinutesInADay)
                throw new ArgumentException("Incorrect start time");
            if (endTime < 0 || endTime >= TimeHelpers.MinutesInADay)
                throw new ArgumentException("Incorrect end time");
            if (interval <= 0 || interval > MaxInterval)
                throw new ArgumentException("Incorrect interval");

            Name = name;
            StartTime = startTime;
            EndTime = endTime;
            Interval = interval;
            Stations = new List<RouteStation>();
        }

        public List<RouteStation> Stations { get; set; }

        public int FirstDepartureFromOrigin(RouteStation station) {
            return TimeHelpers.AddMinutes(StartTime, station.TimeFromOrigin);
        }

        public int LastDepartureFromOrigin(RouteStation station) {
            return TimeHelpers.AddMinutes(EndTime, station.TimeFromOrigin);
        }

        public int FirstDepartureFromDest(RouteStation station) {
            return TimeHelpers.AddMinutes(StartTime, station.TimeFromDest);
        }

        public int LastDepartureFromDest(RouteStation station) {
            return TimeHelpers.AddMinutes(EndTime, station.TimeFromDest);
        }

        public int TimeToNextDeparture(RouteStation station, DateTime currentDt, int FirstDeparture, int LastDeparture)
        {
            int currentTime = currentDt.Hour * 60 + currentDt.Minute;

            if (TimeHelpers.IsTimeWithinInterval(currentTime, FirstDeparture, LastDeparture))
            {

                int difference = TimeHelpers.Difference(FirstDeparture, currentTime);
                int passedTrains = difference / Interval;
                if (difference % Interval != 0)
                    passedTrains++;

                int timeOfNextDeparture = TimeHelpers.AddMinutes(FirstDeparture, passedTrains * Interval);

                return TimeHelpers.Difference(currentTime, timeOfNextDeparture);
            }
            else
            {
                return TimeHelpers.Difference(currentTime, FirstDeparture);
            }
        }
    }
}
