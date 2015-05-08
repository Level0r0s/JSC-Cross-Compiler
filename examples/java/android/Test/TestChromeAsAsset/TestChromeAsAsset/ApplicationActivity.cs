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
using android.content.res;

namespace TestChromeAsAsset.Activities
{


    public class LocalApplication : 
        //Application

        global::org.chromium.chrome.shell.ChromeShellApplication
    {
        // https://developer.android.com/reference/android/support/multidex/MultiDexApplication.html
        // can we get AIR via it?
        // https://android.googlesource.com/platform/frameworks/multidex/+/master/library/src/android/support/multidex/MultiDexApplication.java

        public override void onCreate()
        {

            Console.WriteLine("enter LocalApplication onCreate, first time? "
                // chrome java
                // U:\chromium\src\out\Release\lib.java\chrome_java.jar\org\chromium\chrome\browser\invalidation\
                + " " + typeof(global::org.chromium.chrome.browser.invalidation.UniqueIdInvalidationClientNameGenerator)
                );

            // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
            Toast.makeText(this, "LocalApplication", Toast.LENGTH_LONG).show();

            base.onCreate();
        }

    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://groups.google.com/forum/#!topic/android-developers/Y5wnstMT5Lo


        static ApplicationActivity()
        {
            Console.WriteLine("should we prefetch our .so for JNI_OnLoad?");
            // U:\chromium\src\chrome\android\shell\chrome_shell_entry_point.cc

            // couldn't find "liblibchromeshell.so"
            java.lang.System.loadLibrary("chromeshell");
        }

        protected override void onCreate(Bundle savedInstanceState)
        {
            // can we make chrome into a nuget?

            var activity = this;
            // http://stackoverflow.com/questions/11425020/actionbar-in-a-dialogfragment
            //To show activity as dialog and dim the background, you need to declare android:theme="@style/PopupTheme" on for the chosen activity on the manifest
            //activity.requestWindowFeature(Window.FEATURE_ACTION_BAR);
            //activity.getWindow().setFlags(WindowManager_LayoutParams.FLAG_DIM_BEHIND, WindowManager_LayoutParams.FLAG_DIM_BEHIND);
            //activity.getWindow().setFlags(WindowManager_LayoutParams.FLAG_TRANSLUCENT_STATUS, WindowManager_LayoutParams.FLAG_TRANSLUCENT_STATUS);
            //var @params = activity.getWindow().getAttributes();
            ////@params.height = WindowManager_LayoutParams.FILL_PARENT;
            ////@params.width = 850; //fixed width
            ////@params.height = 450; //fixed width
            //@params.alpha = 1.0f;
            //@params.dimAmount = 0.5f;
            //activity.getWindow().setAttributes(@params);
            //activity.getWindow().setLayout(850, 850);
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText("base: "
                + typeof(global::org.chromium.@base.BaseChromiumApplication)
                + " " + typeof(global::org.chromium.content.app.ContentApplication)
                + " " + typeof(global::org.chromium.chrome.browser.ChromiumApplication)

                // this one wont be from the jar files...
                + " " + typeof(global::org.chromium.chrome.shell.ChromeShellApplication)

                //+ " " + typeof(global::org.chromium.ui.gfx.DeviceDisplayInfo)
                //+ " " + typeof(global::org.chromium.net.GURLUtils)
                //+ " " + typeof(global::org.chromium.content.browser.LocationProviderAdapter)

                 //[javac] W:\src\TestChromeAsAsset\Activities\ApplicationActivity.java:22: error: AudioManagerAndroid is not public in org.chromium.media; cannot be accessed from outside package
                //+ " " + typeof(global::org.chromium.media.AudioManagerAndroid)
                //+ " " + typeof(global::org.chromium.mojo.system.impl.CoreImpl)

                
                //+ " " + global::org.chromium.@base.BaseChromiumApplication.__hello()
                //+ " nativeGetCoreCount: " + org.chromium.@base.CpuFeatures.getCount()
                );


            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );


          
        }

      

    }


}
