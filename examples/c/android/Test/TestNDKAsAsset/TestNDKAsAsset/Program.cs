using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


//[assembly: Obfuscation(Feature = "script")]
[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(TestNDKAsAsset.xNativeActivity))]
// same namespace??
// merged out?
//[assembly: ScriptTypeFilter(ScriptType.Java, typeof(TestNDKAsAsset.xActivity))]


namespace TestNDKAsAsset
{
    using ScriptCoreLib;
    using ScriptCoreLibAndroidNDK.Library;
    using ScriptCoreLibNative.SystemHeaders;
    using ScriptCoreLibNative.SystemHeaders.android;
    using ScriptCoreLibNative.SystemHeaders.sys;
    using System.Runtime.CompilerServices;

    [Obfuscation(StripAfterObfuscation = true)]
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518
        // X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\staging\jni\Application.mk
        //├───arm64-v8a
        //├───armeabi
        //├───armeabi-v7a
        //├───mips
        //├───mips64
        //├───x86
        //└───x86_64

        // first we have to compile our C dll


        static void Main(string[] args)
        {
        }
    }

    //    Updated and renamed default.properties to project.properties
    //    Updated local.properties
    //    No project name specified, using project folder name 'staging'.
    //If you wish to change it, edit the first line of build.xml.
    //Added file X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\build.xml
    //Added file X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\proguard-project.txt

    // to be used by nuget user
    partial class xActivity
    {

    }


    // [armeabi-v7a] Install        : libTestNDKAsAsset.so => libs/armeabi-v7a/libTestNDKAsAsset.so
    // "X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\libs\armeabi-v7a\libTestNDKAsAsset.so"
    [Obfuscation(StripAfterObfuscation = true)]

    // x:\jsc.svn\examples\java\android\androidndknugetexperiment\androidndknugetexperiment\applicationactivity.cs
    [Script]
    partial class xNativeActivity
    {
        // Java_TestNDKAsAsset_xActivity_stringFromJNI

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_TestNDKAsAsset_xActivity_stringFromJNI(
            // what would we be able to do inspecting the runtime?
            ref JNIEnv env,
            jobject thiz)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525

            var n = env.NewStringUTF;

            // look almost the same file!

            // if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n(ref env, "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI. yay");

            return v;


            // ConfigurationCreateNuGetPackage.cs
        }









        //   // returns memory address for ashmem region
        //private static native long native_mmap(FileDescriptor fd, int length, int mode)
        //   void* result = mmap(NULL, length, prot, MAP_SHARED, fd, 0);

        // http://developer.oesf.biz/em/developer/reference/durian/android/os/MemoryFile.html
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\os\MemoryFile.cs
        // X:\jsc.svn\examples\java\android\test\TestMultiProcMemoryFile\TestMultiProcMemoryFile\ApplicationActivity.cs
        // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/os/MemoryFile.java

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static long Java_TestNDKAsAsset_xActivity_mmap(
            // what would we be able to do inspecting the runtime?
            ref JNIEnv env,
            jobject thiz,
            int fd,
            int length)
        {
            const int PROT_READ = 0x1;
            const int PROT_WRITE = 0x2;
            const int MAP_SHARED = 0x0010;

            //jni/TestNDKAsAsset.exe.c:72:13: warning: cast from pointer to integer of different size [-Wpointer-to-int-cast]
            //     return  (long long)mmap((void*)NULL, (int)length, (int)1, (int)16, (int)fd, (int)0);
            // http://osdir.com/ml/android-porting/2010-05/msg00182.html

            var z = (int)ScriptCoreLibNative.SystemHeaders.sys.mman_h.mmap(
                null,
                length,
                //PROT_READ | PROT_WRITE,
                PROT_READ,
                MAP_SHARED,
                fd,
                0
                );

            //log.__android_log_print(

            ConsoleExtensions.tracei("mmap", z);


            // this will crash Vs2015
            //ScriptCoreLibNative.SystemHeaders.android.log.__android_log_print(
            //    ScriptCoreLibNative.SystemHeaders.android.log.android_LogPriority.ANDROID_LOG_INFO,
            //    "System.Console",

            //    fmt: "after mmap"
            //);

            // http://stackoverflow.com/questions/17202741/why-does-mmap-fail-with-permission-denied-for-the-destination-file-of-a-file-c
            // "X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\libs\armeabi-v7a\libTestNDKAsAsset.so"
            // http://linux.die.net/man/3/explain_mmap

            return (long)z;
        }

















        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static unsafe long Java_TestNDKAsAsset_xActivity_send_fd(
              ref JNIEnv env,
              jobject thiz,
              int sock,
              int fd)
        {
            // http://stackoverflow.com/questions/14643571/localsocket-communication-with-unix-domain-in-android-ndk
            // http://en.wikipedia.org/wiki/Unix_domain_socket
            // http://stackoverflow.com/questions/9475442/unix-domain-socket-vs-named-pipes
            // http://keithp.com/blogs/fd-passing/

            // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\sys\socket.cs
            // "X:\opensource\github\libancillary\fd_recv.c"

            byte payload = 0xFF;

            //socket_h.iovec payload_io = { &payload, 1 };
            socket_h.iovec payload_iov;


            payload_iov.iov_base = &payload;
            payload_iov.iov_len = 1;


            __msg_control_cmsghdr_int buffer;


            socket_h.msghdr msghdr;

            //msghdr.msg_name = default(void*);
            msghdr.msg_namelen = 0;
            msghdr.msg_iov = &payload_iov;
            msghdr.msg_iovlen = 1;
            msghdr.msg_flags = 0;
            msghdr.msg_control = &buffer;

            // Severity	Code	Description	Project	File	Line
            //Error CS0266  Cannot implicitly convert type 'int' to 'ulong'.An explicit conversion exists (are you missing a cast?)    TestNDKAsAsset X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\Program.cs	179

            msghdr.msg_controllen = (ulong)sizeof(__msg_control_cmsghdr_int);

            // http://linux.about.com/library/cmd/blcmdl3_CMSG_FIRSTHDR.htm

            buffer.cmsg.cmsg_len = msghdr.msg_controllen;
            buffer.cmsg.cmsg_level = socket_h.SOL_SOCKET;

            // http://man7.org/tlpi/code/online/dist/sockets/scm_rights_send.c.html
            // http://www.lst.de/~okir/blackhats/node121.html
            // http://man7.org/tlpi/code/online/dist/sockets/scm_rights.h.html
            buffer.cmsg.cmsg_type = socket_h.SCM_RIGHTS;

            buffer.data0 = fd;


            // http://linux.die.net/man/2/recvmsg
            var status = socket_h.sendmsg((SOCKET)sock, &msghdr, 0);
            if (status < 0)
                return -1;

            // ok
            return 0;
        }

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static unsafe long Java_TestNDKAsAsset_xActivity_recvmsg_fd(
                ref JNIEnv env,
                jobject thiz,
                int sock,
                int length)
        {
            // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\sys\socket.cs
            // "X:\opensource\github\libancillary\fd_recv.c"

            byte payload = 0xFF;

            //socket_h.iovec payload_io = { &payload, 1 };
            socket_h.iovec payload_iov;


            payload_iov.iov_base = &payload;
            payload_iov.iov_len = 1;


            __msg_control_cmsghdr_int buffer;


            socket_h.msghdr msghdr;

            //msghdr.msg_name = default(void*);
            msghdr.msg_namelen = 0;
            msghdr.msg_iov = &payload_iov;
            msghdr.msg_iovlen = 1;
            msghdr.msg_flags = 0;
            msghdr.msg_control = &buffer;

            // Severity	Code	Description	Project	File	Line
            //Error CS0266  Cannot implicitly convert type 'int' to 'ulong'.An explicit conversion exists (are you missing a cast?)    TestNDKAsAsset X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\Program.cs	179

            msghdr.msg_controllen = (ulong)sizeof(__msg_control_cmsghdr_int);

            // http://linux.about.com/library/cmd/blcmdl3_CMSG_FIRSTHDR.htm

            buffer.cmsg.cmsg_len = msghdr.msg_controllen;
            buffer.cmsg.cmsg_level = socket_h.SOL_SOCKET;

            // http://man7.org/tlpi/code/online/dist/sockets/scm_rights_send.c.html
            // http://www.lst.de/~okir/blackhats/node121.html
            // http://man7.org/tlpi/code/online/dist/sockets/scm_rights.h.html
            buffer.cmsg.cmsg_type = socket_h.SCM_RIGHTS;

            buffer.data0 = -1;


            // http://linux.die.net/man/2/recvmsg
            var recvfrom_status = socket_h.recvmsg((SOCKET)sock, &msghdr, 0);
            if (recvfrom_status < 0)
                return -1;


            return buffer.data0;
        }

        // X:\jsc.svn\examples\c\Test\TestSizeOfUserStruct\TestSizeOfUserStruct\Class1.cs
        [Script]
        public struct __msg_control_cmsghdr_int
        {
            public socket_h.cmsghdr cmsg;

            public int data0;
        }
    }
}