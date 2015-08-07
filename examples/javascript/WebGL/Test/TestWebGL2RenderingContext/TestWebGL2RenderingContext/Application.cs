using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebGL2RenderingContext;
using TestWebGL2RenderingContext.Design;
using TestWebGL2RenderingContext.HTML.Pages;

namespace TestWebGL2RenderingContext
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application : ApplicationWebService
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150718/webgl2

            //https://wiki.mozilla.org/Platform/GFX/WebGL2
            // https://twitter.com/etribz/status/359954523789328387
            // 2years since?
            // https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext
            // cannot find any example. retry later

            var c = new WebGL2RenderingContext();

			new IHTMLPre { new { c } }.AttachToDocument();

            // {{ c = null }}
            // https://github.com/dotnet/corefx/blob/master/src/System.Numerics.Vectors/src/System/Numerics/Matrix4x4.cs
            //  --enable-unsafe-es3-apis.
            // https://groups.google.com/forum/#!topic/webgl-dev-list/8P9Sk47K5hg

            // http://www.marmoset.co/viewer/gallery
            // http://lmv.rocks/

            // "C:\Users\Arvo\AppData\Local\Google\Chrome SxS\Application\chrome.exe - es3.lnk"
            // GL_VERSION	OpenGL ES 2.0 (ANGLE 2.1.0.02df796f466c)

            // GL_VERSION	OpenGL ES 3.0 (ANGLE 2.1.0.02df796f466c)

            ///* Uniform Buffer Objects and Transform Feedback Buffers */
            //421     void bindBufferBase(GLenum target, GLuint index, WebGLBuffer? buffer);
            //422     void bindBufferRange(GLenum target, GLuint index, WebGLBuffer? buffer, GLintptr offset, GLsizeiptr size);

            //  void drawElementsInstanced(GLenum mode, GLsizei count, GLenum type, GLintptr offset, GLsizei instanceCount);

        }

    }
}
