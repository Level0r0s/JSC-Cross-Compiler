using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders.sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.System.Net
{
    [Script(Implements = typeof(global::System.Net.IPAddress))]
    public class __IPAddress
    {
        public socket_h.ip_mreq mreq;

        public static IPAddress Parse(string ipString)
        {
            // Z:\jsc.svn\examples\c\android\NDKUdpClient\xNativeActivity.cs


            var mreq = new socket_h.ip_mreq();

            // "239.1.2.3"
            // ip_mreq3->imr_multiaddr.s_addr = inet_addr((char*)"239.1.2.3");
            //mreq.imr_multiaddr.s_addr = "239.1.2.3".inet_addr();
            mreq.imr_multiaddr.s_addr = ipString.inet_addr();



            var x = new __IPAddress { mreq = mreq };

            return x;
        }



        public static implicit operator global::System.Net.IPAddress(__IPAddress i)
        {
            return (global::System.Net.IPAddress)(object)i;
        }

        public static implicit operator __IPAddress(global::System.Net.IPAddress i)
        {
            return (__IPAddress)(object)i;
        }
    }
}
