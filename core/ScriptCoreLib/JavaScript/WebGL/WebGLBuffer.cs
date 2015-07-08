using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webgl/WebGLBuffer.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webgl/WebGLBuffer.cpp

	[Script(HasNoPrototype = true, InternalConstructor = true)]
	public class WebGLBuffer
	{
		// https://www.opengl.org/sdk/docs/man/html/glCreateBuffers.xhtml

		#region Constructor

		[Obsolete("createBuffer")]
		public WebGLBuffer(WebGLRenderingContext gl)
		{
			// InternalConstructor
		}

		static WebGLBuffer InternalConstructor(WebGLRenderingContext gl)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
			var p = gl.createBuffer();

			return p;
		}

		#endregion
	}
}
