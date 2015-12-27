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
using MovingMusicByBorismus;
using MovingMusicByBorismus.Design;
using MovingMusicByBorismus.HTML.Pages;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.WebAudio;

namespace MovingMusicByBorismus
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151227/x360stereomoon

        // chrome cannot copy url to clipboard! 

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            // THREE.WebGLRenderer 72dev
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


                    var w = Stopwatch.StartNew();
                    Console.WriteLine("awaiting for main()...");

                    dynamic window = Native.window;


                    // X:\opensource\github\moving-music\js\audio-renderer.js
                    // why do we need this?
                    window.forest_impulse_response = new HTML.Audio.FromAssets.forest_impulse_response { }.src;
                    window.VideoRenderer_particle = new HTML.Images.FromAssets.particle { }.src;

                    // X:\opensource\github\moving-music\js\video-renderer.js
                    //var cubemapurls = new[] {
                    //    // 'posx.jpeg', 'negx.jpeg', 'posy.jpeg', 'negy.jpeg', 'posz.jpeg', 'negz.jpeg']

                    //    new HTML.Images.FromAssets.px { }.src,
                    //    new HTML.Images.FromAssets.nx { }.src,
                    //    new HTML.Images.FromAssets.py { }.src,
                    //    new HTML.Images.FromAssets.ny { }.src,
                    //    new HTML.Images.FromAssets.pz { }.src,
                    //    new HTML.Images.FromAssets.nz { }.src,
                    //};

                    //window.cubemapurls = cubemapurls;

                    new HTML.Pages.References { }.AttachToDocument();

                    while (window.main == null)
                    {
                        await Task.Delay(1500);
                        Console.WriteLine("awaiting for main()... " + new { w.ElapsedMilliseconds });
                    }


                    Console.WriteLine("awaiting for main()... " + new { window.main });



                    // escape statemachine stack
                    Native.window.requestAnimationFrame += delegate
                    {
                        //Error	1	Cannot convert anonymous method to type 'dynamic' because it is not a delegate type	Z:\jsc.svn\examples\javascript\audio\synergy\MovingMusicByBorismus\Application.cs	93	68	MovingMusicByBorismus



                        //                        Choreographer.prototype.getAudioFile = function(set, basename) {
                        //  var extension = Util.isMp3Supported() ? 'mp3' : 'ogg';
                        //  return 'snd/' + set + '/' + basename + '.' + extension;
                        //};



                        // Largely from http://learningthreejs.com/blog/2011/08/15/lets-do-a-sky/

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

                                    if (basename == "Cats") return new HTML.Audio.FromAssets.Sweet_Dreams_My_Love_by_Alexander_J_Turner { }.src;
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

                        // X:\opensource\github\moving-music\js\audio-renderer.js


                        window.main();
                    };


                    // cant reset the system. need to rewrite it .
                    #region ondropfile
                    var f = await Native.document.documentElement.async.ondropfile;
                    IHTMLAudio a = f;
                    await a.async.onloadeddata;

                    // Uncaught TypeError: track.update is not a function
                    window.isLoaded = false;
                    Console.WriteLine(new { f.name, f.size, window.manager.trackCount, window.isLoaded });

                    // :4822/view-source:54442 39610ms { name = Sweet Dreams My Love by Alexander_J_Turner.mp3, size = 29790804, trackCount = 4 }

                    //for (int i = 0; i < window.manager.trackCount; i++)


                    foreach (var id in Expando.InternalGetMemberNames((object)window.manager.tracks))
                    {
                        Console.WriteLine(new { id });

                        //window.manager.tracs[item].setAmplitude(0);

                        // https://developer.mozilla.org/en-US/docs/Web/API/AudioContext/createPanner
                        PannerNode panner = window.audio.panners[id];

                        // Failed to set the 'buffer' property on 'AudioBufferSourceNode': The provided value is not of type 'AudioBuffer'.
                        panner.disconnect();

                        THREE.Object3D t = window.video.trackObjects[id];

                        t.parent.remove(t);
                    }




                    window.manager.trackCount = 0;
                    //window.manager.tracks = new { };
                    window.manager.tracks = new object();

                    // vsync
                    await Native.window.async.onframe;

                    var MovingTrack = new IFunction("url", "return  new MovingTrack({ src: url, color: 0x19414B});");




                    dynamic track = MovingTrack.apply(null, a.src);


                    Console.WriteLine("addTrack");
                    window.manager.addTrack(track);



                    Console.WriteLine("loadTrack_");
                    window.audio.loadTrack_(track.id);


                    Func<bool> isready = () => window.audio.ready[track.id];

                    var ready = new TaskCompletionSource<object> { };

                    new { }.With(
                        async delegate
                        {
                            while (!ready.Task.IsCompleted)
                            {
                                await Task.Delay(300);

                                bool xready = isready();

                                Console.WriteLine(new { track.id, xready });

                                if (xready)
                                    ready.SetResult(null);
                            }
                        }
                    );

                    await ready.Task;


                    Console.WriteLine("start");
                    window.audio.start();

                    window.isLoaded = true;

                    //view-source:54481 28681ms { id = b674a511-0580-7000-24ba-02bca7b09197, xready = true }
                    //2015-11-16 17:59:14.044 view-source:54481 28686ms start
                    //2015-11-16 17:59:14.051 view-source:54481 28693ms addPointCloud

                    {
                        Console.WriteLine("addPointCloud " + new { window.choreographer, window.isLoaded });

                        var t = window.video.addPointCloud(new { color = track.color });
                        window.video.trackObjects[track.id] = t;
                    }

                    Console.WriteLine("setMode " + new { window.choreographer.mode_ });
                    window.choreographer.setMode(window.choreographer.mode_);

                    #endregion




                }
            );

            // 
            // window.addEventListener('DOMContentLoaded', main);

        }

        //static Application()
        //{
        //    // 
        //    // window.addEventListener('DOMContentLoaded', main);
        //    new HTML.Pages.References { }.AttachToDocument();
        //}
    }
}

// can we override referenced code like this? like redux?
class Choreographer
{
    // override
    public void initVocal()
    {

    }
}



// lets patch it out

//function onVisibilityChange(e) {
//  if (!document.hidden) {
//    // Play the sounds when page becomes visible.
//    audio.setMute(false);
//  } else {
//    // Pause them when the page is hidden.
//    audio.setMute(true);
//  }
//}

//window.addEventListener('DOMContentLoaded', main);
//window.addEventListener('touchstart', onTouchStart);
//window.addEventListener('keydown', onKeyDown);
//document.addEventListener('visibilitychange', onVisibilityChange);
