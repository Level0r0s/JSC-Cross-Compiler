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
using GoogleMapsTracker;
using GoogleMapsTracker.Design;
using GoogleMapsTracker.HTML.Pages;

// v2. available as a nuget yet?
using ScriptCoreLib.Query.Experimental;
using System.Diagnostics;

namespace GoogleMapsTracker
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
            Native.body.Clear();

            new IHTMLAnchor { href = "/jsc", innerText = "enter diagnostic mode" }.AttachToDocument();







            new { }.With(
                async delegate
                {


                    // Severity	Code	Description	Project	File	Line
                    //Error CS7069  Reference to type 'TaskAwaiter<>' claims it is defined in 'mscorlib', but it could not be found GoogleMapsTracker Z:\jsc.svn\examples\javascript\data\GoogleMapsTracker\Application.cs    40


                    // the new using
                    // 4.6
                    // stored at Z:\jsc.svn\examples\javascript\data\packages
                    // is that where jsc and nuget both agree to store it? no. jsc needs to mimick nuget here.

                    // did we build it?
                    await google.maps.api;


                    var div = new IHTMLDiv
                    {
                        //}.AttachTo(Native.document.documentElement);


                        // future jsc will have the ability to make offline edit and continue changes
                    }.AttachTo(Native.body);

                    div.style.position = IStyle.PositionEnum.absolute;
                    div.style.borderBottom = "1px dashed red";
                    div.style.height = "400px";
                    div.style.top = "0px";
                    div.style.left = "0px";
                    div.style.right = "0px";

                    Native.body.style.marginTop = "420px";

                    var map = new google.maps.Map(div,
                        new
                        {
                            // https://developers.google.com/maps/documentation/javascript/examples/control-disableUI
                            disableDefaultUI = true,


                            center = new { lat = 59.4329527, lng = 24.7023564 },

                            //https://www.google.ee/maps/@59.4329527,24.7023564,14z?hl=en
                            zoom = 6.0
                        }
                    );

                    // https://developers.google.com/maps/documentation/javascript/examples/event-simple



                    var flightPlanCoordinates = new[] {
                        // Object literals are accepted in place of LatLng objects, as a convenience, in many places
                        new { lat = 59.4329527 - 0.25, lng = 24.7023564 - 0.5},
                        new { lat = 59.4329527 - 0.25, lng = 24.7023564 + 0.5 },

                        // up, right
                        new { lat = 59.4329527 + 0.25, lng = 24.7023564 + 0.5},
                        new { lat = 59.4329527 + 0.25, lng = 24.7023564 - 0.5},

                        // close the line
                        //new { lat = 59.4329527 - 0.25, lng = 24.7023564 - 0.5},
                    };


                    // https://developers.google.com/maps/documentation/javascript/examples/polyline-simple
                    var flightPath = new google.maps.Polyline(new
                    {
                        path = flightPlanCoordinates.Concat(flightPlanCoordinates.Take(1)).ToArray(),
                        geodesic = true,
                        strokeColor = "#FF0000",
                        strokeOpacity = 1.0,
                        strokeWeight = 2
                    });

                    // like a stlus huh.
                    flightPath.setMap(map);


                    // alternative is we play a pre recored session.


                    new { }.With(
                        async delegate
                        {
                            await new IHTMLButton { "load other replays (?)" }.AttachToDocument().async.onclick;

                            var other = new IHTMLSelect { }.AttachToDocument();

                            var replayIDs = await base.GetAllReplays();

                            if (replayIDs.Length == 0)
                            {
                                other.Add("n/a");
                                return;
                            }

                            other.Add(replayIDs);

                            var btn = new IHTMLButton { "select one of them!" }.AttachToDocument();

                            other.onchange += async delegate
                            {
                                var xreplayID = other[other.selectedIndex].value;

                                btn.innerText = "show " + new { other.selectedIndex, xreplayID }.ToString();

                                var count = await base.GetReplayCount(xreplayID);

                                btn.innerText = "show " + new { other.selectedIndex, xreplayID, count }.ToString();

                            };

                            btn.onclick += async delegate
                            {
                                // draw polygon onto map from database.

                                var xreplayID = other[other.selectedIndex].value;

                                var sw = Stopwatch.StartNew();

                                new IHTMLPre { () => new { sw.ElapsedMilliseconds } }.AttachToDocument();

                                var xreplay = await base.GetReplay(xreplayID);

                                sw.Stop();

                                new IHTMLPre { () => new { xreplay.Length } }.AttachToDocument();

                                // can gmaps use our datarows too? yes.
                                // will this work on android? and appengine, mysql? and ubuntu?
                                var xreplayPath = new google.maps.Polyline(new
                                {
                                    path = xreplay.ToArray(),
                                    geodesic = true,
                                    strokeColor = "#00FF00",
                                    strokeOpacity = 1.0,
                                    strokeWeight = 4
                                });

                                // like a stlus huh.
                                xreplayPath.setMap(map);
                            };
                        }
                    );



                    // replayID	lat as double	lng as double	comment
                    // we are to record a new 
                    var replayID = "replayID" + new Random().Next().ToString("x8");

                    // lets use this id to stream to database. can be used for later replay?
                    await new IHTMLButton { "record new session as " + replayID }.AttachToDocument().async.onclick;


                    new IHTMLButton { "reload" }.AttachToDocument().onclick += delegate
                    {
                        Native.document.location.reload();
                    };


                    var sw1 = Stopwatch.StartNew();
                    var stop = new IHTMLButton { "stop" }.AttachToDocument().async.onclick;
                    var stop2 = false;


                    // can we now do the autodrawer?

                    // load from db?
                    var history = new List<google.maps.LatLng> { };

                    //var delete = delegate
                    //  Console: Uncaught SyntaxError: Unexpected token delete http://192.168.1.126:15175/view-source:80480

                    // should jsc rename delete token like we do for catch?
                    Action dodelete = delegate
                    {
                    };

                    new { }.With(
                        async delegate
                        {
                            // at every so often remember the point and redraw.

                            //while  await Task.Delay()
                            do
                            {
                                // 1 fps?
                                //await Task.Delay(1000 / 1);
                                //await Task.Delay(1000 / 15);
                                await Task.Delay(1000 / 4);




                                // unless we did not move?
                                history.Add(
                                    map.getCenter()
                                );

                                while (history.Count > 128)
                                    history.RemoveAt(0);

                                dodelete();

                                var historyPath = new google.maps.Polyline(new
                                {
                                    path = history.ToArray(),
                                    geodesic = true,
                                    strokeColor = "#0000FF",
                                    strokeOpacity = 1.0,
                                    strokeWeight = 4
                                });

                                // like a stlus huh.
                                historyPath.setMap(map);

                                dodelete = delegate
                                {
                                    historyPath.setMap(null);
                                };

                                // show buzy
                                div.style.borderBottom = "1px dashed red";
                                await base.AddHistory(replayID, map.getCenter().lng, map.getCenter().lat);

                                // show ready
                                div.style.borderBottom = "1px dashed blue";

                            }
                            while (!stop.IsCompleted);

                            sw1.Stop();
                        }
                    );



                    var marker = new google.maps.Marker(
                                new
                                {
                                    position = map.getCenter(),
                                    label = "R",
                                    title = "click to zoom",
                                    map
                                }
                             );




                    // lets keep polling where are we looking at
                    //new IHTMLPre { () => new { Position = marker.getPosition() } }.AttachToDocument();
                    new IHTMLPre { () => new { stop = stop.IsCompleted, sw1.ElapsedMilliseconds, history.Count, getCenter = map.getCenter(), map.getCenter().lat, map.getCenter().lng } }.AttachToDocument();



                    //new IHTMLPre { map.getCenter }.AttachToDocument();
                    //new IHTMLPre().Add()
                    //while (!stop.IsCompleted)
                    while (true)
                    {
                        await marker.async.onclick;


                        // can we make the poly dissapear?
                        flightPath.setMap(null);

                        map.setZoom(8.0);
                        map.setCenter(marker.getPosition());

                        await marker.async.onclick;

                        // and reappeaar
                        flightPath.setMap(map);

                        map.setZoom(6.0);
                        map.setCenter(marker.getPosition());

                    }



                }


            );
        }

    }

    public partial class ApplicationWebService
    {
        //Error	13	Method must have a return type	Z:\jsc.svn\examples\javascript\data\GoogleMapsTracker\Application.cs	322	11	GoogleMapsTracker

        // Error CS0029  Cannot implicitly convert type 'ScriptCoreLib.Query.Experimental.IQueryStrategy<string>' to 'string[]'	GoogleMapsTracker Z:\jsc.svn\examples\javascript\data\GoogleMapsTracker\Application.cs	239

        // instead of returning the IQueryable, jsc should allow server code inside client code, to allow anonymous type passing
        // how many replays could there be?
        public Task<string[]> GetAllReplays()
        {
            return (
                from x in new Data.replayhistory()
                group x by x.replayID into g
                select g.Key
            ).ToArray().AsResult();
        }

        // get everything? or limit?
        public Task<Data.replayhistoryRow[]> GetReplay(string replayID)
        {
            // with logging on, 471 items will take 22sec on android!
            return (
                from x in new Data.replayhistory()
                where x.replayID == replayID
                select x
                ).ToArray().AsResult();
        }

        public async Task<long> GetReplayCount(string replayID)
        {
            return (
                from x in new Data.replayhistory()
                where x.replayID == replayID
                select x
                ).Count();
        }



        //public async Task AddHistory(string replayID, double lng, double lat)
        public Task AddHistory(string replayID, double lng, double lat)
        {
            Console.WriteLine(new { replayID, lng, lat });

            // we have the data from the client.
            // lets keep it around.


            var key = new Data.replayhistory().Insert(
                new Data.replayhistoryRow { replayID = replayID, lng = lng, lat = lat }
            );

            // look a key. useful if we want a comment attached to this row?
            Console.WriteLine(new { key });

            // first the project needs to include xlsx
            // sheet1 should be named.
            // then rebuild project to generate the da-talayer.

            // slow it down.
            //await Task.Delay(1000 / 15);

            return Task.CompletedTask;

            // android runtime wont show the async error yet
        }
    }
}
