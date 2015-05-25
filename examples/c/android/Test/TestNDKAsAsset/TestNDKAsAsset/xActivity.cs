using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[assembly: Script()]
//[assembly: ScriptCoreLib.Shared.ScriptResourcesAttribute("libs/armeabi-v7a")]
[assembly: ScriptCoreLib.Shared.ScriptResourcesAttribute("libs/armeabi_v7a")]

namespace TestNDKAsAsset
{
    // [armeabi-v7a] Install        : libTestNDKAsAsset.so => libs/armeabi-v7a/libTestNDKAsAsset.so
    // "X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\libs\armeabi-v7a\libTestNDKAsAsset.so"

    public static partial class xActivity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525

        // ConfigurationCreateNuGetPackage 

        // Embedded Resource
        // libs\armeabi-v7a\libTestNDKAsAsset.so

        static xActivity()
        {
            // if we name the asset, will merge rewriter keep it around?
            //var lib = @"libs\armeabi-v7a\libTestNDKAsAsset.so";
            //var lib = @"libs/armeabi-v7a/libTestNDKAsAsset.so";
            var lib = @"libs/armeabi_v7a/libTestNDKAsAsset.so";

            // visual studio mangles the name?

            // 2015 RC for java
            //[javac] W:\src\__AnonymousTypes__AndroidNDKNugetExperiment_AndroidActivity\__f__AnonymousType_34__1_0_1.java:34: error: reference to Format is ambiguous, both method Format(String,Object,Object) in __String and method Format(__IFormatProvider,String,Object[]) in __String match
            //[javac]         return __String.Format(null, "{{ lib = {0} }}", objectArray2);
            //Console.WriteLine(new { lib });

            Console.WriteLine("lib: " + lib);
            Console.WriteLine("loadLibrary: TestNDKAsAsset");

            java.lang.JavaSystem.loadLibrary("TestNDKAsAsset");
        }

        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }


        // "X:\jsc.svn\examples\java\android\AndroidNDKNugetExperiment\AndroidNDKNugetExperiment\bin\Debug\staging\clr\AndroidNDKNugetExperiment.AndroidActivity.dll"
    }
}
