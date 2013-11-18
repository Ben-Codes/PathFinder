using PathFinderCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinderAlgorithms
{
    public static class UnitConverter
    {
        /// <summary>
        /// Used to down convert units used in UI to a simpler cartesian grid 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="baseRatio"></param>
        /// <returns></returns>
        public static PathingMap ConvertMapToBaseUnits(PathingMap map, int baseRatio)
        {
            map.Destination.x = map.Destination.x / baseRatio;
            map.Destination.y = map.Destination.y / baseRatio;

            map.Origin.x = map.Origin.x / baseRatio;
            map.Origin.y = map.Origin.y / baseRatio;

            map.Stage.Height = map.Stage.Height / baseRatio;
            map.Stage.Width = map.Stage.Width / baseRatio;

            foreach (var obstacle in map.Obstacles)
            {
                obstacle.x = obstacle.x / baseRatio;
                obstacle.y = obstacle.y / baseRatio;

                obstacle.Height = obstacle.Height / baseRatio;
                obstacle.Width = obstacle.Width / baseRatio;
            }

            return map;
        }

        public static Point[] ConvertPointsToDisplayUnits(Point[] points, int baseRatio)
        {
            foreach (var point in points)
            {
                point.x = point.x * baseRatio;
                point.y = point.y * baseRatio;
            }

            return points;
        }

    }
}
