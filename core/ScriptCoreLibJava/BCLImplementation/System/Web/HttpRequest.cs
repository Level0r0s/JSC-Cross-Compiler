using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpRequest.cs

    [Script(Implements = typeof(global::System.Web.HttpRequest))]
    public sealed class __HttpRequest
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httpapplication
        // how does a jsc app get access to here?
        // via Z:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\WebServiceHandler.cs
        // would there be a better way to expose it? 
        // see also 
        // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuWebApplication\ApplicationWebService.cs
        // Z:\jsc.svn\examples\javascript\xml\ServerSideContent\ServerSideContent\Application.cs


        // X:\jsc.svn\core\ScriptCoreLibJava.Web\ScriptCoreLibJava.Web\BCLImplementation\System\Web\HttpRequest.cs
        // X:\jsc.svn\examples\java\android\ApplicationWebService\ApplicationWebService\ApplicationActivity.cs
        // Z:\jsc.svn\examples\javascript\appengine\test\TestUserHostAddress\Application.cs
        public string UserHostAddress { get; set; }

        public string Path { get; set; }

        public string HttpMethod { get; set; }


        // Url?

        public NameValueCollection QueryString { get; internal set; }

        public NameValueCollection Form { get; internal set; }

        public NameValueCollection Headers { get; internal set; }

        public HttpCookieCollection Cookies { get; set; }

        public __HttpRequest()
        {
            this.Files = (HttpFileCollection)(object)new __HttpFileCollection();
            this.QueryString = new NameValueCollection();
            this.Form = new NameValueCollection();
            this.Headers = new NameValueCollection();
            this.Cookies = new HttpCookieCollection();
        }


        public HttpFileCollection Files { get; set; }


        public Uri Url { get; set; }


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

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150817
        // X:\jsc.svn\examples\javascript\appengine\AppEngineUserAgentLoggerWithXSLXAsset\AppEngineUserAgentLoggerWithXSLXAsset\ApplicationWebService.cs
        // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs
        // http://stackoverflow.com/questions/4780474/appengine-howto-see-content-from-a-post-request

        // Z:\jsc.svn\examples\javascript\test\TestMultipartRelated\Application.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201512/20151204
        //public Stream InputStream { get; internal set; }
        public Stream InputStream { get; set; }

        public int ContentLength { get; set; }

    }
}
