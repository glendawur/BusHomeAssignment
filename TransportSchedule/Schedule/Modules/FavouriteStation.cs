using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Authorization;

namespace TransportSchedule.Schedule.Modules
{
    public class FavouriteStation
    {
        [Key]
        public int? Id { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public virtual Station Station { get; set; }
        public int? StationID { get; set; }
        
    }
}
