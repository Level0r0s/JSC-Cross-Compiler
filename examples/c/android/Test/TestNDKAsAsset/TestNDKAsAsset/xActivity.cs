using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[assembly: Script()]

namespace TestNDKAsAsset
{
    // [armeabi-v7a] Install        : libTestNDKAsAsset.so => libs/armeabi-v7a/libTestNDKAsAsset.so
    // "X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\libs\armeabi-v7a\libTestNDKAsAsset.so"

    public static partial class xActivity
    {
        // Embedded Resource
        // libs\armeabi-v7a\libTestNDKAsAsset.so

        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }
    }
}
