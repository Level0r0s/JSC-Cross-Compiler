using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.android
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\android\native_window.h"
    // https://github.com/android/platform_frameworks_base/blob/master/core/jni/android_app_NativeActivity.cpp

    [Script(IsNative = true, Header = "android/native_window.h", IsSystemHeader = true)]
    public static class native_window
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\view\Surface.cs

        // http://mobilepearls.com/labs/native-android-api/

        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs


        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs
        [Script(IsNative = true)]
        public class ANativeWindow
        {
        }

        //public static void ANativeWindow_release(ref ANativeWindow window) { }
        public static void ANativeWindow_release(ANativeWindow window) { }

        public static int ANativeWindow_getWidth(ANativeWindow window) { return 0; }

        public static int ANativeWindow_getHeight(ANativeWindow window) { return 0; }

    }

}
