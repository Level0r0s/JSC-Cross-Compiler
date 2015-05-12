using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;

namespace com.abstractatech.hzlauncher.Activities
{
    public class LocalApplication :  global::org.chromium.chrome.shell.ChromeShellApplication
    {
        // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\src\com\oculusvr\vrlib\VrApplication.java
    
    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\src\com\oculusvr\vrlib\VrActivity.java
        // com.oculusvr.vrlib
        // ActivityGroup

        // u:\chromium\src\chrome\android\shell\java\src\org\chromium\chrome\shell\ChromeShellActivity.java
        // ActionBarActivity
        // https://developer.android.com/reference/android/support/v4/app/FragmentActivity.html
        // https://developer.android.com/reference/android/support/v7/app/AppCompatActivity.html
        // http://developer.android.com/reference/android/app/ActivityGroup.html


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText("can we show chrome and vr?");
  

            this.setContentView(sv);
        }


    }


}
