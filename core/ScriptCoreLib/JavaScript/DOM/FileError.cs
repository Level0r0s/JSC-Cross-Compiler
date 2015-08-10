using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // https://developer.mozilla.org/en-US/docs/Web/API/FileError

    [Script(HasNoPrototype = true, ExternalTarget = "FileError")]
    public class FileError : IError
    {
        public ushort code;

    }
}
