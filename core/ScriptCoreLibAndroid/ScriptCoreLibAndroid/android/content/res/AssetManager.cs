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
    //  https://github.com/android/platform_frameworks_base/blob/master/core/java/android/content/res/AssetManager.java

    [Script(IsNative = true)]
    public class AssetManager 
    {
        // X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\android\asset_manager.cs

        // members and types are to be extended by jsc at release build

        public virtual string[] list(string value)
        {
            return default(string[]);
        }

        public virtual InputStream open(string value)
        {
            return default(InputStream);
        }

        public  AssetFileDescriptor openFd(String fileName)
        {
            return null;
        }
    }
}
