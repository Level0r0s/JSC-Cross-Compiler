using java.security;
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

namespace TestKeyStoreDefault
{


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // http://stackoverflow.com/questions/11203483/run-a-java-application-as-a-service-on-linux

            // http://askubuntu.com/questions/99232/how-to-make-a-jar-file-run-on-startup-and-when-you-log-out

            // "X:\torrent\ubuntu-14.04.3-server-amd64.iso"
            // http://standards.freedesktop.org/desktop-entry-spec/desktop-entry-spec-latest.html

            try
            {
                System.Console.WriteLine("hello ubuntu! " + new
                {
                    typeof(object).AssemblyQualifiedName
                }
                );

                // http://www.java2s.com/Tutorial/Java/0490__Security/KeyStoreExample.htm


                var xKeyStoreDefaultType = java.security.KeyStore.getDefaultType();
                //xKeyStoreDefaultType = "/usr/lib/jvm/default-java/jre/lib/security/cacerts";
                //xKeyStoreDefaultType = "cacerts.jks";

                Console.WriteLine("... " + new { xKeyStoreDefaultType });


                #region useless
                // You can't do it with the system properties. You would have to write and load your own X509KeyManager and create your own SSLContext with it.
                // https://docs.oracle.com/cd/E19830-01/819-4712/ablqw/index.html

                var keyStore = java.lang.System.getProperty("javax.net.ssl.keyStore");
                Console.WriteLine(new { keyStore });
                var trustStore = java.lang.System.getProperty("javax.net.ssl.trustStore");
                Console.WriteLine(new { trustStore });
                #endregion

                // are we running in GUI or TTY?
                // can we enumerate keystores?

                // ... { xKeyStoreDefaultType = jks }


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

                            Console.WriteLine(new { aliasKey });

                            // PCSC?hhhhhhhhhhhh
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

                //hello ubuntu! { AssemblyQualifiedName = java.lang.Object, rt }
                //... { xKeyStoreDefaultType = jks }
                //{ xKeyStore = java.security.KeyStore@9980d5 }
                //load... { keyStoreType = jks }
                //aliases...
                //done


                // on RED there are no entries?
                // what about ubuntu?
                f(xKeyStoreDefaultType);

                //C:\Windows\system32>net use u: \\192.168.1.189\staging
                //The command completed successfully.

                // ubuntu is also empty it seems.
                // what about android?

            }
            catch (Exception err)
            {
                Console.WriteLine(new { err.Message, err.StackTrace });
            }

            Console.WriteLine("done");
            //Thread.Sleep(10000);
            Console.ReadLine();



            // CLR not available? unless there was mono?
            //CLRProgram.CLRMain();
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
