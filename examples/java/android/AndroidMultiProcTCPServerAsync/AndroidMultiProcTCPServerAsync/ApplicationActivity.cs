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
using ScriptCoreLibJava.Extensions;
using ScriptCoreLibJava.BCLImplementation.System.Net.Sockets;
using System.IO;
using System.Diagnostics;


namespace AndroidMultiProcTCPServerAsync.Activities
{
    // http://developer.android.com/reference/android/os/ParcelFileDescriptor.html#dup(java.io.FileDescriptor)
    // JNI implementation frameworks \ base \ core \ jni \ android_os_MemoryFile.cpp
    // http://www.phonesdevelopers.com/1785262/
    // http://www.abhisheksur.com/2012/02/inter-process-communication-using.html
    // system/core/include/cutils/ashmem.h

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    public class LocalApplication :
        // can we change our base depeinding on the process we are in?
        Application
    {
        public override void onCreate()
        {
            base.onCreate();

            // yes we are loaded for both processes.
            {
                var myPid = android.os.Process.myPid();

                Console.WriteLine("enter LocalApplication onCreate, first time? " + new { myPid });
                // am i the service or the ui?




                var m = (ActivityManager)this.getSystemService(Context.ACTIVITY_SERVICE);
                // why limit?
                var s = m.getRunningServices(0xffff);

                var a = Enumerable.FirstOrDefault(
                      from i in Enumerable.Range(0, s.size())
                      let rsi = (android.app.ActivityManager.RunningServiceInfo)s.get(i)
                      let cn = rsi.service.getClassName()
                      where cn == typeof(GatewayService).FullName
                      select new { i, rsi, cn, rsi.pid }
                );

                if (a == null)
                {
                    // service not running

                    // https://stackoverflow.com/questions/7686482/when-does-applications-oncreate-method-is-called-on-android
                    Toast.makeText(this, "ui? AndroidMultiProcTCPServerAsync " + new { myPid }, Toast.LENGTH_LONG).show();
                }
                else
                {
                    if (a.pid == myPid)
                    {
                        // looking into mirror...
                        Toast.makeText(this, "service AndroidMultiProcTCPServerAsync " + new { myPid }, Toast.LENGTH_LONG).show();
                    }
                    else
                    {
                        Toast.makeText(this, "ui! AndroidMultiProcTCPServerAsync " + new { myPid }, Toast.LENGTH_LONG).show();
                    }
                }
            }
        }
    }



