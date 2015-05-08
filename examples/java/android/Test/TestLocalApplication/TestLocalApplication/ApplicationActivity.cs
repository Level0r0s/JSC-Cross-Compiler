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

namespace TestLocalApplication.Activities
{
    public class LocalApplication : Application
    {
        public override void onCreate()
        {
            base.onCreate();

            Console.WriteLine("enter LocalApplication onCreate, first time?");

            // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
            Toast.makeText(this, "LocalApplication", Toast.LENGTH_LONG).show();

        }

    }


    //E/AndroidRuntime(24813): Caused by: java.lang.ClassNotFoundException: Didn't find class "TestLocalApplication.Activities.LocalApplication" on path: DexPathList[[zip f
    //E/AndroidRuntime(24813):        at dalvik.system.BaseDexClassLoader.findClass(BaseDexClassLoader.java:56)
    //E/AndroidRuntime(24813):        at java.lang.ClassLoader.loadClass(ClassLoader.java:511)
    //E/AndroidRuntime(24813):        at java.lang.ClassLoader.loadClass(ClassLoader.java:469)
    //E/AndroidRuntime(24813):        at android.app.Instrumentation.newApplication(Instrumentation.java:988)
    //E/AndroidRuntime(24813):        at android.app.LoadedApk.makeApplication(LoadedApk.java:644)
    //E/AndroidRuntime(24813):        ... 10 more
    //E/AndroidRuntime(24813):        Suppressed: java.lang.ClassNotFoundException: TestLocalApplication.Activities.LocalApplication
    //E/AndroidRuntime(24813):                at java.lang.Class.classForName(Native Method)
    //E/AndroidRuntime(24813):                at java.lang.BootClassLoader.findClass(ClassLoader.java:781)
    //E/AndroidRuntime(24813):                at java.lang.BootClassLoader.loadClass(ClassLoader.java:841)
    //E/AndroidRuntime(24813):                at java.lang.ClassLoader.loadClass(ClassLoader.java:504)
    //E/AndroidRuntime(24813):                ... 13 more
    //E/AndroidRuntime(24813):        Caused by: java.lang.NoClassDefFoundError: Class not found using the boot class loader; no stack available


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {

        protected override void onCreate(Bundle savedInstanceState)
        {
            var activity = this;

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText("before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );


        }



    }


}
