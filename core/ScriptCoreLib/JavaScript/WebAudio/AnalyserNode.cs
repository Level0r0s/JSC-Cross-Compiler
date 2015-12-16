using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://webaudio.github.io/web-audio-api/#idl-def-GainNode
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webaudio/AnalyserNode.idl?sortby=file

    [Script(HasNoPrototype = true, ExternalTarget = "AnalyserNode")]
    public class AnalyserNode : AudioNode
    {
        // Z:\jsc.svn\examples\javascript\audio\StereoChannelVisualization\Application.cs


        public double smoothingTimeConstant;
        public ulong fftSize;


        public long frequencyBinCount;

        public void getByteFrequencyData(Uint8Array array)
        { }
    }


}
