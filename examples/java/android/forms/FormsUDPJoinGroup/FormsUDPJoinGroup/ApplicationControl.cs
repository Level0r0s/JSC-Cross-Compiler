using ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.Net.Sockets;
using System.Net;

namespace FormsUDPJoinGroup
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button2.Text = "Clicked!";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button1.Text = "Clicked!";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            // list NIC

            this.FindForm().Text = "NIC?";

            #region f
            Action<IPAddress> f = nic =>
            {
                //Console.WriteLine(ip.Address.ToString());

                var button_onmouseup = new Button
                {

                    Dock = DockStyle.Top,

                    //Text = n.Name + ": " + ip.Address.ToString()
                    Text = "onmouseup " + nic.ToString()
                }.AttachTo(this);

                button_onmouseup.Click += async delegate
                {
                    // wll this run on JVM forms too?


                    button_onmouseup.Enabled = false;

                    // time to listen for udp input.

                    //var port = new Random().Next(16000, 40000);
                    var port = 40804;

                    // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs

                    Console.WriteLine("UdpClient " + new
                    {
                        nic,
                        port
                    });
                    var uu = new UdpClient(
                        port
                    //new IPEndPoint(nic, port)
                    );

                    Console.WriteLine("JoinMulticastGroup");

                    uu.JoinMulticastGroup(
                        // binds to all interfaces?
                        IPAddress.Parse("239.1.2.3"), nic
                        );

                    while (true)
                    {
                        // UdpReceiveResult
                        Console.WriteLine("ReceiveAsync");
                        var x = await uu.ReceiveAsync(); // did we jump to ui thread?

                        //Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer) + " " + x.RemoteEndPoint.Address);
                        Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                        this.FindForm().Text = Encoding.UTF8.GetString(x.Buffer);

                        //Console.WriteLine(new { x.Buffer.Length });
                    }

                };


                var button_onmousemove = new Button
                {

                    Dock = DockStyle.Top,

                    //Text = n.Name + ": " + ip.Address.ToString()
                    Text = "onmousemove " + nic.ToString()
                }.AttachTo(this);

                button_onmousemove.Click += async delegate
                {
                    button_onmousemove.Enabled = false;

                    // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                    // X:\jsc.svn\examples\java\android\LANBroadcastListener\LANBroadcastListener\ApplicationActivity.cs
                    // X:\jsc.svn\examples\java\android\Test\TestUDPSend\TestUDPSend\ApplicationActivity.cs

                    var uu = new UdpClient(41814);
                    uu.JoinMulticastGroup(IPAddress.Parse("239.1.2.3"), nic);
                    while (true)
                    {
                        var x = await uu.ReceiveAsync(); // did we jump to ui thread?
                        Console.WriteLine("ReceiveAsync done " + Encoding.UTF8.GetString(x.Buffer));
                        this.FindForm().Text = Encoding.UTF8.GetString(x.Buffer);
                    }
                };
            };
            #endregion

            System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().WithEach(
                n =>
                {
                    // Z:\jsc.svn\examples\javascript\chrome\hybrid\HybridHopToUDPChromeApp\Application.cs
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
                                    f(ip.Address);
                        }
                    }




                }
            );

        }
    }
}
