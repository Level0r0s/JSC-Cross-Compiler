using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.r
{
    class r
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160107/shadertoy

        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {

            #region /r/
            {"ChromeShaderToyRavingErnieByBero", () => new ChromeShaderToyRavingErnieByBero.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRayFogByDemofox", () => new ChromeShaderToyRayFogByDemofox.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRacingGameByEiffie", () => new ChromeShaderToyRacingGameByEiffie.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRefractiveSpheresByKig", () => new ChromeShaderToyRefractiveSpheresByKig.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRedditAlienByGleurop", () => new ChromeShaderToyRedditAlienByGleurop.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRaymarchByElias", () => new ChromeShaderToyRaymarchByElias.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRaymarchEdgeDetectionByHLorenzi", () => new ChromeShaderToyRaymarchEdgeDetectionByHLorenzi.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRaymarchingExampleByJack", () => new ChromeShaderToyRaymarchingExampleByJack.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRecogniserByFizzer", () => new ChromeShaderToyRecogniserByFizzer.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRefractionByHLorenzi", () => new ChromeShaderToyRefractionByHLorenzi.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRelentlessBySrtuss", () => new ChromeShaderToyRelentlessBySrtuss.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRockyCoast", () => new ChromeShaderToyRockyCoast.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRollingBallByHoskins", () => new ChromeShaderToyRollingBallByHoskins.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRotatePyramidByGyabo", () => new ChromeShaderToyRotatePyramidByGyabo.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRotatingByGa2arch", () => new ChromeShaderToyRotatingByGa2arch.Shaders.ProgramFragmentShader()},
            {"RainbowSlicesByFizzer", () => new RainbowSlicesByFizzer.Shaders.ProgramFragmentShader()},

            {"RayBertByHoskins", () => new RayBertByHoskins.Shaders.ProgramFragmentShader()},
            {"RayConeRayFrustumByRobert", () => new RayConeRayFrustumByRobert.Shaders.ProgramFragmentShader()},
            {"RaymarchingAttempt2ByCraxic", () => new RaymarchingAttempt2ByCraxic.Shaders.ProgramFragmentShader()},
            {"RaymarchingDisplacementByJcanabald", () => new RaymarchingDisplacementByJcanabald.Shaders.ProgramFragmentShader()},
            {"RayMarchingPlayByHagerman", () => new RayMarchingPlayByHagerman.Shaders.ProgramFragmentShader()},
            {"RaymarchingTutorialByObjelisks", () => new RaymarchingTutorialByObjelisks.Shaders.ProgramFragmentShader()},
            {"RaymarchingTweaksByLanza", () => new RaymarchingTweaksByLanza.Shaders.ProgramFragmentShader()},
            {"RaytracedRefractionByDemofox", () => new RaytracedRefractionByDemofox.Shaders.ProgramFragmentShader()},
            {"RedCellsByPMalin", () => new RedCellsByPMalin.Shaders.ProgramFragmentShader()},
            {"RetroFuturistcThingByFlyguy", () => new RetroFuturistcThingByFlyguy.Shaders.ProgramFragmentShader()},


            {"RectangularAreaLightTestByTsone", () => new RectangularAreaLightTestByTsone.Shaders.ProgramFragmentShader()},
            {"RefelectingCubeByTriggerHLM", () => new RefelectingCubeByTriggerHLM.Shaders.ProgramFragmentShader()},
            {"ReflectingCatByDr2", () => new ReflectingCatByDr2.Shaders.ProgramFragmentShader()},
            {"RelativisticLatticeByMakc", () => new RelativisticLatticeByMakc.Shaders.ProgramFragmentShader()},
            {"RhumbLineByBeyondTheStatic", () => new RhumbLineByBeyondTheStatic.Shaders.ProgramFragmentShader()},

            {"RiseOfShroomByAlgorithm", () => new RiseOfShroomByAlgorithm.Shaders.ProgramFragmentShader()},
            {"RiverFlightByDr2", () => new RiverFlightByDr2.Shaders.ProgramFragmentShader()},
            {"RoadToHellByRez", () => new RoadToHellByRez.Shaders.ProgramFragmentShader()},

            {"RockShapesWIPByAsteropaeus", () => new RockShapesWIPByAsteropaeus.Shaders.ProgramFragmentShader()},
            {"RocketScienceByMu6k", () => new RocketScienceByMu6k.Shaders.ProgramFragmentShader()},

            {"RoomScanningEffectByRosme", () => new RoomScanningEffectByRosme.Shaders.ProgramFragmentShader()},
            {"RoseByHoskins", () => new RoseByHoskins.Shaders.ProgramFragmentShader()},
            {"RutherfordAtomByMattz", () => new RutherfordAtomByMattz.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyRubikSolverByKali", () => new ChromeShaderToyRubikSolverByKali.Shaders.ProgramFragmentShader()},
            #endregion

        };

    }
}
