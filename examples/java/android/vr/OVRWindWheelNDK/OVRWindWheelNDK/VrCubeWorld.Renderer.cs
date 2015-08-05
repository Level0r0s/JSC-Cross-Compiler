using com.oculus.gles3jni;
using OVRWindWheelNDK.Library;
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

                    ConsoleExtensions.trace("call CreateProjectionFov");

                    // Setup the projection matrix.
                    this.ProjectionMatrix = __Matrix4x4.CreateProjectionFov(
                        hmdInfo_SuggestedEyeFov[0] * ((float)Math.PI / 180.0f),
                        hmdInfo_SuggestedEyeFov[1] * ((float)Math.PI / 180.0f),
                        0.0f, 0.0f,
                        //nearZ: 1.0f, 
                        //nearZ: 2.0f,
                        //nearZ: 0.5f,

                        // this feels close
                        nearZ: 0.01f,

                        //farZ: 0.1f
                        farZ: 0.0f
                    );

                    ConsoleExtensions.trace("call TanAngleMatrixFromProjection");
                    //this.TanAngleMatrix = VrApi_Helpers.ovrMatrix4f_TanAngleMatrixFromProjection(ref this.ProjectionMatrix);
                    this.TanAngleMatrix = __Matrix4x4.TanAngleMatrixFromProjection(this.ProjectionMatrix);
                    ConsoleExtensions.trace("after call TanAngleMatrixFromProjection");
                }

                ConsoleExtensions.trace("exit ovrRenderer_Create");
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










            mat4stack256 mvMatrixStack = new mat4stack256 { };

            // sent into vrapi_SubmitFrame
            // will use glMapBufferRange

            // no malloc for this method!
            public ovrFrameParms ovrRenderer_RenderFrame(ovrAppThread appThread)
            {
                // http://www.informit.com/articles/article.aspx?p=2033340&seqNum=2
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


                var scale0 = 1.5f;

                var scale = scale0 *
                    (1.0f + (float)Math.Sin(appThread.appState.FrameIndex * 0.05f) * 0.15f);











                // for gearvr we get do pre set all cubes?


                // Calculate the center view matrix.
                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_DefaultHeadModelParms");


                //tracking.HeadPose.Pose.Orientation.y += appState.FrameIndex * 0.05f;

                //var tracking1 = appThread.tracking;

                // no thats not any better
                //tracking1.HeadPose.Pose.Orientation.x += GLES3JNILib.fields_mousey * 0.005f;

                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vrapi_GetCenterEyeViewMatrix");

                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150703/mousex
                // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150705

                // __mat4.rotate(pMatrix, pMatrix, degToRad(-mx), new float[] { 0, 1f, 0f });
                var centerEyeViewMatrix1 = __Matrix4x4.CreateRotation(0, GLES3JNILib.fields_mousex * 0.005f, 0);

                // ovrMatrix4f_Inverse
                //var centerEyeViewMatrix = VrApi_Helpers.vrapi_GetCenterEyeViewMatrix(ref appThread.headModelParms, ref appThread.tracking, ref centerEyeViewMatrix1);

                var centerEyeViewMatrix0 = VrApi_Helpers.vrapi_GetCenterEyeViewMatrix(ref appThread.headModelParms, ref appThread.tracking, input: null);


                //var centerEyeViewMatrix2 = __ovrMatrix4f.CreateTranslation(0, 0, (float)Math.Sin(appThread.appState.FrameIndex * 0.05f));
                //var centerEyeViewMatrix2 = __ovrMatrix4f.CreateTranslation(0, (float)Math.Sin(appThread.appState.FrameIndex * 0.01f), 0);
                var centerEyeViewMatrix2 = __Matrix4x4.CreateTranslation(

                    ovrScene.wasd_x0
                    ,
                    //(float)(0.1f * Math.Sin(appThread.appState.FrameIndex * 0.01f))
                    //+ 
                    ovrScene.wasd_z0

                    ,


                    ovrScene.wasd_y0
                    );

                //var centerEyeViewMatrix = __ovrMatrix4f.Multiply(centerEyeViewMatrix0, centerEyeViewMatrix1, centerEyeViewMatrix2);
                var centerEyeViewMatrix = __Matrix4x4.Multiply(
                    __Matrix4x4.Multiply(centerEyeViewMatrix0, centerEyeViewMatrix1),
                    centerEyeViewMatrix2);

                // 1077

                // NUM_EYES is length of RenderTextures
                for (int eye = 0; eye < NUM_EYES; eye++)
                {
                    fixed (ovrRenderTexture* RenderTexture = &appThread.appState.Renderer.RenderTextures[appThread.appState.Renderer.BufferIndex, eye])
                    {
                        // jni/OVRWindWheelNDK.dll.c:1095:197: error: invalid type argument of '->' (have 'struct tag_Java_com_oculus_gles3jni_GLES3JNILib_ByteArrayWithLength')

                        var loc_vertexTransform = GLES3JNILib.fields_vertexTransform;

                        //// 1085
                        //appState.tracei60("ovrRenderer_RenderFrame, ovrRenderTexture_SetCurrent BufferIndex ", appState.Renderer.BufferIndex);
                        //appState.tracei60("ovrRenderer_RenderFrame, ovrRenderTexture_SetCurrent eye ", eye);
                        //appThread.appState.tracei60("ovrRenderer_RenderFrame, vertexTransform.Length: ", loc_vertexTransform.BufferLength);

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
                        //gl3.glClearColor(0.0f, 0.0f, Math.Abs(appThread.tracking.HeadPose.Pose.Orientation.z + 0.7f) / 1.4f, 0.0f);
                        //gl3.glClearColor(0.0f, 0.0f, Math.Abs(appThread.tracking.HeadPose.Pose.Orientation.x + 0.7f) / 1.4f, 0.0f);
                        //gl3.glClearColor(0.0f, 0.0f, Math.Abs(appThread.tracking.HeadPose.Pose.Orientation.x) / 1.00f, 0.0f);
                        gl3.glClear(gl3.GL_COLOR_BUFFER_BIT | gl3.GL_DEPTH_BUFFER_BIT);

                        // using
                        gl3.glUseProgram(appThread.appState.Scene.Program.Program);

                        // 1094


                        // thisone is pretty static.
                        // 	uniform mat4 ProjectionMatrix; 
                        //gl3.glUniformMatrix4fv(appThread.appState.Scene.Program.Uniforms[(int)ovrUniform_index.UNIFORM_PROJECTION_MATRIX], 1, true, (float*)ref_ProjectionMatrix);
                        gl3.glUniformMatrix4fv(appThread.appState.Scene.Program.Uniforms[(int)ovrUniform_index.UNIFORM_PROJECTION_MATRIX], 1, true, ref appThread.appState.Renderer.ProjectionMatrix);




                        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150618/ovrmatrix4f
                        var eyeViewMatrix = __Matrix4x4.GetEyeViewMatrix(ref  appThread.headModelParms, ref centerEyeViewMatrix, eye);

                        // 	uniform mat4 ViewMatrix; 
                        gl3.glUniformMatrix4fv(appThread.appState.Scene.Program.Uniforms[(int)ovrUniform_index.UNIFORM_VIEW_MATRIX], 1, true, ref eyeViewMatrix);




                        // well at this point we should be able to select the type of cube we are about to draw...


                        var red_or_green = (appThread.appState.FrameIndex / 60) % 3;

                        // we are essentially reseting the tempplate here. udp could help us here.
                        if (red_or_green == 0)
                            appThread.appState.Scene.Cube.BindBufferData_red();
                        else if (red_or_green == 1)
                            appThread.appState.Scene.Cube.BindBufferData_green();
                        else
                            appThread.appState.Scene.Cube.BindBufferData_yellow();



                        #region InstanceTransformBuffer
                        var sizeof_ovrMatrix4f = sizeof(ovrMatrix4f);
                        gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appThread.appState.Scene.InstanceTransformBuffer0);

                        var gpuInstanceTransformBuffer = gl3.glMapBufferRange<ovrMatrix4f>(
                            gl3.GL_ARRAY_BUFFER, 0,
                            // do we need marshal.getsize?
                            NUM_INSTANCES * sizeof_ovrMatrix4f,

                            // the first gl3 members, the other are gl2 apis
                            gl3.GL_MAP_WRITE_BIT | gl3.GL_MAP_INVALIDATE_BUFFER_BIT
                        );

                        // 1057

                        // what if we frame update only half of em?
                        //for (int i = 0; i < NUM_INSTANCES; i++)

                        // they dissapear!

                        //var dynamiccount = NUM_INSTANCES / 2 + (appThread.appState.FrameIndex / (60 * 5)) % (NUM_INSTANCES / 2);
                        var dynamiccount = NUM_INSTANCES;

                        for (int i = 0; i < dynamiccount; i++)
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
                                var translation = __Matrix4x4.CreateTranslation(
                                    appThread.appState.Scene.CubePositions[i].x,
                                    appThread.appState.Scene.CubePositions[i].y,
                                    appThread.appState.Scene.CubePositions[i].z,

                                    //scale: 1.0f
                                    //scale: 0.15f
                                    scale: scale0
                                );

                                gpuInstanceTransformBuffer[i] = __Matrix4x4.Transpose(&translation);
                            }
                            else
                            {
                                var translation = __Matrix4x4.CreateTranslation(
                                    appThread.appState.Scene.CubePositions[i].x,
                                    appThread.appState.Scene.CubePositions[i].y

                                    + GLES3JNILib.fields_mousewheel * 0.1f

                                    //* (1.0f + (GLES3JNILib.fields_mousewheel * 0.5f))

                                    ,

                                    //  moves left to right
                                    appThread.appState.Scene.CubePositions[i].z,
                                    //appThread.appState.Scene.CubePositions[i].z + GLES3JNILib.fields_mousewheel,

                                    scale:
                                    scale
                                    //* (1.0f + (GLES3JNILib.fields_mousewheel * 0.1f))

                                );

                                var rotation = __Matrix4x4.CreateRotation(
                                    0,
                                    (float)Math.Cos(appThread.appState.FrameIndex * 0.05f)
                                    ,
                                    0
                                    //appState.Scene.CubeRotations[i].x * appState.Simulation.CurrentRotation.x,
                                    //appState.Scene.CubeRotations[i].y * appState.Simulation.CurrentRotation.y,
                                    //appState.Scene.CubeRotations[i].z * appState.Simulation.CurrentRotation.z
                                );

                                var transform2 = __Matrix4x4.Multiply(&translation, &rotation);

                                // what does it do??um. it messes up the display otherwise?
                                //var transpose = VrApi_Helpers.ovrMatrix4f_Transpose(ref translation);
                                var transpose = __Matrix4x4.Transpose(&transform2);

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

                        // whats the next buffer to be bound?
                        gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                        #endregion


                        gl3.glBindVertexArray(appThread.appState.Scene.Cube.VertexArrayObject0);
                        gl3.glDrawElementsInstanced(gl3.GL_TRIANGLES, appThread.appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, NUM_INSTANCES);
                        // gl3.glDrawElementsInstanced(gl3.GL_TRIANGLES, appThread.appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, (int)dynamiccount);
                        gl3.glBindVertexArray(0);
                        // landscape done...




                        //var mvMatrixStack = default(mat4stack256);



                        //Action<>

                        // whats the memory going to do?
                        #region specialDraw
                        Action_ffff specialDraw = (x, y, z, xyzscale) =>
                            {
                                #region special draw
                                appThread.appState.Scene.Cube.BindBufferData_yellow();

                                //gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appThread.appState.Scene.InstanceTransformBuffer1);
                                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appThread.appState.Scene.InstanceTransformBuffer0);


                                var gpuInstanceTransformBuffer1 = gl3.glMapBufferRange<ovrMatrix4f>(
                                       gl3.GL_ARRAY_BUFFER, 0,
                                    // do we need marshal.getsize?
                                       NUM_INSTANCES1 * sizeof_ovrMatrix4f,

                                       // the first gl3 members, the other are gl2 apis
                                       gl3.GL_MAP_WRITE_BIT | gl3.GL_MAP_INVALIDATE_BUFFER_BIT
                                   );

                                var translation1 = __Matrix4x4.CreateTranslation(
                                    x: x,
                                    y: y,
                                    z: z,
                                     scale: xyzscale
                                 );

                                gpuInstanceTransformBuffer1[0] = __Matrix4x4.Transpose(&translation1);


                                gl3.glUnmapBuffer(gl3.GL_ARRAY_BUFFER);


                                // would we know how to prep and use multiple distinct VertexArrayObjects ?
                                gl3.glBindVertexArray(appThread.appState.Scene.Cube.VertexArrayObject0);
                                gl3.glDrawElementsInstanced(gl3.GL_TRIANGLES, appThread.appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, (int)1);
                                gl3.glBindVertexArray(0);


                                // whats the next buffer to be bound?
                                gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                                #endregion
                            };
                        #endregion



                        // would be cool if we could use generics someday. jsc rewriter would need to bake em?
                        specialDraw(10, 0, 20, 8f * 8);


                        // https://twitter.com/sfnet_ops/status/621859945487581184
                        // sf.net is down. yikes.
                        specialDraw(-4, 0, 4, scale);
                        specialDraw(-4, 1, 4, scale);
                        specialDraw(-4 - .4f, 1, 4, scale);
                        specialDraw(-4 + .4f, 1, 4, scale);



                        #region UDPDraw
                        Action UDPDraw = () =>
                        {
                            #region special draw
                            appThread.appState.Scene.Cube.BindBufferData_yellow();
                            //appThread.appState.Scene.Cube.BindBufferData_red();

                            //gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appThread.appState.Scene.InstanceTransformBuffer1);
                            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, appThread.appState.Scene.InstanceTransformBuffer0);



                            var UDPMatrixCount = loc_vertexTransform.BufferLength / 64;

                            //appThread.appState.tracei60("ovrRenderer_RenderFrame, vertexTransform UDPMatrixCount: ", UDPMatrixCount);

                            var gpuInstanceTransformBuffer1 = gl3.glMapBufferRange<ovrMatrix4f>(
                                   gl3.GL_ARRAY_BUFFER, 0,
                                // do we need marshal.getsize?
                                   UDPMatrixCount * sizeof_ovrMatrix4f,

                                   // the first gl3 members, the other are gl2 apis
                                   gl3.GL_MAP_WRITE_BIT | gl3.GL_MAP_INVALIDATE_BUFFER_BIT
                               );


                            // implictop from stack
                            //ovrMatrix4f current = mvMatrixStack;
                            //var current = mvMatrixStack.Peek();



                            for (int UDPMatrixIndex = 0; UDPMatrixIndex < UDPMatrixCount; UDPMatrixIndex++)
                            {
                                //appThread.appState.tracei60("ovrRenderer_RenderFrame, vertexTransform UDPMatrixIndex: ", UDPMatrixIndex);
                                //ConsoleExtensions.tracei("ovrRenderer_RenderFrame, vertexTransform UDPMatrixIndex: ", UDPMatrixIndex);


                                //var xyz = __Matrix4x4.CreateTranslation(
                                //  x: -4 + .4f + (appThread.appState.FrameIndex % 60) * 0.01f,

                                //  y: 0,

                                //  // can we make it float?
                                //  z: 3,

                                //  // or animate its size?
                                //    scale: 1.0f
                                //);

                                var xyz = default(ovrMatrix4f);

                                __Matrix4x4.Set(ref xyz, loc_vertexTransform.Buffer, UDPMatrixIndex);

                                gpuInstanceTransformBuffer1[UDPMatrixIndex] = __Matrix4x4.Transpose(&xyz);
                            }

                            //appThread.appState.tracei60("ovrRenderer_RenderFrame UDPDraw lets use loc_vertexTransform");
                            //ConsoleExtensions.trace("ovrRenderer_RenderFrame UDPDraw lets use loc_vertexTransform");

                            gl3.glUnmapBuffer(gl3.GL_ARRAY_BUFFER);

                            // would we know how to prep and use multiple distinct VertexArrayObjects ?
                            gl3.glBindVertexArray(appThread.appState.Scene.Cube.VertexArrayObject0);
                            //gl3.glDrawElementsInstanced(gl3.GL_LINES, appThread.appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, (int)UDPMatrixCount);
                            //gl3.glDrawElementsInstanced(gl3.GL_LINE_LOOP, appThread.appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, (int)UDPMatrixCount);
                            gl3.glDrawElementsInstanced(gl3.GL_TRIANGLES, appThread.appState.Scene.Cube.IndexCount, gl3.GL_UNSIGNED_SHORT, null, (int)UDPMatrixCount);
                            gl3.glBindVertexArray(0);


                            // whats the next buffer to be bound?
                            gl3.glBindBuffer(gl3.GL_ARRAY_BUFFER, 0);
                            #endregion
                        };
                        #endregion


                        //// using?

                        ////var mvMatrix0 = __Matrix4x4.CreateTranslation(
                        ////  x: -4 + .4f + (appThread.appState.FrameIndex % 60) * 0.01f,

                        ////  y: 0,

                        ////  // can we make it float?
                        ////  z: 3

                        ////  // or animate its size?
                        ////    //scale: 0.1f
                        ////);


                        ////// [1]=mvMatrix
                        ////mvMatrixStack.Push(mvMatrix0);

                        UDPDraw();
                        //mvMatrixStackDraw(-0, 1.4f, 0, 0.12f);
                        //mvMatrixStackDraw(-0, 1.8f, 0, 0.13f);
                        //mvMatrixStackDraw(-0, 2.2f, 0, 0.14f);
                        //mvMatrixStackDraw(0, 1, 1, 0.1f);
                        //mvMatrixStackDraw(0, 1, -1, 0.1f);

                        //mvMatrixStack.Pop();




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



                        parms.Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_WORLD].Images[eye].TexId = RenderTexture->FrameBufferColorTexture;
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

    public delegate void Action_ffff(float x, float y, float z, float scale);

}
