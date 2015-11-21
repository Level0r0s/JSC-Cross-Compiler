using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    [Script(HasNoPrototype = true, ExternalTarget = "DragEvent")]
    public class DragEvent : IEvent
    {
        // "X:\jsc.svn\examples\javascript\Test\TestPackageAsApplication\TestPackageAsApplication.sln"
        // downloadURL no longer works?


        // Z:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Control\Control.AllowDrop.cs
        public readonly DataTransfer dataTransfer;

        public DragEvent(string type) { }


        public string text
        {
            [method: Script(DefineAsStatic = true)]
            get
            {
                // Z:\jsc.svn\examples\javascript\Test\TestDropText\Application.cs
                return this.dataTransfer.getData("Text");
            }
        }
    }
}
