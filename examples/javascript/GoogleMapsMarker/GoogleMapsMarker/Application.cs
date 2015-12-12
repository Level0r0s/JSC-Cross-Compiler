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
using GoogleMapsMarker;
using GoogleMapsMarker.Design;
using GoogleMapsMarker.HTML.Pages;

namespace google
{
    public class AsyncEvent
    {
        // Z:\jsc.svn\examples\javascript\io\DropLESTToDisplay\DropLESTToDisplay\Application.cs
        // x:\jsc.svn\examples\javascript\webgl\WebGLGodRay\WebGLGodRay\Application.cs
        public static implicit operator bool(AsyncEvent e)
        {
            // future C# may allow if (obj)
            // but for now booleans are needed

            // enable 
            // while (await Native.window.async.onresize);
            return ((object)e != null);
        }
    }

    public static class maps
    {
        static TaskCompletionSource<object> __api;

        // enable nested .ctors 
        static public Task api
        {
            get
            {
                // You have included the Google Maps API multiple times on this page. This may cause unexpected errors.
                if (__api != null)
                    return __api.Task;

                var x = new TaskCompletionSource<object>();

                __api = x;

                var api = new IHTMLScript
                {
                    //src = "https://maps.googleapis.com/maps/api/js?key=API_KEY&signed_in=true&callback=initMap"
                    src = "https://maps.googleapis.com/maps/api/js?&callback=mapsapi"

                    //async
                    //defer
                    //src = "https://maps.googleapis.com/maps/api/js?"
                };


                //Native.document.head.onm


                //var initMapTask = new TaskCompletionSource<object>();

                // jsc is not exposing delegate as IFunction?
                // Uncaught TypeError: window.initMap is not a function
                //(Native.window as dynamic)["initMap"] = (Action)delegate

                (Native.window as dynamic).mapsapi = (Action)delegate
                {

                    //Native.document.body.style.backgroundColor = "cyan";

                    x.SetResult(null);
                };


                api.AttachToHead();


                //await api.AttachToHead().async.onload;


                //new IHTMLPre { new { (Native.window as dynamic).google } }.AttachToDocument();
                //new IHTMLPre { new { (Native.window as dynamic).google.maps } }.AttachToDocument();

                //await initMapTask.Task;

                return x.Task;
            }
        }



        // https://developers.google.com/maps/documentation/javascript/markers
        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.Marker")]
        public class Marker // : IEventTarget
        {
            public Marker(object options)
            {

            }


            public void setIcon(string e)
            {
            }

            public Tasks async
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    return new Tasks { that = this };
                }
            }

            public class Tasks
            {
                public Marker that;

                public Task<AsyncEvent> onclick
                {
                    get
                    {
                        var x = new TaskCompletionSource<AsyncEvent>();

                        that.onclick += delegate
                        {

                            if (x == null)
                                return;


                            x.SetResult(new AsyncEvent { });
                            x = null;
                        };

                        return x.Task;
                    }
                }

                public Task<AsyncEvent> onmouseover
                {
                    get
                    {
                        var x = new TaskCompletionSource<AsyncEvent>();

                        that.onmouseover += delegate
                        {

                            if (x == null)
                                return;


                            x.SetResult(new AsyncEvent { });
                            x = null;
                        };

                        return x.Task;
                    }
                }

                public Task onmouseout
                {
                    get
                    {
                        var x = new TaskCompletionSource<AsyncEvent>();

                        that.onmouseout += delegate
                        {

                            if (x == null)
                                return;


                            x.SetResult(new AsyncEvent { });
                            x = null;
                        };

                        return x.Task;
                    }
                }
            }


            public event Action onclick
            {
                [Script(DefineAsStatic = true)]
                remove
                { }


                [Script(DefineAsStatic = true)]
                add
                {

                    this.addListener("click", value);
                }

            }

            public event Action onmouseover
            {
                [Script(DefineAsStatic = true)]
                remove
                { }


                [Script(DefineAsStatic = true)]
                add
                {

                    this.addListener("mouseover", value);
                }

            }

            public event Action onmouseout
            {
                [Script(DefineAsStatic = true)]
                remove
                { }


                [Script(DefineAsStatic = true)]
                add
                {

                    this.addListener("mouseout", value);
                }

            }

