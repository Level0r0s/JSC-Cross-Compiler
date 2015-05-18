using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.chromium.mojo.system
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518/testchromeasasset

    //  U:\chromium\src\third_party\mojo\src\mojo\public\java\system\src\org\chromium\mojo\system\InvalidHandle.java"
    class InvalidHandle : UntypedHandle
    {
        public InvalidHandle pass()
        {
            return this;
        }

        Handle Handle.pass()
        {
            throw new NotImplementedException();
        }

        UntypedHandle UntypedHandle.pass()
        {
            throw new NotImplementedException();
        }
    }


    // U:\chromium\src\third_party\mojo\src\mojo\public\java\system\src\org\chromium\mojo\system\UntypedHandle.java
    interface UntypedHandle : Handle
    {
        UntypedHandle pass();
    }

    // U:\chromium\src\third_party\mojo\src\mojo\public\java\system\src\org\chromium\mojo\system\Handle.java
    interface Handle
    {
        Handle pass();
    }

}

//14ac:01:04:1b[001a] 0003 TestInvalidHandle.AssetsLibrary.merge create org.chromium.mojo.system.InvalidHandle
//System.TypeLoadException: Method 'pass' in type 'org.chromium.mojo.system.InvalidHandle' from assembly 'TestInvalidHandle.AssetsLibrary.merge, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.
//  at System.Reflection.Emit.TypeBuilder.TermCreateClass(RuntimeModule module, Int32 tk, ObjectHandleOnStack type)
//   at System.Reflection.Emit.TypeBuilder.CreateTypeNoLock()
//   at System.Reflection.Emit.TypeBuilder.CreateType()
//   at jsc.meta.Library.CreateToJARImportNatives.<>c__DisplayClass16_28.<InternalImplementation>b__106(TypeBuilder SourceTypeBuilder) in X:\jsc.internal.git\compiler\jsc.internal\jsc.internal\meta\Library\CreateToJARImportNatives.cs:line 2687
//use DiagnosticsAttachDebuggerBeforeType for diagnostics..
//{ SourceTypeName = org.chromium.mojo.system.InvalidHandle }
//{ SourceTypeBuilder = org.chromium.mojo.system.InvalidHandle }
//{ BaseType = System.Object }
//{ exMethodName = pass }
//{ source = pass() : org.chromium.mojo.system.InvalidHandle, target = pass() : org.chromium.mojo.system.Handle }
//{ source = pass() : org.chromium.mojo.system.UntypedHandle, target = pass() : org.chromium.mojo.system.Handle }
//System.TypeLoadException: Method 'pass' in type 'org.chromium.mojo.system.InvalidHandle' from assembly 'TestInvalidHandle.AssetsLibrary.merge, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null' does not have an implementation.