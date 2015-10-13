using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using android.content;
using java.security;
using ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.X509Certificates;

namespace AndroidKeyStoreExperiment.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // restarting in TCP mode port: 5555

        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // connected to 192.168.1.126:5555

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150513

        // lets verify this thing . deploy over wifi.

        //C:\Windows\system32> "x:\util\android-sdk-windows\platform-tools\adb.exe" shell netcfg
        //wlan0    UP                               192.168.1.126/24  0x00001043 e8:50:8b:7d:27:7c

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            //var b = new Button(this).AttachTo(ll);

            //b.WithText("before AtClick");
            //b.AtClick(
            //    v =>
            //    {
            //        b.setText("AtClick");
            //    }
            //);


            this.setContentView(sv);





            // http://www.java2s.com/Tutorial/Java/0490__Security/KeyStoreExample.htm

            //I/System.Console( 5182): 143e:0001 ... { xKeyStoreDefaultType = BKS }
            //I/System.Console( 5182): 143e:0001 { xKeyStore = java.security.KeyStore@274cc17e }
            //I/System.Console( 5182): 143e:0001 load... { keyStoreType = BKS }
            //I/System.Console( 5182): 143e:0001 aliases...


            var xKeyStoreDefaultType = java.security.KeyStore.getDefaultType();

            Console.WriteLine("... " + new { xKeyStoreDefaultType });

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
                catch (Exception err)
                {
                    Console.WriteLine(new { err.Message, err.StackTrace });

                }
            };

            //hello ubuntu! { AssemblyQualifiedName = java.lang.Object, rt }
            //... { xKeyStoreDefaultType = jks }
            //{ xKeyStore = java.security.KeyStore@9980d5 }
            //load... { keyStoreType = jks }
            //aliases...
            //done


            new Button(this).AttachTo(ll).WithText(xKeyStoreDefaultType).AtClick(
                delegate
                {

                    // on RED there are no entries?
                    // what about ubuntu?
                    f(xKeyStoreDefaultType);
                }
            );
            //I/System.Console( 7167): 1bff:0001 load... { keyStoreType = AndroidKeyStore }
            //I/System.Console( 7167): 1bff:0001 aliases...


            new Button(this).AttachTo(ll).WithText("AndroidKeyStore").AtClick(
               delegate
               {

                   // on RED there are no entries?
                   // what about ubuntu?
                   f("AndroidKeyStore");
               }
           );



        }



    }


}

//D/AndroidRuntime( 2246): Shutting down VM
//E/AndroidRuntime( 2246): FATAL EXCEPTION: main
//E/AndroidRuntime( 2246): Process: AndroidKeyStoreExperiment.Activities, PID: 2246
//E/AndroidRuntime( 2246): java.lang.RuntimeException: Unable to start activity ComponentInfo{AndroidKeyStoreExperiment.Activities/Androi
//E/AndroidRuntime( 2246):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:3119)
//E/AndroidRuntime( 2246):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:3218)
//E/AndroidRuntime( 2246):        at android.app.ActivityThread.access$1000(ActivityThread.java:198)
//E/AndroidRuntime( 2246):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1676)
//E/AndroidRuntime( 2246):        at android.os.Handler.dispatchMessage(Handler.java:102)
//E/AndroidRuntime( 2246):        at android.os.Looper.loop(Looper.java:145)
//E/AndroidRuntime( 2246):        at android.app.ActivityThread.main(ActivityThread.java:6837)
//E/AndroidRuntime( 2246):        at java.lang.reflect.Method.invoke(Native Method)
//E/AndroidRuntime( 2246):        at java.lang.reflect.Method.invoke(Method.java:372)
//E/AndroidRuntime( 2246):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1404)
//E/AndroidRuntime( 2246):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1199)
//E/AndroidRuntime( 2246): Caused by: java.lang.RuntimeException
//E/AndroidRuntime( 2246):        at ScriptCoreLibJava.Extensions.BCLImplementationExtensions.GetDeclaringFile(BCLImplementationExtension
//E/AndroidRuntime( 2246):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__Assembly.get_InternalDeclaringFile(__Assembl
//E/AndroidRuntime( 2246):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__Assembly.get_Location(__Assembly.java:46)
//E/AndroidRuntime( 2246):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__Assembly.get_FullName(__Assembly.java:41)
//E/AndroidRuntime( 2246):        at ScriptCoreLibJava.BCLImplementation.System.__Type.get_AssemblyQualifiedName(__Type.java:933)
//E/AndroidRuntime( 2246):        at AndroidKeyStoreExperiment.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:61)
//E/AndroidRuntime( 2246):        at android.app.Activity.performCreate(Activity.java:6500)
//E/AndroidRuntime( 2246):        at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1120)
//E/AndroidRuntime( 2246):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:3072)
//E/AndroidRuntime( 2246):        ... 10 more
