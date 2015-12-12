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
using java.nio;
using AndroidCardboardExperiment;
using android.opengl;
using android.content.pm;
using android.content;
using xandroidcardboardcxperiment.xactivities;
using System.Diagnostics;

namespace CardboardForEdgeExperiment.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class XApplicationActivity :

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151212/androidcardboardexperiment
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150430
         com.google.vrtoolkit.cardboard.CardboardActivity,
        com.google.vrtoolkit.cardboard.CardboardView.StereoRenderer
    {
        xandroidcardboardcxperiment.xactivities.ApplicationActivity ref0;

        // I/System.Console(24261): CardboardForEdgeExperiment { ProcessorCount = 8, MODEL = SM-G925F, CurrentManagedThreadId = 17028, FrameCounter = 60, LastFrameMilliseconds = 5, codeFPS = 200.0, pitch = -1.8509252, yaw = 1.5846334 }

        // "x:\util\android-sdk-windows\platform-tools\adb.exe" install -r "r:\jsc.svn\examples\java\android\CardboardForEdgeExperiment\CardboardForEdgeExperiment\bin\Debug\staging\apk\bin\CardboardForEdgeExperiment.Activities-debug.apk"

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
        //private static float Z_FAR = 100.0f;
        private static float Z_FAR = 400.0f;

        private static float CAMERA_Z = 0.01f;
        private static float TIME_DELTA = 0.3f;

        private static float YAW_LIMIT = 0.12f;
        private static float PITCH_LIMIT = 0.12f;

        private static int COORDS_PER_VERTEX = 3;

        // We keep the light always position just above the user.
        private static float[] LIGHT_POS_IN_WORLD_SPACE = new float[] { 0.0f, 2.0f, 0.0f, 1.0f };

        private float[] lightPosInEyeSpace = new float[4];

        private FloatBuffer floorVertices;
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

        private float[] headView;
        private float[] modelViewProjection;

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




            modelViewProjection = new float[16];


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

            var fcolors = 0xA26D41;
            // rgb to float

            //[javac]         return  __Enumerable.<Float>AsEnumerable(__SZArrayEnumerator_1.<Float>Of(x));
            //[javac]                                                                       ^
            //[javac]   required: T#1[]
            //[javac]   found: float[]
            //[javac]   reason: actual argument float[] cannot be converted to Float[] by method invocation conversion

            //          var FLOOR_COLORS = (
            //              from i in Enumerable.Range(0, 6)
            //              select new float[] { 0xA2 / 1.0f, 0x6D / 1.0f, 0x41 / 1.0f, 1.0f }
            //).SelectMany(x => x).ToArray();

            #region floorColors
            var FLOOR_COLORS = new float[4 * 6];

            for (int i = 0; i < FLOOR_COLORS.Length; i += 4)
            {
                FLOOR_COLORS[i + 0] = 0xA2 / 100.0f;
                FLOOR_COLORS[i + 1] = 0x6D / 100.0f;
                FLOOR_COLORS[i + 2] = 0x41 / 100.0f;
                FLOOR_COLORS[i + 3] = 1.0f;
            }



            FloatBuffer floorColors;

            ByteBuffer bbFloorColors = ByteBuffer.allocateDirect(WorldLayoutData.FLOOR_COLORS.Length * 4);
            bbFloorColors.order(ByteOrder.nativeOrder());
            floorColors = bbFloorColors.asFloatBuffer();
            //floorColors.put(WorldLayoutData.FLOOR_COLORS);
            floorColors.put(FLOOR_COLORS);
            floorColors.position(0);
            #endregion


            #region loadGLShader
            Func<int, ScriptCoreLib.GLSL.Shader, int> loadGLShader = (type, xshader) =>
            {
                var code = xshader.ToString();

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


            int vertexShader = loadGLShader(GLES20.GL_VERTEX_SHADER, new AndroidCardboardExperiment.Shaders.light_vertexVertexShader());
            int gridShader = loadGLShader(GLES20.GL_FRAGMENT_SHADER, new Shaders.xgrid_fragmentFragmentShader());
            int passthroughShader = loadGLShader(GLES20.GL_FRAGMENT_SHADER, new AndroidCardboardExperiment.Shaders.passthrough_fragmentFragmentShader());

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
            //GLES20.glEnable(GLES20.GL_FOG);




            checkGLError("onSurfaceCreated");

            Console.WriteLine("exit AndroidCardboardExperiment onSurfaceCreated");


            vFinishFrame = (com.google.vrtoolkit.cardboard.Viewport v) =>
            {

                // GPU thread stops now..
                FrameOne.Stop();
            };

            // I/System.Console(28103): CardboardForEdgeExperiment { ProcessorCount = 8, MODEL = SM-G925F, CurrentManagedThreadId = 11305, FrameCounter = 28, LastFrameMilliseconds = 40, codeFPS = 25.0, pitch = 1.579644, yaw = 1.6225219 }

            #region vNewFrame
            vNewFrame = (com.google.vrtoolkit.cardboard.HeadTransform headTransform) =>
            {
                // http://stackoverflow.com/questions/11851343/raise-fps-on-android-tablet-above-60-for-opengl-game
                // http://gafferongames.com/game-physics/fix-your-timestep/

                #region FrameWatch
                if (FrameWatch.ElapsedMilliseconds >= 1000)
                {
                    var codeFPS = 1000.0 / FrameOne.ElapsedMilliseconds;

                    // we now know how many frames did fit into it
                    // need 60 or more!
                    Console.WriteLine("CardboardForEdgeExperiment " + new
                    {
                        // static
                        System.Environment.ProcessorCount,

                        android.os.Build.MODEL,

                        System.Environment.CurrentManagedThreadId,

                        FrameCounter,

                        // dynamic
                        LastFrameMilliseconds = FrameOne.ElapsedMilliseconds,
                        codeFPS,

                        // very dynamic
                        pitch,
                        yaw
                    });

                    // I/System.Console(28117): CardboardForEdgeExperiment { ProcessorCount = 2, MODEL = Nexus 9, CurrentManagedThreadId = 1647, FrameCounter = 60, LastFrameMilliseconds = 6, codeFPS = 166.66666666666666, pitch = 1.5978987, yaw = -2.0770574 }

                    FrameWatch.Restart();
                    FrameCounter = 0;
                }

                #endregion
                // GPU thread starts now..
                FrameOne.Restart();
                FrameCounter++;


                //Console.WriteLine("AndroidCardboardExperiment onNewFrame");





                headTransform.getHeadView(headView, 0);

                checkGLError("onReadyToDraw");

                // I/System.Console(27769): CardboardForEdgeExperiment { FrameCounter = 61, LastFrameMilliseconds = 0, codeFPS = Infinity, CurrentManagedThreadId = 1637, ProcessorCount = 2, MODEL = Nexus 9 }

                // add placeholder slowdown
                //System.Threading.Thread.Sleep(5);
                // I/System.Console(27840): CardboardForEdgeExperiment { FrameCounter = 60, LastFrameMilliseconds = 6, codeFPS = 166.66666666666666, CurrentManagedThreadId = 1642, ProcessorCount = 2, MODEL = Nexus 9 }

            };
            #endregion

            // if we define it here, we get to see it in vr...
            var modelCube = new float[16];

            // I/System.Console(19917): CardboardForEdgeExperiment { ProcessorCount = 8, MODEL = SM-G925F, CurrentManagedThreadId = 9959, FrameCounter = 46, LastFrameMilliseconds = 6, codeFPS = 166.66666666666666, pitch = 0.9070491, yaw = -0.3660261 }

            #region vDrawEye
            vDrawEye = (com.google.vrtoolkit.cardboard.Eye eye) =>
            {
                // VIDEO via "X:\util\android-sdk-windows\tools\ddms.bat"

                var camera = new float[16];


                // static void	setLookAtM(float[] rm, int rmOffset, float eyeX, float eyeY, float eyeZ, float centerX, float centerY, float centerZ, float upX, float upY, float upZ)
                // Build the camera matrix and apply it to the ModelView.
                Matrix.setLookAtM(camera, 0,

                    0.0f, 0.0f, CAMERA_Z,

                   0f, 0.0f, 0.0f,

                    0.0f, 1.0f, 0.0f);


                #region glClearColor
                // skybox/video instead?
                GLES20.glClearColor(
                    0x87 / 255f,
                    0xCE / 255f,
                    0xEB / 255f, 1.0f
                );

                GLES20.glClear(GLES20.GL_COLOR_BUFFER_BIT | GLES20.GL_DEPTH_BUFFER_BIT);
                #endregion




                var view = new float[16];

                // can we strafe?



                // Apply the eye transformation to the camera.
                Matrix.multiplyMM(view, 0, eye.getEyeView(), 0, camera, 0);


                // we tapped into it. this strafes ius!
                Matrix.translateM(view, 0,

                    (float)Math.Sin(TotalTime.ElapsedMilliseconds * 0.0001f) * objectDistance * 2.5f,


                    // up down
                    //(float)Math.Sin(TotalTime.ElapsedMilliseconds * 0.001f) * floorDepth * 0.5f,
                    (float)Math.Cos(TotalTime.ElapsedMilliseconds * 0.001f) * floorDepth * 0.1f,

                    0
                    );


                // Set the position of the light
                Matrix.multiplyMV(lightPosInEyeSpace, 0, view, 0, LIGHT_POS_IN_WORLD_SPACE, 0);

                // Build the ModelView and ModelViewProjection matrices
                // for calculating cube position and light.
                float[] perspective = eye.getPerspective(Z_NEAR, Z_FAR);


                // just a buffer?
                var modelView = new float[16];


                #region drawCube()
                Action<float, float, float> drawCube = (tx, ty, tz) =>
                {

                    #region isLookingAtObject
                    Func<bool> isLookingAtObject = () =>
                    {
                        float[] initVec = { 0, 0, 0, 1.0f };



                        float[] objPositionVec = new float[4];

                        // Convert object space to camera space. Use the headView from onNewFrame.
                        Matrix.multiplyMM(modelView, 0, headView, 0, modelCube, 0);
                        Matrix.multiplyMV(objPositionVec, 0, modelView, 0, initVec, 0);


        
                        pitch = (float)Math.Atan2(objPositionVec[1], -objPositionVec[2]);
                        yaw = (float)Math.Atan2(objPositionVec[0], -objPositionVec[2]);

                        if (Math.Abs(pitch) < PITCH_LIMIT)
                            if (Math.Abs(yaw) < YAW_LIMIT)
                                return true;
                        return false;
                    };
                    #endregion




                    // Object first appears directly in front of user.
                    Matrix.setIdentityM(modelCube, 0);
                    // cant see it?
                    var scale = 5.0f;
                    //Matrix.scaleM(modelCube, 0, scale, scale, scale);

                    Matrix.translateM(modelCube, 0, tx, ty, tz);


                    Matrix.multiplyMM(modelView, 0, view, 0, modelCube, 0);
                    Matrix.multiplyMM(modelViewProjection, 0, perspective, 0, modelView, 0);


                    // public static void scaleM (float[] m, int mOffset, float x, float y, float z)

                    // Build the Model part of the ModelView matrix.
                    //Matrix.rotateM(modelCube, 0, TIME_DELTA, 0.5f, 0.5f, 1.0f);

                    // cant see rotation?
                    Matrix.rotateM(modelCube, 0, TotalTime.ElapsedMilliseconds * 0.01f,
                        // upwards rot.
                        //0.5f, 

                        0f,

                        // sideways, left to right
                        0.5f
                        , 0.0f);


                    // http://developer.android.com/reference/android/opengl/Matrix.html#translateM(float[], int, float, float, float)


                    // the cube rotates in front of us.
                    // do we need to use a special program to draw a cube?
                    // how can we make it bigger?

                    GLES20.glUseProgram(cubeProgram);

                    GLES20.glUniform3fv(cubeLightPosParam, 1, lightPosInEyeSpace, 0);

                    // Set the Model in the shader, used to calculate lighting
                    GLES20.glUniformMatrix4fv(cubeModelParam, 1, false, modelCube, 0);

                    // Set the ModelView in the shader, used to calculate lighting
                    GLES20.glUniformMatrix4fv(cubeModelViewParam, 1, false, modelView, 0);

                    // Set the position of the cube
                    GLES20.glVertexAttribPointer(cubePositionParam, COORDS_PER_VERTEX, GLES20.GL_FLOAT, false, 0, cubeVertices);

                    // Set the ModelViewProjection matrix in the shader.
                    GLES20.glUniformMatrix4fv(cubeModelViewProjectionParam, 1, false, modelViewProjection, 0);

                    // Set the normal positions of the cube, again for shading
                    GLES20.glVertexAttribPointer(cubeNormalParam, 3, GLES20.GL_FLOAT, false, 0, cubeNormals);


                    #region cubeColors
                    var cc = cubeColors;
                    if (!isLookingAtObject()) cc = cubeFoundColors;

                    GLES20.glVertexAttribPointer(cubeColorParam, 4, GLES20.GL_FLOAT, false, 0, cc);
                    #endregion

                    GLES20.glDrawArrays(GLES20.GL_TRIANGLES, 0, 36);
                    checkGLError("Drawing cube");
                };


                #endregion

                #region drawCube
                drawCube(0, objectDistance, objectDistance * -1.0f);


                drawCube(0, 0, objectDistance * -2.0f);

                // looks like an airstrip

                // low fps?
                //var endOfMatrix = 64;
                var endOfMatrix = 20;
                for (int i = -endOfMatrix; i < endOfMatrix; i++)
                {
                    drawCube(objectDistance, -floorDepth, objectDistance * -2.0f * i);
                    drawCube(-objectDistance, -floorDepth, objectDistance * -2.0f * i);


                    drawCube(objectDistance * 0.5f, 0, objectDistance * -2.0f * i);
                    drawCube(objectDistance * -0.5f, 0, objectDistance * -2.0f * i);
                }
                #endregion





                var modelFloor = new float[16];

                Matrix.setIdentityM(modelFloor, 0);
                Matrix.translateM(modelFloor, 0,

                    // the floor escapes!
                    //TotalTime.ElapsedMilliseconds * 0.01f,
                    0, -floorDepth, 0); // Floor appears below user.

                // Set modelView for the floor, so we draw floor in the correct location
                Matrix.multiplyMM(modelView, 0, view, 0, modelFloor, 0);
                Matrix.multiplyMM(modelViewProjection, 0, perspective, 0, modelView, 0);

                #region drawFloor
                // called by onDrawEye
                Action drawFloor = delegate
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
                };

                drawFloor();
                #endregion


            };
            #endregion

        }



        Stopwatch TotalTime = Stopwatch.StartNew();

        long FrameCounter = 0;
        Stopwatch FrameWatch = Stopwatch.StartNew();
        Stopwatch FrameOne = Stopwatch.StartNew();

        public Action<com.google.vrtoolkit.cardboard.HeadTransform> vNewFrame;
        public void onNewFrame(com.google.vrtoolkit.cardboard.HeadTransform headTransform)
        {
            vNewFrame(headTransform);
        }

        // Error	3	'AndroidCardboardExperiment.Activities.ApplicationActivity.onDrawEye(com.google.vrtoolkit.cardboard.Eye)': no suitable method found to override	X:\jsc.svn\examples\java\android\synergy\AndroidCardboardExperiment\AndroidCardboardExperiment\ApplicationActivity.cs	328	31	AndroidCardboardExperiment
        // StereoRenderer
        public Action<com.google.vrtoolkit.cardboard.Eye> vDrawEye;

        public void onDrawEye(com.google.vrtoolkit.cardboard.Eye eye)
        {
            vDrawEye(eye);
        }

        public Action<com.google.vrtoolkit.cardboard.Viewport> vFinishFrame;
        public void onFinishFrame(com.google.vrtoolkit.cardboard.Viewport value)
        {
            vFinishFrame(value);
        }








        float pitch;
        float yaw;


    }


}
