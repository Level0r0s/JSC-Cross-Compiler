using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.t
{
    class t
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {



            #region /t/
            ["ChromeShaderToyTextCandyByCPU"] = () => new ChromeShaderToyTextCandyByCPU.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyTexturedEllipsoidsByFabrice"] = () => new ChromeShaderToyTexturedEllipsoidsByFabrice.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyTokyoByReinder"] = () => new ChromeShaderToyTokyoByReinder.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyTrainByDr2"] = () => new ChromeShaderToyTrainByDr2.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyTrainRideByDr2"] = () => new ChromeShaderToyTrainRideByDr2.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyTriangleDistanceByIq"] = () => new ChromeShaderToyTriangleDistanceByIq.Shaders.ProgramFragmentShader(),
            ["TestOfBleedingColorByPredatiti"] = () => new TestOfBleedingColorByPredatiti.Shaders.ProgramFragmentShader(),
            ["TetrahedralInterpolationByPaniq"] = () => new TetrahedralInterpolationByPaniq.Shaders.ProgramFragmentShader(),
            ["TetrahedronatorByEiffie"] = () => new TetrahedronatorByEiffie.Shaders.ProgramFragmentShader(),
            ["TetrahedronByCandycat"] = () => new TetrahedronByCandycat.Shaders.ProgramFragmentShader(),
            ["TetrisByKali"] = () => new TetrisByKali.Shaders.ProgramFragmentShader(),
            ["TileableNoiseByHoskins"] = () => new TileableNoiseByHoskins.Shaders.ProgramFragmentShader(),
            ["TileableWaterCausticByHoskins"] = () => new TileableWaterCausticByHoskins.Shaders.ProgramFragmentShader(),
            ["TinyCuttingByAiekick"] = () => new TinyCuttingByAiekick.Shaders.ProgramFragmentShader(),
            ["ToonCloudByAntoineC"] = () => new ToonCloudByAntoineC.Shaders.ProgramFragmentShader(),
            ["TopologicaByOtavio"] = () => new TopologicaByOtavio.Shaders.ProgramFragmentShader(),
            ["TorusJourneyByFalcao"] = () => new TorusJourneyByFalcao.Shaders.ProgramFragmentShader(),
            ["ToTheRoadOfRibbon"] = () => new ToTheRoadOfRibbon.Shaders.ProgramFragmentShader(),
            ["TraceConeWithCRTByKlk"] = () => new TraceConeWithCRTByKlk.Shaders.ProgramFragmentShader(),
            ["TreeInGrassBySphinx"] = () => new TreeInGrassBySphinx.Shaders.ProgramFragmentShader(),
            ["TreesByGuil"] = () => new TreesByGuil.Shaders.ProgramFragmentShader(),
            ["TrollsCaveByFatumR"] = () => new TrollsCaveByFatumR.Shaders.ProgramFragmentShader(),
            ["TruchetTentaclesByWaha"] = () => new TruchetTentaclesByWaha.Shaders.ProgramFragmentShader(),
            ["TruePinballPhysicsByArchee"] = () => new TruePinballPhysicsByArchee.Shaders.ProgramFragmentShader(),
            ["TrumpetByBaldand"] = () => new TrumpetByBaldand.Shaders.ProgramFragmentShader(),
            ["Tunnel1ByWaha"] = () => new Tunnel1ByWaha.Shaders.ProgramFragmentShader(),
            ["TwistyTorusByBloxard"] = () => new TwistyTorusByBloxard.Shaders.ProgramFragmentShader(),
            #endregion



        };

    }
}
