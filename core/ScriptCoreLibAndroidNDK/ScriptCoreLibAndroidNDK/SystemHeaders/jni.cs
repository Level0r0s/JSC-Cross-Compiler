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
    public unsafe struct size_t : jni_h
    {
        // X:\jsc.svn\examples\java\android\vr\OVRMyCubeWorldNDK\OVRMyCubeWorldNDK\References\VrApi.ovrMatrix4f.cs
        public static implicit operator size_t(void* x)
        {
            return default(size_t);
        }

        public static implicit operator void*(size_t x)
        {
            return default(void*);
        }

        public static implicit operator size_t(float[,] x)
        {
            return default(size_t);
        }

        public static implicit operator int(size_t x)
        {
            return default(int);
        }

    }


    // typedef jobject         jstring;
    //typedef void*           jobject;
    [Script(IsNative = true, PointerName = "jobject")]
    public class jobject : jni_h
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Object.cs


        // look we just defined an object?
        // the name already is the pointer?
    }


    // union!
    [Script(IsNative = true, PointerName = "jvalue")]
    public class jvalue : jni_h
    {
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




    [Script(IsNative = true, PointerName = "jfieldID")]
    public class jfieldID
    {

    }


    [Script(IsNative = true, PointerName = "jmethodID")]
    public class jmethodID
    {

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
    public unsafe class JNIEnv : jni_h  // : JNIInvokeInterface
    {
        // X:\jsc.svn\examples\java\android\future\NDKHybridMockup\NDKHybridMockup\ApplicationActivity.cs

        // will we ever need JNEnv instead of JNIEnv* ?

        //   void        (*DeleteGlobalRef)(JNIEnv*, jobject);
        [Script(IsNative = true)]
        public delegate void DeleteGlobalRefDelegate(JNIEnv env, jobject value);
        public DeleteGlobalRefDelegate DeleteGlobalRef;




        #region  jclass      (*GetObjectClass)(JNIEnv*, jobject);

        [Script(IsNative = true)]
        public delegate jclass GetObjectClassDelegate(JNIEnv env, jobject value);

        public GetObjectClassDelegate GetObjectClass;

        #endregion



        // Z:\jsc.svn\examples\java\android\synergy\x360videoNDK\xNativeActivity.cs
        #region jmethodID   (*GetMethodID)(JNIEnv*, jclass, const char*, const char*);

        [Script(IsNative = true)]
        public delegate jmethodID GetMethodIDDelegate(JNIEnv env, jclass value, string n, string t);

        public GetMethodIDDelegate GetMethodID;


        #endregion



        // http://stackoverflow.com/questions/10361369/marshal-va-list-in-c-sharp-delegate
        // http://stackoverflow.com/questions/6694612/c-sharp-p-invoke-varargs-delegate-callback
        #region  void        (*CallVoidMethod)(JNIEnv*, jobject, jmethodID, ...);

        [Script(IsNative = true)]
        public delegate void CallVoidMethodADelegate(JNIEnv env, jobject that, jmethodID method, params jvalue[] args);

        public CallVoidMethodADelegate CallVoidMethodA;

        #endregion




        #region   jfieldID    (*GetFieldID)(JNIEnv*, jclass, const char*, const char*);

        [Script(IsNative = true)]
        public delegate jfieldID GetFieldIDDelegate(JNIEnv env, jclass value, string n, string t);

        public GetFieldIDDelegate GetFieldID;

        #endregion



        #region jint        (*GetIntField)(JNIEnv*, jobject, jfieldID);
        [Script(IsNative = true)]
        public delegate int GetIntFieldDelegate(JNIEnv env, jobject value, jfieldID i);

        public GetIntFieldDelegate GetIntField;
        #endregion

        #region void        (*SetFloatField)(JNIEnv*, jobject, jfieldID, jfloat) __NDK_FPABI__;
        [Script(IsNative = true)]
        public delegate void SetFloatFieldDelegate(JNIEnv env, jobject value, jfieldID i, float f);

        public SetFloatFieldDelegate SetFloatField;
        #endregion




        #region    jfloat      (*GetFloatField)(JNIEnv*, jobject, jfieldID) __NDK_FPABI__;
        [Script(IsNative = true)]
        public delegate float GetFloatFieldDelegate(JNIEnv env, jobject value, jfieldID i);

        public GetFloatFieldDelegate GetFloatField;
        #endregion



        #region   jobject     (*GetObjectField)(JNIEnv*, jobject, jfieldID);
        [Script(IsNative = true)]
        public delegate object GetObjectFieldDelegate(JNIEnv env, jobject value, jfieldID i);

        public GetObjectFieldDelegate GetObjectField;
        #endregion

        #region jsize       (*GetArrayLength)(JNIEnv*, jarray);
        [Script(IsNative = true)]
        public delegate int GetArrayLengthDelegate(JNIEnv env, object value);

        public GetArrayLengthDelegate GetArrayLength;
        #endregion


        // https://docs.oracle.com/javase/1.5.0/docs/guide/jni/spec/functions.html
        #region jbyte*      (*GetByteArrayElements)(JNIEnv*, jbyteArray, jboolean*);
        [Script(IsNative = true)]
        public delegate byte* GetByteArrayElementsDelegate(JNIEnv env, object value, out bool isCopy);

        public GetByteArrayElementsDelegate GetByteArrayElements;
        #endregion

        #region    void        (*ReleaseByteArrayElements)(JNIEnv*, jbyteArray, jbyte*, jint);
        [Script(IsNative = true)]
        public delegate void ReleaseByteArrayElementsDelegate(JNIEnv env, object value, byte* bytes, int mode = 0); // JNI_ABORT

        public ReleaseByteArrayElementsDelegate ReleaseByteArrayElements;
        #endregion


        #region SetLongField
        [Script(IsNative = true)]
        public delegate void SetLongFieldDelegate(JNIEnv env, jobject value, jfieldID i, long f);

        public SetLongFieldDelegate SetLongField;
        #endregion



        #region NewStringUTF
        // Error	7	The type 'ScriptCoreLibNative.SystemHeaders.JNIEnv' already contains a definition for 'NewStringUTF'	X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\jni.cs	155	37	ScriptCoreLibAndroidNDK

        // first method to be made available as a native instance call?
        // then to be used to call into any native function returning a string?
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\GLES3JNILib.cs
        // need to prefix it until field is removed?
        public virtual jstring JNIEnv_NewStringUTF(string value)
        {
            throw null;
        }

        [Script(IsNative = true)]
        public delegate jstring NewStringUTFDelegate(JNIEnv env, string value);


        // tested by?
        public NewStringUTFDelegate NewStringUTF;
        #endregion


        [Script(IsNative = true)]
        public delegate int GetJavaVMDelegate(JNIEnv env, out JavaVM value);
        //jint        (*GetJavaVM)(JNIEnv*, JavaVM**);
        public GetJavaVMDelegate GetJavaVM;

        //jobject     (*NewGlobalRef)(JNIEnv*, jobject);
        [Script(IsNative = true)]
        public delegate jobject NewGlobalRefDelegate(JNIEnv env, jobject value);
        public NewGlobalRefDelegate NewGlobalRef;




        #region const char* (*GetStringUTFChars)(JNIEnv*, jstring, jboolean*);
        [Script(IsNative = true)]
        public delegate string GetStringUTFCharsDelegate(JNIEnv env, jstring s, out bool isCopy);
        //jint        (*GetJavaVM)(JNIEnv*, JavaVM**);
        public GetStringUTFCharsDelegate GetStringUTFChars;
        #endregion

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
