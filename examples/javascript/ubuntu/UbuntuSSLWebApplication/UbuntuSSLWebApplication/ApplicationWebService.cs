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

namespace UbuntuSSLWebApplication
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // http://stackoverflow.com/questions/25553813/ssl-error-from-java-client-but-works-form-poster-in-firefox

        public XElement status = new XElement("ready");

        string id = "guest";

        static ApplicationWebService()
        {

            // EmbeddedResource
            var ref1 = "assets/UbuntuSSLWebApplication/ESTEID-SK_2011.der.crt";

            Console.WriteLine("ApplicationWebService " + new { ref1 });

            //  In the case of Estonian ID cards, the issuer is SK and the certificates are available from their homepage.
            // https://eid.eesti.ee/index.php/Using_eID_with_existing_applications

            //  To work with Estonian ID card 
            // JUUR-SK, 
            // EE Certification Centre Root CA, 
            // ESTEID-SK 2007, and 

            // ESTEID-SK 2011 must be installed.

        }

        public async Task<string> Identity()
        {
            Console.WriteLine("enter Identity " + new { this.id });

            return this.id;
        }

        public async Task<string> Color()
        {
            return "yellow";
        }


        public void Handler(ScriptCoreLib.Ultra.WebService.WebServiceHandler h)
        {
            // ssl handshake gives certificate to global, it gives it to the handler, we give it to UI

            Console.WriteLine("enter Handler " + new { h.ClientCertificate });

            this.id = new { h.ClientCertificate }.ToString();

            this.status.Value = this.id;

        }
    }
}
