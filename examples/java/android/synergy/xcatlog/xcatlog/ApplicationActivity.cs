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

namespace xcatlog.Activities
{
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150606

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : com.nolanlawson.logcat.LogcatActivity // : ListActivity
    {
        // http://developer.android.com/reference/android/app/Activity.html#onCreate(android.os.Bundle)
        // protected void onCreate (Bundle savedInstanceState)
        //     protected virtual void onCreate(Bundle savedInstanceState);

        // http://developer.android.com/reference/android/app/ListActivity.html
        // public void onCreate(Bundle savedInstanceState) {
        //1>X:\jsc.svn\examples\java\android\synergy\xcatlog\xcatlog\ApplicationActivity.cs(21,33,21,41): error CS0507: 'xcatlog.Activities.ApplicationActivity.onCreate(android.os.Bundle)': cannot change access modifiers when overriding 'public' inherited member 'com.nolanlawson.logcat.LogcatActivity.onCreate(android.os.Bundle)'
        // 
        public override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

        }


    }


}
