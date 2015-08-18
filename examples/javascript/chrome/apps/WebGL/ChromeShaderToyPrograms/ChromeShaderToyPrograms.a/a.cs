using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms.a
{
    class a
    {
        // https://www.youtube.com/watch?v=WkUzHQB0kcA

        // when can we get autodetection of subcomponents available?

        public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
        {


            #region /a/
            ["ACloudByMu6k"] = () => new ACloudByMu6k.Shaders.ProgramFragmentShader(),
            ["AkiyoshisSnakesIllusionByHoskins"] = () => new AkiyoshisSnakesIllusionByHoskins.Shaders.ProgramFragmentShader(),
            ["AlienBeaconByOtavio"] = () => new AlienBeaconByOtavio.Shaders.ProgramFragmentShader(),
            //["AlpineJetsByDr2"] = () => new AlpineJetsByDr2.Shaders.ProgramFragmentShader(),
            ["AngelsByIq"] = () => new AngelsByIq.Shaders.ProgramFragmentShader(),
            ["ApollonianByIq"] = () => new ApollonianByIq.Shaders.ProgramFragmentShader(),
            ["AsteroidsByArchee"] = () => new AsteroidsByArchee.Shaders.ProgramFragmentShader(),
            ["ATreeByGuil"] = () => new ATreeByGuil.Shaders.ProgramFragmentShader(),
            ["AttackOfTheFuzziesByEiffie"] = () => new AttackOfTheFuzziesByEiffie.Shaders.ProgramFragmentShader(),
            ["AtticByRobert"] = () => new AtticByRobert.Shaders.ProgramFragmentShader(),
            ["AudioClayByTekF"] = () => new AudioClayByTekF.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyAnalyticalMotionblurByIq"] = () => new ChromeShaderToyAnalyticalMotionblurByIq.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyAlbertArchesByDr2"] = () => new ChromeShaderToyAlbertArchesByDr2.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyAlpsByHoskins"] = () => new ChromeShaderToyAlpsByHoskins.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyAndroidsByGreen"] = () => new ChromeShaderToyAndroidsByGreen.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyAnimationByFlyguy"] = () => new ChromeShaderToyAnimationByFlyguy.Shaders.ProgramFragmentShader(),
            ["ChromeShaderToyAssBlockByDila"] = () => new ChromeShaderToyAssBlockByDila.Shaders.ProgramFragmentShader(),
            #endregion


        };

    }
}
