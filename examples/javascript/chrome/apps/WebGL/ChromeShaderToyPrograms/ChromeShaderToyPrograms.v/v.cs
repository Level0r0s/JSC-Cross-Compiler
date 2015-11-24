using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.v
{
    class v
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /v/
            {"ChromeShaderToyVornoiCubeMapByBenito", () => new ChromeShaderToyVornoiCubeMapByBenito.Shaders.ProgramFragmentShader()},
            {"ValueToBitArrayByNikos", () => new ValueToBitArrayByNikos.Shaders.ProgramFragmentShader()},
            {"ValveNoiseByImpossible", () => new ValveNoiseByImpossible.Shaders.ProgramFragmentShader()},
            {"VisibleClockByDr2", () => new VisibleClockByDr2.Shaders.ProgramFragmentShader()},
            {"VolumetricHelicesByNimitz", () => new VolumetricHelicesByNimitz.Shaders.ProgramFragmentShader()},
            {"VolumetricRaycastingByXt95", () => new VolumetricRaycastingByXt95.Shaders.ProgramFragmentShader()},
            {"VoronoiFastByDavidbargo", () => new VoronoiFastByDavidbargo.Shaders.ProgramFragmentShader()},

            {"ChromeShaderToyVRCardboardGrid", () => new ChromeShaderToyVRCardboardGrid.Shaders.ProgramFragmentShader()},
            {"VoxelPacManByNrx", () => new VoxelPacManByNrx.Shaders.ProgramFragmentShader()},
            {"VoxelSaturnByGaz", () => new VoxelSaturnByGaz.Shaders.ProgramFragmentShader()},
            {"VoxelTyreByHoskins", () => new VoxelTyreByHoskins.Shaders.ProgramFragmentShader()},
            {"VRTestSceneByRaven", () => new VRTestSceneByRaven.Shaders.ProgramFragmentShader()},
            {"VRMazeByNrx", () => new VRMazeByNrx.Shaders.ProgramFragmentShader()},
            #endregion




        };

    }
}
