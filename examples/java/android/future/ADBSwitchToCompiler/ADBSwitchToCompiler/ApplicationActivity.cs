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

namespace ADBSwitchToCompiler.Activities
{
    public struct SwitchToCompilerAwaiter : System.Runtime.CompilerServices.INotifyCompletion
    {
        // Severity	Code	Description	Project	File	Line
        //Error CS4027	'SwitchToCompilerAwaiter' does not implement 'INotifyCompletion'	ADBSwitchToCompiler X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs	110


        // Severity	Code	Description	Project	File	Line
        //Error CS0117	'SwitchToCompilerAwaiter' does not contain a definition for 'IsCompleted'	ADBSwitchToCompiler X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs	103

        public bool IsCompleted
        {
            get { return false; }
        }

        public void OnCompleted(Action continuation)
        {
        }

        // Severity	Code	Description	Project	File	Line
        //Error CS0117	'SwitchToCompilerAwaiter' does not contain a definition for 'GetResult'	ADBSwitchToCompiler X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs	118

        public void GetResult() { }
    }

    //public static class SwitchToCompiler
    public struct SwitchToCompiler
    {
        // Severity	Code	Description	Project	File	Line
        //Error CS1061	'SwitchToCompiler' does not contain a definition for 'GetAwaiter' 
        // and no extension method 'GetAwaiter' accepting a first argument of type 'SwitchToCompiler' could be found(are you missing a using directive or an assembly reference?)	ADBSwitchToCompiler X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs	85

        // Severity	Code	Description	Project	File	Line
        //Error CS0117	'object' does not contain a definition for 'IsCompleted'	ADBSwitchToCompiler X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs	92


        public SwitchToCompilerAwaiter GetAwaiter()
        {

            // copy out
            return default(SwitchToCompilerAwaiter);
        }

        // implemented by the compiler?
        public static void Invoke(Action yield)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150717/adbswitchtocompiler

            // ?
        }
    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "21")]

    // http://swagos.blogspot.com/2012/12/various-themes-available-in-android_28.html
    // Theme.Holo.Dialog.Alert
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Light.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Light")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.DeviceDefault.Light.Dialog")]

    // works for 2.4 too
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    public class ApplicationActivity : Activity
    {
        // http://stackoverflow.com/questions/17513502/support-for-multi-window-app-development

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this);

            b.setText("Vibrate!");

            b.AtClick(
                async delegate
                {
                    var vibrator = (Vibrator)this.getSystemService(Context.VIBRATOR_SERVICE);

                    vibrator.vibrate(600);

                    // or jump to laptop pointerlock?

                    SwitchToCompiler.Invoke(
                        delegate
                        {
                            // did we jump back to compiler?
                            // UDP?
                            // RSA?


                        }
                    );

                    // or

                    //        [javac] W:\src\ADBSwitchToCompiler\Activities\ApplicationActivity___onCreate_b__0_0_d__MoveNext_0600000f.java:48: error: cannot find symbol
                    //[javac]         ApplicationActivity___onCreate_b__0_0_d__MoveNext_0600000f.__workflow(next_0600000f0, ref_awaiter1, ref_compiler2, ref_create_b__0_0_d3);
                    //[javac]                                                                                                                            ^

                    // roslyn wont like it?
                    await default(SwitchToCompiler);
                    // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionHopToTab\ChromeExtensionHopToTab\Application.cs

                    // did we jump back to compiler?
                    // UDP?
                    // RSA?


                    Console.WriteLine("record screen");

                    Console.WriteLine("pull");

                    // https://developers.google.com/youtube/v3/code_samples/dotnet

                }
            );

            ll.addView(b);



            this.setContentView(sv);


            //this.ShowLongToast("http://my.jsc-solutions.net x");
        }


    }
}
