/************************************************************************************

Filename    :   Oculus360Videos.cpp
Content     :
Created     :
Authors     :

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the Oculus360Videos/ directory. An additional grant
of patent rights can be found in the PATENTS file in the same directory.

*************************************************************************************/

#include <sys/stat.h>
#include <sys/types.h>
#include <dirent.h>
#include <jni.h>
#include "Input.h"

#include "Kernel/OVR_Math.h"
#include "Kernel/OVR_TypesafeNumber.h"
#include "Kernel/OVR_Array.h"
#include "Kernel/OVR_String.h"
#include "Kernel/OVR_String_Utils.h"
#include "Kernel/OVR_GlUtils.h"

#include "GlTexture.h"
#include "BitmapFont.h"
#include "GazeCursor.h"
#include "App.h"
#include "Oculus360Videos.h"
#include "GuiSys.h"
#include "ImageData.h"
#include "PackageFiles.h"
#include "Fader.h"
#include "stb_image.h"
#include "stb_image_write.h"
#include "VrCommon.h"

#include "VideoBrowser.h"
#include "VideoMenu.h"
#include "OVR_Locale.h"
#include "PathUtils.h"

#include "VideosMetaData.h"
//Z:\jsc.svn\examples\java\android\synergy\x360videoNDK\staging\jni\Oculus360VideosSDK\VideosMetaData.h

static bool	RetailMode = false;

static const char * videosDirectory = "Oculus/360Videos/";
static const char * videosLabel = "@string/app_name";
static const float	FadeOutTime = 0.25f;
static const float	FadeOverTime = 1.0f;

OVR::OvrMetaDatum * startMovieFromUDP_yield;

