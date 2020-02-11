using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class AdminController : Controller
    {
        [HttpPost]
        public ActionResult Admin(string usernameID, string passwordID)
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
                if (rd3["Username"].ToString() == usernameID && rd3["Password"].ToString() == passwordID)
                {
                    Valid_Username = true;
                    Valid_Password = true;
                    log.Users.Username = usernameID;
                    log.Users.Password = passwordID;
                    break;
                }
            }
            con.Close();
            rd3.Close();
            if (Valid_Password == true && Valid_Username == true)
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