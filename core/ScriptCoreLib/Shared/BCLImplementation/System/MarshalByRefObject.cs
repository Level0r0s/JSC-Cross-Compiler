using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/marshalbyrefobject.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/MarshalByRefObject.cs
    // https://github.com/dot42/api/blob/master/System/MarshalByRefObject.cs

    [Script(Implements = typeof(global::System.MarshalByRefObject))]
    public class __MarshalByRefObject
    {
        // http://en.wikipedia.org/wiki/.NET_Remoting
        // .NET Remoting is a Microsoft application programming interface (API) for interprocess communication released in 2002 with the 1.0 version of .NET Framework. It is one in a series of Microsoft technologies that began in 1990 with the first version of Object Linking and Embedding (OLE) for 16-bit Windows. I

        // https://msdn.microsoft.com/et-ee/library/kwdt6w2k(v=vs.100).aspx

        // yikes. .net remoting has been obseleted already?

        // This topic is specific to a legacy technology that is retained for backward compatibility with existing applications and is not recommended for new development. Distributed applications should now be developed using the Windows Communication Foundation (WCF).
        //.NET remoting enables you to build widely distributed applications easily, whether the application components are all on one computer or spread out across the entire world.You can build client applications that use objects in other processes on the same computer or on any other computer that is reachable over its network. You can also use .NET remoting to communicate with other application domains in the same process. (For details about programming application domains, see Programming with Application Domains.)

        // https://github.com/dotnet/coreclr/blob/master/src/vm/remoting.h
        // https://github.com/dotnet/coreclr/blob/master/src/vm/remoting.cpp

    }
}
