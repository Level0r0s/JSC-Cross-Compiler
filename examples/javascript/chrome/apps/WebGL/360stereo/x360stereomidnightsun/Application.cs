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
using x360stereomidnightsun;
using x360stereomidnightsun.Design;
using x360stereomidnightsun.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace x360stereomidnightsun
{
    using x360stereomidnightsun.HTML.Images.FromAssets;
    using gl = WebGLRenderingContext;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150912/x83

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150816/iss

        // http://youtu.be/Lo1IU8UAutE
        // 60hz 2160 4K!

        // The equirectangular projection was used in map creation since it was invented around 100 A.D. by Marinus of Tyre. 

        //        C:\Users\Arvo> "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hzsky.png" "/sdcard/oculus/360photos/"
        //1533 KB/s(3865902 bytes in 2.461s)

        //C:\Users\Arvo> "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape360globe1\0000.png" "/sdcard/oculus/360photos/tape360globe1.png"
        //1556 KB/s(2714294 bytes in 1.703s)

        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\hz2048c3840x2160.png" "/sdcard/oculus/360photos/"
        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push   "X:\vr\tape360globe1\0000.png" "/sdcard/oculus/360photos/tape360globe2.png"
        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push   "X:\vr\tape360globe1\0000.png" "/sdcard/oculus/360photos/tape360globenight.png"
        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push   "R:\vr\tape360iss\0000.png" "/sdcard/oculus/360photos/tape360iss.png"
        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push   "R:\vr\tape360iss\0230.png" "/sdcard/oculus/360photos/tape360iss0230.png"

        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push    "X:\vr\sh1\0000.png" "/sdcard/oculus/360photos/sh1.png"
        //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push     "R:\vr\tape360columns\0000.png" "/sdcard/oculus/360photos/tape360columns.png"
        // 4041 KB/s (3248448 bytes in 0.785s)


        // could we udp our 360 image from webgl to vr yet?

        // "R:\vr\tape360iss\0230.png"

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809/chrome360hz

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809

        // the eye nor the display will be able to do any stereo
        // until tech is near matrix capability. 2019?

        // cubemap can be used for all long range scenes
        // http://www.imdb.com/title/tt0112111/?ref_=nv_sr_1


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/cubemapcamera
        // subst /D b:
        // subst b: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360stereomidnightsun\x360stereomidnightsun\bin\Debug\staging\x360stereomidnightsun.Application\web
        // subst a: z:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360stereomidnightsun\x360stereomidnightsun\bin\Debug\staging\x360stereomidnightsun.Application\web
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360stereomidnightsun\x360stereomidnightsun\bin\Debug\staging\x360stereomidnightsun.Application\web
        // what if we want to do subst in another winstat or session?

        // ColladaLoader: Empty or non-existing file (assets/x360stereomidnightsun/S6Edge.dae)


        // https://www.opengl.org/sdk/docs/man/html/radians.xhtml
        static double radians(double degrees)
        {
            return Math.PI * degrees / 180;
        }

        // https://www.opengl.org/sdk/docs/man/html/degrees.xhtml
        static double degrees(double radians)
        {
            return radians * 180 / Math.PI;
        }

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

                        new chrome.Notification(title: "x360stereomidnightsun");

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


            // crash
            //int cubefacesizeMAX = 2048 * 2; // 6 faces, ?
            int cubefacesizeMAX = 2048; // 6 faces, ?
            int cubefacesize = cubefacesizeMAX; // 6 faces, ?
            //int cubefacesize = 1024; // 6 faces, ?
            // "X:\vr\tape1\0000x2048.png"
            // for 60hz render we may want to use float camera percision, not available for ui.
            //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x2048.png" "/sdcard/oculus/360photos/"
            //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x128.png" "/sdcard/oculus/360photos/"

            if (Environment.ProcessorCount < 8)
                //cubefacesize = 64; // 6 faces, ?

                // fast gif?
                cubefacesize = 1024; // 6 faces, ?


            // can we keep fast fps yet highp?

            // can we choose this on runtime? designtime wants fast fps, yet for end product we want highdef on our render farm?
            //const int cubefacesize = 128; // 6 faces, ?

            //var cubecameraoffsetx = 256;
            var cubecameraoffsetx = 400;


            //var uizoom = 0.1;
            //var uizoom = cubefacesize / 128f;
            var uizoom = 128f / cubefacesize;


            Native.css.style.backgroundColor = "darkcyan";
            Native.css.style.overflow = IStyle.OverflowEnum.hidden;

            Native.body.Clear();
            (Native.body.style as dynamic).webkitUserSelect = "text";

            IHTMLCanvas shader1canvas = null;




            //return;

            // Earth params
            //var radius = 0.5;
            //var radius = 1024;
            //var radius = 2048;
            //var radius = 512;
            //var radius = 256;
            //var radius = 400;

            // can we have not fly beyond moon too much?
            //var radius = 500;
            var radius = 480;

            //var segments = 32;
            var segments = 128 * 2;
            //var rotation = 6;


            //const int size = 128;
            //const int size = 256; // 6 faces, 12KB
            //const int size = 512; // 6 faces, ?

            // WebGL: drawArrays: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled.

            //const int size = 720; // 6 faces, ?
            //const int size = 1024; // 6 faces, ?
            //const int cubefacesize = 1024; // 6 faces, ?

            // THREE.WebGLRenderer: Texture is not power of two. Texture.minFilter is set to THREE.LinearFilter or THREE.NearestFilter. ( chrome-extension://aemlnmcokphbneegoefdckonejmknohh/assets/x360stereomidnightsun/anvil___spherical_hdri_panorama_skybox_by_macsix_d6vv4hs.jpg )


            var far = 0xffffff;

            new IHTMLPre { new { Environment.ProcessorCount, cubefacesize } }.AttachToDocument();

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

            //scene.add(new THREE.AmbientLight(0x333333));
            //scene.add(new THREE.AmbientLight(0xffffff));
            //scene.add(new THREE.AmbientLight(0xaaaaaa));
            //scene.add(new THREE.AmbientLight(0xcccccc));
            //scene.add(new THREE.AmbientLight(0xeeeeee));
            scene.add(new THREE.AmbientLight(0xffffff));




            //var light = new THREE.DirectionalLight(0xffffff, 1);
            //// sun should be beyond moon
            ////light.position.set(-5 * virtualDistance, -3 * virtualDistance, -5 * virtualDistance);
            ////light.position.set(-15 * virtualDistance, -1 * virtualDistance, -15 * virtualDistance);

            //// where shall the light source be to see half planet?
            //light.position.set(-1 * virtualDistance, -1 * virtualDistance, -15 * virtualDistance);
            //scene.add(light);



            //var lightX = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -60, max = 60, valueAsNumber = 0, title = "lightX" }.AttachToDocument();
            //var lightY = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -60, max = 60, valueAsNumber = 0, title = "lightY" }.AttachToDocument();
            //var lightZ = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -60, max = 60, valueAsNumber = 0, title = "lightZ" }.AttachToDocument();

            //new IHTMLHorizontalRule { }.AttachToDocument();

            // whats WebGLRenderTargetCube do?

            // WebGLRenderer preserveDrawingBuffer 



            var renderer0 = new THREE.WebGLRenderer(

                new
                {
                    //antialias = true,
                    alpha = true,
                    preserveDrawingBuffer = true
                }
            );

            // https://github.com/mrdoob/three.js/issues/3836

            // the construct. white bg
            //renderer0.setClearColor(0xfffff, 1);
            //renderer0.setClearColor(0x0, 1);
            renderer0.setClearColor(0x0, 0);

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

            // whatif we want more than 30sec video? 2min animation? more frames to render? 2gb disk?





            var maxfps = 60;
            //var maxlengthseconds = 60;
            var maxlengthseconds = 120;

            var maxframes = maxlengthseconds * maxfps;

            var frameIDanimation = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, title = "frameIDanimation", @checked = false }.AttachToDocument();

            // whatif we want more than 30sec video? 2min animation? more frames to render? 2gb disk?
            var frameIDslider = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0, max = maxframes, valueAsNumber = 0, title = "frameIDslider" }.AttachToDocument();
            //var frameIDslider = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0, max = 1800, valueAsNumber = 1800 / 2, title = "frameIDslider" }.AttachToDocument();




            new IHTMLHorizontalRule { }.AttachToDocument();

            var camerax = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -32, max = 32, valueAsNumber = 0, title = "camerax" }.AttachToDocument();
            camerax.css.after.contentText = "x: ";
            new IHTMLBreak { }.AttachToDocument();

            //camerax.style.borderLeft = "1em solid red";

            // up. whats the most high a rocket can go 120km?
            //new IHTMLHorizontalRule { }.AttachToDocument();

            // how high is the bunker?
            var cameray = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -32, max = 32, valueAsNumber = 0, title = "cameray" }.AttachToDocument();
            cameray.css.after.contentText = "y: ";
            new IHTMLBreak { }.AttachToDocument();

            // we wont be going to orbit
            //new IHTMLBreak { }.AttachToDocument();
            //var camerayHigh = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = cameray.max, max = 1024 * 256, valueAsNumber = cameray.max, title = "cameray" }.AttachToDocument();
            //new IHTMLHorizontalRule { }.AttachToDocument();
            //var cameraz = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0 - 2048 * 4, max = 0 + 2048 * 4, valueAsNumber = 0, title = "cameraz" }.AttachToDocument();
            //var cameraz = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -2048 / 2, max = 0 + 2048 / 2, valueAsNumber = 0, title = "cameraz" }.AttachToDocument();
            var cameraz = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -32, max = 32, valueAsNumber = 0, title = "cameraz" }.AttachToDocument();
            cameraz.css.after.contentText = "z: ";

            // for render server
            var fcamerax = 0.0;
            var fcameray = 0.0;
            var fcameraz = 0.0;


            new IHTMLHorizontalRule { }.AttachToDocument();


            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151114/stereo
            var itemRotation = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -180, max = 180, valueAsNumber = 0, title = "itemRotation" }.AttachToDocument();
            //var itemRotation = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -90, max = 90, valueAsNumber = 33, title = "itemRotation" }.AttachToDocument();

            //while (await camerax.async.onchange)

            //cameray.onchange += delegate
            //{
            //    if (cameray.valueAsNumber < cameray.max)
            //        camerayHigh.valueAsNumber = camerayHigh.min;
            //};

            //camerayHigh.onmousedown += delegate
            //{
            //    //if (camerayHigh.valueAsNumber > camerayHigh.min)
            //    cameray.valueAsNumber = cameray.max;
            //};


            Action applycameraoffset = delegate
            {
                // make sure UI and gpu sync up

                var cy = cameray;



                // we wont be going to orbit

                //if (cameray.valueAsNumber < cameray.max)
                //    camerayHigh.valueAsNumber = camerayHigh.min;

                //if (camerayHigh.valueAsNumber > camerayHigh.min)
                //    cameray.valueAsNumber = cameray.max;

                //if (cameray.valueAsNumber == cameray.max)
                //    cy = camerayHigh;



                cameraoffset = new THREE.Vector3(
                    // left?
                  1.0 * (camerax + fcamerax),
                    // height?
                    //0,
                    //1600,
                    //1024,

                   // if the camera is in the center, would we need to move the scene?
                    // we have to move the camera. as we move the scene the lights are messed up
                    //2014,
                   1.0 * (cy + fcameray),

                 //1200c
                 1.0 * (cameraz + fcameraz)
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
            // roslyn!
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

            // lets have the item twice the cube item size. and offset -0.5 to recenter.
            // this wont work as we are cloning the buffer for now!
            //var canvasPXitem = new CanvasRenderingContext2D(cubefacesize, cubefacesize * 2);
            var canvasPXitem = new CanvasRenderingContext2D(cubefacesize, cubefacesize);
            canvasPXitem.canvas.style.SetLocation(cubecameraoffsetx + (int)(uizoom * cubefacesize + 8) * 0, 8 + (int)(uizoom * cubefacesize + 8) * 2);
            canvasPXitem.canvas.title = "item";
            canvasPXitem.canvas.AttachToDocument();
            canvasPXitem.canvas.style.transformOrigin = "0 0";
            canvasPXitem.canvas.style.transform = "scale(" + uizoom + ")";
            canvasPXitem.canvas.style.border = "1px solid yellow";

            //canvasPXitem.fillText("hello", 1, 1, cubefacesize);

            //canvasPXitem.drawImage(
            //    //new IHTMLPre { "hello" }
            //    new IHTMLDiv { "hello world. can we draw html into 360 VR yet?" }, 0, 0, cubefacesize, cubefacesize
            //);







            // http://www.w3schools.com/tags/canvas_fillstyle.asp
            canvasPXitem.fillStyle = "red";

            // too big?
            //canvasPXitem.fillRect(
            //    x: cubefacesize / 3,
            //    y: cubefacesize / 4,
            //    w: cubefacesize / 3,
            //    h: cubefacesize / 2
            //);

            canvasPXitem.fillRect(
               x: (cubefacesize - cubefacesize / 6) / 2,
               y: (cubefacesize - cubefacesize / 3) / 2,

               w: cubefacesize / 6,
               h: cubefacesize / 3
           );



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




            // X:\jsc.svn\examples\javascript\Test\TestMouseMovement\TestMouseMovement\Application.cs


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

            // bots cannot get a bigger mp4 from yt, and vrideo renders 2k on gearvr.
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
            var frame0 = new HTML.Images.FromAssets.galaxy_starfield().AttachToDocument();
            //var frame0 = new HTML.Images.FromAssets.galaxy_starfield150FOV().AttachToDocument();
            //var xor = new HTML.Images.FromAssets.Orion360_test_image_8192x4096().AttachToDocument();
            //var xor = new HTML.Images.FromAssets._2_no_clouds_4k().AttachToDocument();
            //var frame0 = new HTML.Images.FromAssets._2294472375_24a3b8ef46_o().AttachToDocument();


            // 270px
            //xor.style.height = "";
            frame0.style.height = "270px";
            frame0.style.width = "480px";
            frame0.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0 + 480 + 16, 8 + (int)(uizoom * cubefacesize + 8) * 3);



            var frame2 = new HTML.Images.FromAssets.galaxy_starfield().AttachToDocument();

            frame2.style.height = "270px";
            frame2.style.width = "480px";
            frame2.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0 + 480 * 2 + 16 * 2, 8 + (int)(uizoom * cubefacesize + 8) * 3);




            #region DirectoryEntry
            var dir = default(DirectoryEntry);

            new IHTMLButton { "openDirectory" }.AttachToDocument().onclick += async delegate
            {
                dir = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });
            };
            frame0.style.cursor = IStyle.CursorEnum.pointer;
            frame0.title = "save frame";


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

            #region render 60hz 30sec
            new IHTMLButton {
                //"render 60hz 30sec"
                //$"render {maxfps}hz {maxlengthseconds}sec"
                "render " + new {maxfps} + "hz " + new {maxlengthseconds} + "sec"
            }.AttachToDocument().onclick += async e =>
            {
                e.Element.disabled = true;

                var canvasTB = new CanvasRenderingContext2D(c.width * 2, c.height * 2);


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

                //var frameid = 0;
                frameIDanimation.@checked = true;
                frameIDslider.valueAsNumber = -1;

                // allow the animation values to sink in
                vsync = new TaskCompletionSource<object>();
                await vsync.Task;



                goto beforeframe;


                // parallax offset?

                await_nextframe:


                var filename = frameIDslider.valueAsNumber.ToString().PadLeft(5, '0') + ".jpg";
                status = "rendering... " + new { filename };



                var xIPD = 4.0;


                // left eye
                fcamerax = -xIPD;
                vsync = new TaskCompletionSource<object>();
                await vsync.Task;
                var f0 = new IHTMLImage { src = gl.canvas.toDataURL() };


                // right eye
                fcamerax = +xIPD;
                vsync = new TaskCompletionSource<object>();
                await vsync.Task;
                var f1 = new IHTMLImage { src = gl.canvas.toDataURL() };
                await f1.async.oncomplete;



                canvasTB.drawImage(f0, 0, 0, c.width, c.height, 0, 0, c.width * 2, c.height);
                canvasTB.drawImage(f1, 0, 0, c.width, c.height, 0, c.height, c.width * 2, c.height);


                // frame0 has been rendered

                var swcapture = Stopwatch.StartNew();
                status = "WriteAllBytes... " + new { filename };
                //await Native.window.async.onframe;

                // https://code.google.com/p/chromium/issues/detail?id=404301
                if (dir == null)
                {
                    frame2.src = canvasTB.canvas.toDataURL();

                    await Task.Delay(500);
                }
                else
                    await dir.WriteAllBytes(filename, canvasTB);
                //await dir.WriteAllBytes(filename, gl);
                //await dir.WriteAllBytes(filename, gl.canvas);

                status = "WriteAllBytes... done " + new { fcamerax, filename, swcapture.ElapsedMilliseconds };
                status = "rdy " + new { filename, fcamerax };
                //await Native.window.async.onframe;





                // design mode v render mode
                if (cubefacesize < cubefacesizeMAX)
                    frameIDslider.valueAsNumber += 60;
                else
                    frameIDslider.valueAsNumber++;




            beforeframe:

                // speed? S6 slow motion?
                // this is really slow. if we do x4x2 =x8 
                // https://www.youtube.com/watch?v=r76ULW16Ib8
                //fcamerax += 16 * (1.0 / 60.0);
                // fcamerax = radius * Math.Cos(Math.PI * (frameid - (60 * 30 / 2f)) / (60 * 30 / 2f));

                // speed? S6 slow motion?
                // this is really slow. if we do x4x2 =x8 
                // https://www.youtube.com/watch?v=r76ULW16Ib8
                //fcamerax += 16 * (1.0 / 60.0);

                // some shaders need to know where the camera is looking from. can we tell them?

                //fcamerax = 2.2 * Math.Sin(Math.PI * (frameIDslider.valueAsNumber - (60 * 30 / 2f)) / (60 * 30 / 2f));
                //fcameraz = 4.4 * Math.Cos(Math.PI * (frameIDslider.valueAsNumber - (60 * 30 / 2f)) / (60 * 30 / 2f));


                //// up
                //fcameray = 4.4 * Math.Cos(Math.PI * (frameIDslider.valueAsNumber - (60 * 30 / 2f)) / (60 * 30 / 2f));

                // cameraz.valueAsNumber = (int)(cameraz.max * Math.Sin(Math.PI * (frameid - (60 * 30 / 2f)) / (60 * 30 / 2f)));


                // up
                //fcameray = 128 * Math.Cos(Math.PI * (frameid - (60 * 30 / 2f)) / (60 * 30 / 2f));

                //fcamerax += (1.0 / 60.0);

                //fcamerax += (1.0 / 60.0) * 120;



                // 60hz 30sec
                if (frameIDslider.valueAsNumber < maxframes)
                {
                    // Blob GC? either this helms or the that we made a Blob static. 
                    await Task.Delay(11);

                    goto await_nextframe;
                }

                total.Stop();
                status = "all done " + new { frameid = frameIDslider.valueAsNumber, total.ElapsedMilliseconds };
                vsync = default(TaskCompletionSource<object>);
                // http://stackoverflow.com/questions/22899333/delete-javascript-blobs

                e.Element.disabled = false;
            };
            #endregion


            // "Z:\jsc.svn\examples\javascript\WebGL\WebGLColladaExperiment\WebGLColladaExperiment\WebGLColladaExperiment.csproj"






            // asus will hang
            // https://3dwarehouse.sketchup.com/model.html?id=fb7a0448d940e575edc01389f336fb0a
            // can we get one frame into vr?

            // cube: mesh to cast shadows



            //{
            //    var planeGeometry0 = new THREE.PlaneGeometry(cubefacesize, cubefacesize, 8, 8);
            //    var floor2 = new THREE.Mesh(planeGeometry0,
            //        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
            //        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xff0000, specular = 0xA26D41, shininess = 1 })
            //        //new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000, specular = 0xff0000 })
            //        new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000 })

            //    );
            //    floor2.position.set(0, 0, -cubefacesize / 2);
            //    floor2.AttachTo(scene);
            //}
            //{
            //    var planeGeometry0 = new THREE.PlaneGeometry(cubefacesize, cubefacesize, 8, 8);
            //    var floor2 = new THREE.Mesh(planeGeometry0,
            //        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
            //        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xff0000, specular = 0xA26D41, shininess = 1 })
            //        //new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000, specular = 0xff0000 })
            //        new THREE.MeshPhongMaterial(new { ambient = 0x0000ff, color = 0x0000ff })

            //    );
            //    floor2.position.set(-cubefacesize / 2, 0, 0);
            //    floor2.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);

            //    floor2.AttachTo(scene);
            //}

            //var tex0 = new THREE.Texture { image = new moon(), needsUpdate = true };
            //var tex0 = new THREE.Texture(new moon());
            //var tex0 = new THREE.Texture(new moon()) { needsUpdate = true };
            var tex0 = new THREE.Texture(

                //shader1canvas

                canvasPXitem.canvas

                ) { needsUpdate = true };


            var planeGeometry0 = new THREE.PlaneGeometry(cubefacesize, cubefacesize, 8, 8);
            var floor2 = new THREE.Mesh(planeGeometry0,
                //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xff0000, specular = 0xA26D41, shininess = 1 })
                //new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000, specular = 0xff0000 })
                new THREE.MeshPhongMaterial(
                    new
                    {

                        map = tex0,

                        transparent = true,
                        alphaTest = 0.5

                        //ambient = 0x00ff00,
                        //color = 0x00ff00
                    })

            );
            //floor2.position.set(0, 0, -cubefacesize  * 0.55);

            floor2.AttachTo(scene);

            applycameraoffset += delegate
            {
                tex0.needsUpdate = true;

                floor2.position.set(-cubefacesize * 0.5, 0, 0);
                floor2.rotation.set(0, 0, 0);
                floor2.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2 + radians(itemRotation.valueAsNumber));
            };






            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeEarth\ChromeEarth\Application.cs
            // X:\jsc.svn\examples\javascript\canvas\ConvertBlackToAlpha\ConvertBlackToAlpha\Application.cs
            // hidden for alpha AppWindows
            //#if FBACKGROUND

            new IHTMLBreak { }.AttachToDocument();


            #region stero
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


                        // we now have a stereo sprite.
                        // can we rotate it on top of the background?


                        // 8K fulldome is a resolution of 8192×8192 
                        // 8K UHD is a resolution of 7680 × 4320 (33.2 megapixels) 
                        // 8192×4320 t
                        // Digital video formats with resolutions of 4K (3840×2160) and 8K (7680×4320)

                        // keep it 4K, as hw, yt is not ready for 60fps 8K!
                        var canvasTB = new CanvasRenderingContext2D(c.width, c.height);
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151203/x360stereomidnightsun

                        canvasTB.drawImage(f0, 0, 0, c.width, c.height, 0, 0, c.width, c.height / 2);
                        canvasTB.drawImage(f1, 0, 0, c.width, c.height, 0, c.height / 2, c.width, c.height / 2);

                        //var canvasTB8K = new CanvasRenderingContext2D(c.width * 2, c.height * 2);
                        //canvasTB8K.drawImage(f0, 0, 0, c.width, c.height, 0, 0, c.width * 2, c.height);
                        //canvasTB8K.drawImage(f1, 0, 0, c.width, c.height, 0, c.height, c.width * 2, c.height);

                        // https://www.reddit.com/r/GearVR/comments/2vrfyu/id_suggest_makers_of_360_videos_make_them_the/
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151114/stereo
                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151203
                        // can we actually watch stereo _TB images on gearVR?


                        // gearVR will get a black screen
                        // 
                        //frame2.src = canvasTB8K.canvas.toDataURL();
                        frame2.src = canvasTB.canvas.toDataURL();


                        onclick = e.async.onclick;

                        //while (!onclick.IsCompleted)
                        //{
                        //    await Task.Delay(1000 / 30);
                        //    frame0.src = f0.src;
                        //    await Task.Delay(1000 / 30);
                        //    frame0.src = f1.src;
                        //}
                    }
                }
            );
            #endregion



            new { }.With(
                async delegate
                {

                    var tex1 = new the_midnight_sun_by_isilmetriel { };

                    await tex1.async.oncomplete;

                    var tex1w = 124;
                    var tex1h = 626;

                    //canvasPXitem.drawImage(
                    //           tex1, 2, 2, tex1w, tex1h, 0, 0, tex1w, tex1h
                    //       );


                    // how long until jsc can upstream small updates to code?
                    canvasPXitem.drawImage(
                               tex1, 2, 2, tex1w, tex1h,

                               // dest
                               (cubefacesize - tex1w) / 2,
                               (cubefacesize - tex1h) / 2,

                               tex1w, tex1h
                           );



                    //// canvasPXitem.drawImage(
                    ////     (IHTMLCanvas)renderer0.domElement,



                    ////    sx: (cubefacesize - cubefacesize / 6) / 2,
                    ////    sy: (cubefacesize - cubefacesize / 3) / 2,

                    ////    sw: cubefacesize / 6,
                    ////    sh: cubefacesize / 3,

                    ////    dx: (cubefacesize - cubefacesize / 6) / 2,
                    ////    dy: (cubefacesize - cubefacesize / 3) / 2,

                    ////    dw: cubefacesize / 6,
                    ////    dh: cubefacesize / 3
                    ////);




                    var iskybox2 = new HTML.Images.FromAssets._2massAllskyGAMMA();

                    await iskybox2.async.oncomplete;




                    //var iskybox1 = new HTML.Images.FromAssets.tiles_regrid();
                    var iskybox1 = new HTML.Images.FromAssets.anvil___spherical_hdri_panorama_skybox_by_macsix_d6vv4hs();
                    //var i = new HTML.Images.FromAssets.galaxy_starfield();
                    //var i = new HTML.Images.FromAssets.galaxy_starfield150FOV();

                    var bytes1 = await iskybox1.async.bytes;

                    //for (int ii = 0; ii < bytes.Length; ii += 4)
                    //{

                    //    bytes[ii + 3] = (byte)(bytes[ii + 0]);

                    //    bytes[ii + 0] = 0xff;
                    //    bytes[ii + 1] = 0xff;
                    //    bytes[ii + 2] = 0xff;
                    //}

                    var cc = new CanvasRenderingContext2D(iskybox1.width, iskybox1.height);

                    cc.bytes = bytes1;

                    //s.image = cc;
                    //s.needsUpdate = true;

                    var skybox1_material = new THREE.MeshBasicMaterial(
                            new
                            {
                                //map = THREE.ImageUtils.loadTexture(new galaxy_starfield().src),
                                map = new THREE.Texture { image = cc, needsUpdate = true },
                                side = THREE.BackSide,
                                transparent = true
                            });


                    var skybox1 = new THREE.Mesh(
                        //new THREE.SphereGeometry(far * 0.92, 64, 64),
                        //new THREE.SphereGeometry(far * 0.80, 64, 64),

                            // still zfighting
                        //new THREE.SphereGeometry(far * 0.50, 64, 64),

                            // the other option is to have a single bg and blend on tht. this is just a rotation visualization.
                            new THREE.SphereGeometry(far * 0.30, 64, 64),
                           skybox1_material
                        );

                    // http://stackoverflow.com/questions/8502150/three-js-how-can-i-dynamically-change-objects-opacity
                    //(stars_material as dynamic).opacity = 0.5;


                    var hideskybox1 = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, title = "hide skybox1", @checked = true }.AttachToDocument();
                    hideskybox1.onchange += delegate
                    {
                        skybox1.visible = !hideskybox1.@checked;
                    };
                    skybox1.visible = !hideskybox1.@checked;


                    scene.add(skybox1);




                    applycameraoffset += delegate
                    {
                        if (frameIDanimation.@checked)
                        {
                            itemRotation.valueAsNumber = (frameIDslider.valueAsNumber / 2) % 360 - 180;

                            hideskybox1.@checked = (frameIDslider.valueAsNumber / 2 + 180) % 720 < 360;
                        }
                    };


                    // target bg
                    var skybox2 = new THREE.Mesh(
                            new THREE.SphereGeometry(far * 0.95, 64, 64),
                           new THREE.MeshBasicMaterial(
                            new
                            {
                                map = new THREE.Texture { image = iskybox2, needsUpdate = true },
                                side = THREE.BackSide,
                                transparent = true
                            })
                    )

                    .AttachTo(scene).With(
                        stars =>
                        {
                            applycameraoffset += delegate
                              {

                                  skybox1.rotation.set(0, 0, 0);
                                  // spin
                                  skybox1.rotateOnAxis(new THREE.Vector3(0, -1, 0),
                                     frameIDslider.valueAsNumber / (60 * 60 / 5.0) * Math.PI * 2
                                 );

                                  // reset
                                  stars.rotation.set(0, 0, 0);

                                  // slow rotate in place
                                  stars.rotateOnAxis(new THREE.Vector3(1, 0, 0),
                                      frameIDslider.valueAsNumber / 3600.0 * Math.PI * 2
                                  );

                                  // follow the moon?
                                  stars.rotateOnAxis(new THREE.Vector3(0, -1, 0),
                                      frameIDslider.valueAsNumber / (60 * 60 / 5.0) * Math.PI * 2
                                  );
                              };
                        }

                    );

                    // can we get our stereo sprite this way?
                    // do we get clean tiles with transparency?
                    // on x83 on frame0 we were able to hide skybox. how?
                    var hideskybox2 = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, title = "hide skybox2", @checked = false }.AttachToDocument();
                    hideskybox2.onchange += delegate
                    {
                        skybox2.visible = !hideskybox2.@checked;
                    };
                    skybox2.visible = !hideskybox2.@checked;



                    Console.WriteLine("skybox added");






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
                            //globesphere.position.y = Math.Sin(fcamerax * 0.001) * 90.0;
                            //clouds.position.y = Math.Cos(fcamerax * 0.001) * 90.0;

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


                            // hide everything else


                            // inversion effect?
                            //// if (hideskybox1.@checked)
                            ////     skybox1.visible = true;
                            //// else
                            ////     skybox1.visible = false;

                            //// floor2.visible = false;
                            //// renderer0.render(scene, cameraPX);
                            //// //canvasPXitem.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);


                            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151203
                            // can we draw from that special image?


                            //canvasPXitem.drawImage(
                            //    tex1, 2, 2, 124, 630 - 4, 0, 0, 124, 626
                            //);

                            //// canvasPXitem.drawImage(
                            ////     (IHTMLCanvas)renderer0.domElement,



                            ////    sx: (cubefacesize - cubefacesize / 6) / 2,
                            ////    sy: (cubefacesize - cubefacesize / 3) / 2,

                            ////    sw: cubefacesize / 6,
                            ////    sh: cubefacesize / 3,

                            ////    dx: (cubefacesize - cubefacesize / 6) / 2,
                            ////    dy: (cubefacesize - cubefacesize / 3) / 2,

                            ////    dw: cubefacesize / 6,
                            ////    dh: cubefacesize / 3
                            ////);


                            //// skybox1.visible = !hideskybox1.@checked;
                            //// floor2.visible = true;


                            #region x
                            canvasPX.clearRect(0, 0, cubefacesize, cubefacesize);
                            // upside down?
                            renderer0.render(scene, cameraPX);
                            canvasPX.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);

                            canvasNX.clearRect(0, 0, cubefacesize, cubefacesize);
                            renderer0.render(scene, cameraNX);
                            canvasNX.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);
                            #endregion

                            #region z

                            canvasPZ.clearRect(0, 0, cubefacesize, cubefacesize);
                            renderer0.render(scene, cameraPZ);
                            canvasPZ.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);

                            canvasNZ.clearRect(0, 0, cubefacesize, cubefacesize);
                            renderer0.render(scene, cameraNZ);
                            canvasNZ.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);
                            #endregion



                            #region y
                            canvasPY.clearRect(0, 0, cubefacesize, cubefacesize);
                            renderer0.render(scene, cameraPY);

                            //canvasPY.save();
                            //canvasPY.translate(0, size);
                            //canvasPY.rotate((float)(-Math.PI / 2));
                            canvasPY.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);
                            //canvasPY.restore();


                            canvasNY.clearRect(0, 0, cubefacesize, cubefacesize);
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


                            //if (cameraz.valueAsNumber == 0)
                            gl.clearColor(0, 0, 0, 0);
                            //else
                            //gl4K.clearColor(0, 0, 0, 1);

                            gl.clear(gl.COLOR_BUFFER_BIT);


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
           );




        }


    }
}


//visualScene = parseScene();
//scene = new THREE.Group();

//		for ( var i = 0; i<visualScene.nodes.length; i ++ ) {

//			scene.add( createSceneGraph(visualScene.nodes[i] ) );

//		}

//    <scene>
//    <instance_visual_scene url = "#ID1" />
//</ scene >


//{ Message = Could not load file or assembly 'Chrome Web Store, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified. }
//1c48:02:01:14 after worker yield...

//Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.