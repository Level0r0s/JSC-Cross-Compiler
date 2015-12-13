# Android.mk 
# because why keep it simple if we can have a special build scripts all over the place.
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151213/androidbrowserndk
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150612/ovroculus360photoshud
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
LOCAL_STATIC_LIBRARIES := android_native_app_glue  

LOCAL_CFLAGS	:= -DANDROID_NDK 

LOCAL_C_INCLUDES :=

LOCAL_SRC_FILES :=

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

LOCAL_SRC_FILES +=  AndroidBrowserVRNDK.dll.c

LOCAL_SHARED_LIBRARIES	:= 
LOCAL_SHARED_LIBRARIES	+= vrapi 
LOCAL_SHARED_LIBRARIES	+= assimp 


include $(BUILD_SHARED_LIBRARY)

$(call import-module,android/native_app_glue)


