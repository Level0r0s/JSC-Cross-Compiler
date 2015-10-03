using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTrigStylusToUDP
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151003/udppenpressure

            // Severity	Code	Description	Project	File	Line
            //Error CS0017  Program has more than one entry point defined. Compile with / main to specify the type that contains the entry point.NTrigStylusToUDP Z:\jsc.svn\examples\rewrite\NTrigStylusToUDP\NTrigStylusToUDP\Program.cs    11

            // run it on xt!
            new FormTestApp.TestForm().ShowDialog();

            //            ---------------------------

            //            ---------------------------
            //            FAILED IsWintabAvailable: System.DllNotFoundException: Unable to load DLL 'Wintab32.dll': The specified module could not be found. (Exception from HRESULT: 0x8007007E)

            //   at WintabDN.CWintabFuncs.WTInfoA(UInt32 wCategory_I, UInt32 nIndex_I, IntPtr lpOutput_O)

            //   at WintabDN.CWintabInfo.IsWintabAvailable() in Z:\jsc.svn\examples\rewrite\NTrigStylusToUDP\NTrigStylusToUDP\opensource\WintabDN\CWintabInfo.cs:line 47
            //-------------------------- -
            //OK
            //-------------------------- -

        }
    }
}
