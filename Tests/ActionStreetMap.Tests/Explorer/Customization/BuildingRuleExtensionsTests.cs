﻿using System.Collections.Generic;
using ActionStreetMap.Core;
using ActionStreetMap.Core.MapCss;
using ActionStreetMap.Core.Tiling.Models;
using ActionStreetMap.Explorer.Customization;
using ActionStreetMap.Maps.Data;
using NUnit.Framework;

namespace ActionStreetMap.Tests.Explorer.Customization
{
    [TestFixture]
    public class BuildingRuleExtensionsTests
    {
        [Test]
        public void CanGetBuildingStyle()
        {
            // ARRANGE
            var provider = new StylesheetProvider(TestHelper.DefaultMapcssFile, TestHelper.GetFileSystemService());
            var stylesheet = provider.Get();

            var tags = new TagCollection();
            tags.Add("building", "residential");
            var building = new Area()
            {
                Id = 1,
                Points = new List<GeoCoordinate>(),
                Tags = tags.AsReadOnly()
            };

            // ACT
            var rule = stylesheet.GetModelRule(building, MapConsts.MaxZoomLevel);
            var style = rule.GetFacadeBuilder();

            // ASSERT
            Assert.AreEqual("default", style);
        }
    }
}
