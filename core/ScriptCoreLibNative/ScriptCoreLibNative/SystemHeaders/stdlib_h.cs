using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders
{
    // "X:\util\android-ndk-r10e\platforms\android-21\arch-arm\usr\include\stdlib.h"
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\SystemHeaders\stdlib_h.cs

    /// <summary>
    /// http://www.unet.univie.ac.at/aix/libs/basetrf1/malloc.htm
    /// </summary>
    /// 
    [Script(IsNative = true, Header = "stdlib.h", IsSystemHeader = true)]
    public unsafe static class stdlib_h
    {
        // http://www.cplusplus.com/reference/cstdlib/malloc/

        public unsafe static void* malloc(int size)
        {
            return default(void*);
        }

        // used by?

        public static object realloc(object ptr, int size)
        {
            return default(object);
        }

        public static T realloc<T>(T ptr, int size)
        {
            return default(T);
        }


        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.cs
        public static void free(object e)
        {

        }

        // android only?

        public static double drand48() 
        {
            throw null;
        }
    }

}
