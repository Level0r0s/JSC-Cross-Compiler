using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\android\native_window_jni.h"

    [Script(IsNative = true, Header = "android/native_window_jni.h", IsSystemHeader = true)]
    public static class native_window_jni
    {
        //ANativeWindow* ANativeWindow_fromSurface(JNIEnv* env, jobject surface);

        // 
        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs

        // ANativeWindow

        public static native_window.ANativeWindow ANativeWindow_fromSurface( JNIEnv env, jobject surface) { return null; }

    }

}
