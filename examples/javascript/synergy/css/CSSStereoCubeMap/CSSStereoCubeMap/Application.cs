using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CSSStereoCubeMap;
using CSSStereoCubeMap.Design;
using CSSStereoCubeMap.HTML.Pages;
using CSSStereoCubeMap.HTML.Images.FromAssets;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSSStereoCubeMap
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150812/cssstereo

        //2d50:02:01:1e RewriteToAssembly error: System.IO.FileNotFoundException: Could not find file 'W:/CSSStereoCubeMap.ApplicationWebService.AssetsLibrary.dll'.
        //File name: 'W:/CSSStereoCubeMap.ApplicationWebService.AssetsLibrary.dll'
        //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)

        // wtf


        public class side
        {
            // T where Image or Canvas
            //public IHTMLElement CSS3DObject_element;
            public IHTMLElement CSS3DObject_element;

            public THREE.Vector3 position;
            public THREE.Vector3 rotation;
        }
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // haha this is rather messed up on android.
            // perhaps the css3d runs out of memory?

            //var frame0 = Task.Delay(100);
            //var frame1 = Task.Delay(200);



            var sw = Stopwatch.StartNew();



            //var lon0 = 90.0;
            var lon0 = 0.0;
            var lon1 = 0.0;

            var lon = new sum(
                 () => lon0,
                 () => lon1
             );

            var lat0 = 0.0;
            var lat1 = 0.0;

            // or could we do it with byref or pointers?
            var lat = new sum(
                () => lat0,
                () => lat1
            );

            var phi = 0.0;
            var theta = 0.0;



            var camera_rotation_z = 0.0;


            var drag = false;



            Action<int> draweye = async (int eyeid) =>
                {
                    // view-source:file:///X:/opensource/github/three.js/examples/css3d_panorama.html

                    //var camera = new THREE.PerspectiveCamera(75, Native.window.aspect, 1, 1000);

                    // wearality lenses?
                    //var camera = new THREE.PerspectiveCamera(90, Native.window.aspect, 1, 1000);
                    //var camera = new THREE.PerspectiveCamera(120, Native.window.aspect, 1, 1000);
                    var camera = new THREE.PerspectiveCamera(96, Native.window.aspect, 1, 1000);
                    var scene = new THREE.Scene();

                    var renderer0 = new THREE.CSS3DRenderer();

                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150621
                    //var f12 = await new airplane().async.oncomplete;
                    //var f12 = await new airplane_low().async.oncomplete;
                    //var f12 = await new airplane_1024().async.oncomplete;
                    //var f12 = await new airplane_729().async.oncomplete;
                    // stop flickering damnet
                    var f12 = await new airplane_400().async.oncomplete;



                    // ok we got this cool 12 frame stereo map.

                    // what happens if we just pass it?
                    // haha. we get all frames in one.


                    //Func<int, var> f = i =>

                    #region f
                    Func<int, IHTMLCanvas> f = i =>
                    {
                        // we do have a skybox example somewhere...
                        var f1 = new CanvasRenderingContext2D(w: f12.height, h: f12.height);


                        // can we keep animating the stereo ?

                        // if we return canvas it gets messed up. why?
                        // looks to  be a bug?

                        //var stale = new IHTMLImage();

                        if (eyeid == 0)
                        {
                            // GearVR would have both eyes!
                            // laptop has to flip between eyes to give similar effect?
                            // if this were a chrome app. could gearvr request the frames into the photos360 app?

                            f1.drawImage(f12, i * f12.height, 0, sw: f12.height, sh: f12.height,
                            dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                            // whenever we call drawImage ? callsite event monitoring?
                            // this seems to be slow
                            //stale.src = f1.canvas.toDataURL();

                            // can we have a synchronized frame choreo?
                            //await Task.Delay(1000 / 15);
                        }
                        else
                        {
                            //await frame0;

                            // update!

                            f1.drawImage(f12, (i + 6) * f12.height, 0, sw: f12.height, sh: f12.height,
                            dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                            //stale.src = f1.canvas.toDataURL();

                            //await frame1;
                            //await Task.Delay(1000 / 15);

                        };

                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150812/cssstereo

                        return f1;
                    };
                    #endregion


                    //var f0 = new CanvasRenderingContext2D(w: f12.height, h: f12.height);
                    //f0.drawImage(f12, 0, 0, sw: f12.height, sh: f12.height,
                    //    dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                    #region sides
                    var sides = new[]
                    {
                new side
                {
                    CSS3DObject_element=  f(0),
                    position= new THREE.Vector3( -512, 0, 0 ),
                    rotation= new THREE.Vector3( 0, Math.PI / 2, 0 )
                },
                new side {
                    //img=  new humus_nx(),
                    CSS3DObject_element = f(1),

                    position= new THREE.Vector3( 512, 0, 0 ),
                    rotation= new THREE.Vector3( 0, -Math.PI / 2, 0 )
                },
                new side{
                    CSS3DObject_element=  f(2),
                    //img=  new humus_py(),
                    position= new THREE.Vector3( 0,  512, 0 ),
                    rotation= new THREE.Vector3( Math.PI / 2, 0, Math.PI )
                },
                new side{
                    //img=  new humus_ny(),
                    CSS3DObject_element=  f(3),
                    position= new THREE.Vector3( 0, -512, 0 ),
                    rotation= new THREE.Vector3( - Math.PI / 2, 0, Math.PI )
                },
                new side{
                    CSS3DObject_element=  f(4),
                    //img=  new humus_pz(),
                    position= new THREE.Vector3( 0, 0,  512 ),
                    rotation= new THREE.Vector3( 0, Math.PI, 0 )
                },
                new side{
                    CSS3DObject_element=  f(5),
                    //img=  new humus_nz(),
                    position= new THREE.Vector3( 0, 0, -512 ),
                    rotation= new THREE.Vector3( 0, 0, 0 )
                }
            };
                    #endregion

                    for (var i = 0; i < sides.Length; i++)
                    {
                        var side = sides[i];

                        var element = side.CSS3DObject_element;

                        element.style.SetSize(1026, 1026);
                        //element.style.SetSize(256, 256);

                        //element.width = 1026; // 2 pixels extra to close the gap.

                        var xobject = new THREE.CSS3DObject(element);
                        xobject.position.set(side.position.x, side.position.y, side.position.z);
                        xobject.rotation.set(side.rotation.x, side.rotation.y, side.rotation.z);
                        scene.add(xobject);

                    }


                    //<div style="-webkit-transform-style: preserve-3d; width: 978px; height: 664px; -webkit-transform: translate3d(0px, 0px, 432.6708237832803px) matrix3d(0.34382355213165283, -0.024581052362918854, -0.938712477684021, 0, 0, -0.9996572732925415, 0.026176948100328445, 0, 0.9390342831611633, 0.00900025200098753, 0.34370577335357666, 0, 0, 0, 0, 0.9999999403953552) translate3d(489px, 332px, 0px);">
                    //        <img src="assets/CSSStereoCubeMap/posx.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, -512, 0, 0, 1);"><img src="assets/CSSStereoCubeMap/negx.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 512, 0, 0, 1);"><img src="assets/CSSStereoCubeMap/posy.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 512, 0, 1);"><img src="assets/CSSStereoCubeMap/negy.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, -512, 0, 1);"><img src="assets/CSSStereoCubeMap/posz.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, 512, 1);"><img src="assets/CSSStereoCubeMap/negz.jpg" width="1026" style="width: 1024px; height: 1024px; position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, -512, 1);"></div>
                    //<div style="-webkit-transform-style: preserve-3d; width: 978px; height: 664px; -webkit-transform: translate3d(0px, 0px, 432.6708237832803px) matrix3d(-0.4524347484111786, 0, 0.8917974829673767, 0, 0, -1, 0, 0, -0.8917974829673767, 0, -0.4524347484111786, 0, 0, 0, 0, 1) translate3d(489px, 332px, 0px);">
                    // <img width="1026" src="textures/cube/Bridge2/posx.jpg"                                             style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(0, 0, -1, 0, 0, -1, 0, 0, 1, 0, 0, 0, -512, 0, 0, 1);"><img width="1026" src="textures/cube/Bridge2/negx.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(0, 0, 1, 0, 0, -1, 0, 0, -1, 0, 0, 0, 512, 0, 0, 1);"><img width="1026" src="textures/cube/Bridge2/posy.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(-1, 0, 0, 0, 0, 0, 1, 0, 0, -1, 0, 0, 0, 512, 0, 1);"><img width="1026" src="textures/cube/Bridge2/negy.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(-1, 0, 0, 0, 0, 0, -1, 0, 0, 1, 0, 0, 0, -512, 0, 1);"><img width="1026" src="textures/cube/Bridge2/posz.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(-1, 0, 0, 0, 0, -1, 0, 0, 0, 0, -1, 0, 0, 0, 512, 1);"><img width="1026" src="textures/cube/Bridge2/negz.jpg" style="position: absolute; -webkit-transform-style: preserve-3d; -webkit-transform: translate3d(-50%, -50%, 0px) matrix3d(1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 1, 0, 0, 0, -512, 1);"></div> 


                    renderer0.domElement.AttachToDocument();

                    #region onresize
                    new { }.With(
                        async delegate
                        {
                            do
                            {

                                //camera.aspect = Native.window.aspect;
                                camera.aspect = Native.window.Width / 2.0 / Native.window.Height;
                                camera.updateProjectionMatrix();

                                //renderer0.setSize(Native.window.Width / 2, Native.window.Height);
                                renderer0.setSize(Native.window.Width / 2, Native.window.Height);
                                //renderer0.domElement.style.SetLocation(Native.window.Width / 2 * eyeid, 0);
                                renderer0.domElement.style.SetLocation(Native.window.Width / 2 * (1 - eyeid), 0);

                            }
                            while (await Native.window.async.onresize);
                        });
                    #endregion

                    var target = new THREE.Vector3();


                    Native.window.onframe +=
                        delegate
                        {
                            //if (Native.document.pointerLockElement == Native.document.body)
                            //    lon += 0.00;
                            //else
                            //    lon += 0.01;

                            //lat = Math.Max(-85, Math.Min(85, lat));

                            Native.document.title = new { lon, lat }.ToString();


                            //phi = THREE.Math.degToRad(90 - lat);
                            //theta = THREE.Math.degToRad(lon);

                            //target.x = Math.Sin(phi) * Math.Cos(theta);
                            //target.y = Math.Cos(phi);
                            //target.z = Math.Sin(phi) * Math.Sin(theta);

                            //camera.lookAt(target);


                            phi = THREE.Math.degToRad(90 - lat);
                            theta = THREE.Math.degToRad(lon);

                            target.x = 500 * Math.Sin(phi) * Math.Cos(theta);
                            target.y = 500 * Math.Cos(phi);
                            target.z = 500 * Math.Sin(phi) * Math.Sin(theta);

                            camera.lookAt(target);
                            camera.rotation.z += camera_rotation_z;


                            renderer0.render(scene, camera);
                        };


                };

            draweye(1);
            draweye(0);


            var compassHeadingOffset = 0.0;
            var compassHeadingInitialized = 0;

            #region compassHeading
            // X:\jsc.svn\examples\javascript\android\Test\TestCompassHeading\TestCompassHeading\Application.cs
            Native.window.ondeviceorientation +=
              dataValues =>
              {
                  // Convert degrees to radians
                  var alphaRad = dataValues.alpha * (Math.PI / 180);
                  var betaRad = dataValues.beta * (Math.PI / 180);
                  var gammaRad = dataValues.gamma * (Math.PI / 180);

                  // Calculate equation components
                  var cA = Math.Cos(alphaRad);
                  var sA = Math.Sin(alphaRad);
                  var cB = Math.Cos(betaRad);
                  var sB = Math.Sin(betaRad);
                  var cG = Math.Cos(gammaRad);
                  var sG = Math.Sin(gammaRad);

                  // Calculate A, B, C rotation components
                  var rA = -cA * sG - sA * sB * cG;
                  var rB = -sA * sG + cA * sB * cG;
                  var rC = -cB * cG;

                  // Calculate compass heading
                  var compassHeading = Math.Atan(rA / rB);

                  // Convert from half unit circle to whole unit circle
                  if (rB < 0)
                  {
                      compassHeading += Math.PI;
                  }
                  else if (rA < 0)
                  {
                      compassHeading += 2 * Math.PI;
                  }

                  /*
                  Alternative calculation (replacing lines 99-107 above):

                    var compassHeading = Math.atan2(rA, rB);

                    if(rA < 0) {
                      compassHeading += 2 * Math.PI;
                    }
                  */

                  // Convert radians to degrees
                  compassHeading *= 180 / Math.PI;

                  // Compass heading can only be derived if returned values are 'absolute'

                  // X:\jsc.svn\examples\javascript\android\Test\TestCompassHeadingWithReset\TestCompassHeadingWithReset\Application.cs

                  //Native.document.body.innerText = new { compassHeading }.ToString();
                  if (compassHeadingInitialized > 0)
                  {
                      lon1 = compassHeading - compassHeadingOffset;
                  }
                  else
                  {
                      compassHeadingOffset = compassHeading;
                      compassHeadingInitialized++;
                  }

              };
            #endregion

            #region gamma
            Native.window.ondeviceorientation +=
                //e => Native.body.innerText = new { e.alpha, e.beta, e.gamma }.ToString();
                //e => lon = e.gamma;
                e =>
                {
                    lat1 = e.gamma;

                    // after servicing a running instance would be nice
                    // either by patching or just re running the whole iteration in the backgrou
                    //camera_rotation_z = e.beta * 0.02;
                    camera_rotation_z = e.beta * -0.01;
                };
            #endregion



            #region camera rotation
            var old = new { clientX = 0, clientY = 0 };

            Native.document.body.ontouchstart +=
                e =>
                {
                    var n = new { e.touches[0].clientX, e.touches[0].clientY };
                    old = n;
                };

            Native.document.body.ontouchmove +=
                    e =>
                    {
                        var n = new { e.touches[0].clientX, e.touches[0].clientY };

                        e.preventDefault();

                        lon0 += (n.clientX - old.clientX) * 0.2;
                        lat0 -= (n.clientY - old.clientY) * 0.2;

                        old = n;
                    };


            Native.document.body.onmousemove +=
                e =>
                {
                    e.preventDefault();

                    if (Native.document.pointerLockElement == Native.document.body)
                    {
                        lon0 += e.movementX * 0.1;
                        lat0 -= e.movementY * 0.1;

                        //Console.WriteLine(new { lon, lat, e.movementX, e.movementY });
                    }
                };


            Native.document.body.onmouseup +=
              e =>
              {
                  //drag = false;
                  e.preventDefault();
              };

            Native.document.body.onmousedown +=
                e =>
                {
                    //e.CaptureMouse();

                    //drag = true;
                    e.preventDefault();
                    Native.document.body.requestPointerLock();

                };



            #endregion

        }

    }

    internal class OculusRiftEffectOptions
    {
        internal double[] chromaAbParameter;
        internal double[] distortionK;
        internal double eyeToScreenDistance;
        internal int hResolution;
        internal double hScreenSize;
        internal double interpupillaryDistance;
        internal double lensSeparationDistance;
        internal int vResolution;
        internal double vScreenSize;
    }

    // http://stackoverflow.com/questions/32664/is-there-a-constraint-that-restricts-my-generic-method-to-numeric-types
    class sum //<T>
    {
        public static implicit operator double(sum that)
        {
            return that.i.Sum(x => x());
        }


        Func<double>[] i;
        public sum(params Func<double>[] i)
        {
            this.i = i;
        }

        //public sum(params ref double[] i)
        //{
        //}
    }
}

//..0388:01:01:0f RewriteToAssembly error: System.IO.FileNotFoundException: Could not load file or assembly 'ScriptCoreLib.Async, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependenc
