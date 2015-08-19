using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.o
{
    class o
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /o/
            ["ChromeShaderToyOblivionByNimitz"] = () => new ChromeShaderToyOblivionByNimitz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyOcclusionClippingByIq"] = () => new ChromeShaderToyOcclusionClippingByIq.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyOculusTestByDaeken"] = () => new ChromeShaderToyOculusTestByDaeken.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyOcuLimboByDaeken"] = () => new ChromeShaderToyOcuLimboByDaeken.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyOnOffSpikesByMovAX13h"] = () => new ChromeShaderToyOnOffSpikesByMovAX13h.Shaders.ProgramFragmentShader(),
            ["OblivionByHoskins"] = () => new OblivionByHoskins.Shaders.ProgramFragmentShader(),
            ["OblivionRadarByNdel"] = () => new OblivionRadarByNdel.Shaders.ProgramFragmentShader(),
            ["OldWarehouseByFizzer"] = () => new OldWarehouseByFizzer.Shaders.ProgramFragmentShader(),
            ["OrchardNightByEiffie"] = () => new OrchardNightByEiffie.Shaders.ProgramFragmentShader(),
            ["OrderedDitherByKlk"] = () => new OrderedDitherByKlk.Shaders.ProgramFragmentShader(),
            ["OtherWorldByAlgorithm"] = () => new OtherWorldByAlgorithm.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
