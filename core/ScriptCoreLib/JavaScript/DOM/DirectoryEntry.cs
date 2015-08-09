using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/FileEntry.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/DirectoryEntry.idl

    // .WebFileSystem
    // .IOFileSystem ?
    [Script(HasNoPrototype = true, ExternalTarget = "")]
    public class DirectoryEntry : Entry
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\Application.cs

        //void getFile([TreatUndefinedAs=NullString] DOMString? path, optional FileSystemFlags options, optional EntryCallback successCallback, optional ErrorCallback errorCallback);
        public void getFile(string path, object options, Action<FileEntry> successCallback)
        {
            // can chrome do vhd?
        }
    }
}
