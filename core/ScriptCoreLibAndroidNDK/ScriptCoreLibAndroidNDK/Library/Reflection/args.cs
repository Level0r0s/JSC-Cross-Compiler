using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.Library.Reflection
{
    // this will end up in CLR Type API

    [Script]
    class args<T>
    {
    }

    // can we have types that are a class and a struct per caller intent?
    [Script]
    public class argsF
    {
        public JNIEnv env;
        public jobject fields;

        // 
        public float this[string fname]
        {
            get
            {
                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeFlashlightTracker\ChromeFlashlightTracker\Application.cs

                var fields_GetType = env.GetObjectClass(env, fields);
                var fref = env.GetFieldID(env, fields_GetType, fname, "F");

                var value = env.GetFloatField(env, fields, fref);

                return value;
            }

            set
            {
                var fields_GetType = env.GetObjectClass(env, fields);
                var fref = env.GetFieldID(env, fields_GetType, fname, "F");

                env.SetFloatField(env, fields, fref, value);
            }
        }
    }

    [Script]
    public class argsI
    {
        public JNIEnv env;
        public jobject fields;

        // 
        public int this[string fname]
        {

            get
            {
                var fields_GetType = env.GetObjectClass(env, fields);
                var fref = env.GetFieldID(env, fields_GetType, fname, "I");

                var value = env.GetIntField(env, fields, fref);

                return value;
            }
        }
    }

    [Script]
    public class argsI64
    {
        public JNIEnv env;
        public jobject fields;

        // 
        public long this[string fname]
        {

            set
            {
                var fields_GetType = env.GetObjectClass(env, fields);
                var fref = env.GetFieldID(env, fields_GetType, fname, "J");

                env.SetLongField(env, fields, fref, value);
            }
        }
    }

    [Script]
    unsafe public struct ByteArrayWithLength
    {
        public byte[] Buffer;
        public int BufferLength;

        // cpp sends us a file name, we import it into our memory, cpp trashes/delocallocs the memory
        public void FromString(byte* ptr)
        {
            // X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\xNativeActivity.cs

            //var MAXPATH = 0xfff;
            const int MAXPATH = 0xfff;

            if (Buffer == null)
            {
                var loc_Buffer = new byte[MAXPATH];
                Buffer = loc_Buffer;
            }

            for (int i = 0; i < MAXPATH; i++)
            {
                var c = ptr[i];

                Buffer[i] = c;

                // memory not valid beyond this point..
                if (c == 0x0)
                {
                    i = MAXPATH;

                    //break;

                }

            }
        }

        public string AsString()
        {
            return (string)(object)Buffer;
        }
    }


    [Script]
    public unsafe class argsByteArray
    {
        public JNIEnv env;
        public jobject fields;

        public byte[] Buffer;
        public int BufferLength;

        public argsByteArray()
        {
            // um. why do we have to set it explictly?
            Buffer = null;
        }

        public ByteArrayWithLength this[string fname]
        {

            get
            {
                //ConsoleExtensions.trace("enter argsByteArray");

                var iscopy = default(bool);

                var fields_GetType = env.GetObjectClass(env, fields);
                var fref = env.GetFieldID(env, fields_GetType, fname, "[B");

                var v = env.GetObjectField(env, fields, fref);

                // I/DEBUG   ( 2940): Abort message: 'sart/runtime/check_jni.cc:65] JNI DETECTED ERROR IN APPLICATION: jarray was NULL'

                if (v == null)
                {
                    ConsoleExtensions.tracei("new Buffer null");
                }
                else
                {
                    var len = env.GetArrayLength(env, v);
                    //ConsoleExtensions.tracei("GetArrayLength", len);
                    if (len > 0)
                    {
                        if (Buffer == null)
                        {
                            // first timer. easy.
                            BufferLength = len;
                            Buffer = new byte[len];

                            ConsoleExtensions.tracei("new Buffer ", len);


                            //jni/OVRWindWheelNDK.dll.c:471:135: error: 'flag4' undeclared (first use in this function)
                            //         byte_5 = (/* typecast */(unsigned char*(*)(JNIEnv*, void*, int*))(*__that->env)->GetByteArrayElements)(__that->env, object2, &flag4);
                            var loc = env.GetByteArrayElements(env, v, out iscopy);

                            if (loc != null)
                            {
                                for (int i = 0; i < len; i++)
                                {
                                    Buffer[i] = loc[i];
                                }

                                env.ReleaseByteArrayElements(env, v, loc);
                            }

                        }
                        else
                        {
                            // same size... no redim.
                            //ConsoleExtensions.tracei("reuse Buffer ", len);


                            //Array.Resize(ref this.Buffer, len);

                            // A pointer to the reallocated memory block, which may be either the same as ptr or a new location.

                            if (BufferLength != len)
                            {
                                this.Buffer = stdlib_h.realloc(this.Buffer, len);
                                this.BufferLength = len;
                            }

                            var loc = env.GetByteArrayElements(env, v, out iscopy);
                            if (loc != null)
                            {
                                for (int i = 0; i < len; i++)
                                {
                                    Buffer[i] = loc[i];
                                }

                                env.ReleaseByteArrayElements(env, v, loc);
                            }
                        }
                    }
                }

                //env.SetLongField(env, fields, fref, value);

                var copyout = new ByteArrayWithLength { Buffer = this.Buffer, BufferLength = this.BufferLength };

                return copyout;
            }
        }
    }



}
