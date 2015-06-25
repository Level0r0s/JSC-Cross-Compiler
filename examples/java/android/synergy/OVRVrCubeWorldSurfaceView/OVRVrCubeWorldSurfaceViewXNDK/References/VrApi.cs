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
    }

    // Row-major 4x4 matrix.
    [Script(IsNative = true)]

    //float M[4][4];
    // 

    //public unsafe struct float4
    //{
    //    float float0;
    //    float float1;
    //    float float2;
    //    float float3;
    //}

    //public unsafe struct float4x4 //: VrApi_h
    //{
    //    public fixed float __value[4 * 4];

    //    // 16 floats in size!

    //    //float4 row0;
    //    //float4 row1;
    //    //float4 row2;
    //    //float4 row3;

    //    // can we return ref float?

    //    float* this[int row]
    //    {
    //        get
    //        {
    //            return (float*)&this;
    //        }
    //    }
    //}


    // allocated by glMapBufferRange, sizeof
    public unsafe struct ovrMatrix4f //: VrApi_h
    {
        // Fixed sized buffers can only be one-dimensional.
        // http://stackoverflow.com/questions/665573/multidimensional-arrays-in-a-struct-in-c-sharp

        // sent to glUniformMatrix4fv

        //public float M[4,4];
        // 8 * 4 * 4 = 128
        public fixed float M[4 * 4]; // no need to init it as it is native

        // http://stackoverflow.com/questions/15071775/struct-with-fixed-sized-array-of-another-struct

        // now we cannot get the size for allocator anymore?

        // X:\jsc.svn\examples\c\Test\TestSizeOfUserStruct\TestSizeOfUserStruct\Class1.cs
        //Error	7	Cannot take the address of, get the size of, or declare a pointer to a managed type ('OVRVrCubeWorldSurfaceViewXNDK.ovrMatrix4f')	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Renderer.cs	100	44	OVRVrCubeWorldSurfaceViewXNDK

    }

    [Script(IsNative = true)]
    public struct ovrInitParms : VrApi_h
    {
    }


    public enum ovrFrameLayerType
    {
        VRAPI_FRAME_LAYER_TYPE_WORLD,
        VRAPI_FRAME_LAYER_TYPE_OVERLAY,
        VRAPI_FRAME_LAYER_TYPE_CURSOR,
        VRAPI_FRAME_LAYER_TYPE_USER,
        VRAPI_FRAME_LAYER_TYPE_MAX
    } ;


    [Script(IsNative = true)]
    public unsafe struct ovrRectf
    {
        public float x;
        public float y;
        public float width;
        public float height;
    }

    // Note that any layer images that are dynamic must be triple buffered.
    [Script(IsNative = true)]
    public unsafe struct ovrFrameLayerImage
    {
        // One of the ovrFrameLayerImageType.
        //ovrFrameLayerImageType	ImageType;

        // If TexId == 0, this image is disabled.
        // Most applications will have the overlay image
        // disabled.
        //
        // Because OpenGL ES does not support clampToBorder,
        // it is the application's responsibility to make sure
        // that all mip levels of the texture have a black border
        // that will show up when time warp pushes the texture partially
        // off screen.
        //
        // Overlap textures will only show through where alpha on the
        // primary texture is not 1.0, so they do not require a border.
        public uint TexId;

        // Experimental separate R/G/B cube maps
        public fixed uint PlanarTexId[3];

        // Points on the screen are mapped by a distortion correction
        // function into ( TanX, TanY, -1, 1 ) vectors that are transformed
        // by this matrix to get ( S, T, Q, _ ) vectors that are looked
        // up with texture2dproj() to get texels.
        public ovrMatrix4f TexCoordsFromTanAngles;

        // Only texels within this range should be drawn.
        // This is a sub-rectangle of the [(0,0)-(1,1)] texture coordinate range.
        public ovrRectf TextureRect;

        // The tracking state for which ModelViewMatrix is correct.
        // It is ok to update the orientation for each eye, which
        // can help minimize black edge pull-in, but the position
        // must remain the same for both eyes, or the position would
        // seem to judder "backwards in time" if a frame is dropped.
        public ovrPoseStatef HeadPose;
    }


    public enum ovrFrameLayerEye
    {
        VRAPI_FRAME_LAYER_EYE_LEFT,
        VRAPI_FRAME_LAYER_EYE_RIGHT,
        VRAPI_FRAME_LAYER_EYE_MAX
    }

    [Script(IsNative = true)]
    public unsafe struct ovrFrameLayer : VrApi_h
    {
        //public fixed ovrFrameLayerImage[] Images[(int)ovrFrameLayerEye.VRAPI_FRAME_LAYER_EYE_MAX];
        public ovrFrameLayerImage[] Images;//[(int)ovrFrameLayerEye.VRAPI_FRAME_LAYER_EYE_MAX];

    }




    [Script(IsNative = true)]
    public unsafe struct ovrFrameParms : VrApi_h
    {
        // Layers composited in the time warp.
        //public fixed ovrFrameLayer[] Layers[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_MAX];
        public ovrFrameLayer[] Layers; //[(int)ovrFrameLayerType.VRAPI_FRAME_LAYER_TYPE_MAX];
        public int LayerCount;

        // created by ovrRenderer_RenderFrame
        // then sent to vrapi_SubmitFrame


        public long FrameIndex;

        // set in ovrRenderer_RenderFrame
        public int MinimumVsyncs;

    }

    [Script(IsNative = true)]
    public struct ovrModeParms
    {
        // These are fixed clock levels.
        public int CpuLevel;
        public int GpuLevel;

        // These threads will get SCHED_FIFO.
        public pid_t MainThreadTid;

        public int RenderThreadTid;
    }


    // vrapi_EnterVrMode
    [Script(IsNative = true)]
    public class ovrMobile : VrApi_h
    {

    }



    //-----------------------------------------------------------------
    // HMD sensor input
    //-----------------------------------------------------------------


    // vec3
    [Script(IsNative = true)]
    public struct ovrVector3f
    {
        public float x, y, z;
    }

    // Quaternion.
    // vec4?
    [Script(IsNative = true)]
    public struct ovrQuatf
    {
        public float x, y, z, w;
    }



    // Position and orientation together.
    [Script(IsNative = true)]
    public unsafe struct ovrPosef
    {
        public ovrQuatf Orientation;
        public ovrVector3f Position;
    }


    // Full pose (rigid body) configuration with first and second derivatives.
    public unsafe struct ovrPoseStatef
    {
        public ovrPosef Pose;
        public ovrVector3f AngularVelocity;
        public ovrVector3f LinearVelocity;
        public ovrVector3f AngularAcceleration;
        public ovrVector3f LinearAcceleration;
        public double TimeInSeconds;			// Absolute time of this state sample.
        public double PredictionInSeconds;	// Seconds this state was predicted ahead.
    }

    // via vrapi_GetPredictedTracking
    [Script(IsNative = true)]
    public struct ovrTracking : VrApi_h
    {
        // GearVR voot

        // Sensor status described by ovrTrackingStatus flags.
        public uint Status;
        // Predicted head configuration at the requested absolute time.
        // The pose describes the head orientation and center eye position.
        public ovrPoseStatef HeadPose;
    }

    [Script(IsNative = true)]
    public unsafe struct ovrHmdInfo : VrApi_h
    {
        // GearVR voot

        // Resolution of the display in pixels.
        public int DisplayPixelsWide;
        public int DisplayPixelsHigh;

        // Refresh rate of the display in cycles per second.
        // Currently 60Hz.
        public float DisplayRefreshRate;

        // With a display resolution of 2560x1440, the pixels at the center
        // of each eye cover about 0.06 degrees of visual arc. To wrap a
        // full 360 degrees, about 6000 pixels would be needed and about one
        // quarter of that would be needed for ~90 degrees FOV. As such, Eye
        // images with a resolution of 1536x1536 result in a good 1:1 mapping
        // in the center, but they need mip-maps for off center pixels. To
        // avoid the need for mip-maps and for significantly improved rendering
        // performance this currently returns a conservative 1024x1024.
        public fixed int SuggestedEyeResolution[2];

        // This is a product of the lens distortion and the screen size,
        // but there is no truly correct answer.
        //
        // There is a tradeoff in resolution and coverage.
        // Too small of an FOV will leave unrendered pixels visible, but too
        // large wastes resolution or fill rate.  It is unreasonable to
        // increase it until the corners are completely covered, but we do
        // want most of the outside edges completely covered.
        //
        // Applications might choose to render a larger FOV when angular
        // acceleration is high to reduce black pull in at the edges by
        // the time warp.
        //
        // Currently symmetric 90.0 degrees.
        public fixed float SuggestedEyeFov[2];
    }

    [Script(IsNative = true)]
    public struct ovrHeadModelParms : VrApi_h
    {
        // GearVR voot
    }




    [Script(IsNative = true)]
    public enum eVrApiEventStatus
    {
        VRAPI_EVENT_ERROR_INTERNAL = -2,		// queue isn't created, etc.
        VRAPI_EVENT_ERROR_INVALID_BUFFER = -1,	// the buffer passed in was invalid
        VRAPI_EVENT_NOT_PENDING = 0,			// no event is waiting
        VRAPI_EVENT_PENDING,					// an event is waiting
        VRAPI_EVENT_CONSUMED,					// an event was pending but was consumed internally
        VRAPI_EVENT_BUFFER_OVERFLOW,			// an event is being returned, but it could not fit into the buffer
        VRAPI_EVENT_INVALID_JSON				// there was an error parsing the JSON data
    }

    [Script(IsNative = true, Header = "VrApi_Android.h")]
    public static class VrApi_Android
    {
        public const float BACK_BUTTON_DOUBLE_TAP_TIME_IN_SECONDS = 0.25f;
        public const float BACK_BUTTON_SHORT_PRESS_TIME_IN_SECONDS = 0.25f;
        public const float BACK_BUTTON_LONG_PRESS_TIME_IN_SECONDS = 0.75f;

        public static eVrApiEventStatus ovr_GetNextPendingEvent(byte[] buffer, uint bufferSize)
        {
            return default(eVrApiEventStatus);
        }


        // if we wanted to group methods by the this pointer, each method could want its own header file still?
        // called by ovrApp_BackButtonAction
        public static bool ovr_StartSystemActivity(ref ovrJava java, string command, string jsonText)
        {
            return false;
        }

    }


    [Script(IsNative = true, Header = "VrApi_Helpers.h")]
    public unsafe static class VrApi_Helpers
    {

        public static ovrMatrix4f ovrMatrix4f_TanAngleMatrixFromProjection(ref  ovrMatrix4f projection)
        {
            throw null;
        }

        public static ovrMatrix4f ovrMatrix4f_CreateProjectionFov(float fovRadiansX, float fovRadiansY,
                                                 float offsetX, float offsetY, float nearZ, float farZ)
        {
            throw null;
        }

        public static ovrMatrix4f ovrMatrix4f_Transpose(ref ovrMatrix4f a)
        {
            throw null;
        }

        public static ovrMatrix4f ovrMatrix4f_Multiply(ref ovrMatrix4f a, ref ovrMatrix4f b)
        {
            throw null;
        }

        public static ovrMatrix4f ovrMatrix4f_CreateRotation(float radiansX, float radiansY, float radiansZ)
        {
            throw null;
        }

        public static ovrMatrix4f ovrMatrix4f_CreateTranslation(float radiansX, float radiansY, float radiansZ)
        {
            throw null;
        }

        public static ovrMatrix4f vrapi_GetEyeViewMatrix(ref ovrHeadModelParms headModelParms,
                                                    ref ovrMatrix4f centerEyeViewMatrix,
                                                    int eye)
        {
            throw null;
        }

        public  static ovrMatrix4f vrapi_GetCenterEyeViewMatrix(
            ref ovrHeadModelParms headModelParms,
            ref ovrTracking tracking,

            // nullable struct?
            ovrMatrix4f* input)
        {
            throw null;
        }

        // ovrRenderer_RenderFrame
        public static ovrHeadModelParms vrapi_DefaultHeadModelParms()
        {
            throw null;
        }

        // vrapi_DefaultModeParms
        //public static ovrModeParms vrapi_DefaultModeParms(ovrJava* java)
        public static ovrModeParms vrapi_DefaultModeParms(ref ovrJava java)
        {
            return default(ovrModeParms);
        }

        public static ovrInitParms vrapi_DefaultInitParms(ref ovrJava java)
        {
            return default(ovrInitParms);
        }

        // called by ovrRenderer_RenderFrame
        // called by AppThreadFunction
        public static ovrFrameParms vrapi_DefaultFrameParms(ref ovrJava java, ovrFrameInit init, uint texId)
        {
            return default(ovrFrameParms);
        }
    }

    [Script(IsNative = true, Header = "VrApi.h")]
    public static class VrApi
    {
        // X:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Include\VrApi.h

        // ovrApp_BackButtonAction

        public const string PUI_GLOBAL_MENU = "globalMenu";


        public const string PUI_CONFIRM_QUIT = "confirmQuit";

        public static void vrapi_LeaveVrMode(ovrMobile ovr)
        {
        }


        public static ovrMobile vrapi_EnterVrMode(ref ovrModeParms parms)
        {
            return default(ovrMobile);
        }


        // ovrApp_HandleKeyEvent
        public static double vrapi_GetTimeInSeconds() { return 0; }


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
