using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Ultra.WebService
{
    /// <summary>
    /// This type is used to serve custom content from the web server.
    /// </summary>




    [Obsolete("experimental")]
    public class WebServiceHandler
    {
        // Z:\jsc.svn\examples\javascript\ubuntu\Test\TestApplicationTypeInfo\TestApplicationTypeInfo\Application.cs




        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httprequest
        // Z:\jsc.svn\examples\javascript\appengine\xml\AppEngineServerSideContent\Application.cs
        // X:\jsc.svn\examples\javascript\xml\ServerSideContent\ServerSideContent\ApplicationWebService.cs


        // saved before Serve and for Invoke
        public InternalWebMethodInfo WebMethod;





        // set by ssl port at login
        // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuSSLWebApplication\UbuntuSSLWebApplication\ApplicationWebService.cs
        public global::System.Security.Cryptography.X509Certificates.X509Certificate2 ClientCertificate;
        public System.Threading.Tasks.Task<string> ClientTrace;




        public HttpContext Context;

        public Action CompleteRequest;


        public Action Diagnostics;
        public Action Default;
        public Action Redirect;

        public WebServiceScriptApplication[] Applications;



        [Obsolete]
        public Func<InternalFileInfo[]> GetFiles;


        public bool IsDefaultPath
        {
            get
            {
                var e = this.Context.Request.Path;

                return InternalIsDefaultPath(e);
            }
        }

        internal static bool InternalIsDefaultPath(string e)
        {
            if (e == "/")
                return true;

            if (e == "/default.htm")
                return true;

            if (e == "/default.aspx")
                return true;

            return false;
        }


        public Action<WebServiceScriptApplication> WriteSource;




        //public static void 
    }
}
