using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.s
{
    class s
    {
        // what about vr.zproxy.xyz
        // and have either janusvr
        // ir gearvr
        // or cardboard look at our collada or sphere skybox?
        // or prerendered 360 video?
    
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {
            
			// this is special
			["ChromeShaderToySimpleLoadingScreenByNdel"] = () => new ChromeShaderToySimpleLoadingScreenByNdel.Shaders.ProgramFragmentShader(),


            #region /s/
            ["ChromeShaderToySadRobotByGreen"] = () => new ChromeShaderToySadRobotByGreen.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySandDunesByBoinx"] = () => new ChromeShaderToySandDunesByBoinx.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySeascapeByTDM"] = () => new ChromeShaderToySeascapeByTDM.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySeabirdsAtSunsetByDr2"] = () => new ChromeShaderToySeabirdsAtSunsetByDr2.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyShadyBuildingByZtri"] = () => new ChromeShaderToyShadyBuildingByZtri.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySomewhere1993ByNimitz"] = () => new ChromeShaderToySomewhere1993ByNimitz.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySpaceRaceByKali"] = () => new ChromeShaderToySpaceRaceByKali.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySphereAndWalls"] = () => new ChromeShaderToySphereAndWalls.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySphereProjectionByIq"] = () => new ChromeShaderToySphereProjectionByIq.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySpitfirePursuitByDr2"] = () => new ChromeShaderToySpitfirePursuitByDr2.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySymmetricOriginsByGood"] = () => new ChromeShaderToySymmetricOriginsByGood.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySubmarinePillars"] = () => new ChromeShaderToySubmarinePillars.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySuperMarioByHLorenzi"] = () => new ChromeShaderToySuperMarioByHLorenzi.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToySnowByUggway"] = () => new ChromeShaderToySnowByUggway.Shaders.ProgramFragmentShader(),
            ["SakuraByFMSCat"] = () => new SakuraByFMSCat.Shaders.ProgramFragmentShader(),
            ["SansNormalByEiffie"] = () => new SansNormalByEiffie.Shaders.ProgramFragmentShader(),
            ["ScatterByGaz"] = () => new ScatterByGaz.Shaders.ProgramFragmentShader(),
            ["SchroedingersCatByDr2"] = () => new SchroedingersCatByDr2.Shaders.ProgramFragmentShader(),
            ["SDFCollisionCheckingByMattz"] = () => new SDFCollisionCheckingByMattz.Shaders.ProgramFragmentShader(),
            ["SeagullByAvix"] = () => new SeagullByAvix.Shaders.ProgramFragmentShader(),
            ["SelfPlayingInterferenceByFatumR"] = () => new SelfPlayingInterferenceByFatumR.Shaders.ProgramFragmentShader(),
            ["SegmentByArthursw"] = () => new SegmentByArthursw.Shaders.ProgramFragmentShader(),
            ["ShaderingChameleonByIapafoto"] = () => new ShaderingChameleonByIapafoto.Shaders.ProgramFragmentShader(),
            ["ShadeyMcShadishByDcoombes"] = () => new ShadeyMcShadishByDcoombes.Shaders.ProgramFragmentShader(),
            ["ShakespeareQuestByEiffie"] = () => new ShakespeareQuestByEiffie.Shaders.ProgramFragmentShader(),
            ["ShapeshifterByMu6k"] = () => new ShapeshifterByMu6k.Shaders.ProgramFragmentShader(),
            ["ShapeLightByBeyondTheStatic"] = () => new ShapeLightByBeyondTheStatic.Shaders.ProgramFragmentShader(),

            ["SHVisualizerByIq"] = () => new SHVisualizerByIq.Shaders.ProgramFragmentShader(),
            ["ShellByFMSCat"] = () => new ShellByFMSCat.Shaders.ProgramFragmentShader(),
            ["SierpinskiByIq"] = () => new SierpinskiByIq.Shaders.ProgramFragmentShader(),
            ["SiggraphLogoByIq"] = () => new SiggraphLogoByIq.Shaders.ProgramFragmentShader(),
            ["Simlicity3DByRunouw"] = () => new Simlicity3DByRunouw.Shaders.ProgramFragmentShader(),
            ["SimpleDigitalClockByMikeCAT"] = () => new SimpleDigitalClockByMikeCAT.Shaders.ProgramFragmentShader(),
            ["SimpleVoxelsByElias"] = () => new SimpleVoxelsByElias.Shaders.ProgramFragmentShader(),
            ["SinMountainsByFred"] = () => new SinMountainsByFred.Shaders.ProgramFragmentShader(),
            ["SminTestByHughsk"] = () => new SminTestByHughsk.Shaders.ProgramFragmentShader(),
            ["SmoothedCSGByTekF"] = () => new SmoothedCSGByTekF.Shaders.ProgramFragmentShader(),
            ["SnowBallByIapafoto"] = () => new SnowBallByIapafoto.Shaders.ProgramFragmentShader(),
            ["SoftShadowTestByDila"] = () => new SoftShadowTestByDila.Shaders.ProgramFragmentShader(),
            ["SomedayByEiffie"] = () => new SomedayByEiffie.Shaders.ProgramFragmentShader(),
            ["SomeSortOfGridByGermangb"] = () => new SomeSortOfGridByGermangb.Shaders.ProgramFragmentShader(),
            ["SoundAcidJamBySrtuss"] = () => new SoundAcidJamBySrtuss.Shaders.ProgramFragmentShader(),

            ["SpaceByReinder"] = () => new SpaceByReinder.Shaders.ProgramFragmentShader(),
            ["SpaceEggByMattz"] = () => new SpaceEggByMattz.Shaders.ProgramFragmentShader(),
            ["SpaceRingsByMu6k"] = () => new SpaceRingsByMu6k.Shaders.ProgramFragmentShader(),

            ["SparksByVanburgler"] = () => new SparksByVanburgler.Shaders.ProgramFragmentShader(),
            ["SparseGridMarchingByNimitz"] = () => new SparseGridMarchingByNimitz.Shaders.ProgramFragmentShader(),
            ["SpeedingInTheDarkByWilddev"] = () => new SpeedingInTheDarkByWilddev.Shaders.ProgramFragmentShader(),
            ["SphereMappingsByNimitz"] = () => new SphereMappingsByNimitz.Shaders.ProgramFragmentShader(),
            ["SphericalVoronoiByMattz"] = () => new SphericalVoronoiByMattz.Shaders.ProgramFragmentShader(),
            ["SpiningRingsBySquid"] = () => new SpiningRingsBySquid.Shaders.ProgramFragmentShader(),
            ["SpoutByPMalin"] = () => new SpoutByPMalin.Shaders.ProgramFragmentShader(),
            ["SpriteEncodingByNikos"] = () => new SpriteEncodingByNikos.Shaders.ProgramFragmentShader(),
            ["StairsByDila"] = () => new StairsByDila.Shaders.ProgramFragmentShader(),
            ["StairwayToHeavenByEiffie"] = () => new StairwayToHeavenByEiffie.Shaders.ProgramFragmentShader(),

            ["StarMapByMorgan"] = () => new StarMapByMorgan.Shaders.ProgramFragmentShader(),
            ["StarwarsShipThingByAlleycatsphinx"] = () => new StarwarsShipThingByAlleycatsphinx.Shaders.ProgramFragmentShader(),

            ["SteamLogoByYakoudbz"] = () => new SteamLogoByYakoudbz.Shaders.ProgramFragmentShader(),
            ["SubmergedByFizzer"] = () => new SubmergedByFizzer.Shaders.ProgramFragmentShader(),
            ["SunsetCloudByKuvkar"] = () => new SunsetCloudByKuvkar.Shaders.ProgramFragmentShader(),
            ["SunsetOnTheSeaByRiccardo"] = () => new SunsetOnTheSeaByRiccardo.Shaders.ProgramFragmentShader(),
            ["SwirlingRingsByyJimmikaelkael"] = () => new SwirlingRingsByyJimmikaelkael.Shaders.ProgramFragmentShader(),
            #endregion



        };

    }
}
