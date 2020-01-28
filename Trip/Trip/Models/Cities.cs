using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class Cities
    {
        public int CityID { get; set; }
        public string City_Name { get; set; }
        public string City_Description { get; set; }
        public string Country_Name { get; internal set; }
        public int City_Country { get; set; }
    }
}