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
using ScriptCoreLibJava.Extensions;
using System.Diagnostics;

namespace TestNuGetAssetsConsumer.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    public class LocalApplication : Application
    {
        public override void onCreate()
        {
            base.onCreate();

            Console.WriteLine("enter LocalApplication onCreate, first time?");

            // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
            Toast.makeText(this, "LocalApplication", Toast.LENGTH_LONG).show();

            // . To open these resources with a raw InputStream, call Resources.openRawResource() with the resource ID, which is R.raw.filename.
            // However, if you need access to original file names and file hierarchy, you might consider saving some resources in the assets/ directory (instead of res/raw/). Files in assets/ are not given a resource ID, so you can read them only using AssetManager.

            var listByRoot = default(Action<Activity, LinearLayout, string>);

            listByRoot =
                (activity, ll, root) =>
                {
                    try
                    {
                        // http://developer.android.com/reference/android/content/res/AssetManager.html
                        var assets = activity.getResources().getAssets();

                        var list = assets.list(root);


                        new Button(activity).WithText("assets: " + new { list.Length, root }).AttachTo(ll);

                        foreach (var item in list)
                        {
                            //                            E/AndroidRuntime(25423): Caused by: java.io.FileNotFoundException: images
                            //E/AndroidRuntime(25423):        at android.content.res.AssetManager.openNonAssetFdNative(Native Method)

                            //var a = fd.getFileDescriptor();
                            var _item = item;

                            new Button(activity).AttachTo(ll).With(
                                i =>
                                {
                                    var fd = default(AssetFileDescriptor);

                                    try
                                    {
                                        // http://stackoverflow.com/questions/5647253/is-there-a-way-to-open-file-as-file-object-from-androids-assets-folder

                                        fd = assets.openFd(item);
                                        //fd = assets.openNonAssetFd(item);
                                        i.WithText(root + "/" + item + " " + new { Length = fd.getLength() });
                                    }
                                    catch
                                    {
                                        i.WithText("dir: " + item);

                                        i.AtClick(
                                            delegate
                                            {
                                                // hop to another activity

                                                Intent intent = new Intent(activity, typeof(SecondaryActivity).ToClass());
                                                intent.setFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);

                                                // share scope
                                                intent.putExtra("_item", _item);

                                                //intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);
                                                activity.startActivity(intent);

                                                //E/AndroidRuntime(10688): Caused by: android.util.AndroidRuntimeException: Calling startActivity() from outside of an Activity  context requires the FLAG_ACTIVITY_NEW_TASK flag. Is this really what you want?
                                                //E/AndroidRuntime(10688):        at android.app.ContextImpl.startActivity(ContextImpl.java:1611)
                                                //E/AndroidRuntime(10688):        at android.app.ContextImpl.startActivity(ContextImpl.java:1598)
                                                //E/AndroidRuntime(10688):        at android.content.ContextWrapper.startActivity(ContextWrapper.java:337)

                                                //listByRoot(_item);
                                            }
                                        );

                                    }

                                }
                            );
                        }
                    }
                    catch
                    {
                        throw;
                    }
                };

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

                // how many readonly assets have we added via nugets?

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150526


                //[javac] W:\src\TestNuGetAssetsConsumer\Activities\ApplicationActivity___c__DisplayClass1.java:32: error: unreported exception IOException; must be caught or declared to be thrown
                //[javac]         stringArray0 = assets.list("");



                listByRoot(activity, ll, "");

            };
            #endregion

            #region SecondaryActivity
            SecondaryActivity.vCreate = (activity, savedInstanceState) =>
            {
                var sv = new ScrollView(activity);
                var ll = new LinearLayout(activity);
                ll.setOrientation(LinearLayout.VERTICAL);
                sv.addView(ll);
                activity.setContentView(sv);

                // resume scope
                var _item = activity.getIntent().getExtras().getString("_item");

                // http://stackoverflow.com/questions/19631894/is-there-a-way-to-get-current-process-name-in-android
                // http://stackoverflow.com/questions/6567768/how-can-an-android-application-have-more-than-one-process
                var myPid = android.os.Process.myPid();
                //Process.GetCurrentProcess().Id;

                //activity.getPackageManager

                activity.setTitle(_item + new { myPid });


                //b.WithText("! secondary " + new { _item });
                //b.AtClick(
                //    v =>
                //    {
                //        activity.finish();
                //    }
                //);

                listByRoot(activity, ll, _item);

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

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Light.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent.NoTitleBar")]
    //public class SecondaryActivity : ScriptCoreLib.Android.CoreAndroidWebServiceActivity
    public class SecondaryActivity : Activity
    {
        public static Action<SecondaryActivity, Bundle> vCreate;

        protected override void onCreate(Bundle savedInstanceState)
        {
            // http://www.mkyong.com/android/android-activity-from-one-screen-to-another-screen/

            base.onCreate(savedInstanceState);
            vCreate(this, savedInstanceState);
        }
    }
}

//A third party open source project might have forgotten to commit some files...
//Source files may be in different folders...
//System.ArgumentOutOfRangeException: Specified argument was out of the range of valid values.
//   at jsc.meta.Commands.Reference.ReferenceAssetsLibrary.InternalInvoke() in x:\jsc.internal.git\compiler\jsc.internal\jsc.internal\meta\Commands\Reference\ReferenceAssetsLibrary.cs:line 1658

//0001 0200054a ScriptCoreLib::ScriptCoreLib.Shared.BCLImplementation.System.Linq.__IdentityFunction+<>c__0`1
//script: error JSC1000: Java : Opcode not implemented: brtrue.s at ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Ordere
//internal compiler error at method

//[javac] W:\src\ScriptCoreLib\Extensions\LinqExtensions.java:227: error: incompatible types
//[javac]             if ((enumerator_10))
//[javac]                  ^


//[javac] W:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:248: error: cannot find symbol
//[javac]         return  __TaskExtensions.<TResult>Unwrap_060009d5(__Task.get_InternalFactory().<__Task_1<TResult>>StartNew(function));
//[javac]                                 ^
//[javac]   symbol:   method <TResult>Unwrap_060009d5(__Task_1<__Task_1<TResult>>)
//[javac]   location: class __TaskExtensions
//[javac]   where TResult is a type-variable:
//[javac]     TResult extends Object declared in method <TResult>Run(__Func_1<__Task_1<TResult>>)

// 0001 020001bf ScriptCoreLibAndroid::ScriptCoreLibJava.BCLImplementation.System.Threading.__AutoResetEvent
//internal compiler error at method
// assembly: C:\util\jsc\bin\ScriptCoreLibAndroid.dll at
// type: ScriptCoreLibJava.BCLImplementation.System.Threading.__EventWaitHandle, ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// method: Set
// Object reference not set to an instance of an object.


