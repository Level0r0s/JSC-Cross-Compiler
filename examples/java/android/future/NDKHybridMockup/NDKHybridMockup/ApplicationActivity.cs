using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using android.content;
using ScriptCoreLibNative.SystemHeaders.android;

namespace NDKHybridMockup.Activities
{



    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201506/20150602/ndk
        // X:\jsc.svn\examples\c\android\HybridOculusVrActivity\HybridOculusVrActivity\OVRJVM\ApplicationActivity.cs
        // X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\xActivity.cs
        public static string stringFromJNI()
        // x:\jsc.svn\examples\java\android\synergy\ovrvrcubeworldsurfaceview\ovrvrcubeworldsurfaceviewxndk\vrcubeworld.cs

        // ref JNIEnv env
        // var n = env.NewStringUTF;
        {
            // NDK heap!
            // CLR API now unavailable.
            // NDK subspace


            // referencing NDK code should mark call site NDK marshallable
            log.__android_log_print(
                  log.android_LogPriority.ANDROID_LOG_INFO,
                  "NDKHybridMockup",
                  "enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI"
            );

            // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\os\MemoryFile.cs

            return "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI. yay";
        }

        protected override void onCreate(Bundle savedInstanceState)
        {
            var activity = this;

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            new Button(this).AttachTo(ll).WithText("invoke").AtClick(
                button =>
                {
                    button.setText("not from NDK");
                }
            );

            new Button(this).AttachTo(ll).WithText("invoke NDK via static").AtClick(
                button =>
                {
                    button.setText(stringFromJNI());
                }
            );


            new Button(this).AttachTo(ll).WithText("invoke NDK via closure").AtClick(
                 button =>
                 {
                     Func<string> closure_stringFromJNI = delegate
                     {
                         // NDK heap!
                         // seemingly we share scope. yet CLR API is exactly no longer available.

                         // CLR API now unavailable.
                         // NDK subspace


                         // referencing NDK code should mark call site NDK marshallable
                         log.__android_log_print(
                               log.android_LogPriority.ANDROID_LOG_INFO,
                               "NDKHybridMockup",
                               "enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI"
                         );

                         return "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI. yay";
                     };

                     button.setText(closure_stringFromJNI());
                 }
             );

            this.setContentView(sv);
        }


    }


}

// without any special NDK support what we get?

//    1>C:\Program Files(x86)\MSBuild\14.0\bin\Microsoft.Common.CurrentVersion.targets(1819,5): warning MSB3274: The primary reference "ScriptCoreLibAndroidNDK" could not be resolved because it was built against the ".NETFramework,Version=v4.6" framework.This is a higher version than the currently targeted framework ".NETFramework,Version=v4.0".
//1>X:\jsc.svn\examples\java\android\future\NDKHybridMockup\NDKHybridMockup\ApplicationActivity.cs(14,7,14,26): error CS0246: The type or namespace name 'ScriptCoreLibNative' could not be found(are you missing a using directive or an assembly reference?)

//[javac]
//W:\src\NDKHybridMockup\Activities\ApplicationActivity.java:21: error: package ScriptCoreLibNative.SystemHeaders.android does not exist
//[javac] import ScriptCoreLibNative.SystemHeaders.android.log;
//[javac]                                                 ^
// [javac]
//W:\src\NDKHybridMockup\Activities\ApplicationActivity___c.java:13: error: package ScriptCoreLibNative.SystemHeaders.android does not exist
//[javac] import ScriptCoreLibNative.SystemHeaders.android.log;
//[javac]                                                 ^
// [javac]
//W:\src\NDKHybridMockup\Activities\ApplicationActivity.java:84: error: cannot find symbol
//[javac] log.__android_log_print(4, "NDKHybridMockup", "enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI");
//[javac]         ^
// [javac]
//symbol:   variable log
//[javac]   location: class ApplicationActivity
// [javac]
//W:\src\NDKHybridMockup\Activities\ApplicationActivity___c.java:62: error: cannot find symbol
//[javac] log.__android_log_print(4, "NDKHybridMockup", "enter Java_HybridOculusVrActivity_OVRJVM_ApplicationActivity_stringFromJNI");
//[javac]         ^
// [javac]
//symbol:   variable log
//[javac]   location: class ApplicationActivity___c