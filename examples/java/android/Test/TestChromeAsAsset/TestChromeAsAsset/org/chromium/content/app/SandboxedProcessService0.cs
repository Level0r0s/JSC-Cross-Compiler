using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace org.chromium.content.app
{
    // U:\chromium\src\content\public\android\java\src\org\chromium\content\app\ChildProcessService.java
    // U:\chromium\src\content\public\android\java\src\org\chromium\content\app\SandboxedProcessService.java
    // U:\chromium\src\content\public\android\java\src\org\chromium\content\app\SandboxedProcessService0.java
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201506/20150601

    // This is needed to register multiple SandboxedProcess services so that we can have more than one sandboxed process.

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "org.chromium.content.browser.NUM_SANDBOXED_SERVICES", value = "6")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":sandboxed_process0")]
    public class SandboxedProcessService0 : SandboxedProcessService { }
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":sandboxed_process1")]
    public class SandboxedProcessService1 : SandboxedProcessService { }
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":sandboxed_process2")]
    public class SandboxedProcessService2 : SandboxedProcessService { }
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":sandboxed_process3")]
    public class SandboxedProcessService3 : SandboxedProcessService { }
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":sandboxed_process4")]
    public class SandboxedProcessService4 : SandboxedProcessService { }
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":sandboxed_process5")]
    public class SandboxedProcessService5 : SandboxedProcessService { }
}
