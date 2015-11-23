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
using TestIEFieldToService;
using TestIEFieldToService.Design;
using TestIEFieldToService.HTML.Pages;

namespace TestIEFieldToService
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // resume with
        // Z:\jsc.svn\examples\javascript\ubuntu\UbuntuMIDExperiment\Application.cs

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new IHTMLButton { "Invoke" }.AttachToDocument().onclick +=
                async delegate
                {
                    //base.foo = "newfoo";

                    //  __XText get { value =  }

                    base.bugcheck = new ScriptCoreLib.Shared.VerifiableString { value = "bugcheck" };

                    // 5799ms AddParameter { key = _06000001_field_bugcheck, value = <_02000012><_0400001e></_0400001e></_02000012> }

                    //                    view-source:54977 2258ms WebServiceForJavascript { Name = Invoke }
                    //2015-11-23 16:34:37.633 view-source:54977 2258ms call InternalWebMethodRequest.AddParameter { Name = foo }
                    //2015-11-23 16:34:37.635 view-source:54977 2268ms AddParameter { key = _06000001_field_foo, value = bmV3Zm9v }
                    //2015-11-23 16:34:37.636 view-source:54977 2268ms call InternalWebMethodRequest.AddParameter { Name = bugcheck }
                    //2015-11-23 16:34:37.641 view-source:54977 2268ms AddParameter { key = _06000001_field_bugcheck, value = <_02000012><_0400001e>YnVnY2hlY2s=</_0400001e></_02000012> }

                    //2015-11-23 16:21:15.156 view-source:54977 1ms NewInstanceConstructor restore fields..
                    //2015-11-23 16:21:17.644 view-source:54977 2483ms enter WebClient.UploadValuesTaskAsync! { address = /xml/Invoke }
                    //2015-11-23 16:21:17.645 view-source:54977 2483ms enter WebClient.ToFormDataString { Count = 4 }
                    //2015-11-23 16:21:17.646 view-source:54977 2483ms WebClient.ToFormDataString { item = _06000001_field_foo }
                    //2015-11-23 16:21:17.646 view-source:54977 2483ms WebClient.ToFormDataString { xdata = bmV3Zm9v }
                    //2015-11-23 16:21:17.646 view-source:54977 2483ms WebClient.ToFormDataString { item = _06000001_field_bugcheck }
                    //2015-11-23 16:21:17.646 view-source:54977 2483ms WebClient.ToFormDataString { xdata = &lt;_02000012&gt;&lt;_0400001e&gt;YnVnY2hlY2s=&lt;/_0400001e&gt;&lt;/_02000012&gt; }

                    await base.Invoke();
                };
        }

    }
}
