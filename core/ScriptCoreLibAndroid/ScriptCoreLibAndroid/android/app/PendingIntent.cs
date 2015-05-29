using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/PendingIntent.java
    // http://developer.android.com/reference/android/app/PendingIntent.html

    [Script(IsNative = true)]
    public class PendingIntent
    {
        public int describeContents() { return 0; }

        // members and types are to be extended by jsc at release build

        public static int FLAG_UPDATE_CURRENT;

        public void send()
        {
        }

        public static PendingIntent getService(Context context, int requestCode, Intent intent, int flags)
        {
            throw null;
        }


        public static PendingIntent getActivity(Context context, int requestCode, Intent intent, int flags)
        {
            return null;
        }
    }

}
