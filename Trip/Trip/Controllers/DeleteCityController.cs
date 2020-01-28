using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class DeleteCityController : Controller
    {
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
    }
}