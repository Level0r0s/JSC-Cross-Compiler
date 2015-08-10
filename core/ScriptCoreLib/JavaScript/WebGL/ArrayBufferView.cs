using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebGL
{
    [Script(HasNoPrototype = true, ExternalTarget = "ArrayBufferView")]
    public class ArrayBufferView
    {
        // X:\jsc.svn\examples\javascript\chrome\apps\MulticastListenExperiment\MulticastListenExperiment\Application.cs
        public readonly ArrayBuffer buffer;



        public static implicit operator ArrayBufferView(byte[] bytes)
        {
            // Uint8ClampedArray : ArrayBufferView

            // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\Application.cs

            return new Uint8ClampedArray(bytes);
        }
    }
}
