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
            det.PopUps.City_not_found = "Find your perfect trip:";
            det.Lists.random3Images();
            return View("Index", det);
        }
    }
}