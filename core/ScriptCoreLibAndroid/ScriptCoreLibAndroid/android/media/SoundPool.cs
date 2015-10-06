using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;
using android.content.res;

namespace android.media
{
    // http://developer.android.com/reference/android/media/SoundPool.html
    [Script(IsNative = true)]
    public class SoundPool
    {
        public void release (){}

        public int play(int soundID, float leftVolume, float rightVolume, int priority, int loop, float rate)
        {
            throw null;
        }



        public int load(String path, int priority) { throw null; }
        public int load(AssetFileDescriptor afd, int priority)
        {
            throw null;
        }
        public SoundPool(int maxStreams, int streamType, int srcQuality)
        { }
    }
}
