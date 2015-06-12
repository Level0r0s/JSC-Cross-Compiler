using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/jni/android_util_Process.cpp
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Process.java
    // http://developer.android.com/reference/android/os/Process.html

    [Script(IsNative = true)]
    public class Process
    {
        public static int THREAD_PRIORITY_URGENT_DISPLAY = -8;

        // X:\jsc.svn\examples\java\android\Test\TestNuGetAssetsConsumer\TestNuGetAssetsConsumer\ApplicationActivity.cs
        public static int myPid() { return 0; }

        public static int myUid() { return 0; }

        public static int getUidForPid(int pid) { return 0; }

        public static readonly int SIGNAL_KILL = 9;

        /**
       * @hide
       * Private impl for avoiding a log message...  DO NOT USE without doing
       * your own log, or the Android Illuminati will find you some night and
       * beat you up.
       */
        public static void killProcessQuiet(int pid)
        {
            sendSignalQuiet(pid, SIGNAL_KILL);
        }
        /**
         * @hide
         * Private impl for avoiding a log message...  DO NOT USE without doing
         * your own log, or the Android Illuminati will find you some night and
         * beat you up.
         */
        public static void sendSignalQuiet(int pid, int signal) { }

    }
}
