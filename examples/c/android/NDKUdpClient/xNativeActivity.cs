using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static ScriptCoreLibNative.SystemHeaders.sys.socket_h;
using System.Reflection;
using System.Runtime.CompilerServices;
using ScriptCoreLibAndroidNDK.Library;
using System.Net.Sockets;
using System.Net;
using System.Threading;

[assembly: Obfuscation(Feature = "script")]

namespace NDKUdpClient.Activities
{
    public class xNativeActivity : ScriptCoreLibAndroidNDK.IAssemblyReferenceToken
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // restarting in TCP mode port: 5555


        // on red
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect  192.168.1.126:5555
        // connected to 192.168.1.126:5555

        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n NDKUdpClient.Activities/NDKUdpClient.Activities.xNativeActivity
        // x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n NDKUdpClient.Activities/android.app.NativeActivity

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160108/udp



        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG"



        // double click on exe./metro?
        // and have it run on android?

        // Error: X:\jsc.svn\examples\c\android\Test\NDKUdpClient\NDKUdpClient\bin\Debug is not a valid project (AndroidManifest.xml not found).


        // https://msdn.microsoft.com/en-us/library/hh534540.aspx
        //static void trace(
        //    string message = "",
        //    [CallerFilePath] string sourceFilePath = "",
        //    [CallerLineNumber] int sourceLineNumber = 0)

        //    // ?
        //    => log.__android_log_print(
        //        log.android_LogPriority.ANDROID_LOG_INFO,
        //        "xNativeActivity",
        //        //"line %i file %s",
        //        "%s:%i %s",
        //        __arglist(
        //            sourceFilePath,
        //            sourceLineNumber,
        //            message
        //        )
        //    );


        unsafe static void trace(
    byte* message,
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0)
        {
            log.__android_log_print(
                log.android_LogPriority.ANDROID_LOG_INFO,
                "xNativeActivity",
                //"line %i file %s",
                "%s:%i %s",
                __arglist(
              sourceFilePath,
              sourceLineNumber,
              message
                )
                );
        }


        //    unsafe static void tracei(
        //string message = "",
        //int value = 0,
        //[CallerFilePath] string sourceFilePath = "",
        //[CallerLineNumber] int sourceLineNumber = 0)
        //=> log.__android_log_print(
        //    log.android_LogPriority.ANDROID_LOG_INFO,
        //    "xNativeActivity",
        //    //"line %i file %s",
        //    "%s:%i %s %i errno: %i %s",
        //    __arglist(
        //        sourceFilePath,
        //        sourceLineNumber,
        //        message,
        //        value,

        //            *errno_h.__errno(),

        //            errno_h.strerror(*errno_h.__errno())

        //    )
        //);


        // http://stackoverflow.com/questions/24581245/send-broadcast-from-c-code


        [Script(NoDecoration = true)]
        unsafe static void android_main(android_native_app_glue.android_app state)
        {
            // http://elfsharp.hellsgate.pl/examples.shtml
            // https://msdn.microsoft.com/en-us/library/dd554932(VS.100).aspx
            // http://mobilepearls.com/labs/native-android-api/

            // X:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\Templates\Java\InternalPopupWebView\XWindow.cs
            // http://stackoverflow.com/questions/13249164/android-using-jni-from-nativeactivity

            android_native_app_glue.app_dummy();
            //Action<

            ConsoleExtensions.trace("before UdpClient");

            var uu = new UdpClient(49814);
            uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"));
            while (true)
            {
                // cannot get data from RED?
                //var x = await uu.ReceiveAsync(); // did we jump to ui thread?
                var x = uu.ReceiveAsync(); // did we jump to ui thread?

                if (x == null)
                {
                    //Environment.FailFast();
                    ConsoleExtensions.trace("after ReceiveAsync null");
                    Thread.Sleep(1000);
                }
                else
                {
                    ConsoleExtensions.trace("after ReceiveAsync...");

                    x.Wait();

                    // http://stackoverflow.com/questions/34168791/ndk-work-with-floatbuffer-as-parameter
                    // http://stackoverflow.com/questions/4841345/sending-ints-between-java-and-c


                    var xResult = (ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets.__UdpReceiveResult)(object)x.Result;

                    ConsoleExtensions.tracei64("xResult: ", (long)(object)xResult);


                    // can we store the length of the buffer in int to the left?
                    var buffer = xResult.Buffer;

                    // syntax suggar.
                    // script: error JSC1000: C : unable to emit ldlen at 'NDKUdpClient.Activities.xNativeActivity.android_main'#00a1: C runtime cannot tell the length of an array;

                    var bufferLength = buffer.Length;
                    // jsc should apply ldlen prefix to all arrays.

                    ConsoleExtensions.tracei("after ReceiveAsync bufferLength ", bufferLength);

                    // cool. jsc does not yet do the right thing for CLR structs for which we implemnt it as a class...


                    Thread.Sleep(100);
                }
            }



        }

    }
}


