using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UbuntuTestUserHostAddress
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151023/ubuntuwebapplication
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httprequest
        // Z:\jsc.svn\examples\javascript\appengine\test\TestRedirect\ApplicationWebService.cs


        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
            // enable GetColor
            if (h.Context.Request.Path.StartsWith("/xml/"))
                return;


            if (h.Context.Request.Path == "/view-source")
                return;


            if (h.Context.Request.Path == "/jsc")
                return;

            //if (!h.IsDefaultPath)
            //    return;

            h.Context.Response.Write(@"<!-- 

hello world! will prerender janusVR scene, as API wont enable all of the features just yet at runtime 

-->
				");

            var x = XElement.Parse(HTML.Pages.AppSource.Text);


            // port rewriter for cassini?
            // { UserHostAddress = 127.0.0.1 }

            // appengine now reports.
            // enter InternalInvokeWebService
            // {{ UserHostAddress = 192.168.1.196 }}

            //x.Element("body").Element("FireBoxRoom").Element("Room").Element("Text").Value = "hello from1 ServerSideContent " + new { Debugger.IsAttached };
            x.Element("body").Element("pre").Value = new
            {
                h.Context.Request.UserHostAddress,
                h.Context.Request.Path,
                h.Context.Request.HttpMethod
            }.ToString();


            //ScriptCoreLib.Ultra.WebService.InternalGlobalExtensions

            h.Context.Request.QueryString.AllKeys.WithEachIndex(

                (key, index) =>
                {
                    var QueryString = h.Context.Request.QueryString[key];

                    x.Element("body").Add(
                        //new XElement("pre", new { key, index, QueryString }
                        new XElement("pre", new { key, index, QueryString }.ToString()
                        )
                    );

                }
            );


            // does IE send the Form headers?
            // can we see the non javascript post form values on ubuntu?

            // { key = formtext1, index = 0, Form = hello }
            h.Context.Request.Form.AllKeys.WithEachIndex(
                (key, index) =>
                {
                    var Form = h.Context.Request.Form[key];
                    x.Element("body").Add(
                        //new XElement("pre", new { key, index, QueryString }
                        new XElement("pre", new { key, index, Form }.ToString()
                        )
                    );

                }
            );

            h.Context.Request.Headers.AllKeys.WithEachIndex(
              (key, index) =>
              {
                  var Header = h.Context.Request.Headers[key];
                  x.Element("body").Add(
                      //new XElement("pre", new { key, index, QueryString }
                      new XElement("pre", new { key, index, Header }.ToString()
                      )
                  );

              }
          );

            h.Context.Request.Cookies.AllKeys.WithEachIndex(
             (key, index) =>
             {
                 var Cookie = h.Context.Request.Cookies[key].Value;
                 x.Element("body").Add(
                     //new XElement("pre", new { key, index, QueryString }
                     new XElement("pre", new { key, index, Cookie }.ToString()
                     )
                 );

             }
         );

            // { key = file1, index = 0, ContentLength = 1704 }
            // { key = file1, index = 0, ContentLength = 1704 }
            h.Context.Request.Files.AllKeys.WithEachIndex(
               (key, index) =>
               {
                   var File = h.Context.Request.Files[key];
                   x.Element("body").Add(
                       //new XElement("pre", new { key, index, QueryString }
                       new XElement("pre", new { key, index, File.ContentLength }.ToString()
                       )
                   );

               }
           );


            x.Add(
                new XElement("script",
                    new XAttribute("src", "view-source"),
                    " "
                )
            );


            h.Context.Response.SetCookie(
                new System.Web.HttpCookie("hello", "world")
            );

            // view-source ?

            h.Context.Response.Write(x.ToString());

            h.CompleteRequest();
            return;
        }



        public async Task<string> GetColor()
        {
            Console.WriteLine("enter GetColor");

            return "cyan";
        }
    }
}
