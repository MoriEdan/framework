﻿using ActionStreetMap.Core.MapCss;
using ActionStreetMap.Core.Tiling;
using ActionStreetMap.Explorer.Infrastructure;
using ActionStreetMap.Explorer.Scene.Builders;
using ActionStreetMap.Explorer.Tiling;
using ActionStreetMap.Infrastructure.Bootstrap;
using ActionStreetMap.Infrastructure.Dependencies;
using ActionStreetMap.Explorer.Scene.Facades;
using ActionStreetMap.Explorer.Scene.Roofs;

namespace ActionStreetMap.Explorer.Bootstrappers
{
    /// <summary> Register scene specific classes. </summary>
    public class SceneBootstrapper: BootstrapperPlugin
    {
        private const string ThemeKey = "theme";

        /// <inheritdoc />
        public override string Name { get { return "scene"; } }

        /// <inheritdoc />
        public override bool Run()
        {
            Container.Register(Component
                .For<IResourceProvider>()
                .Use<UnityResourceProvider>()
                .SetConfig(GlobalConfigSection.GetSection(ThemeKey))
                .Singleton());

            Container.Register(Component.For<IModelLoader>().Use<TileModelLoader>().Singleton());

            // register stylesheet provider
            Container.Register(Component
                .For<IStylesheetProvider>()
                .Use<StylesheetProvider>()
                .SetConfig(GlobalConfigSection)
                .Singleton());

            // register model builders
            Container.Register(Component.For<IModelBuilder>().Use<BuildingModelBuilder>().Named("building").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<SphereModelBuilder>().Named("sphere").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<CylinderModelBuilder>().Named("cylinder").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<WaterModelBuilder>().Named("water").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<BarrierModelBuilder>().Named("barrier").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<InfoModelBuilder>().Named("info").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<TreeModelBuilder>().Named("tree").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<RoadModelBuilder>().Named("road").Singleton());
            Container.Register(Component.For<IModelBuilder>().Use<SurfaceModelBuilder>().Named("surface").Singleton());
           
            // register core behaviours
            // NOTE no standard behaviours so far

            // facades
            Container.Register(Component.For<IFacadeBuilder>().Use<EmptyFacadeBuilder>().Named("empty").Singleton());
            
            // roofs
            Container.Register(Component.For<IRoofBuilder>().Use<GabledRoofBuilder>().Named("gabled").Singleton());
            Container.Register(Component.For<IRoofBuilder>().Use<HippedRoofBuilder>().Named("hipped").Singleton());
            Container.Register(Component.For<IRoofBuilder>().Use<DomeRoofBuilder>().Named("dome").Singleton());
            Container.Register(Component.For<IRoofBuilder>().Use<PyramidalRoofBuilder>().Named("pyramidal").Singleton());
            Container.Register(Component.For<IRoofBuilder>().Use<MansardRoofBuilder>().Named("mansard").Singleton());
            Container.Register(Component.For<IRoofBuilder>().Use<FlatRoofBuilder>().Named("flat").Singleton());
         
            return true;
        }
    }
}
