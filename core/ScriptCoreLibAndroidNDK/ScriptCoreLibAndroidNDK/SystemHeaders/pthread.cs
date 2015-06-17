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

    //[Script(IsNative = true)]
    //public enum pthread_t { }


    [Script(IsNative = true, Header = "pthread.h", IsSystemHeader = true)]
    public unsafe static class pthread
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Exception.cs

        // typedef long pthread_t;
        //public enum pthread_t { }



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

        public static int pthread_join(pthread_t x, void** y) { return 0; }
    }


}
