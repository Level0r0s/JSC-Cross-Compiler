using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content.res;

namespace android.content
{
    [Script(IsNative = true)]
    public interface SharedPreferences_Editor
    {
        bool commit();

        SharedPreferences_Editor putInt(string arg0, int arg1);
    }

    // http://developer.android.com/reference/android/content/SharedPreferences.html
    [Script(IsNative = true)]
    public interface SharedPreferences
    {// Z:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosHUD\VrActivity.cs

        SharedPreferences_Editor edit();


        string getString(string key, string defValue);

        int getInt(string arg0, int arg1);
    }
}
