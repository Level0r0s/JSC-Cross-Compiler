using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRVrCubeWorldSurfaceViewXNDK
{
    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

        //static object AppThreadFunction(object arg)



        public const int MAX_MESSAGES = 1024;



        public enum ovrMQWait
        {
            MQ_WAIT_NONE,		// don't wait
            MQ_WAIT_RECEIVED,	// wait until the consumer thread has received the message
            MQ_WAIT_PROCESSED	// wait until the consumer thread has processed the message
        };

        [Script]
        public struct ovrMessageParms
        {
            // union?

            public object Pointer;
            public int Integer;
            public float Float;


        }

        [Script]
        // why struct?
        // sent to ovrMessageQueue_PostMessage
        public struct ovrMessage
        {
            public VrCubeWorld.MESSAGE Id;
            public ovrMQWait Wait;

            // bit64 union?
            //public long[] Parms;
            public ovrMessageParms[] Parms;

            public void ovrMessage_Init(MESSAGE id, ovrMQWait wait)
            {
                // this byref?

                this.Id = id;
                this.Wait = wait;
                //memset( message->Parms, 0, sizeof( message->Parms ) );

                this.Parms = new ovrMessageParms[MAX_MESSAGES];

                // calloc
                // http://stackoverflow.com/questions/1622196/malloc-zeroing-out-memory

            }

            public void ovrMessage_SetPointerParm(int i, object value)
            {
                Parms[i].Pointer = value;
            }

            public object ovrMessage_GetPointerParm(int i)
            {
                //var p = (size_t*)Parms[i];
                //return (object)(*p);
                return Parms[i].Pointer;
            }

            public void ovrMessage_SetIntegerParm(int i, int value)
            {
                //var p = (int*)Parms[i];
                //*p = value;
                Parms[i].Integer = value;
            }

            public int ovrMessage_GetIntegerParm(int i)
            {
                //var p = (int*)Parms[i];
                //return *p;

                return Parms[i].Integer;
            }

            public void ovrMessage_SetFloatParm(int i, float value)
            {
                //var p = (float*)Parms[i];
                Parms[i].Float = value;
            }
            public float ovrMessage_GetFloatParm(int i)
            {
                //jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c: In function 'OVRVrCubeWorldSurfaceViewXNDK_VrCubeWorld_ovrMessage_ovrMessage_SetFloatParm':
                //jni/OVRVrCubeWorldSurfaceViewXNDK.dll.c:221:22: error: incompatible types when assigning to type 'void *' from type 'float'
                //     __that->Parms[i] = value;

                // X:\jsc.svn\examples\c\Test\TestFloatToObject\TestFloatToObject\Program.cs

                //var p = (float*)Parms[i];
                return Parms[i].Float;
                //return *p;
            }
        }





        // nameless in the c file.
        public enum MESSAGE
        {
            MESSAGE_ON_CREATE,
            MESSAGE_ON_START,
            MESSAGE_ON_RESUME,
            MESSAGE_ON_PAUSE,
            MESSAGE_ON_STOP,
            MESSAGE_ON_DESTROY,
            MESSAGE_ON_SURFACE_CREATED,
            MESSAGE_ON_SURFACE_DESTROYED,
            MESSAGE_ON_KEY_EVENT,
            MESSAGE_ON_TOUCH_EVENT
        }



        // field of ovrAppThread
        // struct? created by?
        [Script]
        public struct ovrMessageQueue
        {
            // fixed array?
            public ovrMessage[] Messages;

            // does js do volatile? not yet?
            public /* volatile*/ int Head;  // dequeue at the head
            public /* volatile*/ int Tail;  // enqueue at the tail
            public /* volatile*/ bool Enabled;
            public ovrMQWait Wait;
            public pthread_mutex_t Mutex;
            public pthread_cond_t Posted;
            public pthread_cond_t Received;
            public pthread_cond_t Processed;



            // ctor?
            // called by ovrAppThread_Create
            public void ovrMessageQueue_Create()
            {
                this.Head = 0;
                this.Tail = 0;
                this.Enabled = false;
                this.Wait = ovrMQWait.MQ_WAIT_NONE;

                var attr = default(pthread_mutexattr_t);

                pthread.pthread_mutexattr_init(ref attr);
                pthread.pthread_mutexattr_settype(ref attr, PTHREAD_MUTEX.PTHREAD_MUTEX_ERRORCHECK);
                pthread.pthread_mutex_init(out Mutex, ref attr);
                pthread.pthread_mutexattr_destroy(ref attr);

                pthread.pthread_cond_init(ref Posted, null);
                pthread.pthread_cond_init(ref Received, null);
                pthread.pthread_cond_init(ref Processed, null);
            }

            // called by ovrAppThread_Destroy
            public void ovrMessageQueue_Destroy()
            {
                pthread.pthread_mutex_destroy(ref Mutex);
                pthread.pthread_cond_destroy(ref Posted);
                pthread.pthread_cond_destroy(ref Received);
                pthread.pthread_cond_destroy(ref Processed);
            }

            public void ovrMessageQueue_Enable(bool value) { Enabled = value; }

            // called by Java_com_oculus_gles3jni_GLES3JNILib_onTouchEvent
            // uithread
            public void ovrMessageQueue_PostMessage(ref ovrMessage message)
            {
                if (!this.Enabled)
                {
                    return;
                }
                while (Tail - Head >= MAX_MESSAGES)
                {
                    // block for a second until messages are processed in the worker?
                    // usleep(microseconds)
                    unistd.usleep(1000);
                }

                // lock(?)
                pthread.pthread_mutex_lock(ref Mutex);
                Messages[Tail & (MAX_MESSAGES - 1)] = message;

                Tail++;
                pthread.pthread_cond_broadcast(ref Posted);
                if (Wait == ovrMQWait.MQ_WAIT_RECEIVED)
                {
                    pthread.pthread_cond_wait(ref Received, ref Mutex);
                }
                else if (Wait == ovrMQWait.MQ_WAIT_PROCESSED)
                {
                    pthread.pthread_cond_wait(ref Processed, ref Mutex);
                }
                pthread.pthread_mutex_unlock(ref Mutex);
            }

            // appworkerThread
            // called by ovrMessageQueue_GetNextMessage
            public void ovrMessageQueue_SleepUntilMessage()
            {
                if (Wait == ovrMQWait.MQ_WAIT_PROCESSED)
                {
                    pthread.pthread_cond_broadcast(ref Processed);
                    Wait = ovrMQWait.MQ_WAIT_NONE;
                }
                pthread.pthread_mutex_lock(ref Mutex);
                if (Tail > Head)
                {
                    pthread.pthread_mutex_unlock(ref Mutex);
                    return;
                }
                pthread.pthread_cond_wait(ref Posted, ref Mutex);
                pthread.pthread_mutex_unlock(ref Mutex);
            }


            // called by AppThreadFunction
            public bool ovrMessageQueue_GetNextMessage(out ovrMessage message, bool waitForMessages)
            {
                // 1716

                // script: error JSC1000: C : Opcode not implemented: volatile. at OVRVrCubeWorldSurfaceViewXNDK.VrCubeWorld+ovrAppThread+ovrMessageQueue.ovrMessageQueue_GetNextMessage

                if (this.Wait == ovrMQWait.MQ_WAIT_PROCESSED)
                {
                    pthread.pthread_cond_broadcast(ref Processed);

                    this.Wait = ovrMQWait.MQ_WAIT_NONE;
                }

                if (waitForMessages)
                {
                    ovrMessageQueue_SleepUntilMessage();
                }

                pthread.pthread_mutex_lock(ref this.Mutex);

                message = default(ovrMessage);

                if (this.Tail <= this.Head)
                {
                    pthread.pthread_mutex_unlock(ref Mutex);
                    return false;
                }

                message = this.Messages[Head & (MAX_MESSAGES - 1)];

                this.Head++;
                pthread.pthread_mutex_unlock(ref Mutex);

                if (this.Wait == ovrMQWait.MQ_WAIT_RECEIVED)
                {
                    pthread.pthread_cond_broadcast(ref Processed);
                }
                else if (this.Wait == ovrMQWait.MQ_WAIT_PROCESSED)
                {
                    this.Wait = ovrMQWait.MQ_WAIT_PROCESSED;
                }

                return false;
            }
        }

    }


}
