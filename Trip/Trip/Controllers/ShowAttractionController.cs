using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trip.Models;

namespace Trip.Controllers
{
    public class ShowAttractionController : Controller
    {
        [HttpPost]
        public ActionResult ShowAttractions()
        {
            Lists list = new Lists();
            return View("DeleteAttractions", list.getAllAttractions());
        }
    }
}