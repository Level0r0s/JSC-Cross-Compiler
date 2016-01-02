using android.app;
using android.content;
using android.content.pm;
using android.content.res;
using android.graphics;
using android.media;
using android.net;
using android.os;
using android.provider;
using android.util;
using android.widget;
using android.view;
using java.io;
using java.util;
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
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160102/x360videos

        // https://answers.oculus.com/questions/3824/what-is-the-ideal-triangle-count-and-draw-calls-li.html
        // https://answers.oculus.com/questions/4436/how-do-i-modify-the-clipping-planes-in-c.html

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

        protected override void onNewIntent(Intent intent)
        {
            var commandString = getCommandStringFromIntent(intent);
            var fromPackageNameString = getPackageStringFromIntent(intent);
            var uriString = getUriStringFromIntent(intent);

            Log.d(TAG, "action:" + intent.getAction());
            Log.d(TAG, "type:" + intent.getType());
            Log.d(TAG, "fromPackageName:" + fromPackageNameString);
            Log.d(TAG, "command:" + commandString);
            Log.d(TAG, "uri:" + uriString);

            nativeNewIntent(appPtr, fromPackageNameString, commandString, uriString);
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

        protected override void onCreate(android.os.Bundle savedInstanceState)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20151006
            base.onCreate(savedInstanceState);


            nativeOnCreate(this);

            //setCurrentLanguage(Locale.getDefault().getLanguage());

            // Create the SoundPool
            soundPool = new SoundPool(3 /* voices */, AudioManager.STREAM_MUSIC, 100);
            soundPoolSoundIds = new java.util.ArrayList<int>();
            soundPoolSoundNames = new java.util.ArrayList<string>();

            AudioManager audioMgr;
            audioMgr = (AudioManager)getSystemService(Context.AUDIO_SERVICE);
            var rate = audioMgr.getProperty(AudioManager.PROPERTY_OUTPUT_SAMPLE_RATE);
            var size = audioMgr.getProperty(AudioManager.PROPERTY_OUTPUT_FRAMES_PER_BUFFER);
            System.Console.WriteLine("rate = " + rate);
            System.Console.WriteLine("size = " + size);

            // Check preferences
            SharedPreferences prefs = getApplicationContext().getSharedPreferences("oculusvr", MODE_PRIVATE);
            var username = prefs.getString("username", "guest");
            System.Console.WriteLine("username = " + username);

            // Check for being launched with a particular intent
            Intent intent = getIntent();

            var commandString = getCommandStringFromIntent(intent);
            var fromPackageNameString = getPackageStringFromIntent(intent);
            var uriString = getUriStringFromIntent(intent);

            System.Console.WriteLine("action:" + intent.getAction());
            System.Console.WriteLine("type:" + intent.getType());
            System.Console.WriteLine("fromPackageName:" + fromPackageNameString);
            System.Console.WriteLine("command:" + commandString);
            System.Console.WriteLine("uri:" + uriString);

            SurfaceView sv = new SurfaceView(this);
            setContentView(sv);


            #region xCallback
            // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceView\ApplicationActivity.cs
            var xCallback = new xCallback
            {
                onsurfaceCreated = holder =>
                {
                    if (appPtr == 0)
                        return;

                    nativeSurfaceCreated(appPtr, holder.getSurface());
                    mSurfaceHolder = holder;
                },

                onsurfaceChanged = (SurfaceHolder holder, int format, int width, int height) =>
                {
                    if (appPtr == 0)
                        return;

                    nativeSurfaceChanged(appPtr, holder.getSurface());
                    mSurfaceHolder = holder;
                },

                onsurfaceDestroyed = holder =>
                {
                    if (appPtr == 0)
                        return;

                    nativeSurfaceDestroyed(appPtr);
                    mSurfaceHolder = holder;
                }
            };
            #endregion

            sv.getHolder().addCallback(xCallback);

            // Force the screen to stay on, rather than letting it dim and shut off
            // while the user is watching a movie.
            //getWindow().addFlags( WindowManager_LayoutParams.FLAG_KEEP_SCREEN_ON );

            //// Force screen brightness to stay at maximum
            //WindowManager.LayoutParams params = getWindow().getAttributes();
            //params.screenBrightness = 1.0f;
            //getWindow().setAttributes( params );

            this.ondispatchTouchEvent += (e) =>
            {
                int action = e.getAction();
                float x = e.getRawX();
                float y = e.getRawY();
                Log.d(TAG, "onTouch dev:" + e.getDeviceId() + " act:" + action + " ind:" + e.getActionIndex() + " @ " + x + "," + y);
                nativeTouch(appPtr, action, x, y);
                return true;
            };

            this.ondispatchKeyEvent = (e) =>
            {
                bool down;
                int keyCode = e.getKeyCode();
                int deviceId = e.getDeviceId();

                if (e.getAction() == KeyEvent.ACTION_DOWN)
                {
                    down = true;
                }
                else if (e.getAction() == KeyEvent.ACTION_UP)
                {
                    down = false;
                }
                else
                {
                    Log.d(TAG,
                            "source " + e.getSource() + " action "
                                    + e.getAction() + " keyCode " + keyCode);

                    return base.dispatchKeyEvent(e);
                }

                Log.d(TAG, "source " + e.getSource() + " keyCode " + keyCode + " down " + down + " repeatCount " + e.getRepeatCount());

                if (keyCode == KeyEvent.KEYCODE_VOLUME_UP)
                {
                    if (down)
                    {
                        adjustVolume(1);
                    }
                    return true;
                }

                if (keyCode == KeyEvent.KEYCODE_VOLUME_DOWN)
                {
                    if (down)
                    {
                        adjustVolume(-1);
                    }
                    return true;
                }


                // Joypads will register as keyboards, but keyboards won't
                // register as position classes
                // || event.getSource() != 16777232)
                // Volume buttons are source 257
                if (e.getSource() == 1281)
                {
                    // do we have one we can test with?

                    //keyCode |= JoyEvent.BUTTON_JOYPAD_FLAG;
                }
                return buttonEvent(deviceId, keyCode, down, e.getRepeatCount());
            };
        }


        protected bool buttonEvent(int deviceId, int keyCode, bool down, int repeatCount)
        {
            //Log.d(TAG, "buttonEvent " + deviceId + " " + keyCode + " " + down);

            // DispatchKeyEvent will cause the K joypads to spawn other
            // apps on select and "game", which we don't want, so manually call
            // onKeyDown or onKeyUp


            var a = down ? KeyEvent.ACTION_DOWN : KeyEvent.ACTION_UP;
            ////KeyEvent ev = new KeyEvent(0, 0, a, keyCode, repeatCount);

            // This was confusing because it called VrActivity::onKeyDown.  Activity::onKeyDown is only supposed to be 
            // called if the app views didn't consume any keys. Since we intercept dispatchKeyEvent and always returning true
            // for ACTION_UP and ACTION_DOWN, we effectively consume ALL key events that would otherwise go to Activity::onKeyDown
            // Activity::onKeyUp, so calling them here means they're getting called when we consume events, even though the 
            // VrActivity versions were effectively the consumers by calling nativeKeyEvent.  Instead, call nativeKeyEvent
            // here directly.
            if (down)
            {
                //nativeKeyEvent(appPtr, keyCode, true, ev.getRepeatCount());
                nativeKeyEvent(appPtr, keyCode, true, repeatCount);
            }
            else
            {
                nativeKeyEvent(appPtr, keyCode, false, 0);
            }
            return true;
        }





        public void finishActivity()
        {
            finish();
        }

        #region Activity life cycle
        //protected override void onRestart()
        //{
        //    base.onStart();
        //}

        protected override void onStart()
        {
            base.onStart();
            //GLES3JNILib.onStart(appPtr);
        }

        protected override void onResume()
        {
            base.onResume();
            nativeOnResume(appPtr);
        }

        protected override void onPause()
        {
            nativeOnPause(appPtr);
            base.onPause();
        }

        protected override void onStop()
        {
            //GLES3JNILib.onStop(appPtr);
            base.onStop();
        }

        protected override void onDestroy()
        {
            if (mSurfaceHolder != null)
            {
                nativeSurfaceDestroyed(appPtr);
            }
            nativeOnDestroy(appPtr);
            base.onDestroy();
            appPtr = 0;

            soundPool.release();
            soundPoolSoundIds.clear();
            soundPoolSoundNames.clear();
        }

        #endregion



        public override void onConfigurationChanged(Configuration value)
        {
            base.onConfigurationChanged(value);
        }



        #region onAudioFocusChange
        // how about 360 audio?
        // For trivial feedback sound effects
        private SoundPool soundPool;
        private java.util.ArrayList<int> soundPoolSoundIds;
        private java.util.ArrayList<string> soundPoolSoundNames;


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


        System.Func<KeyEvent, bool> ondispatchKeyEvent;
        public override bool dispatchKeyEvent(KeyEvent @event)
        {


            return ondispatchKeyEvent(@event);
        }

        public event System.Func<MotionEvent, bool> ondispatchTouchEvent;
        public override bool dispatchTouchEvent(MotionEvent @event)
        {
            return ondispatchTouchEvent(@event);
        }

        #region toasts

        public void clearVrToasts()
        {
            Log.v(TAG, "clearVrToasts, calling nativePopup");
            nativePopup(appPtr, 0, 0, -1.0f);
        }

        SurfaceTexture toastTexture;
        Surface toastSurface;

        public void createVrToastOnUiThread(string text)
        {
            //runOnUiThread( new Thread()
            //{
            // @Override
            //    public void run()
            //    {
            //        VrActivity.this.createVrToast(text);
            //    }
            //});
        }

        // TODO: pass in the time delay

        // The warning about not calling show is irrelevant -- we are
        // drawing it to a texture
        //@SuppressLint("ShowToast")
        public void createVrToast(string text)
        {
            if (text == null)
            {
                text = "null toast text!";
            }
            Log.v(TAG, "createVrToast " + text);

            // If we haven't set up the surface / surfaceTexture yet,
            // do it now.
            if (toastTexture == null)
            {
                toastTexture = nativeGetPopupSurfaceTexture(appPtr);
                if (toastTexture == null)
                {
                    Log.e(TAG, "nativePreparePopup returned NULL");
                    return; // not set up yet
                }
                toastSurface = new Surface(toastTexture);
            }

            Toast toast = Toast.makeText(this.getApplicationContext(), text,
                    Toast.LENGTH_SHORT);

            this.createVrToast(toast.getView());
        }

        public void createVrToast(View toastView)
        {
            // Find how big the toast wants to be.
            toastView.measure(0, 0);
            toastView.layout(0, 0, toastView.getMeasuredWidth(),
                    toastView.getMeasuredHeight());

            Log.v(TAG,
                    "toast size:" + toastView.getWidth() + "x"
                            + toastView.getHeight());
            toastTexture.setDefaultBufferSize(toastView.getWidth(),
                    toastView.getHeight());
            try
            {
                Canvas canvas = toastSurface.lockCanvas(null);
                toastView.draw(canvas);
                toastSurface.unlockCanvasAndPost(canvas);
            }
            catch
            {
                Log.e(TAG, "lockCanvas threw exception");
            }

            nativePopup(appPtr, toastView.getWidth(), toastView.getHeight(), 7.0f);
        }

        #endregion


        // methods called from C?


        public string getInstalledPackagePath(string packageName)
        {
            //I/System.Console(30347): 768b:0001 enter OVROculus360Photos LocalApplication onCreate, first time?
            //W/art     (30113): ConditionVariable::~ConditionVariable for GC request condition variable called with 1 waiters.
            //I/System.Console(30347): 768b:0001 enter OVROculus360Photos ApplicationActivity onCreate
            //I/JniUtils(30347): Using caller's JNIEnv
            //I/System.Console(30347): 768b:0001 Searching installed packages for 'com.oculus.systemactivities'
            //I/JniUtils(30347): ovr_GetCurrentPackageName() = OVROculus360PhotosHUD.Activities
            //I/JniUtils(30347): ovr_GetPackageCodePath() = '/data/app/OVROculus360PhotosHUD.Activities-1/base.apk'
            //E/JniUtils(30347): FindClass( com/oculus/vrappframework/ConsoleReceiver ) failed

            System.Console.WriteLine(
                "Searching installed packages for '" + packageName + "'");
            var appList0 = getPackageManager().getInstalledApplications(0);

            // List<ApplicationInfo>
            var appList = Enumerable.Range(0, appList0.size()).Select(i => (ApplicationInfo)appList0.get(i));



            var outString =
                from info in appList
                where info.className != null
                where info.className.ToLower().Contains(packageName)
                select info.sourceDir;

            if (outString.Any())
                return outString.FirstOrDefault();


            outString =
                  from info in appList
                  where info.sourceDir != null
                  where info.sourceDir.ToLower().Contains(packageName)
                  select info.sourceDir;


            if (outString.Any())
                return outString.FirstOrDefault();

            // /art     (28618): sart/runtime/check_jni.cc:65] JNI DETECTED ERROR IN APPLICATION: GetStringUTFChars received null jstring
            return "";
        }


        class xRunnable : java.lang.Runnable
        {
            public System.Action yield;
            public void run()
            {
                yield();
            }
        }

        private static long downTime;

        // E/JniUtils(11823): couldn't get gazeEventFromNative, (FFZZLandroid/app/Activity;)V
        private static void gazeEventFromNative(float x, float y, bool press, bool release, Activity target)
        {
            Log.d(TAG, "gazeEventFromNative( " + x + " " + y + " " + press + " " + release + " " + target);

            (new Handler(Looper.getMainLooper())).post(

                new xRunnable()
                {
                    yield = delegate
                {
                    long now = SystemClock.uptimeMillis();
                    if (press)
                    {
                        downTime = now;
                    }

                    MotionEvent.PointerProperties pp = new MotionEvent.PointerProperties();
                    pp.toolType = MotionEvent.TOOL_TYPE_FINGER;
                    pp.id = 0;
                    MotionEvent.PointerProperties[] ppa = new MotionEvent.PointerProperties[1];
                    ppa[0] = pp;

                    MotionEvent.PointerCoords pc = new MotionEvent.PointerCoords();
                    pc.x = x;
                    pc.y = y;
                    MotionEvent.PointerCoords[] pca = new MotionEvent.PointerCoords[1];
                    pca[0] = pc;

                    int eventType = MotionEvent.ACTION_MOVE;
                    if (press)
                    {
                        eventType = MotionEvent.ACTION_DOWN;
                    }
                    else if (release)
                    {
                        eventType = MotionEvent.ACTION_UP;
                    }

                    MotionEvent ev = MotionEvent.obtain(
                            downTime, now,
                            eventType,
                            1, ppa, pca,
                            0, /* meta state */
                            0, /* button state */
                            1.0f, 1.0f, /* precision */
                            10,	/* device ID */
                            0,	/* edgeFlags */
                            InputDevice.SOURCE_TOUCHSCREEN,
                            0 /* flags */ );

                    Log.d(TAG, "Synthetic:" + ev);
                    Window w = target.getWindow();
                    View v = w.getDecorView();
                    v.dispatchTouchEvent(ev);
                }
                });
        }














        public static bool getBluetoothEnabled(Activity act)
        {
            return Settings.Global.getInt(act.getContentResolver(),
                    Settings.Global.BLUETOOTH_ON, 0) != 0;
        }

        public static bool isWifiConnected(Activity act)
        {
            ConnectivityManager connManager = (ConnectivityManager)act.getSystemService(Context.CONNECTIVITY_SERVICE);
            NetworkInfo mWifi = connManager.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
            return mWifi.isConnected();
        }

        public static bool isAirplaneModeEnabled(Activity act)
        {
            return Settings.Global.getInt(act.getContentResolver(),
                    Settings.Global.AIRPLANE_MODE_ON, 0) != 0;
        }

        // returns true if time settings specifies 24 hour format
        public static bool isTime24HourFormat(Activity act)
        {
            ContentResolver cr = act.getContentResolver();
            string v = Settings.System.getString(cr, android.provider.Settings.System.TIME_12_24);
            return "12" != v;
        }

        public static bool isHybridApp(Activity act)
        {
            var v = false;

            try
            {
                ApplicationInfo appInfo = act.getPackageManager().getApplicationInfo(act.getPackageName(), PackageManager.GET_META_DATA);
                Bundle bundle = appInfo.metaData;
                string applicationMode = bundle.getString("com.samsung.android.vr.application.mode");
                v = (applicationMode == ("dual"));
            }
            catch
            {
                Log.e(TAG, "Failed to load meta-data");
            }

            return v;
        }

        public static string getExternalStorageDirectory()
        {
            return Environment.getExternalStorageDirectory().getAbsolutePath();
        }



        // Converts some thing like "/sdcard" to "/sdcard/", always ends with "/" to indicate folder path
        public static string toFolderPathFormat(string inStr)
        {
            if (inStr == null ||
                inStr.Length == 0)
            {
                return "/";
            }

            if (inStr[inStr.Length - 1] != '/')
            {
                return inStr + "/";
            }

            return inStr;
        }

        #region Internal Storage
        public static string getInternalStorageRootDir()
        {
            return toFolderPathFormat(android.os.Environment.getDataDirectory().getPath());
        }

        public static string getInternalStorageFilesDir(Activity act)
        {
            if (act != null)
            {
                return toFolderPathFormat(act.getFilesDir().getPath());
            }
            else
            {
                Log.e(TAG, "Activity is null in getInternalStorageFilesDir method");
            }
            return "";
        }

        public static string getInternalStorageCacheDir(Activity act)
        {
            if (act != null)
            {
                return toFolderPathFormat(act.getCacheDir().getPath());
            }
            else
            {
                Log.e(TAG, "activity is null getInternalStorageCacheDir method");
            }
            return "";
        }

        public static long getInternalCacheMemoryInBytes(Activity act)
        {
            if (act != null)
            {
                string path = getInternalStorageCacheDir(act);
                StatFs stat = new StatFs(path);
                return stat.getAvailableBytes();
            }
            else
            {
                Log.e(TAG, "activity is null getInternalCacheMemoryInBytes method");
            }
            return 0;
        }
        #endregion


        #region External Storage
        public static string getExternalStorageFilesDirAtIdx(Activity act, int idx)
        {
            if (act != null)
            {
                File[] filesDirs = act.getExternalFilesDirs(null);
                if (filesDirs != null && filesDirs.Length > idx && filesDirs[idx] != null)
                {
                    return toFolderPathFormat(filesDirs[idx].getPath());
                }
            }
            else
            {
                Log.e(TAG, "activity is null getExternalStorageFilesDirAtIdx method");
            }
            return "";
        }

        public static string getExternalStorageCacheDirAtIdx(Activity act, int idx)
        {
            if (act != null)
            {
                File[] cacheDirs = act.getExternalCacheDirs();
                if (cacheDirs != null && cacheDirs.Length > idx && cacheDirs[idx] != null)
                {
                    return toFolderPathFormat(cacheDirs[idx].getPath());
                }
            }
            else
            {
                Log.e(TAG, "activity is null in getExternalStorageCacheDirAtIdx method with index " + idx);
            }
            return "";
        }
        #endregion

        #region Primary External Storage
        public static int PRIMARY_EXTERNAL_STORAGE_IDX = 0;
        public static string getPrimaryExternalStorageRootDir(Activity act)
        {
            return toFolderPathFormat(Environment.getExternalStorageDirectory().getPath());
        }

        public static string getPrimaryExternalStorageFilesDir(Activity act)
        {
            return getExternalStorageFilesDirAtIdx(act, PRIMARY_EXTERNAL_STORAGE_IDX);
        }

        public static string getPrimaryExternalStorageCacheDir(Activity act)
        {
            return getExternalStorageCacheDirAtIdx(act, PRIMARY_EXTERNAL_STORAGE_IDX);
        }
        #endregion

        #region Secondary External Storage
        public static int SECONDARY_EXTERNAL_STORAGE_IDX = 1;
        public static string getSecondaryExternalStorageRootDir()
        {
            return "/storage/extSdCard/";
        }

        public static string getSecondaryExternalStorageFilesDir(Activity act)
        {
            return getExternalStorageFilesDirAtIdx(act, SECONDARY_EXTERNAL_STORAGE_IDX);
        }

        public static string getSecondaryExternalStorageCacheDir(Activity act)
        {
            return getExternalStorageCacheDirAtIdx(act, SECONDARY_EXTERNAL_STORAGE_IDX);
        }
        #endregion

        #region locale / why bother?

        public static string getCurrentLanguage()
        {
            return Locale.getDefault().getLanguage();
        }

        public static string getDisplayLanguageForLocaleCode(string code)
        {
            Locale locale = new Locale(code);
            return locale.getDisplayLanguage();
        }

        private void setLocale(string localeName)
        {
            Configuration conf = getResources().getConfiguration();
            conf.locale = new Locale(localeName);

            //setCurrentLanguage(conf.locale.getLanguage());

            DisplayMetrics metrics = new DisplayMetrics();
            getWindowManager().getDefaultDisplay().getMetrics(metrics);

            // the next line just changes the application's locale. It changes this
            // globally and not just in the newly created resource
            Resources res = new Resources(getAssets(), metrics, conf);
            // since we don't need the resource, just allow it to be garbage collected.
            res = null;

            Log.d(TAG, "setLocale: locale set to " + localeName);
        }

        private void setDefaultLocale()
        {
            setLocale("en");
        }

        public string getLocalizedString(string name)
        {
            //Log.v("VrLocale", "getLocalizedString for " + name );
            string outString = "";
            int id = getResources().getIdentifier(name, "string", getPackageName());
            if (id == 0)
            {
                // 0 is not a valid resource id
                Log.v("VrLocale", name + " is not a valid resource id!!");
                return outString;
            }
            if (id != 0)
            {
                outString = getResources().getText(id).ToString();
                //Log.v("VrLocale", "getLocalizedString resolved " + name + " to " + outString);
            }
            return outString;
        }

        #endregion
    }
}
