//#define AsWEBSERVER

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
using ChromeEquirectangularCameraExperiment;
using ChromeEquirectangularCameraExperiment.Design;
using ChromeEquirectangularCameraExperiment.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.WebGL;

namespace ChromeEquirectangularCameraExperiment
{
    using gl = WebGLRenderingContext;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809

        // the eye nor the display will be able to do any stereo
        // until tech is near matrix capability. 2019?

        // cubemap can be used for all long range scenes
        // http://www.imdb.com/title/tt0112111/?ref_=nv_sr_1


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/cubemapcamera
        // subst a: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularCameraExperiment\ChromeEquirectangularCameraExperiment\bin\Debug\staging\ChromeEquirectangularCameraExperiment.Application\web
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularCameraExperiment\ChromeEquirectangularCameraExperiment\bin\Debug\staging\ChromeEquirectangularCameraExperiment.Application\web

        // ColladaLoader: Empty or non-existing file (assets/ChromeEquirectangularCameraExperiment/S6Edge.dae)

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

                        new chrome.Notification(title: "ChromeEquirectangularCameraExperiment");

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


            Native.css.style.backgroundColor = "blue";
            Native.css.style.overflow = IStyle.OverflowEnum.hidden;

            Native.body.Clear();

            new IHTMLPre { "can we stream it into VR, shadertoy, youtube 360, youtube stereo yet?" }.AttachToDocument();


            var sw = Stopwatch.StartNew();