extern "C" {

void startMovieFromUDP(JNIEnv *jni, void* interfacePtr, const char* pathName)
{
	LOG( "enter startMovieFromUDP");

	// can we print out the menu we have in c++. its been a while we did c++...
	// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/startmoviefromudp
	OVR::Oculus360Videos * videos = static_cast< OVR::Oculus360Videos * >( ( ( OVR::App * )interfacePtr )->GetAppInterface() );

	LOG( "enter startMovieFromUDP. got videos");

	//VideoBrowser *		Browser;
	//OvrVideoMenu *		VideoMenu;
	//const OvrMetaDatum * ActiveVideo;

//// Called when a panel is activated
//	virtual void OnPanelActivated( OvrGuiSys & guiSys, const OvrMetaDatum * panelData );



	// how can we inspect the menu?

	//void OvrMetaData::InitFromDirectory( const char * relativePath, const Array< String > & searchPaths, const OvrMetaDataFileExtensions & fileExtensions )
	// MetaData->InitFromDirectory( videosDirectory, SearchPaths, fileExtensions );
	// X:\opensource\ovr_sdk_mobile_1.0.0.0\VrAppSupport\VrGUI\Src\MetaDataManager.cpp

	// OvrMetaDatum * datum = CreateMetaDatum( fileBase.ToCStr() );

	//MetaData.PushBack( datum );
	//LOG( "OvrMetaData adding datum %s with index %d to %s", datum->Url.ToCStr(), dataIndex, currentCategory.CategoryTag.ToCStr() );

	// all cool. PushBack adds the menu.
	// X:\opensource\ovr_sdk_mobile_1.0.0.0\VrAppSupport\VrGUI\Src\MetaDataManager.h

//jni/Oculus360VideosSDK/Oculus360Videos.cpp:97:2: error: 'OvrVideosMetaData' was not declared in this scope
//  OvrVideosMetaData * this_MetaData = videos->MetaData;
//  ^

//error: 'OVR::OvrVideosMetaData* OVR::Oculus360Videos::MetaData' is private
// class OvrVideosMetaData : public OvrMetaData
	// const Array< OvrMetaDatum * > &		GetMetaData() const 							{ return MetaData; }

	// lets use that getter?

	// 'class OVR::Oculus360Videos' has no member named 'GetMetaData'
	//OVR::OvrVideosMetaData * this_MetaData = videos->GetMetaData();

	//LOG( "startMovieFromUDP. videos->MetaData");
	OVR::OvrVideosMetaData * this_MetaData = videos->MetaData;

//jni/VrAppSupport/VrGUI/Src/MetaDataManager.h:129:28: error: 'OVR::Array<OVR::OvrMetaDatum*>& OVR::OvrMetaData::GetMetaData()' is protected
//  Array< OvrMetaDatum * > & GetMetaData()            { return MetaData; }
//                            ^


	 //Array< OvrMetaDatum * >	this_MetaData_MetaData = this_MetaData->GetMetaData();
	 OVR::Array< OVR::OvrMetaDatum * >	this_MetaData_MetaData = this_MetaData->MetaData;

	//LOG( "startMovieFromUDP. before");

	// how to iterate??

	for ( int i = 0; i < this_MetaData_MetaData.GetSizeI(); i++ )
	{
		//LOG( "startMovieFromUDP. datum #%i", i);

		OVR::OvrMetaDatum *  datum = this_MetaData_MetaData[i];

		// error: request for member 'Url' in 'datum', which is of pointer type 'OVR::OvrMetaDatum*' (maybe you meant to use '->' ?)


		// how to compare strings??
		// bool        operator == (const char* str) const

		if (datum->Url == pathName)
		{
			LOG( "startMovieFromUDP #%i OvrMetaData  { url = %s }", i, datum->Url.ToCStr() );
			startMovieFromUDP_yield = datum;

			// X:\opensource\ovr_sdk_mobile_1.0.0.0\VrAppSupport\VrGUI\Src\VRMenuEventHandler.cpp
			// VRMenuEvent event( VRMENU_EVENT_TOUCH_UP, EVENT_DISPATCH_FOCUS, FocusedHandle, Vector3f( vrFrame.Input.touchRelative, 0.0f ), result );
			// events.PushBack( event );

			// wrong thread.
			// videos->OnVideoActivated( datum );

			// how can we activate that menu???

			// done..
			break;
		}
		else
		{
		LOG( "startMovieFromUDP #%i skip OvrMetaData  { url = %s }", i, datum->Url.ToCStr() );
		}

//struct OvrMetaDatum
//{
//	mutable int				FolderIndex;	// index of the folder this meta data appears in (not serialized!)
//	mutable int				PanelId;		// panel id associated with this meta data (not serialized!)
//	int						Id;				// index into the array read from the JSON (not serialized!)
//	Array< String >			Tags;
//	String					Url;
//
//protected:
//	OvrMetaDatum() {}
//};


		// call?
		//	virtual void OnPanelActivated( OvrGuiSys & guiSys, const OvrMetaDatum * panelData );



	}

	LOG( "exit startMovieFromUDP.");
}

static jclass GlobalActivityClass;

long Java_com_oculus_oculus360videossdk_MainActivity_nativeSetAppInterface( JNIEnv *jni, jclass clazz, jobject activity,
		jstring fromPackageName, jstring commandString, jstring uriString )
{
	// This is called by the java UI thread.


    //          var type = env.GetObjectClass(env, args);
	//GlobalActivityClass = (jclass)jni->NewGlobalRef( clazz );
	GlobalActivityClass = (jclass)jni->NewGlobalRef(jni->GetObjectClass( activity ));

	LOG( "nativeSetAppInterface");
	return (new OVR::Oculus360Videos())->SetActivity( jni, clazz, activity, fromPackageName, commandString, uriString );
}

void Java_com_oculus_oculus360videossdk_MainActivity_nativeFrameAvailable( JNIEnv *jni, jclass clazz, jlong interfacePtr )
{
	OVR::Oculus360Videos * videos = static_cast< OVR::Oculus360Videos * >( ( ( OVR::App * )interfacePtr )->GetAppInterface() );
	videos->SetFrameAvailable( true );
}

jobject Java_com_oculus_oculus360videossdk_MainActivity_nativePrepareNewVideo( JNIEnv *jni, jclass clazz, jlong interfacePtr )
{
	// set up a message queue to get the return message
	// TODO: make a class that encapsulates this work
	OVR::ovrMessageQueue result( 1 );
	OVR::Oculus360Videos * videos = static_cast< OVR::Oculus360Videos * >( ( ( OVR::App * )interfacePtr )->GetAppInterface() );
	videos->GetMessageQueue().PostPrintf( "newVideo %p", &result );

	result.SleepUntilMessage();
	const char * msg = result.GetNextMessage();
	jobject	texobj;
	sscanf( msg, "surfaceTexture %p", &texobj );
	free( ( void * )msg );

	return texobj;
}

void Java_com_oculus_oculus360videossdk_MainActivity_nativeSetVideoSize( JNIEnv *jni, jclass clazz, jlong interfacePtr, int width, int height )
{
	LOG( "nativeSetVideoSizes: width=%i height=%i", width, height );

	OVR::Oculus360Videos * videos = static_cast< OVR::Oculus360Videos * >( ( ( OVR::App * )interfacePtr )->GetAppInterface() );
	videos->GetMessageQueue().PostPrintf( "video %i %i", width, height );
}

void Java_com_oculus_oculus360videossdk_MainActivity_nativeVideoCompletion( JNIEnv *jni, jclass clazz, jlong interfacePtr )
{
	LOG( "nativeVideoCompletion" );

	OVR::Oculus360Videos * videos = static_cast< OVR::Oculus360Videos * >( ( ( OVR::App * )interfacePtr )->GetAppInterface() );
	videos->GetMessageQueue().PostPrintf( "completion" );
}

void Java_com_oculus_oculus360videossdk_MainActivity_nativeVideoStartError( JNIEnv *jni, jclass clazz, jlong interfacePtr )
{
	LOG( "nativeVideoStartError" );

	OVR::Oculus360Videos * videos = static_cast< OVR::Oculus360Videos * >( ( ( OVR::App * )interfacePtr )->GetAppInterface() );
	videos->GetMessageQueue().PostPrintf( "startError" );
}

} // extern "C"

namespace OVR
{

//==============================================================
// ovrGuiSoundEffectPlayer
class ovrGuiSoundEffectPlayer : public OvrGuiSys::SoundEffectPlayer
{
public:
	ovrGuiSoundEffectPlayer( ovrSoundEffectContext & context )
		: SoundEffectContext( context )
	{
	}

