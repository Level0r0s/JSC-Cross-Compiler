using android.app;
using android.view;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150619/ovrvrcubeworldsurfaceviewx

namespace com.oculus.gles3jni
{
    // inline C ?
    public static class GLES3JNILib
    {
        #region Activity lifecycle
        [Script(IsPInvoke = true)]
        public static long onCreate(Activity obj) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onStart(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onResume(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onPause(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onStop(long handle) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onDestroy(long handle) { throw null; }
        #endregion

        #region Surface lifecycle
        [Script(IsPInvoke = true)]
        public static void onSurfaceCreated(long handle, Surface s) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onSurfaceChanged(long handle, Surface s) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onSurfaceDestroyed(long handle) { throw null; }
        #endregion

        #region Input
        [Script(IsPInvoke = true)]
        public static void onKeyEvent(long handle, int keyCode, int action) { throw null; }
        [Script(IsPInvoke = true)]
        public static void onTouchEvent(long handle, int action, float x, float y) { throw null; }
        #endregion
    }
}
