using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;
using android.content;

namespace TestMultiProcMemoryFile.Activities
{



    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:minSdkVersion", value = "8")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "8")]

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:targetSdkVersion", value = "22")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Dialog")]
    public class ApplicationActivity : Activity
    {



        protected override void onCreate(Bundle savedInstanceState)
        {
            var activity = this;

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);


            var t = typeof(MemoryFile);

            //var m = Activator.CreateInstance(t);

            //E/AndroidRuntime( 4217): Caused by: java.lang.InstantiationException: class android.os.MemoryFile has no zero argument constructor
            //E/AndroidRuntime( 4217):        at java.lang.Class.newInstance(Class.java:1641)
            //E/AndroidRuntime( 4217):        at ScriptCoreLibJava.BCLImplementation.System.__Activator.CreateInstance(__Activator.java:27)
            //E/AndroidRuntime( 4217):        ... 14 more
            //E/AndroidRuntime( 4217): Caused by: java.lang.NoSuchMethodException: <init> []
            //E/AndroidRuntime( 4217):        at java.lang.Class.getConstructor(Class.java:531)
            //E/AndroidRuntime( 4217):        at java.lang.Class.getDeclaredConstructor(Class.java:510)
            //E/AndroidRuntime( 4217):        at java.lang.Class.newInstance(Class.java:1639)
            //E/AndroidRuntime( 4217):        ... 15 more

            var m = default(MemoryFile);

            try { m = new MemoryFile(default(string), 0); }
            catch { throw; }


            new Button(this).AttachTo(ll).WithText(new { t, m }.ToString());

            var fields = t.GetFields(
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            );

            new Button(this).AttachTo(ll).WithText("fields: " + new { fields.Length }.ToString());

            fields.WithEach(
                SourceField =>
                {
                    //E/AndroidRuntime( 9919): Caused by: java.lang.IllegalAccessException: Cannot access field: java.io.FileDescriptor android.os.MemoryFile.mFD
                    //E/AndroidRuntime( 9919):        at java.lang.reflect.Field.get(Native Method)
                    //E/AndroidRuntime( 9919):        at java.lang.reflect.Field.get(Field.java:279)
                    //E/AndroidRuntime( 9919):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__FieldInfo.GetValue(__FieldInfo.java:46)

                    var value = SourceField.GetValue(m);

                    var xFileDescriptor = value as java.io.FileDescriptor;
                    if (xFileDescriptor != null)
                    {
                        var xfields = typeof(java.io.FileDescriptor).GetFields(
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                        );

                        xfields.WithEach(
                            xSourceField =>
                            {
                                var xvalue = xSourceField.GetValue(value);
                                new Button(this).AttachTo(ll).WithText(new { SourceField, xSourceField, xvalue }.ToString());
                            }
                        );


                    }
                    else
                    {
                        new Button(this).AttachTo(ll).WithText(new { SourceField, value }.ToString());
                    }

                }
            );


            this.setContentView(sv);
        }


    }


}
