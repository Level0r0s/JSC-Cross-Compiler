using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


//[assembly: Obfuscation(Feature = "script")]
[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(TestNDKAsAsset.xNativeActivity))]
[assembly: ScriptTypeFilter(ScriptType.Java, typeof(TestNDKAsAsset.xActivity))]


namespace TestNDKAsAsset
{
    using ScriptCoreLib;
    using ScriptCoreLibNative.SystemHeaders;

    [Obfuscation(StripAfterObfuscation = true)]
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150518
        // X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\staging\jni\Application.mk
        //├───arm64-v8a
        //├───armeabi
        //├───armeabi-v7a
        //├───mips
        //├───mips64
        //├───x86
        //└───x86_64

        // first we have to compile our C dll


        static void Main(string[] args)
        {
        }
    }

    //    Updated and renamed default.properties to project.properties
    //    Updated local.properties
    //    No project name specified, using project folder name 'staging'.
    //If you wish to change it, edit the first line of build.xml.
    //Added file X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\build.xml
    //Added file X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\proguard-project.txt

    // to be used by nuget user
    partial class xActivity
    {

    }


    // [armeabi-v7a] Install        : libTestNDKAsAsset.so => libs/armeabi-v7a/libTestNDKAsAsset.so
    // "X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\libs\armeabi-v7a\libTestNDKAsAsset.so"
    [Obfuscation(StripAfterObfuscation = true)]

    // x:\jsc.svn\examples\java\android\androidndknugetexperiment\androidndknugetexperiment\applicationactivity.cs
    [Script]
    partial class xNativeActivity
    {
        // Java_TestNDKAsAsset_xActivity_stringFromJNI

        [Script(NoDecoration = true)]
        // JVM load the .so and calls this native function
        static jstring Java_TestNDKAsAsset_xActivity_stringFromJNI(
            // what would we be able to do inspecting the runtime?
            ref JNIEnv env,
            jobject thiz)
        {

            var n = env.NewStringUTF;

            // look almost the same file!

            // if we change our NDK code, will nuget packaing work on the background, and also upgrade running apps?
            var v = n(ref env, "from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");

            return v;


            // ConfigurationCreateNuGetPackage.cs
        }
    }
}
