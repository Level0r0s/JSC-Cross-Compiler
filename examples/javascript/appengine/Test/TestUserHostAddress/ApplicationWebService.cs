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

namespace TestUserHostAddress
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httprequest

        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {

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

            // view-source ?

            h.Context.Response.Write(x.ToString());

            h.CompleteRequest();
            return;
        }

    }
}
