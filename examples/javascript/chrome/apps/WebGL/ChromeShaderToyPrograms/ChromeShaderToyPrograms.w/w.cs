using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.w
{
    class w
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /w/
            ["WaterfallByZtri"] = () => new WaterfallByZtri.Shaders.ProgramFragmentShader(),
            ["WeirdCanyonByAiekick"] = () => new WeirdCanyonByAiekick.Shaders.ProgramFragmentShader(),

            ["WindWakerOceanByPolyflare"] = () => new WindWakerOceanByPolyflare.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyWireframeByYasuo"] = () => new ChromeShaderToyWireframeByYasuo.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyWetStoneByTDM"] = () => new ChromeShaderToyWetStoneByTDM.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyWolfensteinByReinder"] = () => new ChromeShaderToyWolfensteinByReinder.Shaders.ProgramFragmentShader(),
            ["WinterByIapafoto"] = () => new WinterByIapafoto.Shaders.ProgramFragmentShader(),
            ["WireEggsByEiffie"] = () => new WireEggsByEiffie.Shaders.ProgramFragmentShader(),

            ["WobblyBlobByDr2"] = () => new WobblyBlobByDr2.Shaders.ProgramFragmentShader(),
            ["WobblyThingByAvix"] = () => new WobblyThingByAvix.Shaders.ProgramFragmentShader(),
            ["Wolf128ByFinalPatch"] = () => new Wolf128ByFinalPatch.Shaders.ProgramFragmentShader(),
            ["WormsByIq"] = () => new WormsByIq.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
