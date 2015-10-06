using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.widget;
using ScriptCoreLib;
using android.util;

namespace android.content.res
{
    // http://developer.android.com/reference/android/content/res/Resources.html
    [Script(IsNative = true)]
    public class Resources
    {
        // members and types are to be extended by jsc at release build

        public Configuration getConfiguration()
        {
            throw null;
        }

        public int getIdentifier(string name, string defType, string defPackage)
        {

            throw null;
        }

        public AssetFileDescriptor openRawResourceFd(int id)
        {
            throw null;
        }

        public DisplayMetrics getDisplayMetrics()
        {
            throw null;

        }


        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150526
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150529
        // tested by?
        // X:\jsc.svn\examples\java\android\Test\TestNuGetAssetsConsumer\TestNuGetAssetsConsumer\ApplicationActivity.cs
        public virtual AssetManager getAssets()
        {
            return default(AssetManager);
        }

        public string getText(int id) { throw null; }
    }
}
