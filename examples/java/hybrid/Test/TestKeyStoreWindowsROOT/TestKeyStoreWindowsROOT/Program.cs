using java.security;
using java.security.cert;
using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.X509Certificates;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TestKeyStoreWindowsROOT
{



    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // http://stackoverflow.com/questions/1666052/java-https-client-certificate-authentication
            // http://www.berthou.com/us/2007/12/05/ms-capi-and-java-jce-sunmscapi/


            try
            {
                java.lang.System.setProperty("javax.net.debug", "all");



                #region Certificates
                Func<string, IEnumerable<X509Certificate2>> Certificates = keyStoreType =>
                {
                    var a = new List<X509Certificate2> { };

                    try
                    {
                        // http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b27/sun/security/mscapi/SunMSCAPI.java

                        // https://docs.oracle.com/javase/7/docs/technotes/guides/security/SunProviders.html

                        // https://social.msdn.microsoft.com/Forums/expression/en-US/52dca221-1e05-44c1-8c45-9e0d4a807853/java-keystoreload-for-windowsmy-pops-up-insert-smart-card-window?forum=windowssecurity
                        // I removed some personal certificaties at key manager (certmgr.msc) and wala!

                        //Client Authentication (1.3.6.1.5.5.7.3.2)
                        //Secure Email (1.3.6.1.5.5.7.3.4)


                        // https://www.chilkatsoft.com/p/p_280.asp
                        // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider\Microsoft Base Smart Card Crypto Provider

                        // http://stackoverflow.com/questions/27692904/how-to-avoid-smart-card-selection-popup-when-accessing-windows-my-using-java

                        // http://stackoverflow.com/questions/4552100/how-to-prevent-popups-when-loading-a-keystore
                        // http://stackoverflow.com/questions/15220976/how-to-obtain-a-users-identity-from-a-smartcard-on-windows-mscapi-with-java

                        KeyStore xKeyStore = KeyStore.getInstance(keyStoreType);
                        //Console.WriteLine(new { xKeyStore });
                        //Console.WriteLine("load... " + new { keyStoreType });
                        xKeyStore.load(null, null);
                        //Console.WriteLine("load... done");
                        //Console.WriteLine("aliases...");
                        java.util.Enumeration en = xKeyStore.aliases();
                        //Console.WriteLine("aliases... done");

                        while (en.hasMoreElements())
                        {
                            var aliasKey = (string)en.nextElement();

                            //Console.WriteLine(new { aliasKey });

                            // PCSC?
                            var c509 = xKeyStore.getCertificate(aliasKey) as java.security.cert.X509Certificate;
                            if (c509 != null)
                            {
                                X509Certificate2 crt = new __X509Certificate2 { InternalElement = c509 };

                                //Console.WriteLine(new { crt.Subject, crt.SerialNumber, SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false) });
                                //Console.WriteLine(new { aliasKey, crt.SerialNumber, SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false), crt.Issuer });

                                a.Add(crt);

                            }
                            //if (aliasKey.equals("myKey") ) {
                            //      PrivateKey key = (PrivateKey)ks.getKey(aliasKey, "monPassword".toCharArray());
                            //      Certificate[] chain = ks.getCertificateChain(aliasKey);
                            //}
                        }

                    }
                    catch //(Exception closure)
                    {
                        throw;
                    }

                    return a;
                };
                #endregion




                Action<string> f = keyStoreType =>
                {
                    // jsc should do implicit try catch for closures? while asking for explicit catch for non closures?

                    //{ ks = java.security.KeyStore@d3ade7 }
                    //{ aliasKey = peer integrity authority for cpu BFEBFBFF000306A9, SerialNumber = 03729f49acf3e79d4cc40da08149433d, SimpleName = peer integrity authority for cpu BFEBFBFF000306A9 }
                    //{ aliasKey = peer integrity authority for cpu BFEBFBFF000306C3, SerialNumber = c4761e1ea779bc9546151afce47c7c26, SimpleName = peer integrity authority for cpu BFEBFBFF000306C3 }

                    try
                    {
                        // http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b27/sun/security/mscapi/SunMSCAPI.java

                        // https://docs.oracle.com/javase/7/docs/technotes/guides/security/SunProviders.html

                        // https://social.msdn.microsoft.com/Forums/expression/en-US/52dca221-1e05-44c1-8c45-9e0d4a807853/java-keystoreload-for-windowsmy-pops-up-insert-smart-card-window?forum=windowssecurity
                        // I removed some personal certificaties at key manager (certmgr.msc) and wala!

                        //Client Authentication (1.3.6.1.5.5.7.3.2)
                        //Secure Email (1.3.6.1.5.5.7.3.4)


                        // https://www.chilkatsoft.com/p/p_280.asp
                        // HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider\Microsoft Base Smart Card Crypto Provider

                        // http://stackoverflow.com/questions/27692904/how-to-avoid-smart-card-selection-popup-when-accessing-windows-my-using-java

                        // http://stackoverflow.com/questions/4552100/how-to-prevent-popups-when-loading-a-keystore
                        // http://stackoverflow.com/questions/15220976/how-to-obtain-a-users-identity-from-a-smartcard-on-windows-mscapi-with-java

                        KeyStore xKeyStore = KeyStore.getInstance(keyStoreType);
                        Console.WriteLine(new { xKeyStore });
                        Console.WriteLine("load... " + new { keyStoreType });
                        xKeyStore.load(null, null);
                        //Console.WriteLine("load... done");
                        Console.WriteLine("aliases...");
                        java.util.Enumeration en = xKeyStore.aliases();
                        //Console.WriteLine("aliases... done");

                        while (en.hasMoreElements())
                        {
                            var aliasKey = (string)en.nextElement();

                            //Console.WriteLine(new { aliasKey });

                            // PCSC?
                            var c509 = xKeyStore.getCertificate(aliasKey) as java.security.cert.X509Certificate;
                            if (c509 != null)
                            {
                                X509Certificate2 crt = new __X509Certificate2 { InternalElement = c509 };

                                //Console.WriteLine(new { crt.Subject, crt.SerialNumber, SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false) });
                                Console.WriteLine(new { aliasKey, crt.SerialNumber, SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false), crt.Issuer });

                            }
                            //if (aliasKey.equals("myKey") ) {
                            //      PrivateKey key = (PrivateKey)ks.getKey(aliasKey, "monPassword".toCharArray());
                            //      Certificate[] chain = ks.getCertificateChain(aliasKey);
                            //}
                        }

                    }
                    catch //(Exception closure)
                    {
                        throw;
                    }
                };

                Certificates("Windows-ROOT").WithEach(
                    crt =>
                    {
                        // aliasKey = peer integrity authority for cpu BFEBFBFF000306A9

                        // SimpleName = peer integrity authority for cpu BFEBFBFF000306A9
                        var SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false);


                        //{ SerialNumber = 01 }
                        //{ SerialNumber = 4a19d2388c82591ca55d735f155ddca3 }
                        //{ SerialNumber = 35def4cf }
                        //{ SerialNumber = 00 }
                        //{ SerialNumber = 7dd9fe07cfa81eb7107967fba78934c6 }
                        //{ SerialNumber = 70bae41d10d92934b638ca7b03ccbabf }
                        //{ SerialNumber = 7dd9fe07cfa81eb7107967fba78934c6 }
                        //{ SerialNumber = 35def4cf }
                        //{ SerialNumber = 00 }
                        //{ SerialNumber = 7dd9fe07cfa81eb7107967fba78934c6 }

                        //                        if (SimpleName == null)
                        //                            Console.WriteLine(new { crt.SerialNumber });
                        //                        else
                        if (SimpleName.StartsWith("peer integrity authority for cpu"))
                            Console.WriteLine(new { SimpleName, crt.SerialNumber, crt.Issuer });
                    }
                );

                //f("Windows-MY");
                Console.WriteLine("-");

                //Caused by: java.lang.NullPointerException
                //        at TestKeyStoreWindowsROOT.Program._Main_b__4(Program.java:214)

                // https://msdn.microsoft.com/en-us/library/system.security.cryptography.x509certificates.x509certificate2.friendlyname(v=vs.110).aspx


                f("Windows-MY");

                //Certificates("Windows-MY").WithEach(
                //    crt =>
                //    {
                //        // aliasKey = peer integrity authority for cpu BFEBFBFF000306A9

                //        // SimpleName = peer integrity authority for cpu BFEBFBFF000306A9
                //        var SimpleName = crt.GetNameInfo(System.Security.Cryptography.X509Certificates.X509NameType.SimpleName, false);

                //        //if (SimpleName.StartsWith("peer integrity authority for cpu"))
                //        Console.WriteLine(new { SimpleName, crt.SerialNumber, crt.Issuer });
                //    }
                //);

            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });
            }


            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            MessageBox.Show("click to close");

        }
    }


}
