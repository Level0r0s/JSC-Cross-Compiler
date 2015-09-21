using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.d
{
    class d
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /d/

            ["ChromeShaderToyDustyByHat"] = () => new ChromeShaderToyDustyByHat.Shaders.ProgramFragmentShader(),
            ["DistanceFieldBuzzByHat"] = () => new DistanceFieldBuzzByHat.Shaders.ProgramFragmentShader(),


            // crashes nexus?
            ["ChromeShaderToyDancingViriiByEntropyNine"] = () => new ChromeShaderToyDancingViriiByEntropyNine.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDesertMorningByEPitz"] = () => new ChromeShaderToyDesertMorningByEPitz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDepthByGreen"] = () => new ChromeShaderToyDepthByGreen.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDiamondsForeverByNrx"] = () => new ChromeShaderToyDiamondsForeverByNrx.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDiningRoomBySquid"] = () => new ChromeShaderToyDiningRoomBySquid.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDigitalHeartByJoshp"] = () => new ChromeShaderToyDigitalHeartByJoshp.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDigitsByFabrice"] = () => new ChromeShaderToyDigitsByFabrice.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDistanceFieldBlurByTekF"] = () => new ChromeShaderToyDistanceFieldBlurByTekF.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDoomE1M1ByPMalin"] = () => new ChromeShaderToyDoomE1M1ByPMalin.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyDustStormByNimitz"] = () => new ChromeShaderToyDustStormByNimitz.Shaders.ProgramFragmentShader(),
            ["DancingJediByIapafoto"] = () => new DancingJediByIapafoto.Shaders.ProgramFragmentShader(),
            ["DaRasterizerByTDM"] = () => new DaRasterizerByTDM.Shaders.ProgramFragmentShader(),
            ["DataTransferBySrtuss"] = () => new DataTransferBySrtuss.Shaders.ProgramFragmentShader(),
            ["DEDoFByEiffie"] = () => new DEDoFByEiffie.Shaders.ProgramFragmentShader(),
            ["DesertChaseByNdxbxrme"] = () => new DesertChaseByNdxbxrme.Shaders.ProgramFragmentShader(),
            ["DFLightingByTekF"] = () => new DFLightingByTekF.Shaders.ProgramFragmentShader(),
            ["DigitalClockByAndre"] = () => new DigitalClockByAndre.Shaders.ProgramFragmentShader(),
            ["DirectLightUsingMISByKoiava"] = () => new DirectLightUsingMISByKoiava.Shaders.ProgramFragmentShader(),
            ["DIYSpacemanByEiffie"] = () => new DIYSpacemanByEiffie.Shaders.ProgramFragmentShader(),
            ["DNAByOtavio"] = () => new DNAByOtavio.Shaders.ProgramFragmentShader(),
            ["Doom2ByReinder"] = () => new Doom2ByReinder.Shaders.ProgramFragmentShader(),
            ["DragonflyByDr2"] = () => new DragonflyByDr2.Shaders.ProgramFragmentShader(),
            ["DubstepByAssByRez"] = () => new DubstepByAssByRez.Shaders.ProgramFragmentShader(),
            ["DyingUniverseByBigWings"] = () => new DyingUniverseByBigWings.Shaders.ProgramFragmentShader(),
            #endregion

        };

    }
}
