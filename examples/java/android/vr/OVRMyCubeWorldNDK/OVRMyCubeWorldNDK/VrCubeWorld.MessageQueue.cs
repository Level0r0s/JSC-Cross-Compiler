using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLibNative.SystemHeaders.android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVRMyCubeWorldNDK
{
    // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150619/ovrvrcubeworldsurfaceviewx
    public struct ovrMessageParms
    {
        // X:\jsc.svn\examples\java\android\vr\OVRMyCubeWorldNDK\OVRMyCubeWorldNDK\VrCubeWorld.MessageQueue.cs

        // union?

        public object Pointer;
        //public static explicit operator object(ovrMessageParms value)
        //{
        //    var c = value;
        //    return c.Pointer;
        //}

        public int Integer;
        public static implicit operator ovrMessageParms(int value)
        {
            return new ovrMessageParms { Integer = value };
        }
        public static implicit operator int(ovrMessageParms value)
        {
            var c = value;
            return c.Integer;
            //return value.Integer;
        }

        public float Float;
        public static implicit operator ovrMessageParms(float value)
        {
            return new ovrMessageParms { Float = value };
        }
        public static implicit operator float(ovrMessageParms value)
        {
            var c = value;
            return c.Float;
        }
    }

    public static unsafe partial class VrCubeWorld
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewNDK\staging\jni\VrCubeWorld_SurfaceView.c

        public const int MAX_MESSAGES = 1024;

        public enum ovrMQWait
        {
            MQ_WAIT_NONE,		// don't wait
            MQ_WAIT_RECEIVED,	// wait until the consumer thread has received the message
            MQ_WAIT_PROCESSED	// wait until the consumer thread has processed the message
        };



        // why struct?
        // sent to ovrMessageQueue_PostMessage
        // this is like intentextras between processes?
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

            #region int
            public ovrMessageParms this[int i]
            {
                set
                {
                    Parms[i] = value;
                }
                get
                {
                    return Parms[i];
                }
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
            #endregion

            // Error	5	Type 'OVRMyCubeWorldNDK.VrCubeWorld.ovrMessage' already defines a member called 'this' with the same parameter types	X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRMyCubeWorldNDK\VrCubeWorld.MessageQueue.cs	103	26	OVRMyCubeWorldNDK

            //public float this[int i]
            //{
            //    set
            //    {
            //        Parms[i].Float = value;
            //    }
            //    get
            //    {
            //        return Parms[i].Float;
            //    }
            //}

            public void ovrMessage_SetFloatParm(int i, float value)
            {
                //var p = (float*)Parms[i];
                Parms[i].Float = value;
            }
            public float ovrMessage_GetFloatParm(int i)
            {
                //jni/OVRMyCubeWorldNDK.dll.c: In function 'OVRVrCubeWorldSurfaceViewXNDK_VrCubeWorld_ovrMessage_ovrMessage_SetFloatParm':
                //jni/OVRMyCubeWorldNDK.dll.c:221:22: error: incompatible types when assigning to type 'void *' from type 'float'
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
        // created by ovrAppThread
        public class ovrMessageQueue
        {
            // fixed array?
            public readonly ovrMessage[] Messages = new ovrMessage[MAX_MESSAGES];

            // does js do volatile? not yet? while does not like it.
            public /* volatile*/ int Head = 0;   // dequeue at the head
            public /* volatile*/ int Tail = 0;  // enqueue at the tail
            public /*volatile */bool Enabled = false;

            // setb ?
            public ovrMQWait Wait = ovrMQWait.MQ_WAIT_NONE;

            public pthread_mutex_t Mutex;
            public pthread_cond_t Posted;
            public pthread_cond_t Received;
            public pthread_cond_t Processed;


            public ovrMessageQueue()
            {
                // 1644
                ConsoleExtensions.tracei("enter ovrMessageQueue");

                var attr = default(pthread_mutexattr_t);

                pthread.pthread_mutexattr_init(ref attr);
                pthread.pthread_mutexattr_settype(ref attr, PTHREAD_MUTEX.PTHREAD_MUTEX_ERRORCHECK);
                pthread.pthread_mutex_init(out Mutex, ref attr);
                pthread.pthread_mutexattr_destroy(ref attr);

                pthread.pthread_cond_init(ref Posted, null);
                pthread.pthread_cond_init(ref Received, null);
                pthread.pthread_cond_init(ref Processed, null);

                //ConsoleExtensions.tracei("exit ovrMessageQueue");
            }

            // called by ovrAppThread_Destroy
            public void ovrMessageQueue_Destroy()
            {
                // 1661

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
                // 1674
                //ConsoleExtensions.tracei("enter ovrMessageQueue_PostMessage", (int)message.Id);

                // enabled by?
                if (!this.Enabled)
                {
                    ConsoleExtensions.trace("exit ovrMessageQueue_PostMessage, disabled!");
                    return;
                }
                while (this.Tail - this.Head >= MAX_MESSAGES)
                {
                    // block for a second until messages are processed in the worker?
                    // usleep(microseconds)
                    unistd.usleep(1000);
                }

                // lock(?)
                pthread.pthread_mutex_lock(ref this.Mutex);
                Messages[Tail & (MAX_MESSAGES - 1)] = message;
                Tail++;

                pthread.pthread_cond_broadcast(ref Posted);
                if (message.Wait == ovrMQWait.MQ_WAIT_RECEIVED)
                {
                    //ConsoleExtensions.tracei("ovrMessageQueue_PostMessage MQ_WAIT_RECEIVED, pthread_cond_wait");
                    pthread.pthread_cond_wait(ref Received, ref Mutex);
                }
                else if (message.Wait == ovrMQWait.MQ_WAIT_PROCESSED)
                {
                    //ConsoleExtensions.tracei("ovrMessageQueue_PostMessage MQ_WAIT_PROCESSED, pthread_cond_wait");
                    pthread.pthread_cond_wait(ref Processed, ref Mutex);
                    //ConsoleExtensions.tracei("ovrMessageQueue_PostMessage MQ_WAIT_PROCESSED, pthread_cond_wait done");
                }
                pthread.pthread_mutex_unlock(ref Mutex);

                //ConsoleExtensions.tracei("exit ovrMessageQueue_PostMessage");
            }

            // appworkerThread
            // called by ovrMessageQueue_GetNextMessage
            public void ovrMessageQueue_SleepUntilMessage()
            {
                // 1699
                //ConsoleExtensions.tracei("ovrMessageQueue_SleepUntilMessage");

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
                //ConsoleExtensions.tracei("enter ovrMessageQueue_GetNextMessage");

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


                if (this.Tail <= this.Head)
                {
                    pthread.pthread_mutex_unlock(ref Mutex);
                    message = default(ovrMessage);
                    return false;
                }

                message = this.Messages[Head & (MAX_MESSAGES - 1)];

                this.Head++;
                pthread.pthread_mutex_unlock(ref Mutex);

                if (message.Wait == ovrMQWait.MQ_WAIT_RECEIVED)
                {
                    // 1736!
                    //pthread.pthread_cond_broadcast(ref Processed);
                    pthread.pthread_cond_broadcast(ref Received);
                }
                else if (message.Wait == ovrMQWait.MQ_WAIT_PROCESSED)
                {
                    this.Wait = ovrMQWait.MQ_WAIT_PROCESSED;
                }

                //ConsoleExtensions.tracei("exit ovrMessageQueue_GetNextMessage");
                return true;
            }
        }

    }


}
