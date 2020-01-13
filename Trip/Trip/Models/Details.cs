using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class Details
    {
        public Details()
        {
            this.Cities = new Cities();
            this.Country = new Country();
            this.Attractions = new Attractions();
            this.CityImages = new CityImages();
            this.PopUps = new PopUps();
        }
        public CityImages CityImages { get; set; }
        public Attractions Attractions { get; set; }
        public Country Country { get; set; }
        public Cities Cities { get; set; }
        public PopUps PopUps { get; set; }
    }
}