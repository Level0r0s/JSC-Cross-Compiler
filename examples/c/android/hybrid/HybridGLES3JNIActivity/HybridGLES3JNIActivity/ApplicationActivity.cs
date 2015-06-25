using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using android.opengl;
using android.content;
using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using HybridGLES3JNIActivity.NDK;

namespace HybridGLES3JNIActivity.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // "X:\jsc.svn\examples\c\android\hybrid\HybridGLES3JNIActivity\HybridGLES3JNIActivity\bin\Debug\staging\apk\bin\HybridGLES3JNIActivity.Activities-debug.apk"

        // "X:\util\android-ndk-r10e\samples\gles3jni\src\com\android\gles3jni\GLES3JNIActivity.java"
        // can we compile .c in the same project already?

        GLES3JNIView mView;

        protected override void onCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("HybridGLES3JNIActivity onCreate");

            base.onCreate(savedInstanceState);
            mView = new GLES3JNIView(getApplication());
            setContentView(mView);
        }

        protected override void onPause()
        {
            Console.WriteLine("HybridGLES3JNIActivity onPause");

            base.onPause();
            mView.onPause();
        }

        protected override void onResume()
        {
            Console.WriteLine("HybridGLES3JNIActivity onResume");

            base.onResume();
            mView.onResume();
        }
    }

    class GLES3JNIView : GLSurfaceView
    {
        public GLES3JNIView(Context context)
            : base(context)
        {
            // Pick an EGLConfig with RGB8 color, 16-bit depth, no stencil,
            // supporting OpenGL ES 2.0 or later backwards-compatible versions.
            setEGLConfigChooser(8, 8, 8, 0, 16, 0);
            setEGLContextClientVersion(2);
            setRenderer(new Renderer());
        }

        // http://developer.android.com/reference/android/opengl/GLSurfaceView.Renderer.html
        //[javac] W:\src\HybridGLES3JNIActivity\Activities\GLES3JNIView_Renderer.java:13: error: package GLSurfaceView does not exist
        //[javac] public class GLES3JNIView_Renderer implements GLSurfaceView.Renderer
        //[javac]                                                            ^
        //[javac] W:\src\HybridGLES3JNIActivity\Activities\GLES3JNIView.java:22: error: method setRenderer in class GLSurfaceView cannot be applied to given types;
        //[javac]         this.setRenderer(new GLES3JNIView_Renderer());

        class Renderer : GLSurfaceView.Renderer
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150625
            GLSurfaceView import0;

            public void onDrawFrame(GL10 gl)
            {
                //Console.WriteLine("HybridGLES3JNIActivity onDrawFrame, step");
                GLES3JNILib.step();
            }

            public void onSurfaceChanged(GL10 gl, int width, int height)
            {
                Console.WriteLine("HybridGLES3JNIActivity onSurfaceChanged, resize");
                GLES3JNILib.resize(width, height);
            }

            public void onSurfaceCreated(GL10 gl, javax.microedition.khronos.egl.EGLConfig config)
            {
                Console.WriteLine("HybridGLES3JNIActivity onSurfaceCreated, init");
                GLES3JNILib.init();
            }
        }
    }


}
