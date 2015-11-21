using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices
{
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Runtime/CompilerServices/INotifyCompletion.cs

	[Script(ImplementsViaAssemblyQualifiedName = "System.Runtime.CompilerServices.INotifyCompletion")]
    public interface __INotifyCompletion
    {
        // https://github.com/dotnet/corert/blob/master/src/System.Private.Threading/src/System/Runtime/CompilerServices/INotifyCompletion.cs

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150717/adbswitchtocompiler
        // X:\jsc.svn\examples\java\android\future\ADBSwitchToCompiler\ADBSwitchToCompiler\ApplicationActivity.cs

        // http://msdn.microsoft.com/en-us/library/system.runtime.compilerservices.inotifycompletion.aspx
        void OnCompleted(Action continuation);
    }
}
