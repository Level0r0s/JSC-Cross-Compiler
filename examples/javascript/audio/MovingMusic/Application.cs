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
using MovingMusic;
using MovingMusic.Design;
using MovingMusic.HTML.Pages;
using System.Diagnostics;
using MovingMusic.Library;

namespace MovingMusic
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151227/movingmusic

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // THREE.WebGLRenderer 72dev
            // nuget!
            // vs nuget search is brken!
            Console.WriteLine(new { THREE.REVISION } + " back screen until everithing loads!");


            // http://smus.com/ultrasonic-networking/
            // http://chromium.googlecode.com/svn/trunk/samples/audio/shiny-drum-machine.html
            // http://chromium.googlecode.com/svn/trunk/samples/audio/box2d-js/box2d-audio.html
            // http://chromium.googlecode.com/svn/trunk/samples/audio/simple.html
            // http://chromium.googlecode.com/svn/trunk/samples/audio/oscillator-fm2.html
            // http://borismus.github.io/spectrogram/
            // http://borismus.github.io/moving-music/
            // http://smus.com/spatial-audio-web-vr/
            // can we have 360 audio yet?

            // THREE.WebGLObjects: Converting... THREE.PointCloud THREE.BufferGeometry


            new { }.With(
                async delegate
                {
                    //HTML.Audio.FromAssets.

                    // does jsc assetslibrary do jpeg? or do we need jpg/

                    Native.document.body.Clear();
                    Native.document.body.style.margin = "0px";
                    Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;


                    //Error	1	Cannot convert anonymous method to type 'dynamic' because it is not a delegate type	Z:\jsc.svn\examples\javascript\audio\synergy\MovingMusicByBorismus\Application.cs	93	68	MovingMusicByBorismus



                    //                        Choreographer.prototype.getAudioFile = function(set, basename) {
                    //  var extension = Util.isMp3Supported() ? 'mp3' : 'ogg';
                    //  return 'snd/' + set + '/' + basename + '.' + extension;
                    //};



                    // Largely from http://learningthreejs.com/blog/2011/08/15/lets-do-a-sky/

#if VideoRenderer
                        window.VideoRenderer.prototype.addSkybox = IFunction.OfDelegate(
                           new Func<object>(
                               () =>
                               {
                                   // black screen??
                                   //var far = 0x9999;

                                   var far = 0x999;





                                   var sphere = new THREE.Mesh(
                                       new THREE.SphereGeometry(far, 20, 20),
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

                                   // wont help us.
                                   //sphere.material.opacity = 0.5;


                                   Console.WriteLine("addSkybox... " + new { window.video.scene });

                                   sphere.AttachTo((THREE.Scene)window.video.scene);

                                   return null;
                               }
                           )
                       );
#endif





#if Choreographer
                        //window.Choreographer.prototype.initVocal = (Action)
                        //window.Choreographer.prototype.initVocal = IFunction.OfDelegate(
                        //window.Choreographer.prototype.initVocal = IFunction.Of(
                        window.Choreographer.prototype.getAudioFile = IFunction.OfDelegate(
                            new Func<object, string, string>(
                                (object set, string basename) =>
                                {
                                    Console.WriteLine("window.Choreographer.prototype.getAudioFile " + new { basename });

                                    //var russian = new MovingTrack({
                                    //    src: this.getAudioFile(set, 'Russian'),
                                    //    color: 0x19414B,
                                    //  });

                                    // this.manager.addTrack(cats);


                                    //                                    view-source:54442 29925ms window.Choreographer.prototype.getAudioFile { basename = Cats }
                                    //2015-11-16 12:25:32.573 view-source:54442 29927ms window.Choreographer.prototype.getAudioFile { basename = Nimoy }
                                    //2015-11-16 12:25:32.575 view-source:54442 29929ms window.Choreographer.prototype.getAudioFile { basename = Roth }
                                    //2015-11-16 12:25:32.576 view-source:54442 29930ms window.Choreographer.prototype.getAudioFile { basename = Russian }

                                    //if (basename == "Cats") return new HTML.Audio.FromAssets.Sweet_Dreams_My_Love_by_Alexander_J_Turner { }.src;
                                    if (basename == "Cats") return null;

                                    //if (basename == "Cats") return new HTML.Audio.FromAssets.loop_GallinagoDelicata { }.src;
                                    //if (basename == "Nimoy") return new HTML.Audio.FromAssets.sand_run { }.src;
                                    ////if (basename == "Roth") return new HTML.Audio.FromAssets.snd_jeepengine_start { }.src;
                                    //if (basename == "Roth") return new HTML.Audio.FromAssets.heartbeat3 { }.src;

                                    if (basename == "Nimoy") return null;
                                    if (basename == "Roth") return null;

                                    // the green blob.
                                    //return new HTML.Audio.FromAssets.Russian { }.src;
                                    return new HTML.Audio.FromAssets.crickets { }.src;

                                    // yellow is str track man
                                }
                            )
                        );
#endif




                    // X:\opensource\github\moving-music\js\audio-renderer.js


                    // defined at 
                    #region X:\opensource\github\moving-music\js\main.js
                    //window.main();
                    //  start();

                    //  var set = Util.getParameterByName('set');
                    //var mode = Util.getParameterByName('mode');
                    // Create the world.
                    var choreographer = new Choreographer();
                    //manager = choreographer.manager;
                    ////choreographer.on('modechanged', onModeChanged);

                    //// Create a video renderer.
                    //video = new VideoRenderer({selector: 'body', overview: false});
                    //video.setManager(manager);
                    //video.addLight();
                    //video.addSkybox();

                    // Create the audio renderer.
                    var audio = new AudioRenderer();
                    //audio.setManager(manager);
                    //audio.on('ready', onAudioLoaded);



                    // After a little while, if we're not loaded yet, start updating progress.



                    #endregion






                }
            );
        }

    }
}
