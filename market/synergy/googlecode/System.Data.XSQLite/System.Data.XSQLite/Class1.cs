using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSystem.Data.XSQLite
{
    public class Class1
    {
        // roslyn!
        //{ FullName = Community.CsharpSqlite.Sqlite3, Name = .cctor }
        //22dc:02:01 0026:000c System.Data.XSQLite create Community.CsharpSqlite::Community.CsharpSqlite.Sqlite3

        //error at CopyType:
        //         * Illegal one-byte branch at position: 35. Requested branch was: 128.
        //         * Community.CsharpSqlite.Sqlite3 02000002

        //         * IllegalBranchAt 00000023

        //         * RequestedBranch 128

        // tested by ?

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150911/appengine
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140321


        //rewriting... primary types: 329

        //0a8c:02:01 RewriteToAssembly error: System.InvalidOperationException: System.Data.XSQLite.Class1 ---> System.ArgumentOutOfRangeException: Length cannot be less than zero.
        //Parameter name: length
        //   at System.String.InternalSubStringWithChecks(Int32 startIndex, Int32 length, Boolean fAlwaysCopy)
        //   at System.String.Substring(Int32 startIndex, Int32 length)
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.NamespaceRenameInstructions.get_From()
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.InternalFullNameFixup(String n)
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.FullNameFixup(String n, Type ContextType)
        //   at jsc.meta.Commands.Rewrite.RewriteToAssembly.CopyTypeDefinition.DefineType(TypeBuilder _DeclaringType, String TypeName, Type BaseType, TypeAttributes TypeAttributes)

    }
}
