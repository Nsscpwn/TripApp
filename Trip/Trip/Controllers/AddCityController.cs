using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class AddCityController : Controller
    {
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
            rd4.Close();
            return View("AdminPage", log);
        }
    }
}