using System;
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

            pathingMap.Destination.x = pathingMap.Destination.x / 50;
            pathingMap.Destination.y = pathingMap.Destination.y / 50;

            pathingMap.Origin.x = pathingMap.Origin.x / 50;
            pathingMap.Origin.y = pathingMap.Origin.y / 50;

            pathingMap.Stage.Height = pathingMap.Stage.Height / 50;
            pathingMap.Stage.Width = pathingMap.Stage.Width / 50;


            var router = new AStarRouting();
            var points = router.FindRoute(pathingMap);
            points = MultiplyPoint(points);

            return Json(points);
        }

        private Point[] MultiplyPoint(Point[] points)
        {
            foreach (var point in points)
            {
                point.x = point.x * 50;
                point.y = point.y * 50;
            }

            return points;
        }
    }
}