            var pause = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, title = "pause" }.AttachToDocument();


            pause.onchange += delegate
            {

                if (pause.@checked)
                    sw.Stop();
                else
                    sw.Start();


            };

            var oo = new List<THREE.Object3D>();

            #region scene
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

            var ambient = new THREE.AmbientLight(0x303030);
            scene.add(ambient);

            // should we fix jsc to do a more correct IDL?
            var directionalLight = new THREE.DirectionalLight(0xffffff, 0.7);
            directionalLight.position.set(0, 0, 1);
            scene.add(directionalLight);



            // whats WebGLRenderTargetCube do?

            // WebGLRenderer preserveDrawingBuffer 

            //const int size = 128;
            //const int size = 256; // 6 faces, 12KB
            //const int size = 512; // 6 faces, ?

            // WebGL: drawArrays: texture bound to texture unit 0 is not renderable. It maybe non-power-of-2 and have incompatible texture filtering or is not 'texture complete'. Or the texture is Float or Half Float type with linear filtering while OES_float_linear or OES_half_float_linear extension is not enabled.

            //const int size = 720; // 6 faces, ?
            const int size = 1024; // 6 faces, ?

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
            renderer0.setClearColor(0xfffff, 1);

            //renderer.setSize(window.Width, window.Height);
            renderer0.setSize(size, size);

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

            var uizoom = 0.1;


            #region y
            // need to rotate90?
            var cameraNY = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: 2000);
            cameraNY.lookAt(new THREE.Vector3(0, -1, 0));
            //cameraNY.lookAt(new THREE.Vector3(0, 1, 0));
            var canvasNY = new CanvasRenderingContext2D(size, size);
            canvasNY.canvas.style.SetLocation(8 + (int)(uizoom * size + 8) * 1, 8 + (int)(uizoom * size + 8) * 2);
            canvasNY.canvas.title = "NY";
            canvasNY.canvas.AttachToDocument();
            canvasNY.canvas.style.transformOrigin = "0 0";
            canvasNY.canvas.style.transform = $"scale({uizoom})";

            var cameraPY = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: 2000);
            cameraPY.lookAt(new THREE.Vector3(0, 1, 0));
            //cameraPY.lookAt(new THREE.Vector3(0, -1, 0));
            var canvasPY = new CanvasRenderingContext2D(size, size);
            canvasPY.canvas.style.SetLocation(8 + (int)(uizoom * size + 8) * 1, 8 + (int)(uizoom * size + 8) * 0);
            canvasPY.canvas.title = "PY";
            canvasPY.canvas.AttachToDocument();
            canvasPY.canvas.style.transformOrigin = "0 0";
            canvasPY.canvas.style.transform = $"scale({uizoom})";
            #endregion

            // transpose xz?

            #region x
            var cameraNX = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: 2000);
            cameraNX.lookAt(new THREE.Vector3(0, 0, 1));
            //cameraNX.lookAt(new THREE.Vector3(0, 0, -1));
            //cameraNX.lookAt(new THREE.Vector3(-1, 0, 0));
            //cameraNX.lookAt(new THREE.Vector3(1, 0, 0));
            var canvasNX = new CanvasRenderingContext2D(size, size);
            canvasNX.canvas.style.SetLocation(8 + (int)(uizoom * size + 8) * 2, 8 + (int)(uizoom * size + 8) * 1);
            canvasNX.canvas.title = "NX";
            canvasNX.canvas.AttachToDocument();
            canvasNX.canvas.style.transformOrigin = "0 0";
            canvasNX.canvas.style.transform = $"scale({uizoom})";

            var cameraPX = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: 2000);
            cameraPX.lookAt(new THREE.Vector3(0, 0, -1));
            //cameraPX.lookAt(new THREE.Vector3(0, 0, 1));
            //cameraPX.lookAt(new THREE.Vector3(1, 0, 0));
            //cameraPX.lookAt(new THREE.Vector3(-1, 0, 0));
            var canvasPX = new CanvasRenderingContext2D(size, size);
            canvasPX.canvas.style.SetLocation(8 + (int)(uizoom * size + 8) * 0, 8 + (int)(uizoom * size + 8) * 1);
            canvasPX.canvas.title = "PX";
            canvasPX.canvas.AttachToDocument();
            canvasPX.canvas.style.transformOrigin = "0 0";
            canvasPX.canvas.style.transform = $"scale({uizoom})";
            #endregion



            #region z
            var cameraNZ = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: 2000);
            //cameraNZ.lookAt(new THREE.Vector3(0, 0, -1));
            cameraNX.lookAt(new THREE.Vector3(1, 0, 0));
            //cameraNX.lookAt(new THREE.Vector3(-1, 0, 0));
            //cameraNZ.lookAt(new THREE.Vector3(0, 0, 1));
            var canvasNZ = new CanvasRenderingContext2D(size, size);
            canvasNZ.canvas.style.SetLocation(8 + (int)(uizoom * size + 8) * 3, 8 + (int)(uizoom * size + 8) * 1);
            canvasNZ.canvas.title = "NZ";
            canvasNZ.canvas.AttachToDocument();
            canvasNZ.canvas.style.transformOrigin = "0 0";
            canvasNZ.canvas.style.transform = $"scale({uizoom})";

            var cameraPZ = new THREE.PerspectiveCamera(fov: 90, aspect: 1.0, near: 1, far: 2000);
            //cameraPZ.lookAt(new THREE.Vector3(1, 0, 0));
            cameraPZ.lookAt(new THREE.Vector3(-1, 0, 0));
            //cameraPZ.lookAt(new THREE.Vector3(0, 0, 1));
            //cameraPZ.lookAt(new THREE.Vector3(0, 0, -1));
            var canvasPZ = new CanvasRenderingContext2D(size, size);
            canvasPZ.canvas.style.SetLocation(8 + (int)(uizoom * size + 8) * 1, 8 + (int)(uizoom * size + 8) * 1);
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
            var gl = new WebGLRenderingContext(alpha: true);
            var c = gl.canvas.AttachToDocument();

            //  3840x2160

            //c.style.SetSize(3840, 2160);

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150722/360-youtube


            c.width = 3840;
            c.height = 2160;

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
            c.style.backgroundColor = "yellow";
            c.style.position = IStyle.PositionEnum.absolute;

            c.style.SetLocation(8 + (int)(uizoom * size + 8) * 0, 8 + (int)(uizoom * size + 8) * 3);

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

            //var xor = new HTML.Images.FromAssets.tiles_regrid().AttachToDocument();
            //var xor = new HTML.Images.FromAssets.Orion360_test_image_8192x4096().AttachToDocument();
            var xor = new HTML.Images.FromAssets._2_no_clouds_4k().AttachToDocument();


            // 270px
            //xor.style.height = "";
            xor.style.height = "270px";
            xor.style.width = "480px";
            xor.style.SetLocation(
                8 + (int)(uizoom * size + 8) * 0 + 480 + 16, 8 + (int)(uizoom * size + 8) * 3);


            var mesh = new THREE.Mesh(new THREE.SphereGeometry(500, 50, 50),
           new THREE.MeshBasicMaterial(new
           {
               map = THREE.ImageUtils.loadTexture(
                  //new HTML.Images.FromAssets._2294472375_24a3b8ef46_o().src
                  //new HTML.Images.FromAssets._4008650304_7f837ccbb7_b().src
                  xor.src
                   //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20130616_222058().src
                   //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20121225_210448().src

                   )
           }));
            mesh.scale.x = -1;
            scene.add(mesh);


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

                       scene.add(dae);
                       oo.Add(dae);




                       // view-source:http://threejs.org/examples/webgl_multiple_canvases_circle.html
                       // https://threejsdoc.appspot.com/doc/three.js/src.source/extras/cameras/CubeCamera.js.html
                       Native.window.onframe +=
                           e =>
                           {
                               //if (pause) return;
                               //if (pause.@checked)
                               //    return;


                               // can we float out of frame?
                               // haha. a bit too flickery.
                               //dae.position.x = Math.Sin(e.delay.ElapsedMilliseconds * 0.01) * 50.0;
                               //dae.position.x = Math.Sin(e.delay.ElapsedMilliseconds * 0.001) * 190.0;
                               dae.position.x = Math.Sin(sw.ElapsedMilliseconds * 0.0001) * 190.0;
                               dae.position.y = Math.Cos(sw.ElapsedMilliseconds * 0.0001) * 90.0;
                               // manual rebuild?
                               // red compiler notifies laptop chrome of pending update
                               // app reloads


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
                               canvasPX.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, size, size);

                               renderer0.render(scene, cameraNX);
                               canvasNX.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, size, size);
                               #endregion

                               #region z
                               renderer0.render(scene, cameraPZ);
                               canvasPZ.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, size, size);

                               renderer0.render(scene, cameraNZ);
                               canvasNZ.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, size, size);
                               #endregion



                               #region y
                               renderer0.render(scene, cameraPY);

                               //canvasPY.save();
                               //canvasPY.translate(0, size);
                               //canvasPY.rotate((float)(-Math.PI / 2));
                               canvasPY.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, size, size);
                               //canvasPY.restore();


                               renderer0.render(scene, cameraNY);
                               //canvasNY.save();
                               //canvasNY.translate(size, 0);
                               //canvasNY.rotate((float)(Math.PI / 2));
                               canvasNY.drawImage((IHTMLCanvas)renderer0.domElement, 0, 0, size, size);
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

                           };


                   }
               );


            #endregion



            Console.WriteLine("do you see it?");
        }

    }
}

//{ Message = Could not load file or assembly 'Chrome Web Store, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified. }
//1c48:02:01:14 after worker yield...

//Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.