using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebServiceDoubleArray
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // ......1c58:01:01:0f RewriteToAssembly error: System.NullReferenceException: Object reference not set to an instance of an object.
        //at jsc.meta.Library.ILStringConversions.<>c__DisplayClassc4.<Prepare>b__63(Type CacheType, ILStringConversionArguments e) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 1021
        // doesnt work yet?

        //public void WebMethod2(Action<double[]> y)
        public void WebMethod2(Action<xDoubleArray> y)
        {
            y(
                new xDoubleArray
                {
                    value =
                        new double[] { 1, 2, 3 }
                }
            );
        }

    }

    public class xDoubleArray
    {
        public double[] value;
    }
}

//2620:01:01 0044:0015 TestWebServiceDoubleArray.Application create ScriptCoreLib.Ultra::ScriptCoreLib.JavaScript.Remoting.InternalWebMethodRequest+<>c__DisplayClass1
//2620:01:01 003c:0016 TestWebServiceDoubleArray.Application create TestWebServiceDoubleArray::TestWebServiceDoubleArray.xDoubleArray
//2620:01:01:0f RewriteToAssembly error: System.NullReferenceException: Object reference not set to an instance of an object.
//   at jsc.meta.Library.ILStringConversions.<>c__DisplayClassc4.<Prepare>b__63(Type CacheType, ILStringConversionArguments e) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 1021
//   at jsc.meta.Library.ILStringConversions.ILStringConversion.<.ctor>b__e6(ILStringConversionArguments e) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 48
//   at jsc.meta.Library.ILStringConversions.<>c__DisplayClass9e.<>c__DisplayClassc8.<>c__DisplayClasscc.<Prepare>b__69() in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 1194
//   at jsc.meta.Library.ILGeneratorExtensions.EmitWithLocal(ILGenerator il, LocalBuilder Local, Action handler) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILGeneratorExtensions.cs:line 142
//   at jsc.meta.Library.ILStringConversions.<>c__DisplayClass9e.<>c__DisplayClassc8.<Prepare>b__66(FieldInfo CacheField) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 1185
//   at ScriptCoreLib.Extensions.LinqExtensions.With[T](T e, Action`1 h) in z:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 28
//   at ScriptCoreLib.Extensions.LinqExtensions.InternalWithEach[T](IEnumerable`1 collection, Action`1 h) in z:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 180
//   at ScriptCoreLib.Extensions.LinqExtensions.WithEach[T](IEnumerable`1 collection, Action`1 h) in z:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\LinqExtensions.cs:line 150
//   at jsc.meta.Library.ILStringConversions.<>c__DisplayClass9e.<Prepare>b__65(ILGenerator il, LocalBuilder doc, LocalBuilder value, Type CacheType, ILStringConversionArguments e) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Library\ILStringConversions.cs:line 1167