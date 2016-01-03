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
    // http://referencesource.microsoft.com/#mscorlib/system/multicastdelegate.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\MulticastDelegate.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\MulticastDelegate.cs

    [Script(Implements = typeof(global::System.MulticastDelegate))]
    internal class __MulticastDelegate : __Delegate
    {
        public __MulticastDelegate(object e, global::System.IntPtr p)
            : base(e, p)
        {
        }
    }
}
