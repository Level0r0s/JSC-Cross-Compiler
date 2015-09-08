using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.j
{
    class j
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {




            #region /j/
            ["JackoLanternByPMalin"] = () => new JackoLanternByPMalin.Shaders.ProgramFragmentShader(),
            ["JellyfishByVlad"] = () => new JellyfishByVlad.Shaders.ProgramFragmentShader(),
            ["JoyDivisionByXbe"] = () => new JoyDivisionByXbe.Shaders.ProgramFragmentShader(),
            ["JusterBeaverByMovax"] = () => new JusterBeaverByMovax.Shaders.ProgramFragmentShader(),
            #endregion




        };

    }
}
