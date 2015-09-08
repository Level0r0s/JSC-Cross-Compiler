using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;
// "C:\util\jsc\bin\ScriptCoreLib.dll"
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

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150824
            ["ChromeShaderToySimpleLoadingScreenByNdel"] = () => new ChromeShaderToySimpleLoadingScreenByNdel.Shaders.ProgramFragmentShader(),

			["ChromeShaderToyColumns"] = () => new ChromeShaderToyColumns.Shaders.ProgramFragmentShader(),

            // can we have alt-tab pip ?

            // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyPrograms\ChromeShaderToyPrograms.s\s.cs





			["ChromeShaderToyQuadraticBezierByMattdesl"] = () => new ChromeShaderToyQuadraticBezierByMattdesl.Shaders.ProgramFragmentShader(),
			["QuadraticBezierByIq"] = () => new QuadraticBezierByIq.Shaders.ProgramFragmentShader(),


	
			["UselessBoxByMovax"] = () => new UselessBoxByMovax.Shaders.ProgramFragmentShader(),
			["UglyBrickByPsykotic"] = () => new UglyBrickByPsykotic.Shaders.ProgramFragmentShader(),


		
			["YellowMothByDr2"] = () => new YellowMothByDr2.Shaders.ProgramFragmentShader(),

			// cube?

			// 

		};
	}
}
