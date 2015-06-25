using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.sys
{
    // http://stackoverflow.com/questions/1309509/correct-way-to-marshal-size-t
    //using socklen_t = ulong;
    //using socklen_t = UIntPtr;
    using socklen_t = UInt64;
    using size_t = UInt64;

    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\sys\socket.h"
    // "x:\util\android-ndk-r10e\platforms\android-21\arch-arm64\usr\include\sys\socket.h"

    public enum SOCKET { }

    [Script(IsNative = true, Header = "sys/socket.h", IsSystemHeader = true)]
    public static class socket_h
    {
        // #include <linux/uio.h>
        [Script(IsNative = true)]
        public unsafe struct iovec
        {
            public byte* iov_base;

            public long iov_len;
            //public UIntPtr iov_len;
        };

        [Script(IsNative = true)]
        public struct cmsghdr
        {
            public size_t cmsg_len;
            public int cmsg_level;
            public int cmsg_type;
        };

        [Script(IsNative = true)]
        public unsafe struct msghdr
        {
            // http://stackoverflow.com/questions/2550774/what-is-size-t-in-c

            public void* msg_name;
            public socklen_t msg_namelen;
            public iovec* msg_iov;
            public size_t msg_iovlen;
            public void* msg_control;
            public size_t msg_controllen;
            public int msg_flags;
        };


        public const uint INADDR_ANY = 0;

        public const int AF_INET = 2;
        public const int SOCK_DGRAM = 2;

        public const int IPPROTO_UDP = 17;
        public const int IPPROTO_IP = 0;

        public const int IP_MULTICAST_TTL = 33;
        public const int IP_MULTICAST_IF = 32;
        public const int IP_ADD_MEMBERSHIP = 35;

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740532(v=vs.85).aspx
        public const int SOL_SOCKET = 1;

        public const int SO_REUSEADDR = 2;

        public const int SCM_RIGHTS = 0x01;

        // http://comments.gmane.org/gmane.comp.handhelds.android.ndk/22418
        // http://stackoverflow.com/questions/10408980/android-ndk-sockets-network-unreachable

        // what does it take to build 
        // a native app on the red server?
        // would it be useful for lan udb broadcasts?

        // http://www.roman10.net/simple-tcp-socket-client-and-server-communication-in-c-under-linux/
        // http://www.sockets.com/ch16.htm



        // http://mobilepearls.com/labs/native-android-api/

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402
        // X:\jsc.svn\examples\c\android\Test\TestNDK\TestNDK\xNativeActivity.cs
        // X:\jsc.svn\examples\c\android\Test\TestNDKUDP\TestNDKUDP\xNativeActivity.cs


        // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\sys\socketcalls.h"
        // #define SYS_SENDTO      11              /* sys_sendto(2)                */

        // Show Details	Severity	Code	Description	Project	File	Line
        //Error CS0542	'socket': member names cannot be the same as their enclosing type ScriptCoreLibAndroidNDK socket.cs	35

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740506(v=vs.85).aspx
        public static SOCKET socket(int af, int sock, int ipproto) { return default(int); }


        public unsafe static int setsockopt(this SOCKET s, int level, int optname, void* optval, int optlen) { return default(int); }
        //int setsockopt(int s, int level, int optname, const void* optval, int optlen);

        public unsafe static int bind(this SOCKET s, sockaddr* name, int namelen) { return default(int); }
        //__socketcall int bind(int, const struct sockaddr *, int);


        //__socketcall ssize_t recvfrom(int, void*, size_t, unsigned int, const struct sockaddr *, socklen_t*);
        public unsafe static int recvfrom(
          this SOCKET s,
          byte* buf,
          int len,
          uint flags,
          sockaddr* from,
          int* fromlen
        )
        { return default(int); }



        // http://cyberkinetica.homeunix.net/os2tk45/tcppr/087_L3_Multicastingandthese.html
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms738571(v=vs.85).aspx
        [Script(IsNative = true)]
        public struct in_addr
        {
            public ulong s_addr;

            // 8
        }


        // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\arpa\inet.h"
        public static ulong inet_addr(this string cp) { return default(long); }

        // http://www.beej.us/guide/bgnet/output/html/multipage/inet_ntoaman.html
        public static string inet_ntoa(this in_addr cp) { return default(string); }

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms738557(v=vs.85).aspx
        public static ushort htons(this ushort hostshort) { return default(ushort); }
        public static uint htonl(this uint hostshort) { return default(uint); }

        [Script(IsNative = true)]
        public struct ip_mreq
        {
            public in_addr imr_multiaddr;
            public in_addr imr_interface;
            // 16
        }

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740496(v=vs.85).aspx
        // http://stackoverflow.com/questions/18609397/whats-the-difference-between-sockaddr-and-sockaddr-insockaddr-in6
        [Script(IsNative = true)]
        public unsafe struct sockaddr
        {
            ushort sa_family;
            // 4

            // buffer
            fixed byte sa_data[14];

            // 18
        };


        // X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\linux\in.h
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms740496(v=vs.85).aspx
        // http://stackoverflow.com/questions/2310103/why-a-c-sharp-struct-cannot-be-inherited
        [Script(IsNative = true)]
        public unsafe struct sockaddr_in // : sockaddr
        {
            public short sin_family;

            // http://stackoverflow.com/questions/19207745/htons-function-in-socket-programing
            // 4
            public ushort sin_port;
            // 8

            public in_addr sin_addr;
            // 16

            public fixed byte sin_zero[8];
            // 24
        };

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms738520(v=vs.85).aspx
        // uhp?

        //__socketcall int sendmsg(int, const struct msghdr*, int);
        //__socketcall int recvmsg(int, struct msghdr*, int);

        // ref
        public unsafe static int sendmsg(this SOCKET s, msghdr* msg, int flags) { return default(int); }

        // out
        public unsafe static int recvmsg(this SOCKET s, msghdr* msg, int flags) { return default(int); }
    }

}
