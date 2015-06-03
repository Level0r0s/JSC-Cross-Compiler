using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestSizeOfUserStruct
{
    public unsafe static class foo
    {
        public static void invoke()
        {
            // x:\jsc.svn\examples\c\android\test\testndkasasset\testndkasasset\program.cs

            __msg_control_cmsghdr_int buffer;

            buffer.data0 = -1;

            var len = sizeof(__msg_control_cmsghdr_int);

        }
    }
    public struct __msg_control_cmsghdr_int
    {
        //public socket_h.cmsghdr cmsg;
        //public ulong cmsg;
        public long cmsg;

        public int data0;
    }

    //Error	1	'TestSizeOfUserStruct.__msg_control_cmsghdr_int' does not have a predefined size, therefore sizeof can only be used in an unsafe context (consider using System.Runtime.InteropServices.Marshal.SizeOf)	X:\jsc.svn\examples\c\Test\TestSizeOfUserStruct\TestSizeOfUserStruct\Class1.cs	18	23	TestSizeOfUserStruct


}