            private void addListener(string v, Action value)
            {
                throw new NotImplementedException();
            }

            public object getPosition()
            {
                throw new NotImplementedException();
            }



        }

        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.InfoWindow")]
        public class InfoWindow
        {
            public InfoWindow(object options)
            {

            }

            public void setPosition(object v)
            {
                throw new NotImplementedException();
            }

            // Z:\jsc.svn\examples\javascript\data\GoogleMapsTracker\Application.cs
            public void setContent(string v)
            {
                throw new NotImplementedException();
            }
        }

        // https://developers.google.com/maps/documentation/javascript/examples/polyline-simple
        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.Polyline")]
        public class Polyline
        {
            public Polyline(object args)
            {

            }

            public void setMap(Map map) { }
        }




        // https://developers.google.com/maps/documentation/javascript/reference?hl=en#LatLngBounds
        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.LatLngBounds")]
        public class LatLngBounds
        {
            public LatLng getCenter() { throw null; }
            public LatLng getNorthEast() { throw null; }
            public LatLng getSouthWest() { throw null; }

        }


        // Z:\jsc.svn\examples\javascript\Test\TestNativeMethodAsProperty\TestNativeMethodAsProperty\Class1.cs
        // https://developers.google.com/maps/documentation/javascript/reference?hl=en#LatLng
        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.LatLng")]
        public class LatLng
        {
            // Latitude ranges between -90 and 90 degrees, inclusive

            // TypeError: this.map.getCenter(...).get_lat is not a function
            public double lat { [method: Script(ExternalTarget = "lat")]get; private set; }

            public double lng { [method: Script(ExternalTarget = "lng")]get; private set; }
            // LatLngLiteral 
            //public double lat;

            // Longitude ranges between -180 and 180 degrees, inclusive. 
            //public double lng;
        }

        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.Map")]
        public class Map
        {
            public Map(IHTMLDiv map, object options)
            {

            }




            public LatLngBounds getBounds()
            {
                throw new NotImplementedException();
            }

            public LatLng getCenter()
            {
                throw new NotImplementedException();
            }



            public void setCenter(object v)
            {
                throw new NotImplementedException();
            }



            //public double zoom { [method: Script(ExternalTarget = "lng")]get; private set; }

            public void setZoom(double v)
            {
                throw new NotImplementedException();
            }

            public double getZoom()
            {
                throw new NotImplementedException();
            }



            public Tasks async
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    return new Tasks { that = this };
                }
            }

            public class Tasks
            {
                public Map that;

                public Task onzoomchanged
                {
                    get
                    {
                        var x = new TaskCompletionSource<object>();

                        that.onzoomchanged += delegate
                        {

                            if (x == null)
                                return;


                            x.SetResult(null);
                            x = null;
                        };

                        return x.Task;
                    }
                }
            }


            public event Action onzoomchanged
            {
                [Script(DefineAsStatic = true)]
                remove
                { }


                [Script(DefineAsStatic = true)]
                add
                {

                    this.addListener("zoom_changed", value);
                }

            }

            private void addListener(string v, Action value)
            {
                throw new NotImplementedException();
            }
        }
    }
}

