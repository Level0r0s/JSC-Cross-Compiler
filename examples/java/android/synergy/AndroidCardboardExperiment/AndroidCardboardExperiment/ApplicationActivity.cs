using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using com.google.vrtoolkit.cardboard;
using javax.microedition.khronos.egl;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using android.opengl;
using java.nio;
using android.content;
using android.content.pm;

//namespace AndroidCardboardExperiment.Activities

//xandroidCardboardExperiment

namespace xandroidcardboardcxperiment.xactivities
{
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151212/androidcardboardexperiment
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150815/cardboard

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "16")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "16")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity :
        
        // https://github.com/googlesamples/cardboard-java/blob/master/CardboardSample/libs/cardboard.jar
        // https://developers.google.com/cardboard/android/download

         com.google.vrtoolkit.cardboard.CardboardActivity,
        com.google.vrtoolkit.cardboard.CardboardView.StereoRenderer
    {


        // "x:\util\android-sdk-windows\platform-tools\adb.exe" devices
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  shell netcfg

        // should jsc remember last connected device and reconnect if disconnected?
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // restart helps.




        // https://github.com/googlesamples/cardboard-java/blob/master/CardboardSample/src/main/java/com/google/vrtoolkit/cardboard/samples/treasurehunt/MainActivity.java

        // http://www.engadget.com/2014/06/25/google-vr-cardboard/
        // https://github.com/gkortsaridis/GoogleCardboardPhotoSphere-VR-/blob/master/CardboardPhotoSphere/src/main/AndroidManifest.xml
        // https://github.com/pollux-/GoogleCardboardPhotoSphere-VR-

        //		D/HeadMountedDisplayManager( 3942): Cardboard screen parameters file not found: java.io.FileNotFoundException: /storage/emulated/0/Cardboard/phone_params: open failed: ENOENT(No such file or directory)
        //D/HeadMountedDisplayManager( 3942): Cardboard device parameters file not found: java.io.FileNotFoundException: /storage/emulated/0/Cardboard/current_device_params: open failed: ENOENT(No such file or directory)
        //D/HeadMountedDisplayManager( 3942): Bundled Cardboard device parameters not found: java.io.FileNotFoundException: Cardboard/current_device_params
        //I/art( 3942): Rejecting re-init on previously-failed class java.lang.Class<com.google.vrtoolkit.cardboard.proto.CardboardDevice$DeviceParams>

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150413
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\WebVR\VRDevice.cs

        private static float Z_NEAR = 0.1f;
        private static float Z_FAR = 100.0f;

        private static float CAMERA_Z = 0.01f;
        private static float TIME_DELTA = 0.3f;

        private static float YAW_LIMIT = 0.12f;
        private static float PITCH_LIMIT = 0.12f;

        private static int COORDS_PER_VERTEX = 3;

        // We keep the light always position just above the user.
        private static float[] LIGHT_POS_IN_WORLD_SPACE = new float[] { 0.0f, 2.0f, 0.0f, 1.0f };

        private float[] lightPosInEyeSpace = new float[4];

        private FloatBuffer floorVertices;
        private FloatBuffer floorColors;
        private FloatBuffer floorNormals;

        private FloatBuffer cubeVertices;
        private FloatBuffer cubeColors;
        private FloatBuffer cubeFoundColors;
        private FloatBuffer cubeNormals;

        private int cubeProgram;
        private int floorProgram;

        private int cubePositionParam;
        private int cubeNormalParam;
        private int cubeColorParam;
        private int cubeModelParam;
        private int cubeModelViewParam;
        private int cubeModelViewProjectionParam;
        private int cubeLightPosParam;

        private int floorPositionParam;
        private int floorNormalParam;
        private int floorColorParam;
        private int floorModelParam;
        private int floorModelViewParam;
        private int floorModelViewProjectionParam;
        private int floorLightPosParam;

        private float[] modelCube;
        private float[] camera;
        private float[] view;
        private float[] headView;
        private float[] modelViewProjection;
        private float[] modelView;
        private float[] modelFloor;

        private int score = 0;
        private float objectDistance = 12f;
        private float floorDepth = 20f;

        private Vibrator vibrator;
        private CardboardOverlayView overlayView;





        private static void checkGLError(String label)
        {
            int error = GLES20.glGetError();
            if (error != GLES20.GL_NO_ERROR)
            {
                throw new Exception(label + ": glError " + error);
            }
        }


        protected override void onCreate(Bundle savedInstanceState)
        {
            Console.WriteLine("enter AndroidCardboardExperiment onCreate");
            // https://github.com/googlesamples/cardboard-java/blob/master/CardboardSample/src/main/res/layout/common_ui.xml

            base.onCreate(savedInstanceState);

            this.setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_LANDSCAPE);

            //{ com.google.vrtoolkit.cardboard.FullscreenMode ref0; }

            var ll = new RelativeLayout(this);
            ll.setLayoutParams(new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.FILL_PARENT, RelativeLayout.LayoutParams.FILL_PARENT));


            this.setContentView(ll);

            var cardboardView = new com.google.vrtoolkit.cardboard.CardboardView(this).AttachTo(ll);
            cardboardView.setLayoutParams(new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.FILL_PARENT, RelativeLayout.LayoutParams.FILL_PARENT));
            cardboardView.setRenderer(this);
            setCardboardView(cardboardView);


            modelCube = new float[16];
            camera = new float[16];
            view = new float[16];
            modelViewProjection = new float[16];
            modelView = new float[16];
            modelFloor = new float[16];
            headView = new float[16];
            vibrator = (Vibrator)getSystemService(Context.VIBRATOR_SERVICE);


            overlayView = new CardboardOverlayView(this, null);
            overlayView.setLayoutParams(new RelativeLayout.LayoutParams(RelativeLayout.LayoutParams.FILL_PARENT, RelativeLayout.LayoutParams.FILL_PARENT));
            overlayView.show3DToast("Pull the magnet when you find an object.");

            Console.WriteLine("exit AndroidCardboardExperiment onCreate");
        }

        public void onRendererShutdown()
        {
            Console.WriteLine("AndroidCardboardExperiment onRendererShutdown");
        }

        public void onSurfaceChanged(int arg0, int arg1)
        {
            Console.WriteLine("AndroidCardboardExperiment onSurfaceChanged");
        }

        public void onSurfaceCreated(javax.microedition.khronos.egl.EGLConfig value)
        {
            Console.WriteLine("enter AndroidCardboardExperiment onSurfaceCreated");

            GLES20.glClearColor(0.1f, 0.1f, 0.1f, 0.5f); // Dark background so text shows up well.

            ByteBuffer bbVertices = ByteBuffer.allocateDirect(WorldLayoutData.CUBE_COORDS.Length * 4);
            bbVertices.order(ByteOrder.nativeOrder());
            cubeVertices = bbVertices.asFloatBuffer();
            cubeVertices.put(WorldLayoutData.CUBE_COORDS);
            cubeVertices.position(0);

            ByteBuffer bbColors = ByteBuffer.allocateDirect(WorldLayoutData.CUBE_COLORS.Length * 4);
            bbColors.order(ByteOrder.nativeOrder());
            cubeColors = bbColors.asFloatBuffer();
            cubeColors.put(WorldLayoutData.CUBE_COLORS);
            cubeColors.position(0);

            ByteBuffer bbFoundColors = ByteBuffer.allocateDirect(
                WorldLayoutData.CUBE_FOUND_COLORS.Length * 4);
            bbFoundColors.order(ByteOrder.nativeOrder());
            cubeFoundColors = bbFoundColors.asFloatBuffer();
            cubeFoundColors.put(WorldLayoutData.CUBE_FOUND_COLORS);
            cubeFoundColors.position(0);

            ByteBuffer bbNormals = ByteBuffer.allocateDirect(WorldLayoutData.CUBE_NORMALS.Length * 4);
            bbNormals.order(ByteOrder.nativeOrder());
            cubeNormals = bbNormals.asFloatBuffer();
            cubeNormals.put(WorldLayoutData.CUBE_NORMALS);
            cubeNormals.position(0);

            // make a floor
            ByteBuffer bbFloorVertices = ByteBuffer.allocateDirect(WorldLayoutData.FLOOR_COORDS.Length * 4);
            bbFloorVertices.order(ByteOrder.nativeOrder());
            floorVertices = bbFloorVertices.asFloatBuffer();
            floorVertices.put(WorldLayoutData.FLOOR_COORDS);
            floorVertices.position(0);

            ByteBuffer bbFloorNormals = ByteBuffer.allocateDirect(WorldLayoutData.FLOOR_NORMALS.Length * 4);
            bbFloorNormals.order(ByteOrder.nativeOrder());
            floorNormals = bbFloorNormals.asFloatBuffer();
            floorNormals.put(WorldLayoutData.FLOOR_NORMALS);
            floorNormals.position(0);

            ByteBuffer bbFloorColors = ByteBuffer.allocateDirect(WorldLayoutData.FLOOR_COLORS.Length * 4);
            bbFloorColors.order(ByteOrder.nativeOrder());
            floorColors = bbFloorColors.asFloatBuffer();
            floorColors.put(WorldLayoutData.FLOOR_COLORS);
            floorColors.position(0);


            #region loadGLShader
            Func<int, string, int> loadGLShader = (int type, string code) =>
            {
                int shader = GLES20.glCreateShader(type);
                GLES20.glShaderSource(shader, code);
                GLES20.glCompileShader(shader);

                // Get the compilation status.
                int[] compileStatus = new int[1];
                GLES20.glGetShaderiv(shader, GLES20.GL_COMPILE_STATUS, compileStatus, 0);

                // If the compilation failed, delete the shader.
                if (compileStatus[0] == 0)
                {
                    Console.WriteLine("Error compiling shader: " + GLES20.glGetShaderInfoLog(shader));
                    GLES20.glDeleteShader(shader);
                    shader = 0;
                }

                if (shader == 0)
                {
                    throw new Exception("Error creating shader.");
                }

                return shader;
            };
            #endregion


            int vertexShader = loadGLShader(GLES20.GL_VERTEX_SHADER, new AndroidCardboardExperiment.Shaders.light_vertexVertexShader().ToString());
            int gridShader = loadGLShader(GLES20.GL_FRAGMENT_SHADER, new AndroidCardboardExperiment.Shaders.grid_fragmentFragmentShader().ToString());
            int passthroughShader = loadGLShader(GLES20.GL_FRAGMENT_SHADER, new AndroidCardboardExperiment.Shaders.passthrough_fragmentFragmentShader().ToString());

            cubeProgram = GLES20.glCreateProgram();
            GLES20.glAttachShader(cubeProgram, vertexShader);
            GLES20.glAttachShader(cubeProgram, passthroughShader);
            GLES20.glLinkProgram(cubeProgram);
            GLES20.glUseProgram(cubeProgram);

            checkGLError("Cube program");

            cubePositionParam = GLES20.glGetAttribLocation(cubeProgram, "a_Position");
            cubeNormalParam = GLES20.glGetAttribLocation(cubeProgram, "a_Normal");
            cubeColorParam = GLES20.glGetAttribLocation(cubeProgram, "a_Color");

            cubeModelParam = GLES20.glGetUniformLocation(cubeProgram, "u_Model");
            cubeModelViewParam = GLES20.glGetUniformLocation(cubeProgram, "u_MVMatrix");
            cubeModelViewProjectionParam = GLES20.glGetUniformLocation(cubeProgram, "u_MVP");
            cubeLightPosParam = GLES20.glGetUniformLocation(cubeProgram, "u_LightPos");

            GLES20.glEnableVertexAttribArray(cubePositionParam);
            GLES20.glEnableVertexAttribArray(cubeNormalParam);
            GLES20.glEnableVertexAttribArray(cubeColorParam);

            checkGLError("Cube program params");

            floorProgram = GLES20.glCreateProgram();
            GLES20.glAttachShader(floorProgram, vertexShader);
            GLES20.glAttachShader(floorProgram, gridShader);
            GLES20.glLinkProgram(floorProgram);
            GLES20.glUseProgram(floorProgram);

            checkGLError("Floor program");

            floorModelParam = GLES20.glGetUniformLocation(floorProgram, "u_Model");
            floorModelViewParam = GLES20.glGetUniformLocation(floorProgram, "u_MVMatrix");
            floorModelViewProjectionParam = GLES20.glGetUniformLocation(floorProgram, "u_MVP");
            floorLightPosParam = GLES20.glGetUniformLocation(floorProgram, "u_LightPos");

            floorPositionParam = GLES20.glGetAttribLocation(floorProgram, "a_Position");
            floorNormalParam = GLES20.glGetAttribLocation(floorProgram, "a_Normal");
            floorColorParam = GLES20.glGetAttribLocation(floorProgram, "a_Color");

            GLES20.glEnableVertexAttribArray(floorPositionParam);
            GLES20.glEnableVertexAttribArray(floorNormalParam);
            GLES20.glEnableVertexAttribArray(floorColorParam);

            checkGLError("Floor program params");

            GLES20.glEnable(GLES20.GL_DEPTH_TEST);

            // Object first appears directly in front of user.
            Matrix.setIdentityM(modelCube, 0);
            Matrix.translateM(modelCube, 0, 0, 0, -objectDistance);

            Matrix.setIdentityM(modelFloor, 0);
            Matrix.translateM(modelFloor, 0, 0, -floorDepth, 0); // Floor appears below user.

            checkGLError("onSurfaceCreated");

            Console.WriteLine("exit AndroidCardboardExperiment onSurfaceCreated");
        }



        public void onNewFrame(HeadTransform headTransform)
        {
            //Console.WriteLine("AndroidCardboardExperiment onNewFrame");

            // Build the Model part of the ModelView matrix.
            Matrix.rotateM(modelCube, 0, TIME_DELTA, 0.5f, 0.5f, 1.0f);

            // Build the camera matrix and apply it to the ModelView.
            Matrix.setLookAtM(camera, 0, 0.0f, 0.0f, CAMERA_Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f);

            headTransform.getHeadView(headView, 0);

            checkGLError("onReadyToDraw");
        }

        // Error	3	'AndroidCardboardExperiment.Activities.ApplicationActivity.onDrawEye(com.google.vrtoolkit.cardboard.Eye)': no suitable method found to override	X:\jsc.svn\examples\java\android\synergy\AndroidCardboardExperiment\AndroidCardboardExperiment\ApplicationActivity.cs	328	31	AndroidCardboardExperiment
        // StereoRenderer
        public void onDrawEye(Eye eye)
        {
            GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);

            checkGLError("colorParam");

            // Apply the eye transformation to the camera.
            Matrix.multiplyMM(view, 0, eye.getEyeView(), 0, camera, 0);

            // Set the position of the light
            Matrix.multiplyMV(lightPosInEyeSpace, 0, view, 0, LIGHT_POS_IN_WORLD_SPACE, 0);

            // Build the ModelView and ModelViewProjection matrices
            // for calculating cube position and light.
            float[] perspective = eye.getPerspective(Z_NEAR, Z_FAR);
            Matrix.multiplyMM(modelView, 0, view, 0, modelCube, 0);
            Matrix.multiplyMM(modelViewProjection, 0, perspective, 0, modelView, 0);
            drawCube();

            // Set modelView for the floor, so we draw floor in the correct location
            Matrix.multiplyMM(modelView, 0, view, 0, modelFloor, 0);
            Matrix.multiplyMM(modelViewProjection, 0, perspective, 0,
              modelView, 0);
            drawFloor();
        }

        public void onFinishFrame(Viewport value)
        {
        }

        // called by onDrawEye
        public void drawCube()
        {
            GLES20.glUseProgram(cubeProgram);

            GLES20.glUniform3fv(cubeLightPosParam, 1, lightPosInEyeSpace, 0);

            // Set the Model in the shader, used to calculate lighting
            GLES20.glUniformMatrix4fv(cubeModelParam, 1, false, modelCube, 0);

            // Set the ModelView in the shader, used to calculate lighting
            GLES20.glUniformMatrix4fv(cubeModelViewParam, 1, false, modelView, 0);

            // Set the position of the cube
            GLES20.glVertexAttribPointer(cubePositionParam, COORDS_PER_VERTEX, GLES20.GL_FLOAT,
                false, 0, cubeVertices);

            // Set the ModelViewProjection matrix in the shader.
            GLES20.glUniformMatrix4fv(cubeModelViewProjectionParam, 1, false, modelViewProjection, 0);

            // Set the normal positions of the cube, again for shading
            GLES20.glVertexAttribPointer(cubeNormalParam, 3, GLES20.GL_FLOAT, false, 0, cubeNormals);



            var cc = cubeFoundColors;
            if (isLookingAtObject()) cc = cubeColors;

            GLES20.glVertexAttribPointer(cubeColorParam, 4, GLES20.GL_FLOAT, false, 0,
                cc);

            GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 36);
            checkGLError("Drawing cube");
        }


        // called by onDrawEye
        public void drawFloor()
        {
            GLES20.glUseProgram(floorProgram);

            // Set ModelView, MVP, position, normals, and color.
            GLES20.glUniform3fv(floorLightPosParam, 1, lightPosInEyeSpace, 0);
            GLES20.glUniformMatrix4fv(floorModelParam, 1, false, modelFloor, 0);
            GLES20.glUniformMatrix4fv(floorModelViewParam, 1, false, modelView, 0);
            GLES20.glUniformMatrix4fv(floorModelViewProjectionParam, 1, false,
                modelViewProjection, 0);
            GLES20.glVertexAttribPointer(floorPositionParam, COORDS_PER_VERTEX, GLES20.GL_FLOAT,
                false, 0, floorVertices);
            GLES20.glVertexAttribPointer(floorNormalParam, 3, GLES20.GL_FLOAT, false, 0,
                floorNormals);
            GLES20.glVertexAttribPointer(floorColorParam, 4, GLES20.GL_FLOAT, false, 0, floorColors);

            GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 6);

            checkGLError("drawing floor");
        }


        public override void onCardboardTrigger()
        {
            //Log.i(TAG, "onCardboardTrigger");

            if (isLookingAtObject())
            {
                score++;
                overlayView.show3DToast("Found it! Look around for another one.\nScore = " + score);
                //hideObject();
            }
            else
            {
                overlayView.show3DToast("Look around to find the object!");
            }

            // Always give user feedback.
            vibrator.vibrate(50);
        }




        private bool isLookingAtObject()
        {
            // can we do this in gearVR too?

            float[] initVec = { 0, 0, 0, 1.0f };
            float[] objPositionVec = new float[4];

            // Convert object space to camera space. Use the headView from onNewFrame.
            Matrix.multiplyMM(modelView, 0, headView, 0, modelCube, 0);
            Matrix.multiplyMV(objPositionVec, 0, modelView, 0, initVec, 0);

            float pitch = (float)Math.Atan2(objPositionVec[1], -objPositionVec[2]);
            float yaw = (float)Math.Atan2(objPositionVec[0], -objPositionVec[2]);

            if (Math.Abs(pitch) < PITCH_LIMIT)
                if (Math.Abs(yaw) < YAW_LIMIT)
                    return true;
            return false;
        }
    }


}

//-dex:
//      [dex] input: W:\bin\classes
//      [dex] input: W:\libs\cardboard.jar
//      [dex] Pre-Dexing W:\libs\cardboard.jar -> cardboard-794edd550a067d637de3eed4ac7ff6f3.jar
//      [dex] Found Deleted Target File
//      [dex] Converting compiled files and external libraries into W:\bin\classes.dex...
//       [dx] Merged dex A (682 defs/622.3KiB) with dex B (110 defs/160.6KiB). Result is 792 defs/954.0KiB. Took 0.1s