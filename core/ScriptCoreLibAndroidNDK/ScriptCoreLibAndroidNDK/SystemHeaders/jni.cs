using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // can we move out the support for JVM from jsc
    // into scriptcorelib
    // and then share the API here?

    // "X:\opensource\android-ndk-r10c\platforms\android-12\arch-arm\usr\include\jni.h"

    // or should this be a marker attribute instead?
    [Script(IsNative = true, Header = "jni.h", IsSystemHeader = true)]
    public interface jni_h
    {
    }


    // where is it defined???
    [Script(IsNative = true, PointerName = "size_t")]
    public class size_t : jni_h
    {
    }


    // typedef jobject         jstring;
    //typedef void*           jobject;
    [Script(IsNative = true, PointerName = "jobject")]
    public class jobject : jni_h
    {
        // look we just defined an object?
        // the name already is the pointer?
    }

    [Script(IsNative = true, PointerName = "jstring")]
    public class jstring : jobject
    {
        // look we just defined an object?
        // the name already is the pointer?
    }


    [Script(IsNative = true, PointerName = "jclass")]
    public class jclass : jobject
    {
        // look we just defined an object?
        // the name already is the pointer?
    }


    [Script(IsNative = true, PointerName = "jlong")]
    public unsafe class jlong : jni_h
    {
        // long

        public static implicit operator void* (jlong x)
        {
            return default(void*);
        }

        // size_t

        public static implicit operator size_t(jlong x)
        {
            return default(size_t);
        }
    }

    //typedef const struct JNINativeInterface* JNIEnv;
    [Script(IsNative = true, PointerName = "JNIEnv")]
    public class JNIEnv : jni_h
    {
        // used by 
        // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRNDK\xNativeActivity.cs

        // jint        (*GetVersion)(JNIEnv *);

        //jstring     (*NewStringUTF)(JNIEnv*, const char*);

        // this is actualy a field to function pointer?
        
        // return  (void*)NewStringUTF((void*)env, (void*)env, (char*)"from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");
        // c does not have instance methods!

        [Script(IsNative = true)]
        public delegate jstring NewStringUTFDelegate(ref JNIEnv env, string value);


        // tested by?
        public NewStringUTFDelegate NewStringUTF;

    }

    //typedef const struct JNIInvokeInterface* JavaVM;
    [Script(IsNative = true, PointerName = "JavaVM")]
    public class JavaVM : jni_h
    {
        //jint        (*GetEnv)(JavaVM*, void**, jint);
    }

    [Script(IsNative = true)]
    public class jni : jni_h
    {
        // X:\jsc.internal.git\compiler\jsc.internal\jsc.internal.library\Desktop\JVM\JVMLauncher.cs


        // full circle



    

    }

}
