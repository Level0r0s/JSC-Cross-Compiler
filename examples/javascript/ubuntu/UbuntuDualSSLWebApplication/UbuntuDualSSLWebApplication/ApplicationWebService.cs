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

namespace UbuntuDualSSLWebApplication
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/ubuntumidexperiment
        // http://stackoverflow.com/questions/25553813/ssl-error-from-java-client-but-works-form-poster-in-firefox

        //public XElement status = new XElement("ready");

        string id = "guest";



        public async Task<string> Identity()
        {
            Console.WriteLine("enter Identity " + new { this.id });

            return this.id;
        }

        public async Task<string> Color()
        {
            return "yellow";
        }


        public string Foo;

        public async Task<string> GetSpecialData()
        {
            return "GetSpecialData for " + new { id, Foo };
        }

        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
            // ssl handshake gives certificate to global, it gives it to the handler, we give it to UI

            Console.WriteLine("enter Handler " + new { h.ClientCertificate });

            h.ClientCertificate.With(
                c =>
                {
                    this.id = new { c.Subject }.ToString();
                    //this.status.Value = this.id;
                });

        }
    }
}
