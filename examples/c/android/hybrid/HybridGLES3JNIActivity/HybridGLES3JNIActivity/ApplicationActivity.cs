using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;

namespace HybridGLES3JNIActivity.Activities
{
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "10")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {
        // "X:\util\android-ndk-r10e\samples\gles3jni\src\com\android\gles3jni\GLES3JNIActivity.java"
        // can we compile .c in the same project already?


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText("before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );

            var b2 = new Button(this);
            b2.setText("The other button!");
            ll.addView(b2);

            this.setContentView(sv);
        }


    }


}
