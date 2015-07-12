using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Threading
{
    // used by
    // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\BCLImplementation\System\Threading\Thread.cs
    // NDK

    [Script(Implements = typeof(global::System.Threading.ThreadStart))]
    internal delegate void __ThreadStart();
}
