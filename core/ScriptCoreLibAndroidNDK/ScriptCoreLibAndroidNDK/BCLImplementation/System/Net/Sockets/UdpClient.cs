using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Net.Sockets;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.BCLImplementation.System;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/UdpClient.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/UdpClient.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Sockets\UdpClient.cs
    // X:\jsc.svn\market\synergy\javascript\chrome\chrome\BCLImplementation\System\Net\Sockets\UdpClient.cs
    // Z:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\BCLImplementation\System\Net\Sockets\UdpClient.cs

    [Script(Implements = typeof(global::System.Net.Sockets.UdpClient))]
    public unsafe class __UdpClient
    {
        private SOCKET s;
        private ushort port;

        // Z:\jsc.svn\examples\c\android\NDKUdpClient\xNativeActivity.cs
        // Z:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs

        public __UdpClient(int port)
        {
            ConsoleExtensions.tracei("enter UdpClient port: ", port);

            this.port = (ushort)port;
            this.s = socket_h.socket(socket_h.AF_INET, socket_h.SOCK_DGRAM, socket_h.IPPROTO_UDP);

            ConsoleExtensions.tracei("socket ", (int)s);

        }

        public void JoinMulticastGroup(IPAddress multicastAddr)
        {
            // NIC ?

            var localAddr = new socket_h.in_addr();

            localAddr.s_addr = socket_h.INADDR_ANY;


            // For multicast sending use an IP_MULTICAST_IF flag with the setsockopt() call. This specifies the interface to be used.
            {
                var status = s.setsockopt(socket_h.IPPROTO_IP, socket_h.IP_MULTICAST_IF, &localAddr, sizeof(socket_h.in_addr));

                // anonymous types like linq expressions?
                ConsoleExtensions.tracei("setsockopt IP_MULTICAST_IF: ", status);
            }





            var mreq = ((__IPAddress)multicastAddr).mreq;

            ConsoleExtensions.trace("enter UdpClient JoinMulticastGroup");

            {
                var status = s.setsockopt(socket_h.IPPROTO_IP, socket_h.IP_ADD_MEMBERSHIP, &mreq, sizeof(socket_h.ip_mreq));
                ConsoleExtensions.tracei("setsockopt IP_ADD_MEMBERSHIP: ", status);
            }


            socket_h.sockaddr_in localEndPoint;
            //ushort gport = 40804;

            // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPClipboard\Application.cs
            //ushort gport = 49814;

            localEndPoint.sin_family = socket_h.AF_INET;
            localEndPoint.sin_addr.s_addr = socket_h.INADDR_ANY.htonl();
            localEndPoint.sin_port = this.port.htons();


            // Bind the socket to the port
            {
                int bindret = s.bind((socket_h.sockaddr*)&localEndPoint, sizeof(socket_h.sockaddr_in));

                ConsoleExtensions.tracei("bind: ", bindret);
            }
        }





        public Task<UdpReceiveResult> ReceiveAsync()
        {
            // workaround...
            int malloc_bytearray_paddingleft = 4;



            ConsoleExtensions.trace("enter UdpClient ReceiveAsync");

            var buff = stackalloc byte[0xfff];

            socket_h.sockaddr_in sender;
            var sizeof_sender = sizeof(socket_h.sockaddr_in);

            //ConsoleExtensions.trace("before recvfrom");

            // http://pubs.opengroup.org/onlinepubs/009695399/functions/recvfrom.html
            // Upon successful completion, recvfrom() shall return the length of the message in bytes. 
            var recvfromret = s.recvfrom(buff, 0xfff, 0, (socket_h.sockaddr*)&sender, &sizeof_sender);
            // sent by?

            //I/xNativeActivity(24024): X:\jsc.svn\examples\c\android\Test\NDKUdpClient\NDKUdpClient\xNativeActivity.cs:167 recvfrom:  116 errno: 22 Invalid argument
            //I/xNativeActivity(24024): X:\jsc.svn\examples\c\android\Test\NDKUdpClient\NDKUdpClient\xNativeActivity.cs:168 SenderAddrSize:  16 errno: 22 Invalid argument
            ConsoleExtensions.tracei("recvfrom: ", recvfromret);
            //tracei("sockaddr_in: ", sizeof_sender);

            buff[recvfromret] = 0;


            //Task<UdpReceiveResult> 

            // lets fill up GC. collect when thread shuts down?
            //var Buffer = new byte[recvfromret];


            // what about digitally signing the buffer?
            //var le_bytes = (byte*)stdlib_h.malloc(stdlib_h.malloc_bytearray_paddingleft + recvfromret);
            var le_bytes = (byte*)stdlib_h.malloc(malloc_bytearray_paddingleft + recvfromret);


            // I/xNativeActivity(12673): [2781936] \UdpClient.cs:110 recvfrom:  3
            //I/xNativeActivity(12673): [2781936] \UdpClient.cs:138 bytesLength:  50331648
            ConsoleExtensions.trace("set le_ipointer");
            var le_ipointer = (int*)le_bytes;
            *le_ipointer = recvfromret;

            //int32_5 = ((int*)byte_4);
            //*int32_5 = num3;

            {
                ConsoleExtensions.trace("get le_ipointer");
                var lei = *le_ipointer;
                ConsoleExtensions.tracei("lei: ", lei);
            }


            // Z:\jsc.svn\examples\c\Test\TestPointerOffset\Class1.cs

            #region ___bytes
            // byte_7 = ((unsigned char*)(&(byte_4[1])));
            var ___bytes = le_bytes + malloc_bytearray_paddingleft;
            for (int i = 0; i < recvfromret; i++)
            {
                var byte0 = buff[i];

                ConsoleExtensions.tracei("set byte0: ", byte0);

                //Buffer[i] = buff[i];
                ___bytes[i] = byte0;
            }
            #endregion

            {
                ConsoleExtensions.trace("get le_ipointer");
                var lei = *le_ipointer;
                ConsoleExtensions.tracei("lei: ", lei);
            }


            var bytes = __cast(___bytes);

            {
                ConsoleExtensions.trace("---");

                var byte0 = bytes[0];
                ConsoleExtensions.tracei("byte0: ", byte0);

                var bytesLength = bytes.Length;
                ConsoleExtensions.tracei("bytesLength: ", bytesLength);
            }

            // ptr?

            //{

            //    var xbytes = __cast(bytes);
            //    var xlebytes = xbytes - 4;
            //    var ile = (int*)xlebytes;
            //    ConsoleExtensions.tracei("ile: ", *ile);

            //}


            //I/xNativeActivity( 2541): [2777960] \UdpClient.cs:199 set task_Result_Buffer:  -192277436
            //I/xNativeActivity( 2541): [2777960] \UdpClient.cs:202 Result.Buffer.Length:  2
            //I/xNativeActivity( 2541): [2777960] \UdpClient.cs:211 got task_Result
            //I/xNativeActivity( 2541): [2777960] \UdpClient.cs:229 got task_Result_Buffer:  -867631102

            ConsoleExtensions.tracei64("set task_Result_Buffer: ", (long)__cast(bytes));


            //var Result = new UdpReceiveResult(bytes, null);
            var Result = new __UdpReceiveResult(bytes, null);

            //result13 = ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult__ctor_6000072(__new_ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult(1), byteArray11, NULL);
            //    ScriptCoreLibAndroidNDK_Library_ConsoleExtensions_tracei64("Result: ", (long long)result13, "z:\x5c""jsc.svn\x5c""core\x5c""ScriptCoreLibAndroidNDK\x5c""ScriptCoreLibAndroidNDK\x5c""BCLImplementation\x5c""System\x5c""Net\x5c""Sockets\x5c""UdpClient.cs", 212);


            // \UdpClient.cs:210 Result:  0
            ConsoleExtensions.tracei64("Result: ", (long)(object)Result);


            //typedef struct tag_ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult
            //{
            //    struct tag_ScriptCoreLibJava_BCLImplementation_System_Net___IPEndPoint* _RemoteEndPoint_k__BackingField;
            //    unsigned char* _Buffer_k__BackingField;
            //} ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult, *LPScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult;


            //ConsoleExtensions.tracei("Result.Buffer.Length: ", Result.Buffer.Length);

            //var task = new __Task<UdpReceiveResult>();
            var task = new __Task<__UdpReceiveResult>();

            //typedef struct tag_ScriptCoreLibNative_BCLImplementation_System___Task_1
            //{
            //    void* _Result_k__BackingField;
            //} ScriptCoreLibNative_BCLImplementation_System___Task_1, *LPScriptCoreLibNative_BCLImplementation_System___Task_1;

            task.Result = (__UdpReceiveResult)(object)Result;


            var task_Result = task.Result;

            ConsoleExtensions.trace("got task_Result, crash?");

            ConsoleExtensions.tracei64("task_Result: ", (long)(object)task_Result);


            //Task<UdpReceiveResult> t = new __Task<UdpReceiveResult> { Result = Result };

            //     ScriptCoreLibAndroidNDK_Library_ConsoleExtensions_tracei("t.Result.Buffer.Length: ", (int)((signed int)(/* ldlen */ *(int*)(&(ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult_get_Buffer((ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult*)(&result18))[-4])))), "z:\x5c""jsc.svn\x5c""core\x5c""ScriptCoreLibAndroidNDK\x5c""ScriptCoreLibAndroidNDK\x5c""BCLImplementation\x5c""System\x5c""Net\x5c""Sockets\x5c""UdpClient.cs", 211);

            // struct??
            //if (task_Result == null)
            //{
            //}
            //else
            {
                //  byteArray19 = ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult_get_Buffer((ScriptCoreLib_Shared_BCLImplementation_System_Net_Sockets___UdpReceiveResult*)(&result18));


                var task_Result_Buffer = task_Result.Buffer;

                // \UdpClient.cs:226 got task_Result_Buffer:  848560130
                ConsoleExtensions.tracei64("got task_Result_Buffer: ", (long)__cast(task_Result_Buffer));

                ConsoleExtensions.tracei("t.Result.Buffer.Length: ", task_Result_Buffer.Length);
                //  t.Result.Buffer.Length:  2
            }

            return (Task<UdpReceiveResult>)(object)task;
        }


        //public static int __bytearray_ldlen(byte[] e)
        //{
        //    return 0;
        //}


        [Script(OptimizedCode = "return e;")]
        public static byte[] __cast(byte* e)
        {
            throw null;
        }

        [Script(OptimizedCode = "return e;")]
        public static byte* __cast(byte[] e)
        {
            throw null;
        }
    }
}
