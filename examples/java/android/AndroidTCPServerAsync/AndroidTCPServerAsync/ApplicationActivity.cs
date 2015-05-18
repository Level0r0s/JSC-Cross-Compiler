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

namespace AndroidTCPServerAsync.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150513


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);

            b.WithText("before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );


            this.setContentView(sv);



            var s = new SemaphoreSlim(0);

            //java.lang.Object, rt
            //enter async { ManagedThreadId = 1 }
            //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
            //after delay{ ManagedThreadId = 8 }
            //http://127.0.0.1:8080
            //{ fileName = http://127.0.0.1:8080 }
            //enter catch { mname = <0032> nop.try } ClauseCatchLocal:
            //{ Message = , StackTrace = java.lang.RuntimeException
            //        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpListener.AcceptTcpClientAsync(__TcpListener.java:131)

            new { }.With(
                async delegate
                {
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                    //enter async { ManagedThreadId = 1 }
                    //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
                    //after delay{ ManagedThreadId = 4 }
                    //http://127.0.0.1:8080
                    //awaiting for SemaphoreSlim. done.{ ManagedThreadId = 1 }
                    //--
                    //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 6 }
                    //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
                    //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 8 }
                    //{ ManagedThreadId = 6, input = GET / HTTP/1.1


                    Console.WriteLine("enter async " + new { Thread.CurrentThread.ManagedThreadId });

                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
                    await Task.Delay(100);

                    Console.WriteLine("after delay" + new { Thread.CurrentThread.ManagedThreadId });

                    // Additional information: Only one usage of each socket address (protocol/network address/port) is normally permitted
                    // close the other server!
                    var l = new TcpListener(IPAddress.Any, 8080);

                    l.Start();


                    var href =
                        "http://127.0.0.1:8080";

                    Console.WriteLine(
                        href
                    );



                    this.runOnUiThread(
                        delegate
                        {
                            var i = new Intent(Intent.ACTION_VIEW,
                               android.net.Uri.parse(href)
                           );

                            // http://vaibhavsarode.wordpress.com/2012/05/14/creating-our-own-activity-launcher-chooser-dialog-android-launcher-selection-dialog/
                            var ic = Intent.createChooser(i, href);


                            this.startActivity(ic);
                        }
                    );



                    new { }.With(
                        async delegate
                        {
                            while (true)
                            {
                                var c = await l.AcceptTcpClientAsync();

                                Console.WriteLine("accept " + new { c, Thread.CurrentThread.ManagedThreadId });

                                yield(c);
                            }
                        }
                    );

                    // jump back to main thread..
                    s.Release();
                }
            );
        }


        static async void yield(TcpClient c)
        {
            var s = c.GetStream();

            // could we switch into a worker thread?
            // jsc would need to split the stream object tho

            var buffer = new byte[1024];
            // why no implict buffer?
            var count = await s.ReadAsync(buffer, 0, buffer.Length);

            var input = Encoding.UTF8.GetString(buffer, 0, count);

            //new IHTMLPre { new { input } }.AttachToDocument();
            Console.WriteLine(new { Thread.CurrentThread.ManagedThreadId, input });


            // http://stackoverflow.com/questions/369498/how-to-prevent-iframe-from-redirecting-top-level-window
            var outputString = @"HTTP/1.0 200 OK 
Content-Type:	text/html; charset=utf-8
Connection: close

<body><h1 style='color: red;'>Hello world</h2><h3>jsc</h3>
hello world. jvm clr android async tcp? udp?<iframe  sandbox='allow-forms' src='http://www.whatsmyip.us/'><iframe>
</body>
";
            var obuffer = Encoding.UTF8.GetBytes(outputString);

            await s.WriteAsync(obuffer, 0, obuffer.Length);

            c.Close();
        }
    }


}

// <activity android:name="ApplicationActivity" android:label="@string/app_name" android:launchMode="singleInstance" android:configChanges="orientation|screenSize" android:theme="@android:style/Theme.Holo.Dialog">
//  [aapt] P:\bin\AndroidManifest.xml:14: error: Error: No resource found that matches the given name (at 'label' with value '@string/app_name').


//[javac]
//P:\src\AndroidTCPServerAsync\Activities\ApplicationActivity___c__DisplayClass0_0___onCreate_b__1_d__MoveNext_06000025.java:53: error: cannot find symbol
//[javac] ApplicationActivity___c__DisplayClass0_0___onCreate_b__1_d__MoveNext_06000025.__workflow(next_060000250, ref_awaiter1, ref_create_b__1_d2);
//[javac]                                                                                                                                ^
//  [javac]
//symbol:   variable ref_create_b__1_d2
//[javac]   location: class ApplicationActivity___c__DisplayClass0_0___onCreate_b__1_d__MoveNext_06000025
//  [javac]
//P:\src\AndroidTCPServerAsync\Activities\ApplicationActivity___c__DisplayClass0_1___onCreate_b__3_d__MoveNext_06000028.java:47: error: cannot find symbol
//[javac] ApplicationActivity___c__DisplayClass0_1___onCreate_b__3_d__MoveNext_06000028.__workflow(next_060000280, ref_awaiter_11, ref_create_b__3_d2);
//[javac]                                                                                                                                  ^
//  [javac]
//symbol:   variable ref_create_b__3_d2
//[javac]   location: class ApplicationActivity___c__DisplayClass0_1___onCreate_b__3_d__MoveNext_06000028
//  [javac]
//P:\src\AndroidTCPServerAsync\Activities\ApplicationActivity__yield_d__1__MoveNext_06000022.java:51: error: cannot find symbol
//[javac] ApplicationActivity__yield_d__1__MoveNext_06000022.__workflow(next_060000220, ref_awaiter_11, ref__yield_d__12, ref_awaiter3);
//[javac]                                                                                                       ^
//  [javac]
//symbol:   variable ref__yield_d__12
//[javac]   location: class ApplicationActivity__yield_d__1__MoveNext_06000022
//  [javac]
//P:\src\__AnonymousTypes__AndroidTCPServerAsync_AndroidActivity\__f__AnonymousType_106_1_1.java:34: error: reference to Format is ambiguous, both method Format(String, Object, Object) in __String and method Format(__IFormatProvider, String, Object[]) in __String match
//[javac]         return __String.Format(null, "{{ ManagedThreadId = {0} }}", objectArray2);
//  [javac]                        ^
//  [javac]
//P:\src\__AnonymousTypes__AndroidTCPServerAsync_AndroidActivity\__f__AnonymousType_74_2_2.java:41: error: reference to Format is ambiguous, both method Format(String, Object, Object) in __String and method Format(__IFormatProvider, String, Object[]) in __String match
//[javac]         return __String.Format(null, "{{ c = {0}, ManagedThreadId = {1} }}", objectArray4);
//  [javac]                        ^
//  [javac]
//P:\src\__AnonymousTypes__AndroidTCPServerAsync_AndroidActivity\__f__AnonymousType_95_3_2.java:44: error: reference to Format is ambiguous, both method Format(String, Object, Object) in __String and method Format(__IFormatProvider, String, Object[]) in __String match
//[javac]         return __String.Format(null, "{{ ManagedThreadId = {0}, input = {1} }}", objectArray4);
//  [javac]                        ^
//  [javac]
//Note: P:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.
//  [javac] Note: Recompile with -Xlint:deprecation for details.
//  [javac] Note: Some input files use unchecked or unsafe operations.
//  [javac] Note: Recompile with -Xlint:unchecked for details.
//  [javac] 6 errors