using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/events/PointerEvent.idl
    // https://dxr.mozilla.org/mozilla-central/source/dom/webidl/PointerEvent.webidl?offset=0
    // https://dev.modern.ie/platform/documentation/apireference/interfaces/pointerevent/
    // https://msdn.microsoft.com/en-us/library/windows/apps/hh441233.aspx


    [Script(HasNoPrototype = true, ExternalTarget = "PointerEvent")]
    public class PointerEvent : IEvent
    {
        // http://www.w3.org/TR/pointerevents/

        // can we have a chrome app stream stlys pressure to vr via udp?
        // tested by ?


        // https://code.google.com/p/chromium/issues/detail?id=476563
        // https://code.google.com/p/chromium/issues/detail?id=136119

        public long pointerId;
        public double width;
        public double height;
        public float pressure;
        public long tiltX;
        public long tiltY;
        public string pointerType;
        public bool isPrimary;

    }
}
