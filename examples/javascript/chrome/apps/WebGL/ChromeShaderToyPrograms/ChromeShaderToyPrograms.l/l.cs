using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.l
{
    class l
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {

            #region /l/
            ["ChromeShaderToyLavaDripByFabrice"] = () => new ChromeShaderToyLavaDripByFabrice.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyLightThornByVlad"] = () => new ChromeShaderToyLightThornByVlad.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyLightCycleByGreen"] = () => new ChromeShaderToyLightCycleByGreen.Shaders.ProgramFragmentShader(),

            // 32 crash xt7
            ["ChromeShaderToyLimboByDaeken"] = () => new ChromeShaderToyLimboByDaeken.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyLittleMonsterByHLorenzi"] = () => new ChromeShaderToyLittleMonsterByHLorenzi.Shaders.ProgramFragmentShader(),

            ["LaDecimaByCiberxtrem"] = () => new LaDecimaByCiberxtrem.Shaders.ProgramFragmentShader(),
            ["LifeOfATreeByAdam27"] = () => new LifeOfATreeByAdam27.Shaders.ProgramFragmentShader(),

            ["LegendOfZeldaByHLorenzi"] = () => new LegendOfZeldaByHLorenzi.Shaders.ProgramFragmentShader(),
            ["LightingRoomByCaosdoar"] = () => new LightingRoomByCaosdoar.Shaders.ProgramFragmentShader(),
            ["LightiningByAsti"] = () => new LightiningByAsti.Shaders.ProgramFragmentShader(),
            ["LineIntersectionByThe23"] = () => new LineIntersectionByThe23.Shaders.ProgramFragmentShader(),
            ["LineIntersectionInteractiveByThe23"] = () => new LineIntersectionInteractiveByThe23.Shaders.ProgramFragmentShader(),
            ["LittleFluffyCloudsByGreen"] = () => new LittleFluffyCloudsByGreen.Shaders.ProgramFragmentShader(),
            ["LoadingOrbByBjarnia"] = () => new LoadingOrbByBjarnia.Shaders.ProgramFragmentShader(),
            ["LostInTheFieldByRk"] = () => new LostInTheFieldByRk.Shaders.ProgramFragmentShader(),
            #endregion


            ["ChromeShaderToyllamelsByEiffie"] = () => new ChromeShaderToyllamelsByEiffie.Shaders.ProgramFragmentShader(),



        };

    }
}
