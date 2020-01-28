using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class DeleteAttractionController : Controller
    {
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