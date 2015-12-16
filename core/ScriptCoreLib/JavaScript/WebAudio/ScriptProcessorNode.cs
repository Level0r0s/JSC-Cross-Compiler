using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-GainNode
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webaudio/ScriptProcessorNode.idl?sortby=file

    [Script(HasNoPrototype = true, ExternalTarget = "ScriptProcessorNode")]
    public class ScriptProcessorNode : AudioNode
    {
        // Z:\jsc.svn\examples\javascript\audio\StereoChannelVisualization\Application.cs

        public IFunction onaudioprocess;

    }


}
