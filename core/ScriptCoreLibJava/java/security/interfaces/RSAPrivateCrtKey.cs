using java.math;
using ScriptCoreLib;

namespace java.security.interfaces
{
    // http://developer.android.com/reference/java/security/interfaces/RSAPrivateKey.html
    [Script(IsNative = true)]
    public interface RSAPrivateCrtKey:RSAPrivateKey , PrivateKey, RSAKey
    {
        BigInteger getCrtCoefficient();
        BigInteger getPrimeExponentP();
        BigInteger getPrimeExponentQ();
        BigInteger getPrimeP();
        BigInteger getPrimeQ();
        BigInteger getPublicExponent();
    }
}
