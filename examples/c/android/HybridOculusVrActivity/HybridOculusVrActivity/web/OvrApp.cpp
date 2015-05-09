/************************************************************************************

Filename    :   OvrApp.cpp
Content     :   
Created     :   
Authors     :   

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.


*************************************************************************************/
#include <jni.h>

#include "OvrApp.h"

char* cxxGetString()
{
	// http://en.wikibooks.org/wiki/GCC_Debugging/g%2B%2B/Warnings/deprecated_conversion_from_string_constant

	//return "from cxxGetString";
	return  (char *) "from cxxGetString, with oculussig, vr_dual";
}

// "X:\opensource\ovr_mobile_sdk_0.5.0\VrNative\VrTemplate\jni\OvrApp.cpp"
jlong cxxSetAppInterface(JNIEnv *jni, jclass clazz, jobject activity,
						 jstring javaFromPackageNameString, jstring javaCommandString, jstring javaUriString )
{
	   LOG("enter OvrApp::cxxSetAppInterface, new OvrApp");

	   // http://stackoverflow.com/questions/28062855/writing-jstring-to-logcat-in-jni-function
	   const char *szjavaFromPackageNameString = jni->GetStringUTFChars(javaFromPackageNameString, NULL);
	   LOG("szjavaFromPackageNameString: ");
	   LOG(szjavaFromPackageNameString);

	   // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150504/dae
	   // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/android-mk
	   // return (new JavaSample())->SetActivity( jni, clazz, activity, javaFromPackageNameString, javaCommandString, javaUriString );
       //return (new OvrApp())->SetActivity( jni, clazz, activity );
	   // X:\opensource\ovr_mobile_sdk_0.5.0\VRLib\jni\App.cpp
	   // "X:\opensource\ovr_mobile_sdk_0.5.0\VrNative\VrTemplate\jni\OvrApp.cpp"

	   OvrApp* app = new OvrApp();

	   LOG("enter OvrApp::cxxSetAppInterface, VrAppInterface SetActivity");
	   
	   OVR::VrAppInterface* i = app;

       return i->SetActivity( jni, clazz, activity, javaFromPackageNameString, javaCommandString, javaUriString );
}





OvrApp::OvrApp()
{
	   LOG("enter OvrApp::ctor");
}

OvrApp::~OvrApp()
{
}

void OvrApp::OneTimeInit( const char * fromPackage, const char * launchIntentJSON, const char * launchIntentURI )
{
	// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607

	// called by_
	LOG("enter OvrApp::OneTimeInit");

	// This is called by the VR thread, not the java UI thread.
	MaterialParms materialParms;
	materialParms.UseSrgbTextureFormats = false;
        
	const char * scenePath = "Oculus/tuscany.ovrscene";
	String	        SceneFile;
	Array<String>   SearchPaths;

	const OvrStoragePaths & paths = app->GetStoragePaths();

	paths.PushBackSearchPathIfValid(EST_SECONDARY_EXTERNAL_STORAGE, EFT_ROOT, "RetailMedia/", SearchPaths);
	paths.PushBackSearchPathIfValid(EST_SECONDARY_EXTERNAL_STORAGE, EFT_ROOT, "", SearchPaths);
	paths.PushBackSearchPathIfValid(EST_PRIMARY_EXTERNAL_STORAGE, EFT_ROOT, "RetailMedia/", SearchPaths);
	paths.PushBackSearchPathIfValid(EST_PRIMARY_EXTERNAL_STORAGE, EFT_ROOT, "", SearchPaths);

	if ( GetFullPath( SearchPaths, scenePath, SceneFile ) )
	{
		LOG("invoke Scene.LoadWorldModel");
		Scene.LoadWorldModel( SceneFile , materialParms );
	}
	else
	{
		LOG( "OvrApp::OneTimeInit SearchPaths failed to find %s", scenePath );
	}
}

void OvrApp::OneTimeShutdown()
{
	// Free GL resources
        
}

void OvrApp::Command( const char * msg )
{
	// https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150607/ovr

	LOG("enter OvrApp::Command msg: %s", msg);
}

Matrix4f OvrApp::DrawEyeView( const int eye, const float fovDegrees )
{
	const Matrix4f view = Scene.DrawEyeView( eye, fovDegrees );

	// can we draw a cube?
	// OvrSceneView
	// X:\opensource\ovr_mobile_sdk_0.5.1\VRLib\jni\ModelView.cpp
	return view;
}

Matrix4f OvrApp::Frame(const VrFrame vrFrame)
{
	//LOG("enter OvrApp::Frame");

	// Player movement
    Scene.Frame( app->GetVrViewParms(), vrFrame, app->GetSwapParms().ExternalVelocity );

	app->DrawEyeViewsPostDistorted( Scene.CenterViewMatrix() );

	return Scene.CenterViewMatrix();
}

void OvrApp::ConfigureVrMode( ovrModeParms & modeParms )
{
	LOG("enter OvrApp::ConfigureVrMode");

	// max it out damet
	modeParms.CpuLevel = 4;
	modeParms.GpuLevel = 4;
	modeParms.AllowPowerSave = false;

//I/VrApi   (26677): SetVrSystemPerformance( cpuLevel 2, gpuLevel 2 )
//D/VrLib   (26677): getAvailableFreqLevels Available levels: {GPU MIN, GPU MAX, CPU MIN, CPU MAX}
//D/VrLib   (26677): getAvailableFreqLevels  -> / 0
//D/VrLib   (26677): getAvailableFreqLevels  -> / 4
//D/VrLib   (26677): getAvailableFreqLevels  -> / 0
//D/VrLib   (26677): getAvailableFreqLevels  -> / 5

	// Always use 2x MSAA for now
	app->GetVrParms().multisamples = 2;
}