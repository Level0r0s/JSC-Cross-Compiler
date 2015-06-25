LOCAL_PATH := $(call my-dir)


include $(CLEAR_VARS)

LOCAL_MODULE    := main

LOCAL_CFLAGS			:= -std=c99 -Werror


LOCAL_SRC_FILES :=  HybridGLES3JNIActivityNDK.dll.c

# libs could be discovered by what code we link to in ScriptCoreLibAndroidNDK
LOCAL_LDLIBS    := -llog -landroid -lEGL -lGLESv1_CM -lGLESv2  -lGLESv3

LOCAL_STATIC_LIBRARIES := android_native_app_glue 

# http://stackoverflow.com/questions/3551989/android-library-linking
LOCAL_SHARED_LIBRARIES	:= vrapi

include $(BUILD_SHARED_LIBRARY)

$(call import-module,android/native_app_glue)

