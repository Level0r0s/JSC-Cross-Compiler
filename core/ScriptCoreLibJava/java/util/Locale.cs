using ScriptCoreLib;

namespace java.util
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/util/Locale.html
    // http://developer.android.com/reference/java/util/Locale.html
    [Script(IsNative=true)]
    public sealed class Locale
    {
        public string getLanguage() { throw null; }
        public static Locale getDefault() { throw null; }
        public string getDisplayLanguage (){throw null;}
        public Locale(string lang)
        {

        }
    }
}
