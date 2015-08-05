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
    public static class ConsoleExtensions
    {
        [Script(OptimizedCode = "return e.uordblks;")]
        public static long __uordblks(this mallinfo e)
        {
            throw null;
        }

        public unsafe static void trace(
            string message = "",
            //int value = 0,
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            var m = malloc_h.mallinfo();
            var total = __uordblks(m);

            //var err0 = *errno_h.__errno();

            // jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c:1452:13: error: assignment discards 'volatile' qualifier from pointer target type [-Werror]
            //var err0 = *err;

            // can we clear it?
            //*err = 0;

            // x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.MessageQueue.cs:244


            var bytes = (byte[])(object)sourceFilePath;

            // scan for // or /0

            var f = 0;
            var i = 0;

            while (bytes[i] != 0x0)
            {
                if (bytes[i] == '\\')
                {
                    f = i;
                }

                i++;
            }


            fixed (byte* takeuntil = &bytes[f])
            {

                if (*errno_h.__errno() == 0)
                {
                    log.__android_log_print(
                      log.android_LogPriority.ANDROID_LOG_INFO,
                      "xNativeActivity",
                        //"line %i file %s",
                      "[%lld] %s:%i %s",

                      // only calling convention will change?
                      __arglist(
                        //sourceFilePath,
                        total,
                          takeuntil,
                          sourceLineNumber,
                          message


                      )
                  );
                }
                else
                {
                    log.__android_log_print(
                        log.android_LogPriority.ANDROID_LOG_INFO,
                        "xNativeActivity",
                        //"line %i file %s",
                        "[%lld] %s:%i %s errno: %i %s",

                        // only calling convention will change?
                        __arglist(
                        //sourceFilePath,

                        total,
                            takeuntil,
                            sourceLineNumber,
                            message,

                               *errno_h.__errno(),

                                errno_h.strerror(*errno_h.__errno())

                        )
                    );
                }
            }

            *errno_h.__errno() = 0;

        }




        public unsafe static void traces(
         string message = "",
         string value = "",
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


            var bytes = (byte[])(object)sourceFilePath;

            // scan for // or /0

            var f = 0;
            var i = 0;

            while (bytes[i] != 0x0)
            {
                if (bytes[i] == '\\')
                {
                    f = i;
                }

                i++;
            }


            fixed (byte* takeuntil = &bytes[f])
            {

                if (*errno_h.__errno() == 0)
                {
                    log.__android_log_print(
                      log.android_LogPriority.ANDROID_LOG_INFO,
                      "xNativeActivity",
                        //"line %i file %s",
                      "%s:%i %s %s",

                      // only calling convention will change?
                      __arglist(
                        //sourceFilePath,
                          takeuntil,
                          sourceLineNumber,
                          message,
                          value


                      )
                  );
                }
                else
                {
                    log.__android_log_print(
                        log.android_LogPriority.ANDROID_LOG_INFO,
                        "xNativeActivity",
                        //"line %i file %s",
                        "%s:%i %s %s errno: %i %s",

                        // only calling convention will change?
                        __arglist(
                        //sourceFilePath,
                            takeuntil,
                            sourceLineNumber,
                            message,
                            value,

                               *errno_h.__errno(),

                                errno_h.strerror(*errno_h.__errno())

                        )
                    );
                }
            }

            *errno_h.__errno() = 0;

        }



        public unsafe static void tracef(
         string message = "",
         float value = 0,
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


            var bytes = (byte[])(object)sourceFilePath;

            // scan for // or /0

            var f = 0;
            var i = 0;

            while (bytes[i] != 0x0)
            {
                if (bytes[i] == '\\')
                {
                    f = i;
                }

                i++;
            }


            fixed (byte* takeuntil = &bytes[f])
            {

                if (*errno_h.__errno() == 0)
                {
                    log.__android_log_print(
                      log.android_LogPriority.ANDROID_LOG_INFO,
                      "xNativeActivity",
                        //"line %i file %s",
                      "%s:%i %s %f",

                      // only calling convention will change?
                      __arglist(
                        //sourceFilePath,
                          takeuntil,
                          sourceLineNumber,
                          message,
                          value


                      )
                  );
                }
                else
                {
                    log.__android_log_print(
                        log.android_LogPriority.ANDROID_LOG_INFO,
                        "xNativeActivity",
                        //"line %i file %s",
                        "%s:%i %s %f errno: %i %s",

                        // only calling convention will change?
                        __arglist(
                        //sourceFilePath,
                            takeuntil,
                            sourceLineNumber,
                            message,
                            value,

                               *errno_h.__errno(),

                                errno_h.strerror(*errno_h.__errno())

                        )
                    );
                }
            }

            *errno_h.__errno() = 0;

        }



        public unsafe static void tracei64(
            string message = "",
            long value = 0,
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            var total = malloc_h.mallinfo().__uordblks();


            //var err0 = *errno_h.__errno();

            // jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c:1452:13: error: assignment discards 'volatile' qualifier from pointer target type [-Werror]
            //var err0 = *err;

            // can we clear it?
            //*err = 0;

            // x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.MessageQueue.cs:244


            var bytes = (byte[])(object)sourceFilePath;

            // scan for // or /0

            var f = 0;
            var i = 0;

            while (bytes[i] != 0x0)
            {
                if (bytes[i] == '\\')
                {
                    f = i;
                }

                i++;
            }


            fixed (byte* takeuntil = &bytes[f])
            {

                if (*errno_h.__errno() == 0)
                {
                    log.__android_log_print(
                      log.android_LogPriority.ANDROID_LOG_INFO,
                      "xNativeActivity",
                        //"line %i file %s",
                      "[%lld] %s:%i %s %lld",

                      // only calling convention will change?
                      __arglist(
                        //sourceFilePath,
                        total,
                          takeuntil,
                          sourceLineNumber,
                          message,
                          value


                      )
                  );
                }
                else
                {
                    log.__android_log_print(
                        log.android_LogPriority.ANDROID_LOG_INFO,
                        "xNativeActivity",
                        //"line %i file %s",
                        "[%lld] %s:%i %s %lld errno: %i %s",

                        // only calling convention will change?
                        __arglist(
                        //sourceFilePath,
                        total,
                            takeuntil,
                            sourceLineNumber,
                            message,
                            value,

                               *errno_h.__errno(),

                                errno_h.strerror(*errno_h.__errno())

                        )
                    );
                }
            }

            *errno_h.__errno() = 0;

        }


        public unsafe static void tracei(
            string message = "",
            int value = 0,
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            )
        {
            //var m = ;
            var total = malloc_h.mallinfo().__uordblks();

            //var err0 = *errno_h.__errno();

            // jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c:1452:13: error: assignment discards 'volatile' qualifier from pointer target type [-Werror]
            //var err0 = *err;

            // can we clear it?
            //*err = 0;

            // x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.MessageQueue.cs:244


            var bytes = (byte[])(object)sourceFilePath;

            // scan for // or /0

            var f = 0;
            var i = 0;

            while (bytes[i] != 0x0)
            {
                if (bytes[i] == '\\')
                {
                    f = i;
                }

                i++;
            }


            fixed (byte* takeuntil = &bytes[f])
            {

                if (*errno_h.__errno() == 0)
                {
                    log.__android_log_print(
                      log.android_LogPriority.ANDROID_LOG_INFO,
                      "xNativeActivity",
                        //"line %i file %s",
                      "[%lld] %s:%i %s %i",

                      // only calling convention will change?
                      __arglist(
                        //sourceFilePath,

                        total,
                          takeuntil,
                          sourceLineNumber,
                          message,
                          value


                      )
                  );
                }
                else
                {
                    log.__android_log_print(
                        log.android_LogPriority.ANDROID_LOG_INFO,
                        "xNativeActivity",
                        //"line %i file %s",
                        "[%lld] %s:%i %s %i errno: %i %s",

                        // only calling convention will change?
                        __arglist(
                        //sourceFilePath,
                        total,
                            takeuntil,
                            sourceLineNumber,
                            message,
                            value,

                               *errno_h.__errno(),

                                errno_h.strerror(*errno_h.__errno())

                        )
                    );
                }
            }

            *errno_h.__errno() = 0;

        }
    }
}
