using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // http://developer.android.com/reference/android/os/StatFs.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/StatFs.java

    [Script(IsNative = true)]
    public class StatFs
    {
        public long getAvailableBytes() { throw null; }
        public StatFs(string path)
        {

        }
    }
}
