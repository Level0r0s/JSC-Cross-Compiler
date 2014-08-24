﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net.Security
{
    // http://referencesource.microsoft.com/#System/net/System/Net/SecureProtocols/SslStream.cs
    // https://github.com/mono/mono/blob/effa4c07ba850bedbe1ff54b2a5df281c058ebcb/mcs/class/System/System.Net.Security/SslStream.cs

    [Script(Implements = typeof(global::System.Net.Security.SslStream))]
    internal class __SslStream : __AuthenticatedStream
    {
        // how would CLR or Android send out a custom stream and have it signed?

        public virtual X509Certificate LocalCertificate { get; set; }
        public virtual X509Certificate RemoteCertificate { get; set; }

        // every web request shall expect a callback against its new key
        public override bool IsMutuallyAuthenticated { get; set; }

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\SubtleCrypto.cs

        // X:\opensource\codeplex\webserver\HttpServer\SecureHttpContext.cs
        // when can we use it?
        // when can we do SSL web servers?

        // what about MAC stream?
        // X:\opensource\codeplex\webserver\HttpServer\SecureHttpContext.cs

        // https://support.globalsign.com/customer/portal/articles/1216536-securing-a-public-ip-address---ssl-certificates
        // http://stackoverflow.com/questions/9726802/ssl-socket-between-net-and-java-with-client-authentication
        // X:\opensource\googlecode\dotnetasyncsocket\AsyncSocket\AsyncSocket.cs

    }
}
