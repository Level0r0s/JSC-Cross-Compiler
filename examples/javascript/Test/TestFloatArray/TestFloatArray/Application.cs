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
using TestFloatArray;
using TestFloatArray.Design;
using TestFloatArray.HTML.Pages;

namespace TestFloatArray
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            {
                byte[] bytes =
                {
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1,
                };

                new IHTMLPre { new { bytes } }.AttachToDocument();
                new IHTMLPre { new { bytes.Length } }.AttachToDocument();
                new IHTMLPre { bytes.GetType() }.AttachToDocument();
            }

            {
                var bytes = new byte[16];

                new IHTMLPre { new { bytes } }.AttachToDocument();
                new IHTMLPre { new { bytes.Length } }.AttachToDocument();
                new IHTMLPre { bytes.GetType() }.AttachToDocument();
            }

            {
                float[] floats =
                {
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1,
                };

                new IHTMLPre { new { floats } }.AttachToDocument();
                new IHTMLPre { new { floats.Length } }.AttachToDocument();
                new IHTMLPre { floats.GetType() }.AttachToDocument();
            }


            {
                var floats = new float[16];

                new IHTMLPre { new { floats } }.AttachToDocument();
                new IHTMLPre { new { floats.Length } }.AttachToDocument();
                new IHTMLPre { floats.GetType() }.AttachToDocument();
            }

        }

    }
}

//{{ bytes = 1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1 }}
//? function Uint8ClampedArray() { [native code] }
//{{ bytes = 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }}
//? function Uint8ClampedArray() { [native code] }
//{{ floats = 1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1 }}
//? function Array() { [native code] }
//{{ floats = 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }}
//? function Float32Array() { [native code] }

//    {{ bytes = [object Uint8ClampedArray] }}
//? function Uint8ClampedArray() { [native code] }
//{{ bytes = [object Uint8ClampedArray] }}
//? function Uint8ClampedArray() { [native code] }
//{{ floats = [object Float32Array] }}
//? function Float32Array() { [native code] }
//{{ floats = [object Float32Array] }}
//? function Float32Array() { [native code] }

//    {{ bytes = 1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1 }}
//? function Uint8ClampedArray() { [native code] }
//{{ bytes = 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }}
//? function Uint8ClampedArray() { [native code] }
//{{ floats = 1,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1 }}
//? function Float32Array() { [native code] }
//{{ floats = 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }}
//? function Float32Array() { [native code] }