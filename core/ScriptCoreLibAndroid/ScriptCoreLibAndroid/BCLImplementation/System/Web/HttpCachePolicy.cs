
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpCachePolicy.cs
    // see also: Y:\jsc.svn\core\ScriptCoreLib\PHP\BCLImplementation\System\Web\HttpCachePolicy.cs

    [Script(Implements = typeof(global::System.Web.HttpCachePolicy))]
    public class __HttpCachePolicy
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151022/httpapplication
        // tested by?

        //public __HttpResponse InternalResponse;

        public Action<HttpCacheability> vSetCacheability;
        public void SetCacheability(HttpCacheability cacheability)
        {
            // set by
            // Z:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Web\HttpResponse.cs

            if (vSetCacheability != null)
                vSetCacheability(cacheability);



        }

        public Action<DateTime> vSetExpires;
        public void SetExpires(DateTime date)
        {
            if (vSetExpires != null)
                vSetExpires(date);
        }
    }
}
