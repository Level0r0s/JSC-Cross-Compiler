using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/SystemClock.java

    [Script(IsNative = true)]
    public class SystemClock
    {
        public static long uptimeMillis()
        {
            throw null;
        }
    }
}
