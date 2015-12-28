using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingMusic.Library
{
    class AudioRenderer
    {
        // X:\opensource\github\moving-music\js\audio-renderer.js

        // 300 lines to review

        //audio.setManager(manager);
        //audio.on('ready', onAudioLoaded);


        // https://code.google.com/p/chromium/issues/detail?id=419446

        public AudioRenderer()
        {
            // Whether we should stream the tracks via MediaElements, or load them
            // directly as audio buffers.
            // TODO(smus): Once crbug.com/419446 is fixed, switch to streaming.
            //          this.isStreaming = false;

            //          this.isMuted = false;

            //          // Various audio nodes keyed on UUID (so we can update them later).
            //          this.panners = {};
            //          this.gains = {};
            //          this.buffers = {};
            //          // Tracking buffer loading progress.
            //          this.progress = {};
            //          // For streaming.
            //          this.audioTags = {};
            //          this.ready = {};
            //          this.analysers = {};

            //          // Source nodes.
            //          this.sources = {};

            //          this.times = new Uint8Array(2048);

            //          // Callbacks.
            //          this.callbacks = {};


            //AudioRenderer.prototype.init = function() {
            //  // Start by preparing the audio graph.
            //  // TODO: Fix up for prefixing.
            //  window.AudioContext = window.AudioContext||window.webkitAudioContext;
            //  this.context = new AudioContext();

            //  // For calculating isTrackWithinFov_:
            //  this.cameraDirection = new THREE.Vector3();
            //  this.trackPosition = new THREE.Vector3();

            //  // Pipe the mix through a convolver node for a room effect.
            //  var convolver = this.context.createConvolver();
            //  //Util.loadTrackSrc(this.context, 'snd/forest_impulse_response.wav', function(buffer) {
            //  Util.loadTrackSrc(this.context, forest_impulse_response, function(buffer) {
            //    convolver.buffer = buffer;
            //  });
            //  convolver.connect(this.context.destination);

            //  // Setup the mix.
            //  var mix = this.context.createGain();
            //  mix.connect(convolver);


            //  this.convolver = convolver;
            //  this.mix = mix;
            //};


            //AudioRenderer.prototype.setManager = function(manager) {
            //  this.manager = manager;
            //  // Load audio tracks for each of the tracks in the manager.
            //  for (var id in manager.tracks) {
            //    if (this.isStreaming) {
            //      this.streamTrack_(id);
            //    } else {
            //      this.loadTrack_(id);
            //    }
            //  }
            //};

        }
    }
}
