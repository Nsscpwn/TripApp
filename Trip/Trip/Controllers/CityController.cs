using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class CityController : Controller
    {
        [HttpPost]
        public ActionResult City(string searchID)
        {
            Details det = new Details();
            det.Cities.City_Name = searchID;
            bool Real_City = false;
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd = new SqlCommand
            {
                CommandText = "select * from Cities",
                Connection = con
            };
            SqlCommand cmd2 = new SqlCommand
            {
                CommandText = "select * from Cities inner join Countries on Cities.City_Country=Countries.CountryID" +
               "                                   inner join Attraction on Cities.CityID=Attraction.Attraction_City" +
               "                                   inner join ImagesCity on Cities.CityID=ImagesCity.ImageCity",
                Connection = con
            };
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (rd[1].ToString() == searchID)
                {
                    Real_City = true;
                    det.Cities.City_Description = rd["City_Description"].ToString();
                    break;
                }
            }
            rd.Close();
            SqlDataReader rd2 = cmd2.ExecuteReader();
            while (rd2.Read())
            {
                if (rd2["City_Name"].ToString() == searchID)
                {
                    det.Country.Country_Name = rd2["Country_Name"].ToString();
                    det.Attractions.Attractions_Name = rd2["Attraction_Name"].ToString();
                    det.CityImages.Image_URL = rd2["URL"].ToString();
                    break;
                }
            }
            det.Lists.random3Attractions(searchID);
            det.Lists.random3Cities(searchID);
            rd2.Close();
            if (Real_City == true)
            {
                return View("City", det);

            }

            else
            {
                det.PopUps.City_not_found = "Invalid City!";
                return View("Index", det);
            }
        }
    }
}