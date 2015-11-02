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
using System.Net.Sockets;
using System.Diagnostics;

namespace TestDualSSLWebApplication
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        //<pre id='ClientCertificate'>n/a</pre>
        public XElement ClientCertificate = new XElement("pre", "n/a");



        // AppDomainData
        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
            //#0a > 000a 0x019f bytes
            //about to pass RemoteCertificate to cassini
            //enter InternalCassiniClientCertificateLoader { Location = C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\root\3ea1022a\80f01a9\assembly\dl3\6ebd91e5\89cb4511_4215d101\ScriptCoreLib.Ultra.Library.dll }


            // ssl handshake gives certificate to global, it gives it to the handler, we give it to UI

            //var n = h.Context.Request.InputStream as NetworkStream;


            h.ClientCertificate.With(
                c =>
                {
                    Console.WriteLine("enter Handler " + new { h.ClientCertificate.Subject });
                    //this.status.Value = this.id;

                    ClientCertificate.Value = new { h.ClientCertificate.Subject }.ToString();
                });

        }

    }

    
}
