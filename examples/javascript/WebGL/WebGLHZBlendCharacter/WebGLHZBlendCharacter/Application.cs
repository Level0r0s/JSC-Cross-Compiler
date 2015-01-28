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
using WebGLHZBlendCharacter;
using WebGLHZBlendCharacter.Design;
using WebGLHZBlendCharacter.HTML.Images.FromAssets;
using WebGLHZBlendCharacter.HTML.Pages;

namespace WebGLHZBlendCharacter
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // Could not connect to the feed specified at 'http://my.jsc-solutions.net/nuget'. P


        public Application(IApp page)
        {
            TexturesImages ref0;

            // http://www.realitymeltdown.com/WebGL3/character-controller.html

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150127
            //Native.css
            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;

            //Error CS0246  The type or namespace name 'THREE' could not be found(are you missing a using directive or an assembly reference?)	WebGLHZBlendCharacter Application.cs  46
            // used by, for?
            var clock = new THREE.Clock();
            //var keys = new { LEFT = 37, UP = 38, RIGHT = 39, DOWN = 40, A = 65, S = 83, D = 68, W = 87 };


            var scene = new THREE.Scene();
            var skyScene = new THREE.Scene();

            scene.fog = new THREE.Fog(0xA26D41, 1000, 20000);

            //scene.add(new THREE.AmbientLight(0xaaaaaa));
            scene.add(new THREE.AmbientLight(0xffffff));

            var lightOffset = new THREE.Vector3(0, 1000, 1000.0);

            var light = new THREE.DirectionalLight(0xffffff, 1.0);
            //var light = new THREE.DirectionalLight(0xffffff, 2.5);
            //var light = new THREE.DirectionalLight(0xffffff, 1.5);
            light.position.copy(lightOffset);
            light.castShadow = true;

            var xlight = light as dynamic;
            xlight.shadowMapWidth = 4096;
            xlight.shadowMapHeight = 2048;

            xlight.shadowDarkness = 0.3;
            //xlight.shadowDarkness = 0.5;

            xlight.shadowCameraNear = 10;
            xlight.shadowCameraFar = 10000;
            xlight.shadowBias = 0.00001;
            xlight.shadowCameraRight = 4000;
            xlight.shadowCameraLeft = -4000;
            xlight.shadowCameraTop = 4000;
            xlight.shadowCameraBottom = -4000;
            //xlight.shadowCameraVisible = true;

            scene.add(light);

            var renderer = new THREE.WebGLRenderer(new { antialias = true, alpha = false });
            renderer.setSize(Native.window.Width, Native.window.Height);
            renderer.autoClear = false;
            renderer.shadowMapEnabled = true;
            renderer.shadowMapType = THREE.PCFSoftShadowMap;

            renderer.domElement.AttachToDocument();

            // this will mess up
            // three.OrbitControls.js
            // onMouseMove

            //new IStyle(renderer.domElement)
            //{
            //    position = IStyle.PositionEnum.absolute,
            //    left = "0px",
            //    top = "0px",
            //    right = "0px",
            //    bottom = "0px",
            //};


            //new { }.With(
            //    async delegate
            //    {

            //        await Native.window.async.onresize;

            //        // if the reload were fast, then we could actually do that:D
            //        Native.document.location.reload();

            //    }
            //);





            #region create field

            // THREE.PlaneGeometry: Consider using THREE.PlaneBufferGeometry for lower memory footprint.
            var planeGeometry = new THREE.PlaneGeometry(1000, 1000);
            //var planeMaterial = new THREE.MeshLambertMaterial(
            //    new
            //    {
            //        //map = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets.dirt_tx().src),
            //        color = 0xA26D41
            //        //color = 0xff0000
            //    }
            //);

            //planeMaterial.map.repeat.x = 300;
            //planeMaterial.map.repeat.y = 300;
            //planeMaterial.map.wrapS = THREE.RepeatWrapping;
            //planeMaterial.map.wrapT = THREE.RepeatWrapping;
            var plane = new THREE.Mesh(planeGeometry,
                    new THREE.MeshPhongMaterial(new { ambient = 0x101010, color = 0xA26D41, specular = 0xA26D41, shininess = 1 })

                );
            plane.castShadow = false;
            plane.receiveShadow = true;


            {

                var parent = new THREE.Object3D();
                parent.add(plane);
                parent.rotation.x = -Math.PI / 2;
                parent.scale.set(100, 100, 100);

                scene.add(parent);
            }

            var random = new Random();
            var meshArray = new List<THREE.Mesh>();
            var geometry = new THREE.CubeGeometry(1, 1, 1);

            for (var i = 1; i < 100; i++)
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
                ii.position.x = i % 2 * 500 - 2.5f;

                // raise it up
                ii.position.y = .5f * 100;
                ii.position.z = -1 * i * 400;
                ii.castShadow = true;
                ii.receiveShadow = true;
                //ii.scale.set(100, 100, 100 * i);
                ii.scale.set(100, 100 * i, 100);


                meshArray.Add(ii);

                scene.add(ii);

            }
            #endregion



            new HeatZeekerRTSOrto.HZCannon().Source.Task.ContinueWithResult(
                cube =>
                {
                    // https://github.com/mrdoob/three.js/issues/1285
                    //cube.children.WithEach(c => c.castShadow = true);

                    cube.traverse(
                        new Action<THREE.Object3D>(
                            child =>
                            {
                                // does it work? do we need it?
                                //if (child is THREE.Mesh)

                                child.castShadow = true;
                                //child.receiveShadow = true;

                            }
                        )
                    );

                    // um can edit and continue insert code going back in time?
                    cube.scale.x = 10.0;
                    cube.scale.y = 10.0;
                    cube.scale.z = 10.0;



                    //cube.castShadow = true;
                    //dae.receiveShadow = true;

                    cube.position.x = -100;
                    ////cube.position.y = (cube.scale.y * 50) / 2;
                    //cube.position.z = Math.Floor((random() * 1000 - 500) / 50) * 50 + 25;



                    // if i want to rotate, how do I do it?
                    //cube.rotation.z = random() + Math.PI;
                    //cube.rotation.x = random() + Math.PI;
                    cube.rotation.y = new Random().NextDouble() * Math.PI * 2;



                    scene.add(cube);
                    //interactiveObjects.Add(cube);
                }
            );


            var blendMesh = new THREE.SpeedBlendCharacter();


            blendMesh.load(
                new Models.marine_anims().Content.src,
                new Action(
                    delegate
                    {
                        // buildScene
                        //blendMesh.rotation.y = Math.PI * -135 / 180;
                        blendMesh.castShadow = true;
                        // we cannot scale down we want our shadows
                        //blendMesh.scale.set(0.1, 0.1, 0.1);

                        scene.add(blendMesh);

                        //var characterController = new THREE.CharacterController(blendMesh);

                        var xtrue = true;
                        // run
                        blendMesh.setSpeed(1.0);
                        blendMesh.showSkeleton(!xtrue);

                        var radius = blendMesh.geometry.boundingSphere.radius;


                        Native.document.title = new { radius }.ToString();


                        var camera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        camera.position.set(0.0, radius * 3, radius * 3.5);

                        var skyCamera = new THREE.PerspectiveCamera(45, Native.window.aspect, 1, 20000);
                        skyCamera.position.set(0.0, radius * 3, radius * 3.5);

                        var controls = new THREE.OrbitControls(camera);
                        //controls.noPan = true;


                        //var loader = new THREE.JSONLoader();
                        //loader.load(new Models.ground().Content.src,
                        //    new Action<THREE.Geometry, THREE.Material[]>(

                        //    (xgeometry, materials) =>
                        //    {

                        //        var ground = new THREE.Mesh(xgeometry, materials[0]);
                        //        ground.scale.set(20, 20, 20);
                        //        ground.receiveShadow = true;
                        //        ground.castShadow = true;
                        //        scene.add(ground);

                        //    }
                        //    )
                        // );

                        #region  createSky

                        var urls = new[] {
                            new px().src, new nx().src,
                            new py().src, new ny().src,
                            new pz().src, new nz().src,
                         };

                        var textureCube = THREE.ImageUtils.loadTextureCube(urls);

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
                        var skyMesh = new THREE.Mesh(new THREE.CubeGeometry(10000, 10000, 10000, 1, 1, 1), material);
                        //skyMesh.renderDepth = -10;


                        skyScene.add(skyMesh);
                        #endregion

                        #region onframe
                        Native.window.onframe +=
                            delegate
                            {
                                var scale = 1.0;
                                var delta = clock.getDelta();
                                var stepSize = delta * scale;

                                if (stepSize > 0)
                                {
                                    //characterController.update(stepSize, scale);
                                    //gui.setSpeed(blendMesh.speed);

                                    THREE.AnimationHandler.update(stepSize);
                                    blendMesh.updateSkeletonHelper();
                                }

                                controls.center.copy(blendMesh.position);
                                controls.center.y += radius * 2.0;
                                controls.update();

                                var camOffset = camera.position.clone().sub(controls.center);
                                camOffset.normalize().multiplyScalar(750);
                                camera.position = controls.center.clone().add(camOffset);

                                skyCamera.rotation.copy(camera.rotation);



                                renderer.clear();
                                renderer.render(skyScene, skyCamera);
                                renderer.render(scene, camera);
                            };
                        #endregion

                        #region AtResize
                        Action AtResize = delegate
                        {
                            //renderer.domElement.style.SetLocation(0, 0, Native.window.Width, Native.window.Height);

                            //camera.projectionMatrix.makePerspective(fov, Native.window.aspect, 1, 1100);

                            camera.aspect = Native.window.aspect;
                            camera.updateProjectionMatrix();
                            renderer.setSize(Native.window.Width, Native.window.Height);
                        };

                        Native.window.onresize +=
                            delegate
                            {
                                AtResize();
                            };

                        AtResize();
                        #endregion
                    }
                )
           );




        }

    }
}