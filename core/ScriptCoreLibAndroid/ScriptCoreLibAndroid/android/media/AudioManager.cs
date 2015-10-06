using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.media
{
    // http://developer.android.com/reference/android/media/AudioManager.html

    [Script(IsNative = true)]
    public class AudioManager
    {
        public string getProperty(string key) { return null; }
        public static readonly string PROPERTY_OUTPUT_SAMPLE_RATE;
        public static readonly string PROPERTY_OUTPUT_FRAMES_PER_BUFFER;

        public static readonly int STREAM_MUSIC;

        // Error	40	A constant value is expected	Z:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosHUD\VrActivity.cs	516	22	OVROculus360PhotosHUD

        public const int AUDIOFOCUS_GAIN = 1;
        public const int AUDIOFOCUS_LOSS = -1;
        public const int AUDIOFOCUS_LOSS_TRANSIENT = -2;
        public const int AUDIOFOCUS_LOSS_TRANSIENT_CAN_DUCK = -3;

        public static readonly int AUDIOFOCUS_REQUEST_FAILED;
        public static readonly int AUDIOFOCUS_REQUEST_GRANTED;


        public void adjustStreamVolume(int streamType, int direction, int flags) { }

        public int abandonAudioFocus(AudioManager.OnAudioFocusChangeListener l) {
            throw null;
        }

        public int requestAudioFocus(AudioManager.OnAudioFocusChangeListener l, int streamType, int durationHint)
        {
            throw null;
        }

        [Script(IsNative = true)]
        public interface OnAudioFocusChangeListener
        {
            void onAudioFocusChange(int focusChange);
        }
    }
}