	virtual bool Has( const char * name ) const OVR_OVERRIDE { return SoundEffectContext.GetMapping().HasSound( name ); }
	virtual void Play( const char * name ) OVR_OVERRIDE { SoundEffectContext.Play( name ); }

private:
	ovrSoundEffectContext & SoundEffectContext;
};

Oculus360Videos::Oculus360Videos()
	: SoundEffectContext( NULL )
	, SoundEffectPlayer( NULL )
	, GuiSys( OvrGuiSys::Create() )
	, Locale( NULL )
	, MainActivityClass( GlobalActivityClass )
	, MessageQueue( 100 )
	, BackgroundScene( NULL )
	, BackgroundTexId( 0 )
	, MetaData( NULL )
	, Browser( NULL )
	, VideoMenu( NULL )
	, ActiveVideo( NULL )
	, MenuState( MENU_NONE )
	, Fader( 1.0f )
	, FadeOutRate( 1.0f / 0.5f )
	, VideoMenuVisibleTime( 5.0f )
	, CurrentFadeRate( FadeOutRate )
	, CurrentFadeLevel( 1.0f )
	, VideoMenuTimeLeft( 0.0f )
	, UseSrgb( false )
	, MovieTexture( NULL )
	, CurrentVideoWidth( 0 )
	, CurrentVideoHeight( 480 )
	, BackgroundWidth( 0 )
	, BackgroundHeight( 0 )
	, FrameAvailable( false )
	, VideoPaused( false )
	, HmdMounted( true )
{
}

Oculus360Videos::~Oculus360Videos()
{
	OvrGuiSys::Destroy( GuiSys );
}

void Oculus360Videos::Configure( ovrSettings & settings )
{
	// We need very little CPU for pano browsing, but a fair amount of GPU.
	settings.PerformanceParms.CpuLevel = 1;
	settings.PerformanceParms.GpuLevel = 0;

	// When the app is throttled, go to the platform UI and display a
	// dismissable warning. On return to the app, force 30Hz timewarp.
	settings.ModeParms.AllowPowerSave = true;

	// We could disable the srgb convert on the FBO. but this is easier
	settings.EyeBufferParms.colorFormat = UseSrgb ? COLOR_8888_sRGB : COLOR_8888;
	settings.EyeBufferParms.depthFormat = DEPTH_16;
	// All geometry is blended, so save power with no MSAA
	settings.EyeBufferParms.multisamples = 1;
}

void Oculus360Videos::OneTimeInit( const char * fromPackage, const char * launchIntentJSON, const char * launchIntentURI )
{
	// This is called by the VR thread, not the java UI thread.
	LOG( "--------------- Oculus360Videos OneTimeInit ---------------" );

	const ovrJava * java = app->GetJava();
	SoundEffectContext = new ovrSoundEffectContext( *java->Env, java->ActivityObject );
	SoundEffectContext->Initialize();
	SoundEffectPlayer = new ovrGuiSoundEffectPlayer( *SoundEffectContext );

	Locale = ovrLocale::Create( *app, "default" );

	String fontName;
	GetLocale().GetString( "@string/font_name", "efigs.fnt", fontName );
	GuiSys->Init( this->app, *SoundEffectPlayer, fontName.ToCStr(), &app->GetDebugLines() );

	GuiSys->GetGazeCursor().ShowCursor();

	RetailMode = FileExists( "/sdcard/RetailMedia" );

	PanoramaProgram = BuildProgram(
			"uniform highp mat4 Mvpm;\n"
			"uniform highp mat4 Texm;\n"
			"attribute vec4 Position;\n"
			"attribute vec2 TexCoord;\n"
			"varying  highp vec2 oTexCoord;\n"
			"void main()\n"
			"{\n"
			"   gl_Position = Mvpm * Position;\n"
			"   oTexCoord = vec2( Texm * vec4( TexCoord, 0, 1 ) );\n"
			"}\n"
		,
			"#extension GL_OES_EGL_image_external : require\n"
			"uniform samplerExternalOES Texture0;\n"
			"uniform lowp vec4 UniformColor;\n"
			"uniform lowp vec4 ColorBias;\n"
			"varying highp vec2 oTexCoord;\n"
			"void main()\n"
			"{\n"
			"	gl_FragColor = ColorBias + UniformColor * texture2D( Texture0, oTexCoord );\n"
			"}\n"
		);

	FadedPanoramaProgram = BuildProgram(
			"uniform highp mat4 Mvpm;\n"
			"uniform highp mat4 Texm;\n"
			"attribute vec4 Position;\n"
			"attribute vec2 TexCoord;\n"
			"varying  highp vec2 oTexCoord;\n"
			"void main()\n"
			"{\n"
			"   gl_Position = Mvpm * Position;\n"
			"   oTexCoord = vec2( Texm * vec4( TexCoord, 0, 1 ) );\n"
			"}\n"
		,
			"#extension GL_OES_EGL_image_external : require\n"
			"uniform samplerExternalOES Texture0;\n"
			"uniform sampler2D Texture1;\n"
			"uniform lowp vec4 UniformColor;\n"
			"varying highp vec2 oTexCoord;\n"
			"void main()\n"
			"{\n"
			"	lowp vec4 staticColor = texture2D( Texture1, oTexCoord );\n"
			"	lowp vec4 movieColor = texture2D( Texture0, oTexCoord );\n"
			"	gl_FragColor = UniformColor * mix( movieColor, staticColor, staticColor.w );\n"
			"}\n"
		);

	SingleColorTextureProgram = BuildProgram(
			"uniform highp mat4 Mvpm;\n"
			"attribute highp vec4 Position;\n"
			"attribute highp vec2 TexCoord;\n"
			"varying highp vec2 oTexCoord;\n"
			"void main()\n"
			"{\n"
			"   gl_Position = Mvpm * Position;\n"
			"   oTexCoord = TexCoord;\n"
			"}\n"
		,
			"uniform sampler2D Texture0;\n"
			"uniform lowp vec4 UniformColor;\n"
			"varying highp vec2 oTexCoord;\n"
			"void main()\n"
			"{\n"
			"   gl_FragColor = UniformColor * texture2D( Texture0, oTexCoord );\n"
			"}\n"
		);

	const char *launchPano = NULL;
	if ( ( NULL != launchPano ) && launchPano[ 0 ] )
	{
		BackgroundTexId = LoadTextureFromBuffer( launchPano, MemBufferFile( launchPano ),
			TextureFlags_t( TEXTUREFLAG_NO_DEFAULT ) | TEXTUREFLAG_USE_SRGB, BackgroundWidth, BackgroundHeight );
	}

	// always fall back to valid background
	if ( BackgroundTexId == 0 )
	{
		BackgroundTexId = LoadTextureFromApplicationPackage( "assets/background.jpg",
			TextureFlags_t( TEXTUREFLAG_USE_SRGB ), BackgroundWidth, BackgroundHeight );
	}

	LOG( "Creating Globe" );
	Globe = BuildGlobe();

	// Stay exactly at the origin, so the panorama globe is equidistant
	// Don't clear the head model neck length, or swipe view panels feel wrong.
	ovrHeadModelParms headModelParms = app->GetHeadModelParms();
	headModelParms.EyeHeight = 0.0f;
	app->SetHeadModelParms( headModelParms );

	MaterialParms materialParms;
	materialParms.UseSrgbTextureFormats = UseSrgb;

	BackgroundScene = LoadModelFileFromApplicationPackage( "assets/stars.ovrscene",
		Scene.GetDefaultGLPrograms(),
		materialParms );

	Scene.SetWorldModel( *BackgroundScene );

	// Load up meta data from videos directory
	MetaData = new OvrVideosMetaData();
	if ( MetaData == NULL )
	{
		FAIL( "Oculus360Photos::OneTimeInit failed to create MetaData" );
	}

	const OvrStoragePaths & storagePaths = app->GetStoragePaths();
	storagePaths.PushBackSearchPathIfValid( EST_SECONDARY_EXTERNAL_STORAGE, EFT_ROOT, "RetailMedia/", SearchPaths );
	storagePaths.PushBackSearchPathIfValid( EST_SECONDARY_EXTERNAL_STORAGE, EFT_ROOT, "", SearchPaths );
	storagePaths.PushBackSearchPathIfValid( EST_PRIMARY_EXTERNAL_STORAGE, EFT_ROOT, "RetailMedia/", SearchPaths );
	storagePaths.PushBackSearchPathIfValid( EST_PRIMARY_EXTERNAL_STORAGE, EFT_ROOT, "", SearchPaths );

	// OvrVideosMetaData *	MetaData;
	// Array< String > 	SearchPaths;
	//LOG( " using %d storagePaths", storagePaths.GetSizeI() );
	LOG( " using %d SearchPaths", SearchPaths.GetSizeI() );

	OvrMetaDataFileExtensions fileExtensions;
	fileExtensions.GoodExtensions.PushBack( ".mp4" );
	fileExtensions.GoodExtensions.PushBack( ".m4v" );
	fileExtensions.GoodExtensions.PushBack( ".3gp" );
	fileExtensions.GoodExtensions.PushBack( ".3g2" );
	fileExtensions.GoodExtensions.PushBack( ".ts" );
	fileExtensions.GoodExtensions.PushBack( ".webm" );
	fileExtensions.GoodExtensions.PushBack( ".mkv" );
	fileExtensions.GoodExtensions.PushBack( ".wmv" );
	fileExtensions.GoodExtensions.PushBack( ".asf" );
	fileExtensions.GoodExtensions.PushBack( ".avi" );
	fileExtensions.GoodExtensions.PushBack( ".flv" );

	LOG( "MetaData->InitFromDirectory" );

	MetaData->InitFromDirectory( videosDirectory, SearchPaths, fileExtensions );

	String localizedAppName;
	GetLocale().GetString( videosLabel, videosLabel, localizedAppName );
	//MetaData->RenameCategory( ExtractFileBase( videosDirectory ).ToCStr(), localizedAppName.ToCStr() );

	// Start building the VideoMenu

	LOG( "GuiSys->GetMenu" );


	VideoMenu = ( OvrVideoMenu * )GuiSys->GetMenu( OvrVideoMenu::MENU_NAME );
	if ( VideoMenu == NULL )
	{
		LOG( "OvrVideoMenu::Create" );
		VideoMenu = OvrVideoMenu::Create( *GuiSys, *MetaData, 1.0f, 2.0f );
		OVR_ASSERT( VideoMenu );

		GuiSys->AddMenu( VideoMenu );
	}

	VideoMenu->SetFlags( VRMenuFlags_t( VRMENU_FLAG_PLACE_ON_HORIZON ) | VRMENU_FLAG_SHORT_PRESS_HANDLED_BY_APP );

	// Start building the FolderView
	Browser = ( VideoBrowser * )GuiSys->GetMenu( OvrFolderBrowser::MENU_NAME );
	if ( Browser == NULL )
	{
		Browser = VideoBrowser::Create(
			*this,
			*GuiSys,
			*MetaData,
			256, 20.0f,
			256, 200.0f,
			7,
			5.4f );
		OVR_ASSERT( Browser );

		GuiSys->AddMenu( Browser );
	}

	Browser->SetFlags( VRMenuFlags_t( VRMENU_FLAG_PLACE_ON_HORIZON ) | VRMENU_FLAG_BACK_KEY_EXITS_APP );
	Browser->SetFolderTitleSpacingScale( 0.37f );
	Browser->SetPanelTextSpacingScale( 0.34f );
	Browser->SetScrollBarSpacingScale( 0.9f );
	Browser->SetScrollBarRadiusScale( 1.0f );

	Browser->OneTimeInit( *GuiSys );
	Browser->BuildDirtyMenu( *GuiSys, *MetaData );

	SetMenuState( MENU_BROWSER );
}

void Oculus360Videos::OneTimeShutdown()
{
	// This is called by the VR thread, not the java UI thread.
	LOG( "--------------- Oculus360Videos OneTimeShutdown ---------------" );

	delete SoundEffectPlayer;
	SoundEffectPlayer = NULL;

	delete SoundEffectContext;
	SoundEffectContext = NULL;

	if ( BackgroundScene != NULL )
	{
		delete BackgroundScene;
		BackgroundScene = NULL;
	}

	if ( MetaData != NULL )
	{
		delete MetaData;
		MetaData = NULL;
	}

	Globe.Free();

	FreeTexture( BackgroundTexId );

	if ( MovieTexture != NULL )
	{
		delete MovieTexture;
		MovieTexture = NULL;
	}

	DeleteProgram( PanoramaProgram );
	DeleteProgram( FadedPanoramaProgram );
	DeleteProgram( SingleColorTextureProgram );
}

void Oculus360Videos::EnteredVrMode()
{
	LOG( "Oculus360Videos::EnteredVrMode" );
	Browser->SetMenuPose( Posef() );
	VideoMenu->SetMenuPose( Posef() );
}

void Oculus360Videos::LeavingVrMode()
{
	LOG( "Oculus360Videos::LeavingVrMode" );
	if ( MenuState == MENU_VIDEO_PLAYING )
	{
		SetMenuState( MENU_VIDEO_PAUSE );
	}
}

bool Oculus360Videos::OnKeyEvent( const int keyCode, const int repeatCount, const KeyEventType eventType )
{
	if ( GuiSys->OnKeyEvent( keyCode, repeatCount, eventType ) )
	{
		return true;
	}

	if ( ( ( keyCode == OVR_KEY_BACK ) && ( eventType == KEY_EVENT_SHORT_PRESS ) ) ||
		( ( keyCode == OVR_KEY_BUTTON_B ) && ( eventType == KEY_EVENT_UP ) ) )
	{
		if ( MenuState == MENU_VIDEO_LOADING )
		{
			return true;
		}

		if ( ActiveVideo )
		{
			SetMenuState( MENU_BROWSER );
			return true;	// consume the key
		}
		// if no video is playing (either paused or stopped), let VrLib handle the back key
	}
	else if ( keyCode == OVR_KEY_P && eventType == KEY_EVENT_DOWN )
	{
		PauseVideo( true );
	}

	return false;
}

void Oculus360Videos::Command( const char * msg )
{
	// Always include the space in MatchesHead to prevent problems
	// with commands with matching prefixes.

	if ( MatchesHead( "newVideo ", msg ) )
	{
		delete MovieTexture;
		MovieTexture = new SurfaceTexture( app->GetJava()->Env );
		LOG( "RC_NEW_VIDEO texId %i", MovieTexture->GetTextureId() );

		ovrMessageQueue * receiver;
		sscanf( msg, "newVideo %p", &receiver );

		receiver->PostPrintf( "surfaceTexture %p", MovieTexture->GetJavaObject() );

		// don't draw the screen until we have the new size
		CurrentVideoWidth = 0;

		return;
	}
	else if ( MatchesHead( "completion", msg ) ) // video complete, return to menu
	{
		SetMenuState( MENU_BROWSER );
		return;
	}
	else if ( MatchesHead( "video ", msg ) )
	{
		sscanf( msg, "video %i %i", &CurrentVideoWidth, &CurrentVideoHeight );

		if ( MenuState != MENU_VIDEO_PLAYING ) // If video is already being played dont change the state to video ready
		{
			SetMenuState( MENU_VIDEO_READY );
		}

		return;
	}
	else if ( MatchesHead( "startError", msg ) )
	{
		// FIXME: this needs to do some parameter magic to fix xliff tags
		String message;
		GetLocale().GetString( "@string/playback_failed", "@string/playback_failed", message );
		String fileName = ExtractFile( ActiveVideo->Url );
		message = ovrLocale::GetXliffFormattedString( message.ToCStr(), fileName.ToCStr() );

		GuiSys->GetDefaultFont().WordWrapText( message, 1.0f );
		app->ShowInfoText( 4.5f, message.ToCStr() );
		SetMenuState( MENU_BROWSER );
		return;
	}
}

Matrix4f Oculus360Videos::TexmForVideo( const int eye )
{
	if ( strstr( VideoName.ToCStr(), "_TB.mp4" ) )
	{	// top / bottom stereo panorama
		return eye ?
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0.5f,
				0, 0, 1, 0,
				0, 0, 0, 1 )
			:
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 );
	}
	if ( strstr( VideoName.ToCStr(), "_BT.mp4" ) )
	{	// top / bottom stereo panorama
		return ( !eye ) ?
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0.5f,
				0, 0, 1, 0,
				0, 0, 0, 1 )
			:
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 );
	}
	if ( strstr( VideoName.ToCStr(), "_LR.mp4" ) )
	{	// left / right stereo panorama
		return eye ?
			Matrix4f(
				0.5f, 0, 0, 0,
				0, 1, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 )
			:
			Matrix4f(
				0.5f, 0, 0, 0.5f,
				0, 1, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 );
	}
	if ( strstr( VideoName.ToCStr(), "_RL.mp4" ) )
	{	// left / right stereo panorama
		return ( !eye ) ?
			Matrix4f(
				0.5f, 0, 0, 0,
				0, 1, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 )
			:
			Matrix4f(
				0.5f, 0, 0, 0.5f,
				0, 1, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 );
	}

	// default to top / bottom stereo
	if ( CurrentVideoWidth == CurrentVideoHeight )
	{	// top / bottom stereo panorama
		return eye ?
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0.5f,
				0, 0, 1, 0,
				0, 0, 0, 1 )
			:
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 );

		// We may want to support swapping top/bottom
	}
	return Matrix4f::Identity();
}

