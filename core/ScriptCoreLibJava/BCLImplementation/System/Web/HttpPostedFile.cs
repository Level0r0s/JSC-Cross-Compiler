using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// namespace ScriptCoreLibJava.BCLImplementation.System.Web
namespace ScriptCoreLib.Android.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpPostedFile.cs

    [Script(Implements = typeof(global::System.Web.HttpPostedFile))]
    public class __HttpPostedFile
    {
        public int ContentLength { get; set; }

        public string ContentType { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
    }
}
