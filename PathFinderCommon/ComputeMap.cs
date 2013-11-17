using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderCommon
{
    public class ComputeMap 
    {
        public Point Origin { get; set; }
        public Point Destination { get; set; }
        public Node[,] NodeArray { get; set; }
    }
}
