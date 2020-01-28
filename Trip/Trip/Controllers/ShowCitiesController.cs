using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class ShowCitiesController : Controller
    {
        [HttpPost]
        public ActionResult ShowCities()
        {
            Lists list = new Lists();
            return View("DeleteCity", list.getAllCities());
        }
    }
}