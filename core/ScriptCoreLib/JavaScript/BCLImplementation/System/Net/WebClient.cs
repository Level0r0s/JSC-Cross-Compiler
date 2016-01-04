﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using ScriptCoreLib.JavaScript.DOM;
using System.Threading.Tasks;
using System.Collections.Specialized;
using ScriptCoreLib.JavaScript.WebGL;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/webclient.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net/WebClient.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\WebClient.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Net\WebClient.cs

    [Script(Implements = typeof(global::System.Net.WebClient))]
    public class __WebClient
    {
        // . Many MIME user agents do not recognize multipart/related and treat these messages as multipart/mixed. 
        // To allow for this, some UAs will include the technically unnecessary Content-Disposition header in multipart/related body parts. Content-Location and Content-Base headers are defined to resolve URL references to other body parts. Both headers are valid in any message or body part

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201512/20151204

        // http://tech.slashdot.org/story/15/10/10/0158202/bbc-optimizing-uhd-video-streaming-over-ip
        // http://www.bbc.co.uk/rd/blog/2015/10/streaming-video-on-10-gigabit-ethernet-and-beyond




        // can we do encrypted/signed  web client yet?

        public WebHeaderCollection ResponseHeaders { get; set; }

        // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Library\Templates\JavaScript\InternalWebMethodRequest.cs

        public event UploadValuesCompletedEventHandler UploadValuesCompleted;

        public void UploadValuesAsync(Uri address, NameValueCollection data)
        {

            this.UploadValuesTaskAsync(
                address,
                data
            ).ContinueWith(
                t =>
                {
                    var e = new __UploadValuesCompletedEventArgs { Result = t.Result };

                    if (UploadValuesCompleted != null)
                        UploadValuesCompleted(null, (UploadValuesCompletedEventArgs)(object)e);

                }
            );

        }


        // called by
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\JavaScript\Remoting\InternalWebMethodRequest.cs
        public Task<byte[]> UploadValuesTaskAsync(Uri address, NameValueCollection data)
        {
            //Console.WriteLine("enter WebClient.UploadValuesTaskAsync! " + new { address });
            // Z:\jsc.svn\examples\javascript\Test\TestAfterInvokeResponseHeaders\ApplicationWebService.cs


            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/uploadvaluestaskasync
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/ubuntumidexperiment

            var r = new TaskCompletionSource<byte[]> { };

            // Z:\jsc.svn\examples\javascript\ubuntu\Test\UbuntuTestUploadValues\Application.cs
            //  "Z:\jsc.svn\examples\javascript\ubuntu\Test\UbuntuTestUserHostAddress\UbuntuTestUserHostAddress.sln"



            // http://stackoverflow.com/questions/9713058/sending-post-data-with-a-xmlhttprequest
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140119
            // http://stackoverflow.com/questions/8286934/post-formdata-via-xmlhttprequest-object-in-js-cross-browser
            // http://stackoverflow.com/questions/16917772/ie-lags-on-sending-post-data
            // http://robertnyman.com/2013/02/11/using-formdata-to-send-forms-with-xhr-as-keyvalue-pairs/
            // https://blog.yorkxin.org/posts/2014/02/06/ajax-with-formdata-is-broken-on-ie10-ie11/

            var x = new IXMLHttpRequest();

            // InvalidStateException
            //x.withCredentials = true;


            x.open(Shared.HTTPMethodEnum.POST, address.ToString(), async: true);
            x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");


            var xFormDataString = ToFormDataString(data);

            //Uncaught InvalidStateError: Failed to execute 'send' on 'XMLHttpRequest': the object's state must be OPENED.
            // X:\jsc.svn\examples\javascript\Test\TestUploadValuesAsync\TestUploadValuesAsync\Application.cs

            // UploadValuesAsync { responseType = , response = <document><TaskComplete><TaskResult>13</TaskResult></TaskComplete></document> }

            x.InvokeOnComplete(
                delegate
                {
                    //Console.WriteLine("after WebClient.UploadValuesTaskAsync! " + new { address });

                    #region complete
                    var response = new byte[0];

                    // UploadValuesAsync { status = 204, responseType = arraybuffer, response = [object Uint8ClampedArray] }

                    //if (x.status == 204)
                    // 304?
                    //if (x.status == IXMLHttpRequest.HTTPStatusCodes.NoContent)
                    //{
                    //    // android webview  wants us to do this
                    //    response = new byte[0];
                    //}

                    //Uncaught InvalidStateError: Failed to read the 'responseText' property from 'XMLHttpRequest': 
                    // The value is only accessible if the object's 'responseType' is '' or 'text' (was 'arraybuffer').

                    // X:\jsc.svn\examples\javascript\android\com.abstractatech.battery\com.abstractatech.battery\ApplicationWebService.cs
                    //Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType });

                    //I/chromium(10616): [INFO:CONSOLE(36216)] "%c0:576ms UploadValuesAsync { status = 204, responseType = arraybuffer }", source: http://192.168.1.103:10129/view-source (36216)
                    //I/chromium(10616): [INFO:CONSOLE(49940)] "Uncaught InvalidStateError: An attempt was made to use an object that is not, or is no longer, usable.", source: http://192.168.1.103:10129/view-source (49940)

                    // what about android webview?
                    if (x.response == null)
                    {
                        //Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, x.response, x.responseText });

                        //I/Web Console( 5012): %c0:198484ms UploadValuesAsync { status = 200, responseType = arraybuffer } at http://192.168.43.1:9417/view-source:37081
                        //I/Web Console( 5012): %c0:198500ms UploadValuesAsync { status = 200, responseType = arraybuffer, response = , responseText = <document><avatar><obj>aHR0cDovL3d3dy5ncmF2YXRhci5jb20vYXZhdGFyLzhlNmQzZGUw
                        //I/Web Console( 5012): %c0:198524ms InternalWebMethodRequest.Complete { Name = Gravatar, Length = 0 } at http://192.168.43.1:9417/view-source:37081

                        // did we not fix it already?
                        // android 2.3 only seems to have responseText

                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140413

                        try
                        {
                            response = Encoding.UTF8.GetBytes(x.responseText);
                        }
                        catch
                        {
                            //I/chromium(30556): [INFO:CONSOLE(37861)] "%c92:28288ms UploadValuesAsync { status = 204, responseType = arraybuffer }", source: http://192.168.43.7:4394/view-source (37861)
                            //I/chromium(30556): [INFO:CONSOLE(37861)] "%c92:28290ms responseText failed. thanks webview devs. { status = 204 }", source: http://192.168.43.7:4394/view-source (37861)

                            // X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\ApplicationWebService.cs
                            Console.WriteLine("responseText failed? " + new { x.status });
                        }
                    }
                    else
                    {
                        // http://stackoverflow.com/questions/8022425/getting-blob-data-from-xhr-request

                        var a = (ArrayBuffer)x.response;

                        //Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, a.byteLength });

                        // IE?
                        //var u8 = new Uint8Array(array: a);

                        // X:\jsc.svn\core\ScriptCoreLib.Async\ScriptCoreLib.Async\JavaScript\DOM\FileEntryAsyncExtensions.cs
                        var u8c = new Uint8ClampedArray(array: a);

                        response = u8c;
                    }

                    //Console.WriteLine("UploadValuesAsync " + new { x.status, x.responseType, response });


                    #region ResponseHeaders
                    this.ResponseHeaders = new WebHeaderCollection();
                    //this.ResponseHeaders.Clear();

                    var ResponseHeaders = x.getAllResponseHeaders();

                    // Z:\jsc.svn\examples\javascript\Test\TestAfterInvokeResponseHeaders\ApplicationWebService.cs
                    //Console.WriteLine("after WebClient.UploadValuesTaskAsync! " + new { ResponseHeaders });

                    //0:8209ms { ResponseHeaders = Date: Sat, 15 Mar 2014 12:25:45 GMT
                    //Server: ASP.NET Development Server/11.0.0.0
                    //X-AspNet-Version: 4.0.30319
                    //ETag: BqShORsRkdny750pWBdVyQ==
                    //X-ElapsedMilliseconds: 10
                    //Content-Type: text/xml; charset=utf-8
                    //Access-Control-Allow-Origin: *
                    //Cache-Control: private
                    //Connection: Close
                    //Content-Length: 239

                    foreach (var item in ResponseHeaders.Split('\n'))
                    {
                        var u = item.IndexOf(":");
                        if (u > 0)
                        {
                            var ukey = item.Substring(0, u);
                            var uvalue = item.Substring(u + 1).Trim();

                            //Console.WriteLine(new { ukey, uvalue });

                            this.ResponseHeaders[ukey] = uvalue;
                        }
                    }
                    #endregion

                    //Console.WriteLine(new { ResponseHeaders });


                    r.SetResult(response);

                    #endregion


                }
               );

            x.responseType = "arraybuffer";

            // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuSSLWebApplication\UbuntuSSLWebApplication\Application.cs
            // allow basejump?
            // breaks Task<> call?
            //x.responseType = "text";


            // is IE actually sending it? or is our server decoding it wrong?
            //Console.WriteLine("WebClient.UploadValuesAsync IXMLHttpRequest " + new { xFormDataString });
            x.send(xFormDataString);


            return r.Task;
        }


        // called by? UploadValuesTaskAsync
        public static string ToFormDataString(NameValueCollection data)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/ubuntumidexperiment

            // X:\jsc.svn\examples\javascript\Test\Test453ForEachStringArray\Test453ForEachStringArray\Class1.cs
            // X:\jsc.svn\examples\javascript\css\Test\TestLongWebMethod\TestLongWebMethod\Application.cs

            // IE is misbehaving?
            //Console.WriteLine("enter WebClient.ToFormDataString " + new { data.Count });

            #region AllKeys
            var xx = "";

            // for (e = 0; e; e++)
            foreach (var item in data.AllKeys)
            {
                //Console.WriteLine("WebClient.ToFormDataString " + new { item });


                if (xx != "")
                {
                    xx += "&";
                }



                var xdata = data[item];

                //Console.WriteLine("WebClient.ToFormDataString " + new { xdata });

                // X:\jsc.svn\examples\javascript\async\AsyncComputeAndThenCallServer\AsyncComputeAndThenCallServer\Application.cs
                // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/escape
                //var evalue = Native.window.escape(data[item]).Replace("+", "%" + ((byte)'+').ToString("x2"));
                var evalue = Native.escape(xdata).Replace("+", "%" + ((byte)'+').ToString("x2"));
                xx += item + "=" + evalue;
            }


            #endregion
            return xx;
        }


        // X:\jsc.svn\examples\javascript\Test\TestScriptApplicationIntegrity\TestScriptApplicationIntegrity\Application.cs
        //public Task<byte[]> DownloadDataTaskAsync(string address);

        #region DownloadString
        public event DownloadStringCompletedEventHandler DownloadStringCompleted;

        [Obsolete("this will not work if the baseURI has changed and worker is using blob: workaround")]
        public string DownloadString(string address)
        {

            // X:\jsc.svn\examples\javascript\Test\TestDownloadStringAsync\TestDownloadStringAsync\Application.cs

            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.GET, address.ToString(), async: false);

            //Console.WriteLine("DownloadStringAsync");
            x.send();

            return x.responseText;
        }

        // used by?
        public void DownloadStringAsync(Uri address)
        {
            var x = new IXMLHttpRequest();

            x.open(Shared.HTTPMethodEnum.GET, address.ToString());

            x.InvokeOnComplete(
                r =>
                {
                    //var e = new __DownloadStringCompletedEventArgs { Error = new Exception("Not implemented. (__WebClient.DownloadStringAsync)") };
                    var e = new __DownloadStringCompletedEventArgs { Result = r.responseText };

                    if (DownloadStringCompleted != null)
                        DownloadStringCompleted(null, (DownloadStringCompletedEventArgs)(object)e);
                }
            );

            //Console.WriteLine("DownloadStringAsync");
            x.send();

        }

        public Task<string> DownloadStringTaskAsync(Uri address)
        {
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs
            return DownloadStringTaskAsync(address.ToString());
        }

        public Task<string> DownloadStringTaskAsync(string address)
        {
            var z = new TaskCompletionSource<string>();

            var x = new IXMLHttpRequest();
            x.open(Shared.HTTPMethodEnum.GET, address);

            x.InvokeOnComplete(
                r =>
                {
                    z.SetResult(r.responseText);
                }
            );

            //Console.WriteLine("DownloadStringAsync");
            x.send();

            return z.Task;
        }
        #endregion

        // DownloadDataTaskAsync
        // X:\jsc.svn\examples\javascript\Test\TestScriptApplicationIntegrity\TestScriptApplicationIntegrity\Application.cs
        // shall we test it on a gif?
        // X:\jsc.svn\examples\javascript\io\GIFDecoderExperiment\GIFDecoderExperiment\Application.cs

        // NET 4.5
        public Task<byte[]> DownloadDataTaskAsync(string address)
        {
            // Z:\jsc.svn\examples\javascript\audio\StereoChannelVisualization\Application.cs

            var x = new IXMLHttpRequest();
            x.open(Shared.HTTPMethodEnum.GET, address);
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IXMLHttpRequest.cs

            return x.bytes;
        }



        public static implicit operator WebClient(__WebClient e)
        {
            return (WebClient)(object)e;
        }
    }
}
