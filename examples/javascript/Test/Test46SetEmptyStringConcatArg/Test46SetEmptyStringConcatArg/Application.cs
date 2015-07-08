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
using Test46SetEmptyStringConcatArg;
using Test46SetEmptyStringConcatArg.Design;
using Test46SetEmptyStringConcatArg.HTML.Pages;

namespace Test46SetEmptyStringConcatArg
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
            AllowUserToResizeColumns = true;
        }

        string InternalAllowUserToResizeColumns;
        public bool AllowUserToResizeColumns
        {
            set
            {
                // its fine as we do a merge rewrite..?
            
                // X:\jsc.svn\examples\javascript\Test\Test46SetEmptyStringConcatArg\Test46SetEmptyStringConcatArg\Application.cs
                // X:\jsc.svn\examples\javascript\WebGL\collada\WebGLRah66Comanche\WebGLRah66Comanche\Application.cs
                // jsc wont like the way toslyn does it?
                InternalAllowUserToResizeColumns = "" + value;

            }
        }
    }
}
