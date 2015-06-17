using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    //X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrApi_Types.h
    // "X:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Include\VrApi.h"



    // include $(PREBUILT_SHARED_LIBRARY)
    // LOCAL_SHARED_LIBRARIES	:= vrapi
    [Script(IsNative = true, Header = "VrApi.h", IsSystemHeader = false)]
    public interface VrApi_h
    {



    }

    [Script(IsNative = true, Header = "VrApi_Helpers.h")]
    public enum ovrFrameInit
    {
        VRAPI_FRAME_INIT_DEFAULT,
        VRAPI_FRAME_INIT_BLACK,
        VRAPI_FRAME_INIT_BLACK_FLUSH,
        VRAPI_FRAME_INIT_BLACK_FINAL,
        VRAPI_FRAME_INIT_LOADING_ICON,
        VRAPI_FRAME_INIT_LOADING_ICON_FLUSH,
        VRAPI_FRAME_INIT_MESSAGE,
        VRAPI_FRAME_INIT_MESSAGE_FLUSH
    } ;

    [Script(IsNative = true)]
    public struct ovrInitParms : VrApi_h
    {
    }

    [Script(IsNative = true)]
    public struct ovrFrameParms : VrApi_h
    {
        // created by ovrRenderer_RenderFrame
        // then sent to vrapi_SubmitFrame


        public long FrameIndex;

    }



    [Script(IsNative = true)]
    public class ovrMobile : VrApi_h
    {
    }

    // via vrapi_GetPredictedTracking
    [Script(IsNative = true)]
    public struct ovrTracking : VrApi_h
    {
        // GearVR voot
    }

    [Script(IsNative = true)]
    public struct ovrHmdInfo : VrApi_h
    {
        // GearVR voot
    }

    [Script(IsNative = true, Header = "VrApi_Helpers.h")]
    public static class VrApi_Helpers
    {
        public static ovrInitParms vrapi_DefaultInitParms(ref ovrJava java)
        {
            return default(ovrInitParms);
        }

        public static ovrFrameParms vrapi_DefaultFrameParms(ref ovrJava java, ovrFrameInit init, uint texId)
        {
            return default(ovrFrameParms);
        }
    }

    [Script(IsNative = true, Header = "VrApi.h")]
    public static class VrApi
    {
        public static ovrHmdInfo vrapi_GetHmdInfo(ref ovrJava java)
        {
            return default(ovrHmdInfo);
        }

        public static void vrapi_Initialize(ref ovrInitParms initParms)
        {
        }

        public static ovrTracking vrapi_GetPredictedTracking(this ovrMobile ovr, double absTimeInSeconds)
        {
            return default(ovrTracking);
        }



        public static double vrapi_GetPredictedDisplayTime(this ovrMobile ovr, long frameIndex)
        {
            return 0;
        }

        public static void vrapi_SubmitFrame(this ovrMobile ovr, ref ovrFrameParms parms)
        {
        }

        public static void vrapi_Shutdown()
        {
        }
    }

    //   jni/OVRVrCubeWorldSurfaceViewXNDK.dll.h:12:19: fatal error: VrApi.h: No such file or directory
    //#include "VrApi.h"
    //                  ^

    [Script(IsNative = true)]
    public unsafe struct ovrJava : VrApi_h
    {
        // this is a native header.
        // and it has a pointer to a pointer.
        // c# wont allow byref fields just yet?

        public JavaVM Vm;					// Java Virtual Machine

        // can we dereference the pointer like it was an array?
        //public JavaVM[] Vm;					// Java Virtual Machine
        // JavaVM *	Vm;					// Java Virtual Machine

        public JNIEnv Env;				// Thread specific environment

        public jobject ActivityObject;		// Java activity object

        // Error	1	Cannot take the address of, get the size of, or declare a pointer to a managed type ('ScriptCoreLibNative.SystemHeaders.JavaVM')	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrApi.cs	43	24	OVRVrCubeWorldSurfaceViewXNDK

    }
}
