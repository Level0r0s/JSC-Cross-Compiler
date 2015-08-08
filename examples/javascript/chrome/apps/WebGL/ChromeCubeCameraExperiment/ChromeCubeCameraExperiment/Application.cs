#define AsWEBSERVER

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
using ChromeCubeCameraExperiment;
using ChromeCubeCameraExperiment.Design;
using ChromeCubeCameraExperiment.HTML.Pages;
using System.Diagnostics;

namespace ChromeCubeCameraExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // the eye nor the display will be able to do any stereo
        // until tech is near matrix capability. 2019?

        // cubemap can be used for all long range scenes
        // http://www.imdb.com/title/tt0112111/?ref_=nv_sr_1


        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150808/cubemapcamera
        // subst a: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeCubeCameraExperiment\ChromeCubeCameraExperiment\bin\Debug\staging\ChromeCubeCameraExperiment.Application\web
        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeCubeCameraExperiment\ChromeCubeCameraExperiment\bin\Debug\staging\ChromeCubeCameraExperiment.Application\web

        // ColladaLoader: Empty or non-existing file (assets/ChromeCubeCameraExperiment/S6Edge.dae)

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

                        new chrome.Notification(title: "ChromeCubeCameraExperiment");

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

            Native.body.Clear();

            var oo = new List<THREE.Object3D>();

            #region scene
            var window = Native.window;


            // what about physics and that portal rendering?

            // if we are running as a chrome web server, we may also be opened as android ndk webview app
            var cameraPX = new THREE.PerspectiveCamera(fov: 90, aspect: window.aspect, near: 1, far: 2000);
            // once we update source
            // save the source
            // manually recompile 
            //cameraPX.position.z = 400;

            // the camera should be close enough for the object to float off the FOV of PX
            cameraPX.position.z = 200;

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
            var rendererPX = new THREE.WebGLRenderer(

                new
                {
                    antialias = true,
                    alpha = true,
                    preserveDrawingBuffer = true
                }
            );

            // https://github.com/mrdoob/three.js/issues/3836

            // the construct. white bg
            rendererPX.setClearColor(0xfffff, 1);

            //renderer.setSize(window.Width, window.Height);
            rendererPX.setSize(256, 256);

            rendererPX.domElement.AttachToDocument();
            rendererPX.domElement.style.SetLocation(0, 0);

            var canvasPX = (IHTMLCanvas)rendererPX.domElement;


            var old = new
            {



                CursorX = 0,
                CursorY = 0
            };


            var mouseX = 0;
            var mouseY = 0;
            var st = new Stopwatch();
            st.Start();


            canvasPX.css.active.style.cursor = IStyle.CursorEnum.move;

            #region onmousedown
            canvasPX.onmousedown +=
                e =>
                {

                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        canvasPX.requestFullscreen();
                    }
                    else
                    {
                        // movementX no longer works
                        old = new
                        {


                            e.CursorX,
                            e.CursorY
                        };


                        e.CaptureMouse();
                    }

                };
            #endregion



            // X:\jsc.svn\examples\javascript\Test\TestMouseMovement\TestMouseMovement\Application.cs
            #region onmousemove
            canvasPX.onmousemove +=
                e =>
                {
                    var pointerLock = canvasPX == Native.document.pointerLockElement;


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
            var z = cameraPX.position.z;

            //#region onmousewheel
            //canvas.onmousewheel +=
            //    e =>
            //    {
            //        //camera.position.z = 1.5;

            //        // min max. shall adjust speed also!
            //        // max 4.0
            //        // min 0.6
            //        z -= 10.0 * e.WheelDirection;

            //        //camera.position.z = 400;
            //        z = z.Max(200).Min(500);

            //        //Native.document.title = new { z }.ToString();

            //    };
            //#endregion



            // THREE.WebGLProgram: gl.getProgramInfoLog() (79,3-98): warning X3557: loop only executes for 1 iteration(s), forcing loop to unroll

            new Models.ColladaS6Edge().Source.Task.ContinueWithResult(
                   dae =>
                   {
                       // 90deg
                       dae.rotation.x = -Math.Cos(Math.PI);

                       //dae.scale.x = 30;
                       //dae.scale.y = 30;
                       //dae.scale.z = 30;
                       dae.position.z = 65;

                       var scale = 0.7;

                       // jsc, do we have ILObserver available yet?
                       dae.scale.x = scale;
                       dae.scale.y = scale;
                       dae.scale.z = scale;

                       //dae.position.y = -80;

                       scene.add(dae);
                       oo.Add(dae);


                       var sw = Stopwatch.StartNew();


                       // https://threejsdoc.appspot.com/doc/three.js/src.source/extras/cameras/CubeCamera.js.html
                       Native.window.onframe +=
                           e =>
                           {

                               // can we float out of frame?
                               // haha. a bit too flickery.
                               //dae.position.x = Math.Sin(e.delay.ElapsedMilliseconds * 0.01) * 50.0;
                               //dae.position.x = Math.Sin(e.delay.ElapsedMilliseconds * 0.001) * 190.0;
                               dae.position.x = Math.Sin(sw.ElapsedMilliseconds * 0.001) * 190.0;
                               // manual rebuild?
                               // red compiler notifies laptop chrome of pending update
                               // app reloads


                               rendererPX.clear();

                               cameraPX.aspect = canvasPX.aspect;
                               cameraPX.updateProjectionMatrix();

                               // um what does this do?
                               //cameraPX.position.z += (z - cameraPX.position.z) * e.delay.ElapsedMilliseconds / 200.0;
                               // mousewheel allos the camera to move closer
                               // once we see the frame in vr, can we udp sync vr tracking back to laptop?

                               cameraPX.lookAt(scene.position);

                               // how can we render cubemap?
                               rendererPX.render(scene, cameraPX);


                           };


                   }
               );


            Native.window.onresize +=
                delegate
                {
                    //if (canvas.parentNode == Native.document.body)
                    //{
                    //    renderer.setSize(window.Width, window.Height);
                    //}

                };
            #endregion



            Console.WriteLine("do you see it?");
        }

    }
}

//{ Message = Could not load file or assembly 'Chrome Web Store, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified. }
//1c48:02:01:14 after worker yield...

//Unhandled Exception: System.IO.FileNotFoundException: Could not load file or assembly 'Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.