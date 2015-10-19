using java.security.spec;
using ScriptCoreLib;

namespace java.security
{
    // Z:\jsc.svn\core\ScriptCoreLibJava\java\security\KeyFactory.cs
    // http://developer.android.com/reference/java/security/KeyFactory.html
    [Script(IsNative = true)]
    public class KeyFactory
    {
        public PublicKey generatePublic(KeySpec keySpec)
        {
            return default(PublicKey);
        }

        public static KeyFactory getInstance(string algorithm)
        { 
            return default(KeyFactory);
        }

    }
}
