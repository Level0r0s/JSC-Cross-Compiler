using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.view;

namespace android.media
{
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/media/java/android/media/MediaPlayer.java
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/media/jni/android_media_MediaPlayer.cpp

    [Script(IsNative = true)]
    public class MediaPlayer
    {


        // jint JNI_OnLoad(JavaVM* vm, void* /* reserved */)

        //  if (register_android_media_MediaPlayer(env) < 0) {
        //     return AndroidRuntime::registerNativeMethods(env,
                //"android/media/MediaPlayer", gMethods, NELEM(gMethods));
        // static const JNINativeMethod gMethods[] = {
        // {"_setVideoSurface",    "(Landroid/view/Surface;)V",        (void *)android_media_MediaPlayer_setVideoSurface},
        // private native void _setVideoSurface(Surface surface);






        // whem will webview have this method?
        // Z:\jsc.svn\examples\java\android\synergy\x360video\ApplicationActivity.cs
        public void setSurface(Surface surface) { }



        public static MediaPlayer create(Context c, int id)
        {
            return null;
        }

        public void start()
        {
        }
    }
}
