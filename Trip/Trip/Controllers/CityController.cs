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
            con.Close();
            rd2.Close();
            if (Real_City == true)
            {
                det.Lists.random3Attractions(searchID);
                det.Lists.random3Cities(searchID);
                return View("City", det);

            }

            else
            {
                det.PopUps.City_not_found = "Invalid City!";
                return View("Index", det);
            }
        }

        public ActionResult AddCity(string addCityNameID, string addCountryID, string addCityDescriptionID)
        {
            Details det = new Details();
            LogIn log = new LogIn();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd4 = new SqlCommand
            {
                CommandText = "INSERT INTO Cities  (City_Name, City_Country, City_Description) VALUES (@addCityNameID, @addCountryID, @addCityDescriptionID)",
                Connection = con
            };
            cmd4.Parameters.Add(new SqlParameter("@addCityNameID", addCityNameID));
            cmd4.Parameters.Add(new SqlParameter("@addCountryId", addCountryID));
            cmd4.Parameters.Add(new SqlParameter("@addCityDescriptionID", addCityDescriptionID));
            SqlDataReader rd4 = cmd4.ExecuteReader();
            while (rd4.Read())
            {

                cmd4.ExecuteNonQuery();
            }
            con.Close();
            rd4.Close();
            return View("AdminPage", log);
        }

        [HttpPost]
        public ActionResult DeleteCity(int id)
        {
            Lists list = new Lists();
            List<Cities> listOfCities = new List<Cities>();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd6 = new SqlCommand
            {
                CommandText = "delete from Cities where @id=Cities.CityID",
                Connection = con
            };
            cmd6.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader rd6 = cmd6.ExecuteReader();
            while (rd6.Read())
            {
                cmd6.ExecuteNonQuery();
            }
            con.Close();
            rd6.Close();
            Details det = new Details();
            return View("DeleteCity", list.getAllCities());

        }

        [HttpPost]
        public ActionResult ShowCities()
        {
            Lists list = new Lists();
            return View("DeleteCity", list.getAllCities());
        }
        public ActionResult CityList(int id)
        {
            Details det = new Details();
            det.Cities.CityID = id;
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd12 = new SqlCommand
            {
                CommandText = "Select * from Cities " +
                "inner join Countries on Cities.City_Country=Countries.CountryID " +
                "inner join Attraction on Cities.CityID=Attraction.Attraction_City " +
                "inner join ImagesCity on Cities.CityID=ImagesCity.ImageCity " +
                "where @id=Cities.CityID",
                Connection = con
            };
            cmd12.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader rd12 = cmd12.ExecuteReader();
            while (rd12.Read())
            {
                det.Country.Country_Name = rd12["Country_Name"].ToString();
                det.Attractions.Attractions_Name = rd12["Attraction_Name"].ToString();
                det.CityImages.Image_URL = rd12["URL"].ToString();
                det.Cities.City_Name = rd12["City_Name"].ToString();
                det.Cities.City_Description = rd12["City_Description"].ToString();
            }
            det.Lists.random3Attractions(det.Cities.City_Name);
            det.Lists.random3Cities(det.Cities.City_Name);
            con.Close();
            rd12.Close();
            return View("City",det);
        }
    }  
}