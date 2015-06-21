using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib
{
    /// <summary>
    /// Renames a native namespace. For example java.lang.String could be written as ScriptCoreLibJava.java.lang.String.
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = true)]
    public sealed class ScriptNamespaceRenameAttribute : Attribute
    {
        public string NativeNamespaceName;
        public string VirtualNamespaceName;

        // X:\jsc.svn\examples\c\Test\TestNamespaceFixup\TestNamespaceFixup\Class1.cs
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150621
        ///// <summary>
        ///// Only native classes shall be considered while renaming. Or user 
        ///// </summary>
        //[Obsolete]
        public bool FilterToIsNative;

        public ScriptNamespaceRenameAttribute()
        {

        }
    }

}
