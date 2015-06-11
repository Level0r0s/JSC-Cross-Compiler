/************************************************************************************

Filename    :   VrActivity.java
Content     :   Activity used by the application framework.
Created     :   9/26/2013
Authors     :   John Carmack

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

*************************************************************************************/
package com.oculus.vrappframework;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.ActivityGroup;
import android.content.ContentResolver;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.AssetFileDescriptor;
import android.content.res.Configuration;
import android.content.res.Resources;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.graphics.Canvas;
import android.graphics.SurfaceTexture;
import android.media.AudioManager;
import android.media.SoundPool;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.os.StatFs;
import android.os.Handler;
import android.os.Looper;
import android.os.SystemClock;
import android.provider.Settings;
import android.util.Log;
import android.util.DisplayMetrics;
import android.view.InputDevice;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.Surface;
import android.view.SurfaceHolder;
import android.view.SurfaceView;
import android.view.View;
import android.view.Window;
import android.view.WindowManager;
import android.widget.Toast;
//DVFS import android.os.DVFSHelper;


@SuppressWarnings("deprecation")
public class VrActivity extends ActivityGroup implements 	SurfaceHolder.Callback,
															AudioManager.OnAudioFocusChangeListener {

	public static final String TAG = "VrActivity";

	private static native void nativeOnCreate(VrActivity act);
	private static native void nativeOnResume(long appPtr);
	private static native void nativeOnPause(long appPtr);
	private static native void nativeOnDestroy(long appPtr);
	private static native void nativeNewIntent(long appPtr, String fromPackageName, String command, String uriString);
	private static native void nativeSurfaceCreated(long appPtr, Surface s);
	private static native void nativeSurfaceChanged(long appPtr, Surface s);
	private static native void nativeSurfaceDestroyed(long appPtr);
	private static native void nativeKeyEvent(long appPtr, int keyNum, boolean down, int repeatCount );
	private static native void nativeJoypadAxis(long appPtr, float lx, float ly, float rx, float ry);
	private static native void nativeTouch(long appPtr, int action, float x, float y );
	private static native SurfaceTexture nativeGetPopupSurfaceTexture(long appPtr);
	private static native void nativePopup(long appPtr, int width, int height, float seconds);

	// Pass down to native code so we talk to the right App object.
	// This is set by the subclass in onCreate
	//   setAppPtr( nativeSetAppInterface( this, ... ) );
	private long appPtr;

    public long getAppPtr() {
      return this.appPtr;
    }

    public void setAppPtr(long appPtr) {
      if (this.appPtr != 0) {
        throw new RuntimeException("Application pointer is already set!");
      }
      this.appPtr = appPtr;
    }

	// While there is a valid surface holder, onDestroy can destroy the surface when
	// surfaceDestroyed is not called before onDestroy.
	private SurfaceHolder mSurfaceHolder;

	// For trivial feedback sound effects
	private SoundPool		soundPool;
	private List<Integer>	soundPoolSoundIds;
	private List<String>	soundPoolSoundNames;

	// These variables duplicated in VrLib.java!
	public static final String INTENT_KEY_CMD = "intent_cmd";
	public static final String INTENT_KEY_FROM_PKG = "intent_pkg";

	public static String getCommandStringFromIntent( Intent intent ) {
		String commandStr = "";
		if ( intent != null ) {
			commandStr = intent.getStringExtra( INTENT_KEY_CMD );
			if ( commandStr == null ) {
				commandStr = "";
			}
		}
		return commandStr;
	}

	public static String getPackageStringFromIntent( Intent intent ) {
		String packageStr = "";
		if ( intent != null ) {
			packageStr = intent.getStringExtra( INTENT_KEY_FROM_PKG );
			if ( packageStr == null ) {
				packageStr = "";
			}
		}
		return packageStr;
	}

	public static String getUriStringFromIntent( Intent intent ) {
		String uriString = "";
		if ( intent != null ) {
			Uri uri = intent.getData();
			if ( uri != null ) {
				uriString = uri.toString();
				if ( uriString == null ) {
					uriString = "";
				}
			}
		}
		return uriString;
	}
	
	/*
	================================================================================

	Activity life cycle

	================================================================================
	*/

	@Override protected void onCreate(Bundle savedInstanceState) {
		Log.d( TAG, this + " onCreate()" );
		super.onCreate(savedInstanceState);

		nativeOnCreate( this );

		setCurrentLanguage( Locale.getDefault().getLanguage() );

		// Create the SoundPool
		soundPool = new SoundPool(3 /* voices */, AudioManager.STREAM_MUSIC, 100);
		soundPoolSoundIds = new ArrayList<Integer>();
		soundPoolSoundNames = new ArrayList<String>();
				
		AudioManager audioMgr;
		audioMgr = (AudioManager)getSystemService(Context.AUDIO_SERVICE);
		String rate = audioMgr.getProperty(AudioManager.PROPERTY_OUTPUT_SAMPLE_RATE);
		String size = audioMgr.getProperty(AudioManager.PROPERTY_OUTPUT_FRAMES_PER_BUFFER);
		Log.d( TAG, "rate = " + rate );
		Log.d( TAG, "size = " + size );
		
		// Check preferences
		SharedPreferences prefs = getApplicationContext().getSharedPreferences( "oculusvr", MODE_PRIVATE );
		String username = prefs.getString( "username", "guest" );
		Log.d( TAG, "username = " + username );
		
		// Check for being launched with a particular intent
		Intent intent = getIntent();

		String commandString = getCommandStringFromIntent( intent );
		String fromPackageNameString = getPackageStringFromIntent( intent );
		String uriString = getUriStringFromIntent( intent );

		Log.d( TAG, "action:" + intent.getAction() );
		Log.d( TAG, "type:" + intent.getType() );
		Log.d( TAG, "fromPackageName:" + fromPackageNameString );
		Log.d( TAG, "command:" + commandString );
		Log.d( TAG, "uri:" + uriString );

		SurfaceView sv = new SurfaceView( this );
		setContentView( sv );

		sv.getHolder().addCallback( this );

		// Force the screen to stay on, rather than letting it dim and shut off
		// while the user is watching a movie.
		getWindow().addFlags( WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON );

		// Force screen brightness to stay at maximum
		WindowManager.LayoutParams params = getWindow().getAttributes();
		params.screenBrightness = 1.0f;
		getWindow().setAttributes( params );
	}	
	
	@Override protected void onRestart() {
		Log.d(TAG, this + " onRestart()");		
		super.onRestart();
	}

	@Override protected void onStart() {
		Log.d(TAG, this + " onStart()");
		super.onStart();
	}

	@Override protected void onResume() {
		Log.d(TAG, this + " onResume()");

		super.onResume();
		nativeOnResume(appPtr);
	}
	
	@Override protected void onPause() {
		Log.d(TAG, this + " onPause()");
		
		nativeOnPause(appPtr);
		super.onPause();
	}

	@Override protected void onStop() {
		Log.d(TAG, this + " onStop()");
		super.onStop();
	}
	
	@Override protected void onDestroy() {
		Log.d(TAG, this + " onDestroy()");
		if ( mSurfaceHolder != null ) {
			nativeSurfaceDestroyed(appPtr);
		}

		nativeOnDestroy( appPtr );
		appPtr = 0;
		super.onDestroy();

		soundPool.release();
		soundPoolSoundIds.clear();
		soundPoolSoundNames.clear();
	}
		
	@Override public void onConfigurationChanged(Configuration newConfig) {
		Log.d(TAG, this + " onConfigurationChanged()");
		super.onConfigurationChanged(newConfig);
	}

	@Override protected void onNewIntent( Intent intent ) {
		Log.d(TAG, "onNewIntent()");

		String commandString = getCommandStringFromIntent( intent );
		String fromPackageNameString = getPackageStringFromIntent( intent );
		String uriString = getUriStringFromIntent( intent );

		Log.d(TAG, "action:" + intent.getAction() );
		Log.d(TAG, "type:" + intent.getType() );
		Log.d(TAG, "fromPackageName:" + fromPackageNameString );
		Log.d(TAG, "command:" + commandString );
		Log.d(TAG, "uri:" + uriString );

		nativeNewIntent( appPtr, fromPackageNameString, commandString, uriString );
	}

	public void finishActivity() {
		finish();
	}
		
	/*
	================================================================================

	Surface life cycle

	================================================================================
	*/

	@Override public void surfaceCreated(SurfaceHolder holder) {
		Log.d(TAG, this + " surfaceCreated()");
		if ( appPtr != 0 ) {
			nativeSurfaceCreated(appPtr, holder.getSurface());
			mSurfaceHolder = holder;
		}
	}

	@Override public void surfaceChanged(SurfaceHolder holder, int format, int width, int height) {
		Log.d(TAG, this + " surfaceChanged() format: " + format + " width: " + width + " height: " + height);
		if ( appPtr != 0 ) {
			nativeSurfaceChanged(appPtr, holder.getSurface());
			mSurfaceHolder = holder;
		}
	}
	
	@Override public void surfaceDestroyed(SurfaceHolder holder) {
		Log.d(TAG, this + " surfaceDestroyed()");
		if ( appPtr != 0 ) {
			nativeSurfaceDestroyed(appPtr);
			mSurfaceHolder = null;
		}
	}

	/*
	================================================================================

	Sound

	================================================================================
	*/

	public void playSoundPoolSound(String name) {
		for ( int i = 0 ; i < soundPoolSoundNames.size() ; i++ ) {
			if ( soundPoolSoundNames.get(i).equals( name ) ) {
				soundPool.play( soundPoolSoundIds.get( i ), 1.0f, 1.0f, 1, 0, 1 );
				return;
			}
		}

		Log.d(TAG, "playSoundPoolSound: loading "+name);
		
		// check first if this is a raw resource
		int soundId = 0;
		if ( name.indexOf( "res/raw/" ) == 0 ) {
			String resourceName = name.substring( 4, name.length() - 4 );
			int id = getResources().getIdentifier( resourceName, "raw", getPackageName() );
			if ( id == 0 ) {
				Log.e( TAG, "No resource named " + resourceName );
			} else {
				AssetFileDescriptor afd = getResources().openRawResourceFd( id );
				soundId = soundPool.load( afd, 1 );
			}
		} else {
			try {
				AssetFileDescriptor afd = getAssets().openFd( name );
				soundId = soundPool.load( afd, 1 );
			} catch ( IOException t ) {
				Log.e( TAG, "Couldn't open " + name + " because " + t.getMessage() );
			}
		}

		if ( soundId == 0 )
		{
			// Try to load the sound directly - works for absolute path - for wav files for sdcard for ex.
			soundId = soundPool.load( name, 1 );
		}

		soundPoolSoundNames.add( name );
		soundPoolSoundIds.add( soundId );
		
		soundPool.play( soundPoolSoundIds.get( soundPoolSoundNames.size() - 1 ), 1.0f, 1.0f, 1, 0, 1 );
	}

	void adjustVolume(int direction) {
		AudioManager audio = (AudioManager) getSystemService(Context.AUDIO_SERVICE);
		audio.adjustStreamVolume(AudioManager.STREAM_MUSIC, direction, 0);
	}

	public static void requestAudioFocus( VrActivity act) {
		
		AudioManager audioManager = (AudioManager)act.getSystemService( Context.AUDIO_SERVICE );
		
		// Request audio focus
		int result = audioManager.requestAudioFocus( act, AudioManager.STREAM_MUSIC,
			AudioManager.AUDIOFOCUS_GAIN );
		if ( result == AudioManager.AUDIOFOCUS_REQUEST_GRANTED ) 
		{
			Log.d(TAG,"requestAudioFocus(): GRANTED audio focus");
		} 
		else if ( result == AudioManager.AUDIOFOCUS_REQUEST_FAILED ) 
		{
			Log.d(TAG,"requestAudioFocus(): FAILED to gain audio focus");
		}
	}

	public static void releaseAudioFocus( VrActivity act ) {
		AudioManager audioManager = (AudioManager)act.getSystemService( Context.AUDIO_SERVICE );
		audioManager.abandonAudioFocus( act );
	}
	
	public void onAudioFocusChange(int focusChange) 
    {
		switch( focusChange ) 
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
	
	/*
	================================================================================

	Input

	================================================================================
	*/

	private float deadBand(float f) {
		// The K joypad seems to need a little more dead band to prevent
		// creep.
		if (f > -0.01f && f < 0.01f) {
			return 0.0f;
		}
		return f;
	}

	private int axisButtons(int deviceId, float axisValue, int negativeButton, int positiveButton,
			int previousState) {
		int currentState;
		if (axisValue < -0.5f) {
			currentState = -1;
		} else if (axisValue > 0.5f) {
			currentState = 1;
		} else {
			currentState = 0;
		}
		if (currentState != previousState) {
			if (previousState == -1) {
				// negativeButton up
				buttonEvent(deviceId, negativeButton, false, 0);
			} else if (previousState == 1) {
				// positiveButton up
				buttonEvent(deviceId, positiveButton, false, 0);
			}

			if (currentState == -1) {
				// negativeButton down
				buttonEvent(deviceId, negativeButton, true, 0);
			} else if (currentState == 1) {
				// positiveButton down
				buttonEvent(deviceId, positiveButton, true, 0);
			}
		}
		return currentState;
	}

	private int[] axisState = new int[6];
	private int[] axisAxis = { MotionEvent.AXIS_HAT_X, MotionEvent.AXIS_HAT_Y, MotionEvent.AXIS_X, MotionEvent.AXIS_Y, MotionEvent.AXIS_RX, MotionEvent.AXIS_RY };
	private int[] axisNegativeButton = { JoyEvent.KEYCODE_DPAD_LEFT, JoyEvent.KEYCODE_DPAD_UP, JoyEvent.KEYCODE_LSTICK_LEFT, JoyEvent.KEYCODE_LSTICK_UP, JoyEvent.KEYCODE_RSTICK_LEFT, JoyEvent.KEYCODE_RSTICK_UP };
	private int[] axisPositiveButton = { JoyEvent.KEYCODE_DPAD_RIGHT, JoyEvent.KEYCODE_DPAD_DOWN, JoyEvent.KEYCODE_LSTICK_RIGHT, JoyEvent.KEYCODE_LSTICK_DOWN, JoyEvent.KEYCODE_RSTICK_RIGHT, JoyEvent.KEYCODE_RSTICK_DOWN };
			
	@Override
	public boolean dispatchGenericMotionEvent(MotionEvent event) {
		if ((event.getSource() & InputDevice.SOURCE_CLASS_JOYSTICK) != 0
				&& event.getAction() == MotionEvent.ACTION_MOVE) {
			// The joypad sends a single event with all the axis
			
			// Unfortunately, the Moga joypad in HID mode uses AXIS_Z
			// where the Samsnung uses AXIS_RX, and AXIS_RZ instead of AXIS_RY
			
			// Log.d(TAG,
			// String.format("onTouchEvent: %f %f %f %f",
			// event.getAxisValue(MotionEvent.AXIS_X),
			// event.getAxisValue(MotionEvent.AXIS_Y),
			// event.getAxisValue(MotionEvent.AXIS_RX),
			// event.getAxisValue(MotionEvent.AXIS_RY)));
			nativeJoypadAxis(appPtr, deadBand(event.getAxisValue(MotionEvent.AXIS_X)),
					deadBand(event.getAxisValue(MotionEvent.AXIS_Y)),
					deadBand(event.getAxisValue(MotionEvent.AXIS_RX))
						+ deadBand(event.getAxisValue(MotionEvent.AXIS_Z)),		// Moga uses  Z for R-stick X
					deadBand(event.getAxisValue(MotionEvent.AXIS_RY))
						+ deadBand(event.getAxisValue(MotionEvent.AXIS_RZ)));	// Moga uses RZ for R-stick Y

			// Turn the hat and thumbstick axis into buttons
			for ( int i = 0 ; i < 6 ; i++ ) {
				axisState[i] = axisButtons( event.getDeviceId(),
						event.getAxisValue(axisAxis[i]),
						axisNegativeButton[i], axisPositiveButton[i],
						axisState[i]);
			}
					
			return true;
		}
		return super.dispatchGenericMotionEvent(event);
	}

    @Override
	public boolean dispatchKeyEvent(KeyEvent event) {
		//Log.d(TAG, "dispatchKeyEvent  " + event );
		boolean down;
		int keyCode = event.getKeyCode();
		int deviceId = event.getDeviceId();

		if (event.getAction() == KeyEvent.ACTION_DOWN) {
			down = true;
		} else if (event.getAction() == KeyEvent.ACTION_UP) {
			down = false;
		} else {
			Log.d(TAG,
					"source " + event.getSource() + " action "
							+ event.getAction() + " keyCode " + keyCode);

			return super.dispatchKeyEvent(event);
		}

		Log.d(TAG, "source " + event.getSource() + " keyCode " + keyCode + " down " + down + " repeatCount " + event.getRepeatCount() );

		if (keyCode == KeyEvent.KEYCODE_VOLUME_UP) {
			if ( down ) {
				adjustVolume(1);
			}
			return true;
		}

		if (keyCode == KeyEvent.KEYCODE_VOLUME_DOWN) {
			if ( down ) {
				adjustVolume(-1);	
			}
			return true;
		}

		
		// Joypads will register as keyboards, but keyboards won't
		// register as position classes
		// || event.getSource() != 16777232)
		// Volume buttons are source 257
		if (event.getSource() == 1281) {
			keyCode |= JoyEvent.BUTTON_JOYPAD_FLAG;			
		}
		return buttonEvent(deviceId, keyCode, down, event.getRepeatCount() );
	}

	/*
	 * Called for real key events from dispatchKeyEvent(), and also
	 * the synthetic dpad 
	 * 
	 * This is where the framework can intercept a button press before
	 * it is passed on to the application.
	 * 
	 * Keyboard buttons will be standard Android key codes, but joystick buttons
	 * will have BUTTON_JOYSTICK or'd into it, because you may want arrow keys
	 * on the keyboard to perform different actions from dpad buttons on a
	 * joypad.
	 * 
	 * The BUTTON_* values are what you get in joyCode from our reference
	 * joypad.
	 * 
	 * @return Return true if this event was consumed.
	 */
	protected boolean buttonEvent(int deviceId, int keyCode, boolean down, int repeatCount ) {
		//Log.d(TAG, "buttonEvent " + deviceId + " " + keyCode + " " + down);
		
		// DispatchKeyEvent will cause the K joypads to spawn other
		// apps on select and "game", which we don't want, so manually call
		// onKeyDown or onKeyUp
		
		KeyEvent ev = new KeyEvent( 0, 0, down ? KeyEvent.ACTION_DOWN : KeyEvent.ACTION_UP, keyCode, repeatCount );
		
		// This was confusing because it called VrActivity::onKeyDown.  Activity::onKeyDown is only supposed to be 
		// called if the app views didn't consume any keys. Since we intercept dispatchKeyEvent and always returning true
		// for ACTION_UP and ACTION_DOWN, we effectively consume ALL key events that would otherwise go to Activity::onKeyDown
		// Activity::onKeyUp, so calling them here means they're getting called when we consume events, even though the 
		// VrActivity versions were effectively the consumers by calling nativeKeyEvent.  Instead, call nativeKeyEvent
		// here directly.
		if ( down ) {
			nativeKeyEvent( appPtr, keyCode, true, ev.getRepeatCount() );
		}
		else
		{
			nativeKeyEvent( appPtr, keyCode, false, 0 );
		}
		return true;
	}

	@Override
	public boolean dispatchTouchEvent( MotionEvent e ) {
		// Log.d(TAG, "real:" + e );		
		int action = e.getAction();
		float x = e.getRawX();
		float y = e.getRawY();
		Log.d(TAG, "onTouch dev:" + e.getDeviceId() + " act:" + action + " ind:" + e.getActionIndex() + " @ "+ x + "," + y );
		nativeTouch( appPtr, action, x, y );
		return true;
	}

	private static long downTime;
	
	private static void gazeEventFromNative( final float x, final float y, final boolean press, final boolean release, final Activity target ) {
		Log.d(TAG, "gazeEventFromNative( " + x + " " + y + " " + press + " " + release + " " + target );
	
		(new Handler(Looper.getMainLooper())).post(new Runnable() {
			@Override
			public void run() {
				long now = SystemClock.uptimeMillis();
				if ( press ) {
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
				if ( press )
				{
					eventType = MotionEvent.ACTION_DOWN;
				}
				else if ( release )
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
				
				Log.d(TAG, "Synthetic:" + ev );
				Window w = target.getWindow();
				View v = w.getDecorView();
				v.dispatchTouchEvent( ev );
			}
			});		
	}

	/*
	================================================================================

	Locale

	================================================================================
	*/

	private void setLocale( String localeName )
	{
		Configuration conf = getResources().getConfiguration();
		conf.locale = new Locale( localeName );
		
		setCurrentLanguage( conf.locale.getLanguage() );

		DisplayMetrics metrics = new DisplayMetrics();
		getWindowManager().getDefaultDisplay().getMetrics( metrics );
		
		// the next line just changes the application's locale. It changes this
		// globally and not just in the newly created resource
		Resources res = new Resources( getAssets(), metrics, conf );
		// since we don't need the resource, just allow it to be garbage collected.
		res = null;

		Log.d( TAG, "setLocale: locale set to " + localeName );
	}

	private void setDefaultLocale()
	{
		setLocale( "en" );
	}

	public String getLocalizedString( String name ) {
		//Log.v("VrLocale", "getLocalizedString for " + name );
		String outString = "";
		int id = getResources().getIdentifier( name, "string", getPackageName() );
		if ( id == 0 )
		{
			// 0 is not a valid resource id
			Log.v("VrLocale", name + " is not a valid resource id!!" );
			return outString;
		} 
		if ( id != 0 ) 
		{
			outString = getResources().getText( id ).toString();
			//Log.v("VrLocale", "getLocalizedString resolved " + name + " to " + outString);
		}
		return outString;
	}

	/*
	================================================================================

	Toasts

	================================================================================
	*/
	
	public void clearVrToasts() {
		Log.v(TAG, "clearVrToasts, calling nativePopup" );
		nativePopup(appPtr, 0, 0, -1.0f);
	}

	SurfaceTexture toastTexture;
	Surface toastSurface;

	public void createVrToastOnUiThread(final String text) {
    	runOnUiThread( new Thread()
    	{
		 @Override
    		public void run()
    		{
    			VrActivity.this.createVrToast(text);
            }
    	});
	}
	
	// TODO: pass in the time delay

	// The warning about not calling show is irrelevant -- we are
	// drawing it to a texture
	@SuppressLint("ShowToast")
	public void createVrToast(String text) {
		if ( text == null ) {
			text = "null toast text!";
		}
		Log.v(TAG, "createVrToast " + text);

		// If we haven't set up the surface / surfaceTexture yet,
		// do it now.
		if (toastTexture == null) {
			toastTexture = nativeGetPopupSurfaceTexture(appPtr);
			if (toastTexture == null) {
				Log.e(TAG, "nativePreparePopup returned NULL");
				return; // not set up yet
			}
			toastSurface = new Surface(toastTexture);
		}

		Toast toast = Toast.makeText(this.getApplicationContext(), text,
				Toast.LENGTH_SHORT);

		this.createVrToast( toast.getView() );
	}
	
	public void createVrToast( View toastView )
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
		try {
			Canvas canvas = toastSurface.lockCanvas(null);
			toastView.draw(canvas);
			toastSurface.unlockCanvasAndPost(canvas);
		} catch (Exception t) {
			Log.e(TAG, "lockCanvas threw exception");
		}

		nativePopup(appPtr, toastView.getWidth(), toastView.getHeight(), 7.0f);
	}
	
	/*
	================================================================================

	Misc

	================================================================================
	*/

/*
	public void sendIntent( String packageName, String data ) {
		Log.d(TAG, "sendIntent " + packageName + " : " + data );
		
		Intent launchIntent = getPackageManager().getLaunchIntentForPackage(packageName);
		if ( launchIntent == null )
		{
			Log.d( TAG, "sendIntent null activity" );
			return;
		}
		if ( data.length() > 0 ) {
			launchIntent.setData( Uri.parse( data ) );
		}
		try {
			Log.d(TAG, "startActivity " + launchIntent );
			launchIntent.addFlags( Intent.FLAG_ACTIVITY_NO_ANIMATION );
			startActivity(launchIntent);
		} catch( ActivityNotFoundException e ) {
			Log.d( TAG, "startActivity " + launchIntent + " not found!" );	
			return;
		}
	
		// Make sure we don't leave any background threads running
		// This is not reliable, so it is done with native code.
		// Log.d(TAG, "System.exit" );
		// System.exit( 0 );
	}
*/

	public String getInstalledPackagePath( String packageName )
	{
		Log.d( TAG, "Searching installed packages for '" + packageName + "'" );
		List<ApplicationInfo> appList = getPackageManager().getInstalledApplications( 0 );
		String outString = "";
		for ( ApplicationInfo info : appList )
		{
/*
			if ( info.className != null && info.className.toLowerCase().contains( "oculus" ) )
			{
				Log.d( TAG, "info.className = '" + info.className + "'" );
			}
			else if ( info.sourceDir != null && info.sourceDir.toLowerCase().contains( "oculus" ) )
			{
				Log.d( TAG, "info.sourceDir = '" + info.sourceDir + "'" );
			}
*/
			if ( ( info.className != null && info.className.toLowerCase().contains( packageName ) ) || 
			     ( info.sourceDir != null && info.sourceDir.toLowerCase().contains( packageName ) ) )
			{			
				Log.d( TAG, "Found installed application package " + packageName );
				outString = info.sourceDir;
				return outString;
			}
		}
		return outString;
	}

	// we need this string to track when setLocale has change the language, otherwise if
	// the language is changed with setLocale, we can't determine the current language of
	// the application.
	private static String currentLanguage = null;

	public static void setCurrentLanguage( String lang ) {
		currentLanguage = lang;
		Log.d( TAG, "Current language set to '" + lang + "'." );
	}

	public static String getCurrentLanguage() {
		// In the case of Unity, the activity onCreate does not set the current langage
		// so we need to assume it is defaulted if setLocale() has never been called
		if ( currentLanguage == null || currentLanguage.isEmpty() ) {
			currentLanguage = Locale.getDefault().getLanguage();
		}
		return currentLanguage;
	}

	public static String getDisplayLanguageForLocaleCode( String code )
	{
		Locale locale = new Locale( code );
		return locale.getDisplayLanguage();
	}
	
	public static boolean getBluetoothEnabled( final Activity act ) {
		return Settings.Global.getInt( act.getContentResolver(), 
				Settings.Global.BLUETOOTH_ON, 0 ) != 0;
	}

	public static boolean isWifiConnected( final Activity act ) {
		ConnectivityManager connManager = ( ConnectivityManager ) act.getSystemService( Context.CONNECTIVITY_SERVICE );
		NetworkInfo mWifi = connManager.getNetworkInfo( ConnectivityManager.TYPE_WIFI );
		return mWifi.isConnected();
	}

	public static boolean isAirplaneModeEnabled( final Activity act ) {        
		return Settings.Global.getInt( act.getContentResolver(), 
				Settings.Global.AIRPLANE_MODE_ON, 0 ) != 0;
	}

	// returns true if time settings specifies 24 hour format
	public static boolean isTime24HourFormat( Activity act ) {
		ContentResolver cr = act.getContentResolver();
		String v = Settings.System.getString( cr, android.provider.Settings.System.TIME_12_24 );
		if ( v == null || v.isEmpty() || v.equals( "12" ) ) {
			return false;
		}
		return true;
	}
	
	public static boolean isHybridApp( final Activity act ) {
		try {
		    ApplicationInfo appInfo = act.getPackageManager().getApplicationInfo(act.getPackageName(), PackageManager.GET_META_DATA);
		    Bundle bundle = appInfo.metaData;
		    String applicationMode = bundle.getString("com.samsung.android.vr.application.mode");
		    return (applicationMode.equals("dual"));
		} catch( NameNotFoundException e ) {
			e.printStackTrace();
		} catch( NullPointerException e ) {
		    Log.e(TAG, "Failed to load meta-data, NullPointer: " + e.getMessage());         
		} 
		
		return false;
	}

	public static String getExternalStorageDirectory() {
		return Environment.getExternalStorageDirectory().getAbsolutePath();
	}
	
	// Converts some thing like "/sdcard" to "/sdcard/", always ends with "/" to indicate folder path
	public static String toFolderPathFormat( String inStr ) {
		if( inStr == null ||
			inStr.length() == 0	)
		{
			return "/";
		}
		
		if( inStr.charAt( inStr.length() - 1 ) != '/' )
		{
			return inStr + "/";
		}
		
		return inStr;
	}
	
	/*** Internal Storage ***/
	public static String getInternalStorageRootDir() {
		return toFolderPathFormat( Environment.getDataDirectory().getPath() );
	}
	
	public static String getInternalStorageFilesDir( Activity act ) {
		if ( act != null )
		{
			return toFolderPathFormat( act.getFilesDir().getPath() );
		}
		else
		{
			Log.e( TAG, "Activity is null in getInternalStorageFilesDir method" );
		}
		return "";
	}
	
	public static String getInternalStorageCacheDir( Activity act ) {
		if ( act != null )
		{
			return toFolderPathFormat( act.getCacheDir().getPath() );
		}
		else
		{
			Log.e( TAG, "activity is null getInternalStorageCacheDir method" );
		}
		return "";
	}

	public static long getInternalCacheMemoryInBytes( Activity act )
	{
		if ( act != null )
		{
			String path = getInternalStorageCacheDir( act );
			StatFs stat = new StatFs( path );
			return stat.getAvailableBytes();
		}
		else
		{
			Log.e( TAG, "activity is null getInternalCacheMemoryInBytes method" );
		}
		return 0;
	}
	
	/*** External Storage ***/
	public static String getExternalStorageFilesDirAtIdx( Activity act, int idx ) {
		if ( act != null )
		{
			File[] filesDirs = act.getExternalFilesDirs(null);
			if( filesDirs != null && filesDirs.length > idx && filesDirs[idx] != null )
			{
				return toFolderPathFormat( filesDirs[idx].getPath() );
			}
		}
		else
		{
			Log.e( TAG, "activity is null getExternalStorageFilesDirAtIdx method" );
		}
		return "";
	}
	
	public static String getExternalStorageCacheDirAtIdx( Activity act, int idx ) {
		if ( act != null )
		{
			File[] cacheDirs = act.getExternalCacheDirs();
			if( cacheDirs != null && cacheDirs.length > idx && cacheDirs[idx] != null )
			{
				return toFolderPathFormat( cacheDirs[idx].getPath() );
			}
		}
		else
		{
			Log.e( TAG, "activity is null in getExternalStorageCacheDirAtIdx method with index " + idx );
		}
		return "";
	}
	
	// Primary External Storage
	public static final int PRIMARY_EXTERNAL_STORAGE_IDX = 0;
	public static String getPrimaryExternalStorageRootDir( Activity act ) {
		return toFolderPathFormat( Environment.getExternalStorageDirectory().getPath() );
	}
	
	public static String getPrimaryExternalStorageFilesDir( Activity act ) {
		return getExternalStorageFilesDirAtIdx( act, PRIMARY_EXTERNAL_STORAGE_IDX );
	}
	
	public static String getPrimaryExternalStorageCacheDir( Activity act ) {
		return getExternalStorageCacheDirAtIdx( act, PRIMARY_EXTERNAL_STORAGE_IDX );
	}

	// Secondary External Storage
	public static final int SECONDARY_EXTERNAL_STORAGE_IDX = 1;
	public static String getSecondaryExternalStorageRootDir() {
		return "/storage/extSdCard/";
	}
	
	public static String getSecondaryExternalStorageFilesDir( Activity act ) {
		return getExternalStorageFilesDirAtIdx( act, SECONDARY_EXTERNAL_STORAGE_IDX );
	}
	
	public static String getSecondaryExternalStorageCacheDir( Activity act ) {
		return getExternalStorageCacheDirAtIdx( act, SECONDARY_EXTERNAL_STORAGE_IDX );
	}
}
