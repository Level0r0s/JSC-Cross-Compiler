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
        // "X:\util\android-ndk-r10e\samples\gles3jni\src\com\android\gles3jni\GLES3JNIActivity.java"
        // can we compile .c in the same project already?

        GLES3JNIView mView;

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);
            //mView = new GLES3JNIView(getApplication());
            setContentView(mView);
        }

        protected override void onPause()
        {
            base.onPause();
            //mView.onPause();
        }

        protected override void onResume()
        {
            base.onResume();
            //mView.onResume();
        }
    }

    class GLES3JNIView : GLSurfaceView
    {
        public GLES3JNIView(Context context) : base(context)
        {
            // Pick an EGLConfig with RGB8 color, 16-bit depth, no stencil,
            // supporting OpenGL ES 2.0 or later backwards-compatible versions.
            setEGLConfigChooser(8, 8, 8, 0, 16, 0);
            setEGLContextClientVersion(2);
            setRenderer(new Renderer());
        }

        class Renderer : GLSurfaceView.Renderer
        {
            public void onDrawFrame(GL10 gl)
            {
                // stepping into NDK
                GLES3JNILib.step();
            }

            public void onSurfaceChanged(GL10 gl, int width, int height)
            {
                GLES3JNILib.resize(width, height);
            }

            public void onSurfaceCreated(GL10 gl, javax.microedition.khronos.egl.EGLConfig config)
            {
                GLES3JNILib.init();
            }
        }
    }

    
}
