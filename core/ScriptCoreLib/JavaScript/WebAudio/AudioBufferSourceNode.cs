using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-GainNode
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webaudio/AudioBufferSourceNode.idl?sortby=file

    [Script(HasNoPrototype = true, ExternalTarget = "AudioBufferSourceNode")]
    public class AudioBufferSourceNode : AudioSourceNode
    {
        // Z:\jsc.svn\examples\javascript\audio\StereoChannelVisualization\Application.cs

        public AudioBuffer buffer;

        public bool loop;

        public void start(double when)
        { }

    }


}
