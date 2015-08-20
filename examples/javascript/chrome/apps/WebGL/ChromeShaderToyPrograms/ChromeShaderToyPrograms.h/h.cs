using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.h
{
    class h
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {

            #region /h/
            // alpha! via discard
            ["ChromeShaderToyHardEdgeShadowByGltracy"] = () => new ChromeShaderToyHardEdgeShadowByGltracy.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyHetchyScketchyByXbe"] = () => new ChromeShaderToyHetchyScketchyByXbe.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyHolographicByTdm"] = () => new ChromeShaderToyHolographicByTdm.Shaders.ProgramFragmentShader(),
            ["HalfLife3ByNikos"] = () => new HalfLife3ByNikos.Shaders.ProgramFragmentShader(),
            ["HauntedForest2ByReinder"] = () => new HauntedForest2ByReinder.Shaders.ProgramFragmentShader(),
            ["HeliByAvix"] = () => new HeliByAvix.Shaders.ProgramFragmentShader(),
            ["HexafieldByKevs"] = () => new HexafieldByKevs.Shaders.ProgramFragmentShader(),
            ["HypercubeByElias"] = () => new HypercubeByElias.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
