﻿using System;
using System.Collections.Generic;
using System.Linq;
using ActionStreetMap.Core;
using ActionStreetMap.Core.Geometry.Triangle.Geometry;
using ActionStreetMap.Core.Geometry.Triangle.Meshing;
using ActionStreetMap.Core.Scene;
using ActionStreetMap.Core.Unity;
using ActionStreetMap.Explorer.Geometry;
using ActionStreetMap.Explorer.Infrastructure;
using ActionStreetMap.Explorer.Utils;
using ActionStreetMap.Infrastructure.Dependencies;
using ActionStreetMap.Infrastructure.Utilities;
using ActionStreetMap.Unity.Wrappers;
using UnityEngine;

namespace ActionStreetMap.Explorer.Scene.Roofs
{
    /// <summary> Defines roof builder logic. </summary>
    public interface IRoofBuilder
    {
        /// <summary> Gets name of roof builder. </summary>
        string Name { get; }

        /// <summary> Checks whether this builder can build roof of given building. </summary>
        /// <param name="building"> Building.</param>
        /// <returns> True if can build.</returns>
        bool CanBuild(Building building);

        /// <summary> Builds MeshData which contains information how to construct roof. </summary>
        /// <param name="building"> Building.</param>
        List<MeshData> Build(Building building);
    }

    internal abstract class RoofBuilder : IRoofBuilder
    {
        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public abstract bool CanBuild(Building building);

        /// <inheritdoc />
        public abstract List<MeshData> Build(Building building);

        [Dependency]
        public IObjectPool ObjectPool { get; set; }

        [Dependency]
        public IResourceProvider ResourceProvider { get; set; }

        [Dependency]
        public IGameObjectFactory GameObjectFactory { get; set; }

        /// <summary> Builds flat roof from footprint using provided triangles. </summary>
        protected MeshData BuildFloor(GradientWrapper gradient, List<MapPoint> footprint, float height)
        {
            var polygon = new Polygon(footprint.Count);

            var list = ObjectPool.NewList<Point>(footprint.Count);
            list.AddRange(footprint.Select(point => new Point(point.X, point.Y)));
            polygon.AddContour(list);

            var mesh = polygon.Triangulate(
               new ConstraintOptions
               {
                   ConformingDelaunay = false,
                   SegmentSplitting = 0
               },
               new QualityOptions
               {
                   MaximumArea = 3
               });

            var vertCount = mesh.Triangles.Count * 3;
            var vertices = new Vector3[vertCount];
            var triangles = new int[vertCount * 2];
            var colors = new Color[vertCount];
            int index = 0;
            foreach (var triangle in mesh.Triangles)
            {
                var startIndex = index;
                var color = GetColor(gradient, vertices[index]);
                for (int i = 0; i < 3; i++)
                {
                    var p = triangle.GetVertex(i);
                    vertices[index] = new Vector3((float)p.X, height, (float)p.Y);

                    triangles[index] = index;
                    triangles[vertCount + index] = 2 - i + startIndex;

                    colors[index] = color;
                    index++;
                }
            }

            return new MeshData()
            {
                Vertices = vertices,
                Triangles = triangles,
                Colors = colors
            };
        }

        protected MeshData CopyMeshData(MeshData meshData, float newHeight)
        {
            var meshDataCopy = new MeshData()
            {
                Vertices = new Vector3[meshData.Vertices.Length],
                Triangles = new int[meshData.Triangles.Length],
                Colors = new Color[meshData.Colors.Length],
            };

            var srcVertices = meshData.Vertices;
            var destVertices = meshDataCopy.Vertices;
            for (int i = 0; i < destVertices.Length; i++)
            {
                var v = srcVertices[i];
                destVertices[i] = new Vector3(v.x, newHeight, v.z);
            }

            Array.Copy(meshData.Triangles, meshDataCopy.Triangles, meshData.Triangles.Length);
            Array.Copy(meshData.Colors, meshDataCopy.Colors, meshData.Colors.Length);

            return meshDataCopy;
        }

        protected Color GetColor(GradientWrapper gradient, Vector3 point)
        {
            var value = (Noise.Perlin3D(point, .3f) + 1f) / 2f;
            return gradient.Evaluate(value);
        }
    }
}