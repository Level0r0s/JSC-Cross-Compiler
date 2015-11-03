using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/tuple.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Tuple.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Tuple.cs


    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Tuple.cs


    //[Script(ImplementsViaAssemblyQualifiedName = "System.Tuple, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    [Script(Implements = typeof(Tuple))]
    public static class __Tuple
    {
        public static __Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new __Tuple<T1, T2>(item1, item2);
        }

        public static __Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new __Tuple<T1, T2, T3>(item1, item2, item3);
        }
    }

    //[Script(ImplementsViaAssemblyQualifiedName = "System.Tuple`2, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
    [Script(Implements = typeof(Tuple<,>))]
    public class __Tuple<T1, T2>
    {
        public __Tuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public T1 Item1 { get; internal set; }
        public T2 Item2 { get; internal set; }
    }

    [Script(Implements = typeof(Tuple<,,>))]
    public class __Tuple<T1, T2, T3>
    {
        // x:\jsc.svn\core\scriptcorelib.extensions\scriptcorelib.extensions\query\experimental\queryexpressionbuilder.cs

        public __Tuple(T1 item1, T2 item2, T3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }


        public T1 Item1 { get; internal set; }
        public T2 Item2 { get; internal set; }
        public T3 Item3 { get; internal set; }
    }


    [Script(Implements = typeof(Tuple<,,,>))]
    public class __Tuple<T1, T2, T3, T4>
    {
        // Z:\jsc.svn\examples\javascript\crypto\test\TestWebServiceRSA\TestWebServiceRSA\Application.cs

        public __Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }


        public T1 Item1 { get; internal set; }
        public T2 Item2 { get; internal set; }
        public T3 Item3 { get; internal set; }
        public T4 Item4 { get; internal set; }
    }
}

//0200001e <>f__AnonymousType$229$1
//0200001f TestWebServiceRSA.Application+ctor>b__4>d__6+<MoveNext>0600002b
//no implementation for System.Tuple`4[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLPre,System.Int64,System.String,System.String] f80c9c70-a297-37ce-b67d-52afe3b94259
//script: error JSC1000: No implementation found for this native method, please implement [System.Tuple`4.get_Item1()]