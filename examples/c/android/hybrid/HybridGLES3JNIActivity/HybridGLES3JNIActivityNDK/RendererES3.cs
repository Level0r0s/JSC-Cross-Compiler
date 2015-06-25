using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.EGL;
using ScriptCoreLibNative.SystemHeaders.GLES3;
//using ScriptCoreLibNative.SystemHeaders.linux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HybridGLES3JNIActivity.NDK
{
    // "X:\util\android-ndk-r10e\samples\gles3jni\jni\RendererES3.cpp"


    #region QUAD
    unsafe struct Vertex
    {
        public fixed float pos[2];

        public fixed byte rgba[4];


        public static readonly float[,] __pos = new float[4, 2]
        { 
            {-0.7f, -0.7f},
            { 0.7f, -0.7f},
            {-0.7f,  0.7f},
            { 0.7f,  0.7f},
        };

        public static readonly byte[,] __rgba = new byte[4, 3]
        { 
           {0x00, 0xFF, 0x00},
           {0x00, 0x00, 0xFF},
           {0xFF, 0x00, 0x00},
           {0xFF, 0xFF, 0xFF}
        };
    };

    unsafe class QUAD
    {
        public readonly Vertex[] __value = new Vertex[4];

        public QUAD()
        {
            //ConsoleExtensions.tracei("QUAD = {");

            for (int y = 0; y < 4; y++)
                fixed (Vertex* v = &__value[y])
                {
                    v->pos[0] = Vertex.__pos[y, 0];
                    v->pos[1] = Vertex.__pos[y, 1];

                    v->rgba[0] = Vertex.__rgba[y, 0];
                    v->rgba[1] = Vertex.__rgba[y, 1];
                    v->rgba[2] = Vertex.__rgba[y, 2];
                    v->rgba[3] = Vertex.__rgba[y, 3];

                }
        }
    }
    #endregion

    unsafe class RendererES3
    {
        // "X:\jsc.svn\examples\c\android\hybrid\HybridGLES3JNIActivity\HybridGLES3JNIActivityNDK\bin\Debug\staging\libs\armeabi-v7a\libmain.so"

        readonly QUAD QUAD = new QUAD();

        public const float TWO_PI = (float)(2.0 * Math.PI);
        public const double MAX_ROT_SPEED = (0.3 * TWO_PI);


        public const int MAX_INSTANCES_PER_SIDE = 16;
        public const int MAX_INSTANCES = (MAX_INSTANCES_PER_SIDE * MAX_INSTANCES_PER_SIDE);

        public readonly float[,] centers = new float[2, MAX_INSTANCES_PER_SIDE];


        public readonly float[] mAngularVelocity = new float[MAX_INSTANCES];
        public readonly float[] mAngles = new float[MAX_INSTANCES];
        public readonly float[] mScale = new float[2];

        public const int POS_ATTRIB = 0;
        public const int COLOR_ATTRIB = 1;
        public const uint SCALEROT_ATTRIB = 2;
        public const uint OFFSET_ATTRIB = 3;


        const string VERTEX_SHADER =
          "#version 300 es\n"
          + "layout(location = 0) in vec2 pos;\n"
          + "layout(location=1) in vec4 color;\n"
          + "layout(location=2) in vec4 scaleRot;\n"
          + "layout(location=3) in vec2 offset;\n"
          + "out vec4 vColor;\n"
          + "void main() {\n"
          + "    mat2 sr = mat2(scaleRot.xy, scaleRot.zw);\n"
          + "    gl_Position = vec4(sr*pos + offset, 0.0, 1.0);\n"
          + "    vColor = color;\n"
          + "}\n";

        const string FRAGMENT_SHADER =
          "#version 300 es\n"
          + "precision mediump float;\n"
          + "in vec4 vColor;\n"
          + "out vec4 outColor;\n"
          + "void main() {\n"
          + "    outColor = vColor;\n"
          + "}\n";



        public static RendererES3 createES3Renderer()
        {
            var renderer = new RendererES3();

            if (!renderer.init())
            {
                //stdlib_h.free(renderer);
                return null;
            }
            return renderer;
        }


        uint mProgram;

        public enum VB { VB_INSTANCE, VB_SCALEROT, VB_OFFSET, VB_COUNT };

        readonly uint[] mVB = new uint[(int)VB.VB_COUNT];
        uint mVBState;

        EGLContext mEglContext;

        // set by calcSceneParams
        int mNumInstances;
        ulong mLastFrameNs;

        bool init()
        {
            ConsoleExtensions.tracei("enter init, call eglGetCurrentContext");
            mEglContext = egl.eglGetCurrentContext();


            mProgram = createProgram(VERTEX_SHADER, FRAGMENT_SHADER);
            if (mProgram == 0)
                return false;

            ConsoleExtensions.tracei("init mProgram: ", (int)mProgram);

            gl3.glGenBuffers((int)VB.VB_COUNT, mVB);
            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_INSTANCE]);
            //gl3.glBufferData(gl3.GL_ARRAY_BUFFER, sizeof(QUAD), &QUAD[0], gl3.GL_STATIC_DRAW);
            gl3.glBufferData(gl3.GL_ARRAY_BUFFER, sizeof(Vertex) * 4, QUAD.__value, gl3.GL_STATIC_DRAW);

            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_SCALEROT]);
            gl3.glBufferData(gl3.GL_ARRAY_BUFFER, MAX_INSTANCES * 4 * sizeof(float), null, gl3.GL_DYNAMIC_DRAW);
            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_OFFSET]);
            gl3.glBufferData(gl3.GL_ARRAY_BUFFER, MAX_INSTANCES * 2 * sizeof(float), null, gl3.GL_STATIC_DRAW);

            gl3.glGenVertexArrays(1, ref mVBState);
            gl3.glBindVertexArray(mVBState);

            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_INSTANCE]);
            //gl3.glVertexAttribPointer(POS_ATTRIB, 4, gl3.GL_FLOAT, gl3.GL_FALSE, sizeof(Vertex), (const GLvoid*)offsetof(Vertex, pos));
            var offset_pos = (void*)(0);
            gl3.glVertexAttribPointer(POS_ATTRIB, 4, gl3.GL_FLOAT, false, sizeof(Vertex), offset_pos);
            var offset_rgba = (void*)(2 * sizeof(float));
            //gl3.glVertexAttribPointer(COLOR_ATTRIB, 4, gl3.GL_UNSIGNED_BYTE, gl3.GL_TRUE, sizeof(Vertex), (const GLvoid*)offsetof(Vertex, rgba));
            gl3.glVertexAttribPointer(COLOR_ATTRIB, 4, gl3.GL_UNSIGNED_BYTE, true, sizeof(Vertex), offset_rgba);
            gl3.glEnableVertexAttribArray(POS_ATTRIB);
            gl3.glEnableVertexAttribArray(COLOR_ATTRIB);

            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_SCALEROT]);
            gl3.glVertexAttribPointer(SCALEROT_ATTRIB, 4, gl3.GL_FLOAT, false, 4 * sizeof(float), default(void*));
            gl3.glEnableVertexAttribArray(SCALEROT_ATTRIB);
            gl3.glVertexAttribDivisor(SCALEROT_ATTRIB, 1);

            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_OFFSET]);
            gl3.glVertexAttribPointer(OFFSET_ATTRIB, 2, gl3.GL_FLOAT, false, 2 * sizeof(float), null);
            gl3.glEnableVertexAttribArray(OFFSET_ATTRIB);
            gl3.glVertexAttribDivisor(OFFSET_ATTRIB, 1);

            //ALOGV("Using OpenGL ES 3.0 renderer");
            return true;
        }



        static uint createShader(int shaderType, string src)
        {
            uint shader = gl3.glCreateShader(shaderType);
            if (shader == 0)
            {
                //checkGlError("glCreateShader");
                return 0;
            }

            gl3.glShaderSource(shader, 1, ref src, null);

            int compiled = gl3.GL_FALSE;
            gl3.glCompileShader(shader);
            gl3.glGetShaderiv(shader, gl3.GL_COMPILE_STATUS, out compiled);
            if (compiled == gl3.GL_FALSE)
            {
                //GLint infoLogLen = 0;
                //glGetShaderiv(shader, GL_INFO_LOG_LENGTH, &infoLogLen);
                //if (infoLogLen > 0)
                //{
                //    GLchar* infoLog = (GLchar*)malloc(infoLogLen);
                //    if (infoLog)
                //    {
                //        glGetShaderInfoLog(shader, infoLogLen, NULL, infoLog);
                //        ALOGE("Could not compile %s shader:\n%s\n",
                //                shaderType == GL_VERTEX_SHADER ? "vertex" : "fragment",
                //                infoLog);
                //        free(infoLog);
                //    }
                //}
                //gl3.glDeleteShader(shader);
                return 0;
            }

            return shader;
        }


        static uint createProgram(string vtxSrc, string fragSrc)
        {
            ConsoleExtensions.tracei("enter createProgram");

            uint vtxShader = 0;
            uint fragShader = 0;
            uint program = 0;
            int linked = gl3.GL_FALSE;

            vtxShader = createShader(gl3.GL_VERTEX_SHADER, vtxSrc);

            ConsoleExtensions.tracei("vtxShader: ", (int)vtxShader);

            //if (!vtxShader)
            //    goto exit;

            fragShader = createShader(gl3.GL_FRAGMENT_SHADER, fragSrc);
            ConsoleExtensions.tracei("fragShader: ", (int)fragShader);
            //if (!fragShader)
            //    goto exit;

            program = gl3.glCreateProgram();
            //if (!program)
            //{
            //    checkGlError("glCreateProgram");
            //    goto exit;
            //}
            gl3.glAttachShader(program, vtxShader);
            gl3.glAttachShader(program, fragShader);

            gl3.glLinkProgram(program);
            gl3.glGetProgramiv(program, gl3.GL_LINK_STATUS, out linked);

            ConsoleExtensions.tracei("linked: ", (int)linked);

            //    if (!linked)
            //    {
            //        ALOGE("Could not link program");
            //        GLint infoLogLen = 0;
            //        glGetProgramiv(program, GL_INFO_LOG_LENGTH, &infoLogLen);
            //        if (infoLogLen)
            //        {
            //            GLchar* infoLog = (GLchar*)malloc(infoLogLen);
            //            if (infoLog)
            //            {
            //                glGetProgramInfoLog(program, infoLogLen, NULL, infoLog);
            //                ALOGE("Could not link program:\n%s\n", infoLog);
            //                free(infoLog);
            //            }
            //        }
            //        glDeleteProgram(program);
            //        program = 0;
            //    }

            //exit:
            //    glDeleteShader(vtxShader);
            //    glDeleteShader(fragShader);
            return program;
        }




        float[] mapTransformBuf()
        {
            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_SCALEROT]);
            return gl3.glMapBufferRange<float>(gl3.GL_ARRAY_BUFFER,
                    0, MAX_INSTANCES * 4 * sizeof(float),
                    gl3.GL_MAP_WRITE_BIT | gl3.GL_MAP_INVALIDATE_BUFFER_BIT);
        }

        
        float[] mapOffsetBuf()
        {
            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, mVB[(int)VB.VB_OFFSET]);
            return gl3.glMapBufferRange<float>(gl3.GL_ARRAY_BUFFER,
                    0, MAX_INSTANCES * 2 * sizeof(float),
                    gl3.GL_MAP_WRITE_BIT | gl3.GL_MAP_INVALIDATE_BUFFER_BIT);
        }


        void calcSceneParams(int w, int h, float[] offsets)
        {
            ConsoleExtensions.tracei("enter calcSceneParams");

            // number of cells along the larger screen dimension
            int NCELLS_MAJOR = MAX_INSTANCES_PER_SIDE;
            // cell size in scene space
            float CELL_SIZE = 2.0f / (float)NCELLS_MAJOR;

            // Calculations are done in "landscape", i.e. assuming dim[0] >= dim[1].
            // Only at the end are values put in the opposite order if h > w.
            var dim = new[] { math.fmaxf(w, h), math.fminf(w, h) };

            var aspect = new[] { dim[0] / dim[1], dim[1] / dim[0] };
            var scene2clip = new[] { 1.0f, aspect[0] };
            var ncells = new[]{
                    NCELLS_MAJOR,
                    (int)math.floorf(NCELLS_MAJOR * aspect[1])
            };

            for (int d = 0; d < 2; d++)
            {
                float offset = -ncells[d] / NCELLS_MAJOR; // -1.0 for d=0
                for (int i = 0; i < ncells[d]; i++)
                {
                    centers[d, i] = scene2clip[d] * (CELL_SIZE * (i + 0.5f) + offset);
                }
            }

            int major = w >= h ? 0 : 1;
            int minor = w >= h ? 1 : 0;
            // outer product of centers[0] and centers[1]
            for (int i = 0; i < ncells[0]; i++)
            {
                for (int j = 0; j < ncells[1]; j++)
                {
                    int idx = i * ncells[1] + j;
                    offsets[2 * idx + major] = centers[0, i];
                    offsets[2 * idx + minor] = centers[1, j];
                }
            }

            mNumInstances = (int)(ncells[0] * ncells[1]);

            ConsoleExtensions.tracei("exit calcSceneParams, mNumInstances: ", mNumInstances);

            mScale[major] = 0.5f * CELL_SIZE * scene2clip[0];
            mScale[minor] = 0.5f * CELL_SIZE * scene2clip[1];
        }

        public void resize(int w, int h)
        {
            float[] offsets = mapOffsetBuf();
            calcSceneParams(w, h, offsets);

            gl3.glUnmapBuffer(gl3.GL_ARRAY_BUFFER);


            for (int i = 0; i < mNumInstances; i++)
            {
                mAngles[i] = (float)(stdlib_h.drand48() * TWO_PI);
                mAngularVelocity[i] = (float)(
                    MAX_ROT_SPEED * (2.0 * stdlib_h.drand48() - 1.0)
                    );
            }

            mLastFrameNs = 0;

            gl3.glViewport(0, 0, (int)w, (int)h);
        }


        int frameId = 0;
        void step()
        {
            if (frameId % 60 == 1)
                ConsoleExtensions.tracei("enter step, frameId: ", frameId);


            // http://stackoverflow.com/questions/11153334/timespec-not-found-in-time-h

            //timespec now;
            //time_h.clock_gettime(time_h.CLOCK_MONOTONIC, out now);
            //ulong nowNs = now.tv_sec * 1000000000uL + now.tv_nsec;

            ulong nowNs = 0;

            //if (mLastFrameNs > 0)
            {
                //float dt = (float)(nowNs - mLastFrameNs) * 0.000000001f;
                var dt = 0.001f;

                for (int i = 0; i < mNumInstances; i++)
                {
                    var mAnglesi = mAngles[i] + mAngularVelocity[i] * dt;
                    mAngles[i] = mAnglesi;

                    if (mAngles[i] >= TWO_PI)
                    {
                        //(*((&(__that->mAngles[num2])))) = ((&(__that->mAngles[num2])) - 6.28318548202515);
                        var mAngles_dec = mAngles[i] - TWO_PI;
                        mAngles[i] = mAngles_dec;
                    }
                    else if (mAngles[i] <= -TWO_PI)
                    {
                        var mAngles_inc = mAngles[i] + TWO_PI;
                        mAngles[i] = mAngles_inc;
                    }
                }

                float[] transforms = mapTransformBuf();
                for (int i = 0; i < mNumInstances; i++)
                {
                    float s = math.sinf(mAngles[i]);
                    float c = math.cosf(mAngles[i]);
                    transforms[4 * i + 0] = c * mScale[0];
                    transforms[4 * i + 1] = s * mScale[1];
                    transforms[4 * i + 2] = -s * mScale[0];
                    transforms[4 * i + 3] = c * mScale[1];
                }

                gl3.glUnmapBuffer(gl3.GL_ARRAY_BUFFER);
            }

            mLastFrameNs = nowNs;
        }



        public void render()
        {
            frameId++;

            step();

            gl3.glClearColor(
                (frameId % 256) / 255.0f, 0.2f, 0.3f, 1.0f);
            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT | gl3.GL_DEPTH_BUFFER_BIT);

            gl3.glUseProgram(mProgram);
            gl3.glBindVertexArray(mVBState);
            gl3.glDrawArraysInstanced(gl3.GL_TRIANGLE_STRIP, 0, 4, (int)mNumInstances);
            //checkGlError("Renderer::render");
        }


    }
}

//jni/HybridGLES3JNIActivityNDK.dll.c: In function 'Java_HybridGLES3JNIActivity_NDK_RendererES3_calcSceneParams':
//jni/HybridGLES3JNIActivityNDK.dll.c:254:28: error: expected expression before '?' token
//     singleArray6 = calloc [?][?];
//                            ^

//jni/HybridGLES3JNIActivityNDK.dll.c:309:5: error: unknown type name 'timespec'
//     timespec timespec0;
//     ^
//jni/HybridGLES3JNIActivityNDK.dll.c:317:5: error: passing argument 2 of 'clock_gettime' from incompatible pointer type [-Werror]
//     clock_gettime(1, &timespec0);
//     ^
//In file included from x:/util/android-ndk-r10e/platforms/android-21/arch-arm/usr/include/pthread.h:32:0,
//                 from jni/HybridGLES3JNIActivityNDK.dll.h:6,
//                 from jni/HybridGLES3JNIActivityNDK.dll.c:2:
//x:/util/android-ndk-r10e/platforms/android-21/arch-arm/usr/include/time.h:89:12: note: expected 'struct timespec *' but argument is of type 'int *'
// extern int clock_gettime(clockid_t, struct timespec*) __LIBC_ABI_PUBLIC__;
//            ^
