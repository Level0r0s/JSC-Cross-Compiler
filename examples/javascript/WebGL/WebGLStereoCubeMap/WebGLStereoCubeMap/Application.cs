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
            // https://www.reddit.com/r/GearVR/comments/35g8w7/real_world_stereoscopic_360_panoramas/
            // https://www.reddit.com/r/oculus/comments/35gcn2/real_world_stereoscopic_panoramas_with_gear_vr/
            // http://stackoverflow.com/questions/9032050/canvas-mask-an-image-and-preserve-its-alpha-channel
            // https://3dwarehouse.sketchup.com/model.html?id=48bcce07b0baf689d9e6f00e848ea18
            // https://www.youtube.com/watch?v=OurzBO1cDto

            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.Clear();

            new IHTMLPre { "loading stereo... 2MB!" }.AttachToDocument();

            // if this is a chrome app
            // would GearVR 360 app be able to request current stereo cubemap?






            // Uncaught TypeError: Cannot read property '0' of undefined
            var keys = new int[0xffff];

            //c = a[0].keys[jAEABqtXvT6n4h6m6ZavdA(b)];
            //d = c[0];
            //c[0] = (((d + 1)));



            Native.document.body.onkeyup +=
             e =>
             {

                 var z = keys[e.KeyCode];

                 if (z > 0)
                     z++;
                 else
                     z = 1;

                 keys[e.KeyCode] = z;

                 // 4333ms {{ KeyCode = 83, S = 83, z = NaN }}
                 Console.WriteLine(new { e.KeyCode, System.Windows.Forms.Keys.S, z });

             };



            var iii = new IHTMLImage[] {
                        new px(), new nx(),
                        new py(), new ny(),
                        new pz(), new nz()
                    };


            //var f12 = await new airplane().async.oncomplete;
            //var f12 = new airplane();



            Task.WhenAll(from x in iii select x.async.oncomplete).ContinueWithResult(
                ii =>



               // how can we do an alpha clear on the jpg?
               //new airplane().async.oncomplete.ContinueWithResult(
               //new airplane_leftwindows().async.oncomplete.ContinueWithResult(
               new airplane_mask().async.oncomplete.ContinueWithResult(
               f12_mask =>
               new airplane().async.oncomplete.ContinueWithResult(
               f12 =>
               {
                   //Func<int, string, IHTMLImage> xf = (i, globalCompositeOperation) =>
                   //{
                   //    // we do have a skybox example somewhere...
                   //    var f1 = new CanvasRenderingContext2D(w: f12.height, h: f12.height);

                   //    f1.drawImage(f12, i * f12.height, 0, sw: f12.height, sh: f12.height, dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                   //    // https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/globalCompositeOperation
                   //    f1.globalCompositeOperation = globalCompositeOperation;

                   //    f1.drawImage(f12_mask, i * f12.height, 0, sw: f12.height, sh: f12.height, dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                   //    return f1;
                   //};


                   //new[] {
                   //    "source-over",
                   //    "source-in",
                   //    "source-out",
                   //    "source-atop",
                   //    "destination-over",
                   //    "destination-in",
                   //    "destination-out",
                   //    "destination-atop",
                   //    "lighter",
                   //    "copy",
                   //    "xor",
                   //    "multiply",
                   //    "screen",
                   //    "overlay",
                   //    "darken",
                   //    "lighten",
                   //    "color-dodge",
                   //    "color-burn",
                   //    "hard-light",
                   //    "soft-light",
                   //    "difference",
                   //    "exclusion",
                   //    "hue",
                   //    "saturation",
                   //    "color",
                   //    "luminosity"
                   //}.WithEach(globalCompositeOperation =>
                   //    {

                   //        xf(4, globalCompositeOperation).AttachToDocument().title = globalCompositeOperation;
                   //    }
                   //);


                   Func<int, IHTMLImage> f = i =>
                   {
                       // we do have a skybox example somewhere...
                       var f1 = new CanvasRenderingContext2D(w: f12.height, h: f12.height);

                       f1.drawImage(f12, i * f12.height, 0, sw: f12.height, sh: f12.height, dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                       // https://developer.mozilla.org/en-US/docs/Web/API/CanvasRenderingContext2D/globalCompositeOperation
                       f1.globalCompositeOperation = "multiply";

                       f1.drawImage(f12_mask, i * f12.height, 0, sw: f12.height, sh: f12.height, dx: 0, dy: 0, dw: f12.height, dh: f12.height);

                       return f1;
                   };



                   var skyScene0 = new THREE.Scene();
                   var skyScene1 = new THREE.Scene();
                   var skyScene2 = new THREE.Scene { };


                   #region  createSky
                   // gearvr has photos360 app



                   //var textureCube = THREE.ImageUtils.loadTextureCube(urls);


                   var vertexShader =
            @"
varying vec3 vWorldPosition;
            "
            //+ THREE.ShaderChunk["logdepthbuf_pars_vertex"]
            +
            @"
void main() {
vec4 worldPosition = modelMatrix * vec4( position, 1.0 ); 
vWorldPosition = worldPosition.xyz;
gl_Position = projectionMatrix * modelViewMatrix * vec4( position, 1.0 );
"
    //+ THREE.ShaderChunk["logdepthbuf_vertex"]
    + @"
}
"

        ;
                   // https://www.opengl.org/sdk/docs/tutorials/ClockworkCoders/texturing.php

                   var fragmentShader =
            @"
uniform samplerCube tCube; 
uniform float tFlip; 
varying vec3 vWorldPosition;
                   "
    //+ THREE.ShaderChunk["logdepthbuf_pars_fragment"] 
    + @"
void main() 
{

vec4 c = textureCube( tCube, vec3( tFlip * vWorldPosition.x, vWorldPosition.yz ) );
"

    // : gl.getShaderInfoLog() ERROR: 0:52: 'assign' :  cannot convert from 'const int' to 'highp float'

    //+ THREE.ShaderChunk["logdepthbuf_fragment"] 
    + @"

// _mask.png has the bits
if (c.r == 0.0) if (c.g == 0.0) if (c.b == 0.0) discard;

gl_FragColor = c;

}
"
               // vec4 textureCube(samplerCube s, vec3 coord [, float bias])
               // 
               ;



                   var skyMeshmaterial0 = new THREE.ShaderMaterial(new
                   {
                       fragmentShader = fragmentShader,
                       vertexShader = vertexShader,
                       uniforms = new
                       {
                           tCube = new
                           {
                               type = "t",
                               value = new THREE.CubeTexture(ii
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

                   var mesh0 = new THREE.Mesh(new THREE.BoxGeometry(10000, 10000, 10000), skyMeshmaterial0).AttachTo(skyScene0);



                   // stereo vs mono skybox
                   var skyMeshmaterial1 = new THREE.ShaderMaterial(new
                   {
                       fragmentShader = fragmentShader,
                       vertexShader = vertexShader,
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
                       side = THREE.BackSide,

                   });

                   var mesh1 = new THREE.Mesh(new THREE.BoxGeometry(10000, 10000, 10000), skyMeshmaterial1).AttachTo(skyScene1);


                   var skyMeshmaterial2 = new THREE.ShaderMaterial(new
                   {
                       fragmentShader = fragmentShader,
                       vertexShader = vertexShader,
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

                   var mesh2 = new THREE.Mesh(new THREE.BoxGeometry(10000, 10000, 10000), skyMeshmaterial2).AttachTo(skyScene2);
                   #endregion


                   Native.body.Clear();

                   var skyCamera = new THREE.PerspectiveCamera(90, Native.window.aspect, 1, 20000);
                   //var skyCamera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);

                   var renderer = new THREE.WebGLRenderer(new { antialias = true, alpha = false })
                   {
                       autoClear = false,
                       shadowMapEnabled = true,
                       shadowMapType = THREE.PCFSoftShadowMap
                   };

                   renderer.setSize(Native.window.Width, Native.window.Height);
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



                   var target0 = new THREE.Vector3();

                   var lon = 90.0;
                   var lat = 0.0;
                   var phi = 0.0;
                   var theta = 0.0;





                   var drag = false;

                   var sw = Stopwatch.StartNew();

                   Native.window.onframe +=
                        delegate
                        {
                            var sinfps = 1 + (int)(((Math.Sin(sw.ElapsedMilliseconds * 0.0001) + 1.0) / 2.0) * 59);
                            Native.document.title = "" + new { sinfps };


                            //var eye = (sw.ElapsedMilliseconds / (200)) % 2;
                            //var eye = (sw.ElapsedMilliseconds / (1000 / 15)) % 2;
                            //var eye = (sw.ElapsedMilliseconds / (1000 / 30)) % 2;
                            //var eye = (sw.ElapsedMilliseconds / (1000 / 45)) % 2;
                            //var eye = (sw.ElapsedMilliseconds / (1000 / 60)) % 2;
                            var eye = (sw.ElapsedMilliseconds / (1000 / sinfps)) % 2;
                            //  if we are a multiprocess renderer, get the volatile eye id 


                            // doesnt work?
                            //skyMesh0.visible = eye == 0;

                            //mesh0.rotation = new THREE.Vector3(0, 0, sw.ElapsedMilliseconds);


                            if (Native.document.pointerLockElement == Native.document.body)
                                lon += 0.00;
                            else
                            {

                                lon += 0.01;
                            }

                            lat = Math.Max(-85, Math.Min(85, lat));


                            // this is wrong, but gets the idea across.
                            phi = THREE.Math.degToRad(90 - lat) + Math.Sin(sw.ElapsedMilliseconds * 0.001) * 0.1 - 0.3;
                            theta = THREE.Math.degToRad(lon);

                            target0.x = Math.Sin(phi) * Math.Cos(theta);
                            target0.y = Math.Cos(phi);
                            target0.z = Math.Sin(phi) * Math.Sin(theta);

                            skyCamera.lookAt(target0);


                            renderer.clear();

                            renderer.render(skyScene0, skyCamera);


                            phi = THREE.Math.degToRad(90 - lat);
                            theta = THREE.Math.degToRad(lon);

                            target0.x = Math.Sin(phi) * Math.Cos(theta);
                            target0.y = Math.Cos(phi);
                            target0.z = Math.Sin(phi) * Math.Sin(theta);

                            skyCamera.lookAt(target0);

                            if ((keys[(int)System.Windows.Forms.Keys.S] % 3) == 0)
                            {

                                if (eye == 0)
                                    renderer.render(skyScene1, skyCamera);
                                else
                                    renderer.render(skyScene2, skyCamera);
                            }
                            else if ((keys[(int)System.Windows.Forms.Keys.S] % 3) == 1)
                            {
                                renderer.render(skyScene1, skyCamera);
                            }
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
           )
           )
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
