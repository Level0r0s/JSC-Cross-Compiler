using java.math;
using java.security.spec;
using ScriptCoreLib;

namespace java.security.spec
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/spec/RSAPrivateKeySpec.html
    // http://developer.android.com/reference/java/security/spec/RSAPrivateKeySpec.html
    [Script(IsNative = true)]
    public class RSAPrivateKeySpec : KeySpec
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140829

        public RSAPrivateKeySpec(BigInteger modulus, BigInteger privateExponent)
        {


        }
    }
}
