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
using TestPointerEvent;
using TestPointerEvent.Design;
using TestPointerEvent.HTML.Pages;

namespace TestPointerEvent
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
            // https://developers.google.com/web/fundamentals/input/touch/touchevents/

            // https://googlesamples.github.io/web-fundamentals/samples/input/touch/touch-demo-1.html

            // https://code.google.com/p/chromium/issues/detail?id=516050
            // http://stackoverflow.com/questions/17886073/do-chrome-and-firefox-support-pointer-event-pressure-tilt-touch-etc-any-so
            // https://mobiforge.com/design-development/html5-pointer-events-api-combining-touch-mouse-and-pen

            // http://arstechnica.com/information-technology/2015/03/chromium-team-reverses-course-will-adopt-ies-merged-mouse-touch-apis/
            // http://www.infoq.com/news/2015/04/google-pointer-events

            // http://www.theverge.com/2015/3/25/8291893/google-chrome-pointer-events-support
            // http://www.zdnet.com/article/google-will-implement-pointer-events-api-in-chrome-after-all/

            // http://www.w3.org/TR/pointerevents/

            // http://question.ikende.com/question/32303938343332303831

            // https://www.chromestatus.com/features/4504699138998272

            // https://msdn.microsoft.com/en-us/library/hh771911(v=vs.85).aspx

            var xPointerEvent = (Native.window as dynamic).PointerEvent;

            // --enable-blink-features=PointerEvent
            new IHTMLPre { new { xPointerEvent } }.AttachToDocument();

            //  dom.w3c_pointer_events.enabled
            //  dom.w3c_touch_events.enabled
            // https://hacks.mozilla.org/2015/08/pointer-events-now-in-firefox-nightly/
            new IHTMLPre { "firefox nightly seems to do pen tilt?" }.AttachToDocument();
            // works with ie too.
            // yet oly if we had tilt and pressure
            // does xt even have em natively?

            var onpointerover = new IHTMLPre { "onpointerover ?" }.AttachToDocument();
            var onpointerdown = new IHTMLPre { "onpointerdown ?" }.AttachToDocument();
            var onpointermove = new IHTMLPre { "onpointermove ?" }.AttachToDocument();

            var c = new CanvasRenderingContext2D(800, 200);

            c.canvas.style.border = "1px solid blue";
            c.canvas.AttachToDocument();
            c.canvas.style.SetLocation(0, 0);

            Native.document.body.style.marginTop = "200px";

            c.beginPath();
            c.moveTo(0, 0);

            // whatif xt does not have tilt data available?
            // X:\opensource\unmonitored\WintabDN\FormTestApp\TestForm.cs

            c.canvas.onpointerover += e =>
            {
                // got a stylus dell xt?
                // make sure ip is set to dhcp to get into wifi
                // make sure onenote still works...

                e.stopPropagation();
                e.preventDefault();

                onpointerover.innerText = "onpointerover " + new { e.pointerType, e.tiltX, e.tiltY, e.pressure, e.movementX };

            };

            c.canvas.onpointerdown += e =>
            {
                // got a stylus dell xt?
                // make sure ip is set to dhcp to get into wifi
                // make sure onenote still works...

                e.stopPropagation();
                e.preventDefault();

                onpointerdown.innerText = "onpointerdown " + new { e.pointerType, e.tiltX, e.tiltY, e.pressure, e.movementX };

            };

            //Native.document.body.onpointermove += e =>
            c.canvas.onpointermove += e =>
            {
                // got a stylus dell xt?
                // make sure ip is set to dhcp to get into wifi
                // make sure onenote still works...
                e.stopPropagation();
                e.preventDefault();


                onpointermove.innerText = "onpointermove " + new { e.pointerType, e.tiltX, e.tiltY, e.pressure, e.movementX };


                //Native.document.body.ScrollToBottom();


                if (e.pressure > 0)
                    c.strokeStyle = "red";
                else
                    c.strokeStyle = "blue";
                c.lineTo(e.CursorX, e.CursorY);
                //c.lineTo(e.OffsetX, e.OffsetY);
                //c.lineTo(e.movementX, e.movementY);
                c.stroke();
                // https://web.archive.org/web/20150423093435/http://www.n-trig.com/wintab-compatible-applications/
                // https://web.archive.org/web/20150318140957/http://www.n-trig.com/wintab-2/
                // https://web.archive.org/web/20150316135525/http://www.wacomeng.com/windows/index.html


            };

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151001

            // https://msdn.microsoft.com/en-us/library/windows/hardware/jj151564(v=vs.85).aspx

        }

    }
}
