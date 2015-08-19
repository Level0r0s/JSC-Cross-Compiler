using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.p
{
    class p
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /p/
            ["ChromeShaderToyPangramByHoskins"] = () => new ChromeShaderToyPangramByHoskins.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPhysicsNoCollisionsByIq"] = () => new ChromeShaderToyPhysicsNoCollisionsByIq.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPianoByIq"] = () => new ChromeShaderToyPianoByIq.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPigsRunningByGaz"] = () => new ChromeShaderToyPigsRunningByGaz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPlasmaTriangleByElusivePete"] = () => new ChromeShaderToyPlasmaTriangleByElusivePete.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPortalByHLorenzi"] = () => new ChromeShaderToyPortalByHLorenzi.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPortalTurretByMattz"] = () => new ChromeShaderToyPortalTurretByMattz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPolygonalTerrainByFizzer"] = () => new ChromeShaderToyPolygonalTerrainByFizzer.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPrimitivesByQuilez"] = () => new ChromeShaderToyPrimitivesByQuilez.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyPyramidsByAvix"] = () => new ChromeShaderToyPyramidsByAvix.Shaders.ProgramFragmentShader(),
            ["PacmanByTsone"] = () => new PacmanByTsone.Shaders.ProgramFragmentShader(),
            ["PalmettoStalkByEiffie"] = () => new PalmettoStalkByEiffie.Shaders.ProgramFragmentShader(),
            ["ParallaxMappingByNimitz"] = () => new ParallaxMappingByNimitz.Shaders.ProgramFragmentShader(),
            ["ParkingGarageByHamneggs"] = () => new ParkingGarageByHamneggs.Shaders.ProgramFragmentShader(),
            ["PlanetShadertoyByReinder"] = () => new PlanetShadertoyByReinder.Shaders.ProgramFragmentShader(),
            ["PlasticReflectingCubesByDen"] = () => new PlasticReflectingCubesByDen.Shaders.ProgramFragmentShader(),
            ["PlayingWithRefleksByEiffie"] = () => new PlayingWithRefleksByEiffie.Shaders.ProgramFragmentShader(),
            ["PlottingFunctionsByHornet"] = () => new PlottingFunctionsByHornet.Shaders.ProgramFragmentShader(),
            ["PolygonRaytracingByBranch"] = () => new PolygonRaytracingByBranch.Shaders.ProgramFragmentShader(),
            ["PopularShaderByFizzer"] = () => new PopularShaderByFizzer.Shaders.ProgramFragmentShader(),
            ["PortalGunByRamocles"] = () => new PortalGunByRamocles.Shaders.ProgramFragmentShader(),
            ["PrairieByEiffie"] = () => new PrairieByEiffie.Shaders.ProgramFragmentShader(),
            ["PreloaderByMattdesl"] = () => new PreloaderByMattdesl.Shaders.ProgramFragmentShader(),
            ["PseudoArmillaryTestByRK"] = () => new PseudoArmillaryTestByRK.Shaders.ProgramFragmentShader(),
            #endregion

        };

    }
}
