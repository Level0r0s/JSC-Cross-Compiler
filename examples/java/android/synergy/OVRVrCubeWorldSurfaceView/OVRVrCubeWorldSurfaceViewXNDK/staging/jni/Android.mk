# Copyright (C) 2010 The Android Open Source Project
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
#      http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#
LOCAL_PATH := $(call my-dir)

# http://stackoverflow.com/questions/17172153/ndk-how-to-include-prebuilt-shared-library-regardless-of-architecture
# for fk sake. why is it so complicated???

###include $(CLEAR_VARS)
###
###
###
####Android NDK: ERROR:jni/Android.mk:vrapi: LOCAL_SRC_FILES points to a missing file
####Android NDK: Check that /../libs/armeabi-v7a/libvrapi.so  exists  or that its path is correct
###
###LOCAL_MODULE := vrapi
###LOCAL_SRC_FILES := $(TARGET_ARCH_ABI)/libvrapi.so 
#### only export public headers
####LOCAL_EXPORT_C_INCLUDES := X:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Include\
###
###include $(PREBUILT_SHARED_LIBRARY)
# http://stackoverflow.com/questions/11037765/where-to-place-so-file-so-that-it-gets-included-in-the-final-build
# make.exe: *** No rule to make target `jni/../libs/armeabi-v7a/libvrapi.so', needed by `obj/local/armeabi-v7a/libvrapi.so'.  Stop.




include $(CLEAR_VARS)

#LOCAL_MODULE    := OVRVrCubeWorldNative
# https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/NativeActivity.java
LOCAL_MODULE    := main
# jni/VrApi_Helpers.h:393:2: error: 'for' loop initial declarations are only allowed in C99 mode
LOCAL_CFLAGS			:= -std=c99 -Werror
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607-1/vrcubeworld
# "X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldNativeActivity\OVRVrCubeWorldNative\bin\Debug\staging\jni\OVRVrCubeWorldNative.dll.c"

#LOCAL_SRC_FILES := /../libs/armeabi-v7a/libvrapi.so OVRVrCubeWorldNative.dll.c 
LOCAL_SRC_FILES :=  OVRVrCubeWorldSurfaceViewXNDK.dll.c
#LOCAL_C_INCLUDES := X:\opensource\ovr_mobile_sdk_0.6.0\VrApi\Include\
# libs could be discovered by what code we link to in ScriptCoreLibAndroidNDK
LOCAL_LDLIBS    := -llog -landroid -lEGL -lGLESv1_CM -lGLESv2  -lGLESv3
    

LOCAL_STATIC_LIBRARIES := android_native_app_glue 

# http://stackoverflow.com/questions/3551989/android-library-linking
#LOCAL_SHARED_LIBRARIES	:= vrapi

include $(BUILD_SHARED_LIBRARY)

$(call import-module,android/native_app_glue)

