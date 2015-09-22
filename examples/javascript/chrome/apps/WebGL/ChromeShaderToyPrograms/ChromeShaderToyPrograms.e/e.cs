using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.e
{
    class e
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {




            #region /e/
            ["ChromeShaderToyEdgeAAByTrisomie"] = () => new ChromeShaderToyEdgeAAByTrisomie.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyEiffieBallsByEiffie"] = () => new ChromeShaderToyEiffieBallsByEiffie.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyEiffieBox"] = () => new ChromeShaderToyEiffieBox.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyExplosionByGreen"] = () => new ChromeShaderToyExplosionByGreen.Shaders.ProgramFragmentShader(),
            ["EasterEggByNervus"] = () => new EasterEggByNervus.Shaders.ProgramFragmentShader(),
            ["EffusingLavaByNexor"] = () => new EffusingLavaByNexor.Shaders.ProgramFragmentShader(),
            ["ElectroPrimByAlex"] = () => new ElectroPrimByAlex.Shaders.ProgramFragmentShader(),
            ["EllingtonVisitsShaderToyByMPlanck"] = () => new EllingtonVisitsShaderToyByMPlanck.Shaders.ProgramFragmentShader(),
            ["EscherPlanariaByMattz"] = () => new EscherPlanariaByMattz.Shaders.ProgramFragmentShader(),
            ["EveArrivesByIq"] = () => new EveArrivesByIq.Shaders.ProgramFragmentShader(),
            ["ExovoidLogoByRoberto"] = () => new ExovoidLogoByRoberto.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
