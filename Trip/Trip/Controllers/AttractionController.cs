using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trip.Controllers
{
    public class AttractionController : Controller
    {
        public ActionResult Attraction()
        {
            return View("Attraction");
        }
    }
}