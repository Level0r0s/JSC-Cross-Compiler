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

namespace DropLESTToDisplay
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {

        static ApplicationWebService()
        { 
            //DropLESTToDisplay.NamedKeyPairs.Key1PublicKey.PublicParameters
        }


        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

    }
}

//Error	25	Unable to open module file 'C:\Users\Administrator\AppData\Local\Temp\3\.NETFramework,Version=v4.6.AssemblyAttributes.vb': System Error &H80070002&	C:\Users\Administrator\AppData\Local\Temp\3\.NETFramework,Version=v4.6.AssemblyAttributes.vb	1	1	LEST97
//Error	27	The type or namespace name 'Key1PublicKey' does not exist in the namespace 'DropLESTToDisplay.NamedKeyPairs' (are you missing an assembly reference?)	Z:\jsc.svn\examples\javascript\io\DropLESTToDisplay\DropLESTToDisplay\Program.cs	13	53	DropLESTToDisplay
