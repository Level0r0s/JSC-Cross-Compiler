using Microsoft.CSharp.RuntimeBinder;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.Microsoft.CSharp.RuntimeBinder
{
    [Script(Implements = typeof(global::Microsoft.CSharp.RuntimeBinder.Binder))]
    public static partial class __Binder
    {
        // Z:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\Microsoft\CSharp\RuntimeBinder\Binder.cs

        public static CallSiteBinder InvokeMember(
           CSharpBinderFlags flags,
           string name,
           IEnumerable<Type> typeArguments,
           Type context,
           IEnumerable<CSharpArgumentInfo> argumentInfo
           )
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/ndktype

            return null;
        }
    }
}
