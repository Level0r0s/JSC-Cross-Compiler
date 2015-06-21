using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using ScriptCoreLibNative.SystemHeaders.GLES3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

        public const int NUM_EYES = 2;
        public const int NUM_BUFFERS = 3;
        public const int NUM_MULTI_SAMPLES = 4;

        // element of fixed dimensional array ovrRenderTexture[NUM_BUFFERS, NUM_EYES]
        struct ovrRenderTexture
        {
            public int Width;
            public int Height;
            public int Multisamples;
            public uint ColorTexture;
            public uint DepthBuffer;
            public uint FrameBuffer;

            // called by ovrRenderer_Clear
            public void ovrRenderTexture_Clear()
            {
                // 665

                // set default?
                this.Width = 0;
                this.Height = 0;
                this.Multisamples = 0;
                this.ColorTexture = 0;
                this.DepthBuffer = 0;
                this.FrameBuffer = 0;
            }

            // called by ovrRenderer_Create
            public void ovrRenderTexture_Create(int width, int height, int multisamples)
            {
                // 674

                this.Width = width;
                this.Height = height;
                this.Multisamples = multisamples;

                // http://fabiensanglard.net/quake2/quake2_opengl_renderer.php

                // Create the color buffer texture.
                gl3.glGenTextures(1, out this.ColorTexture);
                gl3.glBindTexture(gl3.GL_TEXTURE_2D, this.ColorTexture);
                gl3.glTexImage2D(gl3.GL_TEXTURE_2D, 0, gl3.GL_RGBA8, width, height, 0, gl3.GL_RGBA, gl3.GL_UNSIGNED_BYTE, null);
                gl3.glTexParameteri(gl3.GL_TEXTURE_2D, gl3.GL_TEXTURE_WRAP_S, gl3.GL_CLAMP_TO_EDGE);
                gl3.glTexParameteri(gl3.GL_TEXTURE_2D, gl3.GL_TEXTURE_WRAP_T, gl3.GL_CLAMP_TO_EDGE);
                gl3.glTexParameteri(gl3.GL_TEXTURE_2D, gl3.GL_TEXTURE_MIN_FILTER, gl3.GL_LINEAR);
                gl3.glTexParameteri(gl3.GL_TEXTURE_2D, gl3.GL_TEXTURE_MAG_FILTER, gl3.GL_LINEAR);
                gl3.glBindTexture(gl3.GL_TEXTURE_2D, 0);

                // 687

                // ?? glRenderbufferStorageMultisampleEXT
                // ?? glFramebufferTexture2DMultisampleEXT

                // Create depth buffer.
                gl3.glGenRenderbuffers(1, out this.DepthBuffer);
                gl3.glBindRenderbuffer(gl3.GL_RENDERBUFFER, this.DepthBuffer);
                gl3.glRenderbufferStorage(gl3.GL_RENDERBUFFER, gl3.GL_DEPTH_COMPONENT24, width, height);
                gl3.glBindRenderbuffer(gl3.GL_RENDERBUFFER, 0);

                // Create the frame buffer.
                gl3.glGenFramebuffers(1, out this.FrameBuffer);
                gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, this.FrameBuffer);
                gl3.glFramebufferRenderbuffer(gl3.GL_FRAMEBUFFER, gl3.GL_DEPTH_ATTACHMENT, gl3.GL_RENDERBUFFER, this.DepthBuffer);
                gl3.glFramebufferTexture2D(gl3.GL_FRAMEBUFFER, gl3.GL_COLOR_ATTACHMENT0, gl3.GL_TEXTURE_2D, this.ColorTexture, 0);

                var renderFramebufferStatus = gl3.glCheckFramebufferStatus(gl3.GL_FRAMEBUFFER);
                gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, 0);

            }

            // called by ovrRenderer_Destroy
            public void ovrRenderTexture_Destroy()
            {
                // 742

                gl3.glDeleteFramebuffers(1, ref this.FrameBuffer);
                gl3.glDeleteRenderbuffers(1, ref this.DepthBuffer);
                gl3.glDeleteTextures(1, ref this.ColorTexture);

                this.ColorTexture = 0;
                this.DepthBuffer = 0;
                this.FrameBuffer = 0;
            }

            // called by ovrRenderer_RenderFrame
            public void ovrRenderTexture_SetCurrent()
            {
                // 753
                gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, this.FrameBuffer);

            }

            // called by ovrRenderer_RenderFrame
            public static void ovrRenderTexture_SetNone()
            {
                // 758

                gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, 0);

            }

            // stackalloc? we are about to leak memory? or move it out of the method?
            static readonly int[] depthAttachment = new[] { gl3.GL_DEPTH_ATTACHMENT };


            // called by ovrRenderer_RenderFrame
            public void ovrRenderTexture_Resolve()
            {
                // 763

                // Discard the depth buffer, so the tiler won't need to write it back out to memory.


                gl3.glInvalidateFramebuffer(gl3.GL_FRAMEBUFFER, 1, depthAttachment);

                // Flush this frame worth of commands.
                gl3.glFlush();
            }
        }

        // created by ovrApp_Clear
        unsafe class ovrRenderer
        {
            // used by ovrRenderer_RenderFrame

            public readonly ovrRenderTexture[,] RenderTextures = new ovrRenderTexture[NUM_BUFFERS, NUM_EYES];

            public int BufferIndex = 0;
            public ovrMatrix4f ProjectionMatrix;
            public ovrMatrix4f TanAngleMatrix;

            public ovrRenderer()
            {
                ovrRenderer_Clear();
            }

            // called by ovrApp_Clear
            void ovrRenderer_Clear()
            {
                // 1000

                for (int i = 0; i < NUM_BUFFERS; i++)
                    for (int eye = 0; eye < NUM_EYES; eye++)
                    {
                        this.RenderTextures[i, eye].ovrRenderTexture_Clear();
                    }

                //this.BufferIndex = 0;
            }

            // called by AppThreadFunction
            public void ovrRenderer_Create(ref ovrHmdInfo hmdInfo)
            {
                ConsoleExtensions.tracei("enter ovrRenderer_Create");

                //fixed (ovrHmdInfo* hmdInfo = &refhmdInfo)
                fixed (int* hmdInfo_SuggestedEyeResolution = hmdInfo.SuggestedEyeResolution)
                fixed (float* hmdInfo_SuggestedEyeFov = hmdInfo.SuggestedEyeFov)
                {
                    // 1012
                    // Create the render Textures.
                    for (int i = 0; i < NUM_BUFFERS; i++)
                        for (int eye = 0; eye < NUM_EYES; eye++)
                        {
                            this.RenderTextures[i, eye].ovrRenderTexture_Create(
                                hmdInfo_SuggestedEyeResolution[0],
                                hmdInfo_SuggestedEyeResolution[1],
                                NUM_MULTI_SAMPLES);
                        }

                    this.BufferIndex = 0;

                    // Setup the projection matrix.
                    this.ProjectionMatrix = VrApi_Helpers.ovrMatrix4f_CreateProjectionFov(
                                                        hmdInfo_SuggestedEyeFov[0] * ((float)Math.PI / 180.0f),
                                                        hmdInfo_SuggestedEyeFov[1] * ((float)Math.PI / 180.0f),
                                                        0.0f, 0.0f, 1.0f, 0.0f);

                    this.TanAngleMatrix = VrApi_Helpers.ovrMatrix4f_TanAngleMatrixFromProjection(ref this.ProjectionMatrix);
                }

                ConsoleExtensions.tracei("exit ovrRenderer_Create");
            }


            // called by AppThreadFunction
            public void ovrRenderer_Destroy()
            {
                // 1035
                for (int i = 0; i < NUM_BUFFERS; i++)
                    for (int eye = 0; eye < NUM_EYES; eye++)
                    {
                        //  OVRVrCubeWorldSurfaceViewXNDK_VrCubeWorld_ovrRenderTexture_ovrRenderTexture_Destroy((OVRVrCubeWorldSurfaceViewXNDK_VrCubeWorld_ovrRenderTexture)(&(__that->RenderTextures[num0][num1])));

                        this.RenderTextures[i, eye].ovrRenderTexture_Destroy();
                    }

                this.BufferIndex = 0;
            }

            // sent into vrapi_SubmitFrame
            // will use glMapBufferRange
            //public ovrFrameParms ovrRenderer_RenderFrame(ref ovrApp appState, ref ovrTracking tracking)
            public ovrFrameParms ovrRenderer_RenderFrame(ovrApp appState, ref ovrTracking tracking)
            {
                //ConsoleExtensions.tracei("enter ovrRenderer_RenderFrame, VRAPI_FRAME_INIT_DEFAULT");

                // can other processes/non ndk stream a surface to us?
                // local socket?
                // shared memory?
                // editn n continue?
                // 1049

                ovrFrameParms parms = VrApi_Helpers.vrapi_DefaultFrameParms(ref appState.Java, ovrFrameInit.VRAPI_FRAME_INIT_DEFAULT, 0u);
                parms.FrameIndex = appState.FrameIndex;
                parms.MinimumVsyncs = appState.MinimumVsyncs;


                #region InstanceTransformBuffer
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appState.Scene.InstanceTransformBuffer);

                // malloc? Activator/CreateArray?


                var sizeof_ovrMatrix4f = sizeof(ovrMatrix4f);

                // I/xNativeActivity(20912): x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Renderer.cs:245 ovrRenderer_RenderFrame, glMapBufferRange sizeof_ovrMatrix4f  64 errno: 0 Success
                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, glMapBufferRange sizeof_ovrMatrix4f ", sizeof_ovrMatrix4f);

                // 
                //var cubeTransforms = (ovrMatrix4f*)gl3.glMapBufferRange(
                var cubeTransforms = gl3.glMapBufferRange<ovrMatrix4f>(
                    gl3.GL_ARRAY_BUFFER, 0,
                    // do we need marshal.getsize?
                    NUM_INSTANCES * sizeof_ovrMatrix4f,

                    // the first gl3 members, the other are gl2 apis
                    gl3.GL_MAP_WRITE_BIT | gl3.GL_MAP_INVALIDATE_BUFFER_BIT
                );

                // 1057

                for (int i = 0; i < NUM_INSTANCES; i++)
                {
                    //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ovrMatrix4f_CreateRotation i ", i);
                    
                    var rotation = VrApi_Helpers.ovrMatrix4f_CreateRotation(
                        appState.Scene.CubeRotations[i].x * appState.Simulation.CurrentRotation.x,
                        appState.Scene.CubeRotations[i].y * appState.Simulation.CurrentRotation.y,
                        appState.Scene.CubeRotations[i].z * appState.Simulation.CurrentRotation.z
                    );

                    //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ovrMatrix4f_CreateTranslation i ", i);

                    var translation = VrApi_Helpers.ovrMatrix4f_CreateTranslation(
                        appState.Scene.CubePositions[i].x,
                        appState.Scene.CubePositions[i].y,
                        appState.Scene.CubePositions[i].z
                    );

                    //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ovrMatrix4f_Multiply i ", i);
                    var transform = VrApi_Helpers.ovrMatrix4f_Multiply(ref translation, ref rotation);


                    var transpose= VrApi_Helpers.ovrMatrix4f_Transpose(ref transform);

                    // I/xNativeActivity(21998): x:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Renderer.cs:282 ovrRenderer_RenderFrame, ubeTransforms[i] = transpose  173 errno: 0 Success
                    //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ubeTransforms[i] = transpose ", i);
                    // (*((matrix4f_2 + (num3 * sizeof(ovrMatrix4f))))) = matrix4f7;
                    //(*((&(matrix4fArray2[num3])))) = matrix4f7;
                    cubeTransforms[i] = transpose; 
                }

                // 1070
                gl3.glUnmapBuffer(gl3.GL_ARRAY_BUFFER);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                #endregion


                // Calculate the center view matrix.
                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_DefaultHeadModelParms");
                var headModelParms = VrApi_Helpers.vrapi_DefaultHeadModelParms();

                var input = default(ovrMatrix4f);
                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_GetCenterEyeViewMatrix");
                var centerEyeViewMatrix = VrApi_Helpers.vrapi_GetCenterEyeViewMatrix(ref headModelParms, ref tracking, ref input);

                // 1077

                // NUM_EYES is length of RenderTextures
                for (int eye = 0; eye < NUM_EYES; eye++)
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150618/ovrmatrix4f
                    var eyeViewMatrix = VrApi_Helpers.vrapi_GetEyeViewMatrix(ref headModelParms, ref centerEyeViewMatrix, eye);



                    fixed (ovrMatrix4f* ref_ProjectionMatrix = &appState.Renderer.ProjectionMatrix)
                    fixed (ovrRenderTexture* rt = &appState.Renderer.RenderTextures[appState.Renderer.BufferIndex, eye])
                    {
                        //// 1085
                        rt->ovrRenderTexture_SetCurrent();


                        gl3.glEnable(gl3.GL_SCISSOR_TEST);
                        gl3.glDepthMask(true);
                        gl3.glEnable(gl3.GL_DEPTH_TEST);
                        gl3.glDepthFunc(gl3.GL_LEQUAL);
                        gl3.glViewport(0, 0, rt->Width, rt->Height);
                        gl3.glScissor(0, 0, rt->Width, rt->Height);
                        gl3.glClearColor(0.125f, 0.0f, 0.125f, 1.0f);
                        gl3.glClear(gl3.GL_COLOR_BUFFER_BIT | gl3.GL_DEPTH_BUFFER_BIT);

                        gl3.glUseProgram(appState.Scene.Program.Program);

                        // 1094

                        gl3.glUniformMatrix4fv(appState.Scene.Program.Uniforms[(int)ovrUniform_index.UNIFORM_VIEW_MATRIX], 1, true, (float*)&eyeViewMatrix);
                        gl3.glUniformMatrix4fv(appState.Scene.Program.Uniforms[(int)ovrUniform_index.UNIFORM_PROJECTION_MATRIX], 1, true, (float*)ref_ProjectionMatrix);

                        gl3.glBindVertexArray(appState.Scene.Cube.VertexArrayObject);
                        gl3.glDrawElementsInstanced(gl3.GL_TRIANGLES, appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, NUM_INSTANCES);
                        gl3.glBindVertexArray(0);
                        gl3.glUseProgram(0);

                        // 1104

                        // what happens if we dont?

                        // Explicitly clear the border texels to black because OpenGL-ES does not support GL_CLAMP_TO_BORDER.
                        {
                            // Clear to fully opaque black.
                            gl3.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            // bottom
                            gl3.glScissor(0, 0, rt->Width, 1);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                            // top
                            gl3.glScissor(0, rt->Height - 1, rt->Width, 1);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                            // left
                            gl3.glScissor(0, 0, 1, rt->Height);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                            // right
                            gl3.glScissor(rt->Width - 1, 0, 1, rt->Height);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                        }

                        //// 1119
                        rt->ovrRenderTexture_Resolve();


                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].TexId = rt->ColorTexture;
                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].TexCoordsFromTanAngles = appState.Renderer.TanAngleMatrix;
                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].HeadPose = tracking.HeadPose;
                    }
                }

                ovrRenderTexture.ovrRenderTexture_SetNone();

                appState.Renderer.BufferIndex = (appState.Renderer.BufferIndex + 1) % NUM_BUFFERS;

                // 1130
                //ConsoleExtensions.tracei("exit ovrRenderer_RenderFrame");
                return parms;
            }
        }



    }


}
