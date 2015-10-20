using java.io;
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UbuntuBootExperiment
{


    static class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            // https://javacruft.wordpress.com/2014/06/18/168k-instances/
            // http://www.ubuntu.com/cloud

            //File.WriteAllText(f, w.ToString());

            try
            {
                // https://lists.ubuntu.com/archives/upstart-devel/2014-August/003360.html

                Console.WriteLine("ready!");

                // http://stackoverflow.com/questions/11203483/run-a-java-application-as-a-service-on-linux

                // http://askubuntu.com/questions/99232/how-to-make-a-jar-file-run-on-startup-and-when-you-log-out

                // "X:\torrent\ubuntu-14.04.3-server-amd64.iso"
                // http://standards.freedesktop.org/desktop-entry-spec/desktop-entry-spec-latest.html
                // http://www.markhneedham.com/blog/2012/09/29/upstart-job-getting-stuck-in-the-startkilled-state/

                System.Console.WriteLine("hello ubuntu!! " + new
                {
                    //typeof(object).AssemblyQualifiedName,

                    // rt location.
                    //typeof(object).Assembly.Location,
                    typeof(Program).Assembly.Location,

                    // /home/xuser
                    //Environment.CurrentDirectory
                }
                );


                //Implementation not found for type import :
                //type: System.Environment
                //method: Void set_CurrentDirectory(System.String)

                //Environment.CurrentDirectory =;

                var fa = new FileInfo(typeof(Program).Assembly.Location);

                //var fadir = new DirectoryInfo(Path.GetDirectoryName(fa.FullName));



                Console.WriteLine(new { fa.Directory });

                var keystorepath = fa.Directory.FullName + "/domain.keystore";

                #region truststore/keystore
                {
                    var xKeyStoreDefaultType = java.security.KeyStore.getDefaultType();
                    //xKeyStoreDefaultType = "/usr/lib/jvm/default-java/jre/lib/security/cacerts";
                    //xKeyStoreDefaultType = "cacerts.jks";

                    Console.WriteLine("... " + new { xKeyStoreDefaultType });


                    // You can't do it with the system properties. You would have to write and load your own X509KeyManager and create your own SSLContext with it.
                    // https://docs.oracle.com/cd/E19830-01/819-4712/ablqw/index.html

                    var keyStore = java.lang.System.getProperty("javax.net.ssl.keyStore");
                    Console.WriteLine(new { keyStore });
                    var trustStore = java.lang.System.getProperty("javax.net.ssl.trustStore");
                    Console.WriteLine(new { trustStore });

                    // are we running in GUI or TTY?
                    // can we enumerate keystores?

                    // ... { xKeyStoreDefaultType = jks }


                    Action<string, Func<InputStream>> f = (keyStoreType, loadstream) =>
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
                            xKeyStore.load(loadstream(), null);
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
                    f(xKeyStoreDefaultType, () =>
                        {
                            var xx = default(FileInputStream);

                            try
                            {
                                xx = new FileInputStream(keystorepath);
                            }
                            catch { throw; }
                            return xx;
                        }
                        );
                }
                #endregion

                var w = new StringBuilder { };

                w.AppendLine(new { DateTime.Now }.ToString());


                // https://bugs.launchpad.net/ubuntu/+source/systemd/+bug/1387241
                // https://www.digitalocean.com/community/tutorials/how-to-use-systemctl-to-manage-systemd-services-and-units
                // http://unix.stackexchange.com/questions/196166/how-to-find-out-if-a-system-uses-sysv-upstart-or-systemd-initsystem
                // https://lists.ubuntu.com/archives/upstart-devel/2011-January/001370.html
                // http://askubuntu.com/questions/62790/upstart-service-never-starts-or-stops-completely
                // http://askubuntu.com/questions/19320/how-to-enable-or-disable-services
                // http://serverfault.com/questions/251982/ubuntu-upstart-script-hangs-on-start-and-stop



                //var servicesdir = new DirectoryInfo("/lib/systemd/system/");

                // https://www.centos.org/forums/viewtopic.php?t=4300
                // http://upstart.ubuntu.com/getting-started.html

                // initctl reload-configuration 
                // https://www.centos.org/forums/viewtopic.php?t=4300
                // initctl show-config
                // http://unix.stackexchange.com/questions/84252/how-to-start-a-service-automatically-when-ubuntu-starts
                // https://serversforhackers.com/video/process-monitoring-with-upstart
                // http://upstart.ubuntu.com/cookbook/

                // http://www.yyosifov.com/2014/04/upstart-syntax-error-bad-fd-number.html
                // /var/log/upstart/ubuntubootexperiment.log

                // http://serverfault.com/questions/453136/understanding-upstart-script-stanza
                // https://bugs.debian.org/cgi-bin/bugreport.cgi?bug=582745
                // http://askubuntu.com/questions/162768/starting-java-processes-with-upstart

                // https://bugs.debian.org/cgi-bin/bugreport.cgi?bug=582745
                // ps aux
                // http://askubuntu.com/questions/397502/reboot-a-server-from-command-line

                var servicesdir = new DirectoryInfo("/etc/init/");
                w.AppendLine(new { servicesdir }.ToString());


                foreach (var service in servicesdir.GetFiles())
                {
                    w.AppendLine(
                        new { service.FullName }.ToString()
                    );

                }


                var ff = fa.Directory.FullName + "/hello.txt";

                Console.WriteLine(new { ff });
                //File.WriteAllText(fadir + "/hello.txt", "hi");

                System.IO.File.WriteAllText(ff, w.ToString());


                // are we running in GUI or TTY?
                // can we enumerate keystores?

                //var sw = Stopwatch.StartNew();

                //while (sw.ElapsedMilliseconds < 20000)
                //{
                //    Console.WriteLine(new { sw.ElapsedMilliseconds });
                //    Thread.Sleep(500);
                //}

                Console.WriteLine("boot into tcp server...");

                // haha. jsc cannot use a release build version of the ref
                // nor can it call the Main again.

                // why cant main call main?
                // cuz the type name is the same?
                JVMCLRTCPServerAsync.Program2.Main2(args);


                //Thread.Sleep(30000);

                // tail -f .log
            }
            catch (Exception err)
            {

                Console.WriteLine(new { err.Message, err.StackTrace });

                Console.ReadLine();
            }


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

// need 2012!

//Y:\staging\web\java\__AnonymousTypes__UbuntuHello__i__d_jvm\__f__AnonymousType_16__19__17_0_1.java:36: error: reference to Format is
//        return __String.Format(null, "{{ AssemblyQualifiedName = {0} }}", objectArray2);
//                       ^
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:311: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000a1b(__Task.get_InternalFactory().<ScriptCoreLibJava.BCLImplementation.System.T
//                                ^
//  symbol:   method<TResult> Unwrap_06000a1b(__Task_1<__Task_1<TResult>>)
//  location: class __TaskExtensions
//  where TResult is a type-variable:
//    TResult extends Object declared in method<TResult>Run_060001b5(__Func_1<__Task_1<TResult>>)



// rebuild ScriptCoreLibJava?

//- javac
//"X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\UbuntuBootExperiment\Program.java
//Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:311: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000a1b(__Task.get_InternalFactory().<ScriptCoreLibJava.BCLImplementation.System.Thread
//                                ^
//  symbol:   method <TResult>Unwrap_06000a1b(__Task_1<__Task_1<TResult>>)
//  location: class __TaskExtensions
//  where TResult is a type-variable:
//    TResult extends Object declared in method <TResult>Run_060001b5(__Func_1<__Task_1<TResult>>)