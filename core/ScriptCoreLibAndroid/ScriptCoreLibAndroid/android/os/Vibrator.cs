using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.os
{
    // https://android.googlesource.com/platform/frameworks/base/+/f96135857f2f3de12576174712d6bea8b363277d/services/jni/com_android_server_VibratorService.cpp?autodive=0%2F
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/os/Vibrator.java

    // http://developer.android.com/reference/android/os/Vibrator.html
    [Script(IsNative = true)]
    public abstract class Vibrator
    {
        // tested by
        // X:\jsc.svn\examples\java\android\AndroidVibrationActivity\AndroidVibrationActivity\ApplicationActivity.cs

        public abstract void vibrate(long milliseconds);
    }
}