Matrix4f Oculus360Videos::TexmForBackground( const int eye )
{
	if ( BackgroundWidth == BackgroundHeight )
	{	// top / bottom stereo panorama
		return eye ?
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0.5f,
				0, 0, 1, 0,
				0, 0, 0, 1 )
			:
			Matrix4f(
				1, 0, 0, 0,
				0, 0.5f, 0, 0,
				0, 0, 1, 0,
				0, 0, 0, 1 );

		// We may want to support swapping top/bottom
	}
	return Matrix4f::Identity();
}

Matrix4f Oculus360Videos::DrawEyeView( const int eye, const float fovDegreesX, const float fovDegreesY, ovrFrameParms & frameParms )
{
	const Matrix4f viewMatrix = Scene.GetEyeViewMatrix( eye );
	const Matrix4f projectionMatrix = Scene.GetEyeProjectionMatrix( eye, fovDegreesX, fovDegreesY );
	const Matrix4f eyeViewProjection = Scene.GetEyeViewProjectionMatrix( eye, fovDegreesX, fovDegreesY );

	if ( MenuState != MENU_VIDEO_PLAYING && MenuState != MENU_VIDEO_PAUSE  )
	{
		// Draw the ovr scene 
		const float fadeColor = CurrentFadeLevel;
		ModelDef & def = *const_cast< ModelDef * >( &Scene.GetWorldModel().Definition->Def );
		for ( int i = 0; i < def.surfaces.GetSizeI(); i++ )
		{
			ovrSurfaceDef & sd = def.surfaces[ i ];
			glUseProgram( SingleColorTextureProgram.program );

			glUniformMatrix4fv( SingleColorTextureProgram.uMvp, 1, GL_TRUE, eyeViewProjection.M[ 0 ] );

			glActiveTexture( GL_TEXTURE0 );
			glBindTexture( GL_TEXTURE_2D, sd.materialDef.textures[ 0 ] );

			glUniform4f( SingleColorTextureProgram.uColor, fadeColor, fadeColor, fadeColor, 1.0f );

			sd.geo.Draw();

			glBindTexture( GL_TEXTURE_2D, 0 ); // don't leave it bound
		}
	}
	else if ( ( MenuState == MENU_VIDEO_PLAYING || MenuState == MENU_VIDEO_PAUSE ) && ( MovieTexture != NULL ) )
	{
		// draw animated movie panorama
		glActiveTexture( GL_TEXTURE0 );
		glBindTexture( GL_TEXTURE_EXTERNAL_OES, MovieTexture->GetTextureId() );

		glActiveTexture( GL_TEXTURE1 );
		glBindTexture( GL_TEXTURE_2D, BackgroundTexId );

		glDisable( GL_DEPTH_TEST );
		glDisable( GL_CULL_FACE );

		GlProgram & prog = ( BackgroundWidth == BackgroundHeight ) ? FadedPanoramaProgram : PanoramaProgram;

		glUseProgram( prog.program );
		glUniform4f( prog.uColor, 1.0f, 1.0f, 1.0f, 1.0f );

		const Matrix4f view = Scene.GetEyeViewMatrix( 0 );
		const Matrix4f proj = Scene.GetEyeProjectionMatrix( 0, fovDegreesX, fovDegreesY );

		const int toggleStereo = VideoMenu->IsOpenOrOpening() ? 0 : eye;

		glUniformMatrix4fv( prog.uTexm, 1, GL_TRUE, TexmForVideo( toggleStereo ).M[ 0 ] );
		glUniformMatrix4fv( prog.uMvp, 1, GL_TRUE, ( proj * view ).M[ 0 ] );
		Globe.Draw();

		glActiveTexture( GL_TEXTURE0 );
		glBindTexture( GL_TEXTURE_EXTERNAL_OES, 0 );

		glActiveTexture( GL_TEXTURE1 );
		glBindTexture( GL_TEXTURE_2D, 0 );
	}

	frameParms.ExternalVelocity = Scene.GetExternalVelocity();
	frameParms.Layers[VRAPI_FRAME_LAYER_TYPE_WORLD].Flags |= VRAPI_FRAME_LAYER_FLAG_CHROMATIC_ABERRATION_CORRECTION;

	GuiSys->RenderEyeView( Scene.GetCenterEyeViewMatrix(), viewMatrix, projectionMatrix );

	return eyeViewProjection;
}

