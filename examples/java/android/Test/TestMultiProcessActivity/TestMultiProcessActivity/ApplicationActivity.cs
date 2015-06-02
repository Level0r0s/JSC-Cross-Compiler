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
using ScriptCoreLibJava.Extensions;

namespace TestMultiProcessActivity.Activities
{


    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    public class LocalApplication : 
        // can we change our base depeinding on the process we are in?
        Application // : ApplicationWebService ?
    {
        public override void onCreate()
        {
            base.onCreate();

            // yes we are loaded for both processes.
            {
                var myPid = android.os.Process.myPid();

                Console.WriteLine("enter LocalApplication onCreate, first time? " + new { myPid });

                // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
                Toast.makeText(this, "TestMultiProcessActivity " + new { myPid }, Toast.LENGTH_LONG).show();
            }

            // . To open these resources with a raw InputStream, call Resources.openRawResource() with the resource ID, which is R.raw.filename.
            // However, if you need access to original file names and file hierarchy, you might consider saving some resources in the assets/ directory (instead of res/raw/). Files in assets/ are not given a resource ID, so you can read them only using AssetManager.


            #region ApplicationActivity
            ApplicationActivity.vCreate = (activity, savedInstanceState) =>
            {
                var myPid = android.os.Process.myPid();

                activity.setTitle("root " + new { myPid });

                var sv = new ScrollView(activity);
                var ll = new LinearLayout(activity);
                ll.setOrientation(LinearLayout.VERTICAL);
                sv.addView(ll);

                activity.setContentView(sv);

                new Button(activity).WithText("Next").AttachTo(ll).AtClick(
                    delegate
                    {
                        Intent intent = new Intent(activity, typeof(SecondaryActivity).ToClass());
                        intent.setFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);

                        // share scope
                        intent.putExtra("_item", "hello from " + new { myPid });

                        //intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);

                        // cached backgroun process?
                        // switching to another process.. easy...
                        activity.startActivity(intent);
                    }
                );

                new Button(activity).WithText("Third").AttachTo(ll).AtClick(
                       delegate
                       {
                           Intent intent = new Intent(activity, typeof(ThirdActivity).ToClass());
                           intent.setFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);

                           // share scope
                           intent.putExtra("_item", "hello from " + new { myPid });

                           //intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);

                           // cached backgroun process?
                           // switching to another process.. easy...
                           activity.startActivity(intent);
                       }
                   );

                new Button(activity).WithText("Foo5Activity").AttachTo(ll).AtClick(
                       delegate
                       {
                           Intent intent = new Intent(activity, typeof(Foo5Activity).ToClass());
                           intent.setFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);

                           // share scope
                           intent.putExtra("_item", "hello from " + new { myPid });

                           //intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);

                           // cached backgroun process?
                           // switching to another process.. easy...
                           activity.startActivity(intent);
                       }
                   );
            };
            #endregion

            // 
            #region SecondaryActivity
            // how will CLR work, on the inherited static field level?
            SecondaryActivity.vCreate = (activity, savedInstanceState) =>
            {
                var sv = new ScrollView(activity);
                var ll = new LinearLayout(activity);
                ll.setOrientation(LinearLayout.VERTICAL);
                sv.addView(ll);
                activity.setContentView(sv);



                // http://stackoverflow.com/questions/19631894/is-there-a-way-to-get-current-process-name-in-android
                // http://stackoverflow.com/questions/6567768/how-can-an-android-application-have-more-than-one-process
                var myPid = android.os.Process.myPid();

                activity.setTitle("" + new { myPid });


                new Button(activity).WithText("exit").AttachTo(ll).AtClick(
                    delegate
                    {

                        // will it be logged?
                        System.Environment.Exit(13);
                    }
                );

            };
            #endregion

        }

    }


    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        public static Action<ApplicationActivity, Bundle> vCreate;

        protected override void onCreate(Bundle savedInstanceState)
        {
            // http://www.mkyong.com/android/android-activity-from-one-screen-to-another-screen/

            base.onCreate(savedInstanceState);
            vCreate(this, savedInstanceState);
        }
    }

    // http://developer.android.com/guide/topics/manifest/activity-element.html
    // http://developer.android.com/guide/topics/manifest/activity-element.html#multi
    // http://developer.android.com/guide/topics/manifest/activity-element.html#proc
    // http://stackoverflow.com/questions/7142921/usage-of-androidprocess

    // This process had its own 16MB heap,

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":foo1")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Light.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent.NoTitleBar")]
    //public class SecondaryActivity : ScriptCoreLib.Android.CoreAndroidWebServiceActivity
    public class SecondaryActivity : SecondaryAbstractActivity<SecondaryActivity>
    {

    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":foo3")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent.NoTitleBar")]
    //public class SecondaryActivity : ScriptCoreLib.Android.CoreAndroidWebServiceActivity
    public class ThirdActivity : SecondaryAbstractActivity<SecondaryActivity>
    {

    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":foo5")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Dialog")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent.NoTitleBar")]
    //public class SecondaryActivity : ScriptCoreLib.Android.CoreAndroidWebServiceActivity
    public class Foo5Activity : SecondaryAbstractActivity<SecondaryActivity>
    {

    }

    // jsc do you work with abstract_
    public abstract class SecondaryAbstractActivity : Activity
    {
        public static Action<Activity, Bundle> vCreate;
    }

    public abstract class SecondaryAbstractActivity<TActivity> : SecondaryAbstractActivity
    // jsc wont like it yet

        //where TActivity : SecondaryAbstractActivity<TActivity>
    {
        //[javac]     W:\gen\TestMultiProcessActivity\Activities\R.java
        //[javac] W:\src\TestMultiProcessActivity\Activities\SecondaryAbstractActivity_1.java:16: error: non-static type variable TActivity cannot be referenced from a static context
        //[javac]     public static __Action_2<SecondaryAbstractActivity_1<TActivity>, Bundle> vCreate;

        // would jsc need to downgrade/redirect such fields?
        //public static Action<SecondaryAbstractActivity<TActivity>, Bundle> vCreate;
        // jsc rewriter would need to do magic/baking here?

        protected override void onCreate(Bundle savedInstanceState)
        {
            // http://www.mkyong.com/android/android-activity-from-one-screen-to-another-screen/

            base.onCreate(savedInstanceState);
            vCreate(this, savedInstanceState);
        }
    }
}
