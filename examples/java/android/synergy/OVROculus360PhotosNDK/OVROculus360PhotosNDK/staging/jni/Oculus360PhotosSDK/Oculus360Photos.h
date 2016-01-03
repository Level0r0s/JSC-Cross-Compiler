/************************************************************************************

Filename    :   Oculus360Photos.h
Content     :   360 Panorama Viewer
Created     :   August 13, 2014
Authors     :   John Carmack, Warsam Osman

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the Oculus360Photos/ directory. An additional grant 
of patent rights can be found in the PATENTS file in the same directory.

************************************************************************************/

#ifndef OCULUS360PHOTOS_H
#define OCULUS360PHOTOS_H

#include "jni.h"
#ifndef __cplusplus
// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/startmoviefromudp
// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150721/ovroculus360photoshud
// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150704/ovroculus360photoshud
// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150611/ovroculus360photos
// jsc is not generating c++ but is generating c and java

// can we call C++ from C?
// did we update Android.mk
 
//jni/OVROculus360PhotosNDK.dll.c:77: error: undefined reference to 'Java_com_oculus_oculus360photossdk_MainActivity_nativeSetAppInterface'
//collect2.exe: error: ld returned 1 exit status

 long Java_com_oculus_oculus360photossdk_MainActivity_nativeSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity,
						 jstring fromPackageName, jstring commandString, jstring uriString, void* arg_AtStartBackgroundPanoLoad);

 //#error we just defined it?

#else

extern "C" {
long Java_com_oculus_oculus360photossdk_MainActivity_nativeSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity,
						 jstring fromPackageName, jstring commandString, jstring uriString, void* arg_AtStartBackgroundPanoLoad);
}





#include "ModelView.h"
#include "VRMenu/Fader.h"
#include "Kernel/OVR_Lockless.h"

namespace OVR
{

class PanoBrowser;
class OvrPanoMenu;
class OvrMetaData;
struct OvrMetaDatum;
class OvrPhotosMetaData;
struct OvrPhotosMetaDatum;

class Oculus360Photos : public VrAppInterface
{
public:
	enum OvrMenuState
	{
		MENU_NONE,
		MENU_BACKGROUND_INIT,
		MENU_BROWSER,
		MENU_PANO_LOADING,
		MENU_PANO_FADEIN,
		MENU_PANO_REOPEN_FADEIN,
		MENU_PANO_FULLY_VISIBLE,
		MENU_PANO_FADEOUT,
		NUM_MENU_STATES
	};

	class DoubleBufferedTextureData
	{
	public:
		DoubleBufferedTextureData();
		~DoubleBufferedTextureData();

		// Returns the current texid to render
		GLuint		GetRenderTexId() const;

		// Returns the free texid for load
		GLuint		GetLoadTexId() const;

		// Set the texid after creating a new texture.
		void		SetLoadTexId( const GLuint texId );

		// Swaps the buffers
		void		Swap();

		// Update the last loaded size
		void		SetSize( const int width, const int height );

		// Return true if passed in size match the load index size
		bool		SameSize( const int width, const int height ) const;

	private:
		GLuint			TexId[ 2 ];
		int				Width[ 2 ];
		int				Height[ 2 ];
		volatile int	CurrentIndex;
	};

						Oculus360Photos();
	virtual				~Oculus360Photos();

	virtual void		Configure( ovrSettings & settings );
	virtual void		OneTimeInit( const char * fromPackage, const char * launchIntentJSON, const char * launchIntentURI );
	virtual void		OneTimeShutdown();
	virtual bool 		OnKeyEvent( const int keyCode, const int repeatCount, const KeyEventType eventType );
	virtual Matrix4f 	Frame( const VrFrame & vrFrame );
	virtual Matrix4f 	DrawEyeView( const int eye, const float fovDegrees );

	ovrMessageQueue &	GetMessageQueue() { return MessageQueue; }

	void				OnPanoActivated( const OvrMetaDatum * panoData );
	PanoBrowser *		GetBrowser()										{ return Browser; }
	OvrPhotosMetaData *	GetMetaData()										{ return MetaData; }
	const OvrPhotosMetaDatum * GetActivePano() const						{ return ActivePano; }
	void				SetActivePano( const OvrPhotosMetaDatum * data )	{ OVR_ASSERT( data );  ActivePano = data; }
	float				GetFadeLevel() const								{ return CurrentFadeLevel;  }
	int					GetNumPanosInActiveCategory( OvrGuiSys & guiSys ) const;

	void				SetMenuState( const OvrMenuState state );
	OvrMenuState		GetCurrentState() const								{ return  MenuState; }

	int					ToggleCurrentAsFavorite();

	bool				GetUseOverlay() const;
	bool				AllowPanoInput() const;
	ovrMessageQueue &	GetBGMessageQueue() { return BackgroundCommands;  }
	
private:
	// Background textures loaded into GL by background thread using shared context
	void				Command( const char * msg );
	static void *		BackgroundGLLoadThread( void * v );
	void				StartBackgroundPanoLoad( const char * filename );
	const char *		MenuStateString( const OvrMenuState state );
	bool 				LoadMetaData( const char * metaFile );
	void				LoadRgbaCubeMap( const int resolution, const unsigned char * const rgba[ 6 ], const bool useSrgbFormat );
	void				LoadRgbaTexture( const unsigned char * data, int width, int height, const bool useSrgbFormat );

	OvrGuiSys *			GuiSys;

	// shared vars
	jclass				mainActivityClass;	// need to look up from main thread
	GlGeometry			Globe;

	OvrSceneView		Scene;
	SineFader			Fader;
	
	// Pano data and menus
	Array< String > 			SearchPaths;
	OvrPhotosMetaData *			MetaData;
	OvrPanoMenu *				PanoMenu;
	PanoBrowser *				Browser;
	const OvrPhotosMetaDatum *	ActivePano;
	String						StartupPano;

	// panorama vars
	DoubleBufferedTextureData	BackgroundPanoTexData;
	DoubleBufferedTextureData	BackgroundCubeTexData;
	bool				CurrentPanoIsCubeMap;

	GlProgram			TexturedMvpProgram;
	GlProgram			CubeMapPanoProgram;
	GlProgram			PanoramaProgram;

	ovrMessageQueue		MessageQueue;
	VrFrame				FrameInput;
	OvrMenuState		MenuState;

	const float			FadeOutRate;
	const float			FadeInRate;
	const float			PanoMenuVisibleTime;
	float				CurrentFadeRate;
	float				CurrentFadeLevel;	
	float				PanoMenuTimeLeft;
	float				BrowserOpenTime;

	bool				UseOverlay;				// use the TimeWarp environment overlay
	bool				UseSrgb;
	
	// Background texture commands produced by FileLoader consumed by BackgroundGLLoadThread
	ovrMessageQueue		BackgroundCommands;

	// The background loader loop will exit when this is set true.
	LocklessUpdater<bool>		ShutdownRequest;

	// BackgroundGLLoadThread private GL context used for loading background textures
	EGLint				EglClientVersion;
	EGLDisplay			EglDisplay;
	EGLConfig			EglConfig;
	EGLSurface			EglPbufferSurface;
	EGLContext			EglShareContext;
};

} // namespace OVR

#endif // OCULUS360PHOTOS_H
#endif 