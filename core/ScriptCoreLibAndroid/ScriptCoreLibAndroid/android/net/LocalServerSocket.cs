using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/net/LocalServerSocket.java
    // http://developer.android.com/reference/android/net/LocalServerSocket.html

    [Script(IsNative = true)]
    public class LocalServerSocket
    {
        // http://alvinalexander.com/java/jwarehouse/android/core/tests/coretests/src/android/net/LocalSocketTest.java.shtml

        public LocalServerSocket(string name)
        {

        }

        public LocalSocket accept()
        {
            return null;
        }
    }
}
