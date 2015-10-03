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




        // https://msdn.microsoft.com/en-us/library/hh772360(v=vs.85).aspx
        //  requires Windows 8.
        //Starting with Internet Explorer 11, this property returns a value of 0.5 for active contact (such as mouse button push) and 0 otherwise on hardware that does not support pressure.

        // X:\opensource\unmonitored\PressureTest\PressureTest\PressureTest.cpp
        // until chrome apps can do pen pressure, we should go native?

        // can we have a UDP stream of pen pressure for VR. as a chrome app in the future, native for xt for now...
        // should we provide PPAPI plugin for chrome?
        // http://stackoverflow.com/questions/2648512/pen-pressure-in-flash
        // http://www.wacomeng.com/web/index.html
        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/events/TouchEvent.html#pressure
        public float pressure;

        // xt not supporint it?
        // diagnostics tell it should be there
        // https://web.archive.org/web/20150316135525/http://www.wacomeng.com/windows/index.html
        // X:\opensource\unmonitored\WintabDN\FormTestApp\TestForm.cs
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPLineTo\Application.cs

        // xt always reports orAltitude 900?

        // "X:\opensource\unmonitored\TiltTest\TILTTEST.C"
        public long tiltX;
        public long tiltY;
        public string pointerType;
        public bool isPrimary;

    }
}
