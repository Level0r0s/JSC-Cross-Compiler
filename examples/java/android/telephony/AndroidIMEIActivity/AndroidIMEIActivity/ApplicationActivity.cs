using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.content;
using android.provider;
using android.telephony;
using android.view;
using android.webkit;
using android.widget;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;

namespace AndroidIMEIActivity.Activities
{

	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
	[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
	public class ApplicationActivity : Activity
	{
		//		BUILD FAILED
		//x:\util\android-sdk-windows\tools\ant\build.xml:542: Unable to resolve project target 'Google Inc.:Google APIs:21'

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150503/udp
		// http://www.happygeek.in/programmatically-get-device-imei-in-android


		protected override void onCreate(global::android.os.Bundle savedInstanceState)
		{
			// http://developer.android.com/guide/topics/ui/notifiers/notifications.html

			base.onCreate(savedInstanceState);

			var sv = new ScrollView(this);

			var ll = new LinearLayout(this);

			ll.setOrientation(LinearLayout.VERTICAL);

			sv.addView(ll);


			Button b = new Button(this).WithText("Whats my IMEI?").AtClick(
				delegate
				{
					TelephonyManager telephonyManager = (TelephonyManager)this.getSystemService(Context.TELEPHONY_SERVICE);

					string imei = telephonyManager.getDeviceId();


					this.setTitle(new { imei }.ToString());

					//this.ShowLongToast("IMEI: " + imei);
				}
			);


			ll.addView(b);

			this.setContentView(sv);



		}



	}
}
