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
using GoogleMapsMarkerLEST97;
using GoogleMapsMarkerLEST97.Design;
using GoogleMapsMarkerLEST97.HTML.Pages;

namespace GoogleMapsMarkerLEST97
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
            // http://xgis.maaamet.ee/xGIS/XGis?app_id=UU82&user_id=at&bbox=529464.185181536,6581178.67615528,549196.965601648,6593508.79911233&LANG=1
            // http://www.maaamet.ee/rr/geo-lest/files/geo-lest_function_vba.txt

            // can we have a list of cities on ?

            var Helsinki = new { x = 6671069.664199971, y = 552396.6626611819 };
            var Tallinn = new { x = 6589000.065127177, y = 542791.0230507818 };
            var Haapsalu = new { x = 6533398.0, y = 480832.0 };
            var Narva = new { x = 6589333.658324879, y = 737954.1228769943 };
            var Tartu = new { x = 6474047.4766877275, y = 659622.4604005406 };



            // alright lets do this.

            new { }.With(
              async delegate
              {
                  await google.maps.api;


                  var div = new IHTMLDiv
                  {
                  }.AttachToDocument();

                  div.style.border = "1px dashed red";
                  div.style.height = "300px";
                  div.style.left = "0px";
                  div.style.right = "0px";


                  var map = new google.maps.Map(div,
                      new
                      {

                          center = new { lat = 59.4329527, lng = 24.7023564 },
                          zoom = 6.0
                      }
                  );

                  var all = new[] { Helsinki, Tallinn, Haapsalu, Narva, Tartu };

                  all.WithEach(
                       data =>
                       {
                           var marker = new google.maps.Marker(
                              new
                              {
                                  position = new
                                  {
                                      lat = LEST97.lest_function_vba.lest_geo(data.x, data.y, 0),
                                      lng = LEST97.lest_function_vba.lest_geo(data.x, data.y, 1)
                                  },
                                  //label = "T",
                                  //title = "Tallinn",
                                  map
                              }
                           );
                       }
                     );





                  new IHTMLPre
                  {
                      () =>
                          new
                          {
                              map.getCenter().lat,
                              map.getCenter().lng,

                              x = LEST97.lest_function_vba.geo_lest( map.getCenter().lat, map.getCenter().lng, 0),
                              y = LEST97.lest_function_vba.geo_lest( map.getCenter().lat, map.getCenter().lng, 1),
                          }
                  }.AttachToDocument();


                  // Cannot read property 'getSouthWest' of undefined
                  new IHTMLPre
                  {
                      //  rectangle.addListener('bounds_changed', showNewRect);

                      delegate
                      {

                          if (map.getBounds() == null)
                          {
                              return "n/a";
                          }

                          var getSouthWest_lat = map.getBounds().getSouthWest().lat;
                          var getSouthWest_lng = map.getBounds().getSouthWest().lng;

                          var getNorthEast_lat = map.getBounds().getNorthEast().lat;
                          var getNorthEast_lng = map.getBounds().getNorthEast().lng;


                          var count = (
                            from data in all

                            let lat = (double)LEST97.lest_function_vba.lest_geo(data.x, data.y, 0)
                            let lng = (double)LEST97.lest_function_vba.lest_geo(data.x, data.y, 1)


                            // lng left to right
                            // lng bottom to top

                            where lat <= getNorthEast_lat
                            where lng <= getNorthEast_lng

                            where lat >= getSouthWest_lat
                            where lng >= getSouthWest_lng

                            select data
                          ).Count();

                          return new
                          {
                              getSouthWest_lat,
                              getSouthWest_lng,

                              getNorthEast_lat,
                              getNorthEast_lng,

                              count
                          }.ToString();
                      }
                  }.AttachToDocument();

                  // http://stackoverflow.com/questions/2472957/how-can-i-change-the-color-of-a-google-maps-marker


                  #region Add
                  new IHTMLButton { "Add" }.AttachToDocument().onclick += delegate
                  {
                      var data = new
                      {
                          map.getCenter().lat,
                          map.getCenter().lng,

                          x = LEST97.lest_function_vba.geo_lest(map.getCenter().lat, map.getCenter().lng, 0),
                          y = LEST97.lest_function_vba.geo_lest(map.getCenter().lat, map.getCenter().lng, 1),
                      };

                      new IHTMLPre { "new { x = " + data.x + ", y = " + data.y + " }" }.AttachToDocument();

                      var marker = new google.maps.Marker(
                        new
                        {
                            position = new
                            {
                                lat = LEST97.lest_function_vba.lest_geo(data.x, data.y, 0),
                                lng = LEST97.lest_function_vba.lest_geo(data.x, data.y, 1)
                            },
                            //label = "H",
                            title = data.ToString(),
                            map
                        }
                     );
                  };
                  #endregion



                  #region Add Bounds
                  new IHTMLButton { "Add Bounds" }.AttachToDocument().onclick += delegate
                  {
                      new google.maps.Marker(
                        new
                        {
                            position = new
                            {
                                map.getBounds().getSouthWest().lat,
                                map.getBounds().getSouthWest().lng,
                            },
                            map
                        }
                     );

                      new google.maps.Marker(
                        new
                        {
                            position = new
                            {
                                map.getBounds().getNorthEast().lat,
                                map.getBounds().getNorthEast().lng,
                            },
                            map
                        }
                     );

                      new google.maps.Marker(
                    new
                    {
                        position = new
                        {
                            map.getBounds().getSouthWest().lat,
                            map.getBounds().getNorthEast().lng,
                        },
                        map
                    }
                 );

                      new google.maps.Marker(
                        new
                        {
                            position = new
                            {
                                map.getBounds().getNorthEast().lat,
                                map.getBounds().getSouthWest().lng,
                            },
                            map
                        }
                     );
                  };
                  #endregion

              }

            );



        }

    }
}
