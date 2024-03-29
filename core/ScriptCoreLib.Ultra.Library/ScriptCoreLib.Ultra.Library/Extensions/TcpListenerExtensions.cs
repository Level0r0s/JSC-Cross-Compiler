﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Net.Security;
using System.Threading.Tasks;

namespace ScriptCoreLib.Extensions
{
    public static class TcpListenerExtensions
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151122/ubuntutcpmultiplex
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151029



        // called by?

        //static void BridgeStreamTo(this NetworkStream x, NetworkStream y, int ClientCounter, string prefix = "#")
        static void BridgeStreamTo(this Stream x, Stream y, int ClientCounter, string prefix = "#",
            TaskCompletionSource<byte> firstByte = null,
            TaskCompletionSource<object> exitBeforeFirstByte = null
            )
        {
            //Console.WriteLine("BridgeStreamTo x: " + x.GetType().AssemblyQualifiedName);

            new Thread(
               delegate()
               {
                   var rereadonzerobyte = 64;

                   var buffer = new byte[0x100000];

                   while (true)
                   {
                       //
                       try
                       {
                       retry:
                           var c = x.Read(buffer, 0, buffer.Length);

                           // is chrome trying to be smart?

                           if (c == 0)
                               if (rereadonzerobyte > 0)
                               {
                                   rereadonzerobyte--;
                                   // will we get 0 twice??

                                   // why send 0 bytes??
                                   // chrome is testing server timer?
                                   Thread.Sleep(500);
                                   goto retry;
                               }

                           if (c <= 0)
                           {
                               if (firstByte != null && !firstByte.Task.IsCompleted)
                                   Console.WriteLine(prefix + ClientCounter.ToString("x4") + " exitBeforeFirstByte " + new { c });
                               //else
                               //Console.WriteLine(prefix + ClientCounter.ToString("x4") + " exit");

                               if (firstByte != null && !firstByte.Task.IsCompleted)
                                   if (exitBeforeFirstByte != null)
                                       exitBeforeFirstByte.SetResult(null);


                               return;
                           }

                           if (firstByte != null)
                           {
                               firstByte.SetResult(buffer[0]);
                               firstByte = null;
                           }

                           Console.WriteLine(prefix + ClientCounter.ToString("x4") + " 0x" + c.ToString("x4") + " bytes");



                           // not for binary data. it will crash RemoteApp console.
                           // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151029
                           if (prefix.Contains("?"))
                           {
                               var x8 = Encoding.ASCII.GetString(buffer, 0, Math.Min(8, c));

                               if (x8.All(xx => xx < 127))
                                   Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, Math.Min(512, c)));
                           }

                           y.Write(buffer, 0, c);

                           // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141209/bridgeconnectiontoport
                           // why is sleep a good idea here?
                           Thread.Sleep(1);
                       }
                       catch (Exception fault)
                       {
                           Console.WriteLine(prefix + ClientCounter.ToString("x4") + " fault " + new { fault.Message });

                           return;
                       }
                   }
               }
           )
            {
                Name = "BridgeStreamTo",
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            }.Start();
        }

        static void BridgeConnectionTo(this TcpClient x, TcpClient y, int ClientCounter, string rx, string tx)
        {
            x.GetStream().BridgeStreamTo(y.GetStream(), ClientCounter, rx);
            y.GetStream().BridgeStreamTo(x.GetStream(), ClientCounter, tx);
        }


        // called by?
        //public static void BridgeConnectionToPort(this TcpListener x, int port)
        //{
        //    BridgeConnectionToPort(x, port, "> ", "< ");
        //}

        // X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\ApplicationWebService.cs
        //  (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
        [Obsolete("wont be visible for the child appdomain?")]
        public static Dictionary<object, Action<RemoteCertificateValidationCallback>> RemoteCertificateValidationCallbackReplay
            = new Dictionary<object, Action<RemoteCertificateValidationCallback>>();




        static string win32_processor_processorID()
        {
            var sw = Stopwatch.StartNew();

            string cpuInfo = string.Empty;
            var mc = new System.Management.ManagementClass("win32_processor");
            var moc = mc.GetInstances();

            foreach (System.Management.ManagementObject mo in moc)
            {
                //if (cpuInfo == "")
                {
                    // mo = {\\ASUS7\root\cimv2:Win32_Processor.DeviceID="CPU0"}

                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();


                    // cpuInfo = "BFEBFBFF000206A7"

                    break;
                }
            }


            // { ElapsedMilliseconds = 1132, cpuInfo = BFEBFBFF000206A7 }
            // { ElapsedMilliseconds = 1090, cpuInfo = BFEBFBFF000206A7 }
            Console.WriteLine(
                new
                {
                    sw.ElapsedMilliseconds,
                    cpuInfo
                }
                );

            return cpuInfo;
        }

        // called by?
        public static string makecert
        {
            get
            {
                var makecert70A = "c:/program files/microsoft sdks/windows/v7.0A/bin/makecert.exe";
                var makecert80 = @"C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe";
                var makecert81 = @"C:\Program Files (x86)\Windows Kits\8.1\bin\x64\makecert.exe";

                // "C:\Program Files (x86)\Windows Kits\8.1\bin\x64\makecert.exe"

                // http://stackoverflow.com/questions/589834/what-rsa-key-length-should-i-use-for-my-ssl-certificates
                // ENISA recommends 15360 Bit. Have a look to the PDF (page 35)
                // Industry standards set by the Certification Authority/Browser (CA/B) Forum require that certificates issued after January 1, 2014 MUST be at least 2048-bit key length.
                // http://stackoverflow.com/questions/589834/what-rsa-key-length-should-i-use-for-my-ssl-certificates

                // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141018-ssl
                // X:\jsc.svn\examples\java\hybrid\JVMCLRTCPMultiplex\JVMCLRTCPMultiplex\Program.cs

                // Error: There is no matching certificate in the issuer's Root cert store
                //Error: There are more than one matching certificate in the issuer's Root cert store
                var makecert = new[] { makecert70A, makecert80, makecert81 }.FirstOrDefault(File.Exists);

                return makecert;
            }
        }

        // threadsafe?
        static int ClientCounter = 0;

        static int SSLEmptyConnections = 0;
        static int SSLDataConnections = 0;


        // called by?
        //    BridgeConnectionToPort(x, port, "> ", "< ");
        public static void BridgeConnectionToPort(this TcpListener xTcpListener, int port, string rx = "> ", string tx = "< ",

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151031
            // port +1 scheduled to ask for client certificate
            bool GetClientCertificate = false)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151031
            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs

            // http://stackoverflow.com/questions/5510063/makecert-exe-missing-in-windows-7-how-to-get-it-and-use-it



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150119

            // certmgr.msc
            //var CN = "device SSL authority for developers";
            var rootCN = "peer integrity authority for cpu " + win32_processor_processorID();
            // should we scan the network and let other peers know
            // and tell them 'trust us'?

            // x:\jsc.svn\examples\javascript\xml\serversidecontent\serversidecontent\application.cs

            #region CertificateFromCurrentUserByLocalEndPoint
            Func<IPEndPoint, X509Certificate> CertificateFromCurrentUserByLocalEndPoint =
                LocalEndPoint =>
                {
                    // do we hav a wan ip?
                    //var upstream = "83.191.217.119";
                    //var upstreamlink = "https://" + upstream + ":" + LocalEndPoint.Port;


                    var host = LocalEndPoint.Address.ToString();


                    var link = !GetClientCertificate ?
                        ("http://" + host + ":" + LocalEndPoint.Port) :

                        // keep the same cert
                        ("http://" + host + ":" + (LocalEndPoint.Port - 1));

                    //link += " " + upstreamlink;

                    #region CertificateFromCurrentUser
                    Func<X509Certificate> CertificateFromCurrentUser =
                        delegate
                        {
                            X509Store store = new X509Store(
                                //StoreName.Root,
                                    StoreName.My,
                                StoreLocation.CurrentUser);
                            // https://syfuhs.net/2011/05/12/making-the-x509store-more-friendly/
                            // http://ftp.icpdas.com/pub/beta_version/VHM/wince600/at91sam9g45m10ek_armv4i/cesysgen/sdk/inc/wintrust.h

                            // Policy Information:
                            //URL = http://127.0.0.5:10500

                            try
                            {

                                store.Open(OpenFlags.ReadOnly);
                                // Additional information: The OID value was invalid.
                                X509Certificate2Collection cers = store.Certificates;


                                foreach (var item in cers)
                                {
                                    // http://comments.gmane.org/gmane.comp.emulators.wine.devel/86862
                                    var SPC_SP_AGENCY_INFO_OBJID = "1.3.6.1.4.1.311.2.1.10";

                                    // // spcSpAgencyInfo private extension

                                    var elink = item.Extensions[SPC_SP_AGENCY_INFO_OBJID];
                                    if (elink != null)
                                    {
                                        var prefix = 6;
                                        var linkvalue = Encoding.UTF8.GetString(elink.RawData, prefix, elink.RawData.Length - prefix);

                                        //Console.WriteLine(new { item.Subject, linkvalue });

                                        if (linkvalue == link)
                                            return item;
                                    }
                                }
                            }
                            finally
                            {

                                store.Close();
                            }

                            return null;

                        };
                    #endregion

                    // are we slowing down checking certs at each connection?
                    // are we spamming the cert store?
                    var n = CertificateFromCurrentUser();

                    if (n == null)
                    {
                        // http://stackoverflow.com/questions/13332569/how-to-create-certificate-authority-certificate-with-makecert
                        // http://www.jayway.com/2014/09/03/creating-self-signed-certificates-with-makecert-exe-for-development/
                        // http://stackoverflow.com/questions/4095297/self-signed-certificates-performance-in-wcf-scenarios
                        // https://social.technet.microsoft.com/Forums/lync/en-US/a91485aa-6c04-4ed3-89d4-f821f7289665/how-to-append-subject-alternative-namesan-information-from-makecert?forum=ocssecurity


                        // https://social.msdn.microsoft.com/Forums/windowsdesktop/en-US/7bdd659c-0f1a-47bb-b986-b3cd1e864c9d/creating-a-certificate-with-makecert-fails-without-the-pe-flag-why?forum=windowssecurity
                        // Can't create the key of the subject ('JoeSoft') 
                        // http://blog.aschommer.de/?tag=/makecert
                        // http://stackoverflow.com/questions/6383054/add-or-create-subject-alternative-name-field-to-self-signed-certificate-using
                        // At least with the version of makecert that comes with Visual Studio 2012, you can specify multiple subjects, simply by specifying a comma separated list -n "CN=domain1, CN=domain2"
                        // http://wiki.cacert.org/VhostTaskForce#head-661e90855b6b4285bbab272390bf7bbd639ed5d9



                        // chrome will fault on multiple CN
                        var args =
                            //" -eku 1.3.6.1.5.5.7.3.1 -a SHA1 -n \"CN=" + upstream + ",CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss MY -sr currentuser -sk deviceSSLcontainer  -is Root -in \"" + rootCN + "\" -l \"" + link + "\"";
                " -eku 1.3.6.1.5.5.7.3.1 -a SHA1 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss MY -sr currentuser -sk deviceSSLcontainer  -is Root -in \"" + rootCN + "\" -l \"" + link + "\"";

                        Console.WriteLine(
                            new { makecert, args }
                            );

                        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\Native.cs

                        // logical store name
                        var p = Process.Start(
                            new ProcessStartInfo(
                            makecert, args
                            //"-r  -n \"CN=localhost\" -m 12 -sky exchange -sv serverCert.pvk -pe -ss my serverCert.cer"
                            //"-r  -n \"CN=localhost\" -m 12 -sky exchange -pe -ss my serverCert.cer -sr localMachine"
                            //"-r  -n \"CN=localhost\" -m 12 -sky exchange -pe -ss my serverCert.cer -sr currentuser"
                            //"-r  -a SHA1 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange -pe -ss my -sr currentuser -l " + link
                            //"-r -cy authority -eku 1.3.6.1.5.5.7.3.1,1.3.6.1.5.5.7.3.2 -a SHA512 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss Root -sr currentuser -l " + link

                            // chrome wont like SHA512
                            // https://code.google.com/p/chromium/issues/detail?id=342230
                            // http://serverfault.com/questions/407006/godaddy-ssl-certificate-shows-domain-name-instead-of-full-company-name
                            // The certificate's O attribute in the subject (organization), along with the C attribute (country) determine what is displayed. If they are absent, it will simply display the primary subject domain name from the certificate.

                            //"-r -cy authority -eku 1.3.6.1.5.5.7.3.1,1.3.6.1.5.5.7.3.2 -a SHA1 -n \"CN=" + host + ",O=JVMCLRTCPMultiplex\"  -len 2048 -m 1 -sky exchange  -ss Root -sr currentuser -l " + link
                            //" -eku 1.3.6.1.5.5.7.3.1,1.3.6.1.5.5.7.3.2 -a SHA1 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss MY -sr currentuser -is Root -in \"" + CN + "\" -l " + link
                            //" -eku 1.3.6.1.5.5.7.3.1 -a SHA1 -n \"CN=" + host + "\"  -len 2048 -m 1 -sky exchange  -ss MY -sr currentuser -is Root -in \"" + CN + "\" -l " + link

                            // http://serverfault.com/questions/193775/ssl-certificate-for-a-public-ip-address
                            // https://social.msdn.microsoft.com/Forums/windowsdesktop/en-US/525879b2-43c0-47fc-aa26-2e0e881b034e/makecert-and-increasing-to-2048-with-len-is-not-working-if-certificate-of-same-name-already-exists?forum=windowssecurity
                            // Error: The requested and current keysize are not the same.
                            // http://stackoverflow.com/questions/11708717/ip-address-as-hostname-cn-when-creating-a-certificate-https-hostname-wrong
                            )

                            {
                                UseShellExecute = false

                            }

                            );

                        p.WaitForExit();
                        //Console.WriteLine(new { p.ExitCode });

                        n = CertificateFromCurrentUser();

                        if (n == null)
                            throw new InvalidOperationException();

                    }

                    return n;
                };
            #endregion


            //---------------------------
            //Security Warning
            //---------------------------
            //You are about to install a certificate from a certification authority (CA) claiming to represent:
            //peer integrity authority for cpu BFEBFBFF000206A7
            //Windows cannot validate that the certificate is actually from "peer integrity authority for cpu BFEBFBFF000206A7". You should confirm its origin by contacting "peer integrity authority for cpu BFEBFBFF000206A7". The following number will assist you in this process:
            //Thumbprint (sha1): 4FE31CF8 CDF53883 BD677A2B A3E79ED9 C0225627
            //Warning:
            //If you install this root certificate, Windows will automatically trust any certificate issued by this CA. Installing a certificate with an unconfirmed thumbprint is a security risk. If you click "Yes" you acknowledge this risk.
            //Do you want to install this certificate?
            //---------------------------
            //Yes   No   
            //---------------------------


            var r = default(X509Certificate);

            #region CertificateRootFromCurrentUser
            Func<X509Certificate> CertificateRootFromCurrentUser =
                delegate
                {
                    X509Store store = new X509Store(
                                StoreName.Root,
                        StoreLocation.CurrentUser);
                    // https://syfuhs.net/2011/05/12/making-the-x509store-more-friendly/
                    // http://ftp.icpdas.com/pub/beta_version/VHM/wince600/at91sam9g45m10ek_armv4i/cesysgen/sdk/inc/wintrust.h

                    // Policy Information:
                    //URL = http://127.0.0.5:10500

                    try
                    {

                        store.Open(OpenFlags.ReadOnly);

                        var item = store.Certificates.Find(X509FindType.FindBySubjectName, rootCN, true);

                        if (item.Count > 0)
                            return item[0];

                    }
                    finally
                    {

                        store.Close();
                    }

                    return null;

                };
            #endregion
            // Makecert is deprecated and above will only work for testing in IE as this is not true SAN certificate
            // http://blogs.technet.com/b/uday/archive/2012/06/21/makecert-exe-san-and-wildcard-certificate.aspx


            #region authority
            if (makecert != null)
            {
                r = CertificateRootFromCurrentUser();

                if (r == null)
                {

                    var args = "-r -cy authority -a SHA1 -n \"CN=" + rootCN + "\"  -len 2048 -m 72 -ss Root -sr currentuser";

                    Console.WriteLine(new { makecert, args });

                    var p = Process.Start(
                        new ProcessStartInfo(
                            makecert,
                        // this cert is constant
                           args
                        )
                        {
                            UseShellExecute = false
                        }

                    );

                    p.WaitForExit();

                    Console.WriteLine(new { p.ExitCode });

                }
            }
            #endregion



            xTcpListener.Start();

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201410/20141019
            // X:\jsc.svn\examples\javascript\async\AsyncWorkerSourceSHA1\AsyncWorkerSourceSHA1\Application.cs
            // { makecert = C:\Program Files (x86)\Windows Kits\8.0\bin\x64\makecert.exe, link = http://0.0.0.0:7847 }
            //Console.WriteLine("prefetching SSL certificate...");
            //CertificateFromCurrentUserByLocalEndPoint((IPEndPoint)x.LocalEndpoint);
            //Console.WriteLine("prefetching SSL certificate... done");

            // Z:\jsc.svn\examples\javascript\Test\TestSSLConnectionLimit\ApplicationWebService.cs

            // AtConnection ?
            Action<TcpClient> yield =
                clientSocket =>
                {
                    // how do we get a break point here?
                    // is the peek broken?
                    var xPeekableStream = new Library.Eugene.PeekableStream(clientSocket.GetStream(), 1);


                    var zbuffer = new byte[1];
                    var z = xPeekableStream.Peek(zbuffer, 0, 1);
                    var peek_char = zbuffer[0];
                    //Console.WriteLine(new { peek_char });

                    if (peek_char == 0x16)
                    {
                        #region 0x16

                        //ScriptCoreLib.Ultra.Library.dll	X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\bin\Debug\ScriptCoreLib.Ultra.Library.dll	No	N/A	Symbols loaded.	X:\jsc.svn\examples\javascript\Test\TestEIDPIN2\TestEIDPIN2\bin\Debug\ScriptCoreLib.Ultra.Library.pdb	8	4.5.0.0	2014-12-09 07:57 PM	01000000-01096000	[0x2888] TestEIDPIN2.exe: Managed (v4.0.30319)		
                        //ScriptCoreLib.Ultra.Library.dll	C:\Users\Arvo\AppData\Local\Temp\Temporary ASP.NET Files\root\859044d8\ccb7784\assembly\dl3\a7ce0579\776f278d_d913d001\ScriptCoreLib.Ultra.Library.dll	No	N/A	Symbols loaded.	x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\obj\Debug\ScriptCoreLib.Ultra.Library.pdb	103	4.5.0.0	2014-12-09 07:57 PM	12AD0000-12B66000	[0x2888] TestEIDPIN2.exe: Managed (v4.0.30319)		




                        // is the request coming from the gateway router?
                        var isPeerProxyUSBS1 = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString() == "192.168.43.1";
                        var isPeerProxyUSBS6 = ((IPEndPoint)clientSocket.Client.RemoteEndPoint).Address.ToString() == "192.168.42.129";
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201510/20151012/proxy

                        // Z:\jsc.svn\examples\java\android\forms\InteractivePortForwarding\InteractivePortForwarding\UserControl1.cs
                        var isPeerProxy = isPeerProxyUSBS1 || isPeerProxyUSBS6;

                        // is chrome testing how many connections it can have?


                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141209
                        // how do we get a break point here?
                        Console.WriteLine(
                             "#" + ClientCounter.ToString("x2") + " "
                            + "TCP enter https "
                                + new
                                {
                                    ClientCounter,
                                    SSLEmptyConnections,
                                    SSLDataConnections,

                                    clientSocket.Client.RemoteEndPoint,
                                    isPeerProxy
                                    //Debugger.IsAttached,
                                    //System.Reflection.Assembly.GetExecutingAssembly().Location
                                }
                        );

                        // https://code.google.com/p/chromium/issues/detail?id=543982&thanks=543982&ts=1444983390
                        // if we dont stop chrome it will become greedy??

                        //#0f TCP enter https { ClientCounter = 15, SSLEmptyConnections = 7, SSLDataConnections = 3 }

                        #region signal chrome to cut it off?
                        if (SSLEmptyConnections > 6)
                        {
                            // Z:\jsc.svn\examples\javascript\Test\TestSSLConnectionLimit\Application.cs

                            //#0e { RemoteEndPoint = 192.168.1.199:52616, isPeerProxy = False }
                            //#0f TCP enter https { ClientCounter = 15, SSLEmptyConnections = 7, SSLDataConnections = 3 }
                            //                            chrome is that you? stop being greedy!

                            Console.WriteLine("chrome is that you? stop being greedy! chrome://net-internals/");

                            Thread.Sleep(1500);

                            // close this or some other empty connection?
                            //clientSocket.Close();

                            // will it start sending data on other empty data connections?

                            //return;
                        }
                        #endregion


                        // http://stackoverflow.com/questions/14523559/remotecertificatenotavailable-root-certificate-not-being-passed-during-authent
                        // When asking for client authentication, this server sends a list of trusted certificate authorities to the client. The client uses this list to choose a client certificate that is trusted by the server. Currently, this server trusts so many certificate authorities that the list has grown too long. This list has thus been truncated. The administrator of this machine should review the certificate authorities trusted for client authentication and remove those that do not really need to be trusted.

                        //using (
                        SslStream xSslStream = new SslStream(
                           innerStream: xPeekableStream,
                           leaveInnerStreamOpen: false,


                           // https://msdn.microsoft.com/en-us/library/system.net.security.localcertificateselectioncallback(v=vs.110).aspx

                           userCertificateSelectionCallback:
                               new LocalCertificateSelectionCallback(
                                   (object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers) =>
                                   {
                                       // examples/javascript/ubuntu/UbuntuDualSSLWebApplication/UbuntuDualSSLWebApplication/AcceptedIssuers/ESTEID-SK_2011.der.crt
                                       // userCertificateSelectionCallback { targetHost = , remoteCertificate = , localCertificates = 1, acceptableIssuers = 0 }

                                       // http://stackoverflow.com/questions/53824/how-to-specify-accepted-certificates-for-client-authentication-in-net-sslstream

                                       // userCertificateSelectionCallback { targetHost = , remoteCertificate = , localCertificates = System.Security.Cryptography.X509Certificates.X509CertificateCollection }
                                       //Console.WriteLine("userCertificateSelectionCallback " + new
                                       //{
                                       //    targetHost,
                                       //    remoteCertificate,
                                       //    localCertificates = localCertificates.Count,
                                       //    acceptableIssuers = acceptableIssuers.Length,

                                       //    //Environment.StackTrace
                                       //});

                                       return localCertificates[0];
                                   }
                               ),
                           userCertificateValidationCallback:
                               new RemoteCertificateValidationCallback(
                                   (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
                                   {
                                       // userCertificateValidationCallback { certificate = , sslPolicyErrors = RemoteCertificateNotAvailable }


                                       // what if the app would also like to know
                                       // how did the client authenticate?

                                       if (certificate != null)
                                       {
                                           // http://stackoverflow.com/questions/2206961/sharing-data-between-appdomains

                                           //ScriptCoreLib.Ultra.WebService.WebServiceHandler.AppDomainData = certificate.Subject;
                                           //ScriptCoreLib.Ultra.WebService.WebServiceHandlerData.AppDomainData = certificate.Subject;

                                           //AppDomain.CurrentDomain.

                                           // AppDomain.set
                                           //Console.WriteLine(
                                           //    //new { certificate, chain }
                                           //    new { certificate.Subject }
                                           //    );

                                           //        SERIALNUMBER=47101010033, G=MARI-LIIS, SN=MÄNNIK, CN="MÄNNIK,MARI-LIIS,47101010033", OU=authentication, O=ESTEID, C=EE

                                           // can we get the data to the web handler in another appdomain this way?


                                           //Console.Title = certificate.GetSerialNumberString();
                                           //Console.Title = new { certificate }.ToString();
                                           //Console.Title = new { certificate.Subject }.ToString();

                                           return true;
                                       }

                                       //RemoteCertificateValidationCallbackReplay[sender] =
                                       // y => y(sender, certificate, chain, sslPolicyErrors);


                                       //#08 TCP enter https { ClientCounter = 8, SSLEmptyConnections = 0, SSLDataConnections = 7, RemoteEndPoint = 192.168.1.196:60424, isPeerProxy = False }
                                       // userCertificateValidationCallback { certificate = , sslPolicyErrors = RemoteCertificateNotAvailable, GetClientCertificate = False }




                                       //#0a TCP enter https { ClientCounter = 10, SSLEmptyConnections = 0, SSLDataConnections = 4, RemoteEndPoint = 192.168.1.196:61922, isPeerProxy = False }
                                       //AuthenticateAsServer { GetClientCertificate = True }
                                       //userCertificateSelectionCallback { remoteCertificate =  }
                                       //userCertificateValidationCallback { certificate =  }
                                       //{ RemoteCertificate =  }

                                       if (GetClientCertificate)
                                       {
                                           Console.WriteLine("userCertificateValidationCallback " + new { certificate, sslPolicyErrors, GetClientCertificate });
                                           return false;
                                       }

                                       return true;
                                   }
                               ),
                           encryptionPolicy: EncryptionPolicy.RequireEncryption
                           );
                        //)
                        {

                            try
                            {
                                // AuthenticateAsServer
                                // can this hang? if we use the wrong stream!

                                // An error occurred during a connection to 192.168.1.12:15606. Peer using unsupported version of security protocol. (Error code: ssl_error_unsupported_version) 
                                //var enabledSslProtocols = System.Security.Authentication.SslProtocols.Default;
                                var enabledSslProtocols = System.Security.Authentication.SslProtocols.Tls;

                                //Console.WriteLine(new { enabledSslProtocols });

                                #region ssl_error_unsupported_version
                                var Tls11 = typeof(System.Security.Authentication.SslProtocols).GetField("Tls11");
                                if (Tls11 != null)
                                {

                                    enabledSslProtocols = (System.Security.Authentication.SslProtocols)768;
                                    //Console.WriteLine(new { enabledSslProtocols });
                                }
                                #endregion

                                // http://stackoverflow.com/questions/28286086/default-securityprotocol-in-net-4-5

                                var Tls12 = typeof(System.Security.Authentication.SslProtocols).GetField("Tls12");

                                if (Tls12 != null)
                                {
                                    // even if we link as 4.0 running in 4.5 we have tls1.2


                                    enabledSslProtocols = (System.Security.Authentication.SslProtocols)3072;
                                    //Console.WriteLine(new { enabledSslProtocols });
                                }


                                //Console.WriteLine(
                                //    new { enabledSslProtocols }
                                //);


                                // This server could not prove that it is 83.191.217.119; its security certificate is from 192.168.43.252. This may be caused by a misconfiguration or an attacker intercepting your connection.

                                //							RemoteEndPoint = 192.168.43.1:33497, isPeerProxy = False }
                                //		certificate = , chain =  }
                                //	nter https
                                // RemoteEndPoint = 192.168.43.252:30522, isPeerProxy = False
                                //}


                                // is the tcp being forwarded? translate local gateway to wan
                                // { RemoteEndPoint = 192.168.43.1:51835, isPeerProxy = False }




                                // for firefox we have to manually import our peer authority?
                                // certmgr.msc
                                // firefox is not using OS roots?
                                // (Error code: sec_error_unknown_issuer)
                                var serverCertificate =

                                isPeerProxy ?

                                    CertificateFromCurrentUserByLocalEndPoint(
                                        new IPEndPoint(
                                    //address: IPAddress.Parse(""),

                                            // how do we know our ip?
                                            address:

                                            isPeerProxyUSBS1 ? IPAddress.Parse("83.180.27.66") :

                                            isPeerProxyUSBS6 ? IPAddress.Parse("178.23.119.87") : null,
                                            port: port
                                        )
                                    )
                                    :
                                    CertificateFromCurrentUserByLocalEndPoint((IPEndPoint)clientSocket.Client.LocalEndPoint);

                                //var upstream = "83.191.217.119";


                                // The following fatal alert was received: 48.

                                //Console.WriteLine("AuthenticateAsServer " + new { GetClientCertificate, serverCertificate.Issuer } );

                                //A temp fix was to create the Reg Key below and reboot the web server.
                                // It stops the server from sending a root certificate to the client and prompts the client for any certificate. 
                                // So long as the root certificate of the certificate provided by the client is in the server's trust root store 
                                // the user will authenticate.

                                // http://support.microsoft.com/kb/933430/EN-US

                                //            HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL
                                //            Value name:
                                //            SendTrustedIssuerList
                                //Value type:
                                //            REG_DWORD
                                //Value data:
                                //            0(False)

                                // http://blogs.msdn.com/b/kaushal/archive/2012/10/06/ssl-tls-alert-protocol-amp-the-alert-codes.aspx
                                // firefox will fail.
                                Microsoft.Win32.Registry.SetValue(
                                      @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL",
                                      "SendTrustedIssuerList",
                                      0
                                );


                                // https://bugzilla.xamarin.com/show_bug.cgi?id=16974


                                // http://stackoverflow.com/questions/617109/sslstream-on-tcp-server-fails-to-validate-client-certificate-with-remotecertific
                                // http://stackoverflow.com/questions/24150489/sslstream-authenticateasserver-certificate-fails-when-loaded-from-sql-but-works
                                xSslStream.AuthenticateAsServer(
                                     serverCertificate: serverCertificate,
                                    //clientCertificateRequired: false,
                                     clientCertificateRequired: GetClientCertificate,
                                    // Tls12 = 3072
                                    //enabledSslProtocols: System.Security.Authentication.SslProtocols.Tls12,
                                     enabledSslProtocols: enabledSslProtocols,


                                     // http://stackoverflow.com/questions/26930398/sslstream-authenticateasserver-with-optional-clientcertificate
                                    //checkCertificateRevocation: true
                                     checkCertificateRevocation: false
                                 );


                                // http://www.codeproject.com/Articles/326574/An-Introduction-to-Mutual-SSL-Authentication

                                //Console.WriteLine(new { xSslStream.RemoteCertificate });


                            }
                            catch (Exception ex)
                            {
                                // Secure Connection Failed

                                Console.WriteLine(
                                    "#" + ClientCounter.ToString("x2") + " AuthenticateAsServer failed. firefox is that you? " +
                                    new { ex.Message, InnerException = ex.InnerException == null ? null : ex.InnerException.Message });



                                // ?
                                clientSocket.Close();

                                return;
                            }

                            //Console.WriteLine("read " + sslStream.GetHashCode());


                            SSLEmptyConnections++;

                            var exitBeforeFirstDataByte = new TaskCompletionSource<object>();
                            var firstDataByte = new TaskCompletionSource<byte>();

                            // Z:\jsc.svn\examples\javascript\Test\TestSSLConnectionLimit\Application.cs
                            // are those greedy tcp connections from chrome?
                            var y = new TcpClient();
                            y.Connect(new System.Net.IPEndPoint(IPAddress.Loopback, port));

                            //Console.WriteLine(new { y.Client.LocalEndPoint, xSslStream.RemoteCertificate.Subject });
                            //Console.WriteLine(new { y.Client.LocalEndPoint });


                            xSslStream.BridgeStreamTo(
                                y.GetStream(),
                                ClientCounter,

                                prefix: "#" + ClientCounter.ToString("x2") + " " + rx,

                                firstByte: firstDataByte,
                                exitBeforeFirstByte: exitBeforeFirstDataByte
                            );

                            exitBeforeFirstDataByte.Task.ContinueWith(
                                delegate
                                {
                                    // probe connection from chrome just got canceled?
                                    SSLEmptyConnections--;
                                }
                            );

                            firstDataByte.Task.ContinueWith(
                                continuationAction: delegate
                                {
                                    SSLEmptyConnections--;
                                    SSLDataConnections++;


                                    if (xSslStream.RemoteCertificate != null)
                                    {
                                        Console.WriteLine("about to pass RemoteCertificate to cassini"
                                             + new { Environment.CurrentDirectory, typeof(ScriptCoreLib.Ultra.WebService.InternalCassiniClientCertificateLoader).Assembly.Location }

                                            );

                                        //about to pass RemoteCertificate to cassini{ CurrentDirectory = V:\staging.net.debug, Location = V:\staging.net.debug\TestDualSSLWebApplicationLauncher.exe }
                                        //enter InternalCassiniClientCertificateLoader { CurrentDirectory = V:\staging.net.debug, Location = C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\root\3ea1022a\80f01a9\assembly\dl3\6ebd91e5\3d706475_4215d101\ScriptCoreLib.Ultra.Library.dll }


                                        File.WriteAllBytes("ClientCertificate.crt", xSslStream.RemoteCertificate.Export(X509ContentType.Cert));
                                        //File.WriteAllBytes("ClientCertificate.crt", xSslStream.RemoteCertificate.Export(X509ContentType.Pkcs7));

                                        // pause all other server clients to make sure this intercepted crt is read by the correct appdomain. until cassini is gone that is.
                                    }

                                    y.GetStream().BridgeStreamTo(xSslStream, ClientCounter, prefix: "#" + ClientCounter.ToString("x2") + " " + tx);

                                }
                            );

                            // whats this about?
                            //                            05#< 0005 0x0576 bytes
                            //07#< 0007 0x0576 bytes
                            //09#< 0009 0x0576 bytes
                            //0a#< 000a 0x0576 bytes
                            //0b#< 000b 0x0576 bytes
                            //0f#< 000f 0x0576 bytes

                            //sslStream.Close();
                        }
                        //Console.WriteLine("exit https");
                        return;
                        #endregion

                    }


                    // { peek_char = 71 }
                    //=>0006 0x0120 bytes
                    {
                        var y = new TcpClient();
                        y.Connect(new System.Net.IPEndPoint(IPAddress.Loopback, port));

                        // how was this able to work? did svn loose state?
                        //clientSocket.BridgeConnectionTo(y, ClientCounter, "?" + rx, tx);
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141209/bridgeconnectiontoport

                        xPeekableStream.BridgeStreamTo(y.GetStream(), ClientCounter, rx);
                        y.GetStream().BridgeStreamTo(clientSocket.GetStream(), ClientCounter, tx);
                    }

                };


            new Thread(
               delegate()
               {
                   while (true)
                   {
                       var clientSocket = xTcpListener.AcceptTcpClient();
                       ClientCounter++;

                       //Console.WriteLine("#" + ClientCounter + " BridgeConnectionToPort");


                       yield(clientSocket);
                   }


               }
           )
            {
                IsBackground = true,
                Name = "BridgeConnectionToPort"
            }.Start();
        }
    }
}
