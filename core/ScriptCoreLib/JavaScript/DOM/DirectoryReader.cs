using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/DirectoryReader.idl

    [Script(HasNoPrototype = true, ExternalTarget = "DirectoryReader")]
    public class DirectoryReader : IEventTarget
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\Application.cs

        //void readEntries(EntriesCallback successCallback, optional ErrorCallback errorCallback);
        public void readEntries(Action<object> successCallback) { }

        // extended by
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\DOM\FileExtensions.cs
    }
}
