using com.oculus.gles3jni;
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

namespace OVRWindWheelNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

        public const int NUM_EYES = 2;
        public const int NUM_BUFFERS = 3;
        public const int NUM_MULTI_SAMPLES = 4;



        // created by ovrApp_Clear
        public unsafe class ovrRenderer
        {
            // used by ovrRenderer_RenderFrame

            public readonly ovrRenderTexture[,] RenderTextures = new ovrRenderTexture[NUM_BUFFERS, NUM_EYES];

            // incremented by ovrRenderer_RenderFrame
            public int BufferIndex = 0;

            // set by ovrMatrix4f_CreateProjectionFov
            public ovrMatrix4f ProjectionMatrix;

            // set by ovrMatrix4f_TanAngleMatrixFromProjection
            public ovrMatrix4f TanAngleMatrix;

            public ovrRenderer()
            {
                // 1000
                // called by ovrApp_Clear?
                ConsoleExtensions.trace("enter ovrRenderer ctor, call ovrRenderTexture_Clear");

                for (int i = 0; i < NUM_BUFFERS; i++)
                    for (int eye = 0; eye < NUM_EYES; eye++)
                    {
                        //this.RenderTextures[i, eye] = default(ovrRenderTexture);
                        this.RenderTextures[i, eye].ovrRenderTexture_Clear();
                    }
            }

            // vrapi_GetHmdInfo
            // called by AppThreadFunction
            public void ovrRenderer_Create(ref ovrHmdInfo hmdInfo)
            {
                ConsoleExtensions.trace("enter ovrRenderer_Create");

                fixed (int* hmdInfo_SuggestedEyeResolution = hmdInfo.SuggestedEyeResolution)
                fixed (float* hmdInfo_SuggestedEyeFov = hmdInfo.SuggestedEyeFov)
                {
                    //I/xNativeActivity(26905): \VrCubeWorld.Renderer.cs:89 hmdInfo_SuggestedEyeResolution width  1024
                    //I/xNativeActivity(26905): \VrCubeWorld.Renderer.cs:90 hmdInfo_SuggestedEyeResolution height  1024
                    //I/xNativeActivity(26905): \VrCubeWorld.Renderer.cs:91 NUM_MULTI_SAMPLES  4
                    //I/xNativeActivity(26905): \VrCubeWorld.Renderer.cs:95 hmdInfo_SuggestedEyeFov[0]  90
                    //I/xNativeActivity(26905): \VrCubeWorld.Renderer.cs:96 hmdInfo_SuggestedEyeFov[1]  90
                    //I/xNativeActivity(26905): \VrCubeWorld.Renderer.cs:97 DisplayRefreshRate  60


                    ConsoleExtensions.tracei("hmdInfo_SuggestedEyeResolution width ", hmdInfo_SuggestedEyeResolution[0]);
                    ConsoleExtensions.tracei("hmdInfo_SuggestedEyeResolution height ", hmdInfo_SuggestedEyeResolution[1]);
                    ConsoleExtensions.tracei("NUM_MULTI_SAMPLES ", NUM_MULTI_SAMPLES);


                    ConsoleExtensions.tracei("hmdInfo_SuggestedEyeFov[0] ", (int)hmdInfo_SuggestedEyeFov[0]);
                    ConsoleExtensions.tracei("hmdInfo_SuggestedEyeFov[1] ", (int)hmdInfo_SuggestedEyeFov[1]);
                    ConsoleExtensions.tracei("DisplayRefreshRate ", (int)hmdInfo.DisplayRefreshRate);


                    // 1012
                    // Create the render Textures.
                    for (int i = 0; i < NUM_BUFFERS; i++)
                        for (int eye = 0; eye < NUM_EYES; eye++)
                        {
                            //ConsoleExtensions.tracei("call ovrRenderTexture_Create i ", i);

                            this.RenderTextures[i, eye].ovrRenderTexture_Create(
                                hmdInfo_SuggestedEyeResolution[0],
                                hmdInfo_SuggestedEyeResolution[1],
                                NUM_MULTI_SAMPLES,

                                i, eye
                            );
                        }

                    this.BufferIndex = 0;

                    // https://www.kickstarter.com/projects/wearality/wearality-sky-limitless-vr
                    // jsc first pass should be a diff, to see if UDP patch can be issued instead of full build.

                    // Setup the projection matrix.
                    this.ProjectionMatrix = VrApi_Helpers.ovrMatrix4f_CreateProjectionFov(
                        hmdInfo_SuggestedEyeFov[0] * ((float)Math.PI / 180.0f),
                        hmdInfo_SuggestedEyeFov[1] * ((float)Math.PI / 180.0f),
                        0.0f, 0.0f,
                        1.0f, 0.0f
                    );

                    this.TanAngleMatrix = VrApi_Helpers.ovrMatrix4f_TanAngleMatrixFromProjection(ref this.ProjectionMatrix);
                }

                //ConsoleExtensions.trace("exit ovrRenderer_Create");
            }

            // Dispose
            // called by AppThreadFunction
            public void ovrRenderer_Destroy()
            {
                // 1035
                for (int i = 0; i < NUM_BUFFERS; i++)
                    for (int eye = 0; eye < NUM_EYES; eye++)
                    {
                        this.RenderTextures[i, eye].ovrRenderTexture_Destroy();
                    }

                this.BufferIndex = 0;
            }

            // sent into vrapi_SubmitFrame
            // will use glMapBufferRange
            //public ovrFrameParms ovrRenderer_RenderFrame(ref ovrApp appState, ref ovrTracking tracking)
            //public ovrFrameParms ovrRenderer_RenderFrame(ovrAppThread appThread, ovrApp appState, ref ovrTracking tracking0)
            //public ovrFrameParms ovrRenderer_RenderFrame(ovrAppThread appThread, ref ovrTracking tracking0)
            public ovrFrameParms ovrRenderer_RenderFrame(ovrAppThread appThread)
            {
                // X:\jsc.svn\examples\javascript\WebGL\WebGLSpadeWarrior\WebGLSpadeWarrior\Application.cs

                //ConsoleExtensions.tracei("enter ovrRenderer_RenderFrame, VRAPI_FRAME_INIT_DEFAULT");

                // can other processes/non ndk stream a surface to us?
                // local socket?
                // shared memory?
                // editn n continue?
                // 1049

                ovrFrameParms parms = VrApi_Helpers.vrapi_DefaultFrameParms(ref appThread.appState.Java, ovrFrameInit.VRAPI_FRAME_INIT_DEFAULT, 0u);
                parms.FrameIndex = appThread.appState.FrameIndex;
                parms.MinimumVsyncs = appThread.appState.MinimumVsyncs;


                var scale = 0.2f *
                    (1.0f + (float)Math.Sin(appThread.appState.FrameIndex * 0.05f) * 0.2f);


                // for gearvr we get do pre set all cubes?
                #region InstanceTransformBuffer
                var sizeof_ovrMatrix4f = sizeof(ovrMatrix4f);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appThread.appState.Scene.InstanceTransformBuffer);

                var gpuInstanceTransformBuffer = gl3.glMapBufferRange<ovrMatrix4f>(
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


                    //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ovrMatrix4f_CreateTranslation i ", i);

                    // VR thread wants to know where our stuff is. lowframe thread changes data it may look choppy.
                    //var translation = VrApi_Helpers.ovrMatrix4f_CreateTranslation(
                    //    appState.Scene.CubePositions[i].x,
                    //    appState.Scene.CubePositions[i].y,
                    //    appState.Scene.CubePositions[i].z
                    //);


                    var rfloor = (i % floors);







                    //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ovrMatrix4f_Multiply i ", i);
                    //var transform = VrApi_Helpers.ovrMatrix4f_Multiply(ref translation, ref rotation);

                    //var scale2 = default(ovrMatrix4f);

                    //fixed (float* ff = scale2.M)

                    //scale2.m
                    //{
                    //    transform.M[0 + 4 * 0] = 2;

                    // glsl program not set up do do the scaling?
                    //translation.M[1 + 4 * 1] = 0.002f;
                    //translation.M[2 + 4 * 2] = 0.002f;
                    //translation.M[3 + 4 * 3] = 0.002f;
                    //}

                    if (rfloor == 0)
                    {
                        var translation = __ovrMatrix4f.CreateTranslation(
                            appThread.appState.Scene.CubePositions[i].x,
                            appThread.appState.Scene.CubePositions[i].y,
                            appThread.appState.Scene.CubePositions[i].z,

                            //scale: 1.0f
                            scale: 0.2f
                        );

                        gpuInstanceTransformBuffer[i] = __ovrMatrix4f.Transpose(&translation);
                    }
                    else
                    {
                        var translation = __ovrMatrix4f.CreateTranslation(
                            appThread.appState.Scene.CubePositions[i].x,
                            appThread.appState.Scene.CubePositions[i].y

                            + GLES3JNILib.fields_mousewheel

                            //* (1.0f + (GLES3JNILib.fields_mousewheel * 0.5f))

                            ,

                            //  moves left to right
                            appThread.appState.Scene.CubePositions[i].z,
                            //appThread.appState.Scene.CubePositions[i].z + GLES3JNILib.fields_mousewheel,

                            scale:
                            scale
                            //* (1.0f + (GLES3JNILib.fields_mousewheel * 0.1f))

                        );

                        var rotation = __ovrMatrix4f.CreateRotation(
                            0,
                            (float)Math.Cos(appThread.appState.FrameIndex * 0.05f)
                            ,
                            0
                            //appState.Scene.CubeRotations[i].x * appState.Simulation.CurrentRotation.x,
                            //appState.Scene.CubeRotations[i].y * appState.Simulation.CurrentRotation.y,
                            //appState.Scene.CubeRotations[i].z * appState.Simulation.CurrentRotation.z
                        );

                        var transform2 = __ovrMatrix4f.Multiply(&translation, &rotation);

                        // what does it do??um. it messes up the display otherwise?
                        //var transpose = VrApi_Helpers.ovrMatrix4f_Transpose(ref translation);
                        var transpose = __ovrMatrix4f.Transpose(&transform2);

                        //transpose.M[0 + 4 * 0] = 2;
                        //transpose.M[1 + 4 * 1] = 2;
                        //transpose.M[2 + 4 * 2] = 2;

                        //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ubeTransforms[i] = transpose ", i);

                        // cuz landscape/portrait???
                        gpuInstanceTransformBuffer[i] = transpose;
                    }
                    //gpuInstanceTransformBuffer[i] = translation;
                }

                // 1070
                gl3.glUnmapBuffer(gl3.GL_ARRAY_BUFFER);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                #endregion


                // Calculate the center view matrix.
                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_DefaultHeadModelParms");
                var headModelParms = VrApi_Helpers.vrapi_DefaultHeadModelParms();


                //tracking.HeadPose.Pose.Orientation.y += appState.FrameIndex * 0.05f;

                //var tracking1 = appThread.tracking;

                // no thats not any better
                //tracking1.HeadPose.Pose.Orientation.x += GLES3JNILib.fields_mousey * 0.005f;

                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_GetCenterEyeViewMatrix");

                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150703/mousex
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150705
                var centerEyeViewMatrix0 = VrApi_Helpers.vrapi_GetCenterEyeViewMatrix(ref headModelParms, ref appThread.tracking, default(ovrMatrix4f*));
                var centerEyeViewMatrix1 = __ovrMatrix4f.CreateRotation(0, GLES3JNILib.fields_mousex * 0.005f, 0);
                var centerEyeViewMatrix = __ovrMatrix4f.Multiply(centerEyeViewMatrix0, centerEyeViewMatrix1);

                //__ovrMatrix4f __centerEyeViewMatrix1 = &centerEyeViewMatrix1;


                //var centerEyeViewMatrix = __centerEyeViewMatrix0.Multiply(__centerEyeViewMatrix1);
                //var centerEyeViewMatrix = __ovrMatrix4f.Multiply(__centerEyeViewMatrix0, __centerEyeViewMatrix1);
                //var centerEyeViewMatrix = __ovrMatrix4f.Multiply(&centerEyeViewMatrix0, &centerEyeViewMatrix1);
                //var centerEyeViewMatrix = __ovrMatrix4f.Multiply(&centerEyeViewMatrix0, &centerEyeViewMatrix1);
                //var centerEyeViewMatrix = __ovrMatrix4f.Multiply(ref centerEyeViewMatrix0, ref centerEyeViewMatrix1);

                //var centerEyeViewMatrix = VrApi_Helpers.ovrMatrix4f_Multiply(
                //    ref centerEyeViewMatrix0,
                //    ref centerEyeViewMatrix1
                //);

                // 1077

                // NUM_EYES is length of RenderTextures
                for (int eye = 0; eye < NUM_EYES; eye++)
                {
                    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150618/ovrmatrix4f
                    var eyeViewMatrix = VrApi_Helpers.vrapi_GetEyeViewMatrix(ref headModelParms, ref centerEyeViewMatrix, eye);

                    fixed (ovrMatrix4f* ref_ProjectionMatrix = &appThread.appState.Renderer.ProjectionMatrix)
                    fixed (ovrRenderTexture* RenderTexture = &appThread.appState.Renderer.RenderTextures[appThread.appState.Renderer.BufferIndex, eye])
                    {
                        //// 1085
                        //appState.tracei60("ovrRenderer_RenderFrame, ovrRenderTexture_SetCurrent BufferIndex ", appState.Renderer.BufferIndex);
                        //appState.tracei60("ovrRenderer_RenderFrame, ovrRenderTexture_SetCurrent eye ", eye);

                        // using?
                        gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, RenderTexture->FrameBuffer);


                        gl3.glEnable(gl3.GL_SCISSOR_TEST);
                        gl3.glDepthMask(true);
                        gl3.glEnable(gl3.GL_DEPTH_TEST);
                        gl3.glDepthFunc(gl3.GL_LEQUAL);
                        gl3.glViewport(0, 0, RenderTexture->Width, RenderTexture->Height);
                        gl3.glScissor(0, 0, RenderTexture->Width, RenderTexture->Height);
                        //gl3.glClearColor(0.125f, 0.0f, 0.125f, 1.0f);
                        //gl3.glClearColor(0.9f, 0.0f, 0.125f, 1.0f);

                        // 
                        //gl3.glClearColor(0.0f, 0.0f, 1.0f, 0.1f);
                        gl3.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
                        gl3.glClear(gl3.GL_COLOR_BUFFER_BIT | gl3.GL_DEPTH_BUFFER_BIT);

                        gl3.glUseProgram(appThread.appState.Scene.Program.Program);

                        // 1094

                        gl3.glUniformMatrix4fv(appThread.appState.Scene.Program.Uniforms[(int)ovrUniform_index.UNIFORM_VIEW_MATRIX], 1, true, (float*)&eyeViewMatrix);
                        gl3.glUniformMatrix4fv(appThread.appState.Scene.Program.Uniforms[(int)ovrUniform_index.UNIFORM_PROJECTION_MATRIX], 1, true, (float*)ref_ProjectionMatrix);

                        gl3.glBindVertexArray(appThread.appState.Scene.Cube.VertexArrayObject);
                        gl3.glDrawElementsInstanced(gl3.GL_TRIANGLES, appThread.appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, NUM_INSTANCES);
                        gl3.glBindVertexArray(0);

                        gl3.glUseProgram(0);

                        // 1104

                        // what happens if we dont?

                        #region Explicitly clear the border texels to black because OpenGL-ES does not support GL_CLAMP_TO_BORDER.
                        {
                            // Clear to fully opaque black.
                            gl3.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
                            // bottom
                            gl3.glScissor(0, 0, RenderTexture->Width, 1);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                            // top
                            gl3.glScissor(0, RenderTexture->Height - 1, RenderTexture->Width, 1);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                            // left
                            gl3.glScissor(0, 0, 1, RenderTexture->Height);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                            // right
                            gl3.glScissor(RenderTexture->Width - 1, 0, 1, RenderTexture->Height);
                            gl3.glClear(gl3.GL_COLOR_BUFFER_BIT);
                        }
                        #endregion

                        //// 1119
                        #region ovrRenderTexture_Resolve
                        // 763
                        // Discard the depth buffer, so the tiler won't need to write it back out to memory.
                        gl3.glInvalidateFramebuffer(gl3.GL_FRAMEBUFFER, 1, depthAttachment);
                        // Flush this frame worth of commands.
                        gl3.glFlush();
                        #endregion



                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].TexId = RenderTexture->ColorTexture;
                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].TexCoordsFromTanAngles = appThread.appState.Renderer.TanAngleMatrix;
                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].HeadPose = appThread.tracking.HeadPose;
                    }
                }

                gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, 0);

                appThread.appState.Renderer.BufferIndex = (appThread.appState.Renderer.BufferIndex + 1) % NUM_BUFFERS;

                // 1130
                //appState.tracei60("exit ovrRenderer_RenderFrame BufferIndex", appState.Renderer.BufferIndex);
                //ConsoleExtensions.tracei("exit ovrRenderer_RenderFrame");
                return parms;
            }

            static readonly int[] depthAttachment = new[] { gl3.GL_DEPTH_ATTACHMENT };

        }



    }


}
