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
using GoogleMapsInfoWindow;
using GoogleMapsInfoWindow.Design;
using GoogleMapsInfoWindow.HTML.Pages;

namespace google
{

    public static class maps
    {

        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.InfoWindow")]
        public class InfoWindow
        {
            public InfoWindow(object options)
            {

            }

            internal void setPosition(object v)
            {
                throw new NotImplementedException();
            }

            internal void setContent(string v)
            {
                throw new NotImplementedException();
            }
        }


        [Script(HasNoPrototype = true, ExternalTarget = "google.maps.Map")]
        public class Map
        {
            public Map(IHTMLDiv map, object options)
            {

            }

            internal object getCenter()
            {
                throw new NotImplementedException();
            }
        }
    }
}

namespace GoogleMapsInfoWindow
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
            // https://developers.google.com/maps/documentation/javascript/examples/map-geolocation

            //chrome.AppWindow
            new { }.With(
                async delegate
                {
                    Native.document.body.style.backgroundColor = "yellow";


                    // Failed to execute 'write' on 'Document': It isn't possible to write into a document from an asynchronously-loaded external script unless it is explicitly opened.

                    var api = new IHTMLScript
                    {
                        //src = "https://maps.googleapis.com/maps/api/js?key=API_KEY&signed_in=true&callback=initMap"
                        src = "https://maps.googleapis.com/maps/api/js?&callback=initMap"

                        //async
                        //defer
                        //src = "https://maps.googleapis.com/maps/api/js?"
                    };


                    //Native.document.head.onm


                    var initMapTask = new TaskCompletionSource<object>();

                    // jsc is not exposing delegate as IFunction?
                    // Uncaught TypeError: window.initMap is not a function
                    //(Native.window as dynamic)["initMap"] = (Action)delegate

                    (Native.window as dynamic).initMap = (Action)delegate
                    {

                        //Native.document.body.style.backgroundColor = "cyan";

                        initMapTask.SetResult(null);
                    };



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

                    //map.id = "map_canvas";
                    await api.AttachToHead().async.onload;


                    new IHTMLPre { new { (Native.window as dynamic).google } }.AttachToDocument();
                    new IHTMLPre { new { (Native.window as dynamic).google.maps } }.AttachToDocument();

                    await initMapTask.Task;

                    //new IHTMLPre { new { (Native.window as dynamic).google.maps.Map } }.AttachToDocument();


                    //api.onlo
                    //await api.async.onc

                    // {{ Map = function Eh(a,b)


                    div.style.border = "1px dashed red";


                    div.style.SetSize(400, 300);


                    // b: "projectionTopLeft"
                    // http://stackoverflow.com/questions/5471848/how-to-get-screen-xy-from-google-maps-v3-latlng

                    //await Native.window.async.onframe;

                    var map = new google.maps.Map(div,
                        new
                        {
                            center = new { lat = -34.397, lng = 150.644 },
                            zoom = 6.0
                        }
                    );

                    new IHTMLPre { "do you see it?" }.AttachToDocument();

                    // Uncaught TypeError: Cannot read property 'x' of undefined

                    var infoWindow = new google.maps.InfoWindow(new { map });

                    infoWindow.setPosition(map.getCenter());
                    infoWindow.setContent("InfoWindow");
                }
            );

        }

    }
}

// https://developers.google.com/maps/documentation/business/clientside/#client_id
// The Google Maps JavaScript API does not require an API key to function correctly.
//This page was unable to display a Google Maps element.The provided Google API key is invalid or this site is not authorized to use it.Error Code: InvalidKeyOrUnauthorizedURLMapError
// Uncaught TypeError: window.initMap is not a function
