﻿using System;
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
    public class FileEntry : Entry
    {
        // 35	    void file(FileCallback successCallback, optional ErrorCallback errorCallback);
        // hide it?
        public void file(IFunction successCallback)
        {
        }

        // Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\360\x360x83\Application.cs

        // jsc nowadays unwraps CLR delegates to IFunctions
        public void file(Action<File> successCallback)
        {
            // used by?

            // tested by?

        }


         //void createWriter(FileWriterCallback successCallback, optional ErrorCallback errorCallback);

        public void createWriter(Action<FileWriter> successCallback)
        { 
        }
    }
}
