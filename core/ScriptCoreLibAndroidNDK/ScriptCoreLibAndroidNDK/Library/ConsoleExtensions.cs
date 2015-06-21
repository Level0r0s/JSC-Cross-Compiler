using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.Library
{
    [Script]
    public class ConsoleExtensions
    {

        public unsafe static void tracei(
            string message = "",
            int value = 0,
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            //var err0 = *errno_h.__errno();

            // jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c:1452:13: error: assignment discards 'volatile' qualifier from pointer target type [-Werror]
            //var err0 = *err;

            // can we clear it?
            //*err = 0;

            // x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.MessageQueue.cs:244

            log.__android_log_print(
                log.android_LogPriority.ANDROID_LOG_INFO,
                "xNativeActivity",
                //"line %i file %s",
                "%s:%i %s %i errno: %i %s",

                // only calling convention will change?
                __arglist(
                    sourceFilePath,
                    sourceLineNumber,
                    message,
                    value,

                       *errno_h.__errno(),

                        errno_h.strerror(*errno_h.__errno())

                )
            );

            *errno_h.__errno() = 0;

        }
    }
}
