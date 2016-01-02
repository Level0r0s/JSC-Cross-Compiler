# hello.
# copy if newer.
#
# can we please compile main.so so our hybrid app can call from java land to NDK land?
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos
#
# ndk: static jstring Java_x360video_Activities_xMarshal_stringFromJNI(JNIEnv env, jobject thiz, jobject args)
# java: public static string stringFromJNI(object args = null) { return default(string); }
# jsc could replace thiz with this and 
# JNIEnv is typeof or Environment ?
# what about edit and continue and shaders? hybrid itself is just hard but not yet worth the effort?


# based upon.
# Z:\jsc.svn\examples\java\android\synergy\AndroidBrowserVRNDK\staging\jni\Android.mk



LOCAL_PATH := $(call my-dir)


include $(CLEAR_VARS)
LOCAL_MODULE    := main
LOCAL_ARM_MODE  := arm					# full speed arm instead of thumb
LOCAL_ARM_NEON  := true					# compile with neon support enabled


LOCAL_LDLIBS    := -llog -landroid -lEGL   -lGLESv3 -lz 
LOCAL_LDLIBS += -ljnigraphics 

LOCAL_STATIC_LIBRARIES := android_native_app_glue  

LOCAL_CFLAGS	:= -DANDROID_NDK -DGL_EXT_texture_sRGB_decode -DGL_EXT_sRGB_write_control

# Component&&
LOCAL_CPPFLAGS += -fexceptions -std=c++11 -D__GXX_EXPERIMENTAL_CXX0X__

LOCAL_C_INCLUDES :=

LOCAL_SRC_FILES :=



# http://stackoverflow.com/questions/11775733/how-can-i-print-message-in-makefile




# usedby jni/LibOVR/Src/Kernel/OVR_GlUtils.h:26:32:
LOCAL_C_INCLUDES += $(LOCAL_PATH)/OpenGL_Loader/Include 
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./OpenGL_Loader/Src/*.cpp))




# used by jni/VrAppFramework/Src/App.cpp:12:0:
## "X:\opensource\ovr_sdk_mobile_1.0.0.0\LibOVRKernel\Src\Kernel\OVR_Types.h"

# usedby jni/VrAppFramework/Include/App.h:21:30
LOCAL_C_INCLUDES += $(LOCAL_PATH)/LibOVR/Src 
LOCAL_C_INCLUDES += $(LOCAL_PATH)/LibOVR/Include 


LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./LibOVR/Src/Kernel/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./LibOVR/Src/Android/*.cpp))

# usedby jni/VrAppFramework/Src/App.cpp:12:0:
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrApi/Include 


# used by jni/VrAppFramework/Include/Input.h:16:30
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppSupport/SystemUtils/Include 


# used by  jni/VrAppFramework/Src/App.cpp:28:23
LOCAL_C_INCLUDES += $(LOCAL_PATH)/stb
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./stb/*.c))



# usedby jni/VrAppFramework/Src/PackageFiles.cpp:18:19

LOCAL_C_INCLUDES += $(LOCAL_PATH)/minizip
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./minizip/*.c))



# usedby jni/Oculus360VideosSDK/Oculus360Videos.cpp:20:19:
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppFramework/Include 
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppFramework/Src/VRMenu/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppFramework/Src/*.cpp))


# used by jni/VrAppSupport/VrGUI/Src/UI/UIButton.cpp:12:25:
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppSupport/VrGUI/Src


# used by jni/Oculus360VideosSDK/Oculus360Videos.cpp:31:24: 
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppSupport/VrGUI/Src/UI/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppSupport/VrGUI/Src/*.cpp))

# used by jni/Oculus360VideosSDK/Oculus360Videos.h:21:23:
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppSupport/VrModel/Src
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppSupport/VrModel/Src/*.cpp))




# used by jni/Oculus360VideosSDK/Oculus360Videos.h:22:32:
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppSupport/VrSound/Include
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppSupport/VrSound/Src/*.cpp))


# used by jni/Oculus360VideosSDK/Oculus360Videos.cpp:44:24:
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppSupport/VrLocale/Src
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppSupport/VrLocale/Src/*.cpp))




LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./Oculus360VideosSDK/*.cpp))

$(info add  x360videoNDK.dll.c)
LOCAL_SRC_FILES +=  x360videoNDK.dll.c

LOCAL_SHARED_LIBRARIES	:= 
#LOCAL_SHARED_LIBRARIES	+= vrapi 
#LOCAL_SHARED_LIBRARIES	+= assimp 


include $(BUILD_SHARED_LIBRARY)

$(call import-module,android/native_app_glue)