namespace GoogleMapsMarker
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151007
            // https://developers.google.com/maps/documentation/javascript/examples/map-geolocation

            //chrome.AppWindow
            new { }.With(
                async delegate
                {
                    // tested with
                    // s6, s1, tab7, ipad2

                    // tested via nfc
                    //   SERIALNUMBER=38505300414, G=ARVO, SN=SULAKATKO, CN="SULAKATKO,ARVO,38505300414", OU=authentication, O=ESTEID (DIGI-ID), C=EE

                    Native.document.body.style.backgroundColor = "yellow";

                    new IHTMLPre { " await google.maps.api" }.AttachToDocument();
                    await google.maps.api;
                    new IHTMLPre { " await google.maps.api done" }.AttachToDocument();


                    // Failed to execute 'write' on 'Document': It isn't possible to write into a document from an asynchronously-loaded external script unless it is explicitly opened.

                    //var api = new IHTMLScript
                    //{
                    //    //src = "https://maps.googleapis.com/maps/api/js?key=API_KEY&signed_in=true&callback=initMap"
                    //    src = "https://maps.googleapis.com/maps/api/js?&callback=initMap"

                    //    //async
                    //    //defer
                    //    //src = "https://maps.googleapis.com/maps/api/js?"
                    //};


                    ////Native.document.head.onm


                    //var initMapTask = new TaskCompletionSource<object>();

                    //// jsc is not exposing delegate as IFunction?
                    //// Uncaught TypeError: window.initMap is not a function
                    ////(Native.window as dynamic)["initMap"] = (Action)delegate

                    //(Native.window as dynamic).initMap = (Action)delegate
                    //{

                    //    //Native.document.body.style.backgroundColor = "cyan";

                    //    initMapTask.SetResult(null);
                    //};



                    //new { }.With(
                    //    async delegate
                    //    {
                    //        await Native.document.head.async.onmutation;

                    //        new IHTMLPre { Native.document.head.AsXElement() }.AttachToDocument();

                    //        await Native.document.head.async.onmutation;

                    //        new IHTMLPre { Native.document.head.AsXElement() }.AttachToDocument();

                    //        await Native.document.head.async.onmutation;

                    //        new IHTMLPre { Native.document.head.AsXElement() }.AttachToDocument();

                    //    }
                    //);

                    var div = new IHTMLDiv
                    {
                        //id = "map"
                    }.AttachToDocument();

                    ////map.id = "map_canvas";
                    //await api.AttachToHead().async.onload;


                    //new IHTMLPre { new { (Native.window as dynamic).google } }.AttachToDocument();
                    //new IHTMLPre { new { (Native.window as dynamic).google.maps } }.AttachToDocument();

                    //await initMapTask.Task;

                    //new IHTMLPre { new { (Native.window as dynamic).google.maps.Map } }.AttachToDocument();


                    //api.onlo
                    //await api.async.onc

                    // {{ Map = function Eh(a,b)


                    div.style.border = "1px dashed red";
                    div.style.height = "300px";
                    div.style.left = "0px";
                    div.style.right = "0px";

                    //div.style.SetSize(400, 300);


                    // b: "projectionTopLeft"
                    // http://stackoverflow.com/questions/5471848/how-to-get-screen-xy-from-google-maps-v3-latlng

                    //await Native.window.async.onframe;

                    var map = new google.maps.Map(div,
                        new
                        {

                            //center = new { lat = -34.397, lng = 150.644 },
                            center = new { lat = 59.4329527, lng = 24.7023564 },

                            //https://www.google.ee/maps/@59.4329527,24.7023564,14z?hl=en
                            zoom = 6.0
                        }
                    );

                    new IHTMLPre { "do you see it?" }.AttachToDocument();

                    // https://developers.google.com/maps/documentation/javascript/examples/marker-labels

                    // Uncaught TypeError: Cannot read property 'x' of undefined

                    //var infoWindow = new google.maps.InfoWindow(new { map });

                    //infoWindow.setPosition(map.getCenter());
                    //infoWindow.setContent("InfoWindow");


                    // https://developers.google.com/maps/documentation/javascript/examples/event-simple

                    var marker = new google.maps.Marker(
                        new
                        {
                            position = map.getCenter(),
                            //label = new { lat = 59.4329527, lng = 24.7023564 }.ToString(),
                            label = "23",
                            title = "click to zoom",
                            map
                        }
                     );

                    //marker.onclick += delegate
                //{
                //    map.setZoom(8.0);
                //    map.setCenter(marker.getPosition());
                //};


                    //while (await marker.async.onclick)


                    next:
                    await marker.async.onclick;
                    {

                        map.setZoom(8.0);
                        map.setCenter(marker.getPosition());

                        await marker.async.onclick;

                        map.setZoom(6.0);
                        map.setCenter(marker.getPosition());

                        goto next;
                    }
                }
            );

        }

    }
}

// https://developers.google.com/maps/documentation/business/clientside/#client_id
// The Google Maps JavaScript API does not require an API key to function correctly.
//This page was unable to display a Google Maps element.The provided Google API key is invalid or this site is not authorized to use it.Error Code: InvalidKeyOrUnauthorizedURLMapError
// Uncaught TypeError: window.initMap is not a function
