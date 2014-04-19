﻿
using Mercraft.Core;

namespace Mercraft.Maps.UnitTests
{
    internal static class TestHelper
    {
        public static readonly GeoCoordinate SmallPbfFileCenter = new GeoCoordinate(52.529814, 13.388015);
        public static readonly GeoCoordinate BerlinGeoCenter = new GeoCoordinate(52.529814, 13.388015);

        public const string ConfigRootFile = "test.config";

        public const string TestPbfFilePath = @"..\..\..\..\Tests\TestAssets\kempen.osm.pbf";

        //52.53057 13.38687 52.52940 13.39022
        public const string TestXmlFilePath = @"..\..\..\..\Tests\TestAssets\berlin_house.osm.xml";

        public const string TestBigPbfFilePath = @"..\..\..\..\Tests\TestAssets\berlin-latest.osm.pbf";

        public const string TestBaseMapcssFile = @"..\..\..\..\Tests\TestAssets\base.mapcss";
        public const string DefaultMapcssFile = @"..\..\..\..\..\Config\default.mapcss";

        public const string BuildingStylesConfig = @"..\..\..\..\Tests\TestAssets\Buildings\Config\Styles\styles.config";
        public const string BuildingTexturesConfig = @"..\..\..\..\Tests\TestAssets\Buildings\Config\Textures\textures.config";
    }
}
