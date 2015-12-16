﻿using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebAudio
{
    // http://webaudio.github.io/web-audio-api/
    // http://dan.nea.me/audiolandscape/
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webaudio/WindowWebAudio.idl?sortby=date
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/webaudio/AudioContext.idl?sortby=file

    [Script(HasNoPrototype = true, ExternalTarget = "AudioContext")]
    public class AudioContext
    {
        // Z:\jsc.svn\examples\javascript\audio\StereoChannelVisualization\Application.cs

        // https://www.shadertoy.com/view/XtSGWy

        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151201/soundcloud
        // can we stream from soundcloud?
        // https://www.shadertoy.com/view/ldcGRN

        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff560842(v=vs.85).aspx

        // http://www.w3.org/2011/audio/wiki/Basic-Examples#Looping_Sounds_Without_Gaps

        // doppler!
        // https://www.shadertoy.com/view/4sfSDX#
        // https://www.shadertoy.com/view/ldlXD2
        // https://www.shadertoy.com/view/XsfXD2
        // https://www.shadertoy.com/view/ldXXDj
        // https://www.shadertoy.com/view/ldjXzz
        // https://www.shadertoy.com/view/4sSSWz
        // https://www.shadertoy.com/view/ltX3zs
        // https://www.shadertoy.com/view/4dsXzS
        // https://www.shadertoy.com/view/XsX3DB
        // https://www.shadertoy.com/view/ldfSW2
        // https://www.shadertoy.com/view/4sSSWz
        // https://www.shadertoy.com/view/ldlXWX
        // https://www.shadertoy.com/view/lssXWS

        // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyColumns\ChromeShaderToyColumns\Application.cs
        // createGain

        // https://www.shadertoy.com/view/Xds3Rr
        // http://webaudio.github.io/web-audio-api/#the-stereopannernode-interface

        // "X:\jsc.svn\market\synergy\javascript\MIDI\MIDI.sln"

        // https://msdn.microsoft.com/en-us/library/aa376846.aspx?f=255&MSPPError=-2147217396

        // http://forestmist.org/share/web-audio-api-demo/
        // http://caniuse.com/#feat=audio-api
        // http://www.w3.org/2011/audio/wiki/Basic-Examples#Looping_Sounds_Without_Gaps

        public readonly AudioDestinationNode destination;

        // https://developer.apple.com/library/iad/documentation/AudioVideo/Conceptual/Using_HTML5_Audio_Video/PlayingandSynthesizingSounds/PlayingandSynthesizingSounds.html
        // http://typedarray.org/from-microphone-to-wav-with-getusermedia-and-web-audio/
        // http://www.sitepoint.com/using-fourier-transforms-web-audio-api/
        // http://webaudio.github.io/web-audio-api/#idl-def-AudioWorkerGlobalScope

        public AudioWorkerNode createAudioWorker(string scriptURL, uint numberOfInputChannels = 2, uint numberOfOutputChannels = 2)
        {
            return default(AudioWorkerNode);
        }

        public OscillatorNode createOscillator()
        {
            return default(OscillatorNode);
        }


        public GainNode createGain()
        {
            return default(GainNode);
        }


        public ScriptProcessorNode createScriptProcessor(long bufferSize, long numberOfInputChannels, long numberOfOutputChannels)
        {
            throw null;
        }

        public AnalyserNode createAnalyser()
        {
            throw null;
        }

        public AudioBufferSourceNode createBufferSource()
        {
            throw null;
        }

        public ChannelSplitterNode createChannelSplitter()
        {
            throw null;
        }


        public void decodeAudioData(ArrayBuffer audioData, Action<AudioBuffer> successCallback)
        {
        }


        public IPromise close()
        {

            return null;
        }
    }
}
