using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets
{
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpReceiveResult.cs

	//[Script(Implements = typeof(global::System.Net.Sockets.UdpReceiveResult))]
	[Script(ImplementsViaAssemblyQualifiedName = "System.Net.Sockets.UdpReceiveResult")]
	public class __UdpReceiveResult
	{
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160108/udp
        // Z:\jsc.svn\examples\c\android\NDKUdpClient\xNativeActivity.cs

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150630/jvmclrudpreceiveasync
		// X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs

        // actionscript likes one constructor
        public __UdpReceiveResult() : this(default(byte[]), default(IPEndPoint))
        {

        }

		public __UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint) 
		{
			this.Buffer = buffer;
			this.RemoteEndPoint = remoteEndPoint;
        }

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150306/udp
		public IPEndPoint RemoteEndPoint { get; set; }
		public byte[] Buffer { get; set; }
	}
}
