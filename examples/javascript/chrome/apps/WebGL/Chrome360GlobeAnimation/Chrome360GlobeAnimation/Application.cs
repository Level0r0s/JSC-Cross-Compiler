#define FWebGLHZBlendCharacter
// can we animate our characters?


//#define AsWEBSERVER
// webserver cannot save images to foler tho
// as webserver imgs will have saves context menu



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
using Chrome360GlobeAnimation;
using Chrome360GlobeAnimation.Design;
using Chrome360GlobeAnimation.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace Chrome360GlobeAnimation
{
    using Chrome360GlobeAnimation.HTML.Images.FromAssets;
    using gl = WebGLRenderingContext;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // http://youtu.be/Lo1IU8UAutE
        // 60hz 2160 4K!

        // The equirectangular projection was used in map creation since it was invented around 100 A.D. by Marinus of Tyre. 

        //        C:\Users\Arvo> "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hzsky.png" "/sdcard/oculus/360photos/"
        //1533 KB/s(3865902 bytes in 2.461s)

        //C:\Users\Arvo> "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape360globe1\0000.png" "/sdcard/oculus/360photos/tape360globe1.png"
        //1556 KB/s(2714294 bytes in 1.703s)

        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hz2048c3840x2160.png" "/sdcard/oculus/360photos/"



        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809/chrome360hz

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809

        // the eye nor the display will be able to do any stereo
        // until tech is near matrix capability. 2019?

        // cubemap can be used for all long range scenes
        // http://www.imdb.com/title/tt0112111/?ref_=nv_sr_1


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/cubemapcamera
        // subst /D b:
        // subst b: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\Chrome360GlobeAnimation\Chrome360GlobeAnimation\bin\Debug\staging\Chrome360GlobeAnimation.Application\web
        // subst a: z:\jsc.svn\examples\javascript\chrome\apps\WebGL\Chrome360GlobeAnimation\Chrome360GlobeAnimation\bin\Debug\staging\Chrome360GlobeAnimation.Application\web
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\Chrome360GlobeAnimation\Chrome360GlobeAnimation\bin\Debug\staging\Chrome360GlobeAnimation.Application\web
        // what if we want to do subst in another winstat or session?

        // ColladaLoader: Empty or non-existing file (assets/Chrome360GlobeAnimation/S6Edge.dae)

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //FormStyler.AtFormCreated =
            //s =>
            //{
            //    s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //    //var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
            //    var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDragWithShadow().AttachTo(s.Context.GetHTMLTarget());



            //    s.Context.GetHTMLTarget().style.backgroundColor = "#efefef";
            //    //s.Context.GetHTMLTarget().style.backgroundColor = "#A26D41";

            //};

#if AsWEBSERVER
            #region += Launched chrome.app.window
            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAppWindow\ChromeTCPServerAppWindow\Application.cs
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                // if we run as a server. we can open up on android.

                //chrome.Notification.DefaultTitle = "Nexus7";
                //chrome.Notification.DefaultIconUrl = new x128().src;
                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                     AppSource.Text
                //, AtFormCreated: FormStyler.AtFormCreated

                //AtFormConstructor:
                //    f =>
                //    {
                //        //arg[0] is typeof System.Int32
                //        //script: error JSC1000: No implementation found for this native method, please implement [static System.Drawing.Color.FromArgb(System.Int32)]

                //        // X:\jsc.svn\examples\javascript\forms\Test\TestFromArgb\TestFromArgb\ApplicationControl.cs

                //        f.BackColor = System.Drawing.Color.FromArgb(0xA26D41);
                //    }
                );
                return;
            }
            #endregion
#else

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
                    //chrome.runtime.UpdateAvailable += delegate
                    //{
                    //    new chrome.Notification(title: "UpdateAvailable");

                    //};

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "Chrome360GlobeAnimation");

                        // https://developer.chrome.com/apps/app_window#type-CreateWindowOptions
                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: new
                               {
                                   alwaysOnTop = true,
                                   visibleOnAllWorkspaces = true
                               }
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


