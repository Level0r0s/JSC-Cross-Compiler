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

            // ok lets do some code review.
            // whats the least ammount of effort to capture a log line and steam it over udp?
            // it seems to start by
            // X:\opensource\github\Catlog\Catlog\src\com\nolanlawson\logcat\LogcatActivity.java
            // then startUpMainLog
            // LogReaderAsyncTask
            // line = reader.readLine()
            //LogcatReaderLoader loader = LogcatReaderLoader.create(LogcatActivity.this, true);
            //         reader = loader.loadReader();
            // import com.nolanlawson.logcat.reader.LogcatReaderLoader;
            // X:\opensource\github\Catlog\Catlog\src\com\nolanlawson\logcat\reader\LogcatReaderLoader.java
            // SingleLogcatReader

            // X:\opensource\github\Catlog\Catlog\src\com\nolanlawson\logcat\reader\SingleLogcatReader.java
            // 	logcatProcess = LogcatHelper.getLogcatProcess(logBuffer);
            //bufferedReader = new BufferedReader(new InputStreamReader(logcatProcess
            //        .getInputStream()), 8192);
            // did we find the magic?

            // X:\opensource\github\Catlog\Catlog\src\com\nolanlawson\logcat\helper\LogcatHelper.java
            // um
            // "logcat", "-v", "time")

            // so.  logging is like the shell example we used to have?





            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150606/adb
        }


    }


}

//W/SuperUserHelper(15924): Cannot obtain root
//W/SuperUserHelper(15924): java.io.IOException: Error running exec(). Command: [su] Working Directory: null Environment: null
//W/SuperUserHelper(15924):       at java.lang.ProcessManager.exec(ProcessManager.java:211)
//W/SuperUserHelper(15924):       at java.lang.Runtime.exec(Runtime.java:173)
//W/SuperUserHelper(15924):       at java.lang.Runtime.exec(Runtime.java:246)
//W/SuperUserHelper(15924):       at java.lang.Runtime.exec(Runtime.java:189)
//W/SuperUserHelper(15924):       at com.nolanlawson.logcat.helper.SuperUserHelper.requestRoot(SuperUserHelper.java:155)

// rather useless then?