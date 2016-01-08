using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.SystemHeaders.sys
{
    // "R:\util\android-ndk-r10e\platforms\android-21\arch-arm64\usr\include\sys\capability.h"
    // "x:\util\android-ndk-r10e\platforms\android-21\arch-arm64\usr\include\sys\capability.h"


    [Script(IsNative = true, Header = "sys/capability.h", IsSystemHeader = true)]
    public static class capability_h
    {
        // used by?

        //extern int capget(cap_user_header_t hdrp, cap_user_data_t datap);
        //extern int capset(cap_user_header_t hdrp, const cap_user_data_t datap);
    }

}
