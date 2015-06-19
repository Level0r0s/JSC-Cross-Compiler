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

    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\jni.h"

    // or should this be a marker attribute instead?
    [Script(IsNative = true, Header = "jni.h", IsSystemHeader = true)]
    public interface jni_h
    {
    }


    // where is it defined???
    [Script(IsNative = true, PointerName = "size_t")]
    public struct size_t : jni_h
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

        public static implicit operator void*(jlong x)
        {
            return default(void*);
        }

        // size_t

        public static implicit operator size_t(jlong x)
        {
            return default(size_t);
        }
    }


    // struct JNIInvokeInterface;


    [Script(IsNative = true)]
    public struct JNINativeInterface : jni_h
    {
        public object reserved0;
        public object reserved1;
        public object reserved2;
        public object reserved3;
    }

    [Script(IsNative = true)]
    public struct JNIInvokeInterface : jni_h
    {
        public object reserved0;
        public object reserved1;
        public object reserved2;


        //jint        (*DestroyJavaVM)(JavaVM*);
        //jint        (*AttachCurrentThread)(JavaVM*, JNIEnv**, void*);
        //jint        (*DetachCurrentThread)(JavaVM*);
        //jint        (*GetEnv)(JavaVM*, void**, jint);
        //jint        (*AttachCurrentThreadAsDaemon)(JavaVM*, JNIEnv**, void*);

        // used by 
        // X:\jsc.svn\examples\c\android\Test\TestHybridOVR\TestHybridOVR\OVRNDK\xNativeActivity.cs

        // jint        (*GetVersion)(JNIEnv *);

        //jstring     (*NewStringUTF)(JNIEnv*, const char*);

        // this is actualy a field to function pointer?

        // return  (void*)NewStringUTF((void*)env, (void*)env, (char*)"from Java_TestHybridOVR_OVRJVM_ApplicationActivity_stringFromJNI");
        // c does not have instance methods!



    }




    //typedef const struct JNINativeInterface* JNIEnv;
    //typedef const struct JNIInvokeInterface* JavaVM;


    //typedef const struct JNINativeInterface* JNIEnv;
    //[Script(IsNative = true, PointerName = "JNINativeInterface*")]
    [Script(IsNative = true, PointerName = "JNIEnv*")]
    //[Script(IsNative = true)]
    public class JNIEnv : jni_h  // : JNIInvokeInterface
    {
        // X:\jsc.svn\examples\java\android\future\NDKHybridMockup\NDKHybridMockup\ApplicationActivity.cs

        // will we ever need JNEnv instead of JNIEnv* ?

        //   void        (*DeleteGlobalRef)(JNIEnv*, jobject);
        [Script(IsNative = true)]
        public delegate void DeleteGlobalRefDelegate(JNIEnv env, jobject value);
        public DeleteGlobalRefDelegate DeleteGlobalRef;

        [Script(IsNative = true)]
        public delegate jstring NewStringUTFDelegate(JNIEnv env, string value);


        // tested by?
        public NewStringUTFDelegate NewStringUTF;


        [Script(IsNative = true)]
        public delegate int GetJavaVMDelegate(JNIEnv env, out JavaVM value);
        //jint        (*GetJavaVM)(JNIEnv*, JavaVM**);
        public GetJavaVMDelegate GetJavaVM;

        //jobject     (*NewGlobalRef)(JNIEnv*, jobject);
        [Script(IsNative = true)]
        public delegate jobject NewGlobalRefDelegate(JNIEnv env, jobject value);
        public NewGlobalRefDelegate NewGlobalRef;
    }

    //typedef const struct JNIInvokeInterface* JavaVM;
    //[Script(IsNative = true, PointerName = "JavaVM")]
    //[Script(IsNative = true, PointerName = "JNIInvokeInterface*")]
    //[Script(IsNative = true)]
    [Script(IsNative = true, PointerName = "JavaVM*")]
    public class JavaVM : jni_h //: JNIInvokeInterface
    {
        //jint        (*GetEnv)(JavaVM*, void**, jint);

        [Script(IsNative = true)]
        //public delegate int AttachCurrentThreadDelegate(ref JavaVM vm, out JNIEnv env, jobject value);
        public delegate int AttachCurrentThreadDelegate(JavaVM byref_vm, out JNIEnv env, jobject value);
        public AttachCurrentThreadDelegate AttachCurrentThread;

        // (JavaVM**, JNIEnv*, jobject)
        //jint        (*AttachCurrentThread)(JavaVM*, JNIEnv**, void*);



        //jint        (*DetachCurrentThread)(JavaVM*);
        [Script(IsNative = true)]
        public delegate int DetachCurrentThreadDelegate(JavaVM byref_vm);
        public DetachCurrentThreadDelegate DetachCurrentThread;

    }

    [Script(IsNative = true)]
    public class jni : jni_h
    {
        // X:\jsc.internal.git\compiler\jsc.internal\jsc.internal.library\Desktop\JVM\JVMLauncher.cs


        // full circle





    }

}
