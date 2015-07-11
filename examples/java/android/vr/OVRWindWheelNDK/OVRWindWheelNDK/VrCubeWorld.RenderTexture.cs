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
    // we set unsafe  for DEBUG build configuration
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c


        // element of fixed dimensional array ovrRenderTexture[NUM_BUFFERS, NUM_EYES]
        public struct ovrRenderTexture
        {
            public int Width;
            public int Height;

            public int Multisamples;

            // set via glGenTextures
            // sent to TexId
            // GL_TEXTURE_2D
            public uint FrameBufferColorTexture;

            // GL_RENDERBUFFER
            public uint FrameBufferDepthBuffer;

            // set by ovrRenderTexture_Create
            // used via glBindFramebuffer
            // GL_FRAMEBUFFER
            public uint FrameBuffer;

            // called by ovrRenderer_Clear
            public void ovrRenderTexture_Clear()
            {
                // 665

                // set default?
                this.Width = 0;
                this.Height = 0;
                this.Multisamples = 0;
                this.FrameBufferColorTexture = 0;
                this.FrameBufferDepthBuffer = 0;
                this.FrameBuffer = 0;
            }

            // called by ovrRenderer_Create
            public void ovrRenderTexture_Create(int width, int height, int multisamples, int index, int eye)
            {
                // 674
                //ConsoleExtensions.tracei("enter ovrRenderTexture_Create i: ", index);
                //ConsoleExtensions.tracei("enter ovrRenderTexture_Create width ", width);
                //ConsoleExtensions.tracei("enter ovrRenderTexture_Create height ", height);
                //ConsoleExtensions.tracei("enter ovrRenderTexture_Create multisamples ", multisamples);

                this.Width = width;
                this.Height = height;
                this.Multisamples = multisamples;

                // http://fabiensanglard.net/quake2/quake2_opengl_renderer.php

                // Create the color buffer texture.
                gl3.glGenTextures(1, out this.FrameBufferColorTexture);
                gl3.glBindTexture(gl3.GL_TEXTURE_2D, this.FrameBufferColorTexture);
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
                gl3.glGenRenderbuffers(1, out this.FrameBufferDepthBuffer);
                gl3.glBindRenderbuffer(gl3.GL_RENDERBUFFER, this.FrameBufferDepthBuffer);
                gl3.glRenderbufferStorage(gl3.GL_RENDERBUFFER, gl3.GL_DEPTH_COMPONENT24, width, height);
                gl3.glBindRenderbuffer(gl3.GL_RENDERBUFFER, 0);

                // Create the frame buffer.
                gl3.glGenFramebuffers(1, out this.FrameBuffer);
                gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, this.FrameBuffer);
                gl3.glFramebufferRenderbuffer(gl3.GL_FRAMEBUFFER, gl3.GL_DEPTH_ATTACHMENT, gl3.GL_RENDERBUFFER, this.FrameBufferDepthBuffer);
                gl3.glFramebufferTexture2D(gl3.GL_FRAMEBUFFER, gl3.GL_COLOR_ATTACHMENT0, gl3.GL_TEXTURE_2D, this.FrameBufferColorTexture, 0);

                var renderFramebufferStatus = gl3.glCheckFramebufferStatus(gl3.GL_FRAMEBUFFER);
                gl3.glBindFramebuffer(gl3.GL_FRAMEBUFFER, 0);

                //ConsoleExtensions.trace("exit ovrRenderTexture_Create ");
            }

            // called by ovrRenderer_Destroy
            public void ovrRenderTexture_Destroy()
            {
                // 742

                gl3.glDeleteFramebuffers(1, ref this.FrameBuffer);
                gl3.glDeleteRenderbuffers(1, ref this.FrameBufferDepthBuffer);
                gl3.glDeleteTextures(1, ref this.FrameBufferColorTexture);

                this.FrameBufferColorTexture = 0;
                this.FrameBufferDepthBuffer = 0;
                this.FrameBuffer = 0;
            }

            // inlined
            //public void ovrRenderTexture_SetCurrent()
            //public static void ovrRenderTexture_SetNone()
            //public void ovrRenderTexture_Resolve()
        }




    }


}
