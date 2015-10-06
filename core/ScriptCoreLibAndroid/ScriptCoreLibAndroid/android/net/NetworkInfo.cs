using android.os;
using java.io;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.net
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/net/NetworkInfo.java
    // http://developer.android.com/reference/android/net/NetworkInfo.html

    [Script(IsNative = true)]
    public class NetworkInfo : Parcelable
    {

        public int describeContents()
        {
            throw new NotImplementedException();
        }

        public void writeToParcel(Parcel dest, int flags)
        {
            throw new NotImplementedException();
        }


        public bool isConnected() { throw null; }
    }
}
