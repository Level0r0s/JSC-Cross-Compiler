using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.System.Threading
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/thread.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/Thread.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Diagnostics.Process/src/System/Diagnostics/ProcessThread.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/Thread.cs

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Threading/Thread.cs
    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Activator.cs
    // https://github.com/konsoletyper/teavm/blob/master/teavm-classlib/src/main/java/org/teavm/classlib/java/lang/TThread.java

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\BCLImplementation\System\Threading\Thread.cs


    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal unsafe class __Thread
    {
        public pthread_t InternalThread;


        ThreadStart __ThreadStart;
        public __Thread(ThreadStart e)
        {
            //if (e.Target != null)
            //{
            //    //throw new NotImplementedException("for now ScriptCoreLibNative supports only static thread starts..");
            //    Console.WriteLine("for now ScriptCoreLibNative supports only static thread starts..");

            //    return;
            //}
            //Console.WriteLine("__Thread ");
            __ThreadStart = e;
        }


        public void Start()
        {
            // X:\jsc.svn\examples\java\android\vr\OVRWindWheelNDK\OVRWindWheelNDK\VrCubeWorld.AppThread.cs
            var createErr = pthread.pthread_create(out this.InternalThread, null, AppThreadFunction, this);

        }

        static object AppThreadFunction(__Thread that)
        {
            that.__ThreadStart();

            return null;
        }

        public void Join()
        {
            pthread.pthread_join(this.InternalThread, null);
        }


        public static void Sleep(int millisecondsTimeout)
        {
            unistd.usleep(1000 * millisecondsTimeout);
        }

    }
}
