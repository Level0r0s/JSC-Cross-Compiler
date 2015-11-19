using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webgl/WebGLTexture.cpp
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webgl/WebGLTexture.idl

	[Script(HasNoPrototype = true, InternalConstructor = true)]
    public class WebGLTexture
    {
        // http://blog.tojicode.com/2011/12/protecting-webgl-content-and-why-you.html

		// https://www.opengl.org/sdk/docs/man/html/glCreateTextures.xhtml

		#region Constructor

		[Obsolete("createTexture")]
		public WebGLTexture(WebGLRenderingContext gl)
		{
			// InternalConstructor
		}

		static WebGLTexture InternalConstructor(WebGLRenderingContext gl)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeWebGLFrameBuffer\ChromeWebGLFrameBuffer\Application.cs
			// X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Library\ShaderToy.cs
			var p = gl.createTexture();

			return p;
		}

		#endregion
	}
}
