﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace JVMLauncher
{
    [StructLayout(LayoutKind.Sequential)]
    struct _jobjectArray
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    struct _jobject
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    struct _jclass
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    struct _jmethodID
    {
    }

    [StructLayout(LayoutKind.Sequential)]
    struct _jstring
    {
    }

    unsafe delegate _jclass* JNINativeInterface_FindClass(JNIEnv* env, string name);
    unsafe delegate _jmethodID* JNINativeInterface_GetStaticMethodID(JNIEnv* env, _jclass* clazz, string name, string sig);
    unsafe delegate void JNINativeInterface_CallStaticVoidMethodA(JNIEnv* env, _jclass* clazz, _jmethodID* methodID, void* args);


    unsafe delegate _jobjectArray* JNINativeInterface_NewObjectArray(JNIEnv* env, int len, _jclass* clazz, void* init);

    unsafe delegate _jstring* JNINativeInterface_NewStringUTF(JNIEnv* env, string utf);
    unsafe delegate void JNINativeInterface_SetObjectArrayElement(JNIEnv* env, _jobjectArray* array, int index, void* val);


    [StructLayout(LayoutKind.Sequential, Pack = 4, Size = 932)]
    //[Obfuscation(Exclude = true, ApplyToMembers = true)]
    unsafe struct JNINativeInterface
    {
        public IntPtr reserved0;
        public IntPtr reserved1;
        public IntPtr reserved2;
        public IntPtr reserved3;
        public IntPtr GetVersion;
        public IntPtr DefineClass;
        public IntPtr FindClass;
        public IntPtr FromReflectedMethod;
        public IntPtr FromReflectedField;
        public IntPtr ToReflectedMethod;
        public IntPtr GetSuperclass;
        public IntPtr IsAssignableFrom;
        public IntPtr ToReflectedField;
        public IntPtr Throw;
        public IntPtr ThrowNew;
        public IntPtr ExceptionOccurred;
        public IntPtr ExceptionDescribe;
        public IntPtr ExceptionClear;
        public IntPtr FatalError;
        public IntPtr PushLocalFrame;
        public IntPtr PopLocalFrame;
        public IntPtr NewGlobalRef;
        public IntPtr DeleteGlobalRef;
        public IntPtr DeleteLocalRef;
        public IntPtr IsSameObject;
        public IntPtr NewLocalRef;
        public IntPtr EnsureLocalCapacity;
        public IntPtr AllocObject;
        public IntPtr NewObject;
        public IntPtr NewObjectV;
        public IntPtr NewObjectA;
        public IntPtr GetObjectClass;
        public IntPtr IsInstanceOf;
        public IntPtr GetMethodID;
        public IntPtr CallObjectMethod;
        public IntPtr CallObjectMethodV;
        public IntPtr CallObjectMethodA;
        public IntPtr CallBooleanMethod;
        public IntPtr CallBooleanMethodV;
        public IntPtr CallBooleanMethodA;
        public IntPtr CallByteMethod;
        public IntPtr CallByteMethodV;
        public IntPtr CallByteMethodA;
        public IntPtr CallCharMethod;
        public IntPtr CallCharMethodV;
        public IntPtr CallCharMethodA;
        public IntPtr CallShortMethod;
        public IntPtr CallShortMethodV;
        public IntPtr CallShortMethodA;
        public IntPtr CallIntMethod;
        public IntPtr CallIntMethodV;
        public IntPtr CallIntMethodA;
        public IntPtr CallLongMethod;
        public IntPtr CallLongMethodV;
        public IntPtr CallLongMethodA;
        public IntPtr CallFloatMethod;
        public IntPtr CallFloatMethodV;
        public IntPtr CallFloatMethodA;
        public IntPtr CallDoubleMethod;
        public IntPtr CallDoubleMethodV;
        public IntPtr CallDoubleMethodA;
        public IntPtr CallVoidMethod;
        public IntPtr CallVoidMethodV;
        public IntPtr CallVoidMethodA;
        public IntPtr CallNonvirtualObjectMethod;
        public IntPtr CallNonvirtualObjectMethodV;
        public IntPtr CallNonvirtualObjectMethodA;
        public IntPtr CallNonvirtualBooleanMethod;
        public IntPtr CallNonvirtualBooleanMethodV;
        public IntPtr CallNonvirtualBooleanMethodA;
        public IntPtr CallNonvirtualByteMethod;
        public IntPtr CallNonvirtualByteMethodV;
        public IntPtr CallNonvirtualByteMethodA;
        public IntPtr CallNonvirtualCharMethod;
        public IntPtr CallNonvirtualCharMethodV;
        public IntPtr CallNonvirtualCharMethodA;
        public IntPtr CallNonvirtualShortMethod;
        public IntPtr CallNonvirtualShortMethodV;
        public IntPtr CallNonvirtualShortMethodA;
        public IntPtr CallNonvirtualIntMethod;
        public IntPtr CallNonvirtualIntMethodV;
        public IntPtr CallNonvirtualIntMethodA;
        public IntPtr CallNonvirtualLongMethod;
        public IntPtr CallNonvirtualLongMethodV;
        public IntPtr CallNonvirtualLongMethodA;
        public IntPtr CallNonvirtualFloatMethod;
        public IntPtr CallNonvirtualFloatMethodV;
        public IntPtr CallNonvirtualFloatMethodA;
        public IntPtr CallNonvirtualDoubleMethod;
        public IntPtr CallNonvirtualDoubleMethodV;
        public IntPtr CallNonvirtualDoubleMethodA;
        public IntPtr CallNonvirtualVoidMethod;
        public IntPtr CallNonvirtualVoidMethodV;
        public IntPtr CallNonvirtualVoidMethodA;
        public IntPtr GetFieldID;
        public IntPtr GetObjectField;
        public IntPtr GetBooleanField;
        public IntPtr GetByteField;
        public IntPtr GetCharField;
        public IntPtr GetShortField;
        public IntPtr GetIntField;
        public IntPtr GetLongField;
        public IntPtr GetFloatField;
        public IntPtr GetDoubleField;
        public IntPtr SetObjectField;
        public IntPtr SetBooleanField;
        public IntPtr SetByteField;
        public IntPtr SetCharField;
        public IntPtr SetShortField;
        public IntPtr SetIntField;
        public IntPtr SetLongField;
        public IntPtr SetFloatField;
        public IntPtr SetDoubleField;
        public IntPtr GetStaticMethodID;
        public IntPtr CallStaticObjectMethod;
        public IntPtr CallStaticObjectMethodV;
        public IntPtr CallStaticObjectMethodA;
        public IntPtr CallStaticBooleanMethod;
        public IntPtr CallStaticBooleanMethodV;
        public IntPtr CallStaticBooleanMethodA;
        public IntPtr CallStaticByteMethod;
        public IntPtr CallStaticByteMethodV;
        public IntPtr CallStaticByteMethodA;
        public IntPtr CallStaticCharMethod;
        public IntPtr CallStaticCharMethodV;
        public IntPtr CallStaticCharMethodA;
        public IntPtr CallStaticShortMethod;
        public IntPtr CallStaticShortMethodV;
        public IntPtr CallStaticShortMethodA;
        public IntPtr CallStaticIntMethod;
        public IntPtr CallStaticIntMethodV;
        public IntPtr CallStaticIntMethodA;
        public IntPtr CallStaticLongMethod;
        public IntPtr CallStaticLongMethodV;
        public IntPtr CallStaticLongMethodA;
        public IntPtr CallStaticFloatMethod;
        public IntPtr CallStaticFloatMethodV;
        public IntPtr CallStaticFloatMethodA;
        public IntPtr CallStaticDoubleMethod;
        public IntPtr CallStaticDoubleMethodV;
        public IntPtr CallStaticDoubleMethodA;
        public IntPtr CallStaticVoidMethod;
        public IntPtr CallStaticVoidMethodV;
        public IntPtr CallStaticVoidMethodA;
        public IntPtr GetStaticFieldID;
        public IntPtr GetStaticObjectField;
        public IntPtr GetStaticBooleanField;
        public IntPtr GetStaticByteField;
        public IntPtr GetStaticCharField;
        public IntPtr GetStaticShortField;
        public IntPtr GetStaticIntField;
        public IntPtr GetStaticLongField;
        public IntPtr GetStaticFloatField;
        public IntPtr GetStaticDoubleField;
        public IntPtr SetStaticObjectField;
        public IntPtr SetStaticBooleanField;
        public IntPtr SetStaticByteField;
        public IntPtr SetStaticCharField;
        public IntPtr SetStaticShortField;
        public IntPtr SetStaticIntField;
        public IntPtr SetStaticLongField;
        public IntPtr SetStaticFloatField;
        public IntPtr SetStaticDoubleField;
        public IntPtr NewString;
        public IntPtr GetStringLength;
        public IntPtr GetStringChars;
        public IntPtr ReleaseStringChars;
        public IntPtr NewStringUTF;
        public IntPtr GetStringUTFLength;
        public IntPtr GetStringUTFChars;
        public IntPtr ReleaseStringUTFChars;
        public IntPtr GetArrayLength;
        public IntPtr NewObjectArray;
        public IntPtr GetObjectArrayElement;
        public IntPtr SetObjectArrayElement;
        public IntPtr NewBooleanArray;
        public IntPtr NewByteArray;
        public IntPtr NewCharArray;
        public IntPtr NewShortArray;
        public IntPtr NewIntArray;
        public IntPtr NewLongArray;
        public IntPtr NewFloatArray;
        public IntPtr NewDoubleArray;
        public IntPtr GetBooleanArrayElements;
        public IntPtr GetByteArrayElements;
        public IntPtr GetCharArrayElements;
        public IntPtr GetShortArrayElements;
        public IntPtr GetIntArrayElements;
        public IntPtr GetLongArrayElements;
        public IntPtr GetFloatArrayElements;
        public IntPtr GetDoubleArrayElements;
        public IntPtr ReleaseBooleanArrayElements;
        public IntPtr ReleaseByteArrayElements;
        public IntPtr ReleaseCharArrayElements;
        public IntPtr ReleaseShortArrayElements;
        public IntPtr ReleaseIntArrayElements;
        public IntPtr ReleaseLongArrayElements;
        public IntPtr ReleaseFloatArrayElements;
        public IntPtr ReleaseDoubleArrayElements;
        public IntPtr GetBooleanArrayRegion;
        public IntPtr GetByteArrayRegion;
        public IntPtr GetCharArrayRegion;
        public IntPtr GetShortArrayRegion;
        public IntPtr GetIntArrayRegion;
        public IntPtr GetLongArrayRegion;
        public IntPtr GetFloatArrayRegion;
        public IntPtr GetDoubleArrayRegion;
        public IntPtr SetBooleanArrayRegion;
        public IntPtr SetByteArrayRegion;
        public IntPtr SetCharArrayRegion;
        public IntPtr SetShortArrayRegion;
        public IntPtr SetIntArrayRegion;
        public IntPtr SetLongArrayRegion;
        public IntPtr SetFloatArrayRegion;
        public IntPtr SetDoubleArrayRegion;
        public IntPtr RegisterNatives;
        public IntPtr UnregisterNatives;
        public IntPtr MonitorEnter;
        public IntPtr MonitorExit;
        public IntPtr GetJavaVM;
        public IntPtr GetStringRegion;
        public IntPtr GetStringUTFRegion;
        public IntPtr GetPrimitiveArrayCritical;
        public IntPtr ReleasePrimitiveArrayCritical;
        public IntPtr GetStringCritical;
        public IntPtr ReleaseStringCritical;
        public IntPtr NewWeakGlobalRef;
        public IntPtr DeleteWeakGlobalRef;
        public IntPtr ExceptionCheck;
        public IntPtr NewDirectByteBuffer;
        public IntPtr GetDirectBufferAddress;
        public IntPtr GetDirectBufferCapacity;
        public IntPtr GetObjectRefType;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct JavaVM
    {
        public JNINativeInterface* functions;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct JNIEnv
    {
        public JNINativeInterface* functions;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct JavaVMOption
    {
        public string optionString;
        public void* extraInfo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    unsafe struct JavaVMInitArgs
    {
        public int version;
        public int nOptions;

        public void* options;

        public byte ignoreUnrecognized;
    }

    unsafe delegate int JNI_CreateJavaVM(JavaVM** pvm, JNIEnv** penv, IntPtr args);


    unsafe static class JVMLauncher
    {
        [DllImport("kernel32", EntryPoint = "LoadLibrary", SetLastError = true)]
        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi, EntryPoint = "GetProcAddress", ExactSpelling = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, String lpProcName);


        public static void Invoke(string Target, string EntryPointTypeName, params string[] args)
        {
            var jre = RegistryKey.OpenBaseKey(
                RegistryHive.LocalMachine,
                RegistryView.Registry32
            ).OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");

            var jvm = (string)jre.OpenSubKey((string)jre.GetValue("CurrentVersion")).GetValue("RuntimeLib");

            var CLASS_PATH = @"-Djava.class.path=" + Path.GetFullPath(Target);


            JVMLauncher.InternalInvoke(
                RUNTIME_DLL: jvm,
                CLASS_PATH: CLASS_PATH,
                CLASS_NAME: EntryPointTypeName.Replace(".", "/"),
                args: args
            );
        }

        public static void InternalInvoke(
            string RUNTIME_DLL = @"C:\Program Files\Java\jdk1.6.0_24\jre\bin\client\jvm.dll",
            string CLASS_PATH = @"-Djava.class.path=Z:\jsc.svn\examples\java\CLRJVMConsole\CLRJVMConsole\bin\Debug\staging\web\bin\CLRJVMConsole.dll",
            string CLASS_NAME = "CLRJVMConsole/Program",
            string[] args = null)
        {

            //var CLASS_PATH = @"-Djava.class.path=Z:\research\20110427_jvmdll\19065\JavaDaemon\HelloKNR\HelloKNR.jar";
            //var CLASS_NAME = "com/doorul/HelloKNR";



            var JNI_VERSION_1_4 = 0x00010004;

            //Console.WriteLine("LoadLibrary"); ;
            var handle = LoadLibrary(RUNTIME_DLL);

            //Console.WriteLine("GetProcAddress"); ;
            var __JNI_CreateJavaVM = GetProcAddress(handle, "JNI_CreateJavaVM");

            // http://www.codeproject.com/KB/cs/DynamicInvokeCSharp.aspx
            var JNI_CreateJavaVM = (JNI_CreateJavaVM)Marshal.GetDelegateForFunctionPointer(__JNI_CreateJavaVM, typeof(JNI_CreateJavaVM));


            var options = new JavaVMOption
            {
                optionString =
                    CLASS_PATH
            };
            //(void*)Marshal.StringToHGlobalAnsi(CLASS_PATH) };

            var options_ptr_len = Marshal.SizeOf(options) + 64;
            IntPtr options_ptr = Marshal.AllocHGlobal(options_ptr_len);
            Marshal.Copy(Enumerable.Range(0, options_ptr_len).Select(k => (byte)0xCC).ToArray(), 0, options_ptr, options_ptr_len);
            Marshal.StructureToPtr(options, options_ptr, false);


            var vm_args = new JavaVMInitArgs
            {
                version = JNI_VERSION_1_4,
                options = (void*)options_ptr,
                nOptions = 1,
                ignoreUnrecognized = 0
            };

            JavaVM* vm;
            JNIEnv* env;



            // http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.marshal.structuretoptr.aspx#Y800
            var vm_args_ptr_len = Marshal.SizeOf(vm_args) + 64;
            IntPtr vm_args_ptr = Marshal.AllocHGlobal(vm_args_ptr_len);
            Marshal.Copy(Enumerable.Range(0, vm_args_ptr_len).Select(k => (byte)0xCC).ToArray(), 0, vm_args_ptr, vm_args_ptr_len);

            Marshal.StructureToPtr(vm_args, vm_args_ptr, false);

            //Console.WriteLine("JNI_CreateJavaVM"); ;
            var res = JNI_CreateJavaVM((JavaVM**)&vm, (JNIEnv**)&env, vm_args_ptr);

            var FindClass = (JNINativeInterface_FindClass)Marshal.GetDelegateForFunctionPointer(
                env->functions->FindClass,
                typeof(JNINativeInterface_FindClass)
            );

            //Console.WriteLine("FindClass"); ;

            var cls = FindClass(env,
                CLASS_NAME
                );


            //(void*)Marshal.StringToHGlobalAnsi(CLASS_NAME));


            var GetStaticMethodID = (JNINativeInterface_GetStaticMethodID)Marshal.GetDelegateForFunctionPointer(
                env->functions->GetStaticMethodID,
                typeof(JNINativeInterface_GetStaticMethodID)
            );


            //Console.WriteLine("GetStaticMethodID"); 
            var mid = GetStaticMethodID(
                env,
                cls,
                "main",
                "([Ljava/lang/String;)V"
                //(void*)Marshal.StringToHGlobalAnsi("main"),
                //(void*)Marshal.StringToHGlobalAnsi("([Ljava/lang/String;)V")
            );

            var CallStaticVoidMethodA = (JNINativeInterface_CallStaticVoidMethodA)Marshal.GetDelegateForFunctionPointer(
               env->functions->CallStaticVoidMethodA,
               typeof(JNINativeInterface_CallStaticVoidMethodA)
           );

            var NewObjectArray = (JNINativeInterface_NewObjectArray)Marshal.GetDelegateForFunctionPointer(
               env->functions->NewObjectArray,
               typeof(JNINativeInterface_NewObjectArray)
            );

            var NewStringUTF = (JNINativeInterface_NewStringUTF)Marshal.GetDelegateForFunctionPointer(
               env->functions->NewStringUTF,
               typeof(JNINativeInterface_NewStringUTF)
            );

            var SetObjectArrayElement = (JNINativeInterface_SetObjectArrayElement)Marshal.GetDelegateForFunctionPointer(
              env->functions->SetObjectArrayElement,
              typeof(JNINativeInterface_SetObjectArrayElement)
            );

            // http://www.velocityreviews.com/forums/t370129-java-native-interface-translate-java-call-to-jni.html

            //Console.WriteLine("FindClass"); 

            ///* build the argument list */
            var str = FindClass(env, "java/lang/String");
            var num_args = args.Length;
            //Console.WriteLine("NewObjectArray");
            var jargs = NewObjectArray(env, num_args, str, null);

            for (int i = 0; i < args.Length; i++)
            {
                //Console.WriteLine("NewStringUTF");
                //Console.WriteLine("NewStringUTF: " + env->functions->NewStringUTF.ToInt32().ToString("x8"));
                //Console.WriteLine("env: " + new IntPtr(env).ToInt32().ToString("x8"));
                //Console.WriteLine("i: " + i);
                //Console.WriteLine("args[i]: " + args[i]);

                var value = NewStringUTF(env, args[i]);
                //Console.WriteLine("SetObjectArrayElement");

                SetObjectArrayElement(env, jargs, i, (void*)value);

            }
            ///* prefer to do this in a loop if args already in an array of char* */
            //SetObjectArrayElement(env, jargs, 1, (void*)NewStringUTF(env, "world"));

            //(*env)->SetObjectArrayElement(env, jargs, 0, (*env)->NewStringUTF(env, "-fo"))
            //(*env)->SetObjectArrayElement(env, jargs, 1, (*env)->NewStringUTF(env, "Data.xml"))
            //(*env)->SetObjectArrayElement(env, jargs, 2, (*env)->NewStringUTF(env, "-c"))
            ///* etcetera */

            var mm = new MemoryStream();
            var mw = new BinaryWriter(mm);

            mw.Write(new IntPtr(jargs).ToInt32());
            mw.Write((int)0);

            //Marshal.Copy(Enumerable.Range(0, 8).Select(k => (byte)0x00).ToArray(), 0, __args, 8);

            IntPtr __args = Marshal.AllocHGlobal(8);
            Marshal.Copy(mm.ToArray(), 0, __args, 8);


            //CallStaticVoidMethodA(env, cls, mid, (void*)__args);
            //Console.WriteLine("CallStaticVoidMethodA");
            CallStaticVoidMethodA(env, cls, mid, (void*)__args);
        }
    }

}
