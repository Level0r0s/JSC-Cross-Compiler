using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.os;
using android.provider;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;

namespace AndroidNDKNugetExperiment.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        //  [aapt] W:\bin\AndroidManifest.xml:14: error: Error: No resource found that matches the given name (at 'label' with value '@string/app_name').



        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this);

            b.setText(
                
                // http://stackoverflow.com/questions/19954156/android-build-separate-apks-for-different-processor-architectures

                TestNDKAsAsset.xActivity.stringFromJNI()
                //"Vibrate!"
            );

            b.AtClick(
                delegate
                {
                    var vibrator = (Vibrator)this.getSystemService(Context.VIBRATOR_SERVICE);

                    vibrator.vibrate(600);
                }
            );

            ll.addView(b);



            this.setContentView(sv);


            //this.ShowLongToast("http://my.jsc-solutions.net x");
        }


    }
}

//I/System.Console(26838): lib: libs/armeabi_v7a/libTestNDKAsAsset.so
//I/System.Console(26838): loadLibrary: TestNDKAsAsset
//E/art(26838): No implementation found for java.lang.String TestNDKAsAsset.xActivity.stringFromJNI() (tried Java_TestNDKAsAsset_xActivity_stringFromJNI and Java_TestNDKAsAsset_xActivity_stringFromJNI__)
//E/AndroidRuntime(26838): Process: AndroidNDKNugetExperiment.Activities, PID: 26838
//E/AndroidRuntime(26838): java.lang.UnsatisfiedLinkError: No implementation found for java.lang.String TestNDKAsAsset.xActivity.stringFromJNI() (tried Java_TestNDKAsAsset_xActivity_stringFromJNI and Java_TestNDKAsAsset_xActivity_stringFromJNI__)
//E/AndroidRuntime(26838):        at TestNDKAsAsset.xActivity.stringFromJNI(Native Method)
//E/AndroidRuntime(26838):        at AndroidNDKNugetExperiment.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:48)
//E/AndroidRuntime(26838):        at android.app.Activity.performCreate(Activity.java:6374)
//E/AndroidRuntime(26838):        at android.app.Instrumentation.callActivityOnCreate(Instrumentation.java:1119)
//E/AndroidRuntime(26838):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2767)
//E/AndroidRuntime(26838):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
//E/AndroidRuntime(26838):        at android.app.ActivityThread.access$900(ActivityThread.java:182)
//E/AndroidRuntime(26838):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
//E/AndroidRuntime(26838):        at android.os.Handler.dispatchMessage(Handler.java:102)
//E/AndroidRuntime(26838):        at android.os.Looper.loop(Looper.java:145)
//E/AndroidRuntime(26838):        at android.app.ActivityThread.main(ActivityThread.java:6141)
//E/AndroidRuntime(26838):        at java.lang.reflect.Method.invoke(Native Method)
//E/AndroidRuntime(26838):        at java.lang.reflect.Method.invoke(Method.java:372)
//E/AndroidRuntime(26838):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//E/AndroidRuntime(26838):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)