bool Oculus360Videos::IsVideoPlaying() const
{
	jmethodID methodId = app->GetJava()->Env->GetMethodID( MainActivityClass, "isPlaying", "()Z" );
	if ( !methodId )
	{
		LOG( "Couldn't find isPlaying methodID" );
		return false;
	}

	bool isPlaying = app->GetJava()->Env->CallBooleanMethod( app->GetJava()->ActivityObject, methodId );
	return isPlaying;
}

void Oculus360Videos::PauseVideo( bool const force )
{
	LOG( "PauseVideo()" );

	jmethodID methodId = app->GetJava()->Env->GetMethodID( MainActivityClass,
		"pauseMovie", "()V" );
	if ( !methodId )
	{
		LOG( "Couldn't find pauseMovie methodID" );
		return;
	}

	app->GetJava()->Env->CallVoidMethod( app->GetJava()->ActivityObject, methodId );
	VideoPaused = true;
}

void Oculus360Videos::StopVideo()
{
	LOG( "StopVideo()" );

	jmethodID methodId = app->GetJava()->Env->GetMethodID( MainActivityClass,
		"stopMovie", "()V" );
	if ( !methodId )
	{
		LOG( "Couldn't find stopMovie methodID" );
		return;
	}

	app->GetJava()->Env->CallVoidMethod( app->GetJava()->ActivityObject, methodId );

	delete MovieTexture;
	MovieTexture = NULL;
	VideoPaused = false;
}

