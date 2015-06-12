using android.app;
using android.content;
using android.content.res;
using android.graphics;
using android.media;
using android.util;
using android.view;
using ScriptCoreLib;
//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;

// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150612/ovroculus360photoshud

namespace com.oculus.vrappframework
{
    // http://developer.android.com/reference/android/app/ActivityGroup.html

    public abstract class VrActivity : ActivityGroup,

        AudioManager.OnAudioFocusChangeListener
    {
        const string TAG = "xVrActivity";

        // X:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360Photos\com\oculus\vrappframework\VrActivity.java


        // any need for getAppPtr/ setAppPtr?
        public long appPtr;

        #region extern "C"

        [Script(IsPInvoke = true)]
        public static void nativeOnCreate(VrActivity act) { }
        [Script(IsPInvoke = true)]
        public static void nativeOnResume(long appPtr) { }
        [Script(IsPInvoke = true)]
        public static void nativeOnPause(long appPtr) { }
        [Script(IsPInvoke = true)]
        public static void nativeOnDestroy(long appPtr) { }
        [Script(IsPInvoke = true)]
        public static void nativeNewIntent(long appPtr, string fromPackageName, string command, string uriString) { }
        [Script(IsPInvoke = true)]
        public static void nativeSurfaceCreated(long appPtr, Surface s) { }
        [Script(IsPInvoke = true)]
        public static void nativeSurfaceChanged(long appPtr, Surface s) { }
        [Script(IsPInvoke = true)]
        public static void nativeSurfaceDestroyed(long appPtr) { }
        [Script(IsPInvoke = true)]
        public static void nativeKeyEvent(long appPtr, int keyNum, bool down, int repeatCount) { }
        [Script(IsPInvoke = true)]
        public static void nativeJoypadAxis(long appPtr, float lx, float ly, float rx, float ry) { }
        [Script(IsPInvoke = true)]
        public static void nativeTouch(long appPtr, int action, float x, float y) { }
        [Script(IsPInvoke = true)]
        public static SurfaceTexture nativeGetPopupSurfaceTexture(long appPtr) { return null; }
        [Script(IsPInvoke = true)]
        public static void nativePopup(long appPtr, int width, int height, float seconds) { }

        #endregion


        // While there is a valid surface holder, onDestroy can destroy the surface when
        // surfaceDestroyed is not called before onDestroy.
        private SurfaceHolder mSurfaceHolder;



        #region Intent
        // These variables duplicated in VrLib.java!
        public static string INTENT_KEY_CMD = "intent_cmd";
        public static string INTENT_KEY_FROM_PKG = "intent_pkg";

        public static string getCommandStringFromIntent(Intent intent)
        {
            string commandStr = "";
            if (intent != null)
            {
                commandStr = intent.getStringExtra(INTENT_KEY_CMD);
                if (commandStr == null)
                {
                    commandStr = "";
                }
            }
            return commandStr;
        }

        public static string getPackageStringFromIntent(Intent intent)
        {
            string packageStr = "";
            if (intent != null)
            {
                packageStr = intent.getStringExtra(INTENT_KEY_FROM_PKG);
                if (packageStr == null)
                {
                    packageStr = "";
                }
            }
            return packageStr;
        }

        public static string getUriStringFromIntent(Intent intent)
        {
            string uriString = "";
            if (intent != null)
            {
                android.net.Uri uri = intent.getData();
                if (uri != null)
                {
                    uriString = uri.ToString();
                    if (uriString == null)
                    {
                        uriString = "";
                    }
                }
            }
            return uriString;
        }
        #endregion

        public class xCallback : SurfaceHolder_Callback
        {

            public System.Action<SurfaceHolder, int, int, int> onsurfaceChanged;
            public void surfaceChanged(SurfaceHolder arg0, int format, int width, int height)
            {
                onsurfaceChanged(arg0, format, width, height);
            }

            public System.Action<SurfaceHolder> onsurfaceCreated;
            public void surfaceCreated(SurfaceHolder value)
            {
                onsurfaceCreated(value);
            }

            public System.Action<SurfaceHolder> onsurfaceDestroyed;
            public void surfaceDestroyed(SurfaceHolder value)
            {
                onsurfaceDestroyed(value);
            }
        }




        #region onAudioFocusChange
        // For trivial feedback sound effects
        private SoundPool soundPool;
        private java.util.List<int> soundPoolSoundIds;
        private java.util.List<string> soundPoolSoundNames;


        public void playSoundPoolSound(string name)
        {
            for (int i = 0; i < soundPoolSoundNames.size(); i++)
            {
                if (soundPoolSoundNames.get(i).Equals(name))
                {
                    soundPool.play(
                        (int)soundPoolSoundIds.get(i), 1.0f, 1.0f, 1, 0, 1);
                    return;
                }
            }

            Log.d(TAG, "playSoundPoolSound: loading " + name);

            // check first if this is a raw resource
            int soundId = 0;
            if (name.IndexOf("res/raw/") == 0)
            {
                string resourceName = name.Substring(4);
                int id = getResources().getIdentifier(resourceName, "raw", getPackageName());
                if (id == 0)
                {
                    Log.e(TAG, "No resource named " + resourceName);
                }
                else
                {
                    AssetFileDescriptor afd = getResources().openRawResourceFd(id);
                    soundId = soundPool.load(afd, 1);
                }
            }
            else
            {
                try
                {
                    AssetFileDescriptor afd = getAssets().openFd(name);
                    soundId = soundPool.load(afd, 1);
                }
                catch
                {
                    Log.e(TAG, "Couldn't open " + name);
                }
            }

            if (soundId == 0)
            {
                // Try to load the sound directly - works for absolute path - for wav files for sdcard for ex.
                soundId = soundPool.load(name, 1);
            }

            soundPoolSoundNames.add(name);
            soundPoolSoundIds.add(soundId);

            soundPool.play((int)soundPoolSoundIds.get(soundPoolSoundNames.size() - 1), 1.0f, 1.0f, 1, 0, 1);
        }

        void adjustVolume(int direction)
        {
            AudioManager audio = (AudioManager)getSystemService(Context.AUDIO_SERVICE);
            audio.adjustStreamVolume(AudioManager.STREAM_MUSIC, direction, 0);
        }

        public static void requestAudioFocus(VrActivity act)
        {

            AudioManager audioManager = (AudioManager)act.getSystemService(Context.AUDIO_SERVICE);

            // Request audio focus
            int result = audioManager.requestAudioFocus(act, AudioManager.STREAM_MUSIC,
                AudioManager.AUDIOFOCUS_GAIN);
            if (result == AudioManager.AUDIOFOCUS_REQUEST_GRANTED)
            {
                Log.d(TAG, "requestAudioFocus(): GRANTED audio focus");
            }
            else if (result == AudioManager.AUDIOFOCUS_REQUEST_FAILED)
            {
                Log.d(TAG, "requestAudioFocus(): FAILED to gain audio focus");
            }
        }

        public static void releaseAudioFocus(VrActivity act)
        {
            AudioManager audioManager = (AudioManager)act.getSystemService(Context.AUDIO_SERVICE);
            audioManager.abandonAudioFocus(act);
        }

        public void onAudioFocusChange(int focusChange)
        {
            switch (focusChange)
            {
                case AudioManager.AUDIOFOCUS_GAIN:
                    // resume() if coming back from transient loss, raise stream volume if duck applied
                    Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_GAIN");
                    break;
                case AudioManager.AUDIOFOCUS_LOSS:				// focus lost permanently
                    // stop() if isPlaying
                    Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_LOSS");
                    break;
                case AudioManager.AUDIOFOCUS_LOSS_TRANSIENT:	// focus lost temporarily
                    // pause() if isPlaying
                    Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_LOSS_TRANSIENT");
                    break;
                case AudioManager.AUDIOFOCUS_LOSS_TRANSIENT_CAN_DUCK:	// focus lost temporarily
                    // lower stream volume
                    Log.d(TAG, "onAudioFocusChangedListener: AUDIOFOCUS_LOSS_TRANSIENT_CAN_DUCK");
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
