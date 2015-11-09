using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EquirectangularToAzimuthal;
using EquirectangularToAzimuthal.Design;
using EquirectangularToAzimuthal.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using ScriptCoreLib.JavaScript.WebAudio;

namespace EquirectangularToAzimuthal
{
    using ScriptCoreLib.GLSL;
    using System.Diagnostics;
    using gl = WebGLRenderingContext;
    using EquirectangularToAzimuthal.HTML.Images.FromAssets;



    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151016/azimuthal
        // works. ssl needs to be trusted tho, othwerwise chrome abuses tcp connections..



        // once this actually works. we can then perhaps start reviewing other shaders that also relay on cubemaps?
        // where we get to generate the cubemaps?
        // https://www.shadertoy.com/view/XsBSDR#

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/equirectangular
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150718/shadertoy
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150706

        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\EquirectangularToAzimuthal\EquirectangularToAzimuthal\bin\Debug\staging\EquirectangularToAzimuthal.Application\web
        // subst b: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\EquirectangularToAzimuthal\EquirectangularToAzimuthal\bin\Debug\staging\EquirectangularToAzimuthal.Application\web


        public Application(IApp page)
        {
            #region += Launched chrome.app.window
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");

                    // pass thru
                }
                else
                {
                    // should jsc send a copresence udp message?
                    chrome.runtime.UpdateAvailable += delegate
                    {
                        new chrome.Notification(title: "UpdateAvailable");

                    };

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "ChromeUDPSendAsync");

                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: null
                        );

                        //xappwindow.setAlwaysOnTop

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion

            Native.body.style.backgroundColor = "yellow";

            // view-source:https://www.shadertoy.com/view/Xls3WS
            // https://www.shadertoy.com/api

            // https://www.shadertoy.com/view/Xls3WS
            // https://www.shadertoy.com/js/cmRenderUtils.js
            // https://www.shadertoy.com/js/effect.js

            // what does it take to import those nice shaders into jsc world?

            // x:\jsc.svn\examples\javascript\webgl\webglchocolux\webglchocolux\application.cs
            // it looks there are no channels.
            // is it a vert or frag?
            //  fragColor = vec4( col, 1.0 );
            // must be a frag
            // <body onload="watchInit()" 


            //EquirectangularToAzimuthal.Library.ShaderToy.AttachToDocument(
            //	new Shaders.ProgramFragmentShader()
            //);

            // WebGL: drawArrays: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled.


            new { }.With(
                async delegate
                {
                    Native.body.style.margin = "0px";
                    (Native.body.style as dynamic).webkitUserSelect = "auto";

                    var vs = new Shaders.ProgramFragmentShader();

                    var mAudioContext = new AudioContext();

                    var gl = new WebGLRenderingContext(alpha: true);

                    if (gl == null)
                    {
                        new IHTMLPre { "webgl disabled?" }.AttachToDocument();
                        return;

                    }

                    var c = gl.canvas.AttachToDocument();

                    //  3840x2160

                    //c.style.SetSize(3840, 2160);

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150722/360-youtube

                    //c.width = 3840;
                    //c.height = 2160;


                    c.width = 512;
                    c.height = 512;

                    // this has the wrong aspect?
                    //c.width = 6466;
                    //c.height = 3232;

                    new IHTMLPre { new { c.width, c.height } }.AttachToDocument();

                    //6466x3232

                    var uizoom = 400f / c.width;

                    c.style.transformOrigin = "0 0";
                    c.style.transform = $"scale({uizoom})";

                    c.style.position = IStyle.PositionEnum.absolute;

                    c.style.SetLocation(500, 8, c.width, c.height);

                    var u = new UIKeepRendering
                    {
                        //animate = true

                        // is chrome portscanning the server??
                        animate = false
                    }.AttachToDocument();

                    //new IHTMLPre { "init..." }.AttachToDocument();

                    // function ShaderToy( parentElement, editorParent, passParent )
                    // function buildInputsUI( me )

                    //  this.mGLContext = createGlContext( this.mCanvas, false, true );
                    //  {alpha: useAlpha, depth: false, antialias: false, stencil: true, premultipliedAlpha: false, preserveDrawingBuffer: usePreserveBuffer } 

                    var mMouseOriX = 0;
                    var mMouseOriY = 0;
                    var mMousePosX = 0;
                    var mMousePosY = 0;

                    // 308
                    //var mEffect = new Library.ShaderToy.Effect(
                    //	mAudioContext,
                    //	gl,

                    //	callback: delegate
                    //	{
                    //		new IHTMLPre { "at callback" }.AttachToDocument();

                    //	},
                    //	obj: null,
                    //	forceMuted: false,
                    //	forcePaused: false
                    //);


                    ////mEffect.mPasses[0].NewTexture
                    //// EffectPass.prototype.NewTexture = function( wa, gl, slot, url )
                    //// this.mPasses[j].Create( rpass.type, this.mAudioContext, this.mGLContext );
                    //// EffectPass.prototype.MakeHeader_Image = function( precission, supportDerivatives )
                    //mEffect.mPasses[0].MakeHeader_Image();

                    //// EffectPass.prototype.NewShader = function( gl, shaderCode )
                    //// EffectPass.prototype.NewShader_Image = function( gl, shaderCode )
                    //mEffect.mPasses[0].NewShader_Image(vs);

                    //// ShaderToy.prototype.resetTime = function()
                    // Effect.prototype.ResetTime = function()

                    // ShaderToy.prototype.startRendering = function()
                    // Effect.prototype.Paint = function(time, mouseOriX, mouseOriY, mousePosX, mousePosY, isPaused)
                    // EffectPass.prototype.Paint = function( wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, xres, yres, isPaused )
                    // EffectPass.prototype.Paint_Image = function( wa, gl, time, mouseOriX, mouseOriY, mousePosX, mousePosY, xres, yres )

                    var pass = new Library.ShaderToy.EffectPass(
                        mAudioContext,
                        gl,
                        precission: Library.ShaderToy.DetermineShaderPrecission(gl),
                        supportDerivatives: gl.getExtension("OES_standard_derivatives") != null,
                        callback: null,
                        obj: null,
                        forceMuted: false,
                        forcePaused: false,
                        //quadVBO: Library.ShaderToy.createQuadVBO(gl, right: 0, top: 0),
                        outputGainNode: null
                    );

                    // how shall we upload our textures?
                    // can we reference GLSL.samplerCube yet?
                    //pass.mInputs[0] = new samplerCube { };
                    //pass.mInputs[0] = new Library.ShaderToy.samplerCube { };

                    var xsampler2D = new Library.ShaderToy.sampler2D { };

                    pass.mInputs[0] = xsampler2D;

                    pass.MakeHeader_Image();
                    pass.NewShader_Image(vs);

                    //var all = new Textures2 { }.Images;
                    var all = new[] {
                      new EquirectangularToAzimuthal.HTML.Images.FromAssets._20151001T0000 { }


                    };

                    new { }.With(
                        async delegate
                        {

                            var i = 0;

                            while (true)
                            {
                                xsampler2D.upload(
                                    all[i % all.Length]
                               //new HTML.Images.FromAssets._20151016T0000 { }
                               );

                                i++;

                                await Task.Delay(1000);
                            }
                        }
                    );



                    var sw = Stopwatch.StartNew();

                    var paintsw = Stopwatch.StartNew();


                    new IHTMLPre { () => new { paintsw.ElapsedMilliseconds } }.AttachToDocument();

                    do
                    {
                        await u.animate.async.@checked;

                        paintsw.Restart();

                        
                        pass.Paint_Image(
                            sw.ElapsedMilliseconds / 1000.0f,

                            mMouseOriX,
                            mMouseOriY,
                            mMousePosX,
                            mMousePosY
                        //,

                        // gl_FragCoord
                        // cannot be scaled, and can be referenced directly.
                        // need another way to scale
                        //zoom: 0.3f
                        );

                        paintsw.Stop();


                        // what does it do?
                        // need nonpartial code.
                        gl.flush();

                    }
                    while (await Native.window.async.onframe);

                }
            );
        }

    }
}
