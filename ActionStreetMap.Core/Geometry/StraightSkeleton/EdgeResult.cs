﻿using System.Collections.Generic;
using ActionStreetMap.Core.Geometry.StraightSkeleton.Circular;

namespace ActionStreetMap.Core.Geometry.StraightSkeleton
{
    internal class EdgeResult
    {
        public readonly Edge Edge;
        public readonly List<Vector2d> Polygon;

        public EdgeResult(Edge edge, List<Vector2d> polygon)
        {
            Edge = edge;
            Polygon = polygon;
        }
    }
}