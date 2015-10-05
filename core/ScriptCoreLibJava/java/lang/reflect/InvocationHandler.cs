using ScriptCoreLib;

namespace java.lang.reflect
{
    // https://developer.android.com/reference/java/lang/reflect/InvocationHandler.html
    [Script(IsNative = true)]
    public interface InvocationHandler
    {
        object invoke(object proxy, Method method, object[] args);
    }

}
