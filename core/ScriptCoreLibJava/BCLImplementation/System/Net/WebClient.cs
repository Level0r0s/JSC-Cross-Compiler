using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using java.net;
using java.io;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System.Collections.Specialized;
using System.Threading;
using ScriptCoreLib.Shared.BCLImplementation.System.Net;
using System.IO;
using javax.net.ssl;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Net
{
    // http://referencesource.microsoft.com/#System/net/System/Net/webclient.cs
    // https://github.com/mono/mono/tree/master/mcs/class/System/System.Net/WebClient.cs
    // https://github.com/dot42/api/blob/master/System/Net/WebClient.cs

    [Script(Implements = typeof(global::System.Net.WebClient))]
    internal class __WebClient : __Component
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140212/w

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\WebClient.cs

        public Encoding Encoding { get; set; }

        public WebHeaderCollection Headers { get; set; }


        //required: ScriptCoreLib.Shared.BCLImplementation.System.Net.__WebHeaderCollection
        //found:    ScriptCoreLibJava.BCLImplementation.System.Net.__WebHeaderCollection
        public __WebClient()
        {
            var x = new __WebHeaderCollection();
            this.Headers = (WebHeaderCollection)(object)x;
        }

        public byte[] UploadValues(string address, NameValueCollection data)
        {
            return UploadValues(new Uri(address), data);
        }

        public byte[] UploadValues(Uri address, NameValueCollection data)
        {
            // http://www.xyzws.com/Javafaq/how-to-use-httpurlconnection-post-data-to-web-server/139
            // http://stackoverflow.com/questions/3038176/httpurlconnection-does-not-read-the-whole-respnse

            var addressString = address.ToString();
            //Console.WriteLine("enter UploadValues " + new { addressString });




            //             String urlParameters =
            //"fName=" + URLEncoder.encode("???", "UTF-8") +
            //"&lName=" + URLEncoder.encode("???", "UTF-8")

            var m = new MemoryStream();

            try
            {
                //Console.WriteLine("before urlParameters");
                #region urlParameters
                var urlParameters = new StringBuilder();

                //Implementation not found for type import :
                //type: System.Collections.Specialized.NameObjectCollectionBase
                //method: KeysCollection get_Keys()
                //Did you forget to add the [Script] attribute?
                //Please double check the signature!

                //foreach (string key in data.Keys)

                foreach (string key in data.AllKeys)
                {
                    if (urlParameters.Length > 0)
                        urlParameters.Append("&");


                    urlParameters.Append(
                        key + "=" + URLEncoder.encode(data[key], "UTF-8")
                    );

                }
                #endregion

                //Console.WriteLine("after urlParameters");

                //            Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Net\__WebClient___c__DisplayClass2.java:60: error: unreported exception UnsupportedEncodingException; must be caught or declared to be thrown
                //builder0.Append(__String.Concat(string1, "=", URLEncoder.encode(this.data.get_Item(string1), "UTF-8")));
                //                                                               ^

                //Console.WriteLine(
                //    new { addressString }
                //);

                var url = new java.net.URL(addressString);

                // https://developer.android.com/training/articles/security-ssl.html

                var connection = (HttpURLConnection)url.openConnection();
                var https = connection as HttpsURLConnection;
                if (https != null)
                {
                    Console.WriteLine(new { https });
                }


                connection.setUseCaches(false);
                connection.setDoInput(true);
                connection.setDoOutput(true);
                connection.setChunkedStreamingMode(0);

                // Numeric status code, 403: Forbidden

                // UserAgent:  Java/1.7.0_45
                //HtmlElement: Access denied | my.monese.com used CloudFlare to restrict access</title>
                //- Http: Request, POST /xml/GetCurrencyRateBasedOnString 
                //   Command: POST
                // + URI: /xml/GetCurrencyRateBasedOnString
                //   ProtocolVersion: HTTP/1.1
                // + ContentType:  application/x-www-form-urlencoded
                //   Cache-Control:  no-cache
                //   Pragma:  no-cache
                //   UserAgent:  Java/1.7.0_45
                //   Host:  my.monese.com
                //   Accept:  text/html, image/gif, image/jpeg, *; q=.2, */*; q=.2
                //   Connection:  keep-alive
                //   ContentLength:  106
                //   HeaderEnd: CRLF

                //- Http: Request, POST /xml/GetCurrencyRateBasedOnString 
                //   Command: POST
                // + URI: /xml/GetCurrencyRateBasedOnString
                //   ProtocolVersion: HTTP/1.1
                // + ContentType:  application/x-www-form-urlencoded
                //   Host:  my.monese.com
                //   ContentLength:  106
                //   Expect:  100-continue
                //   Connection:  Keep-Alive
                //   HeaderEnd: CRLF


                //                error { Message = Connection timed out: connect, StackTrace = java.net.ConnectException: Connection timed out: connect
                //at java.net.DualStackPlainSocketImpl.connect0(Native Method)

                connection.setRequestMethod("POST");

                // https://issues.jenkins-ci.org/browse/JENKINS-21033?page=com.atlassian.jira.plugin.system.issuetabpanels:all-tabpanel
                // https://github.com/scalaj/scalaj-http/issues/27

                connection.setRequestProperty("User-Agent", "WebClient");
                connection.setRequestProperty("Accept", "application/xml");

                connection.setRequestProperty(
                    "Content-Type", "application/x-www-form-urlencoded"
                     );

                var bytes = Encoding.UTF8.GetBytes(
                    urlParameters.ToString()
                );


                connection.setRequestProperty("Content-Length", "" + bytes.Length);

                //connection.setRequestProperty("Content-Language", "en-US");  


                if (Headers != null)
                {
                    foreach (string key in Headers.AllKeys)
                        connection.addRequestProperty(key, Headers[key]);
                }

                //Console.WriteLine("before writeBytes " + new { bytes.Length });

                #region Send request
                var wr = new DataOutputStream(
                            connection.getOutputStream());

                wr.write((sbyte[])(object)bytes);

                //wr.writeBytes(urlParameters.ToString());
                wr.flush();
                #endregion

                //error { Message = Server returned HTTP response code: 403 for URL: 
                //        at sun.net.www.protocol.http.HttpURLConnection.getInputStream(Unknown Source)
                //        at ScriptCoreLibJava.BCLImplementation.System.Net.__WebClient___c__DisplayClass2._UploadValuesAsync_b__1(__WebClient___c__DisplayClass2.java:82)

                //Console.WriteLine("before Read ");

                // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\WebClient.cs

                //Get Response	
                // namespace java.io

                var asw = Stopwatch.StartNew();

                var ResponseCode = connection.getResponseCode();

                //Console.WriteLine("awaiting for input...");
                var xis = connection.getInputStream().ToNetworkStream();

                var buffer = new byte[0x4000];
                //var buffer = new byte[0x10000];

                // do we have to wait on android?
                //Console.WriteLine(new { xis.DataAvailable, asw.ElapsedMilliseconds });

                //var ss = xis.Read(buffer, 0, 0);

                //Console.WriteLine(new { ss, xis.DataAvailable, asw.ElapsedMilliseconds });


                //I/System.Console( 7821): { DataAvailable = false, ElapsedMilliseconds = 8278 }
                //I/System.Console( 7821): awaiting for input... { s = 2730 }
                //I/System.Console( 7821): awaiting for input... { s = 1340 }
                //I/System.Console( 7821): awaiting for input... { s = 438 }
                //I/System.Console( 7821): awaiting for input... { s = -1 }
                //I/System.Console( 7821): bytes: {{ Length = 4508 }}
                //I/System.Console( 7821): source: {{ Length = 4496 }}

                //I/System.Console(10970): { DataAvailable = true, ElapsedMilliseconds = 236 }
                //I/System.Console(10970): { ss = 0, DataAvailable = true, ElapsedMilliseconds = 237 }

                //var ok = true;

                while (xis.DataAvailable)
                //while (ok)
                {
                    var s = xis.Read(buffer, 0, buffer.Length);
                    //Console.WriteLine("awaiting for input... " + new { s });

                    //if (s < 0)
                    //{
                    //    ok = false;
                    //}
                    //else 
                    if (s > 0)
                        m.Write(buffer, 0, s);
                }

                //wr.close();
                //xis.Close();
                if (connection != null)
                {
                    connection.disconnect();
                }
                //xis.Read(
                //xis.readall
            }
            catch (Exception ex)
            {
                //error { Message = failed to connect to apps.emta.ee/213.184.49.80 (port 80): connect failed: ETIMEDOUT (Connection timed out), StackTrace = java.net.ConnectException: failed to connect to apps.emta.ee/213.184.49.80 (port 80): connect failed: ETIMEDOUT (Connection timed out)
                //       at libcore.io.IoBridge.connect(IoBridge.java:114)
                //       at java.net.PlainSocketImpl.connect(PlainSocketImpl.java:192)
                //       at java.net.PlainSocketImpl.connect(PlainSocketImpl.java:459)
                //       at java.net.Socket.connect(Socket.java:843)
                //       at com.android.okhttp.internal.Platform.connectSocket(Platform.java:131)
                //       at com.android.okhttp.Connection.connect(Connection.java:101)
                //       at com.android.okhttp.internal.http.HttpEngine.connect(HttpEngine.java:294)
                //       at com.android.okhttp.internal.http.HttpEngine.sendSocketRequest(HttpEngine.java:255)
                //       at com.android.okhttp.internal.http.HttpEngine.sendRequest(HttpEngine.java:206)
                //       at com.android.okhttp.internal.http.HttpURLConnectionImpl.execute(HttpURLConnectionImpl.java:345)
                //       at com.android.okhttp.internal.http.HttpURLConnectionImpl.connect(HttpURLConnectionImpl.java:89)
                //       at com.android.okhttp.internal.http.HttpURLConnectionImpl.getOutputStream(HttpURLConnectionImpl.java:197)

                // ?
                Console.WriteLine("error " + new { ex.Message, ex.StackTrace });

            }

            //Thread.Sleep(666);
            var Result = m.ToArray();

            return Result;
        }


        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Net\UploadValuesCompletedEventArgs.cs
        public event UploadValuesCompletedEventHandler UploadValuesCompleted;

        public void UploadValuesAsync(Uri address, NameValueCollection data)
        {
            // X:\jsc.svn\examples\java\Test\JVMCLRWebClient\JVMCLRWebClient\Program.cs

            //{ addressString = http://my.monese.com:80/xml/GetCurrencyRateBasedOnString }
            //before writeBytes { urlParameters = _06000039_currency=RVVS&WebMethodMetadataToken=06000039&WebMethodMetadataName=GetCurrencyRateBasedOnString }


            new Thread(
                delegate()
                {
                    var Result = UploadValues(address, data);


                    Console.WriteLine("yield UploadValuesAsync " + new { Result.Length });


                    RaiseUploadValuesCompleted(Result);
                }
            ) { IsBackground = true }.Start();
        }

        private void RaiseUploadValuesCompleted(byte[] Result)
        {
            //                    - javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRWebClient\Program.java
            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Net\__WebClient___c__DisplayClass3.java:106: error: UploadValuesCompleted has private access in __WebClient
            //        if (!(this.__4__this.UploadValuesCompleted == null))
            //                            ^


            if (UploadValuesCompleted != null)
                UploadValuesCompleted(
                    this,
                    (UploadValuesCompletedEventArgs)(object)new __UploadValuesCompletedEventArgs
                    {
                        Result = Result
                    }
                    );
        }



        #region DownloadString
        public string DownloadString(string u)
        {
            return DownloadString(new Uri(u));
        }

        public string DownloadString(Uri u)
        {
            var w = new StringBuilder();

            try
            {
                var url = new java.net.URL(u.ToString());
                var i = new java.io.InputStreamReader(url.openStream(), "UTF-8");
                var reader = new java.io.BufferedReader(i);

                // can't we just read to the end?
                var line = reader.readLine();
                while (line != null)
                {
                    w.AppendLine(line);

                    line = reader.readLine();
                }
                reader.close();
            }
            catch
            {
                // oops
            }

            return w.ToString();
        }
        #endregion









        // http://www.rgagnon.com/javadetails/java-fix-certificate-problem-in-HTTPS.html


        [Script]
        class localX509TrustManager : X509TrustManager
        {
            public void checkServerTrusted(java.security.cert.X509Certificate[] chain, string authType)
            {
                //Console.WriteLine("enter checkServerTrusted");
            }

            public void checkClientTrusted(java.security.cert.X509Certificate[] chain, string authType)
            {
                //Console.WriteLine("enter checkClientTrusted");
            }

            public java.security.cert.X509Certificate[] getAcceptedIssuers()
            {
                //Console.WriteLine("enter getAcceptedIssuers");
                return null;
            }
        }

        [Script]
        class localHostnameVerifier : HostnameVerifier
        {

            public bool verify(string hostname, SSLSession session)
            {
                //Console.WriteLine("localHostnameVerifier " + new { hostname });

                return true;
            }
        }
        #region UploadString

        public string UploadString(string u, string data)
        {
            // Z:\jsc.svn\examples\java\hybrid\JVMCLRWSDLMID\Program.cs

            return UploadString(new Uri(u), method: "POST", data: data);
        }

        public string UploadString(string u, string method, string data)
        {
            return UploadString(new Uri(u), method: method, data: data);
        }

        public Task<string> UploadStringTaskAsync(string address, string method, string data)
        {
            // Z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\ServiceModel\ClientBase.cs

            var x = new TaskCompletionSource<string>();

            new Thread(
                delegate()
                {
                    var Result = UploadString(address, method, data);


                    x.SetResult(Result);

                }
            ) { IsBackground = true }.Start();

            return x.Task;
        }

        public string UploadString(Uri u, string method, string data)
        {
            // http://hg.openjdk.java.net/jdk7/jdk7/jdk/file/tip/src/share/classes/sun/net/www/protocol/http/HttpURLConnection.java
            // fails on openJDK why?

            Console.WriteLine("enter UploadString " + new { u, method });

            var w = new StringBuilder();

            HttpURLConnection xHttpURLConnection = null;

            try
            {
                #region NSA is that you? intercept? we can only trust pinned off device certs
                var trustAllCerts = new[] { 
                    
                        new localX509TrustManager{}
                    };

                SSLContext sc = SSLContext.getInstance("SSL");
                sc.init(null, trustAllCerts, new java.security.SecureRandom());
                HttpsURLConnection.setDefaultSSLSocketFactory(sc.getSocketFactory());

                HttpsURLConnection.setDefaultHostnameVerifier(new localHostnameVerifier { });
                #endregion


                //UploadString getOutputStream
                //enter checkServerTrusted
                //enter getAcceptedIssuers
                //UploadString writeBytes

                var url = new java.net.URL(u.ToString());

                xHttpURLConnection = (HttpURLConnection)url.openConnection();



                var https = xHttpURLConnection as HttpsURLConnection;
                if (https != null)
                {
                    //Console.WriteLine(new { https });


                }


                //conn.setHostnameVerifier(new localHostnameVerifier { });

                xHttpURLConnection.setDoOutput(true);
                xHttpURLConnection.setDoInput(true);
                xHttpURLConnection.setInstanceFollowRedirects(false);
                //conn.setInstanceFollowRedirects(true);

                xHttpURLConnection.setRequestMethod(method);


                var xContentType = default(string);


                try
                {

                    if (Headers != null)
                    {
                        foreach (string key in Headers.AllKeys)
                        {
                            if (key == "Content-Type")
                            {
                                xContentType = Headers[key];
                            }


                            xHttpURLConnection.addRequestProperty(key, Headers[key]);
                        }
                    }
                }
                catch (Exception e)
                {
                    //System.Console.WriteLine("ERROR: Failed to write headers. Exception was:" + e.Message);
                }

                if (xContentType == null)
                {
                    xHttpURLConnection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
                    xHttpURLConnection.setRequestProperty("charset", "utf-8");
                }

                //conn.setRequestProperty("content-length", "" + data.Length);
                xHttpURLConnection.setRequestProperty("Content-Length", "" + data.Length);

                xHttpURLConnection.setUseCaches(false);


                //Console.WriteLine("UploadString getOutputStream");
                var o = xHttpURLConnection.getOutputStream();

                //Console.WriteLine("UploadString writeBytes");

                // 
                DataOutputStream wr = new DataOutputStream(o);
                wr.writeBytes(data);
                //Console.WriteLine("UploadString flush");
                wr.flush();
                //Console.WriteLine("UploadString close");
                wr.close();


                //Console.WriteLine("UploadString readLine");

                //var i = new java.io.InputStreamReader(url.openStream(), "UTF-8");
                var i = new java.io.InputStreamReader(xHttpURLConnection.getInputStream(), "UTF-8");
                var reader = new java.io.BufferedReader(i);

                // can't we just read to the end?
                var line = reader.readLine();
                while (line != null)
                {
                    w.AppendLine(line);

                    line = reader.readLine();
                }
                reader.close();
            }
            catch (Exception err)
            {
                // 500 ?

                // = java.net.ProtocolException: Invalid HTTP method:

                // oops
                Console.WriteLine("UploadString " + new { err });
            }

            //Console.WriteLine("exit UploadString " + new { conn });

            if (xHttpURLConnection != null)
                xHttpURLConnection.disconnect();

            return w.ToString();
        }
        #endregion



    }
}
