using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule {
    public class RouteStation {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        public virtual Station Station { get; set; }
        public int StationID { get; set; }
        public int TimeFromOrigin { get; set; }
        public int TimeFromDest { get; set; }
    }
}
