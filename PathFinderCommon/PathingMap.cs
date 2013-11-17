using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PathFinderCommon
{
    public class PathingMap
    {
        public Point Origin { get; set; }
        public Point Destination { get; set; }
        public Stage Stage { get; set; }
        public Rectangle Obstacles { get; set; }
    }
}