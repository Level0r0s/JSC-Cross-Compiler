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





    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "org.chromium.content.browser.NUM_SANDBOXED_SERVICES", value = "5")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "org.chromium.content.browser.NUM_PRIVILEGED_SERVICES", value = "3")]


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@style/Theme.AppCompat")]
    public class LocalApplicationActivity : 
        //android.support.v7.app.ActionBarActivity
        //com.oculusvr.vrlib.VrActivity 

        global::org.chromium.chrome.shell.ChromeShellActivity
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
            //org.chromium.chrome.shell.TabManager.DEFAULT_URL = "https://explore.xavalon.net";
            org.chromium.chrome.shell.TabManager.DEFAULT_URL = "http://192.168.1.126:27535";
            base.onCreate(savedInstanceState);
            return;

#if xgearvr

            // https://forums.oculus.com/viewtopic.php?f=67&t=22886&p=266775#p266775

            //base.onCreate(savedInstanceState);

            //var sv = new ScrollView(this);
            //var ll = new LinearLayout(this);
            ////ll.setOrientation(LinearLayout.VERTICAL);
            //sv.addView(ll);

            //var b = new Button(this).AttachTo(ll);



            //b.WithText("can we show chrome and vr?");
  

            //this.setContentView(sv);


            android.content.Intent intent = getIntent();

            // we are vr_dual arent we
            Console.WriteLine("invoke HybridOculusVrActivity.OVRJVM ApplicationActivity onCreate logApplicationVrType");
            com.oculusvr.vrlib.VrLib.logApplicationVrType(this);

            var commandString = com.oculusvr.vrlib.VrLib.getCommandStringFromIntent(intent);
            var fromPackageNameString = com.oculusvr.vrlib.VrLib.getPackageStringFromIntent(intent);
            var uriString = com.oculusvr.vrlib.VrLib.getUriStringFromIntent(intent);
            // why are we roundtriping intent details?


            Console.WriteLine("invoke base  HybridOculusVrActivity.OVRJVM ApplicationActivity nativeSetAppInterface "  );

            // https://forums.oculus.com/viewtopic.php?f=67&t=17790
            this.appPtr = HybridOculusVrActivity.OVRJVM.ApplicationActivity.nativeSetAppInterface(
                this,
                fromPackageNameString, commandString, uriString
            );

            Console.WriteLine("invoke base  HybridOculusVrActivity.OVRJVM ApplicationActivity nativeSetAppInterface done");
#endif

        }


    }


}

namespace HybridOculusVrActivity.OVRJVM
{
    // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\src\android\app\IVRManager.java

    // type name linked to NDK implementation
    public static class ApplicationActivity
    {
        static ApplicationActivity()
        {
            Console.WriteLine("enter  HybridOculusVrActivity.OVRJVM ApplicationActivity cctor, loadLibrary HybridOculusVrActivity");

            // LOCAL_MODULE    := HybridOculusVrActivity
            // X:\jsc.svn\examples\c\android\Test\HybridOculusVrActivity\HybridOculusVrActivity\staging\jni\Android.mk

            // PInvoke wont load implictly? 
            java.lang.System.loadLibrary("HybridOculusVrActivity");

            Console.WriteLine("enter  HybridOculusVrActivity.OVRJVM ApplicationActivity cctor, loadLibrary HybridOculusVrActivity done");
        }

        //appPtr = nativeSetAppInterface( this );       
        //public static native long nativeSetAppInterface( VrActivity act ); 





        // X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\jni\App.cpp
        // OVR::VrAppInterface* SetActivity

        [Script(IsPInvoke = true)]
        public static long nativeSetAppInterface(object act,
            string fromPackageNameString, string commandString, string uriString)
        { return default(long); }



        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }
    }
}
