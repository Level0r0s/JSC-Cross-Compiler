using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.u
{
    class u
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /u/
            ["UglyBrickByPsykotic"] = () => new UglyBrickByPsykotic.Shaders.ProgramFragmentShader(),
            ["UmbrellarByCandycat"] = () => new UmbrellarByCandycat.Shaders.ProgramFragmentShader(),
            ["UselessBoxByMovax"] = () => new UselessBoxByMovax.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
