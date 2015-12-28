using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovingMusic.Library
{
    class Choreographer
    {
        // X:\opensource\github\moving-music\js\choreographer.js

        // 260 lines to review
        // need trackmanager?

        public readonly TrackManager manager = new TrackManager();
        public readonly DwellDetector dwellDetector = new DwellDetector();

        // can we get rid of the mode?
        public Choreographer(int mode = 0)
        {
            //          var params = opt_params || {};
            //var set = params.set || 'speech';
            //var mode = params.mode || Choreographer.Modes.CLUSTERED;
            //mode = parseInt(mode);


            //this.mode_ = -1;
            //this.callbacks_ = {};

            //if (set == 'speech') {
            //  this.initVocal();
            //} else if (set == 'phoenix') {
            //  this.initPhoenix();
            //} else if (set == 'jazz') {
            //  this.initJazz();
            //}

            this.setMode(mode);
        }

        public void setMode(int mode, int delay = 0)
        {

            //var index = 0;
            //// Go through each track, setting the trajectory to the appropriate mode
            //for (var trackId in this.manager.tracks) {
            //  var track = this.manager.tracks[trackId];
            //  var trajectory = this.createTrajectoryForMode_(mode, index);
            //  track.changeTrajectory(trajectory, delay);
            //  index += 1;
            //}

            //// Notify others that the mode changed.
            //this.fire_(this.callbacks_.modechanged, this.mode_);
        }

    }
}
