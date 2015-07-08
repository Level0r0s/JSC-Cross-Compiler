using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.WebVR
{
	// http://mozvr.github.io/webvr-spec/webvr.html#vrdevice
	// https://chromium.googlesource.com/experimental/chromium/src/+/refs/wip/bajones/webvr/content/browser/vr/cardboard/cardboard_vr_device.cc
	// https://chromium.googlesource.com/experimental/chromium/src/+/refs/wip/bajones/webvr/content/public/android/java/src/org/chromium/content/browser/input/CardboardVRDevice.java
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/vr/VRDevice.idl

	[Script(HasNoPrototype = true)]
	public class VRDevice
	{
        // 20150529 - 2 days until gearVR is here.

        // https://drive.google.com/folderview?id=0BzudLt22BqGRbW9WTHMtOWMzNjQ&usp=sharing#list

        // https://youtu.be/UGv_sFMn0tQ?t=7882


        // https://www.khronos.org/registry/gles/extensions/OVR/multiview2.txt
        // https://www.khronos.org/registry/gles/extensions/OVR/multiview.txt

        // https://dodocase.zendesk.com/hc/en-us/articles/203453464-Will-Cardboard-work-with-my-phone-

        // https://wemo.io/google-chrome-and-the-future-of-virtual-reality-interview-with-531

        // https://github.com/tparisi/WebVR/blob/master/examples/cube-cardboard.html

        // https://developers.google.com/cardboard/android/
        // https://developers.google.com/cardboard/android/download
        // https://github.com/googlesamples/cardboard-java
        // https://developers.google.com/cardboard/android/get-started

        // https://www.google.com/get/cardboard/get-cardboard.html

        // http://voicesofvr.com/100-sebastian-kuntz-on-virtual-reality-presence-lessons-from-13-years-in-vr/
        // http://voicesofvr.com/112-brandon-jones-on-webvr-for-chrome/
        // 20150323
        // yay. vr!


        // https://docs.google.com/document/d/1dP9m3WLh2lsBs9jJ9LRwv1l0AtuBQAqGLAV-fUbtz2U/edit
        // https://www.google.com/get/cardboard/get-cardboard.html
        // http://timesofindia.indiatimes.com/tech/more-gadgets/Google-Cardboard-a-VR-gadget-for-the-masses/articleshow/46687802.cms


        // http://janusvr.com/
        // http://www.reddit.com/r/janusVR/comments/2vbzya/lowlevel_hmds_and_gear_vr/

        // X:\jsc.svn\examples\javascript\examples\JanusVRExperiment\JanusVRExperiment\Application.cs
        // where is our android cardboard ndk, adk, web example?
        // https://github.com/mrdoob/three.js/blob/dev/examples/js/effects/VREffect.js
        // http://vihart.github.io/webVR-playing-with/underConstruction
        // http://elevr.com/updates-webvr-phonevr-wearality-kickstarter-etc/

        // http://mozvr.com/downloads/


        // X:\jsc.svn\examples\java\android\synergy\AndroidCardboardExperiment\AndroidCardboardExperiment\ApplicationActivity.cs
    }

    //partial interface Navigator
    //{
    //	Promise<sequence<VRDevice>> getVRDevices();
    //};
}
