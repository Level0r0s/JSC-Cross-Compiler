using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/fileapi/Blob.h
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/fileapi/Blob.cpp

    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/Blob.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/fileapi/Blob.idl

    // https://github.com/bridgedotnet/Bridge/blob/master/Html5/File/Blob.cs

    [Script(HasNoPrototype = true, ExternalTarget = "Blob")]
    public class Blob
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\Application.cs

        public readonly ulong size;

        [Obsolete("chrome debugger tells, close method is not there. yikes.", error: true)]
        public void close() { }

        public Blob()
        {

        }


        // http://stackoverflow.com/questions/19327749/javascript-blob-filename-without-link

        public Blob(string[] e)
        {

        }

        // sequence<(ArrayBuffer or ArrayBufferView or Blob or DOMString)> blobParts

        public Blob(ArrayBuffer[] blobParts, object options) { }
        public Blob(ArrayBufferView[] blobParts, object options) { }
        public Blob(Blob[] blobParts, object options) { }
        public Blob(string[] blobParts, object options)
        {

        }

        public static implicit operator Blob(byte[] bytes)
        {
            // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\Application.cs

            var blob = new Blob(
                blobParts: new ArrayBufferView[] { bytes },
                options: new { type = "application/octet-stream" }
            );

            return blob;
        }
    }

    [Script]
    public static class BlobExtensions
    {
        // X:\jsc.svn\examples\javascript\Test\TestRedirectWebWorker\TestRedirectWebWorker\Application.cs

        [Obsolete("jsc faults?")]
        public static string ToObjectURL(this Blob e)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\ScriptDynamicSourceBuilder\ScriptDynamicSourceBuilder\Application.cs

            //Console.WriteLine("ToObjectURL");

            // is jsc trying to inline without the line above?
            return URL.createObjectURL(e);
        }
    }
}
