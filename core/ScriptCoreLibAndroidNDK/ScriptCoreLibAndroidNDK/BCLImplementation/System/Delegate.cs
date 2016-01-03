using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.BCLImplementation.System.Reflection;

//namespace ScriptCoreLib.Shared.BCLImplementation.System
//namespace ScriptCoreLibNative.BCLImplementation.System
namespace ScriptCoreLibAndroidNDK.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/delegate.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Delegate.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Delegate.cs

    [Script(Implements = typeof(global::System.Delegate))]
    internal class __Delegate
    {
        public object Target { get; set; }
        public MethodInfo Method { get; set; }

        public __Delegate(object e, global::System.IntPtr p)
        {
            //Console.WriteLine("__Delegate.ctor");

            // X:\jsc.svn\examples\c\Test\TestAction\TestAction\Program.cs

            this.Target = e;
            this.Method = new __MethodInfo
            {
                //InternalMethod = ((__IntPtr)(object)p).MethodToken 
                MethodToken = p
            };
        }

        //public object DynamicInvoke(params object[] args);
    }
}
