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



        // created by ovrApp_Clear
        unsafe class ovrRenderer
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

                    // Setup the projection matrix.
                    this.ProjectionMatrix = VrApi_Helpers.ovrMatrix4f_CreateProjectionFov(
                                                        hmdInfo_SuggestedEyeFov[0] * ((float)Math.PI / 180.0f),
                                                        hmdInfo_SuggestedEyeFov[1] * ((float)Math.PI / 180.0f),
                                                        0.0f, 0.0f, 1.0f, 0.0f);

                    this.TanAngleMatrix = VrApi_Helpers.ovrMatrix4f_TanAngleMatrixFromProjection(ref this.ProjectionMatrix);
                }

                //ConsoleExtensions.trace("exit ovrRenderer_Create");
            }


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
                var sizeof_ovrMatrix4f = sizeof(ovrMatrix4f);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appState.Scene.InstanceTransformBuffer);
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


                    var transpose = VrApi_Helpers.ovrMatrix4f_Transpose(ref transform);

                    //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, ubeTransforms[i] = transpose ", i);
                    cubeTransforms[i] = transpose;
                }

                // 1070
                gl3.glUnmapBuffer(gl3.GL_ARRAY_BUFFER);
                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                #endregion


                // Calculate the center view matrix.
                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_DefaultHeadModelParms");
                var headModelParms = VrApi_Helpers.vrapi_DefaultHeadModelParms();

                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_GetCenterEyeViewMatrix");
                var centerEyeViewMatrix = VrApi_Helpers.vrapi_GetCenterEyeViewMatrix(ref headModelParms, ref tracking, default(ovrMatrix4f*));

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
                        //appState.tracei60("ovrRenderer_RenderFrame, ovrRenderTexture_SetCurrent BufferIndex ", appState.Renderer.BufferIndex);
                        //appState.tracei60("ovrRenderer_RenderFrame, ovrRenderTexture_SetCurrent eye ", eye);

                        rt->ovrRenderTexture_SetCurrent();


                        gl3.glEnable(gl3.GL_SCISSOR_TEST);
                        gl3.glDepthMask(true);
                        gl3.glEnable(gl3.GL_DEPTH_TEST);
                        gl3.glDepthFunc(gl3.GL_LEQUAL);
                        gl3.glViewport(0, 0, rt->Width, rt->Height);
                        gl3.glScissor(0, 0, rt->Width, rt->Height);
                        //gl3.glClearColor(0.125f, 0.0f, 0.125f, 1.0f);
                        //gl3.glClearColor(0.9f, 0.0f, 0.125f, 1.0f);
                        gl3.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
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

                        #region Explicitly clear the border texels to black because OpenGL-ES does not support GL_CLAMP_TO_BORDER.
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
                        #endregion

                        //// 1119
                        //appState.tracei60("ovrRenderer_RenderFrame, ovrRenderTexture_Resolve, glInvalidateFramebuffer");
                        rt->ovrRenderTexture_Resolve();


                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].TexId = rt->ColorTexture;
                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].TexCoordsFromTanAngles = appState.Renderer.TanAngleMatrix;
                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].HeadPose = tracking.HeadPose;
                    }
                }

                ovrRenderTexture.ovrRenderTexture_SetNone();

               
                appState.Renderer.BufferIndex = (appState.Renderer.BufferIndex + 1) % NUM_BUFFERS;

                // 1130
                appState.tracei60("exit ovrRenderer_RenderFrame BufferIndex", appState.Renderer.BufferIndex);
                //ConsoleExtensions.tracei("exit ovrRenderer_RenderFrame");
                return parms;
            }
        }



    }


}
