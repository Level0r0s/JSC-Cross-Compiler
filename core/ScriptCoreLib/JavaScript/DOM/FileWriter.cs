using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://mxr.mozilla.org/mozilla-central/source/dom/webidl/FileReader.webidl
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/fileapi/FileReader.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/FileWriter.idl

    [Script(HasNoPrototype = true, ExternalTarget = "FileWriter")]
    public class FileWriter : IEventTarget
    {
        public long position;

        public void truncate(long size) { }

         public FileError error;

        //attribute EventHandler onerror;
        public IFunction onerror;

        // event? WriteAsync?
        public IFunction onwriteend;

        public void write(Blob data)
        { }
    }
}
