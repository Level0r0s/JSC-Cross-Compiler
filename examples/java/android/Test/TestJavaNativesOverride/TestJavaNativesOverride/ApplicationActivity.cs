extern alias assets;
// Error	1	The extern alias 'assets' was not specified in a /reference option	X:\jsc.svn\examples\java\android\test\TestJavaNativesOverride\TestJavaNativesOverride\ApplicationActivity.cs	1	1	TestJavaNativesOverride


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
using ScriptCoreLib.Extensions;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using android.content;

namespace foo
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201506/20150602
    public class Bar
    {
        public string field1 = "CLR ctor field1";
        public string field2 = "f2";

        public Bar()
        {
            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Activator.cs
            Console.WriteLine("Bar ctor implemented");
        }

        [Obsolete("if we do not reimplement it, we wont have the implementation, as we are not decompiling java bytecode here!")]
        public string GetBarString()
        {
            return new { field1, field2 }.ToString();
        }
    }
}

namespace TestJavaNativesOverride.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150513

        // Warning	1	The type 'foo.Bar' in 'x:\jsc.svn\examples\java\android\Test\TestJavaNativesOverride\TestJavaNativesOverride\ApplicationActivity.cs' conflicts with the imported type 'foo.Bar' in 'x:\jsc.svn\examples\java\android\Test\TestJavaNativesOverride\TestJavaNativesOverride\bin\staging.AssetsLibrary\TestJavaNativesOverride.AssetsLibrary.dll'. Using the type defined in 'x:\jsc.svn\examples\java\android\Test\TestJavaNativesOverride\TestJavaNativesOverride\ApplicationActivity.cs'.	X:\jsc.svn\examples\java\android\test\TestJavaNativesOverride\TestJavaNativesOverride\ApplicationActivity.cs	39	13	TestJavaNativesOverride

        // some code keep refin the old version
        assets::foo.Bar bar = new assets::foo.Bar { field1 = "!" };

        // new code link into the new code..
        // while merge, jsc would now strip the script attribute?
        global::foo.Bar bar2 = new global::foo.Bar { field2 = "!!" };

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);

            var goo = new foo.Goo();

            //Activator.CreateInstance<

            //var goo = (foo.Goo)Activator.CreateInstance(
            //    typeof(foo.Goo)
            //);

            //E/AndroidRuntime(32112): Caused by: java.lang.RuntimeException
            //E/AndroidRuntime(32112):        at foo.Bar.GetBarString(Bar.java:29)
            //E/AndroidRuntime(32112):        at foo.Goo.GetString(Goo.java:9)
            //E/AndroidRuntime(32112):        at TestJavaNativesOverride.Activities.ApplicationActivity.onCreate(ApplicationActivity.java:69)

            b.WithText(goo.GetString());
            b.AtClick(
                v =>
                {
                    b.setText(
                         new assets::foo.Bar().GetBarString()
                    );
                }
            );


            this.setContentView(sv);



        }


    }


}

//[javac] W:\src\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\__TaskExtensions.java:34: error: cannot find symbol
//[javac]         task.ContinueWith_06000318(new __Action_1<__Task_1<__Task_1<TResult>>>(class2_10,
//[javac]             ^

//E/AndroidRuntime(31989): java.lang.RuntimeException: Unable to instantiate activity ComponentInfo{TestJavaNativesOverride.Activities/TestJavaNativesOverride.Activities.ApplicationActivity}: java.lang.RuntimeException
//E/AndroidRuntime(31989):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2665)
//E/AndroidRuntime(31989):        at android.app.ActivityThread.handleLaunchActivity(ActivityThread.java:2879)
//E/AndroidRuntime(31989):        at android.app.ActivityThread.access$900(ActivityThread.java:182)
//E/AndroidRuntime(31989):        at android.app.ActivityThread$H.handleMessage(ActivityThread.java:1475)
//E/AndroidRuntime(31989):        at android.os.Handler.dispatchMessage(Handler.java:102)
//E/AndroidRuntime(31989):        at android.os.Looper.loop(Looper.java:145)
//E/AndroidRuntime(31989):        at android.app.ActivityThread.main(ActivityThread.java:6141)
//E/AndroidRuntime(31989):        at java.lang.reflect.Method.invoke(Native Method)
//E/AndroidRuntime(31989):        at java.lang.reflect.Method.invoke(Method.java:372)
//E/AndroidRuntime(31989):        at com.android.internal.os.ZygoteInit$MethodAndArgsCaller.run(ZygoteInit.java:1399)
//E/AndroidRuntime(31989):        at com.android.internal.os.ZygoteInit.main(ZygoteInit.java:1194)
//E/AndroidRuntime(31989): Caused by: java.lang.RuntimeException
//E/AndroidRuntime(31989):        at foo.Bar.<init>(Bar.java:19)
//E/AndroidRuntime(31989):        at TestJavaNativesOverride.Activities.ApplicationActivity.<init>(ApplicationActivity.java:38)
//E/AndroidRuntime(31989):        at java.lang.reflect.Constructor.newInstance(Native Method)
//E/AndroidRuntime(31989):        at java.lang.Class.newInstance(Class.java:1650)
//E/AndroidRuntime(31989):        at android.app.Instrumentation.newActivity(Instrumentation.java:1079)
//E/AndroidRuntime(31989):        at android.app.ActivityThread.performLaunchActivity(ActivityThread.java:2655)
//E/AndroidRuntime(31989):        ... 10 more