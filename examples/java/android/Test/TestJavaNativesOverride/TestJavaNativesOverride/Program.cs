using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace TestJavaNativesOverride
{
    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201506/20150602
    [System.Reflection.Obfuscation(Feature = "invalidmerge")]
    class Program
    {
        [STAThread]
        public static void Main(string[] e)
        {
            global::jsc.AndroidLauncher.Launch(
                 typeof(TestJavaNativesOverride.Activities.ApplicationActivity)
            );
        }
    }
}