    // android:excludeFromRecents="true" ...


    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]

    // ? http://stackoverflow.com/questions/13385289/remove-app-from-recent-apps-programmatically

    // excludeFromRecents prevents bg service being restarted
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:excludeFromRecents", value = "true")]
    public class ApplicationActivity : Activity
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555

        //  x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG"

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150513
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150531

        // 01. lets have our service think of a port, and send it back to activity at start. or save into file as cpu does it?
        // http://developer.android.com/reference/android/os/MemoryFile.html
        // http://stackoverflow.com/questions/19778196/class-memoryfile-of-any-use
        // http://stackoverflow.com/questions/15123402/share-memory-between-two-processes-in-dalvik
        // http://www.slideshare.net/tetsu.koba/interprocess-communication-of-android
        // http://www.codota.com/android/scenarios/52fcbca7da0a12229fc989b1/android.os.MemoryFile?tag=dragonfly
        // https://groups.google.com/forum/#!topic/android-developers/r-oqeI7MlJg
        // http://stackoverflow.com/questions/19778196/class-memoryfile-of-any-use
        // https://vec.io/posts/andriod-ipc-shared-memory-with-ashmem-memoryfile-and-binder
        // http://notjustburritos.tumblr.com/post/21442138796/an-introduction-to-android-shared-memory
        // https://developer.android.com/training/articles/memory.html
        // http://www.slideshare.net/jserv/android-ipc-mechanism

        //Action AtActivityResult;

        //protected override void onActivityResult(int arg0, int arg1, Intent arg2)
        //{
        //    base.onActivityResult(arg0, arg1, arg2);

        //    if (AtActivityResult != null)
        //        AtActivityResult();
        //}




        //        I/ActivityManager(  475): Killing 7649:AndroidMultiProcTCPServerAsync.Activities/u0a49 (adj 9): remove task
        //I/ActivityManager(  475): Killing 7627:AndroidMultiProcTCPServerAsync.Activities:gateway7/u0a49 (adj 5): remove task
        //I/ActivityManager(  475): START u0 {act=android.intent.action.MAIN cat=[android.intent.category.HOME] flg=0x10200000 cmp=com.android.l
        //W/ActivityManager(  475): Scheduling restart of crashed service AndroidMultiProcTCPServerAsync.Activities/.GatewayService in 16000ms

        //protected override void onPause()
        //{
        //    Console.WriteLine("enter onPause, finishAndRemoveTask");


        //    // not called on back button?
        //    // keep service running, remove activity
        //    this.finishAndRemoveTask();

        //    base.onPause();



        //}

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            // fill the button
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            this.setContentView(sv);

            var activity = this;


            new Button(activity).WithText("is the service already running?").AttachTo(ll);

            var m = (ActivityManager)this.getSystemService(Context.ACTIVITY_SERVICE);
            // why limit?
            var s = m.getRunningServices(0xffff);

            var a = Enumerable.FirstOrDefault(
                from i in Enumerable.Range(0, s.size())
                let rsi = (android.app.ActivityManager.RunningServiceInfo)s.get(i)
                let cn = rsi.service.getClassName()
                where cn == typeof(GatewayService).FullName
                select new { i, rsi, cn }
            );

            if (a == null)
            {
                new Button(activity).WithText("start service").AttachTo(ll).AtClick(
                    delegate
                    {
                        // start the service unless already running?
                        var intent = new Intent(this.getApplicationContext(), typeof(GatewayService).ToClass());
                        // if the user can swipe us away service is restarted?
                        this.getApplicationContext().startService(intent);

                    }
                );
            }
            else
            {

            }


            // whats the port?

            new Button(activity).WithText("which port are we on? " + new { a }).AttachTo(ll).AtClick(
                delegate
                {
                    var aa = default(AsyncReplyReceiver);

                    aa = new AsyncReplyReceiver
                    {
                        AtReceive = (cc, ii) =>
                        {
                            Console.WriteLine("ui now has the port...");
                            this.unregisterReceiver(aa);


                            var host = ii.getStringExtra("host");
                            var port = ii.getIntExtra("port", 0);

                            new Button(activity).WithText("open " + host + ":" + port).AttachTo(ll).AtClick(
                                 delegate
                                 {

                                     var href =
                                         "http://" + host + ":" + port;

                                     Console.WriteLine(
                                         href
                                     );



                                     //this.runOnUiThread(
                                     //    delegate
                                     //    {
                                     var i = new Intent(Intent.ACTION_VIEW,
                                        android.net.Uri.parse(href)
                                    );

                                     // http://vaibhavsarode.wordpress.com/2012/05/14/creating-our-own-activity-launcher-chooser-dialog-android-launcher-selection-dialog/
                                     var ic = Intent.createChooser(i, href);


                                     this.startActivity(ic);
                                     //    }
                                     //);
                                 }
                            );
                        }
                    };

                    var intentFilter = new IntentFilter();
                    intentFilter.addAction(GatewayService.ACTION + "reply");
                    this.registerReceiver(aa, intentFilter);


                    var intent = new Intent();
                    intent.setAction(GatewayService.ACTION);
                    intent.putExtra("whats my port", "?");
                    this.sendBroadcast(intent);
                }
            );



            new Button(this).WithText("exit").AttachTo(ll).AtClick(
             delegate
             {

                 // will it be logged?
                 System.Environment.Exit(13);

                 // application still visible in tasks?
             }
         );

            new Button(this).WithText("finish").AttachTo(ll).AtClick(
               delegate
               {
                   //this.finishAndRemoveTask();
                   this.finish();

                   // will it be logged?
                   //System.Environment.Exit(13);

                   // application still visible in tasks?
               }
           );

            new Button(this).WithText("finishAndRemoveTask").AttachTo(ll).AtClick(
             delegate
             {
                 this.finishAndRemoveTask();
                 //this.finish();

                 // will it be logged?
                 //System.Environment.Exit(13);

                 // application still visible in tasks?
             }
         );


            //            [javac] W:\src\AndroidMultiProcTCPServerAsync\Activities\ApplicationActivity.java:56: error: unreported exception IOException; must be caught or declared to be thrown
            //[javac]         class22.m = new MemoryFile("foo0", 100);

            //// ipc memory referenced
            //var m = default(MemoryFile);

            //try
            //{
            //    m = new MemoryFile("foo0", 100);
            //}
            //catch { throw; }


            // MemoryFile not available yet. bypass to NDK? workaround to filesystem?

            // E/AndroidRuntime(28716): Caused by: android.system.ErrnoException: open failed: EROFS (Read-only file system)

            // http://developer.android.com/training/basics/data-storage/files.html

            //  File.WriteAllText(this.getFilesDir().getAbsolutePath() + "/MemoryFile-foo0", "awaiting...");



            //  new Button(activity).WithText("Next " +

            //      File.ReadAllText(this.getFilesDir().getAbsolutePath() + "/MemoryFile-foo0")

            //      ).AttachTo(ll).AtClick(
            //      button =>
            //      {
            //          Intent intent = new Intent(activity, typeof(SecondaryActivity).ToClass());
            //          intent.setFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);

            //          // share scope
            //          var myPid = android.os.Process.myPid();
            //          intent.putExtra("_item", "hello from " + new { myPid });

            //          //intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);

            //          //new Button(activity).WithText("read").AttachTo(ll).AtClick(
            //          //      button2 =>
            //          //      {
            //          //          var m2 = default(MemoryFile);

            //          //          try
            //          //          {
            //          //              m2 = new MemoryFile("foo0", 100);
            //          //          }
            //          //          catch { throw; }


            //          //          var pos = 0;

            //          //          //var __out = new __NetworkStream { InternalOutputStream = m.getOutputStream() };
            //          //          var __in = new __NetworkStream { InternalInputStream = m2.getInputStream() };

            //          //          // block the ui?

            //          //          var ipcByte = 0;
            //          //          while (ipcByte == 0)
            //          //          {
            //          //              ipcByte = __in.ReadByte();
            //          //              button2.WithText(new { ipcByte, pos }.ToString());
            //          //              pos++;
            //          //          }
            //          //      }
            //          //  );

            //          AtActivityResult +=
            //              delegate
            //              {
            //                  // if we read too early we get all zeros..

            //                  //var pos = 0;

            //                  ////var __out = new __NetworkStream { InternalOutputStream = m.getOutputStream() };
            //                  //var __in = new __NetworkStream { InternalInputStream = m.getInputStream() };

            //                  //// block the ui?

            //                  //var ipcByte = 0;
            //                  //while (ipcByte == 0)
            //                  //{
            //                  //    ipcByte = __in.ReadByte();
            //                  //    button.WithText(new { ipcByte, pos }.ToString());
            //                  //    pos++;
            //                  //}

            //                  button.WithText(
            //                   File.ReadAllText(this.getFilesDir().getAbsolutePath() + "/MemoryFile-foo0")
            //                 );

            //              };

            //          // cached backgroun process?
            //          // switching to another process.. easy...
            //          //activity.startActivity(intent);
            //          activity.startActivityForResult(intent, requestCode: 0x14);

            //      }
            //);

            //  //var s = new SemaphoreSlim(0);

            //  ////java.lang.Object, rt
            //  ////enter async { ManagedThreadId = 1 }
            //  ////awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
            //  ////after delay{ ManagedThreadId = 8 }
            //  ////http://127.0.0.1:8080
            //  ////{ fileName = http://127.0.0.1:8080 }
            //  ////enter catch { mname = <0032> nop.try } ClauseCatchLocal:
            //  ////{ Message = , StackTrace = java.lang.RuntimeException
            //  ////        at ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__TcpListener.AcceptTcpClientAsync(__TcpListener.java:131)

            //  //new { }.With(
            //  //    async delegate
            //  //    {
            //  //        //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //  //        //enter async { ManagedThreadId = 1 }
            //  //        //awaiting for SemaphoreSlim{ ManagedThreadId = 1 }
            //  //        //after delay{ ManagedThreadId = 4 }
            //  //        //http://127.0.0.1:8080
            //  //        //awaiting for SemaphoreSlim. done.{ ManagedThreadId = 1 }
            //  //        //--
            //  //        //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 6 }
            //  //        //System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
            //  //        //accept { c = System.Net.Sockets.TcpClient, ManagedThreadId = 8 }
            //  //        //{ ManagedThreadId = 6, input = GET / HTTP/1.1





            //  //        // jump back to main thread..
            //  //        s.Release();
            //  //    }
            //  //);
        }

    }


    public class AsyncReplyReceiver : BroadcastReceiver
    {
        // http://stackoverflow.com/questions/4805269/programmatically-register-a-broadcast-receiver

        public Action<Context, Intent> AtReceive;

        public override void onReceive(Context c, Intent i)
        {
            AtReceive(c, i);
        }
    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":SecondaryActivity")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Light.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent.NoTitleBar")]
    //public class SecondaryActivity : ScriptCoreLib.Android.CoreAndroidWebServiceActivity
    public class SecondaryActivity : Activity
    {
        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            // fill the button
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


            this.setContentView(sv);

            var activity = this;

            //var m = default(MemoryFile);
            //var fd = default(java.io.FileDescriptor);

            //try
            //{
            //    //[javac] W:\src\AndroidMultiProcTCPServerAsync\Activities\SecondaryActivity.java:70: error: cannot find symbol
            //    //[javac]             file3.getFileDescriptor().sync();
            //    //[javac]                  ^
            //    //[javac]   symbol:   method getFileDescriptor()
            //    //[javac]   location: variable file3 of type MemoryFile

            //    m = new MemoryFile("foo0", 100);

            //    var buffer = new byte[] { 0x44 };
            //    m.writeBytes(
            //        buffer: buffer,
            //        srcOffset: 0,
            //        destOffset: 0,
            //        count: 1
            //    );

            //    fd = m.getFileDescriptor();

            //    // http://gotoanswer.com/?q=what+is+the+use+of+MemoryFile+in+android

            //}
            //catch { throw; }


            //var __out = new __NetworkStream { InternalOutputStream = m.getOutputStream() };

            //__out.WriteByte(0x13);
            //__out.WriteByte(0x14);



            //new Button(activity).WithText("read "

            //    + File.ReadAllText("MemoryFile-foo0")

            //    //+ new { fd }

            //    ).AttachTo(ll).AtClick(
            //       button2 =>
            //       {
            //           var m2 = default(MemoryFile);

            //           var buffer = new byte[100];
            //           var read = -3;

            //           try
            //           {
            //               //m2 = m;
            //               //m2 = new MemoryFile("foo0", 100);
            //               m2 = new MemoryFile(fd, 100, "r");

            //               read = m2.readBytes(buffer, 0, 0, 3);

            //           }
            //           catch { throw; }


            //           var pos = 0;

            //           ////var __out = new __NetworkStream { InternalOutputStream = m.getOutputStream() };
            //           //var __in = new __NetworkStream { InternalInputStream = m2.getInputStream() };

            //           //// block the ui?

            //           //var ipcByte = 0;
            //           //while (ipcByte == 0)
            //           //{
            //           //    ipcByte = __in.ReadByte();
            //           button2.WithText(new { read, byte0 = buffer[0] }.ToString());
            //           //    pos++;
            //           //}
            //       }
            //   );

            new Button(activity).WithText("done"

                + File.ReadAllText(this.getFilesDir().getAbsolutePath() + "/MemoryFile-foo0")

                ).AttachTo(ll).AtClick(
              delegate
              {
                  File.WriteAllText(this.getFilesDir().getAbsolutePath() + "/MemoryFile-foo0", "ready!");


                  this.finishAndRemoveTask();
              }
          );
        }
    }

    // pack it up into nuget?
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":gateway7")]
    public sealed class GatewayService : Service
    {
        // http://stackoverflow.com/questions/26842675/continue-service-even-if-application-is-cleared-from-recent-app
        // http://stackoverflow.com/questions/23716832/prevent-killing-of-service-when-clear-recent-tasks
        // https://groups.google.com/forum/#!topic/android-developers/H-DSQ4-tiac
        // http://www.androidguys.com/2009/09/09/diamonds-are-forever-services-are-not/
        // http://devescape.blogspot.com/2011/02/persistent-services-in-android.html
        // nexus7 will kill the service once swiped away

        public const string ACTION = "NotifyServiceAction";

        public const int RQS_STOP_SERVICE = 1;

        public override void onCreate()
        {
            base.onCreate();

            Console.WriteLine("enter onCreate ");

        }



        //        [javac] W:\src\_PrivateImplementationDetails___02000012__06000059__System\IDisposable\Dispose_0000__ldarg_0__lookup.java:9: error: cannot find symbol
        //[javac]     public static _ArrayType_12 data = new _ArrayType_12();
        //[javac]                   ^

        static IEnumerable<System.Net.NetworkInformation.NetworkInterface> AllNetworkInterfaces
        {
            get
            {
                Console.WriteLine("609 enter AllNetworkInterfaces ");
                var stale = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                Console.WriteLine("611 AllNetworkInterfaces " + new { stale });

                foreach (var item in stale)
                {
                    yield return item;
                }

                Console.WriteLine("618 AllNetworkInterfaces yield break");
                yield break;
            }
        }

        // gateway service process/ event thread / async enabled?
        public override int onStartCommand(Intent value0, int flags, int startId)
        {
            // Options that have been set in the service declaration in the manifest.
            // http://developer.android.com/reference/android/content/pm/ServiceInfo.html#FLAG_STOP_WITH_TASK

            Console.WriteLine("enter onStartCommand " + new { flags, startId });



            // until wifi changes?
            var xipv4 =
                //from n in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                from n in AllNetworkInterfaces
                let IPProperties = n.GetIPProperties()
                let c = IPProperties.UnicastAddresses.Count
                from i in Enumerable.Range(0, c)
                let ip = IPProperties.UnicastAddresses[i]


                where ip.Address.AddressFamily == AddressFamily.InterNetwork
                //let loop = IPAddress.Loopback == ip.Address
                let loop = IPAddress.IsLoopback(ip.Address)

                orderby loop
                select new { n, ip, loop, ip.Address };

            //I/System.Console(20546): 5042:0001 { n = ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation.__NetworkInterface@37e1894a, ip = { Address = 192.168.1.126 }, loop = false, Address = 192.168.1.126 }
            //I/System.Console(20546): 5042:0001 { n = ScriptCoreLibJava.BCLImplementation.System.Net.NetworkInformation.__NetworkInterface@3c1830bb, ip = { Address = 127.0.0.1 }, loop = true, Address = 127.0.0.1 }

            xipv4.WithEach(
                Console.WriteLine
            );

            var BestGuessHost = xipv4.FirstOrDefault();
            Console.WriteLine(" " + new { BestGuessHost });


            //var port = new Random().Next(8000, 30000);

            // cached by cloudflare?
            //var port = 80;
            var port = 8080;
            // https://github.com/NanoHttpd/nanohttpd/blob/master/core/src/main/java/fi/iki/elonen/NanoHTTPD.java

            var notifyServiceReceiver = new AsyncReplyReceiver
            {
                AtReceive = (cc, ii) =>
                {
                    Console.WriteLine("enter onReceive");

                    //android.content.IntentFilter
                    //android.content.Intent.ACTION_BOOT_COMPLETED
                    int rqs = ii.getIntExtra("RQS", 0);

                    if (rqs == RQS_STOP_SERVICE)
                        this.stopSelf();

                    if (ii.hasExtra("whats my port"))
                    {
                        // how do we reply?
                        // sharedmemory implementation is missing and useless

                        xipv4.WithEach(
                            Console.WriteLine
                        );

                        var intent = new Intent();
                        //intent.putExtra("host", BestGuessHost.Address.ToString());

                        // did we switch networks?
                        intent.putExtra("host", xipv4.FirstOrDefault().ToString());
                        intent.putExtra("port", port);
                        intent.setAction(GatewayService.ACTION + "reply");
                        this.sendBroadcast(intent);
                    }
                }
            };



            #region AtDestroy
            this.AtDestroy = delegate
            {
                Console.WriteLine("enter AtDestroy");

                this.unregisterReceiver(notifyServiceReceiver);
                //            I/System.Console( 8080): onDestroy { xmyPid = 8080 }
                //I/art     ( 8080): System.exit called, status: 42

                var xmyPid = android.os.Process.myPid();
                Console.WriteLine("onDestroy " + new { xmyPid });

                System.Environment.Exit(42);
            };
            #endregion


            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = ip6tnl0, Description = ip6tnl0, SupportsMulticast = false, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet4, Description = rmnet4, SupportsMulticast = true, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet2, Description = rmnet2, SupportsMulticast = true, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet3, Description = rmnet3, SupportsMulticast = true, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet7, Description = rmnet7, SupportsMulticast = true, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet5, Description = rmnet5, SupportsMulticast = true, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet6, Description = rmnet6, SupportsMulticast = true, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet1, Description = rmnet1, SupportsMulticast = true, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = rmnet0, Description = rmnet0, SupportsMulticast = true, InetAddressesString =  }

            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 1, Name = lo, Description = lo, SupportsMulticast = false, InetAddressesString = , ::1%1, 127.0.0.1 }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 2, Name = sit0, Description = sit0, SupportsMulticast = false, InetAddressesString =  }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 1, Name = p2p0, Description = p2p0, SupportsMulticast = true, InetAddressesString = , fe80::e850:8bff:fe7d:277c%p2p0 }
            //I/System.Console( 9390): 24ae:0001 { OperationalStatus = 1, Name = wlan0, Description = wlan0, SupportsMulticast = true, InetAddressesString = , 2001:7d0:8414:3001:ea50:8bff:fe7d:277c%13, fe80::ea50:8bff:fe7d:277c%wlan0, 2001:7d0:8414:3001:b421:4790:ede8:826c%13, 192.168.1.126 }



            //I/System.Console(15259): 3b9b:0001 { OperationalStatus = 1, Name = lo, Description = lo, SupportsMulticast = false, InetAddressesString = , ::1%1, 127.0.0.1 }
            //I/System.Console(15259): 3b9b:0001 { OperationalStatus = 2, Name = dummy0, Description = dummy0, SupportsMulticast = false, InetAddressesString =  }
            //I/System.Console(15259): 3b9b:0001 { OperationalStatus = 2, Name = sit0, Description = sit0, SupportsMulticast = false, InetAddressesString =  }
            //I/System.Console(15259): 3b9b:0001 { OperationalStatus = 2, Name = ip6tnl0, Description = ip6tnl0, SupportsMulticast = false, InetAddressesString =  }
            //I/System.Console(15259): 3b9b:0001 { OperationalStatus = 1, Name = p2p0, Description = p2p0, SupportsMulticast = true, InetAddressesString = , fe80::10bf:48ff:febe:6b7d%p2p0 }
            //I/System.Console(15259): 3b9b:0001 { OperationalStatus = 1, Name = wlan0, Description = wlan0, SupportsMulticast = true, InetAddressesString = , 2001:7d0:8414:3001:a06f:6dfe:8dfc:42b8%6, 2001:7d0:8414:3001:12bf:48ff:febe:6b7d%6, fe80::12bf:48ff:febe:6b7d%wlan0, 192.168.1.211 }

            // I/System.Console(11408): 2c90:0001 { OperationalStatus = 1, Name = rmnet0, Description = rmnet0, SupportsMulticast = true, InetAddressesString = , 83.187.193.24 }

            //Implementation not found for type import :
            //type: System.Net.NetworkInformation.UnicastIPAddressInformationCollection
            //method: System.Net.NetworkInformation.UnicastIPAddressInformation get_Item(Int32)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!







            // http://stackoverflow.com/questions/14182014/android-oncreate-or-onstartcommand-for-starting-service



            var myPid = android.os.Process.myPid();

            var intentFilter = new IntentFilter();
            intentFilter.addAction(ACTION);
            registerReceiver(notifyServiceReceiver, intentFilter);


            Func<TcpListener> ctor = delegate
            {
                TcpListener x = null;
                try
                {
                    Console.WriteLine("774 TcpListener " + new { port });
                    x = new TcpListener(IPAddress.Any, port);
                    Console.WriteLine("776 TcpListener ");

                    // signal UI service is yet again available
                    //Console.WriteLine("before Start ");

                    // I/System.Console(25817): 64d9:0001 { err = java.lang.RuntimeException: bind failed: EACCES (Permission denied) }

                    x.Start();

                    Console.WriteLine("782 TcpListener ");

                }
                catch (Exception err)
                {
                    Console.WriteLine(new { err });
                    System.Environment.Exit(42);
                }

                return x;
            };



            #region TcpListener
            new { }.With(
                async delegate
                {

                    //var l = new TcpListener(IPAddress.Any, port);
                    var l = ctor();

            


                    var href =
                        "http://127.0.0.1:" + port;

                    Console.WriteLine(
                        href
                    );

                    while (true)
                    {
                        //Console.WriteLine("before AcceptTcpClientAsync ");

                        var c = await l.AcceptTcpClientAsync();

                        // time to do firewall or security?

                        //Console.WriteLine("before yield " + new { c });

                        yield(c);
                        //Console.WriteLine("after yield " + new { c });
                    }
                }
            );
            #endregion


            var onStartCommand_status = base.onStartCommand(value0, flags, startId);

            Console.WriteLine("exit onStartCommand " + new { onStartCommand_status });

            // I/System.Console( 9005): 232d:0001 exit onStartCommand { onStartCommand_status = 1 }

            return onStartCommand_status;
        }




        static async void yield(TcpClient c)
        {
            // "X:\util\janusvr_windows\JanusVR_Win32\janusvr.exe"

            //Console.WriteLine("enter yield, before ReadAsync");

            // 83.187.193.24:16943
            // I/System.Console(11801): 2e19:757c { RemoteEndPoint = 90. :63806 }

            // I/System.Console( 9900): 26ac:7429 { RemoteEndPoint = 192. :63789 }

            Console.WriteLine(
                new
                {
                    // do we trust that device?

                    c.Client.RemoteEndPoint
                }
            );


            var s = c.GetStream();

            // could we switch into a worker thread?
            // jsc would need to split the stream object tho



            var buffer = new byte[1024];
            // why no implict buffer?
            var count = await s.ReadAsync(buffer, 0, buffer.Length);

            if (count < 0)
            {
                //Console.WriteLine("after ReadAsync failed?");
            }
            else
            {
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150706

                var input = Encoding.UTF8.GetString(buffer, 0, count);

                //new IHTMLPre { new { input } }.AttachToDocument();
                //Console.WriteLine("enter yield: " + input);



                // http://stackoverflow.com/questions/369498/how-to-prevent-iframe-from-redirecting-top-level-window
                var outputString = @"HTTP/1.0 200 OK 
Content-Type:	text/html; charset=utf-8
Connection: close

<title>janusVR</title><body>

<FireBoxRoom>
	<Assets>
		<AssetWebSurface src='#surface' id='webpage_id' width='800' height='400' />
	<AssetScript src='view-source' />
		</Assets>
<Room use_local_asset='room_plane'  pos='0 0 0' fwd='0 0 1' col='0 0 1'  >

<Text pos='0 2 -3' fwd='0 0 1' col='0 0 1' scale='9 9 9'>hi! Extremely minimal initial JavaScript example</Text>
<Paragraph pos='5 5 5' fwd='0 0 1' col='0.5 0.8 0.5' scale='2 2 2' locked='false'>example paragraph's text</Paragraph>

<Link url='bookmarks' title='Bookmarks' draw_text='false' auto_load='true' pos='4 4 -3' fwd='0 0 1'  scale='2.3 2.8 1.0' />

	<Object id='plane' pos='-4 1 -1' fwd='0 0 1' scale='2 2 4' websurface_id='webpage_id' />
</Room>	
</FireBoxRoom>

</body>
";
                var obuffer = Encoding.UTF8.GetBytes(outputString);

                await s.WriteAsync(obuffer, 0, obuffer.Length);
            }


            c.Close();

            //Console.WriteLine("exit yield");

        }



        Action AtDestroy;

        public override void onDestroy()
        {
            base.onDestroy();



        }


        // http://apiwave.com/java/api/android.app.PendingIntent

        public override android.os.IBinder onBind(Intent value)
        {
            return null;
        }


    }


}

//[javac]     W:\gen\AndroidMultiProcTCPServerAsync\Activities\R.java
//[javac] W:\src\AndroidMultiProcTCPServerAsync\Activities\SecondaryActivity.java:72: error: cannot find symbol
//[javac]             class25.fd = file3.getFileDescriptor();
//[javac]                               ^
//[javac]   symbol:   method getFileDescriptor()
//[javac]   location: variable file3 of type MemoryFile
//[javac] W:\src\AndroidMultiProcTCPServerAsync\Activities\SecondaryActivity___c__DisplayClass2.java:43: error: constructor MemoryFile in class MemoryFile cannot be applied to given types;
//[javac]             file0 = new MemoryFile(this.fd, 100, "r");
//[javac]                     ^
//[javac]   required: String,int
//[javac]   found: FileDescriptor,int,String
//[javac]   reason: actual and formal argument lists differ in length