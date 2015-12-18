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
using StereoChannelVisualization;
using StereoChannelVisualization.Design;
using StereoChannelVisualization.HTML.Pages;
using ScriptCoreLib.JavaScript.WebAudio;
using System.Net;
using ScriptCoreLib.JavaScript.WebGL;
using System.Diagnostics;

namespace StereoChannelVisualization
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
            // http://www.smartjava.org/examples/webaudio/example2.html


            //var audioBuffer;
            //var sourceNode;
            //var splitter;
            //var analyser, analyser2;
            //var javascriptNode;

            // get the context from the canvas to draw on
            //var ctx = $("#canvas").get()[0].getContext("2d");


            new { }.With(
                async delegate
                {

                    var ctx = new CanvasRenderingContext2D { };

                    ctx.canvas.AttachToDocument();


                    // create a gradient for the fill. Note the strange
                    // offset, since the gradient is calculated based on
                    // the canvas, not the specific element we draw


                    var context = new AudioContext();


                    // setup a javascript node
                    var javascriptNode = context.createScriptProcessor(2048, 1, 1);
                    // connect to destination, else it isn't called
                    javascriptNode.connect(context.destination);


                    // setup a analyzer
                    var analyser = context.createAnalyser();
                    analyser.smoothingTimeConstant = 0.3;
                    analyser.fftSize = 1024;

                    var analyser2 = context.createAnalyser();
                    analyser2.smoothingTimeConstant = 0.0;
                    analyser2.fftSize = 1024;

                    // create a buffer source node
                    var sourceNode = context.createBufferSource();
                    var splitter = context.createChannelSplitter();

                    // connect the source to the analyser and the splitter
                    sourceNode.connect(splitter);

                    // connect one of the outputs from the splitter to
                    // the analyser
                    splitter.connect(analyser, 0, 0);
                    splitter.connect(analyser2, 1, 0);

                    // connect the splitter to the javascriptnode
                    // we use the javascript node to draw at a
                    // specific interval.
                    analyser.connect(javascriptNode);

                    //        splitter.connect(context.destination,0,0);
                    //        splitter.connect(context.destination,0,1);

                    // and connect to destination
                    sourceNode.connect(context.destination);
                    // load the specified sound


                    // jsc doesnt like audio assets on root?
                    //{ src_value0 = /RookConfirmCommandStefanWeDontLiveOnAPlanet.mp3 }
                    //13d4:02:01:1c RewriteToAssembly error: System.InvalidOperationException: Referenced asset not found in the project. Did you forget to include it? - /RookConfirmCommandStefanWeDontLiveOnAPlanet.mp3

                    var buffer = await new WebClient().DownloadDataTaskAsync(
                        //new RoosterAudioExample.HTML.Audio.FromAssets.rooster { }.src
                        //new AARPMartialLawLoop.HTML.Audio.FromAssets.loop { }.src
                        new HTML.Audio.FromAssets.RookConfirmCommandStefanWeDontLiveOnAPlanet { }.src
                    );



                    // await ?
                    context.decodeAudioData(new Uint8ClampedArray(buffer).buffer,
                        xbuffer =>
                        {
                            // when the audio is decoded play the sound
                            sourceNode.buffer = xbuffer;



                            var sw = Stopwatch.StartNew();

                            var log = new StringBuilder();



                            var xleft = new List<int> { };
                            // 302 elements for  11 sec is 27fps
                            var xaverage = 0;

                            sourceNode.onended = IFunction.Of(
                                delegate
                                {
                                    // { min = 0, max = 63, Count = 264 }
                                    // { min = 0, max = 60, Count = 264 }

                                    var min = xleft.Min();
                                    var max = xleft.Max();

                                    new IHTMLPre { new { min, max, xleft.Count } }.AttachToDocument();




                                    new IHTMLTextArea { value = log.ToString() }.AttachToDocument();
                                }
                            );

                            Func<Uint8Array, double> getAverageVolume = (array) =>
                            {
                                var values = 0;
                                var average = 0.0;

                                var length = array.buffer.byteLength;

                                // get all the frequency amplitudes
                                for (var i = 0u; i < length; i++)
                                {
                                    values += array[i];
                                }

                                average = values / length;
                                return average;
                            };


                            var aMilliseconds = 0L;
                            var asw = new Stopwatch();

                            // 40ms per frame is trice for 60hz

                            // { xleft = 397, xaverage = 37, aMilliseconds = 40 }
                            new IHTMLPre { () => new { xleft = xleft.Count, xaverage, aMilliseconds } }.AttachToDocument();


                            // when the javascript node is called
                            // we use information from the analyzer node
                            // to draw the volume
                            javascriptNode.onaudioprocess = IFunction.Of(
                                delegate
                                {
                                    aMilliseconds = asw.ElapsedMilliseconds;


                                    // get the average for the first channel
                                    var array = new Uint8Array(new byte[analyser.frequencyBinCount]);
                                    // jsc could have all byte[] shadowed by bytebuffers for implict conversions...
                                    analyser.getByteFrequencyData(array);
                                    xaverage = (int)getAverageVolume(array);

                                    xleft.Add(xaverage);


                                    // get the average for the second channel
                                    var array2 = new Uint8Array(new byte[analyser2.frequencyBinCount]);
                                    analyser2.getByteFrequencyData(array2);
                                    var average2 = (int)getAverageVolume(array2);

                                    // clear the current state
                                    ctx.clearRect(0, 0, 60, 130);

                                    // set the fill style
                                    ctx.fillStyle = "red";


                                    // create the meters
                                    ctx.fillRect(0, 130 - xaverage, 25, 130);
                                    ctx.fillRect(30, 130 - average2, 25, 130);

                                    asw.Restart();

                                    log.AppendLine("new { ms = " + sw.ElapsedMilliseconds + ", x = " + xaverage + "},");

                                }
                            );




                            sourceNode.start(0);
                            //sourceNode.loop = true;

                        }
                     );






                }
            );






        }

    }
}
