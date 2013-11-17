using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderCommon
{
    public class Node
    {

        public Node(int x, int y, int destinationX, int destinationY)
        {
            Location = new Point(x, y);
            
            //Heuristic is computed on the fly
            //In this case we are using Manhattan type Heuristics
            //http://en.wikipedia.org/wiki/Heuristic_function
            Heuristic = Math.Abs(x - destinationX) + Math.Abs(y - destinationY);
        }

        public Node Parent { get; set; }
        public Point Location { get; private set; }
        public int Heuristic { get; private set; }
        public int MovementCost { get; set; }
        public int MovementCostFromCurrentAdjacent { get; set; }
        public int TotalCost { get { return Heuristic + MovementCost; } }
        public bool PreviouslyCreated { get; set; }
    }
}
