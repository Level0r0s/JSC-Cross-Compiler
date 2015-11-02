using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Security
{
    // http://referencesource.microsoft.com/#System/net/System/Net/SecureProtocols/_SslStream.cs
    // http://msdn.microsoft.com/en-us/library/system.net.security.sslstream.aspx
    // http://referencesource.microsoft.com/#System/net/System/Net/SecureProtocols/SslStream.cs
    // https://github.com/mono/mono/tree/master/mcs/class/System/System.Net.Security/SslStream.cs

    // Z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\Security\SslStream.cs

    [Script(Implements = typeof(global::System.Net.Security.SslStream))]
    public class __SslStream : __AuthenticatedStream
    {
        //  if (ServicePointManager.DisableStrongCrypto) ??

        //if (issuers == null)
        // issuers = GetIssuers();

        // http://referencesource.microsoft.com/#System/net/System/Net/_SecureChannel.cs

    }
}
