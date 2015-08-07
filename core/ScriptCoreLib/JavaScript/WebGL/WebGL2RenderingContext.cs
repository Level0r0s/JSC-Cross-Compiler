using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    // https://chromium.googlesource.com/chromium/src/+/master/gpu/blink/webgraphicscontext3d_impl.cc
    // https://chromium.googlesource.com/chromium/src/+/master/gpu/blink/webgraphicscontext3d_impl.h
    // http://dxr.mozilla.org/mozilla-central/source/dom/webidl/WebGL2RenderingContext.webidl
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/WebGL2RenderingContext.webidl
    // https://www.khronos.org/registry/webgl/specs/latest/2.0/webgl2.idl

    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webgl/WebGL2RenderingContextBase.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webgl/WebGL2RenderingContext.idl

    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webgl/WebGLContextAttributes.idl

    // getting closer. 
    // http://webglreport.com/?v=2
    [Script(HasNoPrototype = true, InternalConstructor = true)]
    [Obsolete("for future reference")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class WebGL2RenderingContext : WebGLRenderingContext
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150718/webgl2

        // Vulkan API is more low-level than OpenGL (programmer is responsible for memory and threads management for example), 
        //  there will not need to be a ‘WebVulkan.’ WebGL will continue to absorb capabilities of underlying APIs
        // http://home.seekscale.com/blog/the-future-of-graphics-programming-the-vulkan-api

        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\GLES3\gl3.cs

        // https://code.google.com/p/chromium/issues/detail?id=288731
        // https://code.google.com/p/chromium/issues/detail?id=483890

        // http://toji.github.io/webgl2-particles/

        // http://www.pcworld.com/article/2900814/tested-directx-12s-potential-performance-leap-is-insane.html
        // The only current support for OpenGL is WebGL in Html apps.
        // https://social.msdn.microsoft.com/Forums/windowsapps/en-US/a861db02-dce8-4f61-9969-b8a7a7cd55c7/opengl-used-in-the-metro-style-app?forum=winappswithcsharp

        // https://bugzilla.mozilla.org/show_bug.cgi?id=709490

        // https://code.google.com/p/chromium/issues/detail?id=295792#c13
        // https://code.google.com/p/chromium/issues/detail?id=295792&q=WebGL2&colspec=ID%20Pri%20M%20Iteration%20ReleaseBlock%20Cr%20Status%20Owner%20Summary%20OS%20Modified

        // http://en.wikipedia.org/wiki/Metaverse
        // https://wemo.io/google-chrome-and-the-future-of-virtual-reality-interview-with-531
        // http://gamedev.stackexchange.com/questions/62164/opengl-what-are-the-adoption-rates-of-the-various-versions-and-whats-a-reason

        // http://www.reddit.com/comments/1iy0vj
        // 2 adds multiple render targets which makes it much more reasonable to bring deferred rendering engines to the web.

        // http://blog.tojicode.com/2014/07/bringing-vr-to-chrome.html

        // 20141228
        // can we have multiscreen HZ on webgl yet?
        // the internet still does not yet have any examples for webgl2?

        // https://wiki.mozilla.org/Platform/GFX/WebGL2


        // http://blog.tojicode.com/2013/09/whats-coming-in-webgl-20.html

        // http://blog.tojicode.com/2014/02/how-blink-has-affected-webgl-part-2.html
        // X:\jsc.svn\examples\java\webgl\Test\TestInstancedANGLE\TestInstancedANGLE\Application.cs


        // tested by ?
        // https://www.youtube.com/watch?v=zPNM3yOsP0I
        // https://wiki.mozilla.org/Platform/GFX/WebGL2

        #region Constructor

        public WebGL2RenderingContext(
            )
        {
            // InternalConstructor
        }

        static WebGL2RenderingContext InternalConstructor(

            )
        {
            // tested by X:\jsc.svn\examples\javascript\ImageCachedIntoLocalStorageExperiment\ImageCachedIntoLocalStorageExperiment\Application.cs
            // X:\jsc.svn\examples\javascript\WebGL\Test\TestWebGL2RenderingContext\TestWebGL2RenderingContext\Application.cs

            //  ['webgl2', 'experimental-webgl2']
            // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeWebGLExtensions\ChromeWebGLExtensions\Application.cs

            var canvas = new IHTMLCanvas();
            //var context = (WebGL2RenderingContext)canvas.getContext("experimental-webgl2");
            var context = (WebGL2RenderingContext)canvas.getContext("webgl2");

            return context;
        }

        #endregion
    }
}
