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
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace TestUDPSend.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText("UDP!");
            b.AtClick(
                v =>
                {

                    Action<IPAddress> sendTracking = nic =>
                    {
                        var port = new Random().Next(16000, 40000);

                        //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                        // where is bind async?
                        // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\UDPWindWheel\Program.cs
                        var socket = new UdpClient(
                             new IPEndPoint(nic, port)
                            );

                        Console.WriteLine("about to bind " + new { nic, port });

                        //  x:\util\android-sdk-windows\platform-tools\adb.exe logcat 
//I/System.Console(15571): 3cd3:0001 enter __UdpClient ctor
//I/System.Console(15571): 3cd3:83e5 exit __IPAddress worker { ElapsedMilliseconds = 7 }
//I/System.Console(15571): 3cd3:0001 enter __UdpClient before this.Client
//I/System.Console(15571): 3cd3:0001 enter __UdpClient after this.Client { Client = ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__Socket@3777711a }
//I/System.Console(15571): 3cd3:0001 about to bind { nic = 192.168.1.126, port = 17052 }
//I/System.Console(15571): 3cd3:0001 enter __Socket Bind { vBind = ScriptCoreLibJava.BCLImplementation.System.Net.Sockets.__Socket_BindDelegate@166d444b }
//D/AndroidRuntime(15571): Shutting down VM
//E/AndroidRuntime(15571): FATAL EXCEPTION: main
//E/AndroidRuntime(15571): Process: TestUDPSend.Activities, PID: 15571
//E/AndroidRuntime(15571): java.lang.RuntimeException

                        //socket.Client.Bind(
                        //     new IPEndPoint(nic, port)
                        //);

                        // who is on the other end?
                        var nmessage = "hello!";

                        var data = Encoding.UTF8.GetBytes(nmessage);      //creates a variable b of type byte


                        //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                        Console.WriteLine("about to Send");
                        socket.Send(
                             data,
                             data.Length,
                             hostname: "239.1.2.3",
                             port: 49834
                         );




                    };

                    #region udp broadcast
                    // overkill at 60hz
                    NetworkInterface.GetAllNetworkInterfaces().WithEach(
                         n =>
                         {
                             // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                             // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

                             var IPProperties = n.GetIPProperties();
                             var PhysicalAddress = n.GetPhysicalAddress();



                             foreach (var ip in IPProperties.UnicastAddresses)
                             {
                                 // ipv4
                                 if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                 {
                                     if (!IPAddress.IsLoopback(ip.Address))
                                         if (n.SupportsMulticast)
                                         {
                                             //fWASDC(ip.Address);
                                             //fParallax(ip.Address);
                                             //fvertexTransform(ip.Address);
                                             sendTracking(ip.Address);
                                         }
                                 }
                             }




                         }
                     );



                    #endregion

                }
            );


            this.setContentView(sv);
        }


    }


}

  //[javac] W:\src\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:248: error: cannot find symbol
  //[javac]         return  __TaskExtensions.<TResult>Unwrap_06000b5d(__Task.get_InternalFactory().<__Task_1<TResult>>StartNew(function));
  //[javac]                                 ^
  //[javac]   symbol:   method <TResult>Unwrap_06000b5d(__Task_1<__Task_1<TResult>>)