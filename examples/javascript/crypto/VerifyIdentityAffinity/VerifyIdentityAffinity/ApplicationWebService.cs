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

namespace VerifyIdentityAffinity
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // the idea of this experiment is
        // to allow the https user to establish the client certificate
        // either by smartcard or by MID
        // after the identity is established the rest of the api
        // can inspect the claim signed by local webserviceauthority.

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151105/namedkey

        //public async Task GetStatus(XElement status)
        // can we modify the UI like this? no not yet

        // this is synced back to ui
        public XElement status;

        public async Task GetStatus()
        {
            status.Value = "guest";
        }

    }
}
//Error	4	Async methods cannot have ref or out parameters	Z:\jsc.svn\examples\javascript\crypto\VerifyIdentityAffinity\VerifyIdentityAffinity\ApplicationWebService.cs	31	37	VerifyIdentityAffinity

