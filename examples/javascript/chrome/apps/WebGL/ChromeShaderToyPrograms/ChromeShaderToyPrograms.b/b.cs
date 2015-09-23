using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.b
{
    class b
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {

            #region /b/
            ["BatmanLogoByIq"] = () => new BatmanLogoByIq.Shaders.ProgramFragmentShader(),
            ["BatsByGaz"] = () => new BatsByGaz.Shaders.ProgramFragmentShader(),
            ["BeautypiByIq"] = () => new BeautypiByIq.Shaders.ProgramFragmentShader(),
            ["BeeHiveByMovax"] = () => new BeeHiveByMovax.Shaders.ProgramFragmentShader(),
            ["BeenoxGoesBlackOps3ByFungos"] = () => new BeenoxGoesBlackOps3ByFungos.Shaders.ProgramFragmentShader(),

            
            //["BiplanesInTheBadlandsByDr2"] = () => new BiplanesInTheBadlandsByDr2.Shaders.ProgramFragmentShader(),
            ["BlankspaceByNBickford"] = () => new BlankspaceByNBickford.Shaders.ProgramFragmentShader(),
            ["BlobsByPaulo"] = () => new BlobsByPaulo.Shaders.ProgramFragmentShader(),
            ["BlueWallClockByC3d"] = () => new BlueWallClockByC3d.Shaders.ProgramFragmentShader(),

            ["BobsledByDr2"] = () => new BobsledByDr2.Shaders.ProgramFragmentShader(),
            ["BoingBallByUnitZeroOne"] = () => new BoingBallByUnitZeroOne.Shaders.ProgramFragmentShader(),

            ["BRDFsRUsByAntonalog"] = () => new BRDFsRUsByAntonalog.Shaders.ProgramFragmentShader(),
            ["BokehBlurByKabuto"] = () => new ChromeShaderToyBokehBlurByKabuto.Shaders.ProgramFragmentShader(),
            ["ButterfliesByFizzer"] = () => new ButterfliesByFizzer.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyBenderByIq"] = () => new ChromeShaderToyBenderByIq.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}


//looking at { Name = packages.config }
//{ FixupHintPath = Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\synergy\b\ChromeShaderToyBokehBlurByKabuto\packages\Chrome.Web.Store.1.0.0.0 }
//will need to find package  { id = Chrome.Web.Store }
//will find package  { id = Chrome.Web.Store, ElapsedMilliseconds = 0 }
//updating { id = Chrome.Web.Store, ElapsedMilliseconds = 0 }
//copy { RestorePackagesFromFile = c:/util/jsc/nuget/Chrome.Web.Store.1.0.0.0.nupkg, ElapsedMilliseconds = 0, path = C:\Users\Administrator\AppData\Local\NuGet\Cache\Chrome.Web.Store.1.0.0.0.nupkg }
//extracting bytes from { extracted_path = Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\synergy\b\ChromeShaderToyBokehBlurByKabuto\packages\Chrome.Web.Store.1.0.0.0\Chrome.Web.Store.1.0.0.0.nupkg }
//extracting bytes to { TargetFilePath = Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\synergy\b\ChromeShaderToyBokehBlurByKabuto\packages\Chrome.Web.Store.1.0.0.0\lib\Chrome Web Store.dll }
//updated { id = Chrome.Web.Store, ElapsedMilliseconds = 487 }
//{ FixupHintPath = Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\synergy\b\ChromeShaderToyBokehBlurByKabuto\packages\ScriptCoreLib.Async.1.0.0.0 }
//will need to find package  { id = ScriptCoreLib.Async }
//will find package  { id = ScriptCoreLib.Async, ElapsedMilliseconds = 0 }
//updating { id = ScriptCoreLib.Async, ElapsedMilliseconds = 0 }
//copy { RestorePackagesFromFile = c:/util/jsc/nuget/ScriptCoreLib.Async.1.0.0.0.nupkg, ElapsedMilliseconds = 0, path = C:\Users\Administrator\AppData\Local\NuGet\Cache\ScriptCoreLib.Async.1.0.0.0.nupkg }
//System.IO.IOException: The process cannot access the file 'C:\Users\Administrator\AppData\Local\NuGet\Cache\ScriptCoreLib.Async.1.0.0.0.nupkg' because it is being used by another process.
//   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)