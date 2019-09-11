using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule {
    public class Station: IEquatable<Station> {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
        public bool Equals(Station other)
        {
            if (this.Id.Equals(other.Id))
                return true;
            else return false;
        }
    }
}
