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
using ChromeEquirectangularPanorama;
using ChromeEquirectangularPanorama.Design;
using ChromeEquirectangularPanorama.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace ChromeEquirectangularPanorama
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150807/ovroculus360photosndk

        // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\bin\Debug\staging\ChromeEquirectangularPanorama.Application\web
        // subst a: r:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\bin\Debug\staging\ChromeEquirectangularPanorama.Application\web
        // subst a: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\bin\Debug\staging\ChromeEquirectangularPanorama.Application\web
        // subst b: s:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\bin\Debug\staging\ChromeEquirectangularPanorama.Application\web

        // 237ms UdpClient.Client.vBind { ipString = 0.0.0.0, Port = 49000, bind = 0 }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://www.shadertoy.com/view/lsSGRz




            // "X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPFloats\ChromeUDPFloats\Application.cs"


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

            //02000047 ChromeEquirectangularPanorama.Application+ctor>b__7>d__1d+<MoveNext>0600001d
            //script: error JSC1000: *** stack is empty, invalid pop?
            //script: error JSC1000: error at ChromeEquirectangularPanorama.Application+ctor>b__7>d__1d+<MoveNext>0600001d.<00c3> ldloca.s.try,
            // assembly: W:\ChromeEquirectangularPanorama.Application.exe
            // type: ChromeEquirectangularPanorama.Application+ctor>b__7>d__1d+<MoveNext>0600001d, ChromeEquirectangularPanorama.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x003e
            //  method:Int32 <00c3> ldloca.s.try(<MoveNext>0600001d, ctor>b__7>d__1d ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[chrome.NetworkInterface[]] ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[chrome.NetworkInterface[]] ByRef)
            //*** Compiler cannot continue... press enter to quit.






            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/udp




            var window = Native.window;

            // Error creating WebGL context.

            #region webgl
            //var fov = 70.0;
            var fov = 90.0;

            var camera = new THREE.PerspectiveCamera(fov,
                window.aspect, 1, 1100);
            var target = new THREE.Vector3(0, 0, 0);

            //(camera as dynamic).target = target;

            var scene = new THREE.Scene();

            var meshmaterial =
                new THREE.MeshBasicMaterial(new
                {
                    map = THREE.ImageUtils.loadTexture(
                        new ChromeEquirectangularPanorama.HTML.Images.FromAssets._2294472375_24a3b8ef46_o().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20130616_222058().src
                        //new WebGLEquirectangularPanorama.HTML.Images.FromAssets.PANO_20121225_210448().src

                        )
                });

            var mesh = new THREE.Mesh(new THREE.SphereGeometry(500, 60, 40),
                meshmaterial
                );

            mesh.scale.x = -1;
            scene.add(mesh);

            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(window.Width, window.Height);

            renderer.domElement.AttachToDocument();

            //renderer.domElement.style.position = IStyle.PositionEnum.absolute;
            renderer.domElement.style.SetLocation(0, 0);

            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;


            #region onresize
            Native.window.onresize +=
                delegate
                {
                    camera.aspect = Native.window.aspect;
                    camera.updateProjectionMatrix();

                    renderer.setSize(Native.window.Width, Native.window.Height);
                };
            #endregion


            Native.document.body.onmousewheel +=
                e =>
                {
                    fov -= e.WheelDirection * 5.0;
                    camera.projectionMatrix.makePerspective(fov,
                        (double)window.Width / window.Height, 1, 1100);
                };

            var gearvr_x = 0f;
            // left to right
            var gearvr_y = 0f;
            var gearvr_z = 0f;


            // set by?
            var gearvr_filename = "";

            var lon = 90.0;
            var lat = 0.0;
            var phi = 0.0;
            var theta = 0.0;

            Native.window.onframe +=
                delegate
                {
                    if (Native.document.pointerLockElement == Native.document.body)
                        lon += 0.00;
                    else
                        lon += 0.01;

                    lat = Math.Max(-85, Math.Min(85, lat));



                    //Native.document.title = new { lon, lat }.ToString();

                    // http://www.gamedev.net/topic/626401-quaternion-from-latitude-and-longitude/
                    //void xyz_to_latlon(const double x, const double y, const double z, double &lat, double &lon)
                    //{
                    //    double theta = pi + atan2(z, x);
                    //    double phi = acos(-y);

                    //    lat = phi/pi*180.0 - 90.0;
                    //    lon = theta/(2*pi)*360.0 - 180.0;
                    //}


                    //double vrtheta = Math.PI + Math.Atan2(gearvr_z, gearvr_x);
                    //double vrphi = Math.Acos(-gearvr_y);

                    //var vrlat = phi / Math.PI * 180.0 - 90.0;
                    //var vrlon = theta / (2 * Math.PI) * 360.0 - 180.0;


                    // 	const ovrMatrix4f centerEyeRotation = ovrMatrix4f_CreateFromQuaternion( &tracking->HeadPose.Pose.Orientation );

                    // http://stackoverflow.com/questions/11665562/three-js-how-to-use-quaternion-to-rotate-camera

                    phi = THREE.Math.degToRad(90 - lat) - Math.Sign(gearvr_x) * (gearvr_x * gearvr_x) * Math.PI;
                    theta = THREE.Math.degToRad(lon) - Math.Sign(gearvr_y) * (gearvr_y * gearvr_y) * Math.PI;

                    target.x = 500 * Math.Sin(phi) * Math.Cos(theta);
                    target.y = 500 * Math.Cos(phi);
                    target.z = 500 * Math.Sin(phi) * Math.Sin(theta);

                    camera.lookAt(target);

                    renderer.render(scene, camera);

                };
            #endregion



            // http://blog.thematicmapping.org/2013/10/terrain-visualization-with-threejs-and.html

            #region camera rotation
            Native.document.body.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        lon += e.movementX * 0.1;
                        lat -= e.movementY * 0.1;

                        //Console.WriteLine(new { lon, lat, e.movementX, e.movementY });
                    }
                };


            Native.document.body.onmouseup +=
              e =>
              {
                  if (e.MouseButton == IEvent.MouseButtonEnum.Right)
                      Native.document.exitPointerLock();

                  //drag = false;
                  e.preventDefault();
              };

            renderer.domElement.onmousedown +=
                async e =>
                {
                    //e.CaptureMouse();

                    //drag = true;
                    e.preventDefault();
                    Native.document.body.requestPointerLock();

                    //await renderer.domElement.async.onmousedown;

                    //Native.document.exitPointerLock();
                };


            //Native.document.body.oncontextmenu +=
            //    delegate
            //    {
            //        Native.document.exitPointerLock();

            //    };
            //Native.document.body.ondblclick +=
            //    e =>
            //    {
            //        e.preventDefault();

            //        Console.WriteLine("requestPointerLock");
            //    };

            #endregion


            var file64 = "";
            var file = new MemoryStream();
            var segment0 = new byte[0];

            #region toolbar
            var toolbar = new IHTMLDiv { }.AttachToDocument();

            Native.body.style.margin = "0em";
            Native.body.style.padding = "0em";
            Native.document.documentElement.style.margin = "0em";
            Native.document.documentElement.style.padding = "0em";

            new IStyle(toolbar)
            {
                position = IStyle.PositionEnum.absolute,

                padding = "1em",

                top = "0em",
                right = "0em",
                bottom = "0em",

                color = "yellow",

                //overflow = IStyle.OverflowEnum.scroll
                overflow = IStyle.OverflowEnum.auto
            };

            toolbar.css[IHTMLElement.HTMLElementEnum.img].style.cursor = IStyle.CursorEnum.pointer;

            toolbar.css[IHTMLElement.HTMLElementEnum.img].style.display = IStyle.DisplayEnum.block;
            //toolbar.css.children.style.display = IStyle.DisplayEnum.block;

            new IHTMLButton { "update pending... update available. click to reload.." }.AttachTo(toolbar).onclick += delegate
            {
                // can we get an udp signal from the compiler when the app is out of date, when the update is pending?
                chrome.runtime.reload();
            };



            //var n = await chrome.socket.getNetworkList();
            ////var n24 = n.Where(x => x.prefixLength == 24).ToArray();



            #region UDPClipboardSend
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/x360videoui
            // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\Application.cs
            Action<byte[]> UDPClipboardSend = async data =>
            {
                var n = await chrome.socket.getNetworkList();

                new IHTMLPre { new { n.Length } }.AttachToDocument();

                // LINQ and async wont mix for 2012?

                //foreach (var item in n.Where(x => x.prefixLength == 24))
                foreach (var item in n) if (item.prefixLength == 24)
                    {
                        new IHTMLPre { new { item.prefixLength, item.name, item.address } }.AttachToDocument();

                        //{ prefixLength = 64, name = {AE3B881D-488F-4C3A-93F8-7DA0D65B9300}, address = fe80::fc45:cae9:46ca:7b0f }
                        //about to bind... { port = 29129 }
                        //about to send... { Length = 0 }
                        //sent: -2
                        //{ prefixLength = 24, name = {AE3B881D-488F-4C3A-93F8-7DA0D65B9300}, address = 192.168.1.12 }
                        //about to bind... { port = 25162 }
                        //about to send... { Length = 0 }
                        //sent: 0


                        // X:\jsc.svn\examples\merge\TestDetectOpenFiles\TestDetectOpenFiles\Program.cs
                        // X:\jsc.svn\examples\javascript\chrome\apps\MulticastListenExperiment\MulticastListenExperiment\Application.cs

                        // https://code.google.com/p/chromium/issues/detail?id=455352

                        // X:\jsc.svn\examples\merge\TestDetectOpenFiles\TestDetectOpenFiles\Program.cs

                        // bind?

                        //var data = Encoding.UTF8.GetBytes(message);	   //creates a variable b of type byte

                        // http://stackoverflow.com/questions/13691119/chrome-packaged-app-udp-sockets-not-working
                        // http://www.chinabtp.com/how-to-do-udp-broadcast-using-chrome-sockets-udp-api/

                        // chrome likes 0 too.
                        var port = new Random().Next(16000, 40000);
                        //var port = 0;
                        // 
                        //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                        // where is bind async?
                        var socket = new UdpClient();
                        socket.Client.Bind(

                            //new IPEndPoint(IPAddress.Any, port: 40000)
                            new IPEndPoint(IPAddress.Parse(item.address), port)
                        );


                        //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                        var s = await socket.SendAsync(
                            data,
                            data.Length,
                            hostname: "239.1.2.3",
                            port: 39814
                        );

                        //new IHTMLPre { "sent: " + s }.AttachToDocument();


                        //socket.ReceiveAsync
                        //socket.Close();

                        //new IHTMLPre { $"sent: {s}" }.AttachToDocument();

                        // android cannot see it. why? because it needs to know which NIC to use.

                    }
            };
            #endregion



            new { }.With(
                  async delegate
                  {
                      //          if (nic.prefixLength != 24)
                      //              return;

                      //var status = new IHTMLPre { new { nic.address } }.AttachTo(toolbar);
                      var HUD = new IHTMLPre { "awaiting segment0..." }.AttachTo(toolbar);

                      // 500000

                      var uu = new UdpClient(49000);

                      //uu.ExclusiveAddressUse = false;
                      var md5string = "";

                      uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"));
                      while (true)
                      {
                          var data = await uu.ReceiveAsync(); // did we jump to ui thread?

                          // jpeg progressive?
                          segment0 = data.Buffer;
                          file.Write(segment0, 0, segment0.Length);

                          // if segment is less than 65507
                          // then download is complete

                          if (file.Length > 0)
                              if (segment0.Length < 65507)
                              {

                                  //lobal::System.Security.Cryptography.MD5CryptoServiceProvider

                                  var bytes = file.ToArray();


                                  var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(bytes);

                                  // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/x360videoui
                                  md5string = md5.ToHexString();


                                  // this is slow.. worker?
                                  file64 = Convert.ToBase64String(bytes);

                                  //data:[<MIME-type>][;charset=<encoding>][;base64],

                                  file = new MemoryStream();
                                  var src = "data:image/jpeg;base64," + file64;
                                  new { }.With(
                                      async delegate
                                      {
                                          var img = new IHTMLImage { src = src, title = md5string }.AttachTo(toolbar);
                                          img.style.height = "6em";

                                          do
                                          {
                                              // send udp back. so vr knows which image we went back to?

                                              mesh.material.map.image = img;
                                              mesh.material.map.needsUpdate = true;

                                              await img.async.onclick;

                                              //send sha1
                                              UDPClipboardSend(md5);
                                          }
                                          while (true);
                                      }
                                  );


                              }

                          HUD.innerText =
                              "segment0  " + segment0.Length
                              + "\nfile " + file.Length
                            + "\nfile64 " + file64.Length;


                      }
                  }
              );



            #region awaiting tracking
            new { }.With(
                    async delegate
                    {
                        //if (nic.prefixLength != 24)
                        //    return;

                        //var status = new IHTMLPre { new { nic.address } }.AttachTo(toolbar);
                        var HUD = new IHTMLPre { "awaiting tracking..." }.AttachTo(toolbar);


                        var uu = new UdpClient(49834);
                        //uu.ExclusiveAddressUse = false;
                        uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"));
                        while (true)
                        {
                            var data = await uu.ReceiveAsync(); // did we jump to ui thread?
                            //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                            //args.vertexTransform = x.Buffer;

                            var xy = Encoding.UTF8.GetString(data.Buffer).Split(':');

                            gearvr_x = float.Parse(xy[0]);
                            gearvr_y = float.Parse(xy[1]);
                            gearvr_z = float.Parse(xy[2]);
                            var w = float.Parse(xy[3]);
                            gearvr_filename = xy[4];

                            HUD.innerText = new { gearvr_x, gearvr_y, gearvr_z, w, gearvr_filename }.ToString().Replace(",", ",\n");

                        }
                    }
            );
            #endregion


            #endregion

        }

    }
}
