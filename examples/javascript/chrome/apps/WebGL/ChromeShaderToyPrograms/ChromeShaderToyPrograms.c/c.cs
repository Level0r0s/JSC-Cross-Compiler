using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.c
{
    class c
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {

            #region /c/
            ["CanyonRollerByDr2"] = () => new CanyonRollerByDr2.Shaders.ProgramFragmentShader(),
            ["ChainReactionByEiffie"] = () => new ChainReactionByEiffie.Shaders.ProgramFragmentShader(),
            ["CheesyByPMalin"] = () => new CheesyByPMalin.Shaders.ProgramFragmentShader(),
            ["ChaosTrendLogoByLuther"] = () => new ChaosTrendLogoByLuther.Shaders.ProgramFragmentShader(),
            ["CheeseByMu6k"] = () => new CheeseByMu6k.Shaders.ProgramFragmentShader(),

            ["ClawByGreen"] = () => new ClawByGreen.Shaders.ProgramFragmentShader(),
            ["CloudsTunnelByAvix"] = () => new CloudsTunnelByAvix.Shaders.ProgramFragmentShader(),
            ["CloudsPhysicallyBasedByJamiep"] = () => new CloudsPhysicallyBasedByJamiep.Shaders.ProgramFragmentShader(),
            
            ["ClippedDiscHypertextureByFabrice"] = () => new ClippedDiscHypertextureByFabrice.Shaders.ProgramFragmentShader(),
            ["CompassesByGijs"] = () => new CompassesByGijs.Shaders.ProgramFragmentShader(),
            ["ConformalPolarByFabrice"] = () => new ConformalPolarByFabrice.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyCarcarspacecarByEiffie"] = () => new ChromeShaderToyCarcarspacecarByEiffie.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyCatchByAhihi"] = () => new ChromeShaderToyCatchByAhihi.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyCCLatticesByPaniq"] = () => new ChromeShaderToyCCLatticesByPaniq.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyCentaursByErucipe"] = () => new ChromeShaderToyCentaursByErucipe.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyChainsGearsByPMalin"] = () => new ChromeShaderToyChainsGearsByPMalin.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyCookTorranceByXbe"] = () => new ChromeShaderToyCookTorranceByXbe.Shaders.ProgramFragmentShader(),
            // crashes nexus?
            ["ChromeShaderToyCubeOfCubesByFlyguy"] = () => new ChromeShaderToyCubeOfCubesByFlyguy.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyCubicEntanglementByEiffie"] = () => new ChromeShaderToyCubicEntanglementByEiffie.Shaders.ProgramFragmentShader(),
            ["CrystalBallByAaecheve"] = () => new CrystalBallByAaecheve.Shaders.ProgramFragmentShader(),
            ["CubeOcubeByFred"] = () => new CubeOcubeByFred.Shaders.ProgramFragmentShader(),
            ["CubitreeByXt"] = () => new CubitreeByXt.Shaders.ProgramFragmentShader(),
            ["CyclonicSphereByNusso"] = () => new CyclonicSphereByNusso.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
