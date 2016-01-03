using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/runtimehandles.cs

    [Script(Implements = typeof(global::System.RuntimeTypeHandle))]
    internal class __RuntimeTypeHandle
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/ndktype

        // http://bugs.adobe.com/jira/browse/ASC-2677
        public IntPtr Value { get; /* private */ set; }

        //public static explicit operator __RuntimeTypeHandle(global::java.lang.Class _ptr)
        //{
        //    return new __RuntimeTypeHandle { Value = (IntPtr)(object)new __IntPtr { ClassToken = _ptr } };
        //}
    }
}
