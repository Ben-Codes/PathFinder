﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PathFinderCommon;
using PathFinderAlgorithms;

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

            pathingMap = UnitConverter.ConvertMapToBaseUnits(pathingMap, Globals.UNIT_RATIO);

            var router = new AStarRouting();
            var points = router.FindRoute(pathingMap);

            points = UnitConverter.ConvertPointsToDisplayUnits(points, Globals.UNIT_RATIO);

            return Json(points);
        }

    }
}
