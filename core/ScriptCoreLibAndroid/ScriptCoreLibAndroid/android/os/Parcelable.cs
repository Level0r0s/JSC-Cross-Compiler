using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Parcelable.java
    // http://developer.android.com/reference/android/os/Parcelable.html
    [Script(IsNative = true)]
    public interface Parcelable
    {
         int describeContents();

         void writeToParcel(Parcel dest, int flags);

    }
}
