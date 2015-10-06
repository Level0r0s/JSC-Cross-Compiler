using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/net/ConnectivityManager.java
    // http://developer.android.com/reference/android/net/ConnectivityManager.html

    [Script(IsNative = true)]
    public class ConnectivityManager
    {
        public NetworkInfo getNetworkInfo(int networkType)
        {
            throw null;
        }
        public NetworkInfo getNetworkInfo(Network network)
        {
            throw null;
        }

        public static readonly int TYPE_WIFI;
    }
}
