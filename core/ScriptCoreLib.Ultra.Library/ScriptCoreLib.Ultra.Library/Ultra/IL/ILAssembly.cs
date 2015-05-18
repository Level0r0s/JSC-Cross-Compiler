using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.IDL;

namespace ScriptCoreLib.Ultra.IL
{
    public class ILAssembly
    {
        public readonly List<ILAssemblyExtern> AssemblyExternList = new List<ILAssemblyExtern>();

        public readonly List<ILAssemblyMethod> Methods = new List<ILAssemblyMethod>();
    }

    public class ILAssemblyMethod
    {
        public IDLParserToken Token;

        // .method private hidebysig static void modopt([mscorlib]System.Runtime.CompilerServices.CallConvCdecl) 
        //__AssemblyLoad() cil managed

        public bool IsPrivate;
        public bool IsStatic;
        
        // X:\jsc.svn\examples\c\Test\TestConsoleWriteLine\TestConsoleWriteLine\Program.cs
        public bool IsUnmanagedExport;

        public IDLParserToken NameToken;
        public IDLParserToken ParameterStartToken;
        public IDLParserToken BodyStartToken;

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/08-cx/20150518
        // implicitly should filter out private methods
    }
}
