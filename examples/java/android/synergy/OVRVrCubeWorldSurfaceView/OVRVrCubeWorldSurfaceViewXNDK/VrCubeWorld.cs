using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Script()]
[assembly: ScriptTypeFilter(ScriptType.C, typeof(OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld))]

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    [Script]
    public static class VrCubeWorld
    {

        static void EglErrorString() { }
        static void EglFrameBufferStatusString() { }
        static void EglCheckErrors() { }

        [Script]
        class ovrEgl
        { }

        static void ovrEgl_Clear(this ovrEgl that) { }
        static void ovrEgl_CreateContext(this ovrEgl that) { }
        static void ovrEgl_DestroyContext(this ovrEgl that) { }
        static void ovrEgl_CreateSurface(this ovrEgl that) { }
        static void ovrEgl_DestroySurface(this ovrEgl that) { }

        [Script]
        class ovrVertexAttribPointer
        { }

        [Script]
        class ovrGeometry
        { }

        [Script]
        class ovrVertexAttribute
        { }

        static ovrVertexAttribute[] ProgramVertexAttributes;

        static void ovrGeometry_Clear(this ovrGeometry that) { }
        static void ovrGeometry_CreateCube(this ovrGeometry that) { }
        static void ovrGeometry_Destroy(this ovrGeometry that) { }
        static void ovrGeometry_CreateVAO(this ovrGeometry that) { }
        static void ovrGeometry_DestroyVAO(this ovrGeometry that) { }

        [Script]
        class ovrProgram { }
        [Script]
        class ovrUniform { }

        static ovrUniform[] ProgramUniforms;

        static void ovrProgram_Clear(this ovrProgram that) { }
        static void ovrProgram_Create(this ovrProgram that) { }
        static void ovrProgram_Destroy(this ovrProgram that) { }

        [Script]
        class ovrRenderTexture { }

        static void ovrRenderTexture_Clear(this ovrRenderTexture that) { }
        static void ovrRenderTexture_Create(this ovrRenderTexture that) { }
        static void ovrRenderTexture_Destroy(this ovrRenderTexture that) { }
        static void ovrRenderTexture_SetCurrent(this ovrRenderTexture that) { }
        static void ovrRenderTexture_SetNone(this ovrRenderTexture that) { }
        static void ovrRenderTexture_Resolve(this ovrRenderTexture that) { }

        [Script]
        class ovrScene { }

        //VERTEX_SHADER
        //FRAGMENT_SHADER

        static void ovrScene_Clear(this ovrScene that) { }
        static void ovrScene_IsCreated(this ovrScene that) { }
        static void ovrScene_CreateVAOs(this ovrScene that) { }
        static void ovrScene_DestroyVAOs(this ovrScene that) { }
        static void ovrScene_Create(this ovrScene that) { }
        static void ovrScene_Destroy(this ovrScene that) { }

        [Script]
        class ovrSimulation { }

        static void ovrSimulation_Clear(this ovrSimulation that)
        { }
        static void ovrSimulation_AdvanceSimulation(this ovrSimulation that) { }

        [Script]
        class ovrRenderer { }

        static void ovrRenderer_Clear(this ovrRenderer that) { }
        static void ovrRenderer_Create(this ovrRenderer that) { }
        static void ovrRenderer_Destroy(this ovrRenderer that) { }
        static void ovrRenderer_RenderFrame(this ovrRenderer that) { }

        [Script]
        class ovrRenderThread { }

        static void RenderThreadFunction() { }
        static void ovrRenderThread_Clear(this ovrRenderThread that) { }
        static void ovrRenderThread_Create(this ovrRenderThread that) { }
        static void ovrRenderThread_Destroy(this ovrRenderThread that) { }
        static void ovrRenderThread_Submit(this ovrRenderThread that) { }
        static void ovrRenderThread_Wait(this ovrRenderThread that) { }
        static void ovrRenderThread_GetTid(this ovrRenderThread that) { }

        [Script]
        class ovrApp { }

        static void ovrApp_Clear(this ovrApp that) { }
        static void ovrApp_HandleVrModeChanges(this ovrApp that) { }
        static void ovrApp_BackButtonAction(this ovrApp that) { }
        static void ovrApp_HandleKeyEvent(this ovrApp that) { }
        static void ovrApp_HandleTouchEvent(this ovrApp that) { }
        static void ovrApp_HandleSystemEvents(this ovrApp that) { }

        [Script]
        class ovrMessage { }

        static void ovrMessage_Init(this ovrMessage that)
        { }

        static void ovrMessage_SetPointerParm(this ovrMessage that) { }
        static void ovrMessage_GetPointerParm(this ovrMessage that) { }
        static void ovrMessage_SetIntegerParm(this ovrMessage that) { }
        static void ovrMessage_GetIntegerParm(this ovrMessage that) { }
        static void ovrMessage_SetFloatParm(this ovrMessage that) { }
        static void ovrMessage_GetFloatParm(this ovrMessage that) { }

        [Script]
        class ovrMessageQueue { }

        static void ovrMessageQueue_Create(this ovrMessageQueue that) { }
        static void ovrMessageQueue_Destroy(this ovrMessageQueue that) { }
        static void ovrMessageQueue_Enable(this ovrMessageQueue that) { }
        static void ovrMessageQueue_PostMessage(this ovrMessageQueue that) { }
        static void ovrMessageQueue_SleepUntilMessage(this ovrMessageQueue that) { }
        static void ovrMessageQueue_GetNextMessage(this ovrMessageQueue that) { }

        [Script]
        class ovrAppThread { }

        static void AppThreadFunction() { }
        static void ovrAppThread_Create(this ovrMessageQueue that) { }
        static void ovrAppThread_Destroy(this ovrMessageQueue that) { }

        // java, jsc hybrid?

        static void Java_com_oculus_gles3jni_GLES3JNILib_onCreate() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onStart() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onResume() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onPause() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onStop() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onDestroy() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceCreated() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceChanged() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onSurfaceDestroyed() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onKeyEvent() { }
        static void Java_com_oculus_gles3jni_GLES3JNILib_onTouchEvent() { }
    }
}
