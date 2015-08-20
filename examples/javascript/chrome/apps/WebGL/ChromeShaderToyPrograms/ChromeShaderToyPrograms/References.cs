using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
using ScriptCoreLib.GLSL;

namespace ChromeShaderToyPrograms
{
	public static class References
	{
		// https://github.com/mrdoob/three.js/wiki/How-to-use-OpenGL-or-ANGLE-rendering-on-Windows
		// chrome.exe --use-gl=desktop
		//		GL_RENDERER ANGLE(Intel(R) HD Graphics Family Direct3D11 vs_5_0 ps_5_0)
		//GL_VERSION OpenGL ES 2.0 (ANGLE 2.1.524e3bde19d0)


		// http://on-demand.gputechconf.com/gtc/2014/video/S4550-shadertoy-fragment-shader.mp4

		// http://stackoverflow.com/questions/17047602/proper-way-to-initialize-a-c-sharp-dictionary-with-values-already-in-it
		// on nexus 9 we should lear to react to touch move
		// can we get a continious feedback loop going where 
		// we could add content from the live version of the debugged app?
		// like describe what we are seeing and have it stored as source code for the next iteration
		public static Dictionary<string, Func<FragmentShader>> programs = new Dictionary<string, Func<FragmentShader>>
		{
			// should we want to generate it?

			// group by runs on all devices, fps?
			// tags?

			//  /FilterTo:$(SolutionDir)
			// how will those shaders look like on VR?

			// this is special
			["ChromeShaderToySimpleLoadingScreenByNdel"] = () => new ChromeShaderToySimpleLoadingScreenByNdel.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyColumns"] = () => new ChromeShaderToyColumns.Shaders.ProgramFragmentShader(),

			// can we have alt-tab pip ?


		


			#region /j/
			["JackoLanternByPMalin"] = () => new JackoLanternByPMalin.Shaders.ProgramFragmentShader(),
			["JellyfishByVlad"] = () => new JellyfishByVlad.Shaders.ProgramFragmentShader(),
			["JoyDivisionByXbe"] = () => new JoyDivisionByXbe.Shaders.ProgramFragmentShader(),
			["JusterBeaverByMovax"] = () => new JusterBeaverByMovax.Shaders.ProgramFragmentShader(),
			#endregion

		


			["ChromeShaderToyQuadraticBezierByMattdesl"] = () => new ChromeShaderToyQuadraticBezierByMattdesl.Shaders.ProgramFragmentShader(),
			["QuadraticBezierByIq"] = () => new QuadraticBezierByIq.Shaders.ProgramFragmentShader(),

			#region /r/
			["ChromeShaderToyRavingErnieByBero"] = () => new ChromeShaderToyRavingErnieByBero.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRayFogByDemofox"] = () => new ChromeShaderToyRayFogByDemofox.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRacingGameByEiffie"] = () => new ChromeShaderToyRacingGameByEiffie.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRefractiveSpheresByKig"] = () => new ChromeShaderToyRefractiveSpheresByKig.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRedditAlienByGleurop"] = () => new ChromeShaderToyRedditAlienByGleurop.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchByElias"] = () => new ChromeShaderToyRaymarchByElias.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchEdgeDetectionByHLorenzi"] = () => new ChromeShaderToyRaymarchEdgeDetectionByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRaymarchingExampleByJack"] = () => new ChromeShaderToyRaymarchingExampleByJack.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRecogniserByFizzer"] = () => new ChromeShaderToyRecogniserByFizzer.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRefractionByHLorenzi"] = () => new ChromeShaderToyRefractionByHLorenzi.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRelentlessBySrtuss"] = () => new ChromeShaderToyRelentlessBySrtuss.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRockyCoast"] = () => new ChromeShaderToyRockyCoast.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRollingBallByHoskins"] = () => new ChromeShaderToyRollingBallByHoskins.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRotatePyramidByGyabo"] = () => new ChromeShaderToyRotatePyramidByGyabo.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRotatingByGa2arch"] = () => new ChromeShaderToyRotatingByGa2arch.Shaders.ProgramFragmentShader(),
			["RainbowSlicesByFizzer"] = () => new RainbowSlicesByFizzer.Shaders.ProgramFragmentShader(),
			["RayBertByHoskins"] = () => new RayBertByHoskins.Shaders.ProgramFragmentShader(),
			["RayConeRayFrustumByRobert"] = () => new RayConeRayFrustumByRobert.Shaders.ProgramFragmentShader(),
			["RaymarchingAttempt2ByCraxic"] = () => new RaymarchingAttempt2ByCraxic.Shaders.ProgramFragmentShader(),
			["RaymarchingDisplacementByJcanabald"] = () => new RaymarchingDisplacementByJcanabald.Shaders.ProgramFragmentShader(),
			["RaymarchingTutorialByObjelisks"] = () => new RaymarchingTutorialByObjelisks.Shaders.ProgramFragmentShader(),
			["RaymarchingTweaksByLanza"] = () => new RaymarchingTweaksByLanza.Shaders.ProgramFragmentShader(),
			["RedCellsByPMalin"] = () => new RedCellsByPMalin.Shaders.ProgramFragmentShader(),
			["RefelectingCubeByTriggerHLM"] = () => new RefelectingCubeByTriggerHLM.Shaders.ProgramFragmentShader(),
			["ReflectingCatByDr2"] = () => new ReflectingCatByDr2.Shaders.ProgramFragmentShader(),
			["RiseOfShroomByAlgorithm"] = () => new RiseOfShroomByAlgorithm.Shaders.ProgramFragmentShader(),
			["RiverFlightByDr2"] = () => new RiverFlightByDr2.Shaders.ProgramFragmentShader(),
			["RoadToHellByRez"] = () => new RoadToHellByRez.Shaders.ProgramFragmentShader(),
			["RockShapesWIPByAsteropaeus"] = () => new RockShapesWIPByAsteropaeus.Shaders.ProgramFragmentShader(),
			["RoseByHoskins"] = () => new RoseByHoskins.Shaders.ProgramFragmentShader(),
			["RutherfordAtomByMattz"] = () => new RutherfordAtomByMattz.Shaders.ProgramFragmentShader(),
			["ChromeShaderToyRubikSolverByKali"] = () => new ChromeShaderToyRubikSolverByKali.Shaders.ProgramFragmentShader(),
			#endregion

	
			["UselessBoxByMovax"] = () => new UselessBoxByMovax.Shaders.ProgramFragmentShader(),
			["UglyBrickByPsykotic"] = () => new UglyBrickByPsykotic.Shaders.ProgramFragmentShader(),


			["Xor3DAlienLandByXor"] = () => new Xor3DAlienLandByXor.Shaders.ProgramFragmentShader(),
			["XorMountainsByXor"] = () => new XorMountainsByXor.Shaders.ProgramFragmentShader(),
			["XorStormByXor"] = () => new XorStormByXor.Shaders.ProgramFragmentShader(),
			["x2001SpaceStationByOtavio"] = () => new x2001SpaceStationByOtavio.Shaders.ProgramFragmentShader(),
			["x2DFoldingByGaz"] = () => new x2DFoldingByGaz.Shaders.ProgramFragmentShader(),
			["x2DShadowCastingByTharich"] = () => new x2DShadowCastingByTharich.Shaders.ProgramFragmentShader(),
			["x2DSkullByStanton"] = () => new x2DSkullByStanton.Shaders.ProgramFragmentShader(),
			["x3DExcQByGaz"] = () => new x3DExcQByGaz.Shaders.ProgramFragmentShader(),
			["x3DLightingByXor"] = () => new x3DLightingByXor.Shaders.ProgramFragmentShader(),
			["x3DMetashapesByLewiz"] = () => new x3DMetashapesByLewiz.Shaders.ProgramFragmentShader(),

			["YellowMothByDr2"] = () => new YellowMothByDr2.Shaders.ProgramFragmentShader(),

			// cube?

			// 

		};
	}
}
