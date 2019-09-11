using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace TransportSchedule
{
    public interface IRepository
    {
        void GenerateData();
    }

    public abstract class DataRepository<T>
    {
        protected List<T> _items;

        public abstract void GenerateData();

        public List<T> Items
        {
            get { return _items; }
        }
    }

    public class RouteRepository : DataRepository<Route>
    {
        public override void GenerateData()
        {
            var text = File.ReadAllText("WpfApp1/routes.json");
            _items = JsonConvert.DeserializeObject<List<Route>>(text);
        }

        public RouteRepository()
        {
            this.GenerateData();
        }
    }

    public class StationRepository : DataRepository<Station>
    {
        public override void GenerateData()
        {
            var text = File.ReadAllText("WpfApp1/stations.json");
            _items = JsonConvert.DeserializeObject<List<Station>>(text);
        }

        public StationRepository()
        {
            this.GenerateData();
        }
    }

}
