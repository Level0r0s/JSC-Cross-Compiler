using java.math;
using ScriptCoreLib;

namespace java.security.interfaces
{
    // http://developer.android.com/reference/java/security/interfaces/RSAPrivateKey.html
    [Script(IsNative = true)]
    public interface RSAPrivateKey : PrivateKey, RSAKey
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs
        BigInteger getPrivateExponent();

    }
}
