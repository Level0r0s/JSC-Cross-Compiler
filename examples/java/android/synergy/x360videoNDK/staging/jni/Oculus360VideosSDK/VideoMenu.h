/************************************************************************************

Filename    :	VideoMenu.h
Content     :   VRMenu shown when within panorama
Created     :   October 9, 2014
Authors     :   Warsam Osman

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the Oculus360Videos/ directory. An additional grant 
of patent rights can be found in the PATENTS file in the same directory.

************************************************************************************/

#if !defined( OVR_VideoMenu_h )
#define OVR_VideoMenu_h

#include "VRMenu.h"

namespace OVR {

class Oculus360Videos;
class SearchPaths;
class OvrVideoMenuRootComponent;

class OvrMetaData;
struct OvrMetaDatum;

//==============================================================
// OvrPanoMenu
class OvrVideoMenu : public VRMenu
{
public:
	static char const *	MENU_NAME;
	static const VRMenuId_t ID_CENTER_ROOT;
	static const VRMenuId_t	ID_BROWSER_BUTTON;
	static const VRMenuId_t	ID_VIDEO_BUTTON;

	// only one of these every needs to be created
	static  OvrVideoMenu *		Create(
		OvrGuiSys & guiSys,
		OvrMetaData & metaData,
		float fadeOutTime,
		float radius );

	OvrMetaData & 			GetMetaData() 					{ return MetaData; }

private:
	OvrVideoMenu( OvrGuiSys & guiSys, OvrMetaData & metaData, float fadeOutTime, float radius );

	// private assignment operator to prevent copying
	OvrVideoMenu &	operator = ( OvrVideoMenu & );

	virtual ~OvrVideoMenu( );
	virtual void			Open_Impl( OvrGuiSys & guiSys );
	virtual void			OnItemEvent_Impl( OvrGuiSys & guiSys, VRMenuId_t const itemId, VRMenuEvent const & event );
	virtual void			Frame_Impl( OvrGuiSys & guiSys, VrFrame const & vrFrame );

	// Globals
	OvrMetaData & 			MetaData;

	menuHandle_t			LoadingIconHandle;
	menuHandle_t			AttributionHandle;
	menuHandle_t			BrowserButtonHandle;
	menuHandle_t			VideoControlButtonHandle;

	const float				Radius;

	float					ButtonCoolDown;

	double					OpenTime;

};

}

#endif
