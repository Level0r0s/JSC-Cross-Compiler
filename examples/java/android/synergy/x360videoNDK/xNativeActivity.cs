using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos
// scriptcorelibA
[assembly: ScriptCoreLib.Script()]
[assembly: ScriptCoreLib.ScriptTypeFilter(ScriptCoreLib.ScriptType.C)]


namespace x360videoNDK
{
    [Script]
    public class xNativeActivity
    {
        // Z:\jsc.svn\examples\java\android\synergy\AndroidBrowserVRNDK\xNativeActivity.cs

        // add tools, staging

        // add ScriptCoreLibAndroidNDK
        // add the test function to test jsc c and lets run it on android.



        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_x360video_Activities_xMarshal_stringFromJNI(JNIEnv env, jobject thiz, jobject args)
        {
            ConsoleExtensions.trace("enter Java_x360video_Activities_xMarshal_stringFromJNI");

            // do we have a console yet?
            //Console.WriteLine("enter Java_AndroidBrowserVRNDK_Activities_xMarshal_stringFromJNI");


            var n = env.NewStringUTF;

            //// if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n(env, "hello from Java_x360video_Activities_xMarshal_stringFromJNI. yay");

            return v;
            // ConfigurationCreateNuGetPackage.cs
        }
    }
}
