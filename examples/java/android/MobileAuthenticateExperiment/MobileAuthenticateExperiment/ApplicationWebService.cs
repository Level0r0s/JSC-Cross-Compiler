using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MobileAuthenticateExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService
    {
        // "x:\util\android-sdk-windows\platform-tools\adb.exe"  tcpip 5555
        // restarting in TCP mode port: 5555

        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // connected to 192.168.1.126:5555

    }
}

//enter AndroidLauncher { AndroidPayload = Y:\staging\MobileAuthenticateExperiment.Applicati
//192.168.1.126:5555
//enter InstallToDevice { AndroidPayload = Y:\staging\MobileAuthenticateExperiment.Applicati
//Y:\staging\MobileAuthenticateExperiment.ApplicationWebService\staging.apk\staging\apk\bin\
//1628520 bytes
//x:\util\android-sdk-windows\platform-tools\adb.exe
// -s 192.168.1.126:5555 install -r "Y:\staging\MobileAuthenticateExperiment.ApplicationWebS
//        pkg: /data/local/tmp/MobileAuthenticateExperiment.Activities-debug.apk
//Success
//480 KB/s (1628520 bytes in 3.307s)

//all done!


