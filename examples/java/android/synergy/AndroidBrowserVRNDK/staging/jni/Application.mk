# armeabi-v7a
#APP_ABI := all
# http://stackoverflow.com/questions/19447817/android-ndk-what-should-i-set-in-application-mk-for-app-abi
# s6 edge ?
APP_ABI := armeabi-v7a
# Check that jni/arm64-v8a/libvrapi.so  exists  or that its path is correct
#APP_ABI := arm64-v8a
#APP_PLATFORM := android-21
# https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150611/ovroculus360photosndk
# Target id 'android-19' is not valid
APP_PLATFORM := android-22
