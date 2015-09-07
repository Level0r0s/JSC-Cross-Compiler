using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.telephony
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/telephony/java/android/telephony/SubscriptionManager.java
    [Script(IsNative = true)]
    public class SubscriptionManager
    {
        // S6 software update needed. 650MB download.


        //D/AndroidRuntime(19052): Shutting down VM
        //E/AndroidRuntime(19052): FATAL EXCEPTION: main
        //E/AndroidRuntime(19052): Process: com.sec.android.app.camera.shootingmode.panorama3d, PID: 19052
        //E/AndroidRuntime(19052): java.lang.NoSuchMethodError: No static method getSubId(I)[I in class Landroid/telephony/SubscriptionManager; or its super classes (declaration of 'android.telephony.SubscriptionManager' appears in /system/framework/framework.jar:classes2.dex)
        //E/AndroidRuntime(19052):        at com.sec.android.app.camera.Camera.registerCallStateListener(Camera.java:10159)
        //E/AndroidRuntime(19052):        at com.sec.android.app.camera.Camera.onResume(Camera.java:8389)
        //E/AndroidRuntime(19052):        at android.app.Instrumentation.callActivityOnResume(Instrumentation.java:1255)
        //E/AndroidRuntime(19052):        at android.app.Activity.performResume(Activity.java:6495)
        //E/AndroidRuntime(19052):        at android.app.ActivityThread.performResumeActivity(ActivityThread.java:3522)
        //E/AndroidRuntime(19052):        at android.app.ActivityThread.handleResumeActivity(ActivityThread.java:3564)
        //E/AndroidRuntime(19052):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2884)
        //E/AndroidRuntime(19052):        at android.app.ActivityThread.access$900(ActivityThread.java:182)
        //E/AndroidRuntime(19052):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
        //E/AndroidRuntime(19052):        at android.os.Handler.dispatchMessage(Handler.java:102)
        //E/AndroidRuntime(19052):        at android.os.Looper.loop(Looper.java:145)
        //E/AndroidRuntime(19052):        at android.app.ActivityThread.main(ActivityThread.java:6141)
        //E/AndroidRuntime(19052):        at java.lang.reflect.Method.invoke(Native Method)
        //E/AndroidRuntime(19052):        at java.lang.reflect.Method.invoke(Method.java:372)
        //E/AndroidRuntime(19052):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
        //E/AndroidRuntime(19052):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)

    }

}
