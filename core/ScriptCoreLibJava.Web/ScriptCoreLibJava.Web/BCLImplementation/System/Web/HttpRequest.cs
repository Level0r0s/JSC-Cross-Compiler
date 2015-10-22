using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections.Specialized;
using System.Web;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;


using java.io;
using ScriptCoreLib.Android.BCLImplementation.System.Web;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
    [Script]
    static class __copy
    {
        public static string TakeUntilOrEmpty(this string e, string u)
        {
            var i = e.IndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(0, i);
        }

        public static string SkipUntilOrEmpty(this string e, string u)
        {
            if (null == e)
                return "";

            if (u == null)
                return "";


            var i = e.IndexOf(u);

            if (i < 0)
                return "";

            return e.Substring(i + u.Length);
        }
    }

    [Script(Implements = typeof(global::System.Web.HttpRequest))]
    internal class __HttpRequest
    {
        // check sdk!
        //Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Web\__HttpRequest.java:5: error: package javax.servlet.http does not exist
        //import javax.servlet.http.Cookie;
        //                         ^

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150817


        //[jsc.internal] UnhandledException:
        //{ FullName = System.InvalidOperationException, InnerException =  }

        //{ ExceptionObject = System.InvalidOperationException: Cannot process request because the process (5724) has exited.
        //   at System.Diagnostics.Process.GetProcessHandle(Int32 access, Boolean throwIfExited)
        //   at System.Diagnostics.Process.Kill()
        //   at jsc.meta.Commands.Configuration.ConfigurationDisposeSubst.<>c__DisplayClass2.<Monitor>b__1()
        //   at jsc.meta.Library.VolumeFunctions.VolumeFunctionsExtensions.ToVirtualDriveToDirectory.Dispose()

        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Web\HttpRequest.cs
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalHttpServlet.cs
        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalAndroidWebServiceActivity.cs

        // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
        public string UserHostAddress { get; set; }
        public string Path { get; set; }
        public string HttpMethod { get; set; }
        public NameValueCollection QueryString { get; internal set; }
        public NameValueCollection Form { get; internal set; }
        public NameValueCollection Headers { get; internal set; }

        public HttpCookieCollection Cookies { get; set; }


        // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs
        // http://stackoverflow.com/questions/4780474/appengine-howto-see-content-from-a-post-request

        public HttpFileCollection Files { get; set; }

        public javax.servlet.http.HttpServletRequest InternalContext;



        public Uri Url
        {
            get
            {
                return
                    new Uri(
                        this.InternalContext.getRequestURL().toString()
                    );

            }
        }





        public __HttpRequest()
        {
            this.Files = (HttpFileCollection)(object)new __HttpFileCollection();

            this.QueryString = new NameValueCollection();
            this.Form = new NameValueCollection();
            this.Headers = new NameValueCollection();
            this.Cookies = new HttpCookieCollection();

        }

        public Uri UrlReferrer
        {
            get
            {
                return new Uri(this.Headers["Referer"]);
            }
        }




        public string ContentType
        {
            get
            {
                var __Request = this;
                var __RequestContentType = __Request.Headers["Content-Type"];

                return __RequestContentType;
            }
        }



    }

   
}
