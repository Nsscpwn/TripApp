using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class AttractionController : Controller
    {
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

        public ActionResult AttractionList()
        {
            return View("AttractionList");
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

        [HttpPost]
        public ActionResult ShowAttractions()
        {
            Lists list = new Lists();
            return View("DeleteAttractions", list.getAllAttractions());
        }
    }
}