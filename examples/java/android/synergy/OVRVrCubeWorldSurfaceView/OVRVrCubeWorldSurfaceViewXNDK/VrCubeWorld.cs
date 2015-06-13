using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static class VrCubeWorld
    {
     
        EglErrorString
            EglFrameBufferStatusString
            EglCheckErrors

               class ovrEgl
        { }

        ovrEgl_Clear
            ovrEgl_CreateContext
            ovrEgl_DestroyContext
            ovrEgl_CreateSurface
            ovrEgl_DestroySurface

            class ovrVertexAttribPointer
        { }

        class ovrGeometry
        { }

        class ovrVertexAttribute
        { }

        static ovrVertexAttribute[] ProgramVertexAttributes;

        ovrGeometry_Clear
            ovrGeometry_CreateCube
            ovrGeometry_Destroy
            ovrGeometry_CreateVAO
            ovrGeometry_DestroyVAO

         class ovrProgram        { }
         class ovrUniform { }

        static ovrUniform[] ProgramUniforms;

        ovrProgram_Clear
            ovrProgram_Create
            ovrProgram_Destroy

            class ovrRenderTexture { }

        ovrRenderTexture_Clear
            ovrRenderTexture_Create
            ovrRenderTexture_Destroy
            ovrRenderTexture_SetCurrent
            ovrRenderTexture_SetNone
            ovrRenderTexture_Resolve

            class ovrScene { }

        VERTEX_SHADER
            FRAGMENT_SHADER

            ovrScene_Clear
            ovrScene_IsCreated
            ovrScene_CreateVAOs
            ovrScene_DestroyVAOs
            ovrScene_Create
            ovrScene_Destroy

            class ovrSimulation { }

        ovrSimulation_Clear
            ovrSimulation_AdvanceSimulation

            class ovrRenderer { }

        ovrRenderer_Clear
            ovrRenderer_Create
ovrRenderer_Destroy
            ovrRenderer_RenderFrame

            class ovrRenderThread { }

        RenderThreadFunction
            ovrRenderThread_Clear
            ovrRenderThread_Create
            ovrRenderThread_Destroy
            ovrRenderThread_Submit
            ovrRenderThread_Wait
            ovrRenderThread_GetTid

            class ovrApp_Clear { }

        ovrApp_HandleVrModeChanges
            ovrApp_BackButtonAction
            ovrApp_HandleKeyEvent
            ovrApp_HandleTouchEvent
            ovrApp_HandleSystemEvents

            class ovrMessage { }

        ovrMessage_Init

            ovrMessage_SetPointerParm
            ovrMessage_GetPointerParm
            ovrMessage_SetIntegerParm
            ovrMessage_GetIntegerParm
            ovrMessage_SetFloatParm
            ovrMessage_GetFloatParm

            class ovrMessageQueue { }

        ovrMessageQueue_Create
            ovrMessageQueue_Destroy
            ovrMessageQueue_Enable
            ovrMessageQueue_PostMessage
            ovrMessageQueue_SleepUntilMessage
            ovrMessageQueue_GetNextMessage

            class ovrAppThread { }

        AppThreadFunction
            ovrAppThread_Create
            ovrAppThread_Destroy

        // java

        Java_com_oculus_gles3jni_GLES3JNILib_onCreate
            Java_com_oculus_gles3jni_GLES3JNILib_onStart
            Java_com_oculus_gles3jni_GLES3JNILib_onResume
            Java_com_oculus_gles3jni_GLES3JNILib_onPause
            Java_com_oculus_gles3jni_GLES3JNILib_onStop
            Java_com_oculus_gles3jni_GLES3JNILib_onDestroy

            Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceCreated
            Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceChanged
            Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceDestroyed
            Java_com_oculus_gles3jni_GLES3JNILib_onKeyEvent
    }
}
