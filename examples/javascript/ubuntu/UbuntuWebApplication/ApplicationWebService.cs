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

namespace UbuntuWebApplication
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151019

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
            Console.WriteLine("enter Handler");

            if (h.IsDefaultPath)
            {
                //h.Context.Response.Redirect("/redirect" + DateTime.Now.Millisecond);
                h.Context.Response.Redirect("/redirect" + DateTime.Now.Second);
                h.CompleteRequest();
            }
            else
            {
                // { UserHostAddress = 127.0.0.1, Path = /redirect168, HttpMethod = GET }

                h.Context.Response.AddHeader("hello", "world");

                h.Context.Response.Write(

                    new XElement("pre",
                        new
                        {
                            h.Context.Request.UserHostAddress,
                            h.Context.Request.Path,
                            h.Context.Request.HttpMethod
                        }.ToString()
                    ).ToString()

                );
                h.CompleteRequest();
            }
        }


    }
}

// Z:\jsc.svn\examples\javascript\ubuntu\UbuntuWebApplication\bin\Debug\staging\UbuntuWebApplication.ApplicationWebService\staging.ubuntu
// C:\util\jsc\bin\jsc.meta.exe RewriteToHybridJavaApplication /assembly:UbuntuWebApplication.ApplicationWebService.exe

//copy $(TargetPath) U:\
