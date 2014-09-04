﻿using System;
using System.Collections.Generic;
using Mercraft.Core;
using Mercraft.Core.Elevation;

namespace Mercraft.Models.Geometry
{
    public static class LineUtils
    {
        public static MapPoint[] GetIntermediatePoints(HeightMap heightMap, MapPoint[] original, float maxDistance)
        {
            var result = new List<MapPoint>(original.Length);
            for (int i = 1; i < original.Length; i++)
            {
                var point1 = original[i - 1];
                var point2 = original[i];

                point1.Elevation = heightMap.LookupHeight(point1);
                result.Add(point1);

                
                var distance = point1.DistanceTo(point2);
                while (distance > maxDistance)
                {
                    var ration = maxDistance/distance;
                    point1 = new MapPoint(
                        point1.X + ration*(point2.X - point1.X),
                        point1.Y + ration*(point2.Y - point1.Y));

                    point1.Elevation = heightMap.LookupHeight(point1);

                    distance = point1.DistanceTo(point2);
                    // we should prevent us to have small distances between points when we have turn
                    if (distance < 5f)
                        break;
                    
                    result.Add(point1);
                }
                
            }
            // add last as we checked previous item in cycle
            var last = original[original.Length - 1];
            last.Elevation = heightMap.LookupHeight(last);
            result.Add(last);
            return result.ToArray();
        }

        public static MapPoint GetNextIntermediatePoint(HeightMap heightMap, MapPoint point1, MapPoint point2, float maxDistance)
        {
            var distance = point1.DistanceTo(point2);
            if (distance > maxDistance)
            {
                var ration = maxDistance / distance;
                var next = new MapPoint(
                            point1.X + ration * (point2.X - point1.X),
                            point1.Y + ration * (point2.Y - point1.Y));

                next.Elevation = heightMap.LookupHeight(point1);
                return next;
            }

            return point2; // NOTE should we return point2?
        }

       /* public static MapPoint GetNextIntermediatePoint(HeightMap heightMap, MapPoint point1, MapPoint point2, float maxDistance)
        {
            var next = point1;
            if (Math.Abs(point1.Elevation - point2.Elevation) > 0.1f)
            {
                var distance = point1.DistanceTo(point2);
                var ration = maxDistance / distance;
                next = new MapPoint(
                            point1.X + ration * (point2.X - point1.X),
                            point1.Y + ration * (point2.Y - point1.Y));

                next.Elevation = heightMap.LookupHeight(point1);
            }

            return next;
        }*/
    }
}
