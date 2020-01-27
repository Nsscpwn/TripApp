using System;
using System.Collections.Generic;
using System.Data;
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
            return View("Index",det);
        }
        [HttpPost]
        public ActionResult City(string searchID)
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
                if (rd2["City_Name"].ToString()== searchID)
                {
                    det.Country.Country_Name = rd2["Country_Name"].ToString();
                    det.Attractions.Attractions_Name = rd2["Attraction_Name"].ToString();
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
        public ActionResult AddCity(string addCityNameID,string addCountryID,string addCityDescriptionID)
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
            rd4.Close();           
            return View("AdminPage",log);
        }
        [HttpPost]
        public ActionResult ShowCities()
        {
            Lists list = new Lists();
            return View("DeleteCity", list.getAllCities());
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
            rd6.Close();
            Details det = new Details();
            return View("DeleteCity", list.getAllCities());
            
        }
        public ActionResult AddAttraction(string addAttractionPrice, string addAttractionProgram, string addAttractionCityID, string addAttractionDescription, string addAttractionName)
        {
            Details det = new Details();
            LogIn log = new LogIn();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd7 = new SqlCommand
            {
                CommandText = "INSERT INTO Attraction  (Attraction_Price, Attraction_Program, Attraction_City,Attraction_Name,Attraction_Description) VALUES (@addAttractionPrice, @addAttractionProgram, @addAttractionCityID,@addAttractionName,@addAttractionDescription)",
                Connection = con
            };
            cmd7.Parameters.Add(new SqlParameter("@addAttractionPrice", addAttractionPrice));
            cmd7.Parameters.Add(new SqlParameter("@addAttractionProgram", addAttractionProgram));
            cmd7.Parameters.Add(new SqlParameter("@addAttractionCityID", addAttractionCityID));
            cmd7.Parameters.Add(new SqlParameter("@addAttractionDescription", addAttractionDescription));
            cmd7.Parameters.Add(new SqlParameter("@addAttractionName", addAttractionName));
            SqlDataReader rd7 = cmd7.ExecuteReader();
            while (rd7.Read())
            {

                cmd7.ExecuteNonQuery();
            }
            rd7.Close();
            return View("AdminPage", log);
        }
        [HttpPost]
        public ActionResult ShowAttractions()
        {
            Lists list = new Lists();
            return View("DeleteAttractions", list.getAllAttractions());
        }
        public ActionResult DeleteAttraction(int id)
        {
            Lists list = new Lists();
            List<Cities> listOfCities = new List<Cities>();
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=TrueData Source=DESKTOP-RBTV5AJ\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True"
            };
            con.Open();
            SqlCommand cmd9 = new SqlCommand
            {
                CommandText = "delete from Attraction where @id=Attraction.AttractionID",
                Connection = con
            };
            cmd9.Parameters.Add(new SqlParameter("@id", id));
            SqlDataReader rd9 = cmd9.ExecuteReader();
            while (rd9.Read())
            {
                cmd9.ExecuteNonQuery();
            }
            rd9.Close();
            Details det = new Details();
            return View("DeleteAttractions", list.getAllAttractions());

        }
    }
}