using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.k
{
    class k
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {

            #region /k/
            ["ChromeShaderToyKajastusByMarken"] = () => new ChromeShaderToyKajastusByMarken.Shaders.ProgramFragmentShader(),
            ["KalizylByBergi"] = () => new KalizylByBergi.Shaders.ProgramFragmentShader(),
            ["KMoonByKali"] = () => new KMoonByKali.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
