using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.widget;
using ScriptCoreLib;
using java.io;

namespace android.content.res
{
    //  https://github.com/android/platform_frameworks_base/blob/master/core/java/android/content/res/AssetFileDescriptor.java

    [Script(IsNative = true)]
    public class AssetFileDescriptor
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\asset_manager.cs

        // members and types are to be extended by jsc at release build

        public long getLength()
        {
            return 0;
        }
    }
}
