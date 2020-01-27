using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class Attractions
    {
        public string Attractions_TicketPrice { get; set; }
        public string Attractions_Schedule { get; set; }
        public string Attractions_Name { get; set; }
        public string Attraction_Descriptions { get; set; }
        public int Attraction_CityID { get; set; }
        public int AttractionID { get; set; }
    }
}