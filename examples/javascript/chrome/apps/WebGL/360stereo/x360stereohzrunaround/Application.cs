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
using x360stereohzrunaround;
using x360stereohzrunaround.Design;
using x360stereohzrunaround.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace x360stereohzrunaround
{
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

        //C:\Users\Arvo> "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hznosky.png" "/sdcard/oculus/360photos/"
        //1556 KB/s(2714294 bytes in 1.703s)

        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hz2048c3840x2160.png" "/sdcard/oculus/360photos/"



        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809/chrome360hz

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809

        // the eye nor the display will be able to do any stereo
        // until tech is near matrix capability. 2019?

        // cubemap can be used for all long range scenes
        // http://www.imdb.com/title/tt0112111/?ref_=nv_sr_1


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/cubemapcamera
        // subst /D a:
        // subst a: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360stereohzrunaround\x360stereohzrunaround\bin\Debug\staging\x360stereohzrunaround.Application\web
        // subst a: z:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360stereohzrunaround\x360stereohzrunaround\bin\Debug\staging\x360stereohzrunaround.Application\web
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360stereohzrunaround\x360stereohzrunaround\bin\Debug\staging\x360stereohzrunaround.Application\web
        // what if we want to do subst in another winstat or session?

        // ColladaLoader: Empty or non-existing file (assets/x360stereohzrunaround/S6Edge.dae)

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

                        new chrome.Notification(title: "x360stereohzrunaround");

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



            //const int size = 128;
            //const int size = 256; // 6 faces, 12KB
            //const int size = 512; // 6 faces, ?

            // WebGL: drawArrays: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled.

            //const int size = 720; // 6 faces, ?
            //const int size = 1024; // 6 faces, ?
            //const int cubefacesize = 1024; // 6 faces, ?

            // THREE.WebGLRenderer: Texture is not power of two. Texture.minFilter is set to THREE.LinearFilter or THREE.NearestFilter. ( chrome-extension://aemlnmcokphbneegoefdckonejmknohh/assets/x360stereohzrunaround/anvil___spherical_hdri_panorama_skybox_by_macsix_d6vv4hs.jpg )

            // https://support.oculus.com/hc/en-us/articles/204100086-Viewing-Your-Panoramic-Photos-in-Oculus-360-Photos
            // Any JPEG photos with either an equirectangular projection (recommended 4096x2048) or a cube map (recommended 1536x1536 per cube side) will render in the application.


            int cubefacesize = 2048; // 6 faces, ?
            // "X:\vr\tape1\0000x2048.png"
            // for 60hz render we may want to use float camera percision, not available for ui.
            //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x2048.png" "/sdcard/oculus/360photos/"
            //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x128.png" "/sdcard/oculus/360photos/"

            if (Environment.ProcessorCount < 8)
                cubefacesize = 128; // 6 faces, ?

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

            Native.css.style.backgroundColor = "darkcyan";
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
            //scene.rotateOnAxis(new THREE.Vector3(0, 1, 0), -Math.PI / 2);
            scene.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);
            // yet for headtracking we shall rotate camera


            //sceneg.position.set(0, 0, -1024);
            //sceneg.position.set(0, -1024, 0);

            var ambient = new THREE.AmbientLight(0x303030).AttachTo(sceneg);
            //scene.add(ambient);

            // should we fix jsc to do a more correct IDL?
            //var directionalLight = new THREE.DirectionalLight(0xffffff, 0.7);
            //directionalLight.position.set(0, 0, 1);
            //scene.add(directionalLight);

            #region light
            //var light = new THREE.DirectionalLight(0xffffff, 1.0);
            var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 1.5);
            //var lightOffset = new THREE.Vector3(0, 1000, 2500.0);
            var lightOffset = new THREE.Vector3(
                2000,
                700,

                // lower makes longer shadows 
                700.0
                );
            light.position.copy(lightOffset);
            light.castShadow = true;

            var xlight = light as dynamic;
            xlight.shadowMapWidth = 4096;
            xlight.shadowMapHeight = 2048;

            xlight.shadowDarkness = 0.1;
            //xlight.shadowDarkness = 0.5;

            xlight.shadowCameraNear = 10;
            xlight.shadowCameraFar = 10000;
            xlight.shadowBias = 0.00001;
            xlight.shadowCameraRight = 4000;
            xlight.shadowCameraLeft = -4000;
            xlight.shadowCameraTop = 4000;
            xlight.shadowCameraBottom = -4000;

            // wont show if we add skybox?
            xlight.shadowCameraVisible = true;

            //scene.add(light);
            light.AttachTo(sceneg);
            #endregion




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
            canvasNY.canvas.style.transform = "scale(" + uizoom + ")";

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
            canvasPY.canvas.style.transform = "scale(" + uizoom + ")";
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
            canvasNX.canvas.style.transform = "scale(" + uizoom + ")";

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
            canvasPX.canvas.style.transform = "scale(" + uizoom + ")";
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
            canvasNZ.canvas.style.transform = "scale(" + uizoom + ")";

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
            canvasPZ.canvas.style.transform = "scale(" + uizoom + ")";
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





            // need it? no
            //canvasPZ.canvas.tabIndex = 77;

            // Z:\jsc.svn\examples\javascript\async\Test\TestMouseCaptureWhileMove\Application.cs


            // https://visualstudio.uservoice.com/forums/121579-visual-studio-2015/suggestions/10773216-while-var-u
            // https://visualstudio.uservoice.com/forums/121579-visual-studio-2015/suggestions/5342192-awaitable-events

            new { }.With(
                async delegate
                {
                    canvasPZ.canvas.style.cursor = IStyle.CursorEnum.move;
                    canvasPZ.canvas.css.style.border = "1px solid cyan";
                    canvasPZ.canvas.css.hover.style.border = "1px solid yellow";
                    canvasPZ.canvas.css.active.style.border = "1px solid red";
                    var mousedown = default(IEvent);
                    var mousemove = default(IEvent);
                    while (mousedown = await canvasPZ.canvas.async.onmousedown)
                    {
                        var xold = new { mousedown.CursorX, mousedown.CursorY };
                        while (mousemove = await canvasPZ.canvas.async.oncapturedmousemove)
                        {
                            cameraz.valueAsNumber += 2 * (mousemove.CursorX - xold.CursorX);
                            xold = new { mousedown.CursorX, mousedown.CursorY };
                        }
                    }
                }
            );



            canvasNY.canvas.onmousewheel += e =>
            {
                // zoom in out
                cameray.valueAsNumber += -24 * e.WheelDirection;

            };

            // floor
            canvasNY.With(
                 async cc =>
                 {
                     cc.canvas.style.cursor = IStyle.CursorEnum.move;
                     cc.canvas.css.style.border = "1px solid cyan";
                     cc.canvas.css.hover.style.border = "1px solid yellow";
                     cc.canvas.css.active.style.border = "1px solid red";
                     var mousedown = default(IEvent);
                     var mousemove = default(IEvent);
                     while (mousedown = await cc.canvas.async.onmousedown)
                     {
                         //mousedown.xy;
                         var xold = new { mousedown.CursorX, mousedown.CursorY };
                         while (mousemove = await cc.canvas.async.oncapturedmousemove)
                         {
                             // left right
                             cameraz.valueAsNumber += 2 * (mousemove.CursorX - xold.CursorX);
                             // up down
                             camerax.valueAsNumber += -2 * (mousemove.CursorY - xold.CursorY);
                             xold = new { mousedown.CursorX, mousedown.CursorY };
                         }
                     }
                 }
             );



            canvasNZ.With(
               async cc =>
               {
                   cc.canvas.style.cursor = IStyle.CursorEnum.move;
                   cc.canvas.css.style.border = "1px solid cyan";
                   cc.canvas.css.hover.style.border = "1px solid yellow";
                   cc.canvas.css.active.style.border = "1px solid red";
                   var mousedown = default(IEvent);
                   var mousemove = default(IEvent);
                   while (mousedown = await cc.canvas.async.onmousedown)
                   {
                       //mousedown.xy;
                       var xold = new { mousedown.CursorX, mousedown.CursorY };
                       while (mousemove = await cc.canvas.async.oncapturedmousemove)
                       {
                           cameraz.valueAsNumber += -2 * (mousemove.CursorX - xold.CursorX);
                           xold = new { mousedown.CursorX, mousedown.CursorY };
                       }
                   }
               }
           );




            // X:\jsc.svn\examples\javascript\Test\TestMouseMovement\TestMouseMovement\Application.cs
            // https://np.reddit.com/r/GearVR/comments/2typim/john_carmack_on_twitter_next_release_of_oculus/
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151119/x360stereohzrunaround
            // PZ!


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
            c.style.transform = "scale(" + suizoom + ")";
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
            var frame2 = new HTML.Images.FromAssets.anvil___spherical_hdri_panorama_skybox_by_macsix_d6vv4hs().AttachToDocument();
            var frame0 = new HTML.Images.FromAssets.anvil___spherical_hdri_panorama_skybox_by_macsix_d6vv4hs().AttachToDocument();
            //var xor = new HTML.Images.FromAssets.Orion360_test_image_8192x4096().AttachToDocument();
            //var xor = new HTML.Images.FromAssets._2_no_clouds_4k().AttachToDocument();
            //var frame0 = new HTML.Images.FromAssets._2294472375_24a3b8ef46_o().AttachToDocument();


            // 270px
            //xor.style.height = "";
            frame0.style.height = "270px";
            frame0.style.width = "480px";
            frame0.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0 + 480 + 16, 8 + (int)(uizoom * cubefacesize + 8) * 3);


            frame2.style.height = "270px";
            frame2.style.width = "480px";
            frame2.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0 + 480 * 2 + 16 * 2, 8 + (int)(uizoom * cubefacesize + 8) * 3);


            #region  skybox
            // what shall the skybox do if we reach upper altitude?
            // fade into space skybox ?
            var skybox = new THREE.Mesh(new THREE.SphereGeometry(far * 0.9, 50, 50),
           new THREE.MeshBasicMaterial(new
           {
               map = THREE.ImageUtils.loadTexture(
                   //new HTML.Images.FromAssets._2294472375_24a3b8ef46_o().src
                   //new HTML.Images.FromAssets._4008650304_7f837ccbb7_b().src
                  frame0.src
                   //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20130616_222058().src
                   //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20121225_210448().src

                   )
           }));
            skybox.scale.x = -1;
            skybox.AttachTo(sceneg);
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



            #region stero
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151118/x360hzrunaround
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151114/stereo
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151112
            new IHTMLButton { "make me a stero TB image " }.AttachToDocument().With(
                async e =>
                {
                    // http://www.vrideo.com/watch/ALdE7mm
                    // https://www.youtube.com/watch?v=S3iTPxMIlCI

                    var onclick = e.async.onclick;

                    while (await onclick)
                    {
                        var xIPD = 4.0;

                        fcamerax = -xIPD;

                        await Native.window.async.onframe;
                        var f0 = new IHTMLImage { src = gl.canvas.toDataURL() };

                        fcamerax = +xIPD;

                        await Native.window.async.onframe;
                        var f1 = new IHTMLImage { src = gl.canvas.toDataURL() };

                        //await Native.window.async.onframe;
                        await f1.async.oncomplete;

                        var canvasTB = new CanvasRenderingContext2D(c.width, c.height);

                        canvasTB.drawImage(f0, 0, 0, c.width, c.height, 0, 0, c.width, c.height / 2);
                        canvasTB.drawImage(f1, 0, 0, c.width, c.height, 0, c.height / 2, c.width, c.height / 2);

                        frame2.src = canvasTB.canvas.toDataURL();


                        onclick = e.async.onclick;

                        while (!onclick.IsCompleted)
                        {
                            await Task.Delay(1000 / 30);
                            frame0.src = f0.src;
                            await Task.Delay(1000 / 30);
                            frame0.src = f1.src;
                        }
                    }
                }
            );
            #endregion


            var vsync = default(TaskCompletionSource<object>);


            #region render
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
                    //dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });
                }

                total.Restart();



                vsync = new TaskCompletionSource<object>();
                await vsync.Task;

                status = "rendering... vsync";

                var frameid = -1;

                goto beforeframe;
            //fcamerax = -15.0;

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

                if (dir != null)
                    // https://code.google.com/p/chromium/issues/detail?id=404301
                    await dir.WriteAllBytes(filename, gl);

                //await dir.WriteAllBytes(filename, gl.canvas);

                status = "WriteAllBytes... done " + new { fcamerax, filename, swcapture.ElapsedMilliseconds };
                status = "rdy " + new { filename, fcamerax };
            //await Native.window.async.onframe;



                beforeframe:

                // speed? S6 slow motion?
                // this is really slow. if we do x4x2 =x8 
                // https://www.youtube.com/watch?v=r76ULW16Ib8
                //fcamerax += 16 * (1.0 / 60.0);
                fcamerax = 128 * Math.Sin(Math.PI * (frameid - (60 * 30 / 2f)) / (60 * 30 / 2f));
                fcameraz = 256 * Math.Cos(Math.PI * (frameid - (60 * 30 / 2f)) / (60 * 30 / 2f));


                // up
                fcameray = 128 * Math.Cos(Math.PI * (frameid - (60 * 30 / 2f)) / (60 * 30 / 2f));

                //fcamerax += (1.0 / 60.0);

                //fcamerax += (1.0 / 60.0) * 120;

                if (Environment.ProcessorCount < 8)
                    frameid += 30;
                else
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
            #endregion

            // "Z:\jsc.svn\examples\javascript\WebGL\WebGLColladaExperiment\WebGLColladaExperiment\WebGLColladaExperiment.csproj"

            #region WebGLRah66Comanche
            // why isnt it being found?
            // "Z:\jsc.svn\examples\javascript\WebGL\collada\WebGLRah66Comanche\WebGLRah66Comanche\WebGLRah66Comanche.csproj"
            new global::WebGLRah66Comanche.Comanche(
            ).Source.Task.ContinueWithResult(
                dae =>
                {
                    dae.AttachTo(sceneg);
                    //dae.position.y = -40;
                    //dae.position.z = 280;
                    //scene.add(dae);
                    //oo.Add(dae);

                    // wont do it
                    //dae.castShadow = true;

                    dae.children[0].children[0].children.WithEach(x => x.castShadow = true);


                    // the rotors?
                    dae.children[0].children[0].children.Last().children.WithEach(x => x.castShadow = true);


                    dae.scale.set(0.5, 0.5, 0.5);
                    dae.position.x = -900;
                    dae.position.z = +900;

                    // raise it up
                    dae.position.y = 400;

                    //var sw = Stopwatch.StartNew();

                    //Native.window.onframe += delegate
                    //{
                    //    //dae.children[0].children[0].children.Last().al
                    //    //dae.children[0].children[0].children.Last().rotation.z = sw.ElapsedMilliseconds * 0.01;
                    //    //dae.children[0].children[0].children.Last().rotation.x = sw.ElapsedMilliseconds * 0.01;
                    //    dae.children[0].children[0].children.Last().rotation.y = sw.ElapsedMilliseconds * 0.01;
                    //};
                }
            );
            #endregion



            #region tree
            // "Z:\jsc.svn\examples\javascript\WebGL\WebGLGodRay\WebGLGodRay\WebGLGodRay.csproj"

            var materialScene = new THREE.MeshBasicMaterial(new { color = 0x000000, shading = THREE.FlatShading });
            var tloader = new THREE.JSONLoader();

            // http://stackoverflow.com/questions/16539736/do-not-use-system-runtime-compilerservices-dynamicattribute-use-the-dynamic
            // https://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.dynamicattribute%28v=vs.110%29.aspx
            //System.Runtime.CompilerServices.DynamicAttribute

            tloader.load(

                new WebGLGodRay.Models.tree().Content.src,

                new Action<THREE.Geometry>(
                xgeometry =>
                {

                    var treeMesh = new THREE.Mesh(xgeometry, materialScene);
                    treeMesh.position.set(0, -150, -150);
                    treeMesh.position.x = -900;
                    treeMesh.position.z = -900;

                    treeMesh.position.y = 25;

                    var tsc = 400;
                    treeMesh.scale.set(tsc, tsc, tsc);

                    treeMesh.matrixAutoUpdate = false;
                    treeMesh.updateMatrix();


                    //treeMesh.AttachTo(scene);
                    treeMesh.AttachTo(sceneg);

                }
                )
                );
            #endregion


            // http://learningthreejs.com/blog/2013/09/16/how-to-make-the-earth-in-webgl/

            #region create floor

            // THREE.PlaneGeometry: Consider using THREE.PlaneBufferGeometry for lower memory footprint.
            // can we have our checkerboard?

            var floorColors = new[] {
                0xA26D41,
                0xA06040,
                0xAF6F4F,
                // marker to detect horizon
                0xAF0000,



                0xA26D41,
                0xA06040,
                0xAF6F4F,
                // marker to detect horizon
                0x006D00,



                0xA26D41,
                0xA06040,
                0xAF6F4F,
                // marker to detect horizon
                0x0000FF
            };


            // human eye can see up to 10miles, then horizion flattens out.
            var planeGeometry = new THREE.CubeGeometry(2048, 1, 2048);
            var planeGeometryMarkerH = new THREE.CubeGeometry(2048, 1, 2048 * 5);


            var planeGeometryMarkerV = new THREE.CubeGeometry(2048 * 5 * 4, 1, 2048 * 4);
            var planeGeometryV = new THREE.CubeGeometry(2048 * 4, 1, 2048 * 4);
            ////var floor0 = new THREE.Mesh(planeGeometry,
            ////        new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })

            ////    );
            //////plane.castShadow = false;
            ////floor0.receiveShadow = true;
            ////floor0.AttachTo(sceneg);

            ////var floor1 = new THREE.Mesh(planeGeometry,
            ////       //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
            ////       new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA06040, specular = 0xA26D41, shininess = 1 })

            ////   );
            //////plane.castShadow = false;
            ////floor1.receiveShadow = true;
            ////floor1.position.set(2048, 0, 1024);
            ////floor1.AttachTo(sceneg);

            // can we see horizon?
            for (int i = 0; i < 3 * 256; i++)
            {
                var planeGeometry0 = planeGeometry;

                if (i % 4 == 3)
                {
                    planeGeometry0 = planeGeometryMarkerH;

                    // for high altitude zoom level

                    var i4 = (i / 4);


                    var planeGeometryV0 = planeGeometryV;

                    if (i4 % 4 == 3)
                        planeGeometryV0 = planeGeometryMarkerV;

                    {
                        var floor2 = new THREE.Mesh(planeGeometryV0,
                            //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                            new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = floorColors[i4 % floorColors.Length], specular = 0xA26D41, shininess = 1 })

                        );
                        //plane.castShadow = false;
                        floor2.receiveShadow = true;
                        floor2.position.set(1024 * -i, 0, 2048 * i);
                        floor2.AttachTo(sceneg);
                    }


                    {
                        var floor2 = new THREE.Mesh(planeGeometryV0,
                            //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                            new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = floorColors[(i / 4) % floorColors.Length], specular = 0xA26D41, shininess = 1 })

                        );
                        //plane.castShadow = false;
                        floor2.receiveShadow = true;
                        floor2.position.set(-1024 * -i, 0, -2048 * i);
                        floor2.AttachTo(sceneg);
                    }
                }

                {
                    var floor2 = new THREE.Mesh(planeGeometry0,
                        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                        new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = floorColors[i % floorColors.Length], specular = 0xA26D41, shininess = 1 })

                    );
                    //plane.castShadow = false;
                    floor2.receiveShadow = true;
                    floor2.position.set(2048 * i, 0, 1024 * i);
                    floor2.AttachTo(sceneg);
                }

                // flipz
                {
                    var floor2 = new THREE.Mesh(planeGeometry0,
                        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                        new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = floorColors[i % floorColors.Length], specular = 0xA26D41, shininess = 1 })

                    );
                    //plane.castShadow = false;
                    floor2.receiveShadow = true;
                    floor2.position.set(2048 * -i, 0, 1024 * -i);
                    floor2.AttachTo(sceneg);
                }
            }

            #endregion

            #region create walls

            var random = new Random();
            var meshArray = new List<THREE.Mesh>();
            var geometry = new THREE.CubeGeometry(1, 1, 1);
            //var sw = Stopwatch.StartNew();

            for (var i = 3; i < 9; i++)
            {

                //THREE.MeshPhongMaterial
                var ii = new THREE.Mesh(geometry,


                    new THREE.MeshPhongMaterial(new { ambient = 0x000000, color = 0xA06040, specular = 0xA26D41, shininess = 1 })

                    //new THREE.MeshLambertMaterial(
                    //new
                    //{
                    //    color = (Convert.ToInt32(0xffffff * random.NextDouble())),
                    //    specular = 0xffaaaa,
                    //    ambient= 0x050505, 
                    //})

                    );
                ii.position.x = i % 7 * 200 - 2.5f;

                // raise it up
                ii.position.y = .5f * 100 * i;
                ii.position.z = -1 * i * 100;

                ii.castShadow = true;
                ii.receiveShadow = true;
                //ii.scale.set(100, 100, 100 * i);
                ii.scale.set(100, 100 * i, 100);


                meshArray.Add(ii);

                //scene.add(ii);
                ii.AttachTo(sceneg);

                if (i % 2 == 0)
                {

                    // Z:\jsc.svn\examples\javascript\WebGL\WebGLHZBlendCharacter\WebGLHZBlendCharacter\Application.cs
#if FWebGLHZBlendCharacter
                    #region SpeedBlendCharacter
                    var _i = i;
                    { WebGLHZBlendCharacter.HTML.Pages.TexturesImages ref0; }

                    var blendMesh = new THREE.SpeedBlendCharacter();
                    blendMesh.load(
                        new WebGLHZBlendCharacter.Models.marine_anims().Content.src,
                        new Action(
                            delegate
                            {
                                // buildScene
                                //blendMesh.rotation.y = Math.PI * -135 / 180;
                                blendMesh.castShadow = true;
                                // we cannot scale down we want our shadows
                                //blendMesh.scale.set(0.1, 0.1, 0.1);

                                blendMesh.position.x = (_i + 2) % 7 * 200 - 2.5f;

                                // raise it up
                                //blendMesh.position.y = .5f * 100;
                                blendMesh.position.z = -1 * _i * 100;


                                var xtrue = true;
                                // run
                                blendMesh.setSpeed(1.0);

                                // will in turn call THREE.AnimationHandler.play( this );
                                blendMesh.run.play();
                                // this wont help. bokah does not see the animation it seems.
                                blendMesh.run.update(1);

                                blendMesh.showSkeleton(!xtrue);

                                //scene.add(blendMesh);
                                blendMesh.AttachTo(sceneg);


                                //Native.window.onframe +=
                                // delegate
                                // {

                                //     blendMesh.rotation.y = Math.PI * 0.0002 * sw.ElapsedMilliseconds;



                                //     ii.rotation.y = Math.PI * 0.0002 * sw.ElapsedMilliseconds;

                                // };

                            }
                        )
                    );
                    #endregion
#endif
                }

            }
            #endregion


            #region HZCannon
            // "Z:\jsc.svn\examples\javascript\WebGL\HeatZeekerRTSOrto\HeatZeekerRTSOrto\HeatZeekerRTSOrto.csproj"
            new HeatZeekerRTSOrto.HZCannon().Source.Task.ContinueWithResult(
                async cube =>
                {
                    // https://github.com/mrdoob/three.js/issues/1285
                    //cube.children.WithEach(c => c.castShadow = true);

                    //cube.traverse(
                    //    new Action<THREE.Object3D>(
                    //        child =>
                    //        {
                    //            // does it work? do we need it?
                    //            //if (child is THREE.Mesh)

                    //            child.castShadow = true;
                    //            //child.receiveShadow = true;

                    //        }
                    //    )
                    //);

                    // um can edit and continue insert code going back in time?
                    cube.scale.x = 10.0;
                    cube.scale.y = 10.0;
                    cube.scale.z = 10.0;



                    //cube.castShadow = true;
                    //dae.receiveShadow = true;

                    //cube.position.x = -100;

                    ////cube.position.y = (cube.scale.y * 50) / 2;
                    //cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;



                    // if i want to rotate, how do I do it?
                    //cube.rotation.z = random() + Math.PI;
                    //cube.rotation.x = random() + Math.PI;
                    //var sw2 = Stopwatch.StartNew();



                    cube.AttachTo(sceneg);
                    //scene.add(cube);
                    //interactiveObjects.Add(cube);

                    // offset is wrong
                    //while (true)
                    //{
                    //    await Native.window.async.onframe;

                    //    cube.rotation.y = Math.PI * 0.0002 * sw2.ElapsedMilliseconds;

                    //}
                }
            );
            #endregion


            #region HZCannon
            new HeatZeekerRTSOrto.HZCannon().Source.Task.ContinueWithResult(
                async cube =>
                {
                    // https://github.com/mrdoob/three.js/issues/1285
                    //cube.children.WithEach(c => c.castShadow = true);

                    //cube.traverse(
                    //    new Action<THREE.Object3D>(
                    //        child =>
                    //        {
                    //            // does it work? do we need it?
                    //            //if (child is THREE.Mesh)

                    //            child.castShadow = true;
                    //            //child.receiveShadow = true;

                    //        }
                    //    )
                    //);

                    // um can edit and continue insert code going back in time?
                    cube.scale.x = 10.0;
                    cube.scale.y = 10.0;
                    cube.scale.z = 10.0;



                    //cube.castShadow = true;
                    //dae.receiveShadow = true;


                    // jsc shat about out of band code patching?
                    cube.position.z = 600;
                    cube.position.x = -900;
                    //cube.position.y = -400;

                    //cube.position.x = -100;
                    //cube.position.y = -400;

                    ////cube.position.y = (cube.scale.y * 50) / 2;
                    //cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;



                    // if i want to rotate, how do I do it?
                    //cube.rotation.z = random() + Math.PI;
                    //cube.rotation.x = random() + Math.PI;
                    var sw2 = Stopwatch.StartNew();



                    //scene.add(cube);
                    cube.AttachTo(sceneg);
                    //interactiveObjects.Add(cube);

                    // offset is wrong
                    //while (true)
                    //{
                    //    await Native.window.async.onframe;

                    //    cube.rotation.y = Math.PI * 0.0002 * sw2.ElapsedMilliseconds;

                    //}
                }
            );
            #endregion


            #region HZBunker
            new HeatZeekerRTSOrto.HZBunker().Source.Task.ContinueWithResult(
                     cube =>
                     {
                         // https://github.com/mrdoob/three.js/issues/1285
                         //cube.children.WithEach(c => c.castShadow = true);
                         cube.castShadow = true;

                         //cube.traverse(
                         //    new Action<THREE.Object3D>(
                         //        child =>
                         //        {
                         //            // does it work? do we need it?
                         //            //if (child is THREE.Mesh)
                         //            child.castShadow = true;
                         //            //child.receiveShadow = true;

                         //        }
                         //    )
                         //);

                         // um can edit and continue insert code going back in time?
                         cube.scale.x = 10.0;
                         cube.scale.y = 10.0;
                         cube.scale.z = 10.0;

                         //cube.castShadow = true;
                         //dae.receiveShadow = true;

                         cube.position.x = -1000;
                         //cube.position.y = (cube.scale.y * 50) / 2;
                         cube.position.z = 0;

                         cube.AttachTo(sceneg);
                         //scene.add(cube);
                     }
                 );
            #endregion






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
                    //dae.position.x = Math.Sin(fcamerax * 0.001) * 190.0;
                    //dae.position.y = Math.Cos(fcamerax * 0.001) * 90.0;
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







            Console.WriteLine("do you see it?");
        }

    }
}

//{ Message = Could not load file or assembly 'Chrome Web Store, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified. }
//1c48:02:01:14 after worker yield...

//Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.