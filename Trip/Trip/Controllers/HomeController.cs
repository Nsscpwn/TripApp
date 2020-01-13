using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Details det = new Details();
            det.PopUps.City_not_found= "Find your perfect trip!";
            return View(det);
        }
        [HttpPost]
        public ActionResult Search(string searchID)
        {
            Details det = new Details();
            det.Cities.City_Name= searchID;
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
               CommandText = "select * from Cities inner join Countries on Cities.Country=Countries.CountryID" +
               "                                   inner join Attractions on Cities.CityID=Attractions.City" +
               "                                   inner join ImagesCity on Cities.CityID=ImagesCity.ImageCity",
               Connection = con
            };
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                if (rd[1].ToString() == searchID)
                {
                    Real_City = true;
                    det.Cities.City_Description = rd["Description"].ToString();
                    break;
                }
            }
            rd.Close();
            SqlDataReader rd2 = cmd2.ExecuteReader();
            while (rd2.Read())
            {
                if (rd2["CityName"].ToString()== searchID)
                {
                    det.Country.Country_Name = rd2["CountryName"].ToString();
                    det.Attractions.Attractions_Name = rd2["Name"].ToString();
                    det.Attractions.Attractions_Schedule = rd2["Program"].ToString();
                    var pricestring = rd2["Price"].ToString();
                    det.Attractions.Attractions_TicketPrice = Int32.Parse(pricestring);
                    det.Attractions.Attraction_Descriptions = rd2["AttractionDesciption"].ToString();
                    det.CityImages.Image_URL = rd2["URL"].ToString();
                    break;
                }
            }
            rd2.Close();
            if (Real_City == true)
            {
                return View("City",det);

            }

            else
            {
                det.PopUps.City_not_found = "Invalid City!";
                return View("Index",det);
            }
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(string usernameID,string passwordID)
        {
            LogIn log = new LogIn();
            bool Valid_Username = false;
            bool Valid_Password = false;
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd3 = new SqlCommand
            {
                CommandText = "select * from Users",
                Connection = con
            };
            SqlDataReader rd3 = cmd3.ExecuteReader();
            while (rd3.Read())
            {
                if (rd3["Username"].ToString() == usernameID && rd3["Password"].ToString()==passwordID)
                {
                    Valid_Username = true;
                    Valid_Password = true;
                    log.Users.Username = usernameID;
                    log.Users.Password = passwordID;
                    break;
                }
            }
            rd3.Close();
            if(Valid_Password==true && Valid_Username == true)
            {
                return View("AdminPage", log);
            }
            else
            {
                return View("LogIn", log);
            }
        }
    }
}