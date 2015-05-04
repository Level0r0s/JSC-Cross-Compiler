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
	   LOG("enter OvrApp::cxxSetAppInterface");



	   // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150504/dae
	   // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150402/android-mk
	   // return (new JavaSample())->SetActivity( jni, clazz, activity, javaFromPackageNameString, javaCommandString, javaUriString );
       //return (new OvrApp())->SetActivity( jni, clazz, activity );
	   // X:\opensource\ovr_mobile_sdk_0.5.0\VRLib\jni\App.cpp
	   // "X:\opensource\ovr_mobile_sdk_0.5.0\VrNative\VrTemplate\jni\OvrApp.cpp"

       return (new OvrApp())->SetActivity( jni, clazz, activity, javaFromPackageNameString, javaCommandString, javaUriString );
}





OvrApp::OvrApp()
{
}

OvrApp::~OvrApp()
{
}

void OvrApp::OneTimeInit( const char * fromPackage, const char * launchIntentJSON, const char * launchIntentURI )
{
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
	LOG("enter OvrApp::Command");
}

Matrix4f OvrApp::DrawEyeView( const int eye, const float fovDegrees )
{
	const Matrix4f view = Scene.DrawEyeView( eye, fovDegrees );

	return view;
}

Matrix4f OvrApp::Frame(const VrFrame vrFrame)
{
	LOG("enter OvrApp::Frame");

	// Player movement
    Scene.Frame( app->GetVrViewParms(), vrFrame, app->GetSwapParms().ExternalVelocity );

	app->DrawEyeViewsPostDistorted( Scene.CenterViewMatrix() );

	return Scene.CenterViewMatrix();
}