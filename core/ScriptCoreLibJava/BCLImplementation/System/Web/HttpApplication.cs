using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using java.io;
using System.IO;
using ScriptCoreLibJava.BCLImplementation.System.IO;


namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpApplication.cs

    [Script(Implements = typeof(global::System.Web.HttpApplication))]
    public class __HttpApplication : __IHttpAsyncHandler, __IHttpHandler, __IComponent, IDisposable
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151021
        // X:\jsc.svn\examples\java\android\ApplicationWebService\ApplicationWebService\ApplicationActivity.cs
        // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuWebApplication\ApplicationWebService.cs

        public HttpRequest Request { get; set; }
        public HttpResponse Response { get; set; }

        __HttpContext _Context;
        public HttpContext Context
        {
            get
            {
                if (_Context == null)
                    _Context = new __HttpContext { Request = this.Request, Response = this.Response };

                return (HttpContext)(object)_Context;
            }
        }

        public void CompleteRequest()
        {
            this.Response.Close();
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessRequest(global::System.Web.HttpContext context)
        {
            throw new NotImplementedException();
        }

        public event EventHandler Disposed;

        public void Dispose()
        {
            CompleteRequest();
        }

        public global::System.ComponentModel.ISite Site
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }



    }
}