void Oculus360Videos::ResumeVideo()
{
	LOG( "ResumeVideo()" );

	GuiSys->CloseMenu( Browser, false );

	jmethodID methodId = app->GetJava()->Env->GetMethodID( MainActivityClass,
		"resumeMovie", "()V" );
	if ( !methodId )
	{
		LOG( "Couldn't find resumeMovie methodID" );
		return;
	}

	app->GetJava()->Env->CallVoidMethod( app->GetJava()->ActivityObject, methodId );
	VideoPaused = false;
}

void Oculus360Videos::StartVideo( const double nowTime )
{
	if ( ActiveVideo )
	{
		SetMenuState( MENU_VIDEO_LOADING );
		VideoName = ActiveVideo->Url;
		LOG( "StartVideo( %s )", ActiveVideo->Url.ToCStr() );
		SoundEffectContext->Play( "sv_select" );

		jmethodID startMovieMethodId = app->GetJava()->Env->GetMethodID( MainActivityClass,
			"startMovieFromNative", "(Ljava/lang/String;)V" );

		if ( !startMovieMethodId )
		{
			LOG( "Couldn't find startMovie methodID" );
			return;
		}

		LOG( "moviePath = '%s'", ActiveVideo->Url.ToCStr() );
		jstring jstr = app->GetJava()->Env->NewStringUTF( ActiveVideo->Url.ToCStr() );
		app->GetJava()->Env->CallVoidMethod( app->GetJava()->ActivityObject, startMovieMethodId, jstr );
		app->GetJava()->Env->DeleteLocalRef( jstr );
		VideoPaused = false;

		LOG( "StartVideo done" );
	}
}

