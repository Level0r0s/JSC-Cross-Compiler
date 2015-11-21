using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsa.cs
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Security.Cryptography/RSAParameters.cs

    [Script(Implements = typeof(global::System.Security.Cryptography.RSAParameters))]
    public sealed class __RSAParameters
    {
        // http://tech.slashdot.org/story/15/11/19/1332244/manhattan-da-pressures-google-and-apple-to-kill-zero-knowledge-encryption

        // Z:\jsc.svn\examples\javascript\crypto\ClientVerifiedWebServiceAuthority\ClientVerifiedWebServiceAuthority\Application.cs

        // https://weakdh.org/imperfect-forward-secrecy-ccs15.pdf
        // https://freedom-to-tinker.com/blog/haldermanheninger/how-is-nsa-breaking-so-much-crypto/

        // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProvider\JVMCLRRSACryptoServiceProvider\Program.cs
        // Z:\jsc.svn\examples\javascript\crypto\test\TestWebServiceRSA\TestWebServiceRSA\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\forms\Test\TestRSACryptoServiceProvider\TestRSACryptoServiceProvider\ApplicationControl.cs

        public byte[] D;
        public byte[] DP;
        public byte[] DQ;
        public byte[] Exponent;
        public byte[] InverseQ;
        public byte[] Modulus;
        public byte[] P;
        public byte[] Q;






    }
}
