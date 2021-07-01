using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.ApiCoronavirus.Models
{
    public class DTOInfected
    {
        public string ID { get; set; }
        public DateTime BirthDate { get; set; }
        public string Sex { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}