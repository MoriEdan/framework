﻿using System.Collections.Generic;
using ActionStreetMap.Core.Polygons;
using ActionStreetMap.Core.Polygons.Meshing.Iterators;
using ActionStreetMap.Core.Polygons.Tools;

namespace ActionStreetMap.Explorer.Terrain
{
    public class MeshContext
    {
        public Mesh Mesh;
        public QuadTree Tree;
        public RegionIterator Iterator;

        public Dictionary<int, int> TriangleMap;

        public List<UnityEngine.Vector3> Vertices;
        public List<int> Triangles;
        public List<UnityEngine.Color> Colors;
    }
}