#endif

            // Earth params
            //var radius = 0.5;
            var radius = 1024;

            //var segments = 32;
            var segments = 128;
            var rotation = 6;


            //const int size = 128;
            //const int size = 256; // 6 faces, 12KB
            //const int size = 512; // 6 faces, ?

            // WebGL: drawArrays: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled.

            //const int size = 720; // 6 faces, ?
            //const int size = 1024; // 6 faces, ?
            //const int cubefacesize = 1024; // 6 faces, ?

            // THREE.WebGLRenderer: Texture is not power of two. Texture.minFilter is set to THREE.LinearFilter or THREE.NearestFilter. ( chrome-extension://aemlnmcokphbneegoefdckonejmknohh/assets/Chrome360GlobeAnimation/anvil___spherical_hdri_panorama_skybox_by_macsix_d6vv4hs.jpg )
            int cubefacesize = 2048; // 6 faces, ?
                                     // "X:\vr\tape1\0000x2048.png"
                                     // for 60hz render we may want to use float camera percision, not available for ui.
                                     //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x2048.png" "/sdcard/oculus/360photos/"
                                     //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x128.png" "/sdcard/oculus/360photos/"

            //if (Environment.ProcessorCount < 8)
            //    cubefacesize = 64; // 6 faces, ?

            new IHTMLPre { new { Environment.ProcessorCount, cubefacesize } }.AttachToDocument();

            // can we keep fast fps yet highp?

            // can we choose this on runtime? designtime wants fast fps, yet for end product we want highdef on our render farm?
            //const int cubefacesize = 128; // 6 faces, ?

            //var cubecameraoffsetx = 256;
            var cubecameraoffsetx = 400;


            //var uizoom = 0.1;
            //var uizoom = cubefacesize / 128f;
            var uizoom = 128f / cubefacesize;

            var far = 0xffffff;

            Native.css.style.backgroundColor = "blue";
            Native.css.style.overflow = IStyle.OverflowEnum.hidden;

            Native.body.Clear();
            (Native.body.style as dynamic).webkitUserSelect = "text";

            //new IHTMLPre { "can we stream it into VR, shadertoy, youtube 360, youtube stereo yet?" }.AttachToDocument();


            var sw = Stopwatch.StartNew();



            var oo = new List<THREE.Object3D>();

            var window = Native.window;


            // what about physics and that portal rendering?

            // if we are running as a chrome web server, we may also be opened as android ndk webview app
            //var cameraPX = new THREE.PerspectiveCamera(fov: 90, aspect: window.aspect, near: 1, far: 2000);
            // once we update source
            // save the source
            // manually recompile 
            //cameraPX.position.z = 400;

            //// the camera should be close enough for the object to float off the FOV of PX
            //cameraPX.position.z = 200;

            // scene
            // can we make the 3D object orbit around us ?
            // and
            // stream it to vr?
            var scene = new THREE.Scene();

            // since our cube camera is somewhat a fixed thing
            // would it be easier to move mountains to come to us?
            // once we change code would chrome app be able to let VR know that a new view is available?
            var sceneg = new THREE.Group();
            sceneg.AttachTo(scene);


            // fly up?
            //sceneg.translateZ(-1024);
            // rotate the world, as the skybox then matches what we have on filesystem
            scene.rotateOnAxis(new THREE.Vector3(0, 1, 0), -Math.PI / 2);
            // yet for headtracking we shall rotate camera


            //sceneg.position.set(0, 0, -1024);
            //sceneg.position.set(0, -1024, 0);

            scene.add(new THREE.AmbientLight(0x333333));


            var light = new THREE.DirectionalLight(0xffffff, 1);
            light.position.set(5, 3, 5);
            scene.add(light);





            // whats WebGLRenderTargetCube do?

            // WebGLRenderer preserveDrawingBuffer 



            var renderer0 = new THREE.WebGLRenderer(

                new
                {
                    antialias = true,
                    alpha = true,
                    preserveDrawingBuffer = true
                }
            );

            // https://github.com/mrdoob/three.js/issues/3836

            // the construct. white bg
            //renderer0.setClearColor(0xfffff, 1);
            renderer0.setClearColor(0x0, 1);

            //renderer.setSize(window.Width, window.Height);
            renderer0.setSize(cubefacesize, cubefacesize);

            //renderer0.domElement.AttachToDocument();
            //rendererPX.domElement.style.SetLocation(0, 0);
            //renderer0.domElement.style.SetLocation(4, 4);


            // top

            // http://stackoverflow.com/questions/27612524/can-multiple-webglrenderers-render-the-same-scene


            // need a place to show the cubemap face to GUI 
            // how does the stereo OTOY do it?
            // https://www.opengl.org/wiki/Sampler_(GLSL)

            // http://www.richardssoftware.net/Home/Post/25

            // [+X, –X, +Y, –Y, +Z, –Z] fa



            // move up
            //camera.position.set(-1200, 800, 1200);
            //var cameraoffset = new THREE.Vector3(0, 15, 0);

            // can we aniamte it?
            //var cameraoffset = new THREE.Vector3(0, 800, 1200);
            // can we have linear animation fromcenter of the map to the edge and back?
            // then do the flat earth sun orbit?
            var cameraoffset = new THREE.Vector3(
                // left?
                -512,
                // height?
                //0,
                //1600,
                //1024,

                // if the camera is in the center, would we need to move the scene?
                // we have to move the camera. as we move the scene the lights are messed up
                //2014,
                1024,

                //1200
                0
                // can we hover top of the map?
                );

            // original vieworigin
            //var cameraoffset = new THREE.Vector3(-1200, 800, 1200);



            var camerax = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0 - 2048, max = 0 + 2048, valueAsNumber = -512, title = "camerax" }.AttachToDocument();
            // up. whats the most high a rocket can go 120km?
            new IHTMLHorizontalRule { }.AttachToDocument();


            // how high is the bunker?
            var cameray = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 64, max = 2048, valueAsNumber = 1024, title = "cameray" }.AttachToDocument();
            new IHTMLBreak { }.AttachToDocument();
            var camerayHigh = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = cameray.max, max = 1024 * 256, valueAsNumber = cameray.max, title = "cameray" }.AttachToDocument();
            new IHTMLHorizontalRule { }.AttachToDocument();
            var cameraz = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0 - 2048, max = 0 + 2048, valueAsNumber = 0, title = "cameraz" }.AttachToDocument();

            // for render server
            var fcamerax = 0.0;
            var fcameray = 0.0;
            var fcameraz = 0.0;

            //while (await camerax.async.onchange)

            //cameray.onchange += delegate
            //{
            //    if (cameray.valueAsNumber < cameray.max)
            //        camerayHigh.valueAsNumber = camerayHigh.min;
            //};

            camerayHigh.onmousedown += delegate
            {
                //if (camerayHigh.valueAsNumber > camerayHigh.min)
                cameray.valueAsNumber = cameray.max;
            };


            Action applycameraoffset = delegate
            {
                // make sure UI and gpu sync up

                var cy = cameray;

                if (cameray.valueAsNumber < cameray.max)
                    camerayHigh.valueAsNumber = camerayHigh.min;

                if (camerayHigh.valueAsNumber > camerayHigh.min)
                    cameray.valueAsNumber = cameray.max;

                if (cameray.valueAsNumber == cameray.max)
                    cy = camerayHigh;



                cameraoffset = new THREE.Vector3(
                  // left?
                  camerax + fcamerax,
                   // height?
                   //0,
                   //1600,
                   //1024,

                   // if the camera is in the center, would we need to move the scene?
                   // we have to move the camera. as we move the scene the lights are messed up
                   //2014,
                   cy + fcameray,

                   //1200
                   cameraz + fcameraz
                   // can we hover top of the map?
                   );
            };


            #region y
            // need to rotate90?
            var cameraNY = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: far);
            applycameraoffset += delegate
            {
                cameraNY.position.copy(new THREE.Vector3(0, 0, 0));
                cameraNY.lookAt(new THREE.Vector3(0, -1, 0));
                cameraNY.position.add(cameraoffset);
            };

            //cameraNY.lookAt(new THREE.Vector3(0, 1, 0));
            var canvasNY = new CanvasRenderingContext2D(cubefacesize, cubefacesize);
            canvasNY.canvas.style.SetLocation(cubecameraoffsetx + (int)(uizoom * cubefacesize + 8) * 1, 8 + (int)(uizoom * cubefacesize + 8) * 2);
            canvasNY.canvas.title = "NY";
            canvasNY.canvas.AttachToDocument();
            canvasNY.canvas.style.transformOrigin = "0 0";
            canvasNY.canvas.style.transform = $"scale({uizoom})";

            var cameraPY = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: far);
            applycameraoffset += delegate
            {
                cameraPY.position.copy(new THREE.Vector3(0, 0, 0));
                cameraPY.lookAt(new THREE.Vector3(0, 1, 0));
                cameraPY.position.add(cameraoffset);
            };
            //cameraPY.lookAt(new THREE.Vector3(0, -1, 0));
            var canvasPY = new CanvasRenderingContext2D(cubefacesize, cubefacesize);
            canvasPY.canvas.style.SetLocation(cubecameraoffsetx + (int)(uizoom * cubefacesize + 8) * 1, 8 + (int)(uizoom * cubefacesize + 8) * 0);
            canvasPY.canvas.title = "PY";
            canvasPY.canvas.AttachToDocument();
            canvasPY.canvas.style.transformOrigin = "0 0";
            canvasPY.canvas.style.transform = $"scale({uizoom})";
            #endregion

            // transpose xz?

            #region x
            var cameraNX = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: far);
            applycameraoffset += delegate
            {
                cameraNX.position.copy(new THREE.Vector3(0, 0, 0));
                cameraNX.lookAt(new THREE.Vector3(0, 0, 1));
                cameraNX.position.add(cameraoffset);
            };
            //cameraNX.lookAt(new THREE.Vector3(0, 0, -1));
            //cameraNX.lookAt(new THREE.Vector3(-1, 0, 0));
            //cameraNX.lookAt(new THREE.Vector3(1, 0, 0));
            var canvasNX = new CanvasRenderingContext2D(cubefacesize, cubefacesize);
            canvasNX.canvas.style.SetLocation(cubecameraoffsetx + (int)(uizoom * cubefacesize + 8) * 2, 8 + (int)(uizoom * cubefacesize + 8) * 1);
            canvasNX.canvas.title = "NX";
            canvasNX.canvas.AttachToDocument();
            canvasNX.canvas.style.transformOrigin = "0 0";
            canvasNX.canvas.style.transform = $"scale({uizoom})";

            var cameraPX = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: far);
            applycameraoffset += delegate
            {
                cameraPX.position.copy(new THREE.Vector3(0, 0, 0));
                cameraPX.lookAt(new THREE.Vector3(0, 0, -1));
                cameraPX.position.add(cameraoffset);
            };
            //cameraPX.lookAt(new THREE.Vector3(0, 0, 1));
            //cameraPX.lookAt(new THREE.Vector3(1, 0, 0));
            //cameraPX.lookAt(new THREE.Vector3(-1, 0, 0));
            var canvasPX = new CanvasRenderingContext2D(cubefacesize, cubefacesize);
            canvasPX.canvas.style.SetLocation(cubecameraoffsetx + (int)(uizoom * cubefacesize + 8) * 0, 8 + (int)(uizoom * cubefacesize + 8) * 1);
            canvasPX.canvas.title = "PX";
            canvasPX.canvas.AttachToDocument();
            canvasPX.canvas.style.transformOrigin = "0 0";
            canvasPX.canvas.style.transform = $"scale({uizoom})";
            #endregion



            #region z
            var cameraNZ = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: far);
            //cameraNZ.lookAt(new THREE.Vector3(0, 0, -1));
            applycameraoffset += delegate
            {
                cameraNZ.position.copy(new THREE.Vector3(0, 0, 0));
                cameraNZ.lookAt(new THREE.Vector3(1, 0, 0));
                cameraNZ.position.add(cameraoffset);
            };
            //cameraNX.lookAt(new THREE.Vector3(-1, 0, 0));
            //cameraNZ.lookAt(new THREE.Vector3(0, 0, 1));
            var canvasNZ = new CanvasRenderingContext2D(cubefacesize, cubefacesize);
            canvasNZ.canvas.style.SetLocation(cubecameraoffsetx + (int)(uizoom * cubefacesize + 8) * 3, 8 + (int)(uizoom * cubefacesize + 8) * 1);
            canvasNZ.canvas.title = "NZ";
            canvasNZ.canvas.AttachToDocument();
            canvasNZ.canvas.style.transformOrigin = "0 0";
            canvasNZ.canvas.style.transform = $"scale({uizoom})";

            var cameraPZ = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: far);
            //cameraPZ.lookAt(new THREE.Vector3(1, 0, 0));
            applycameraoffset += delegate
            {
                cameraPZ.position.copy(new THREE.Vector3(0, 0, 0));
                cameraPZ.lookAt(new THREE.Vector3(-1, 0, 0));
                cameraPZ.position.add(cameraoffset);
            };
            //cameraPZ.lookAt(new THREE.Vector3(0, 0, 1));
            //cameraPZ.lookAt(new THREE.Vector3(0, 0, -1));
            var canvasPZ = new CanvasRenderingContext2D(cubefacesize, cubefacesize);
            canvasPZ.canvas.style.SetLocation(cubecameraoffsetx + (int)(uizoom * cubefacesize + 8) * 1, 8 + (int)(uizoom * cubefacesize + 8) * 1);
            canvasPZ.canvas.title = "PZ";
            canvasPZ.canvas.AttachToDocument();
            canvasPZ.canvas.style.transformOrigin = "0 0";
            canvasPZ.canvas.style.transform = $"scale({uizoom})";
            #endregion




            // c++ alias locals would be nice..
            var canvas0 = (IHTMLCanvas)renderer0.domElement;


            var old = new
            {



                CursorX = 0,
                CursorY = 0
            };


            var st = new Stopwatch();
            st.Start();

            //canvas0.css.active.style.cursor = IStyle.CursorEnum.move;

            #region onmousedown
            Native.body.onmousedown +=
                async e =>
                {
                    if (e.Element.nodeName.ToLower() != "canvas")
                        return;

                    // movementX no longer works
                    old = new
                    {


                        e.CursorX,
                        e.CursorY
                    };


                    //e.CaptureMouse();
                    var release = e.Element.CaptureMouse();
                    await e.Element.async.onmouseup;

                    release();


                };
            #endregion



            // X:\jsc.svn\examples\javascript\Test\TestMouseMovement\TestMouseMovement\Application.cs
            #region onmousemove
            Native.body.onmousemove +=
                e =>
                {
                    if (e.Element.nodeName.ToLower() != "canvas")
                    {
                        Native.body.style.cursor = IStyle.CursorEnum.@default;
                        return;
                    }

                    e.preventDefault();
                    e.stopPropagation();


                    Native.body.style.cursor = IStyle.CursorEnum.move;

                    var pointerLock = canvas0 == Native.document.pointerLockElement;


                    //Console.WriteLine(new { e.MouseButton, pointerLock, e.movementX });

                    if (e.MouseButton == IEvent.MouseButtonEnum.Left)
                    {

                        oo.WithEach(
                            x =>
                            {
                                x.rotation.y += 0.006 * (e.CursorX - old.CursorX);
                                x.rotation.x += 0.006 * (e.CursorY - old.CursorY);
                            }
                        );

                        old = new
                        {


                            e.CursorX,
                            e.CursorY
                        };



                    }

                };
            #endregion

            // THREE.WebGLProgram: gl.getProgramInfoLog() C:\fakepath(78,3-98): warning X3557: loop only executes for 1 iteration(s), forcing loop to unroll
            // THREE.WebGLProgram: gl.getProgramInfoLog() (79,3-98): warning X3557: loop only executes for 1 iteration(s), forcing loop to unroll

            // http://www.roadtovr.com/youtube-confirms-stereo-3d-360-video-support-coming-soon/
            // https://www.youtube.com/watch?v=D-Wl9jAB45Q



            #region spherical
            var gl = new WebGLRenderingContext(alpha: true, preserveDrawingBuffer: true);
            var c = gl.canvas.AttachToDocument();

            //  3840x2160

            //c.style.SetSize(3840, 2160);

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150722/360-youtube


            c.width = 3840;
            c.height = 2160;


            //c.width = 3840 * 2;
            //c.height = 2160 * 2;


            //c.width = 3840;
            //c.height = 2160;
            // 1,777777777777778

            // https://www.youtube.com/watch?v=fTfJwzRsE-w
            //c.width = 7580;
            //c.height = 3840;
            //1,973958333333333

            //7580
            //    3840

            // wont work
            //c.width = 8192;
            //c.height = 4096;


            // this has the wrong aspect?
            //c.width = 6466;
            //c.height = 3232;

            new IHTMLPre { new { c.width, c.height } }.AttachToDocument();

            //6466x3232

            //var suizoom = 720f / c.height;
            //var suizoom = 360f / c.height;
            var suizoom = 480f / c.width;

            c.style.transformOrigin = "0 0";
            c.style.transform = $"scale({suizoom})";
            //c.style.backgroundColor = "yellow";
            c.style.position = IStyle.PositionEnum.absolute;

            c.style.SetLocation(8 + (int)(uizoom * cubefacesize + 8) * 0, 8 + (int)(uizoom * cubefacesize + 8) * 3);

            var pass = new CubeToEquirectangular.Library.ShaderToy.EffectPass(
                       null,
                       gl,
                       precission: CubeToEquirectangular.Library.ShaderToy.DetermineShaderPrecission(gl),
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
            pass.mInputs[0] = new CubeToEquirectangular.Library.ShaderToy.samplerCube { };

            pass.MakeHeader_Image();
            var vs = new Shaders.ProgramFragmentShader();
            pass.NewShader_Image(vs);

            #endregion




            //var frame0 = new HTML.Images.FromAssets.tiles_regrid().AttachToDocument();
            var frame0 = new HTML.Images.FromAssets.galaxy_starfield().AttachToDocument();
            //var xor = new HTML.Images.FromAssets.Orion360_test_image_8192x4096().AttachToDocument();
            //var xor = new HTML.Images.FromAssets._2_no_clouds_4k().AttachToDocument();
            //var frame0 = new HTML.Images.FromAssets._2294472375_24a3b8ef46_o().AttachToDocument();


            // 270px
            //xor.style.height = "";
            frame0.style.height = "270px";
            frame0.style.width = "480px";
            frame0.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0 + 480 + 16, 8 + (int)(uizoom * cubefacesize + 8) * 3);





            #region fixup rotation

            //mesh.rotateOnAxis(new THREE.Vector3(1, 0, 0), Math.PI / 2);
            //mesh.rotateOnAxis(new THREE.Vector3(1, 0, 0), -Math.PI / 2);

            // dont need the fixup. unless we want to animate the sky rotate?
            //mesh.rotateOnAxis(new THREE.Vector3(0, 1, 0), -Math.PI / 2);
            #endregion


            // hide the sky to see camera lines?
            //  can we show this as HUD on VR in webview?
            //skybox.visible = false;
            //scene.add(skybox);




            //new IHTMLButton { }

            #region DirectoryEntry
            var dir = default(DirectoryEntry);

            new IHTMLButton { "openDirectory" }.AttachToDocument().onclick += async delegate
            {
                dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });
            };

            frame0.onclick += delegate
            {
                // http://paulbourke.net/papers/vsmm2006/vsmm2006.pdf
                //            A method of creating synthetic stereoscopic panoramic images that can be implemented
                //in most rendering packages has been presented. If single panoramic pairs can be created
                //then stereoscopic panoramic movies are equally possible giving rise to the prospect of
                //movies where the viewer can interact with, at least with regard to what they choose to look
                //at.These images can be projected so as to engage the two features of the human visual
                //system that assist is giving us a sense of immersion, the feeling of “being there”. That is,
                //imagery that contains parallax information as captured from two horizontally separated eye
                //positions (stereopsis)and imagery that fills our peripheral vision.The details that define
                //how the two panoramic images should be created in rendering packages are provided, in
                //particular, how to precisely configure the virtual cameras and control the distance to zero
                //parallax.

                // grab a frame

                if (dir == null)
                {
                    // not exporting to file system?
                    var f0 = new IHTMLImage { src = gl.canvas.toDataURL() };

                    //var f0 = (IHTMLImage)gl.canvas;
                    //var f0 = (IHTMLImage)gl.canvas;
                    //var base64 = gl.canvas.toDataURL();


                    //frame0.src = base64;
                    frame0.src = f0.src;

                    // 7MB!

                    return;
                }

                //                // ---------------------------
                //IrfanView
                //---------------------------
                //Warning !
                //The file: "X:\vr\tape1\0001.jpg" is a PNG file with incorrect extension !
                //Rename ?
                //---------------------------
                //Yes   No   
                //---------------------------

                // haha this will render the thumbnail.
                //dir.WriteAllBytes("0000.png", frame0);

                //dir.WriteAllBytes("0000.png", gl.canvas);

                var glsw = Stopwatch.StartNew();
                dir.WriteAllBytes("0000.png", gl);

                new IHTMLPre { new { glsw.ElapsedMilliseconds } }.AttachToDocument();

                // {{ ElapsedMilliseconds = 1548 }}

                // 3.7MB
                // 3840x2160

            };

            #endregion

            var vsync = default(TaskCompletionSource<object>);

            new IHTMLButton {
                "render 60hz 30sec"
            }.AttachToDocument().onclick += async e =>
            {
                e.Element.disabled = true;


                var total = Stopwatch.StartNew();
                var status = "rendering... " + new { dir };

                new IHTMLPre { () => status }.AttachToDocument();

                if (dir == null)
                {
                    dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });
                }

                total.Restart();



                vsync = new TaskCompletionSource<object>();
                await vsync.Task;

                status = "rendering... vsync";

                var frameid = 0;

                fcamerax = -15.0;

                // parallax offset?

                await_nextframe:


                var filename = frameid.ToString().PadLeft(4, '0') + ".png";
                status = "rendering... " + new { frameid, filename };


                vsync = new TaskCompletionSource<object>();
                await vsync.Task;

                // frame0 has been rendered

                var swcapture = Stopwatch.StartNew();
                status = "WriteAllBytes... " + new { filename };
                //await Native.window.async.onframe;

                // https://code.google.com/p/chromium/issues/detail?id=404301
                await dir.WriteAllBytes(filename, gl);
                //await dir.WriteAllBytes(filename, gl.canvas);

                status = "WriteAllBytes... done " + new { fcamerax, filename, swcapture.ElapsedMilliseconds };
                status = "rdy " + new { filename, fcamerax };
                //await Native.window.async.onframe;

                // speed? S6 slow motion?
                fcamerax += (1.0 / 60.0);
                //fcamerax += (1.0 / 60.0) * 120;
                frameid++;

                // 60hz 30sec
                if (frameid < 60 * 30)
                {
                    // Blob GC? either this helms or the that we made a Blob static. 
                    await Task.Delay(11);

                    goto await_nextframe;
                }

                total.Stop();
                status = "all done " + new { frameid, total.ElapsedMilliseconds };
                vsync = default(TaskCompletionSource<object>);
                // http://stackoverflow.com/questions/22899333/delete-javascript-blobs

                e.Element.disabled = false;
            };

            // "Z:\jsc.svn\examples\javascript\WebGL\WebGLColladaExperiment\WebGLColladaExperiment\WebGLColladaExperiment.csproj"





            #region sphere
            var moonsphere = new THREE.Mesh(
                    new THREE.SphereGeometry(radius * 0.33, segments, segments),
                    new THREE.MeshPhongMaterial(
                            new
                            {
                                map = new THREE.Texture().With(

                                    async s =>
                                    {
                                        //0:75ms event: _2_no_clouds_4k_low view-source:36543
                                        //Application Cache Progress event (1 of 2) http://192.168.1.72:22248/view-source 192.168.1.72/:1
                                        //Application Cache Progress event (2 of 2)  192.168.1.72/:1
                                        //Application Cache Cached event 192.168.1.72/:1
                                        //1:1018ms event: _2_no_clouds_4k_low done view-source:36543
                                        //1:1019ms event: _2_no_clouds_4k view-source:36543
                                        //event.returnValue is deprecated. Please use the standard event.preventDefault() instead. view-source:2995
                                        //1:16445ms event: _2_no_clouds_4k done 

                                        // ~ tilde to open css editor?


                                        //await 20000;

                                        //Console.WriteLine("event: _2_no_clouds_4k");
                                        s.image = await new moon();
                                        s.needsUpdate = true;
                                        //Console.WriteLine("event: _2_no_clouds_4k done");
                                    }
                                ),


                                //bumpMap = THREE.ImageUtils.loadTexture(
                                //    new elev_bump_4k().src
                                ////new elev_bump_4k_low().src
                                //),


                                //// applies onyl to shaders to create the shadow
                                //bumpScale = 0.005,

                                //specularMap = new THREE.Texture().With(
                                //    async s =>
                                //    {

                                //        Console.WriteLine("event: water_4k");
                                //        s.image = await new water_4k().async.oncomplete;
                                //        s.needsUpdate = true;
                                //        Console.WriteLine("event: water_4k done");
                                //    }
                                //),


                                ////specular =    new THREE.Color("grey")								
                                //specular = new THREE.Color(0xa0a0a0)
                            })
                );
            #endregion


            moonsphere.position.set(radius * 4, 0, 0);

            scene.add(moonsphere);


            #region sphere
            var sphere = new THREE.Mesh(
                    new THREE.SphereGeometry(radius, segments, segments),
                    new THREE.MeshPhongMaterial(
                            new
                            {
                                map = new THREE.Texture().With(

                                    async s =>
                                    {
                                        //0:75ms event: _2_no_clouds_4k_low view-source:36543
                                        //Application Cache Progress event (1 of 2) http://192.168.1.72:22248/view-source 192.168.1.72/:1
                                        //Application Cache Progress event (2 of 2)  192.168.1.72/:1
                                        //Application Cache Cached event 192.168.1.72/:1
                                        //1:1018ms event: _2_no_clouds_4k_low done view-source:36543
                                        //1:1019ms event: _2_no_clouds_4k view-source:36543
                                        //event.returnValue is deprecated. Please use the standard event.preventDefault() instead. view-source:2995
                                        //1:16445ms event: _2_no_clouds_4k done 

                                        // ~ tilde to open css editor?


                                        //await 20000;

                                        //Console.WriteLine("event: _2_no_clouds_4k");
                                        s.image = await new _2_no_clouds_4k();
                                        s.needsUpdate = true;
                                        //Console.WriteLine("event: _2_no_clouds_4k done");
                                    }
                                ),


                                bumpMap = THREE.ImageUtils.loadTexture(
                                    new elev_bump_4k().src
                                //new elev_bump_4k_low().src
                                ),


                                // applies onyl to shaders to create the shadow
                                bumpScale = 0.005,

                                specularMap = new THREE.Texture().With(
                                    async s =>
                                    {

                                        Console.WriteLine("event: water_4k");
                                        s.image = await new water_4k().async.oncomplete;
                                        s.needsUpdate = true;
                                        Console.WriteLine("event: water_4k done");
                                    }
                                ),


                                //specular =    new THREE.Color("grey")								
                                specular = new THREE.Color(0xa0a0a0)
                            })
                );
            #endregion

            // http://stackoverflow.com/questions/12447734/three-js-updateing-texture-on-plane


            sphere.rotation.y = rotation;
            scene.add(sphere);


            #region clouds
            var clouds = new THREE.Mesh(
                    new THREE.SphereGeometry(
                        //radius + 0.003,
                        radius + radius * 0.005,
                        segments, segments),
                    new THREE.MeshPhongMaterial(
                        new
                        {
                            //map = THREE.ImageUtils.loadTexture(
                            //    //new fair_clouds_4k().src
                            //    new fair_clouds_4k_low().src
                            //    ),


                            map = new THREE.Texture().With(
                                async s =>
                                {


                                    Console.WriteLine("event: fair_clouds_4k");
                                    s.image = await new fair_clouds_4k().async.oncomplete;
                                    s.needsUpdate = true;
                                    Console.WriteLine("event: fair_clouds_4k done");
                                }
                            ),


                            transparent = true
                        })
                );
            clouds.rotation.y = rotation;
            scene.add(clouds);
            #endregion



            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeEarth\ChromeEarth\Application.cs
            // X:\jsc.svn\examples\javascript\canvas\ConvertBlackToAlpha\ConvertBlackToAlpha\Application.cs
            // hidden for alpha AppWindows
            //#if FBACKGROUND

            #region galaxy_starfield
            new THREE.Texture().With(
                async s =>
                {
                    var i = new HTML.Images.FromAssets.galaxy_starfield();

                    var bytes = await i.async.bytes;

                    //for (int ii = 0; ii < bytes.Length; ii += 4)
                    //{

                    //    bytes[ii + 3] = (byte)(bytes[ii + 0]);

                    //    bytes[ii + 0] = 0xff;
                    //    bytes[ii + 1] = 0xff;
                    //    bytes[ii + 2] = 0xff;
                    //}

                    var cc = new CanvasRenderingContext2D(i.width, i.height);

                    cc.bytes = bytes;

                    s.image = cc;
                    s.needsUpdate = true;

                    var stars_material = new THREE.MeshBasicMaterial(
                            new
                            {
                                //map = THREE.ImageUtils.loadTexture(new galaxy_starfield().src),
                                map = s,
                                side = THREE.BackSide,
                                transparent = true
                            });


                    var stars = new THREE.Mesh(
                            new THREE.SphereGeometry(far * 0.9, 64, 64),
                           stars_material
                        );

                    // http://stackoverflow.com/questions/8502150/three-js-how-can-i-dynamically-change-objects-opacity
                    //(stars_material as dynamic).opacity = 0.5;


                    scene.add(stars);
                }
           );
            #endregion




            new Models.ColladaS6Edge().Source.Task.ContinueWithResult(
                   dae =>
                   {
                       // 90deg
                       dae.rotation.x = -Math.Cos(Math.PI);

                       //dae.scale.x = 30;
                       //dae.scale.y = 30;
                       //dae.scale.z = 30;
                       dae.position.z = -(65 - 200);





                       var scale = 0.9;

                       // jsc, do we have ILObserver available yet?
                       dae.scale.x = scale;
                       dae.scale.y = scale;
                       dae.scale.z = scale;


                       #region onmousewheel
                       Native.body.onmousewheel +=
                           e =>
                           {
                               e.preventDefault();

                               //camera.position.z = 1.5;

                               // min max. shall adjust speed also!
                               // max 4.0
                               // min 0.6
                               dae.position.z -= 10.0 * e.WheelDirection;

                               //camera.position.z = 400;
                               //dae.position.z = dae.position.z.Max(-200).Min(200);

                               //Native.document.title = new { z }.ToString();

                           };
                       #endregion


                       //dae.position.y = -80;

                       //dae.AttachTo(sceneg);
                       //scene.add(dae);
                       //oo.Add(dae);




                       // view-source:http://threejs.org/examples/webgl_multiple_canvases_circle.html
                       // https://threejsdoc.appspot.com/doc/three.js/src.source/extras/cameras/CubeCamera.js.html
                       Native.window.onframe +=
                           e =>
                           {
                               // let render man know..
                               if (vsync != null)
                                   if (vsync.Task.IsCompleted)
                                       return;


                               //if (pause) return;
                               //if (pause.@checked)
                               //    return;


                               // can we float out of frame?
                               // haha. a bit too flickery.
                               //dae.position.x = Math.Sin(e.delay.ElapsedMilliseconds * 0.01) * 50.0;
                               //dae.position.x = Math.Sin(e.delay.ElapsedMilliseconds * 0.001) * 190.0;
                               sphere.position.y = Math.Sin(fcamerax * 0.001) * 90.0;
                               clouds.position.y = Math.Cos(fcamerax * 0.001) * 90.0;

                               //sphere.rotation.y += speed;
                               //clouds.rotation.y += speed;

                               // manual rebuild?
                               // red compiler notifies laptop chrome of pending update
                               // app reloads

                               applycameraoffset();
                               renderer0.clear();
                               //rendererPY.clear();

                               //cameraPX.aspect = canvasPX.aspect;
                               //cameraPX.updateProjectionMatrix();

                               // um what does this do?
                               //cameraPX.position.z += (z - cameraPX.position.z) * e.delay.ElapsedMilliseconds / 200.0;
                               // mousewheel allos the camera to move closer
                               // once we see the frame in vr, can we udp sync vr tracking back to laptop?


                               //this.targetPX.x += 1;
                               //this.targetNX.x -= 1;

                               //this.targetPY.y += 1;
                               //this.targetNY.y -= 1;

                               //this.targetPZ.z += 1;
                               //this.targetNZ.z -= 1;

                               // how does the 360 or shadertoy want our cubemaps?


                               // and then rotate right?

                               // how can we render cubemap?


                               #region x
                               // upside down?
                               renderer0.render(scene, cameraPX);
                               canvasPX.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);

                               renderer0.render(scene, cameraNX);
                               canvasNX.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);
                               #endregion

                               #region z
                               renderer0.render(scene, cameraPZ);
                               canvasPZ.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);

                               renderer0.render(scene, cameraNZ);
                               canvasNZ.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);
                               #endregion



                               #region y
                               renderer0.render(scene, cameraPY);

                               //canvasPY.save();
                               //canvasPY.translate(0, size);
                               //canvasPY.rotate((float)(-Math.PI / 2));
                               canvasPY.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);
                               //canvasPY.restore();


                               renderer0.render(scene, cameraNY);
                               //canvasNY.save();
                               //canvasNY.translate(size, 0);
                               //canvasNY.rotate((float)(Math.PI / 2));
                               canvasNY.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);
                               //canvasNY.restore();
                               // ?
                               #endregion


                               //renderer0.render(scene, cameraPX);


                               //rendererPY.render(scene, cameraPY);

                               // at this point we should be able to render the sphere texture

                               //public const uint TEXTURE_CUBE_MAP_POSITIVE_X = 34069;
                               //public const uint TEXTURE_CUBE_MAP_NEGATIVE_X = 34070;
                               //public const uint TEXTURE_CUBE_MAP_POSITIVE_Y = 34071;
                               //public const uint TEXTURE_CUBE_MAP_NEGATIVE_Y = 34072;
                               //public const uint TEXTURE_CUBE_MAP_POSITIVE_Z = 34073;
                               //public const uint TEXTURE_CUBE_MAP_NEGATIVE_Z = 34074;


                               //var cube0 = new IHTMLImage[] {
                               //        new CSS3DPanoramaByHumus.HTML.Images.FromAssets.humus_px(),
                               //        new CSS3DPanoramaByHumus.HTML.Images.FromAssets.humus_nx(),

                               //        new CSS3DPanoramaByHumus.HTML.Images.FromAssets.humus_py(),
                               //        new CSS3DPanoramaByHumus.HTML.Images.FromAssets.humus_ny(),


                               //        new CSS3DPanoramaByHumus.HTML.Images.FromAssets.humus_pz(),
                               //        new CSS3DPanoramaByHumus.HTML.Images.FromAssets.humus_nz()
                               //};

                               new[] {
                                   canvasPX, canvasNX,
                                   canvasPY, canvasNY,
                                   canvasPZ, canvasNZ
                               }.WithEachIndex(
                                   (img, index) =>
                                   {
                                       gl.bindTexture(gl.TEXTURE_CUBE_MAP, pass.tex);

                                       //gl.pixelStorei(gl.UNPACK_FLIP_X_WEBGL, false);
                                       gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, false);

                                       // http://stackoverflow.com/questions/15364517/pixelstoreigl-unpack-flip-y-webgl-true

                                       // https://msdn.microsoft.com/en-us/library/dn302429(v=vs.85).aspx
                                       //gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 0);
                                       //gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);

                                       gl.texImage2D(gl.TEXTURE_CUBE_MAP_POSITIVE_X + (uint)index, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, img.canvas);

                                   }
                                );

                               // could do dynamic resolution- fog of war or fog of FOV. where up to 150deg field of vision is encouragedm, not 360
                               pass.Paint_Image(
                                     0,

                                     0,
                                     0,
                                     0,
                                     0
                                //,

                                // gl_FragCoord
                                // cannot be scaled, and can be referenced directly.
                                // need another way to scale
                                //zoom: 0.3f
                                );

                               //paintsw.Stop();


                               // what does it do?
                               gl.flush();

                               // let render man know..
                               if (vsync != null)
                                   if (!vsync.Task.IsCompleted)
                                       vsync.SetResult(null);
                           };


                   }
               );





            Console.WriteLine("do you see it?");
        }

    }
}

//{ Message = Could not load file or assembly 'Chrome Web Store, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified. }
//1c48:02:01:14 after worker yield...

//Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.