using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.n
{
    class n
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {





            #region /n/
            ["ChromeShaderToyNeonParallaxByNimitz"] = () => new ChromeShaderToyNeonParallaxByNimitz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyNoiseDistributionsByHornet"] = () => new ChromeShaderToyNoiseDistributionsByHornet.Shaders.ProgramFragmentShader(),
            ["NanoTubesByTrisomie"] = () => new NanoTubesByTrisomie.Shaders.ProgramFragmentShader(),
            ["NeptunianByEspitz"] = () => new NeptunianByEspitz.Shaders.ProgramFragmentShader(),
            ["NoiseTutorialByAkaitora"] = () => new NoiseTutorialByAkaitora.Shaders.ProgramFragmentShader(),
            ["NSAEyeballByEiffie"] = () => new NSAEyeballByEiffie.Shaders.ProgramFragmentShader(),
            ["NumbersByPMalin"] = () => new NumbersByPMalin.Shaders.ProgramFragmentShader(),
            #endregion



        };

    }
}
