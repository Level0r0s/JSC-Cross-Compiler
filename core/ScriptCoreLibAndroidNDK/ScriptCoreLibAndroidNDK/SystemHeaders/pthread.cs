using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\pthread.h"

    //[Script(IsNative = true, PointerName = "pthread_t")]
    [Script(IsNative = true)]
    public struct pthread_t //: pthread_h
    {
    }

    // allow null
    [Script(IsNative = true)]
    public class pthread_condattr_t //: pthread_h
    {
    }


    [Script(IsNative = true)]
    public struct pthread_mutexattr_t //: pthread_h
    {
    }

    [Script(IsNative = true)]
    public struct pthread_mutex_t //: pthread_h
    {
    }

    [Script(IsNative = true)]
    public struct pthread_cond_t //: pthread_h
    {
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.MessageQueue.cs
    }

    //[Script(IsNative = true)]
    //public enum pthread_t { }


    //[Script(IsNative = true)]
    public enum PTHREAD_MUTEX
    {
        PTHREAD_MUTEX_NORMAL = 0,
        PTHREAD_MUTEX_RECURSIVE = 1,
        PTHREAD_MUTEX_ERRORCHECK = 2,

        PTHREAD_MUTEX_ERRORCHECK_NP = PTHREAD_MUTEX_ERRORCHECK,
        PTHREAD_MUTEX_RECURSIVE_NP = PTHREAD_MUTEX_RECURSIVE,

        PTHREAD_MUTEX_DEFAULT = PTHREAD_MUTEX_NORMAL
    } ;


    [Script(IsNative = true, Header = "pthread.h", IsSystemHeader = true)]
    public unsafe static class pthread
    {
        

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Exception.cs

        // typedef long pthread_t;
        //public enum pthread_t { }


        #region pthread_create
        [Script(IsNative = true)]
        public delegate object start_routine(object arg);

        //x:/util/android-ndk-r10e/platforms/android-21/arch-arm/usr/include/pthread.h:177:5: note: expected 'void * (*)(void *)' but argument is of type 'void (*)(void)'
        // int pthread_create(pthread_t*, pthread_attr_t const*, void *(*)(void*), void*) __nonnull((1, 3));


        // http://man7.org/linux/man-pages/man3/pthread_create.3.html
        public static int pthread_create(out pthread_t thread, void* attr, start_routine start_routine, object arg) { throw null; }


        // allow typed callback 
        [Script(IsNative = true)]
        public delegate object start_routine<TArg0>(TArg0 arg);
        public static int pthread_create<TArg0>(out pthread_t thread, void* attr, start_routine<TArg0> start_routine, TArg0 arg) { throw null; }
        #endregion


        public static int pthread_join(pthread_t x, void** y) { return 0; }


        public static int pthread_cond_broadcast(ref pthread_cond_t c) { return 0; }

        public static int pthread_cond_wait(ref pthread_cond_t c, ref pthread_mutex_t m) { return 0; }

        public static int pthread_cond_destroy(ref pthread_cond_t c) { return 0; }

        public static int pthread_cond_init(ref pthread_cond_t c, pthread_condattr_t a) { return 0; }



        public static int pthread_mutexattr_init(ref pthread_mutexattr_t a) { return 0; }
        public static int pthread_mutexattr_settype(ref pthread_mutexattr_t a, PTHREAD_MUTEX t) { return 0; }
        public static int pthread_mutexattr_destroy(ref pthread_mutexattr_t a) { return 0; }

        public static int pthread_mutex_init(out pthread_mutex_t m, ref pthread_mutexattr_t ma) { return 0; }


        public static int pthread_mutex_lock(ref pthread_mutex_t m) { return 0; }
        public static int pthread_mutex_unlock(ref pthread_mutex_t m) { return 0; }


        public static int pthread_mutex_destroy(ref pthread_mutex_t m) { return 0; }


    }


}
