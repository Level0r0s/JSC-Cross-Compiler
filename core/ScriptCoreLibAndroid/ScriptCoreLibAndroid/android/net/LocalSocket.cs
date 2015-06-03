using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/net/LocalSocket.java
    // http://developer.android.com/reference/android/net/LocalSocket.html

    [Script(IsNative = true)]
    public class LocalSocket
    {
        // http://alvinalexander.com/java/jwarehouse/android/core/tests/coretests/src/android/net/LocalSocketTest.java.shtml

        public void connect(LocalSocketAddress endpoint)
        {

        }

        public InputStream getInputStream()
        {
            return null;
        }

        public OutputStream getOutputStream()
        {
            return null;
        }
    }
}
