using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // http://developer.android.com/reference/android/os/Binder.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/android-4.3_r2.1/core/jni/android_util_Binder.cpp
    // https://android.googlesource.com/platform/frameworks/native/+/jb-dev/libs/binder/Binder.cpp
    // http://stackoverflow.com/questions/14215462/how-to-create-a-android-native-service-and-use-binder-to-communicate-with-it

    [Script(IsNative = true)]
    public class Binder
    {
        //  can't use Binder because it's not part of the NDK APIs.

    }

}
