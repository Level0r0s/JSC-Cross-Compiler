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

            // if this is a chrome app
            // would GearVR 360 app be able to request current stereo cubemap?


            var iii = new IHTMLImage[] {
                new px(), new nx(),
                new py(), new ny(),
                new pz(), new nz()
            };

            

            Task.WhenAll(from x in iii select x.async.oncomplete).ContinueWithResult(
                ii =>
                {

                    var skyScene = new THREE.Scene();


                    #region  createSky
                    // gearvr has photos360 app



                    var textureCube = new THREE.CubeTexture(ii)
                    {
                        flipY = false,

                        // !!
                        needsUpdate = true
                    };

                    //var textureCube = THREE.ImageUtils.loadTextureCube(urls);

                    dynamic shader = THREE.ShaderLib["cube"];
                    shader.uniforms["tCube"].value = textureCube;

                    // We're inside the box, so make sure to render the backsides
                    // It will typically be rendered first in the scene and without depth so anything else will be drawn in front
                    var material = new THREE.ShaderMaterial(new
                    {
                        fragmentShader = shader.fragmentShader,
                        vertexShader = shader.vertexShader,
                        uniforms = shader.uniforms,
                        depthWrite = false,
                        side = THREE.BackSide
                    });

                    // THREE.CubeGeometry has been renamed to THREE.BoxGeometry
                    // The box dimension size doesn't matter that much when the camera is in the center.  Experiment with the values.
                    //var skyMesh = new THREE.Mesh(new THREE.CubeGeometry(10000, 10000, 10000, 1, 1, 1), material);
                    var skyMesh = new THREE.Mesh(new THREE.BoxGeometry(10000, 10000, 10000), material);
                    //skyMesh.renderDepth = -10;


                    skyScene.add(skyMesh);
                    #endregion



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


                    Native.window.onframe +=
                        delegate
                        {
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

                            renderer.render(skyScene, skyCamera);

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
