using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if JNIEnv

[assembly: Script(IsScriptLibrary = true)]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(HybridGLES3JNIActivity.NDK.GLES3JNILib))]

// .so export needs to prefox java methods by Java.
[assembly: ScriptNamespaceRename(NativeNamespaceName = "HybridGLES3JNIActivity.NDK", VirtualNamespaceName = "Java.HybridGLES3JNIActivity.NDK")]
//[assembly: ScriptNamespaceRename(NativeNamespaceName = "HybridGLES3JNIActivity.NDK", VirtualNamespaceName = "Java.*")]
#endif


namespace HybridGLES3JNIActivity.NDK
{
    // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\GLES3JNILib.cs
    // "X:\util\android-ndk-r10e\samples\gles3jni\src\com\android\gles3jni\GLES3JNIActivity.java"
    // "X:\util\android-ndk-r10e\samples\gles3jni\jni\gles3jni.cpp"

    static class GLES3JNILib
    {

#if JNIEnv
        private static unsafe RendererES3 g_renderer;

//        E/AndroidRuntime( 1880): java.lang.UnsatisfiedLinkError: No implementation found for void HybridGLES3JNIActivity.NDK.GLES3JNILib.init() (tried
//E/AndroidRuntime( 1880):        at HybridGLES3JNIActivity.NDK.GLES3JNILib.init(Native Method)
//E/AndroidRuntime( 1880):        at HybridGLES3JNIActivity.Activities.GLES3JNIView_Renderer.onSurfaceCreated(GLES3JNIView_Renderer.java:39)
//E/AndroidRuntime( 1880):        at android.opengl.GLSurfaceView$GLThread.guardedRun(GLSurfaceView.java:1539)
//E/AndroidRuntime( 1880):        at android.opengl.GLSurfaceView$GLThread.run(GLSurfaceView.java:1278)

     

        static void init(JNIEnv env, jobject obj)
        {
            ConsoleExtensions.tracei("enter init, call createES3Renderer");

            g_renderer = RendererES3.createES3Renderer();
            // here we are storing a pointer in native heap. why is oculus sending it back to java world??

            //resize =
        }

        public static void resize(JNIEnv env, jobject obj, int width, int height)
        {
            if (g_renderer != null)
            {
                g_renderer.resize(width, height);
            }
        }

        public static void step(JNIEnv env, jobject obj, int width, int height)
        {
            if (g_renderer != null)
            {
                g_renderer.render();
            }
        }



#else
           static GLES3JNILib()
        {
            global::java.lang.System.loadLibrary("main");
        }
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201506/20150625

        // infer IsPInvoke from ndk?
        [Script(IsPInvoke = true)]
        public static void init() { throw null; }



        [Script(IsPInvoke = true)]
        public static void resize(int width, int height) { throw null; }

        [Script(IsPInvoke = true)]
        public static void step() { throw null; }
#endif





    }
}
