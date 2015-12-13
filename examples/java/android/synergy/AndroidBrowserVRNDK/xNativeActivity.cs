using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: ScriptCoreLib.Script()]
[assembly: ScriptCoreLib.ScriptTypeFilter(ScriptCoreLib.ScriptType.C)]


namespace AndroidBrowserVRNDK
{
    [Script]
    public class xNativeActivity
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151213/androidbrowserndk

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_AndroidBrowserVRNDK_Activities_xMarshal_stringFromJNI(
            // what would we be able to do inspecting the runtime?
             JNIEnv env,
            jobject thiz,
            jobject args
            )
        {
            ConsoleExtensions.trace("enter Java_AndroidBrowserVRNDK_Activities_xMarshal_stringFromJNI");

            // do we have a console yet?
            //Console.WriteLine("enter Java_AndroidBrowserVRNDK_Activities_xMarshal_stringFromJNI");


            var n = env.NewStringUTF;

            //// if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n(env, "hello from Java_AndroidBrowserVRNDK_Activities_xMarshal_stringFromJNI. yay");

            return v;
            // ConfigurationCreateNuGetPackage.cs
        }
    }
}
