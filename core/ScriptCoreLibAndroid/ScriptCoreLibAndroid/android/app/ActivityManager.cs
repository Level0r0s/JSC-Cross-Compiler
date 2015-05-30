using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using android.content;

namespace android.app
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/ActivityManager.java
    // http://developer.android.com/reference/android/app/ActivityManager.html
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/app/ActivityManagerNative.java
    // GET_RUNNING_SERVICE_CONTROL_PANEL_TRANSACTION
    // http://grepcode.com/file_/repository.grepcode.com/java/ext/com.google.android/android/5.1.0_r1/com/android/server/am/ActivityManagerService.java/?v=source

    [Script(IsNative = true)]
    public class ActivityManager
    {
        // http://developer.android.com/reference/android/app/ActivityManager.RunningServiceInfo.html
        [Script(IsNative = true)]
        public class RunningServiceInfo
        {
            public int pid;
            public string process;
            public ComponentName service;
            public bool started;
        }

        // members and types are to be extended by jsc at release build
        // http://stackoverflow.com/questions/2002288/static-way-to-get-context-on-android

        //public List<ActivityManager.RunningServiceInfo> getRunningServices(int maxNum)
        public java.util.List getRunningServices(int maxNum)
        {
            return null;
        }

        public PendingIntent getRunningServiceControlPanel(ComponentName service)
        {
            return null;
        }
    }
}
