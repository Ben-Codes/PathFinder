using PathFinderCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderAlgorithms
{
    class MapGenerator
    {
        static internal ComputeMap GenerateMap(PathingMap pathingMap)
        {
            ComputeMap map = new ComputeMap();
            map.Origin = pathingMap.Origin;
            map.Destination = pathingMap.Destination;

            map.NodeArray = new Node[10,10]; 

            return new ComputeMap();
        }

        private Node[,] PopulateNodeArray(int width, int height)
        {
            var nodeArray = new Node[width, height]; 

            //For loop improves one time iteration speed
            //Height first for small perf gain
            for (int i = 0; i < height; i++)
            {
                for (int ii = 0; ii < width; ii++)
                {
                    //var node = new Node();
                    //node.Location = new Point(ii,i);
                    //nodeArray[ii, i] = node;
                }
            }

            return nodeArray;
        }

    }
}
