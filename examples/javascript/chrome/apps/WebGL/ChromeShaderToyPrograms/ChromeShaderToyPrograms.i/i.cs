using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.i
{
    class i
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {

            #region /i/
            ["ChromeShaderToyInfiniteRepetitionBySsdsa"] = () => new ChromeShaderToyInfiniteRepetitionBySsdsa.Shaders.ProgramFragmentShader(),
            ["IceLakeByXor"] = () => new IceLakeByXor.Shaders.ProgramFragmentShader(),
            ["IcyMoonByDr2"] = () => new IcyMoonByDr2.Shaders.ProgramFragmentShader(),
            ["IkaChanBy301"] = () => new IkaChanBy301.Shaders.ProgramFragmentShader(),
            ["IKSolverByIq"] = () => new IKSolverByIq.Shaders.ProgramFragmentShader(),
            ["ImpactByCabbibo"] = () => new ImpactByCabbibo.Shaders.ProgramFragmentShader(),
            ["ImpactTextTestByCabbibo"] = () => new ImpactTextTestByCabbibo.Shaders.ProgramFragmentShader(),
            ["InterleavedGradientNoiseByAlgorithm"] = () => new InterleavedGradientNoiseByAlgorithm.Shaders.ProgramFragmentShader(),
            ["InputMouseByIq"] = () => new InputMouseByIq.Shaders.ProgramFragmentShader(),
            ["InputTimeByIq"] = () => new InputTimeByIq.Shaders.ProgramFragmentShader(),
            ["InvadersByIapafoto"] = () => new InvadersByIapafoto.Shaders.ProgramFragmentShader(),
            ["InvasionByProtarget"] = () => new InvasionByProtarget.Shaders.ProgramFragmentShader(),
            ["InversionMachineByKali"] = () => new InversionMachineByKali.Shaders.ProgramFragmentShader(),
            ["IslandByVanburgler"] = () => new IslandByVanburgler.Shaders.ProgramFragmentShader(),
            #endregion
        };

    }
}
