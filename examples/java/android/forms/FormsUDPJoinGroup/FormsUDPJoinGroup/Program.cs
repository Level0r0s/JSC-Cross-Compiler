using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace FormsUDPJoinGroup
{
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
#if DEBUG
            //if (System.Diagnostics.Debugger.IsAttached)
            {
                ScriptCoreLib.Desktop.Forms.Extensions.DesktopFormsExtensions.Launch(
                    () => new ApplicationControl()
                );
                return;
            }
#endif
            global::jsc.AndroidLauncher.Launch(
                 typeof(FormsUDPJoinGroup.Activities.ApplicationActivity)
            );
        }
    }
}

// { Message = Could not load type 'android.text.NoCopySpan' from assembly 'ScriptCoreLibAndroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. }

// run before rebuild?

//asset merge: FormsUDPJoinGroup.ApplicationControl.resources
//1ad4:02:01:1e RewriteToAssembly error: System.IO.FileNotFoundException: Could not find file 'Y:/FormsUDPJoinGroup.AssetsLibrary.dll'.
//File name: 'Y:/FormsUDPJoinGroup.AssetsLibrary.dll'
//   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
//   at System.IO.File.InternalCopy(String sourceFileName, String destFileName, Boolean overwrite, Boolean checkHost)
//   at System.IO.File.Copy(String sourceFileName, String destFileName)
//   at jsc.meta.Commands.Rewrite.RewriteToAssembly.<InternalInvoke>b__3a3(AssemblyMergeInstruction k) in x:\jsc.internal.git\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.cs:line 681

