# Android.mk 
# because why keep it simple if we can have a special build scripts all over the place.
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151213/androidbrowserndk
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150612/ovroculus360photoshud
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos
# do we know what needs to be done?



LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)
LOCAL_MODULE := vrapi
LOCAL_SRC_FILES := $(TARGET_ARCH_ABI)/libvrapi.so 
include $(PREBUILT_SHARED_LIBRARY)

include $(CLEAR_VARS)
LOCAL_MODULE := assimp
LOCAL_SRC_FILES := $(TARGET_ARCH_ABI)/libassimp.so
include $(PREBUILT_SHARED_LIBRARY)



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




LOCAL_C_INCLUDES := $(LOCAL_PATH)/stb
LOCAL_SRC_FILES :=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./stb/*.c))


LOCAL_C_INCLUDES += $(LOCAL_PATH)/minizip
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./minizip/*.c))


LOCAL_C_INCLUDES += $(LOCAL_PATH)/LibOVR/Src 
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppFramework/Src 
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppSupport/Src 
LOCAL_C_INCLUDES += $(LOCAL_PATH)/LibOVR/Include 

LOCAL_C_INCLUDES += $(LOCAL_PATH)/LibOVR/Src/Capture/include 

LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrApi/Include 
LOCAL_C_INCLUDES += $(LOCAL_PATH)/OpenGL_Loader/Include 


LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./OpenGL_Loader/Src/*.cpp))


LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./LibOVR/Src/Kernel/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./LibOVR/Src/Android/*.cpp))
#LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./LibOVR/Src/Capture/src/*.cpp))


# since ovr_mobile_sdk_0.6.1.0
LOCAL_C_INCLUDES += $(LOCAL_PATH)/VrAppFramework/Include 
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppFramework/Src/VRMenu/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppFramework/Src/*.cpp))

# SceneView.h
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./VrAppSupport/Src/*.cpp))

LOCAL_C_INCLUDES += $(LOCAL_PATH)/contrib/assimp
LOCAL_C_INCLUDES +=	$(LOCAL_PATH)/contrib/assimp/include
LOCAL_C_INCLUDES +=	$(LOCAL_PATH)/contrib/assimp/include/Compiler

LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./contrib/jassimp2/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./contrib/libpng/*.c))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./contrib/libpng/*.s))


LOCAL_C_INCLUDES += $(LOCAL_PATH)/contrib

LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./contrib/glm/detail/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./contrib/glm/gtc/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./contrib/glm/gtx/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./contrib/glm/virtrev/*.cpp))



LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./eglextension/msaa/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./eglextension/tiledrendering/*.cpp))


LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./util/*.cpp))


LOCAL_C_INCLUDES +=	$(LOCAL_PATH)/contrib/libpng

LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./objects/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./objects/components/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./objects/textures/*.cpp))


LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./shaders/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./shaders/material/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./shaders/posteffect/*.cpp))



LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./engine/importer/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./engine/picker/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./engine/renderer/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./engine/memory/*.cpp))

LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./sensor/ksensor/*.cpp))
LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./sensor/ksensor/math/*.cpp))

LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./monoscopic/*.cpp))

LOCAL_SRC_FILES +=  $(subst $(LOCAL_PATH)/./,,$(wildcard $(LOCAL_PATH)/./oculus/*.cpp))



LOCAL_SRC_FILES +=  AndroidBrowserVRNDK.dll.c

LOCAL_SHARED_LIBRARIES	:= 
LOCAL_SHARED_LIBRARIES	+= vrapi 
LOCAL_SHARED_LIBRARIES	+= assimp 


include $(BUILD_SHARED_LIBRARY)

$(call import-module,android/native_app_glue)


