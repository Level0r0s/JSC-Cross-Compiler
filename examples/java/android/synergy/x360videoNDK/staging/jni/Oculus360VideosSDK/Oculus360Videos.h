/************************************************************************************

Filename    :   Oculus360Videos.h
Content     :   Panorama viewer based on SwipeView
Created     :   February 14, 2014
Authors     :   John Carmack

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the Oculus360Videos/ directory. An additional grant 
of patent rights can be found in the PATENTS file in the same directory.

************************************************************************************/

#ifndef OVR_Oculus360Videos_h
#define OVR_Oculus360Videos_h

#include "jni.h"
#ifndef __cplusplus

// define jsc callable methods here
void startMovieFromUDP(JNIEnv *jni, void* interfacePtr, const char* pathName);


#else

extern "C" {
	// expected 'char *' but argument is of type 'const char *'
void startMovieFromUDP(JNIEnv *jni, void* interfacePtr, const char* pathName);
}

#include "App.h"
#include "Fader.h"
#include "SceneView.h"
#include "SoundEffectContext.h"
#include <memory>
#include "GuiSys.h"

namespace OVR
{

class OvrVideosMetaData;
class OvrMetaData;
struct OvrMetaDatum;
class VideoBrowser;
class OvrVideoMenu;

enum Action
{
	ACT_NONE,
	ACT_LAUNCHER,
	ACT_STILLS,
	ACT_VIDEOS,
};


class Oculus360Videos : public OVR::VrAppInterface
{
public:
	// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/startmoviefromudp
	OvrVideosMetaData *	MetaData;

	enum OvrMenuState
	{
		MENU_NONE,
		MENU_BROWSER,
		MENU_VIDEO_LOADING,
		MENU_VIDEO_READY,
		MENU_VIDEO_PLAYING,
		MENU_VIDEO_PAUSE,
		MENU_VIDEO_RESUME,
		NUM_MENU_STATES
	};

						Oculus360Videos();
	virtual				~Oculus360Videos();

	virtual void		Configure( ovrSettings & settings );
	virtual void		OneTimeInit( const char * fromPackage, const char * launchIntentJSON, const char * launchIntentURI );
	virtual void		OneTimeShutdown();
	virtual void		EnteredVrMode();
	virtual void		LeavingVrMode();
	virtual bool 		OnKeyEvent( const int keyCode, const int repeatCount, const KeyEventType eventType );
	virtual Matrix4f 	Frame( const VrFrame & vrFrame );
	virtual Matrix4f 	DrawEyeView( const int eye, const float fovDegreesX, const float fovDegreesY, ovrFrameParms & frameParms );

	ovrMessageQueue &	GetMessageQueue() { return MessageQueue; }

	void 				StopVideo();
	void 				PauseVideo( bool const force );
	void 				ResumeVideo();
	bool				IsVideoPlaying() const;

	void 				StartVideo( const double nowTime );
	void				SeekTo( const int seekPos );

	Matrix4f			TexmForVideo( const int eye );
	Matrix4f			TexmForBackground( const int eye );

	void				SetMenuState( const OvrMenuState state );
	OvrMenuState		GetCurrentState() const				{ return  MenuState; }

	void				SetFrameAvailable( bool const a ) { FrameAvailable = a; }

	void				OnVideoActivated( const OvrMetaDatum * videoData );
	const OvrMetaDatum * GetActiveVideo()	{ return ActiveVideo;  }
	float				GetFadeLevel()		{ return CurrentFadeLevel; }

	class ovrLocale &	GetLocale() { return *Locale; }

private:
	void				Command( const char * msg );
	const char*			MenuStateString( const OvrMenuState state );

	// shared vars
	ovrSoundEffectContext * SoundEffectContext;
	OvrGuiSys::SoundEffectPlayer * SoundEffectPlayer;
	OvrGuiSys *			GuiSys;
	class ovrLocale *	Locale;
	jclass				MainActivityClass;	// need to look up from main thread
	ovrMessageQueue		MessageQueue;
	GlGeometry			Globe;
	OvrSceneView		Scene;
	ModelFile *			BackgroundScene;

	// panorama vars
	GLuint				BackgroundTexId;
	GlProgram			PanoramaProgram;
	GlProgram			FadedPanoramaProgram;
	GlProgram			SingleColorTextureProgram;

	Array< String > 	SearchPaths;
	//OvrVideosMetaData *	MetaData;
	VideoBrowser *		Browser;
	OvrVideoMenu *		VideoMenu;
	const OvrMetaDatum * ActiveVideo;
	OvrMenuState		MenuState;
	SineFader			Fader;
	const float			FadeOutRate;
	const float			VideoMenuVisibleTime;
	float				CurrentFadeRate;
	float				CurrentFadeLevel;	
	float				VideoMenuTimeLeft;

	bool				UseSrgb;

	// video vars
	String				VideoName;
	SurfaceTexture	* 	MovieTexture;

	// Set when MediaPlayer knows what the stream size is.
	// current is the aspect size, texture may be twice as wide or high for 3D content.
	int					CurrentVideoWidth;	// set to 0 when a new movie is started, don't render until non-0
	int					CurrentVideoHeight;

	int					BackgroundWidth;
	int					BackgroundHeight;

	bool				FrameAvailable;
	bool 				VideoPaused;
	bool 				HmdMounted;
};

} // namespace OVR

#endif	// __cplusplus
#endif // OVR_Oculus360Videos_h
