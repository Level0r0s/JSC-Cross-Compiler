using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestAfterInvokeResponseHeaders
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // can we use it in UDP to NDK yet?
        //public VerifiableString foo = new VerifiableString { "guest" };
        public VerifiableString foo = new VerifiableString { value = "guest" };

        // redux?

        //public async Task Invoke1()
        public async Task<string> Invoke1()
        {
            // could we do this in NDK yet? could the rewriter make it a stringbuilder instead?
            // or even a consolebuilder?
            Console.WriteLine(new { foo });

            //{ foo = { value = guest } }
            //{ foo =  }


            //return delegate
            //{
            //    // can we detect this is client code ?
            //    new IHTMLPre { new { foo } }.AttachToDocument();
            //};

            return "200 some content";
            // ok. where is the client reading the values?

        }



        // .field-field_foo:<_02000013>%0d%0a  <_04000021>Z3Vlc3Q=</_04000021>%0d%0a</_02000013>
        // _06000001_field_foo:&lt;_02000013&gt;&lt;_04000021&gt;Z3Vlc3Q=&lt;/_04000021&gt;&lt;/_02000013&gt;

    }
}

//view-source:54800 1804ms after WebClient.UploadValuesTaskAsync! { ResponseHeaders = 

//.field-field_foo: <_02000013>%0d%0a  <_04000021>Z3Vlc3Q=</_04000021>%0d%0a</_02000013>

//Date: Mon, 04 Jan 2016 17:02:00 GMT
//Server: ASP.NET Development Server/11.0.0.0
//X-AspNet-Version: 4.0.30319
//ETag: ffozEMbpgfuYyjFXJSoCow==
//X-ElapsedMilliseconds: 2
//Content-Type: text/xml; charset=utf-8
//Access-Control-Allow-Origin: *
//Cache-Control: private
//Connection: Close
//Content-Length: 99
// }




//view-source:54811 1ms enter GetInternalFields
//2016-01-04 19:43:59.160 view-source:54811 34ms NewInstanceConstructor restore fields..
//2016-01-04 19:44:04.781 view-source:54811 5655ms InternalWebMethodRequest.Invoke { Name = Invoke1 }
//2016-01-04 19:44:04.783 view-source:54811 5658ms enter WebClient.UploadValuesTaskAsync! { address = /xml/Invoke1 }
//2016-01-04 19:44:05.987 view-source:54811 6861ms after WebClient.UploadValuesTaskAsync! { address = /xml/Invoke1 }
//2016-01-04 19:44:05.988 view-source:54811 6862ms after WebClient.UploadValuesTaskAsync! { ResponseHeaders = .field-field_foo: <_02000013>%0d%0a  <_04000021>Z3Vlc3Q=</_04000021>%0d%0a</_02000013>
//Date: Mon, 04 Jan 2016 17:44:05 GMT
//Server: ASP.NET Development Server/11.0.0.0
//X-AspNet-Version: 4.0.30319
//ETag: ffozEMbpgfuYyjFXJSoCow==
//X-ElapsedMilliseconds: 2
//Content-Type: text/xml; charset=utf-8
//Access-Control-Allow-Origin: *
//Cache-Control: private
//Connection: Close
//Content-Length: 99
// }
//2016-01-04 19:44:05.988 view-source:54811 6863ms { ukey = .field-field_foo, uvalue = <_02000013>%0d%0a  <_04000021>Z3Vlc3Q=</_04000021>%0d%0a</_02000013> }
//2016-01-04 19:44:05.989 view-source:54811 6863ms { ukey = Date, uvalue = Mon, 04 Jan 2016 17:44:05 GMT }
//2016-01-04 19:44:05.989 view-source:54811 6863ms { ukey = Server, uvalue = ASP.NET Development Server/11.0.0.0 }
//2016-01-04 19:44:05.989 view-source:54811 6863ms { ukey = X-AspNet-Version, uvalue = 4.0.30319 }
//2016-01-04 19:44:05.989 view-source:54811 6863ms { ukey = ETag, uvalue = ffozEMbpgfuYyjFXJSoCow== }
//2016-01-04 19:44:05.989 view-source:54811 6863ms { ukey = X-ElapsedMilliseconds, uvalue = 2 }
//2016-01-04 19:44:05.989 view-source:54811 6863ms { ukey = Content-Type, uvalue = text/xml; charset=utf-8 }
//2016-01-04 19:44:05.990 view-source:54811 6864ms { ukey = Access-Control-Allow-Origin, uvalue = * }
//2016-01-04 19:44:05.990 view-source:54811 6864ms { ukey = Cache-Control, uvalue = private }
//2016-01-04 19:44:05.990 view-source:54811 6864ms { ukey = Connection, uvalue = Close }
//2016-01-04 19:44:05.990 view-source:54811 6864ms { ukey = Content-Length, uvalue = 99 }
//2016-01-04 19:44:05.990 view-source:54811 6865ms { ukey = , uvalue =  }
//2016-01-04 19:44:05.991 view-source:54811 6865ms InternalWebMethodRequest.Invoke complete { Name = Invoke1 }
//2016-01-04 19:44:05.991 view-source:54811 6866ms enter InternalWebMethodRequest.Complete
//2016-01-04 19:44:05.998 view-source:54811 6873ms enter GetInternalFields
//2016-01-04 19:44:06.000 view-source:54811 6874ms GetInternalFields { FieldName = field_foo, FieldValue = <_02000013>%0d%0a  <_04000021>Z3Vlc3Q=</_04000021>%0d%0a</_02000013> }