using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.f
{
    class f
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /f/
            ["ChromeShaderToyFastBallsByIq"] = () => new ChromeShaderToyFastBallsByIq.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFastEdgeDetectionByNimitz"] = () => new ChromeShaderToyFastEdgeDetectionByNimitz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFireballByGreen"] = () => new ChromeShaderToyFireballByGreen.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFireCounterByFabrice"] = () => new ChromeShaderToyFireCounterByFabrice.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFlowingLavaByFizzer"] = () => new ChromeShaderToyFlowingLavaByFizzer.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFlyingBoatByGaz"] = () => new ChromeShaderToyFlyingBoatByGaz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFogMountainByESpitz"] = () => new ChromeShaderToyFogMountainByESpitz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFrostedTorusByPwd"] = () => new ChromeShaderToyFrostedTorusByPwd.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFrozenCrytekLogo"] = () => new ChromeShaderToyFrozenCrytekLogo.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFrozenWastelandByDave"] = () => new ChromeShaderToyFrozenWastelandByDave.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyFutureBillboardByEiffie"] = () => new ChromeShaderToyFutureBillboardByEiffie.Shaders.ProgramFragmentShader(),

            ["FaceEdgeVertexByPaniq"] = () => new FaceEdgeVertexByPaniq.Shaders.ProgramFragmentShader(),
            ["FakeGlobalIlluminationByTz"] = () => new FakeGlobalIlluminationByTz.Shaders.ProgramFragmentShader(),
            ["FakeVolumeLightByEiffie"] = () => new FakeVolumeLightByEiffie.Shaders.ProgramFragmentShader(),
            ["FerrisWheelByGaz"] = () => new FerrisWheelByGaz.Shaders.ProgramFragmentShader(),
            ["FirstDistanceMapByChuckeles"] = () => new FirstDistanceMapByChuckeles.Shaders.ProgramFragmentShader(),

            ["FlappyBirdByMovAx"] = () => new FlappyBirdByMovAx.Shaders.ProgramFragmentShader(),
            ["FlatlandByPMalin"] = () => new FlatlandByPMalin.Shaders.ProgramFragmentShader(),
            ["FloatByAlex"] = () => new FloatByAlex.Shaders.ProgramFragmentShader(),
            ["FloatPrintByCasty"] = () => new FloatPrintByCasty.Shaders.ProgramFragmentShader(),
            ["FloorByStarboxByXilconic"] = () => new FloorByStarboxByXilconic.Shaders.ProgramFragmentShader(),
            ["FloorStarBoxMarbleByXilconic"] = () => new FloorStarBoxMarbleByXilconic.Shaders.ProgramFragmentShader(),
            ["FlossingSpaceByFlorian"] = () => new FlossingSpaceByFlorian.Shaders.ProgramFragmentShader(),
            ["FlowerByIq"] = () => new FlowerByIq.Shaders.ProgramFragmentShader(),
            ["FluxCoreByOtaviogood"] = () => new FluxCoreByOtaviogood.Shaders.ProgramFragmentShader(),
            ["FlyByNightByMhnewman"] = () => new FlyByNightByMhnewman.Shaders.ProgramFragmentShader(),

            ["ForestMurshroomBySquid"] = () => new ForestMurshroomBySquid.Shaders.ProgramFragmentShader(),
            ["FoldingByReinder"] = () => new FoldingByReinder.Shaders.ProgramFragmentShader(),
            ["FractalBridgeByDr2"] = () => new FractalBridgeByDr2.Shaders.ProgramFragmentShader(),
            ["FractalCondosByEiffie"] = () => new FractalCondosByEiffie.Shaders.ProgramFragmentShader(),
            ["FractalSphereByGuil"] = () => new FractalSphereByGuil.Shaders.ProgramFragmentShader(),

            ["FurTreesByEiffie"] = () => new FurTreesByEiffie.Shaders.ProgramFragmentShader(),
            ["FuturisticDoorKnobByDiLemming"] = () => new FuturisticDoorKnobByDiLemming.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
