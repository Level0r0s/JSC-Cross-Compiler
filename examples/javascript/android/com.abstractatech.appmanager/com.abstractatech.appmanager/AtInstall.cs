using android.content;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;


namespace com.abstractatech.appmanager
{
    // defined at?

    [ScriptCoreLib.Android.Manifest.ApplicationIntentFilter(Action = Intent.ACTION_PACKAGE_REPLACED)]
    [ScriptCoreLib.Android.Manifest.ApplicationIntentFilter(Action = Intent.ACTION_PACKAGE_INSTALL)]
    [ScriptCoreLib.Android.Manifest.ApplicationIntentFilterData(scheme = "package")]
    public class AtInstall : BroadcastReceiver
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150514
        // http://stackoverflow.com/questions/8680874/created-broadcastreceiver-which-displays-application-name-and-version-number-on

        public static List<string> History = new List<string>();


        public override void onReceive(Context arg0, Intent arg1)
        {
            var context = ThreadLocalContextReference.CurrentContext;

            var uri = arg1.getData();

            var packageName = uri.getSchemeSpecificPart();

            Console.WriteLine("AtInstall " + new { packageName });
            // I/System.Console( 3900): AtInstall { arg1 = Intent { act=android.intent.action.PACKAGE_REPLACED dat=package:NASDAQSNA.Activities flg=0x8000010 cmp=ReinstallNotification.Activities/.AtInstall (has extras) }, packageName = NASDAQSNA.Activities }

            History.Add(packageName);

        }
    }
}
