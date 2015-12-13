# Android.mk 
# because why keep it simple if we can have a special build scripts all over the place.
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151213/androidbrowserndk
# do we know what needs to be done?

LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

LOCAL_ARM_MODE  := arm					# full speed arm instead of thumb
LOCAL_ARM_NEON  := true					# compile with neon support enabled


LOCAL_MODULE    := main



LOCAL_LDLIBS    := -llog -landroid -lEGL   -lGLESv3 -lz 
LOCAL_STATIC_LIBRARIES := android_native_app_glue  



include $(BUILD_SHARED_LIBRARY)

$(call import-module,android/native_app_glue)


