using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestGetHexStringToBytes;
using TestGetHexStringToBytes.Design;
using TestGetHexStringToBytes.HTML.Pages;

namespace TestGetHexStringToBytes
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Convert.ToByte(System.String, System.Int32)]
        //public static byte[] StringToByteArray(string hex)
        //{
        //    return Enumerable.Range(0, hex.Length)
        //                     .Where(x => x % 2 == 0)
        //                     .Select(x =>

        //                     byte.Parse(hex.Substring(x, 2), System.Globalization.NumberStyles.HexNumber)

        //                     //Convert.ToByte(hex.Substring(x, 2), 16)
        //                     )
        //                     .ToArray();
        //}

        public static IEnumerable<byte> StringToByteArray(string hex) =>
            from x in Enumerable.Range(0, hex.Length)
            where x % 2 == 0
            select byte.Parse(hex.Substring(x, 2), System.Globalization.NumberStyles.HexNumber);



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array

            //Error CS0452  The type 'byte' must be a reference type in order to use it as parameter 'T' in the generic type or method 'LinqExtensions.WithEach<T>(IEnumerable<T>, Action<T>)'  TestGetHexStringToBytes X:\jsc.svn\examples\javascript\Test\TestGetHexStringToBytes\TestGetHexStringToBytes\Application.cs  44
            //StringToByteArray(page.UID.innerText).WithEach(u8 => new IHTMLPre { new { u8 } }.AttachToDocument());

            foreach (var u8 in StringToByteArray(page.UID.innerText))
            {
                new IHTMLPre { new { u8 } }.AttachToDocument();
            }

            //            FFCA00000A
            //{ { u8 = 255 } }
            //            { { u8 = 202 } }
            //            { { u8 = 0 } }
            //            { { u8 = 0 } }
            //            { { u8 = 10 } }
        }

    }
}
