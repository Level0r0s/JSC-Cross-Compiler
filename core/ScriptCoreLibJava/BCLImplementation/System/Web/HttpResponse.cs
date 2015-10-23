using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLibJava.BCLImplementation.System.IO;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
using System.Net.Sockets;
using System.Web;
using ScriptCoreLib.Android.BCLImplementation.System.Web;
using System.Collections.Specialized;

namespace ScriptCoreLibJava.BCLImplementation.System.Web
{
    // http://referencesource.microsoft.com/#System.Web/xsp/system/Web/HttpResponse.cs

    [Script(Implements = typeof(global::System.Web.HttpResponse))]
    public class __HttpResponse
    {
        //public javax.servlet.http.HttpServletResponse InternalContext;

        #region StatusCode
        internal int InternalStatusCode;
        public Action<int> vStatusCode;
        public int StatusCode
        {
            get
            {
                return InternalStatusCode;
            }
            set
            {
                InternalStatusCode = value;

                if (vStatusCode != null)
                    vStatusCode(value);
            }
        }
        #endregion




        #region ContentType
        internal string InternalContentType;
        public Action<string> vContentType;
        public string ContentType
        {
            get
            {
                return InternalContentType;
            }

            set
            {
                InternalContentType = value;
                if (vContentType != null)
                    vContentType(value);
            }
        }
        #endregion


        public void Write(object s)
        {

            Write("" + s);
        }

        public void Write(string s)
        {
            // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs

            try
            {
                //this.InternalContext.getWriter().print(s);

                var bytes = Encoding.UTF8.GetBytes(s);


                this.OutputStream.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                throw;
            }
        }




        #region Redirect
        public Action<string> vRedirect;
        public void Redirect(string url)
        {
            if (vRedirect != null)
                vRedirect(url);
        }
        #endregion

        #region OutputStream
        public NetworkStream InternalOutputStream;
        public Func<NetworkStream> vInternalOutputStream;
        public Stream OutputStream
        {
            get
            {
                if (this.InternalOutputStream == null)
                    if (this.vInternalOutputStream != null)
                        this.InternalOutputStream = this.vInternalOutputStream();



                return this.InternalOutputStream;
            }
        }
        #endregion



        public NameValueCollection Headers { get; set; }


        #region AddHeader
        public Action<string, string> vAddHeader;
        public void AddHeader(string name, string value)
        {
            if (vAddHeader != null)
                vAddHeader(name, value);
        }
        #endregion


        // called by?
        public void WriteFile(string filename)
        {
            Console.WriteLine("enter WriteFile " + new { filename });
            Console.WriteLine("enter WriteFile " + new { typeof(__HttpResponse).Assembly.Location });
            Console.WriteLine("enter WriteFile " + new { Environment.CurrentDirectory });

            // we only work with absolute paths anyway
            if (filename.StartsWith("/"))
                filename = filename.Substring(1);

            // X:\jsc.smokescreen.svn\core\javascript\com.abstractatech.analytics\com.abstractatech.analytics\ApplicationWebService.cs
            // should we also report file size?

            try
            {
                var bytes = File.ReadAllBytes(filename);

                Console.WriteLine("WriteFile " + new { filename, bytes.Length });

                this.OutputStream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception fault)
            {
                Console.WriteLine("fault WriteFile " + new { filename, fault.Message });
            }
        }

        public __HttpCachePolicy Cache
        {
            get
            {
                return new __HttpCachePolicy { };
            }
        }



        public Action vClose;
        public void Close()
        {
            if (this.vClose != null)
                this.vClose();

        }

        public Action vFlush;
        public void Flush()
        {
            if (vFlush != null)
                vFlush();

        }




        #region SetCookie
        public HttpCookieCollection Cookies { get; set; }

        public __HttpResponse()
        {
            Cookies = new HttpCookieCollection();
            Headers = new NameValueCollection { };
        }

        public void SetCookie(HttpCookie e)
        {
            // http://en.wikipedia.org/wiki/HTTP_cookie

            // Set-Cookie:session="eyB0aWNrcyA9IDYzNDkzNzg5MDQyMzM5MDAwMCwgYWNjb3VudCA9IDEsIGNvbW1lbnQgPSB3ZSBzaGFsbCBTSEExIHRoaXMhIH0="
            // Set-Cookie:session=eyB0aWNrcyA9IDYzNDkzNzk2NTU3NzczMjI5MiwgYWNjb3VudCA9IDIsIGNvbW1lbnQgPSB3ZSBzaGFsbCBTSEExIHRoaXMhIH0=; path=/

            this.AddHeader("Set-Cookie",
                e.Name + "=" + e.Value + ";  path=/");

        }
        #endregion


        public bool IsClientConnected
        {
            get;
            set;
        }
    }
}
