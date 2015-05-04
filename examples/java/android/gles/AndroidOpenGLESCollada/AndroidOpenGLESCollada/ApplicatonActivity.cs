
using System;
using System.ComponentModel;
using android.app;
using android.content;
using android.content.pm;
using android.opengl;
using android.os;
using android.provider;
using android.util;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using java.nio;
//using javax.microedition.khronos.egl;
using javax.microedition.khronos.opengles;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.WebGL;
using java.io;

namespace AndroidOpenGLESCollada.Activities
{
    using gl = WebGLRenderingContext;
    using opengl = GLES20;

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]

    // unavailable for android 2.4
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class AndroidOpenGLESCollada : ckt.projects.acl.AndroidColladaLoader
    {

        //protected override void onCreate(global::android.os.Bundle savedInstanceState)
        public override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            System.Console.WriteLine("AndroidOpenGLESCollada onCreate");

            // a blue blob.

            //W/System.err( 3385): java.io.FileNotFoundException: model.dae
            //W/System.err( 3385):    at android.content.res.AssetManager.openAsset(Native Method)
            //W/System.err( 3385):    at android.content.res.AssetManager.open(AssetManager.java:330)
            //W/System.err( 3385):    at android.content.res.AssetManager.open(AssetManager.java:304)
            //W/System.err( 3385):    at ckt.projects.acl.AndroidColladaLoader.onSurfaceCreated(AndroidColladaLoader.java:89)
            //W/System.err( 3385):    at android.opengl.GLSurfaceView$GLThread.guardedRun(GLSurfaceView.java:1539)
            //W/System.err( 3385):    at android.opengl.GLSurfaceView$GLThread.run(GLSurfaceView.java:1278)

//W/System.err( 6519): java.lang.NullPointerException: Attempt to get length of null array
//W/System.err( 6519):    at ckt.projects.acl.ColladaObject.<init>(ColladaObject.java:19)
//W/System.err( 6519):    at ckt.projects.acl.ColladaHandler.parseFile(ColladaHandler.java:90)
//W/System.err( 6519):    at ckt.projects.acl.AndroidColladaLoader.onSurfaceCreated(AndroidColladaLoader.java:89)
//W/System.err( 6519):    at android.opengl.GLSurfaceView$GLThread.guardedRun(GLSurfaceView.java:1539)
//W/System.err( 6519):    at android.opengl.GLSurfaceView$GLThread.run(GLSurfaceView.java:1278)

//            W/System.err( 7541): java.lang.NullPointerException: Attempt to get length of null array
//W/System.err( 7541):    at ckt.projects.acl.ColladaObject.<init>(ColladaObject.java:19)
//W/System.err( 7541):    at ckt.projects.acl.ColladaHandler.parseFile(ColladaHandler.java:90)
//W/System.err( 7541):    at ckt.projects.acl.AndroidColladaLoader.onSurfaceCreated(AndroidColladaLoader.java:89)
//W/System.err( 7541):    at android.opengl.GLSurfaceView$GLThread.guardedRun(GLSurfaceView.java:1539)
//W/System.err( 7541):    at android.opengl.GLSurfaceView$GLThread.run(GLSurfaceView.java:1278)
        }

    }








}
