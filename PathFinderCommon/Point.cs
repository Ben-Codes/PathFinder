using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PathFinderCommon
{
    public class Point
    {

        public Point() { }

        public Point(int x, int y){
            this.x = x;
            this.y = y;
        }

        //lowercase to make point canvas system happy
        public int x { get; set;}
        public int y { get; set; }
    }
}