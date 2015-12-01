using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.m
{
    class m
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /m/
            {"ChromeShaderToyMarchingCubesByFizzer", () => new ChromeShaderToyMarchingCubesByFizzer.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMarkersByRougier", () => new ChromeShaderToyMarkersByRougier.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMaterialMenuByTekF", () => new ChromeShaderToyMaterialMenuByTekF.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMinecraftBlocksByReinder", () => new ChromeShaderToyMinecraftBlocksByReinder.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMetatunnelByDuprat", () => new ChromeShaderToyMetatunnelByDuprat.Shaders.ProgramFragmentShader()},
            {"MaggotsByKig", () => new MaggotsByKig.Shaders.ProgramFragmentShader()},
            {"MantaRayByDakrunch", () => new MantaRayByDakrunch.Shaders.ProgramFragmentShader()},

            {"MarbleFantasies", () => new MarbleFantasies.Shaders.ProgramFragmentShader()},
            {"MarioMushroomByAlgorithm", () => new MarioMushroomByAlgorithm.Shaders.ProgramFragmentShader()},
            {"MechanicalByIq", () => new MechanicalByIq.Shaders.ProgramFragmentShader()},
            {"MercuryCratersByGuil", () => new MercuryCratersByGuil.Shaders.ProgramFragmentShader()},
            {"MarioCrossEye3DByHLorenzi", () => new MarioCrossEye3DByHLorenzi.Shaders.ProgramFragmentShader()},
            {"MegaWaveByAiekick", () => new MegaWaveByAiekick.Shaders.ProgramFragmentShader()},

            {"MidnightCommsByFizzer", () => new MidnightCommsByFizzer.Shaders.ProgramFragmentShader()},
            {"MidnightSnowByRavenWorks", () => new MidnightSnowByRavenWorks.Shaders.ProgramFragmentShader()},
            {"MarbleSculptureByTekF", () => new MarbleSculptureByTekF.Shaders.ProgramFragmentShader()},
            {"MikeByIq", () => new MikeByIq.Shaders.ProgramFragmentShader()},
            {"MissileGameByAsti", () => new MissileGameByAsti.Shaders.ProgramFragmentShader()},
            {"MobiusHeartsByXMunkki", () => new MobiusHeartsByXMunkki.Shaders.ProgramFragmentShader()},

            {"MirrorBoxByPurton", () => new MirrorBoxByPurton.Shaders.ProgramFragmentShader()},
            {"MirrorRoomByDiLemming", () => new MirrorRoomByDiLemming.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMorningCityByDevin", () => new ChromeShaderToyMorningCityByDevin.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMotionBlurByKig", () => new ChromeShaderToyMotionBlurByKig.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMountainsByHoskins", () => new ChromeShaderToyMountainsByHoskins.Shaders.ProgramFragmentShader()},
            {"ChromeShaderToyMorphingTeapotByIapafoto", () => new ChromeShaderToyMorphingTeapotByIapafoto.Shaders.ProgramFragmentShader()},
            {"MonumentValleyByGltracy", () => new MonumentValleyByGltracy.Shaders.ProgramFragmentShader()},
            {"MoonCratersByGuil", () => new MoonCratersByGuil.Shaders.ProgramFragmentShader()},
            {"MusicMarioByIq", () => new MusicMarioByIq.Shaders.ProgramFragmentShader()},
            {"MyAstronautByLio", () => new MyAstronautByLio.Shaders.ProgramFragmentShader()},
            #endregion

        };

    }
}

//Closing partial types...
//3268:02:01:1e RewriteToAssembly error: System.InvalidOperationException: Sequence contains no elements
//   at System.Linq.Enumerable.Last[TSource](IEnumerable`1 source)
//   at jsc.meta.Library.VolumeFunctions.VolumeFunctionsExtensions.GetnextAvailableDrive() in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\VolumeFunctions\VolumeFunctionsExtensions.cs:line 200
//   at jsc.meta.Library.VolumeFunctions.VolumeFunctionsExtensions.ToVirtualDrive(DirectoryInfo SourceDirectory) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\VolumeFunctions\VolumeFunctionsExtensions.cs:line 156