void Oculus360Videos::SeekTo( const int seekPos )
{
	if ( ActiveVideo )
	{
		jmethodID seekToMethodId = app->GetJava()->Env->GetMethodID( MainActivityClass,
			"seekToFromNative", "(I)V" );

		if ( !seekToMethodId )
		{
			LOG( "Couldn't find seekToMethodId methodID" );
			return;
		}

		app->GetJava()->Env->CallVoidMethod( app->GetJava()->ActivityObject, seekToMethodId, seekPos );

		LOG( "SeekTo %i done", seekPos );
	}
}

void Oculus360Videos::SetMenuState( const OvrMenuState state )
{
	OvrMenuState lastState = MenuState;
	MenuState = state;
	LOG( "%s to %s", MenuStateString( lastState ), MenuStateString( MenuState ) );
	switch ( MenuState )
	{
	case MENU_NONE:
		break;
	case MENU_BROWSER:
		Fader.ForceFinish();
		Fader.Reset();
		GuiSys->CloseMenu( VideoMenu, false );
		GuiSys->OpenMenu( OvrFolderBrowser::MENU_NAME );
		if ( ActiveVideo )
		{
			StopVideo();
			ActiveVideo = NULL;
		}
		GuiSys->GetGazeCursor().ShowCursor();
		break;
	case MENU_VIDEO_LOADING:
		if ( MovieTexture != NULL )
		{
			delete MovieTexture;
			MovieTexture = NULL;
		}
		GuiSys->CloseMenu( Browser, false );
		GuiSys->CloseMenu( VideoMenu, false );
		Fader.StartFadeOut();
		GuiSys->GetGazeCursor().HideCursor();
		break;
	case MENU_VIDEO_READY:
		break;
	case MENU_VIDEO_PLAYING:
		Fader.Reset();
		VideoMenuTimeLeft = VideoMenuVisibleTime;
		if ( !HmdMounted && !VideoPaused )
		{
			// Need to pause video, because HMD was unmounted during loading and we weren't able to
			// pause during loading.
			PauseVideo( false );
			GuiSys->GetGazeCursor().ShowCursor();
		}
		break;
	case MENU_VIDEO_PAUSE:
		GuiSys->OpenMenu( OvrVideoMenu::MENU_NAME );
		VideoMenu->RepositionMenu( app->GetLastViewMatrix() );
		PauseVideo( false );
		GuiSys->GetGazeCursor().ShowCursor();
		break;
	case MENU_VIDEO_RESUME:
		GuiSys->CloseMenu( VideoMenu, false );
		ResumeVideo();
		MenuState = MENU_VIDEO_PLAYING;
		GuiSys->GetGazeCursor().HideCursor();
		break;
	default:
		LOG( "Oculus360Videos::SetMenuState unknown state: %d", static_cast< int >( state ) );
		OVR_ASSERT( false );
		break;
	}
}

