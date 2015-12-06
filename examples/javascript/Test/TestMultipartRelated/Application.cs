using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.Shared.IO;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestMultipartRelated;
using TestMultipartRelated.Design;
using TestMultipartRelated.HTML.Pages;

namespace TestMultipartRelated
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLButton { "check" }.AttachToDocument().onclick += async delegate
            {
                var lastupload = await base.getlastupload();

                lastupload.WithEachIndex(
                     (x, index) =>
                     {
                         foreach (var header in x.Headers)
                         {
                             //Console.WriteLine(new { index, header });
                             new IHTMLPre { new { index, header } }.AttachToDocument();

                         }

                         //Console.WriteLine(new { index, x.Content });
                         new IHTMLPre { new { index, x.Content } }.AttachToDocument();

                     }
                 );
            };

            new { }.With(
                async delegate
                {
                    // http://footle.org/2007/07/31/binary-multipart-posts-in-javascript/

                    // https://msdn.microsoft.com/en-us/library/ms527355(v=exchg.10).aspx

                    var send = new IHTMLButton { "send" }.AttachToDocument();

                    while (await send.async.onclick)
                    {



                        // is it useful?

                        //var f = new FormData();

                        //f.append("hello", "world");
                        //f.append("foo", "bar");

                        // http://stackoverflow.com/questions/5165337/xmlhttprequest-overridemimetype-for-outgoing-data
                        var x = new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload", true);

                        var doubleDash = "--";
                        var boundary = "12345678901234567890";


                        var wRequestPayload = new StringBuilder();

                        wRequestPayload.Append(@"--12345678901234567890
Content-Type: text/xml; charset=UTF-8


<xml1>hello</xml1>

--12345678901234567890
Content-type: text/xml
e-x: 34
e-y: pp

<xml1>world</xml1>
--12345678901234567890--"
                         );


                        var data = wRequestPayload.ToString();

                        x.setRequestHeader("Content-Type", "multipart/related; boundary=" + boundary);

                        // java stream hangs if we try to read past data?
                        //x.setRequestHeader("Content-Length", "" + data.Length);



                        x.send(data);

                    }

                    //x.overrideMimeType()

                }
            );

        }

    }

    // for 3rd party comptability
    public class HTTPHeadersWithXElement
    {
        public string[] Headers;
        public XElement Content;
    }

    public partial class ApplicationWebService
    {


        static HTTPHeadersWithXElement[] InputStreamToXElements(string boundary, Stream InputStream)
        {
            //Console.WriteLine("enter InputStreamToXElements " + new { InputStream, InputStream.Position });
            Console.WriteLine("enter InputStreamToXElements " + new { InputStream });
            var a = new List<HTTPHeadersWithXElement> { };

            var rSmartStreamReader = new ScriptCoreLib.Shared.IO.SmartStreamReader(InputStream);


            //var dump = rSmartStreamReader.ReadToEnd();

            //Console.WriteLine(new { dump });

            //return a.ToArray();

            //MultipartReader
            var header0 = rSmartStreamReader.ReadLine();


            while (header0 == "--" + boundary)
            {
                // opening multipart..
                header0 = "";

                var header2 = rSmartStreamReader.ReadLine();
                var ContentType = header2.SkipUntilOrEmpty("Content-Type:");


                var hlist = new List<string> { };
                var headers = true;
                while (headers)
                {
                    var header3 = rSmartStreamReader.ReadLine();

                    headers = !string.IsNullOrEmpty(header3);

                    if (headers)
                    {
                        hlist.Add(header3);
                    }
                }

                {
                    // read data
                    // http://www.w3.org/TR/html401/interact/forms.html#h-17.13.4.2
                    // X:\jsc.svn\examples\rewrite\TestReadToBoundary\TestReadToBoundary\Program.cs

                    var data = rSmartStreamReader.ReadToBoundary("--" + boundary);




                    var xstring = Encoding.UTF8.GetString(data.ToArray());
                    var x = XElement.Parse(xstring);

                    a.Add(new HTTPHeadersWithXElement { Headers = hlist.ToArray(), Content = x });


                    var header4 = rSmartStreamReader.ReadLine();
                    header0 = header4;
                    // which is it, the end or is there more?
                    //Console.WriteLine("#" + cid + " " + new { header4 });

                    //I/System.Console( 6761): #5 { Length = 256 }
                    //I/System.Console( 6761): #5 { header4 = ------WebKitFormBoundaryrn6MyQlGvhETZkjw-- }
                    // the end?
                }
            }


            return a.ToArray();
        }

        // whatif clientside were to be allowed to async read server statics?
        static HTTPHeadersWithXElement[] lastupload;

        public async Task<HTTPHeadersWithXElement[]> getlastupload() { return lastupload; }


        public void Handler(WebServiceHandler h)
        {
            if (h.Context.Request.Path == "/upload")
            {

                //var char5 = '5';
                //var w5 = new StringBuilder().Append(char5);

                //Console.WriteLine(new { w5 });



                // http://stackoverflow.com/questions/12189338/java-socket-inputstream-hangs-blocks-on-both-client-and-server
                var ContentLength = h.Context.Request.ContentLength;
                // h.Context.Request.ContentLength = 189

                // chrome seems to always just send the le per data
                // need a tool to not send it?


                // { ContentType = multipart/related; boundary=12345678901234567890, multipart_related_boundary =  }
                var multipart_related_boundary = h.Context.Request.ContentType.SkipUntilOrEmpty("multipart/related; boundary=");

                Console.WriteLine(new { ContentLength, h.Context.Request.ContentType, multipart_related_boundary });

                if (!string.IsNullOrEmpty(multipart_related_boundary))
                {
                    var boundary = multipart_related_boundary;

                    Console.WriteLine(new { boundary });

                    // cassini wont allow to read input bytes if thee is no content length?

                    //h.Context.Request.InputStream.bas

                    //public Stream GetBufferlessInputStream();
                    //public Stream GetBufferlessInputStream(bool disableMaxRequestLength);

                    //var BufferlessInputStream = h.Context.Request.GetBufferlessInputStream(disableMaxRequestLength: true);


                    //var byte0 = h.Context.Request.InputStream.ReadByte();
                    // byte0 = -1


                    //upload { InputStream = TestMultipartRelated.ApplicationWebService__i__d.Internal.Shared.IO.LengthLimitedStream@2a0e7630 }
                    //{ byte0 = 0 }



                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201512/20151206
                    // why do we see byte 0?
                    Console.WriteLine("upload " + new { h.Context.Request.InputStream });
                    //var byte0 = BufferlessInputStream.ReadByte();
                    // byte0 = -1

                    //var byte0 = h.Context.Request.InputStream.ReadByte();
                    //Console.WriteLine(new { byte0 });

                    //var byte1 = h.Context.Request.InputStream.ReadByte();
                    //Console.WriteLine(new { byte1 });


                    //var a = new SmartStreamReader(h.Context.Request.InputStream);

                    //var all = a.ReadToEnd();

                    ////var all = h.Context.Request.InputStream.ReadToEnd();


                    //h.Context.Response.StatusCode = 204;
                    //h.CompleteRequest();
                    //Console.WriteLine("upload discarded. " + new { all.Length });

                    //return;


                    // testing gearvr?
                    lastupload = InputStreamToXElements(boundary, h.Context.Request.InputStream);

                    lastupload.WithEachIndex(
                        (x, index) =>
                        {
                            foreach (var header in x.Headers)
                            {
                                Console.WriteLine(new { index, header });

                            }

                            Console.WriteLine(new { index, x.Content });

                        }
                    );

                    h.Context.Response.StatusCode = 204;
                    h.CompleteRequest();
                    Console.WriteLine("upload ok.");
                    return;
                }

                // http://www.intstrings.com/ramivemula/articles/file-upload-using-multipartformdatastreamprovider-in-asp-net-webapi/
                //h.Context.Request.content

                //MultipartFormDataContent

                // [System.Web.HttpValueCollection] = {hello=world&foo=bar}
                // 		ContentType	"multipart/related; boundary=12345678901234567890"	string


                Console.WriteLine("upload fail.");
            }
        }
    }
}
