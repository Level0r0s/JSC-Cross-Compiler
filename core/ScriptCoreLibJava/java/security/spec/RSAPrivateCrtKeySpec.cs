using java.math;
using java.security.spec;
using ScriptCoreLib;

namespace java.security.spec
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/spec/RSAPrivateCrtKeySpec.html
    // http://developer.android.com/reference/java/security/spec/RSAPrivateCrtKeySpec.html
    [Script(IsNative = true)]
    public class RSAPrivateCrtKeySpec : RSAPrivateKeySpec
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151103/rsaparameters
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140829

        public RSAPrivateCrtKeySpec(BigInteger modulus,
                     BigInteger publicExponent,
                     BigInteger privateExponent,
                     BigInteger primeP,
                     BigInteger primeQ,
                     BigInteger primeExponentP,
                     BigInteger primeExponentQ,
                     BigInteger crtCoefficient) : base(modulus, privateExponent)
        {


        }
    }
}