const char * menuStateNames [ ] =
{
	"MENU_NONE",
	"MENU_BROWSER",
	"MENU_VIDEO_LOADING",
	"MENU_VIDEO_READY",
	"MENU_VIDEO_PLAYING",
	"MENU_VIDEO_PAUSE",
	"MENU_VIDEO_RESUME",
	"NUM_MENU_STATES"
};

const char* Oculus360Videos::MenuStateString( const OvrMenuState state )
{
	OVR_ASSERT( state >= 0 && state < NUM_MENU_STATES );
	return menuStateNames[ state ];
}

void Oculus360Videos::OnVideoActivated( const OvrMetaDatum * videoData )
{
	ActiveVideo = videoData;
	StartVideo( vrapi_GetTimeInSeconds() );
}

Matrix4f Oculus360Videos::Frame( const VrFrame & vrFrame )
{
	// Process incoming messages until the queue is empty.
	for ( ; ; )
	{
		const char * msg = MessageQueue.GetNextMessage();
		if ( msg == NULL )
		{
			break;
		}
		Command( msg );
		free( (void *)msg );
	}

	// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/startmoviefromudp
	if (startMovieFromUDP_yield != NULL)
	{
		LOG( "startMovieFromUDP_yield");

		this->OnVideoActivated( startMovieFromUDP_yield );
		
		startMovieFromUDP_yield = NULL;
	}

	// Disallow player foot movement, but we still want the head model
	// movement for the swipe view.
	VrFrame vrFrameWithoutMove = vrFrame;
	vrFrameWithoutMove.Input.sticks[ 0 ][ 0 ] = 0.0f;
	vrFrameWithoutMove.Input.sticks[ 0 ][ 1 ] = 0.0f;

	Scene.Frame( vrFrameWithoutMove, app->GetHeadModelParms() );

	// Check for new video frames
	// latch the latest movie frame to the texture.
	if ( MovieTexture != NULL && CurrentVideoWidth != 0 )
	{
		MovieTexture->Update();
		FrameAvailable = false;
	}

	if ( MenuState == MENU_VIDEO_PLAYING || MenuState == MENU_VIDEO_PAUSE )
	{
		if ( ( vrFrame.Input.buttonReleased & BUTTON_A ) ||
			( vrFrame.Input.buttonState & BUTTON_TOUCH_SINGLE ) )
		{
			SoundEffectContext->Play( "sv_release_active" );
			if ( IsVideoPlaying() )
			{
				SetMenuState( MENU_VIDEO_PAUSE );
			}
			else
			{
				SetMenuState( MENU_VIDEO_RESUME );
			}
		}
	}

	const bool mountingOn = !HmdMounted && ( vrFrame.DeviceStatus.DeviceIsDocked && vrFrame.DeviceStatus.HeadsetIsMounted );
	if ( mountingOn )
	{
		HmdMounted = true;
	}
	else
	{
		const bool mountingOff = HmdMounted && !vrFrame.DeviceStatus.HeadsetIsMounted;
		if ( mountingOff )
		{
			HmdMounted = false;
			if ( IsVideoPlaying() )
			{
				SetMenuState( MENU_VIDEO_PAUSE );
			}
		}
	}

	// State transitions
	if ( Fader.GetFadeState() != Fader::FADE_NONE )
	{
		Fader.Update( CurrentFadeRate, vrFrame.DeltaSeconds );
	}
	else if ( ( MenuState == MENU_VIDEO_READY ) &&
		( Fader.GetFadeAlpha() == 0.0f ) &&
		( MovieTexture != NULL ) )
	{
		SetMenuState( MENU_VIDEO_PLAYING );
		app->RecenterYaw( true );
	}
	CurrentFadeLevel = Fader.GetFinalAlpha();

	// update gui systems after the app frame, but before rendering anything
	GuiSys->Frame( vrFrame, Scene.GetCenterEyeViewMatrix() );

	return Scene.GetCenterEyeViewMatrix();
}

} // namespace OVR
