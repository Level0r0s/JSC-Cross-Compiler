using ScriptCoreLib;

namespace java.security
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/Signature.html
    // http://developer.android.com/reference/java/security/Signature.html
    [Script(IsNative = true)]
    public class Signature : SignatureSpi
    {
        // http://www.java2s.com/Tutorial/Java/0490__Security/SimpleDigitalSignatureExample.htm
        // Z:\jsc.svn\examples\javascript\crypto\test\TestWebServiceRSA\TestWebServiceRSA\Program.cs

        public static Signature getInstance(string algorithm) { throw null; }

        public void initSign(PrivateKey privateKey) { throw null; }
        public void initVerify(PublicKey publicKey) { throw null; }

        public void update(sbyte[] data) { throw null; }

        public sbyte[] sign() { throw null; }

        public bool verify(sbyte[] signature) { throw null; }
    }
}
