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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebGLStereoCubeMap;
using WebGLStereoCubeMap.Design;
using WebGLStereoCubeMap.HTML.Images.FromAssets;
using WebGLStereoCubeMap.HTML.Pages;

namespace WebGLStereoCubeMap
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.Clear();

            new IHTMLPre { "loading stereo... 2MB!" }.AttachToDocument();

            // if this is a chrome app
            // would GearVR 360 app be able to request current stereo cubemap?









            //var iii = new IHTMLImage[] {
            //            new px(), new nx(),
            //            new py(), new ny(),
            //            new pz(), new nz()
            //        };


            //var f12 = await new airplane().async.oncomplete;
            //var f12 = new airplane();



            //Task.WhenAll(from x in iii select x.async.oncomplete).ContinueWithResult(
            new airplane().async.oncomplete.ContinueWithResult(
               f12 =>
               {
                   Func<int, IHTMLImage> f = i =>
                   {
                       // we do have a skybox example somewhere...
                       var f1 = new CanvasRenderingContext2D(w: f12.height, h: f12.height);


                       // can we keep animating the stereo ?

                       // if we return canvas it gets messed up. why?
                       // looks to  be a bug?

                       //var stale = new IHTMLImage();

                       //activateeye += eye =>
                       //{
                       //    if (eye == 0)
                       //    {
                       // GearVR would have both eyes!
                       // laptop has to flip between eyes to give similar effect?
                       // if this were a chrome app. could gearvr request the frames into the photos360 app?

                       f1.drawImage(f12, i * f12.height, 0, sw: f12.height, sh: f12.height,
                       dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                       // whenever we call drawImage ? callsite event monitoring?
                       // this seems to be slow
                       //stale.src = f1.canvas.toDataURL();

                       // can we have a synchronized frame choreo?
                       //        //await Task.Delay(1000 / 15);
                       //    }
                       //    else
                       //    {
                       //        //await frame0;

                       //        // update!

                       //        f1.drawImage(f12, (i + 6) * f12.height, 0, sw: f12.height, sh: f12.height,
                       //        dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                       //        //stale.src = f1.canvas.toDataURL();

                       //        //await frame1;
                       //        //await Task.Delay(1000 / 15);

                       //    };
                       //};

                       return f1;
                   };


                   var skyScene0 = new THREE.Scene();
                   var skyScene1 = new THREE.Scene();


                   #region  createSky
                   // gearvr has photos360 app



                   //var textureCube = THREE.ImageUtils.loadTextureCube(urls);


                   var cube0 = new
                   {
                       vertexShader =
            @"
varying vec3 vWorldPosition;
            "
            + THREE.ShaderChunk["logdepthbuf_pars_vertex"]
            +
            @"
void main() {
vec4 worldPosition = modelMatrix * vec4( position, 1.0 ); 
vWorldPosition = worldPosition.xyz;
gl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );
"
    + THREE.ShaderChunk["logdepthbuf_vertex"]
    + @"
}
"

        ,

                       fragmentShader =
                @"
uniform samplerCube tCube; 
uniform float tFlip; 
varying vec3 vWorldPosition;
" + THREE.ShaderChunk["logdepthbuf_pars_fragment"] + @"
            void main() {
gl_FragColor = textureCube( tCube, vec3( tFlip * vWorldPosition.x, vWorldPosition.yz ) );
" + THREE.ShaderChunk["logdepthbuf_fragment"] + @"
}
"
                   };


                   //dynamic shader = THREE.ShaderLib["cube"];
                   //shader.uniforms["tCube"].value = textureCube;


                   // We're inside the box, so make sure to render the backsides
                   // It will typically be rendered first in the scene and without depth so anything else will be drawn in front

                   // stereo vs mono skybox
                   var skyMeshmaterial0 = new THREE.ShaderMaterial(new
                   {
                       fragmentShader = cube0.fragmentShader,
                       vertexShader = cube0.vertexShader,
                       uniforms = new
                       {
                           tCube = new
                           {
                               type = "t",
                               value = new THREE.CubeTexture(
                                 f(0), f(1),
                                f(2), f(3),
                                f(4), f(5)
                               )
                               {
                                   flipY = false,

                                   // !!
                                   needsUpdate = true
                               }
                           },
                           tFlip = new { type = "f", value = -1 }
                       },
                       depthWrite = false,
                       side = THREE.BackSide
                   });

                   skyScene0.add(new THREE.Mesh(new THREE.BoxGeometry(10000, 10000, 10000), skyMeshmaterial0));


                   var skyMeshmaterial1 = new THREE.ShaderMaterial(new
                   {
                       fragmentShader = cube0.fragmentShader,
                       vertexShader = cube0.vertexShader,
                       uniforms = new
                       {
                           tCube = new
                           {
                               type = "t",
                               value = new THREE.CubeTexture(
                                f(0 + 6), f(1 + 6),
                               f(2 + 6), f(3 + 6),
                               f(4 + 6), f(5 + 6)
                              )
                               {
                                   flipY = false,

                                   // !!
                                   needsUpdate = true
                               }
                           },
                           tFlip = new { type = "f", value = -1 }
                       },
                       depthWrite = false,
                       side = THREE.BackSide
                   });

                   skyScene1.add(new THREE.Mesh(new THREE.BoxGeometry(10000, 10000, 10000), skyMeshmaterial1));
                   #endregion


                   Native.body.Clear();

                   var skyCamera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                   //skyCamera.position.set(0.0, radius * 3, radius * 3.5);





                   var renderer = new THREE.WebGLRenderer(new { antialias = true, alpha = false });
                   renderer.setSize(Native.window.Width, Native.window.Height);
                   renderer.autoClear = false;
                   renderer.shadowMapEnabled = true;
                   renderer.shadowMapType = THREE.PCFSoftShadowMap;

                   renderer.domElement.AttachToDocument();


                   #region onresize
                   new { }.With(
                        async delegate
                        {
                            do
                            {
                                skyCamera.aspect = Native.window.aspect;
                                skyCamera.updateProjectionMatrix();
                                renderer.setSize(Native.window.Width, Native.window.Height);

                            } while (await Native.window.async.onresize);
                        }
                    );
                   #endregion

                   Native.document.body.onmousewheel +=
                      e =>
                      {
                          skyCamera.fov -= e.WheelDirection * 5.0;
                          skyCamera.updateProjectionMatrix();
                      };



                   var target = new THREE.Vector3();

                   var lon = 90.0;
                   var lat = 0.0;
                   var phi = 0.0;
                   var theta = 0.0;





                   var drag = false;


                   var sw = Stopwatch.StartNew();

                   Native.window.onframe +=
                        delegate
                        {
                            //var eye = (sw.ElapsedMilliseconds / (200)) % 2;
                            //var eye = (sw.ElapsedMilliseconds / (1000 / 15)) % 2;
                            var eye = (sw.ElapsedMilliseconds / (1000 / 30)) % 2;
                            //  if we are a multiprocess renderer, get the volatile eye id 

                            Native.document.title = "" + new { eye };

                            // doesnt work?
                            //skyMesh0.visible = eye == 0;


                            if (Native.document.pointerLockElement == Native.document.body)
                                lon += 0.00;
                            else
                                lon += 0.01;

                            lat = Math.Max(-85, Math.Min(85, lat));

                            //Native.document.title = new { lon, lat }.ToString();


                            phi = THREE.Math.degToRad(90 - lat);
                            theta = THREE.Math.degToRad(lon);

                            target.x = Math.Sin(phi) * Math.Cos(theta);
                            target.y = Math.Cos(phi);
                            target.z = Math.Sin(phi) * Math.Sin(theta);

                            skyCamera.lookAt(target);

                            renderer.clear();

                            if (eye == 0)
                                renderer.render(skyScene0, skyCamera);
                            else
                                renderer.render(skyScene1, skyCamera);

                        };

                   #region ontouchmove
                   var touchX = 0;
                   var touchY = 0;

                   Native.document.body.ontouchstart +=
                                          e =>
                                          {
                                              e.preventDefault();

                                              var touch = e.touches[0];

                                              touchX = touch.screenX;
                                              touchY = touch.screenY;

                                          };


                   Native.document.body.ontouchmove +=
                     e =>
                     {
                         e.preventDefault();

                         var touch = e.touches[0];

                         lon -= (touch.screenX - touchX) * 0.1;
                         lat += (touch.screenY - touchY) * 0.1;

                         touchX = touch.screenX;
                         touchY = touch.screenY;

                     };
                   #endregion






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
                         drag = false;
                         e.preventDefault();
                     };

                   Native.document.body.onmousedown +=
                       e =>
                       {
                           //e.CaptureMouse();

                           drag = true;
                           e.preventDefault();
                           Native.document.body.requestPointerLock();

                       };


                   Native.document.body.ondblclick +=
                       e =>
                       {
                           e.preventDefault();

                           Console.WriteLine("requestPointerLock");
                       };

                   #endregion



               }
           );

        }

    }
}

//020000af WebGLStereoCubeMap.Application+<>c+<<-ctor>b__0_0>d+<MoveNext>0600002a
//script: error JSC1000:
//error:
//  statement cannot be a load instruction (or is it a bug?)
//  [0x0000]
//ldarg.0    +1 -0

// assembly: V:\WebGLStereoCubeMap.Application.exe
// type: WebGLStereoCubeMap.Application+<>c+<<-ctor>b__0_0>d+<MoveNext>0600002a, WebGLStereoCu
// offset: 0x0000
//  method:Int32<030c> ldsfld.try(<MoveNext>0600002a, System.Runtime.CompilerServices.TaskAwa
