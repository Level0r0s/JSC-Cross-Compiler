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
using x360x83;
using x360x83.Design;
using x360x83.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace x360x83
{
    using x360x83.HTML.Images.FromAssets;
    using gl = WebGLRenderingContext;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151006/360

        // R:\p900\7\DCIM\100NIKON\DSCN0018
        // https://www.google.ee/maps/@59.3803632,24.6605239,3a,15y,295.32h,98.91t/data=!3m7!1e1!3m5!1spwLJuYM6Ie410vJtVdTvEw!2e0!6s%2F%2Fgeo0.ggpht.com%2Fcbk%3Fpanoid%3DpwLJuYM6Ie410vJtVdTvEw%26output%3Dthumbnail%26cb_client%3Dmaps_sv.tactile.gps%26thumb%3D2%26w%3D203%26h%3D100%26yaw%3D30.668749%26pitch%3D0!7i13312!8i6656?hl=en


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

        // "X:\vr\zoomout.jpg"
        //  "r:\util\android-sdk-windows\platform-tools\adb.exe" push    "X:\vr\zoomout.jpg" "/sdcard/oculus/360photos/"
        //  "r:\util\android-sdk-windows\platform-tools\adb.exe" push    "X:\vr\zoominx.jpg" "/sdcard/oculus/360photos/"

        //  "r:\util\android-sdk-windows\platform-tools\adb.exe" push    "X:\vr\zoom8192.png" "/sdcard/oculus/360photos/"
        // perhaps the sky needs to be dimmed or radial blurred if zoomed in?
        // but it needs to be front center for sure.

        // "X:\vr\c8k.jpg"
        //  "r:\util\android-sdk-windows\platform-tools\adb.exe" push    "X:\vr\c8k.jpg" "/sdcard/oculus/360photos/"
        //  "r:\util\android-sdk-windows\platform-tools\adb.exe" push   "X:\vr\z83x1.jpg" "/sdcard/oculus/360photos/"

        //  "r:\util\android-sdk-windows\platform-tools\adb.exe" push   "r:\vr\x83.jpg" "/sdcard/oculus/360photos/"
        // 

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
        // subst b: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360x83\x360x83\bin\Debug\staging\x360x83.Application\web
        // subst a: z:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360x83\x360x83\bin\Debug\staging\x360x83.Application\web
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\x360x83\x360x83\bin\Debug\staging\x360x83.Application\web
        // what if we want to do subst in another winstat or session?

        // ColladaLoader: Empty or non-existing file (assets/x360x83/S6Edge.dae)









        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150917/x360x83

        //var vsync0ambient = default(TaskCompletionSource<object>);
        //var vsync1renderman = default(TaskCompletionSource<object>);

        static TaskCompletionSource<object> vsync0ambient;
        static TaskCompletionSource<object> vsync1renderman;


        static void nop() { }


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

                        new chrome.Notification(title: "x360x83");

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


            // GpuProcessHostUIShim: The GPU process crashed!

            var poke = new WebGLRenderingContext();

            if (poke == null)
            {
                new IHTMLPre { "GpuProcessHostUIShim: The GPU process crashed! restart process."
                }.AttachToDocument();
                return;
            }

            // are we in a RemoteApp ? software renderer?
            // this may hang the buggy rdp protocol...




            // crash
            //int cubefacesizeMAX = 2048 * 2; // 6 faces, ?
            //int cubefacesizeMAX = 2048 * 2; // 6 faces, ?
            int cubefacesizeMAX = 2048 * 1; // 6 faces, ?
            int cubefacesize = cubefacesizeMAX; // 6 faces, ?
            //int cubefacesize = 1024; // 6 faces, ?
            // "X:\vr\tape1\0000x2048.png"
            // for 60hz render we may want to use float camera percision, not available for ui.
            //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x2048.png" "/sdcard/oculus/360photos/"
            //  "x:\util\android-sdk-windows\platform-tools\adb.exe" push "X:\vr\tape1\0000x128.png" "/sdcard/oculus/360photos/"

            //if (Environment.ProcessorCount < 8)
            //    //cubefacesize = 64; // 6 faces, ?

            //    // fast gif?
            //cubefacesize = 1024; // 6 faces, ?

            // not 8k..
            //cubefacesize = 512; // 6 faces, ?

            // big cubeface may be draw only half of itself?


            // can we keep fast fps yet highp?

            // can we choose this on runtime? designtime wants fast fps, yet for end product we want highdef on our render farm?
            //const int cubefacesize = 128; // 6 faces, ?

            //var cubecameraoffsetx = 256;
            var cubecameraoffsetx = 400;


            //var uizoom = 0.1;
            //var uizoom = cubefacesize / 128f;
            var uizoom = 128f / cubefacesize;


            Native.css.style.backgroundColor = "blue";
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
            //var radius = 480;
            //var radius = -480;

            //var segments = 32;
            //var segments = 128 * 2;
            //var rotation = 6;


            //const int size = 128;
            //const int size = 256; // 6 faces, 12KB
            //const int size = 512; // 6 faces, ?

            // WebGL: drawArrays: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled.

            //const int size = 720; // 6 faces, ?
            //const int size = 1024; // 6 faces, ?
            //const int cubefacesize = 1024; // 6 faces, ?

            // THREE.WebGLRenderer: Texture is not power of two. Texture.minFilter is set to THREE.LinearFilter or THREE.NearestFilter. ( chrome-extension://aemlnmcokphbneegoefdckonejmknohh/assets/x360x83/anvil___spherical_hdri_panorama_skybox_by_macsix_d6vv4hs.jpg )


            // var far = 0xffffff;

            // need a zoom effect
            // 5pixels to 33%


            // radius needs to be a bit bigger so wa cant zoom thru it
            // far image at this distance 
            var skyboxradius0 = 0 + 2048 * 4;

            var near = cubefacesize * 0.33;


            var skyboxradius = skyboxradius0 * 1.2;

            var far = skyboxradius * 2;
            //var near = cubefacesize * 0.5;
            //var near = cubefacesize * 0.4;
            //var near = cubefacesize * 0.25;

            new IHTMLPre { new { Environment.ProcessorCount, cubefacesize } }.AttachToDocument();

            //new IHTMLPre { "can we stream it into VR, shadertoy, youtube 360, youtube stereo yet?" }.AttachToDocument();


            var sw = Stopwatch.StartNew();



            var oo = new List<THREE.Object3D>();



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

            // whatif we reuse the skybox as is and skip rendering it?
            // that means we cannot rotate via sky, we have to rotate other elements in reverse.
            // can we have a checkbox to hide or render the skybox?
            var scenezooms = new THREE.Group();
            scenezooms.AttachTo(scene);




            // fly up?
            //sceneg.translateZ(-1024);
            // rotate the world, as the skybox then matches what we have on filesystem
            scene.rotateOnAxis(new THREE.Vector3(0, 1, 0), -Math.PI / 2);
            //scene.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);
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

            // cyan?
            //renderer0.setClearColor(0xfffff, 1);
            //renderer0.setClearColor(0xfffff, 0);
            renderer0.setClearColor(0x0, 0);
            //renderer0.setClearColor(0x0, 1);

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
            var frameIDslider = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0, max = 1800, valueAsNumber = 1800 / 2, title = "frameIDslider" }.AttachToDocument();












            var xframeID = 0;



            new { }.With(

                async delegate
                {

                next:

                    //Console.WriteLine("enter vsync0ambient");
                    Native.document.title = new { xframeID }.ToString();


                    vsync0ambient = new TaskCompletionSource<object>();

                    await vsync0ambient.Task;

                    await Task.Delay(1000 / 15);
                    //await Task.Delay(1000);
                    //Console.WriteLine("await vsync0ambient 5");
                    //await Task.Delay(5000);

                    xframeID++;

                    goto next;
                }

            );














            new IHTMLHorizontalRule { }.AttachToDocument();

            var camerazMIN = 0 - 2048 * 4;

            var camerax = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0 - 2048 * 4, max = 0 + 2048 * 4, valueAsNumber = 0, title = "camerax" }.AttachToDocument();
            // up. whats the most high a rocket can go 120km?
            new IHTMLHorizontalRule { }.AttachToDocument();


            // how high is the bunker?
            var cameray = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0 - 2048 * 4, max = 2048 * 4, valueAsNumber = 0, title = "cameray" }.AttachToDocument();
            new IHTMLBreak { }.AttachToDocument();
            var camerayHigh = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = cameray.max, max = 1024 * 256, valueAsNumber = cameray.max, title = "cameray" }.AttachToDocument();
            new IHTMLHorizontalRule { }.AttachToDocument();
            //var cameraz = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0 - 2048 * 4, max = 0 + 2048 * 4, valueAsNumber = 0, title = "cameraz" }.AttachToDocument();
            //var cameraz = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 0 - 2048 * 4, max = 0 + 2048 * 4, valueAsNumber = 0 - 2048 * 4, title = "cameraz" }.AttachToDocument();
            var cameraz = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = camerazMIN, max = 0, valueAsNumber = camerazMIN, title = "cameraz" }.AttachToDocument();
            // the zoom hting..


            new IHTMLHorizontalRule { }.AttachToDocument();

            var skyrotup = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -450, max = 450, valueAsNumber = 0, title = "skyrotup" }.AttachToDocument();
            var skyrotright = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -450, max = 450, valueAsNumber = 0, title = "skyrotright" }.AttachToDocument();

            new IHTMLPre { () => new { skyrotup = skyrotup.valueAsNumber, skyrotright = skyrotright.valueAsNumber } }.AttachToDocument();


            new IHTMLHorizontalRule { }.AttachToDocument();

            // were we able to test for it?
            //var zoomrotup = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -450, max = 450, valueAsNumber = -12, title = "up" }.AttachToDocument();
            var zoomrotup = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -450, max = 450, valueAsNumber = 0, title = "up" }.AttachToDocument();
            var zoomrotright = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = -450, max = 450, valueAsNumber = 0, title = "right" }.AttachToDocument();

            new IHTMLPre { () => new { 
                

                // on 0 zoom we should rely on the original skybox?
                cameraz = cameraz.valueAsNumber, 
                
                zoomrotup = zoomrotup.valueAsNumber, zoomrotright = zoomrotright.valueAsNumber } }.AttachToDocument();



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
                  1.0 * (camerax + fcamerax),
                    // height?
                    //0,
                    //1600,
                    //1024,

                   // if the camera is in the center, would we need to move the scene?
                    // we have to move the camera. as we move the scene the lights are messed up
                    //2014,
                   1.0 * (cy + fcameray),

                 //1200
                 1.0 * (cameraz + fcameraz)
                    // can we hover top of the map?
                   );
            };


            #region y
            // need to rotate90?
            var cameraNY = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: near, far: far);
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
            //canvasNY.canvas.style.transform = $"scale({uizoom})";
            canvasNY.canvas.style.transform = "scale(" + uizoom + ")";

            var cameraPY = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: near, far: far);
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
            //canvasPY.canvas.style.transform = $"scale({uizoom})";
            canvasPY.canvas.style.transform = "scale(" + uizoom + ")";
            #endregion

            // transpose xz?

            #region x
            var cameraNX = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: near, far: far);
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
            //canvasNX.canvas.style.transform = $"scale({uizoom})";
            canvasNX.canvas.style.transform = "scale(" + uizoom + ")";


            // front??
            var cameraPX = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: near, far: far);
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
            //canvasPX.canvas.style.transform = $"scale({uizoom})";
            canvasPX.canvas.style.transform = "scale(" + uizoom + ")";
            #endregion



            #region z
            var cameraNZ = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: near, far: far);
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
            //canvasNZ.canvas.style.transform = $"scale({uizoom})";
            canvasNZ.canvas.style.transform = "scale(" + uizoom + ")";

            var cameraPZ = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: near, far: far);
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
            //canvasPZ.canvas.style.transform = $"scale({uizoom})";
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



            #region gl4K spherical
            var gl4K = new WebGLRenderingContext(alpha: true, preserveDrawingBuffer: true);
            var c4k = gl4K.canvas.AttachToDocument();


            //  3840x2160

            //c.style.SetSize(3840, 2160);

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150722/360-youtube


            // when can we go up?
            c4k.width = 3840;
            c4k.height = 2160;

            // https://www.youtube.com/watch?v=sLprVF6d7Ug
            // 8K is 7680 4320

            // https://www.youtube.com/watch?v=RNdHaeBhT9Q
            // 8K is 7680 3840

            //c.width = 7680;
            ////c.height = 3840;
            //c.height = 4320;



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

            new IHTMLPre { new { c4k.width, c4k.height } }.AttachToDocument();

            //6466x3232

            //var suizoom = 720f / c.height;
            //var suizoom = 360f / c.height;
            var suizoom = 480f / c4k.width;

            c4k.style.backgroundColor = "yellow";
            c4k.style.transformOrigin = "0 0";
            //c.style.transform = $"scale({suizoom})";
            c4k.style.transform = "scale(" + suizoom + ")";
            //c.style.backgroundColor = "yellow";
            c4k.style.position = IStyle.PositionEnum.absolute;

            c4k.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0,
                8 + (int)(uizoom * cubefacesize + 8) * 3 + 120
                );


            // until we figure out how to fix the shader, we can try to fake it?
            // will allow atleast a nice static 8K image?
            // S6 did a 6546x3272 image. 5k?

            // 1.77
            var c8k = new CanvasRenderingContext2D(3840 * 2, 2160 * 2);

            //var c8k = new CanvasRenderingContext2D(5120, 2880);

            // 5120 x 2880 pixel

            // 8k canvas wont load in chrome?
            c8k.canvas.AttachToDocument();


            c8k.canvas.style.backgroundColor = "cyan";
            c8k.canvas.style.transformOrigin = "0% 0%";
            c8k.canvas.style.transform = "scale(" + (suizoom / 2) + ")";

            c8k.canvas.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0 + 120,
                8 + (int)(uizoom * cubefacesize + 8) * 3 + 120 + 320
                );



            var pass = new CubeToEquirectangular.Library.ShaderToy.EffectPass(
                       null,
                       gl4K,
                       precission: CubeToEquirectangular.Library.ShaderToy.DetermineShaderPrecission(gl4K),
                       supportDerivatives: gl4K.getExtension("OES_standard_derivatives") != null,
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



            // why is it flipped?

            //var frame0 = new HTML.Images.FromAssets.tiles_regrid().AttachToDocument();
            //var frame0 = new HTML.Images.FromAssets.galaxy_starfield().AttachToDocument();
            var frame0 = new HTML.Images.FromAssets._20150912_154522().AttachToDocument();
            //var frame0 = new HTML.Images.FromAssets._20150912_154522recenter().AttachToDocument();


            //var frame0 = new HTML.Images.FromAssets.galaxy_starfield150FOV().AttachToDocument();
            //var xor = new HTML.Images.FromAssets.Orion360_test_image_8192x4096().AttachToDocument();
            //var xor = new HTML.Images.FromAssets._2_no_clouds_4k().AttachToDocument();
            //var frame0 = new HTML.Images.FromAssets._2294472375_24a3b8ef46_o().AttachToDocument();


            // 270px
            //xor.style.height = "";
            frame0.style.height = "270px";
            frame0.style.width = "480px";
            frame0.style.SetLocation(
                8 + (int)(uizoom * cubefacesize + 8) * 0 + 480 + 16,
                8 + (int)(uizoom * cubefacesize + 8) * 3 + 120);




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
                    var c8ksw = Stopwatch.StartNew();

                    c8k.drawImage(frame0, 0, 0, c8k.canvas.width, c8k.canvas.height);
                    c8k.drawImage(gl4K, 0, 0, c8k.canvas.width, c8k.canvas.height);


                    // not exporting to file system?
                    //var f0 = new IHTMLImage { src = gl4K.canvas.toDataURL() };
                    //var f0 = new IHTMLImage { src = c8k.canvas.toDataURL() };

                    // png would be 50mb?
                    var f0 = new IHTMLImage { src = c8k.canvas.toDataURL(quality: 0.9) };
                    // 22989ms { c8ksw = 00:00:12.12976 }
                    Console.WriteLine(new { c8ksw });

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
                dir.WriteAllBytes("0000.png", gl4K);

                new IHTMLPre { new { glsw.ElapsedMilliseconds } }.AttachToDocument();

                // {{ ElapsedMilliseconds = 1548 }}

                // 3.7MB
                // 3840x2160

            };

            #endregion


            #region render 60hz 30sec
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



                vsync1renderman = new TaskCompletionSource<object>();
                await vsync1renderman.Task;

                status = "rendering... vsync";

                //var frameid = 0;
                frameIDslider.valueAsNumber = -1;

                goto beforeframe;


                // parallax offset?

                await_nextframe:


                //var filename = frameIDslider.valueAsNumber.ToString().PadLeft(4, '0') + ".png";
                var filename = frameIDslider.valueAsNumber.ToString().PadLeft(5, '0') + ".jpg";
                status = "rendering... " + new { filename };


                vsync1renderman = new TaskCompletionSource<object>();
                await vsync1renderman.Task;

                // frame0 has been rendered

                var swcapture = Stopwatch.StartNew();
                status = "WriteAllBytes... " + new { filename };
                //await Native.window.async.onframe;

                // https://code.google.com/p/chromium/issues/detail?id=404301
                if (dir != null)
                {
                    c8k.drawImage(frame0, 0, 0, c8k.canvas.width, c8k.canvas.height);
                    c8k.drawImage(gl4K, 0, 0, c8k.canvas.width, c8k.canvas.height);


                    //await dir.WriteAllBytes(filename, gl4K);
                    await dir.WriteAllBytes(filename, c8k);
                }

                //await dir.WriteAllBytes(filename, gl.canvas);

                status = "WriteAllBytes... done " + new { fcamerax, filename, swcapture.ElapsedMilliseconds };
                status = "rdy " + new { filename, fcamerax };
                //await Native.window.async.onframe;





                // design mode v render mode
                if (cubefacesize < cubefacesizeMAX)
                    frameIDslider.valueAsNumber += 15;
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


                // in 30 sec can we have a zoom in and out?

                // so 15 sec at 60 fps needs to be -max z


                var a = Math.Abs(frameIDslider.valueAsNumber - (60 * 15));
                var aa = a / (60f * 15);

                //cameraz.valueAsNumber = (int)(camerazMIN * aa);
                cameraz.valueAsNumber = (int)(camerazMIN * (1.0 - aa));


                // 60hz 30sec
                if (frameIDslider.valueAsNumber < 60 * 30)
                {
                    // Blob GC? either this helms or the that we made a Blob static. 
                    //await Task.Delay(11);
                    await Task.Delay(33);

                    goto await_nextframe;
                }

                total.Stop();
                status = "all done " + new { frameid = frameIDslider.valueAsNumber, total.ElapsedMilliseconds };
                vsync1renderman = default(TaskCompletionSource<object>);
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

            //var p900toCubeSize = cubefacesize / 1080f;
            var p900toCubeSize = cubefacesize / 1920f;

            //p900toCubeSize *= 0.7f;

            // where is this magic number coming from??
            p900toCubeSize *= 0.65f;
            //p900toCubeSize *= 0.5f;

            // http://stackoverflow.com/questions/17648067/three-js-drawing-two-overlapping-transparent-spheres-and-hiding-intersection



            var farimage = new output00609();
            var nearimage = new output01085();

            // front?
            {
                //var tex0 = new THREE.Texture { image = new moon(), needsUpdate = true };
                //var tex0 = new THREE.Texture(new moon());
                //var tex0 = new THREE.Texture(new moon()) { needsUpdate = true };
                //var tex0 = new THREE.Texture(shader1canvas) { needsUpdate = true };


                //var tex0 = new THREE.Texture(new output01027()) { needsUpdate = true };
                //var tex0 = new THREE.Texture(new output00630()) { needsUpdate = true };
                var tex0 = new THREE.Texture(farimage) { needsUpdate = true, minFilter = THREE.LinearFilter };
                applycameraoffset += delegate { tex0.needsUpdate = true; };

                //var planeGeometry0 = new THREE.PlaneGeometry(1920, 1080, 8, 8);
                var planeGeometry0 = new THREE.PlaneGeometry((int)(1920 * p900toCubeSize), (int)(1080 * p900toCubeSize), 8, 8);
                var floor2 = new THREE.Mesh(planeGeometry0,
                    //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                    //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xff0000, specular = 0xA26D41, shininess = 1 })
                    //new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000, specular = 0xff0000 })
                    new THREE.MeshPhongMaterial(
                        new
                        {
                            // black otherwise?
                            transparent = true,

                            map = tex0,


                            //ambient = 0x00ff00,
                            //color = 0x00ff00
                        })

                );

                //(floor2 as dynamic).renderDepth = 0.2;

                //floor2.position.set(0, 0, -cubefacesize  * 0.55);

                // zoom in and get 90FOV clouseup?
                floor2.position.set(-cubefacesize * 0.50 - skyboxradius0, 0, 0);
                //floor2.position.set(-skyboxradius0 - 128, 0, 0);
                floor2.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);
                //floor2.AttachTo(scene);
                floor2.AttachTo(scenezooms);
            }





            {
                //var tex0 = new THREE.Texture { image = new moon(), needsUpdate = true };
                //var tex0 = new THREE.Texture(new moon());
                //var tex0 = new THREE.Texture(new moon()) { needsUpdate = true };
                //var tex0 = new THREE.Texture(shader1canvas) { needsUpdate = true };


                //var tex0 = new THREE.Texture(new output01027()) { needsUpdate = true };
                var tex0 = new THREE.Texture(nearimage) { needsUpdate = true, minFilter = THREE.LinearFilter };
                //var tex0 = new THREE.Texture(new output00630()) { needsUpdate = true };

                applycameraoffset += delegate { tex0.needsUpdate = true; };

                //var planeGeometry0 = new THREE.PlaneGeometry(1920, 1080, 8, 8);
                //var planeGeometry0 = new THREE.PlaneGeometry((int)(1920 * 0.1), (int)(1080 * 0.1), 8, 8);



                var planeGeometry0 = new THREE.PlaneGeometry((int)(1920 * p900toCubeSize), (int)(1080 * p900toCubeSize), 8, 8);
                var floor2 = new THREE.Mesh(planeGeometry0,
                    //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                    //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xff0000, specular = 0xA26D41, shininess = 1 })
                    //new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000, specular = 0xff0000 })
                    new THREE.MeshPhongMaterial(
                        new
                        {
                            // black otherwise?
                            transparent = true,

                            map = tex0,


                            //ambient = 0x00ff00,
                            //color = 0x00ff00
                        })

                );
                //floor2.position.set(0, 0, -cubefacesize  * 0.55);
                //(floor2 as dynamic).renderDepth = 0.3;

                // zoom in and get 90FOV clouseup?
                floor2.position.set(-cubefacesize * 0.50, 0, 0);
                floor2.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);
                //floor2.AttachTo(scene);
                floor2.AttachTo(scenezooms);
            }


            Action<IHTMLImage, double> AddFrame = (img, z) =>
            {
                {
                    //var tex0 = new THREE.Texture { image = new moon(), needsUpdate = true };
                    //var tex0 = new THREE.Texture(new moon());
                    //var tex0 = new THREE.Texture(new moon()) { needsUpdate = true };
                    //var tex0 = new THREE.Texture(shader1canvas) { needsUpdate = true };


                    //var tex0 = new THREE.Texture(new output01027()) { needsUpdate = true };
                    //var tex0 = new THREE.Texture(new output00630()) { needsUpdate = true };
                    var tex0 = new THREE.Texture(img) { needsUpdate = true, minFilter = THREE.LinearFilter };
                    applycameraoffset += delegate { tex0.needsUpdate = true; };

                    //var planeGeometry0 = new THREE.PlaneGeometry(1920, 1080, 8, 8);
                    var planeGeometry0 = new THREE.PlaneGeometry((int)(1920 * p900toCubeSize), (int)(1080 * p900toCubeSize), 8, 8);
                    var floor2 = new THREE.Mesh(planeGeometry0,
                        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
                        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xff0000, specular = 0xA26D41, shininess = 1 })
                        //new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000, specular = 0xff0000 })
                        new THREE.MeshPhongMaterial(
                            new
                            {
                                // black otherwise?
                                transparent = true,

                                map = tex0,


                                //ambient = 0x00ff00,
                                //color = 0x00ff00
                            })

                    );

                    //(floor2 as dynamic).renderDepth = 0.2;

                    //floor2.position.set(0, 0, -cubefacesize  * 0.55);

                    // zoom in and get 90FOV clouseup?
                    floor2.position.set(-cubefacesize * 0.50 - skyboxradius0 * z, 0, 0);
                    //floor2.position.set(-skyboxradius0 - 128, 0, 0);
                    floor2.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);
                    //floor2.AttachTo(scene);
                    floor2.AttachTo(scenezooms);
                }

            };

            new IHTMLButton { "load frames from disk " }.AttachToDocument().onclick += async e =>
            {
                // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\360\x360x83\Application.cs
                // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\360\x360azimuthal\Application.cs

                e.Element.disabled = true;

                // how do we load the files?
                var dir2 = (DirectoryEntry)await chrome.fileSystem.chooseEntry(new { type = "openDirectory" });

                var dir2r = dir2.createReader();

                var files2 = await dir2r.readFileEntries();

                var files2count = files2.Count();

                Console.WriteLine(new { files2count });

                // does this dir have the first and last image we already know of?
                // 55390ms { files2count = 4324 }

                var firstcandidate = files2.First();

                //Console.WriteLine(new { firstcandidate, farimage.src });
                // 19230ms { firstcandidate = [object FileEntry], src = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/assets/x360x83/output00609.png }

                // 10903ms { firstcandidate = output00001.png, farimage = output00609.png }

                var files2skip = files2.SkipWhile(firstcandidate1 => firstcandidate1.name != farimage.src.SkipUntilLastOrEmpty("/"));
                //Console.WriteLine(new { files2skip = files2skip.Count() });

                var files2take = files2skip.TakeWhile(firstcandidate1 => firstcandidate1.name != nearimage.src.SkipUntilLastOrEmpty("/"));

                //6228ms { files2count = 4324 }
                //view-source:54116 6377ms { files2take = 476 }

                // reverse?
                var files3 = files2take.ToArray();
                var files3count = files3.Count();


                //Console.WriteLine(new { files2take = files2take.Count() });

                //var step = 8;
                //var step = 1;//crashes
                //var step = 2;//crashes after load
                var step = 4;//crashes after load
                //var step = 3;//
                //var step = (int)(files3count * 0.05);
                //var step = (int)(files3count * 0.25);

                for (int i = step; i < files3count; i += step)
                {
                    //files3[i].

                    Console.WriteLine(new { i });

                    e.Element.innerText = new { step, i, files3count }.ToString();


                    //files3[i].file()
                    var ff = await files3[i].file();

                    //83fc21e4-a2da-408d-9831-571313ead641
                    //Refcount: 1
                    //Content Type: image/png
                    //Type: file
                    //Path: X:\p900\7\DCIM\100NIKON\DSCN0018\output00767.png
                    //Modification Time: Sunday, September 13, 2015 at 10:24:01 AM
                    //Length: 2,131,791

                    // are we running out of blobs?

                    var url = ff.ToObjectURL();
                    var img = new IHTMLImage(url);


                    await img.async.oncomplete;

                    //async ff =>
                    //{
                    //    var ffbytes = await ff.readAsBytes();

                    //var ffimage = (IHTMLImage)ffbytes;

                    var aa = (double)i / files3count;

                    AddFrame(img, 1.0 - aa);

                    //    }
                    //);

                    //files3[i].
                }

                e.Element.innerText = "done " + new { step, files3count }.ToString();

            };


            //{
            //    //var tex0 = new THREE.Texture { image = new moon(), needsUpdate = true };
            //    //var tex0 = new THREE.Texture(new moon());
            //    //var tex0 = new THREE.Texture(new moon()) { needsUpdate = true };
            //    var tex0 = new THREE.Texture(shader1canvas) { needsUpdate = true };

            //    applycameraoffset += delegate { tex0.needsUpdate = true; };

            //    var planeGeometry0 = new THREE.PlaneGeometry(cubefacesize, cubefacesize, 8, 8);
            //    var floor2 = new THREE.Mesh(planeGeometry0,
            //        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })
            //        //new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xff0000, specular = 0xA26D41, shininess = 1 })
            //        //new THREE.MeshPhongMaterial(new { ambient = 0xff0000, color = 0xff0000, specular = 0xff0000 })
            //        new THREE.MeshPhongMaterial(
            //            new
            //            {

            //                map = tex0,


            //                //ambient = 0x00ff00,
            //                //color = 0x00ff00
            //            })

            //    );
            //    floor2.position.set(cubefacesize * 0.55, 0, 0);
            //    floor2.rotateOnAxis(new THREE.Vector3(0, 1, 0), -Math.PI / 2);

            //    floor2.AttachTo(scene);
            //}





            // X:\jsc.svn\examples\javascript\chrome\apps\ChromeEarth\ChromeEarth\Application.cs
            // X:\jsc.svn\examples\javascript\canvas\ConvertBlackToAlpha\ConvertBlackToAlpha\Application.cs
            // hidden for alpha AppWindows
            //#if FBACKGROUND


            #region galaxy_starfield
            new THREE.Texture().With(
                async s =>
                {
                    //var i = new HTML.Images.FromAssets.galaxy_starfield();
                    //var i = new HTML.Images.FromAssets.galaxy_starfield150FOV();

                    var bytes = await frame0.async.bytes;

                    //for (int ii = 0; ii < bytes.Length; ii += 4)
                    //{

                    //    bytes[ii + 3] = (byte)(bytes[ii + 0]);

                    //    bytes[ii + 0] = 0xff;
                    //    bytes[ii + 1] = 0xff;
                    //    bytes[ii + 2] = 0xff;
                    //}

                    var cc = new CanvasRenderingContext2D(frame0.width, frame0.height);

                    cc.bytes = bytes;

                    // does not do a thing?
                    //s.flipY = true;

                    s.image = cc;
                    s.needsUpdate = true;

                    var stars_material = new THREE.MeshBasicMaterial(
                            new
                            {
                                //map = THREE.ImageUtils.loadTexture(new galaxy_starfield().src),
                                map = s,
                                // both?
                                //side = THREE.BackSide,
                                transparent = true
                            });

                    // nice
                    //stars_material.opacity = 0.5;



                    // THREE.SphereGeometry = function ( radius, widthSegments, heightSegments, phiStart, phiLength, thetaStart, thetaLength ) {
                    // http://learningthreejs.com/blog/2011/10/05/performance-merging-geometry/

                    // how are we to construct geometry that has higher detail in one spot for zoom in
                    var skyboxsphere = new THREE.SphereGeometry(skyboxradius, 512, 512

                    // left to right
                        // 0 .. 45 deg

                    // center it?
                        //Math.PI - Math.PI / 4
                        //, Math.PI / 4,



                    //// up to down
                        //// 0 .. 22 deg
                        //0, Math.PI / 2
                    );


                    //
                    var stars = new THREE.Mesh(
                        // radius not used?
                        //new THREE.SphereGeometry(skyboxradius, 64, 64),
                        //new THREE.SphereGeometry(skyboxradius, 8, 8),

                            // we need to be able to zoom in!
                        //new THREE.SphereGeometry(skyboxradius, 256, 256),

                            // chrome will crash on laptop?
                        // chrome will crash on red!
                        //new THREE.SphereGeometry(skyboxradius, 1024, 1024),
                        //new THREE.SphereGeometry(skyboxradius, 1024, 1024),
                        //new THREE.SphereGeometry(skyboxradius, 512, 512),
                        //new THREE.SphereGeometry(skyboxradius, 600, 600),

                            // orr perhaps do we need detailed geometry only in specific spot?
                            skyboxsphere,

                           stars_material
                        );

                    // http://stackoverflow.com/questions/8502150/three-js-how-can-i-dynamically-change-objects-opacity
                    //(stars_material as dynamic).opacity = 0.5;

                    stars.scale.x = -1;


                    // http://stackoverflow.com/questions/31797871/three-js-alpha-on-entire-object
                    applycameraoffset += delegate
                    {
                        if (cameraz.valueAsNumber == 0)
                        {
                            // static 5k image should take over...
                            stars_material.opacity = 0.0;
                            return;
                        }


                        var a = (cameraz.valueAsNumber / (double)camerazMIN);

                        stars.rotation.set(0, 0, 0);

                        //    skyrotright
                        //stars.rotateOnAxis(new THREE.Vector3(0, 0, 1), (Math.PI / 2) * (skyrotup.valueAsNumber / 900.0));
                        stars.rotateOnAxis(new THREE.Vector3(0, 1, 0), (Math.PI / 2) * (skyrotright.valueAsNumber / 900.0));



                        stars.rotateOnAxis(new THREE.Vector3(0, 0, 1), (Math.PI / 2) * ((a * 35.0 + skyrotup.valueAsNumber) / 900.0));


                        stars_material.opacity = (1.0 - a) * 0.7 + 0.3;
                    };




                    // can we get our hrozion recentered?
                    //stars.rotateOnAxis(new THREE.Vector3(0, 0, 1), (Math.PI / 2) * (3 / 90.0));
                    //stars.rotateOnAxis(new THREE.Vector3(0, 0, 1), (Math.PI / 2) * (1.3 / 90.0));
                    //stars.rotateOnAxis(new THREE.Vector3(0, 0, 1), (Math.PI / 2) * (1.5 / 90.0));

                    applycameraoffset += delegate
                    {

                        scenezooms.rotation.set(0, 0, 0);
                        // keep skybox where it is

                        scenezooms.rotateOnAxis(new THREE.Vector3(0, 0, 1), (Math.PI / 2) * (zoomrotup.valueAsNumber / 900.0));
                        scenezooms.rotateOnAxis(new THREE.Vector3(0, 1, 0), (Math.PI / 2) * (zoomrotright.valueAsNumber / 900.0));

                    };


                    var hideskybox = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, title = "hide skybox", @checked = true }.AttachToDocument();

                    //Native.window.onframe += delegate
                    //{
                    //    //
                    //    stars.visible = !hideskybox.@checked;
                    //};

                    hideskybox.onchange += delegate
                    {
                        //
                        stars.visible = !hideskybox.@checked;
                    };
                    stars.visible = !hideskybox.@checked;



                    scene.add(stars);
                }
           );
            #endregion


            //var NYonly = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, title = "NY only" }.AttachToDocument();
            var PXonly = new IHTMLInput
            {
                type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox,
                @checked = true,
                title = "PX only"
            }.AttachToDocument();


            //new Models.ColladaS6Edge().Source.Task.ContinueWithResult(
            //       dae =>
            {



                //dae.position.y = -80;

                //dae.AttachTo(sceneg);
                //scene.add(dae);
                //oo.Add(dae);




                // view-source:http://threejs.org/examples/webgl_multiple_canvases_circle.html
                // https://threejsdoc.appspot.com/doc/three.js/src.source/extras/cameras/CubeCamera.js.html
                Native.window.onframe +=
                    e =>
                    {
                        // Z:\jsc.svn\examples\javascript\test\TestDelegateIfIfReturn\Application.cs

                        // let render man know..

                        var flag1 = vsync1renderman != null;
                        // nonroslyn!!
                        if (flag1)
                        // this if block is not detected?
                        {
                            // whats going on  with if clauses?
                            nop();

                            // wtf???
                            var flag0 = vsync1renderman.Task.IsCompleted;
                            if (flag0)
                                return;
                        }
                        if (vsync0ambient != null)
                            if (vsync0ambient.Task.IsCompleted)
                                return;

                        // 38045ms Native.window.onframe { vsync1renderman = , vsync0ambient = [object Object] }

                        //Console.WriteLine("Native.window.onframe " + new { vsync1renderman, vsync0ambient });

                        //return;

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

                        if (cameraz.valueAsNumber == 0)
                            renderer0.setClearColor(0x0, 0);
                        else
                            renderer0.setClearColor(0x0, 1);

                        new[] {
                                   canvasPX, canvasNX,
                                   canvasPY, canvasNY,
                                   canvasPZ, canvasNZ
                        }.WithEach(cc =>
                            {




                                cc.clearRect(0, 0, cubefacesize, cubefacesize);
                            }
                        );

                        //gl.clear()

                        if (PXonly.@checked)
                        {
                            var cameraPXsw = Stopwatch.StartNew();

                            renderer0.render(scene, cameraPX);

                            // 35207ms { cameraPXsw = 00:00:00.88 }
                            //75505ms { cameraPXsw = 00:00:00.61 }
                            //Console.WriteLine(new { cameraPXsw });

                            // clear if transparent?
                            canvasPX.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, cubefacesize, cubefacesize);

                            //return;

                        }
                        else
                        {

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


                            // the floor?

                            // render only this one?
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

                            #region Paint_Image
                            new[] {
                                   canvasPX, canvasNX,
                                   canvasPY, canvasNY,
                                   canvasPZ, canvasNZ
                        }.WithEachIndex(
                         (img, index) =>
                         {
                             gl4K.bindTexture(gl.TEXTURE_CUBE_MAP, pass.tex);

                             //gl.pixelStorei(gl.UNPACK_FLIP_X_WEBGL, false);
                             gl4K.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, false);

                             // http://stackoverflow.com/questions/15364517/pixelstoreigl-unpack-flip-y-webgl-true

                             // https://msdn.microsoft.com/en-us/library/dn302429(v=vs.85).aspx
                             //gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 0);
                             //gl.pixelStorei(gl.UNPACK_FLIP_Y_WEBGL, 1);

                             gl4K.texImage2D(gl.TEXTURE_CUBE_MAP_POSITIVE_X + (uint)index, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, img.canvas);

                         }
                      );


                            // http://stackoverflow.com/questions/11544608/how-to-clear-a-rectangle-area-in-webgl

                            if (cameraz.valueAsNumber == 0)
                                gl4K.clearColor(0, 0, 0, 0);
                            else
                                gl4K.clearColor(0, 0, 0, 1);

                            gl4K.clear(gl.COLOR_BUFFER_BIT);

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
                            gl4K.flush();
                            #endregion


                        }

                        // let render man know..
                        if (vsync1renderman != null)
                            if (!vsync1renderman.Task.IsCompleted)
                                vsync1renderman.SetResult(null);

                        if (vsync0ambient != null)
                            if (!vsync0ambient.Task.IsCompleted)
                                vsync0ambient.SetResult(null);
                    };


            }
            //);





            Console.WriteLine("do you see it?");
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



// wtf??

//020000e7 CubeToEquirectangular.Library.ShaderToy+<AttachToDocument>d__14+<MoveNext>06000038
//script: error JSC1000: *** stack is empty, invalid pop?
//script: error JSC1000: error at CubeToEquirectangular.Library.ShaderToy+<AttachToDocument>d__14+<MoveNext>06000038.<00a9> ldarg.0.try,
// assembly: W:\x360x83.Application.exe
// type: CubeToEquirectangular.Library.ShaderToy+<AttachToDocument>d__14+<MoveNext>06000038, x360x83.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x0154
//  method:Int32 <00a9> ldarg.0.try(<MoveNext>06000038, <AttachToDocument>d__14 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[ScriptCoreLib.JavaScript.DOM.IWindow+FrameEvent] ByRef, System.Runtime.Co
//*** Compiler cannot continue... press enter to quit.



