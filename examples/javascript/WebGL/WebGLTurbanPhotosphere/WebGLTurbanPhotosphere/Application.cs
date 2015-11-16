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
using WebGLTurbanPhotosphere;
using WebGLTurbanPhotosphere.Design;
using WebGLTurbanPhotosphere.HTML.Pages;


//using static THREE;
//using static ScriptCoreLib.JavaScript.Native;

namespace WebGLTurbanPhotosphere
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
            // http://stackoverflow.com/questions/29048161/how-to-export-a-three-js-scene-into-a-360-texture-for-photosphere

            Native.body.style.background = "black";

            Native.body.style.margin = "0px";
            Native.body.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.Clear();



            // https://github.com/turban/photosphere/blob/gh-pages/stolanuten.html

            var scene = new THREE.Scene();


            var renderer = new THREE.WebGLRenderer();
            renderer.setSize(Native.window.Width, Native.window.Height);
            // the thing you attach to dom
            renderer.domElement.AttachToDocument();


            // Z:\jsc.svn\examples\javascript\audio\synergy\MovingMusicByBorismus\Application.cs

            var sphere = new THREE.Mesh(
                new THREE.SphereGeometry(100, 20, 20),
                new THREE.MeshBasicMaterial(
                    new
                    {
                        //20150608_165300.jpg
                        //map = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets.stolanuten().src)
                        map = THREE.ImageUtils.loadTexture(new HTML.Images.FromAssets._20150608_165300().src)
                    }
                )
            );
            sphere.scale.x = -1;
            sphere.AttachTo(scene);

            var camera = new THREE.PerspectiveCamera(75, Native.window.aspect, 1, 1000);
            camera.position.x = 0.1;

            var controls = new THREE.OrbitControls(camera, renderer.domElement);






            Native.window.onframe +=
                delegate
                {
                    controls.update();
                    camera.position = controls.center.clone();

                    renderer.render(scene, camera);


                };



            Native.window.onresize +=
              delegate
              {
                  camera.aspect = Native.window.aspect;
                  camera.updateProjectionMatrix();

                  renderer.setSize(Native.window.Width, Native.window.Height);

              };

            // http://www.visualstudio.com/en-us/news/vs2015-vs
        }

    }
}
