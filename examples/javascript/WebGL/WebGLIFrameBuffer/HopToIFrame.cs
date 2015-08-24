using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace WebGLIFrameBuffer
{
    public struct HopToIFrame : System.Runtime.CompilerServices.INotifyCompletion
    {
        //public chrome.AppWindow window;
        public IHTMLIFrame that;




        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150822/hoptochromeapp

        public static Action<HopToIFrame, Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation)
        {

            VirtualOnCompleted(this, continuation);
        }

        //Error   CS1929	'HopToChromeExtension' does not contain a definition for 'GetAwaiter' and the best extension method overload 'IXMLHttpRequestAsyncExtensions.GetAwaiter(IXMLHttpRequest)' requires a receiver of type 'IXMLHttpRequest'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	404	IntelliSense

        public HopToIFrame GetAwaiter() { return this; }



        //Error   CS0117	'HopToChromeExtension' does not contain a definition for 'IsCompleted'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	409	IntelliSense
        public bool IsCompleted { get { return false; } }


        //Error CS0117	'HopToChromeExtension' does not contain a definition for 'GetResult'	ChromeHybridCapture Z:\jsc.svn\examples\javascript\chrome\hybrid\ChromeHybridCapture\ChromeHybridCapture\Application.cs	414	IntelliSense
        public void GetResult() { }

    }
}
