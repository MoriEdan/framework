﻿using Mercraft.Core;
using Mercraft.Core.Algorithms;
using Mercraft.Core.World.Buildings;
using UnityEngine;

namespace Mercraft.Models.Buildings.Roofs
{
    public class FlatRoofBuilder: IRoofBuilder
    {
        public string Name { get { return "flat"; } }

        public MeshData Build(Building building, BuildingStyle style)
        {
            return new MeshData()
            {
                Vertices = GetVerticies3D(building.Footprint, building.BottomOffset + building.Height),
                Triangles = Triangulator.Triangulate(building.Footprint),
                UV = GetUV(building.Footprint),
                Texture = style.Roof.Texture,
                Material = style.Roof.Material
            };
        }

        private Vector3[] GetVerticies3D(MapPoint[] verticies2D, float top)
        {
            var length = verticies2D.Length;
            var vertices3D = new Vector3[length];
            for (int i = 0; i < length; i++)
            {
                vertices3D[i] = new Vector3(verticies2D[i].X, top, verticies2D[i].Y);
            }
            return vertices3D;
        }

        private Vector2[] GetUV(MapPoint[] verticies2D)
        {
            var uv = new Vector2[verticies2D.Length];

            for (int i = 0; i< uv.Length; i++)
            {
                uv[i] = new Vector2(0, 0);
            }

            return uv;
        }
    }
}