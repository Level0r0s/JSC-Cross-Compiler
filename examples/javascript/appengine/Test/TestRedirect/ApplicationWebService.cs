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

namespace TestRedirect
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
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

//Implementation not found for type import :
//type: System.DateTime
//method: Int32 get_Millisecond()
//Did you forget to add the[Script] attribute?
//Please double check the signature!
