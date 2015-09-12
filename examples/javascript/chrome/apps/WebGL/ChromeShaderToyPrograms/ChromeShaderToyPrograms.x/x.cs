using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.x
{
    class x
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /x/
            ["x16SegmentDisplayByFlyguy"] = () => new x16SegmentDisplayByFlyguy.Shaders.ProgramFragmentShader(),
            ["x2DFoldingByGaz"] = () => new x2DFoldingByGaz.Shaders.ProgramFragmentShader(),
            ["x2DShadowCastingByTharich"] = () => new x2DShadowCastingByTharich.Shaders.ProgramFragmentShader(),
            ["x2DSkullByStanton"] = () => new x2DSkullByStanton.Shaders.ProgramFragmentShader(),

            ["x2001SpaceStationByOtavio"] = () => new x2001SpaceStationByOtavio.Shaders.ProgramFragmentShader(),
            ["x2013ByW23"] = () => new x2013ByW23.Shaders.ProgramFragmentShader(),

            ["x3DExcQByGaz"] = () => new x3DExcQByGaz.Shaders.ProgramFragmentShader(),
            ["x3DLightingByXor"] = () => new x3DLightingByXor.Shaders.ProgramFragmentShader(),
            ["x3DMetashapesByLewiz"] = () => new x3DMetashapesByLewiz.Shaders.ProgramFragmentShader(),

            ["Xor3DAlienLandByXor"] = () => new Xor3DAlienLandByXor.Shaders.ProgramFragmentShader(),
            ["XorMountainsByXor"] = () => new XorMountainsByXor.Shaders.ProgramFragmentShader(),
            ["XorStormByXor"] = () => new XorStormByXor.Shaders.ProgramFragmentShader(),
            ["XyptonjtrozByNimitz"] = () => new XyptonjtrozByNimitz.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
