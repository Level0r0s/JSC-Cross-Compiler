using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using System.Security.Cryptography;

namespace ScriptCoreLib.Shared
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151123/ubuntumidexperiment
    // Z:\jsc.svn\examples\javascript\crypto\test\TestWebServiceRSA\TestWebServiceRSA\ApplicationWebService.cs

    //public struct VerifiableString
    public sealed class VerifiableString
    {
        public string value;

        public byte[] signature;

        public override string ToString()
        {
            //{ Message = , StackTrace = java.lang.NullPointerException
            //        at UbuntuMIDExperiment.ApplicationWebService__i__d.Internal.Extensions.StringExtensions.ToHexString(StringExtensions.java:40)
            //        at UbuntuMIDExperiment.ApplicationWebService__i__d.Internal.Shared.VerifiableString.toString(VerifiableString.java:26)


            if (signature == null)
                return new { value }.ToString();

            return new { value, signature = signature.ToHexString() }.ToString();
        }

        public static implicit operator string(VerifiableString e)
        {
            // Z:\jsc.svn\examples\javascript\crypto\WebServiceAuthorityExperiment\WebServiceAuthorityExperiment\Application.cs

            return e.value;
        }
    }

    public static class VerifiableStringExtensions
    {
        // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuMIDExperiment\Application.cs

        public static VerifiableString Sign(this VerifiableString e, RSAParameters rsa)
        {
            e.signature = new RSACryptoStream(rsa).SignString(e.value);

            return e;
        }

        public static bool Verify(this VerifiableString e, RSAParameters rsa)
        {
            if (e == null)
                return false;

            // Verify { identity = { value =  } }

            if (e.value == null)
                return false;

            return new RSACryptoStream(rsa).VerifyString(e.value, e.signature);
        }
    }
}
