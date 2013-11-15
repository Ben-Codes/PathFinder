using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Path_finder.Models;

namespace Path_finder.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PlotPath(PathingMap pathingMap)
        {
            return Json(pathingMap);
        }
    }
}
