using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.Threading;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/environment.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Environment.cs
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/environment.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Environment.cs

    // https://github.com/dot42/api/blob/master/System/Environment.cs

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Environment.cs
    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Environment.cs
    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Environment.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Environment.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Environment.cs


    [Script(Implements = typeof(global::System.Environment))]
    internal class __Environment
    {
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRYieldStatement\TestJVMCLRYieldStatement\Program.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\Program.cs

        public static void Exit(int exitCode)
        {
            // http://stackoverflow.com/questions/3715967/when-should-we-call-system-exit-in-java

            // any critical operations?

            java.lang.System.exit(exitCode);

            // will ubuntu upstart restart the service?

            //      respawn
            //A service or task with this stanza will be automatically started
            //if  it  should  stop  abnormally.   All  reasons  for  a service
            //stopping, except the  stop(8)  command  itself,  are  considered
            //abnormal.   Tasks  may  exit  with a zero exit status to prevent
            //being respawned.

        }

        public static int CurrentManagedThreadId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        public static int ProcessorCount
        {
            get
            {
                // X:\jsc.svn\examples\java\hybrid\Test\JVMCLRProcessorCount\JVMCLRProcessorCount\Program.cs

                // http://stackoverflow.com/questions/13834692/threads-configuration-based-on-no-of-cpu-cores
                // http://stackoverflow.com/questions/4759570/finding-number-of-cores-in-java
                int cores = java.lang.Runtime.getRuntime().availableProcessors();

                return cores;
            }
        }

        public static string NewLine
        {
            get
            {
                return "\r\n";
            }
        }


        public static string CurrentDirectory
        {
            // X:\jsc.svn\examples\java\hybrid\JVMCLRLINQOrderByLastWriteTime\JVMCLRLINQOrderByLastWriteTime\Program.cs

            // You cannot change the library path for a running JVM.
            // http://stackoverflow.com/questions/5013547/how-to-influence-search-path-of-system-loadlibrary-through-java-code

            get
            {
                // http://www.devx.com/tips/Tip/13804

                var f = new java.io.File(".");
                var c = default(string);

                try
                {
                    c = f.getCanonicalPath();
                }
                catch
                {
                    throw;
                }

                return c;
            }
        }

        public static string StackTrace
        {
            get
            {
                // Z:\jsc.svn\examples\javascript\crypto\WebServiceAuthorityExperiment\WebServiceAuthorityExperiment\Application.cs
                // X:\jsc.svn\examples\javascript\test\TestChromeStackFrames\TestChromeStackFrames\Application.cs
                // X:\jsc.svn\examples\javascript\CodeTraceExperiment\CodeTraceExperiment\Application.cs
                // X:\jsc.svn\examples\javascript\Test\TestDelegateInvokeDisplayName\TestDelegateInvokeDisplayName\Application.cs

                // can we provide some good caller intel yet?
                // for code patching?
                var value = default(string);

                try
                {
                    //--TypeError: Cannot read property 'stack' of null
                    //throw null;
                    //throw new Exception();

                    // Z:\jsc.svn\examples\javascript\ubuntu\Test\UbuntuTestMySQLInsert\UbuntuTestMySQLInsert\ApplicationWebService.cs
                    __ThrowIt();
                }
                catch (Exception err)
                {
                    value = err.StackTrace;
                }

                return value;
            }
        }

        static void __ThrowIt()
        {
            throw new Exception();
        }
    }
}
