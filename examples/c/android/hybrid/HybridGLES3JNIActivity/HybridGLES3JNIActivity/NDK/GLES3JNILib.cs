using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(HybridGLES3JNIActivity.NDK.GLES3JNILib))]

// .so export needs to prefox java methods by Java.
[assembly: ScriptNamespaceRename(NativeNamespaceName = "HybridGLES3JNIActivity.NDK", VirtualNamespaceName = "Java.HybridGLES3JNIActivity.NDK")]
//[assembly: ScriptNamespaceRename(NativeNamespaceName = "HybridGLES3JNIActivity.NDK", VirtualNamespaceName = "Java.*")]


namespace HybridGLES3JNIActivity.NDK
{
    // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\GLES3JNILib.cs

    // LibraryImport
    static class GLES3JNILib
    {

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201506/20150625

        // infer IsPInvoke from ndk?
        [Script(IsPInvoke = true)]
        public static void init() { throw null; }

        //[javac] W:\src\Java\HybridGLES3JNIActivity\NDK\GLES3JNILib.java:24: error: cannot find symbol
        //[javac]     public static  void init(JNIEnv env, jobject obj)
        //[javac]                              ^

        // lets have jsc make ndk methods dissapear from java side..

        static void init(JNIEnv env, jobject obj)
        {




        }


        [Script(IsPInvoke = true)]
        public static void resize(int width, int height) { throw null; }

        [Script(IsPInvoke = true)]
        public static void step() { throw null; }
    }
}
