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
using ScriptCoreLibJava.Extensions;

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
            var m_descriptor = 0;

            //try { m = new MemoryFile(default(string), 0); }
            try { m = new MemoryFile("name1", 0x07); }
            catch { throw; }

            try
            {
                m.writeBytes(
                    new byte[] { 7, 6, 5, 4, 3, 2, 1 }, 0, 0, 0x07
                );
            }
            catch { }

            var buffer = new byte[0x07];
            try
            {
                m.readBytes(buffer, 0, 0, 0x07);
            }
            catch { }
            var buffer0 = buffer[0];

            //new Button(this).AttachTo(ll).WithText(new { t, m, buffer0 }.ToString());
            new Button(this).AttachTo(ll).WithText(new { buffer0 }.ToString());

            #region fields
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
                            xFileDescriptor_SourceField =>
                            {
                                var xvalue = xFileDescriptor_SourceField.GetValue(value);

                                //if (xFileDescriptor_SourceField.FieldType == typeof(int))
                                if (xFileDescriptor_SourceField.Name == "descriptor")
                                    m_descriptor = (int)xvalue;

                                new Button(this).AttachTo(ll).WithText(xFileDescriptor_SourceField + new { xvalue }.ToString());
                            }
                        );


                    }
                    else
                    {
                        new Button(this).AttachTo(ll).WithText(new { SourceField, value }.ToString());
                    }

                }
            );
            #endregion


            new Button(activity).WithText("Next " + new { m_descriptor }).AttachTo(ll).AtClick(
                delegate
                {
                    Intent intent = new Intent(activity, typeof(SecondaryActivity).ToClass());
                    intent.setFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);

                    // share scope
                    intent.putExtra("m_descriptor", m_descriptor);

                    //intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);

                    // cached backgroun process?
                    // switching to another process.. easy...
                    activity.startActivity(intent);
                }
            );

            this.setContentView(sv);

            // https://developer.android.com/training/run-background-service/create-service.html
        }


    }

    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:process", value = ":foo1")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Holo.Light.Dialog")]
    [ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Dialog")]
    //[ScriptCoreLib.Android.Manifest.ApplicationMetaData(name = "android:theme", value = "@android:style/Theme.Translucent.NoTitleBar")]
    //public class SecondaryActivity : ScriptCoreLib.Android.CoreAndroidWebServiceActivity
    public class SecondaryActivity : Activity
    {
        protected override void onCreate(Bundle savedInstanceState)
        {
            // http://www.mkyong.com/android/android-activity-from-one-screen-to-another-screen/
            // https://groups.google.com/forum/#!topic/android-ndk/sjIiMsLkHCM

            base.onCreate(savedInstanceState);


            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var activity = this;

            var m = default(MemoryFile);

            //try { m = new MemoryFile(default(string), 0); }
            try { m = new MemoryFile("name1", 0x07); }
            catch { throw; }
            var m_descriptor = this.getIntent().getIntExtra("m_descriptor", 0);

            ////var fs = new java.io.FileDescriptor { };

            ////var xfields = typeof(java.io.FileDescriptor).GetFields(
            ////    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            ////);

            ////xfields.WithEach(
            ////    xFileDescriptor_SourceField =>
            ////    {
            ////        var xvalue = xFileDescriptor_SourceField.GetValue(fs);

            ////        //if (xFileDescriptor_SourceField.FieldType == typeof(int))
            ////        if (xFileDescriptor_SourceField.Name == "descriptor")
            ////        {
            ////            xFileDescriptor_SourceField.SetValue(fs, m_descriptor);

            ////        }
            ////    }
            ////);



            // need to call native_mmap

            var t = typeof(MemoryFile);

            // internal static int native_mmap(java.io.FileDescriptor fd, int length, int mode);
            //var native_mmap = t.GetMethod("native_mmap", new[] {
            //        typeof(java.io.FileDescriptor), 
            //        typeof(int), 
            //        typeof(int)
            //    }
            //);

            //var methods = t.GetMethods(
            //    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static
            //);

            //var native_mmap = methods.FirstOrDefault(x => x.Name == "native_mmap");

            //#region time to patch it?

            //#endregion

            //var available = 0;
            //var buffer = new byte[0x07];
            ////X:\jsc.svn\examples\java\android\Test\TestNewByteArray7\TestNewByteArray7\Class1.cs
            //try { m.readBytes(buffer, 0, 0, 0x07); }
            //catch { }

            //try
            //{
            //    //buffer0 = new java.io.FileInputStream(fs).read();
            //    available = new java.io.FileInputStream(fs).available();
            //}
            //catch
            //{ }

            this.setTitle(
                 new { m_descriptor }.ToString()
            );


            //new Button(this).AttachTo(ll).WithText("methods: " + new
            //{
            //    methods.Length
            //    ,
            //    native_mmap
            //}.ToString());


            //methods.WithEach(
            //    SourceMethod =>
            //    {
            //        new Button(this).AttachTo(ll).WithText(new { SourceMethod }.ToString());
            //    }
            //);

            Action patch = delegate { };




            #region fields
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
                            xFileDescriptor_SourceField =>
                            {
                                var xvalue = xFileDescriptor_SourceField.GetValue(value);

                                //if (xFileDescriptor_SourceField.FieldType == typeof(int))
                                if (xFileDescriptor_SourceField.Name == "descriptor")
                                {
                                    //m_descriptor = (int)xvalue;

                                    patch = delegate
                                    {
                                        Console.WriteLine("enter patch");

                                        //mAddress = native_mmap(mFD, length, modeToProt(mode));
                                        //mOwnsRegion = false;

                                        var field_mAddress = fields.FirstOrDefault(xx => xx.Name == "mAddress");

                                        Console.WriteLine("enter patch " + new { field_mAddress } + " invoke native_mmap");

                                        xFileDescriptor_SourceField.SetValue(value, m_descriptor);

                                        var PROT_READ = 0x1;

                                        //E/AndroidRuntime( 8047): Caused by: java.lang.IllegalAccessException: access to method denied
                                        //E/AndroidRuntime( 8047):        at java.lang.reflect.Method.invokeNative(Native Method)
                                        //E/AndroidRuntime( 8047):        at java.lang.reflect.Method.invoke(Method.java:507)
                                        //E/AndroidRuntime( 8047):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)

                                        var mAddress = 0;
                                        
                                        //native_mmap.Invoke(null,
                                        //    new object[]
                                        //                {
                                        //                    value,
                                        //                    0x07,
                                        //                    PROT_READ
                                        //                }
                                        //);

                                        Console.WriteLine("exit patch " + new { mAddress });

                                        field_mAddress.SetValue(m, mAddress);

                                        // http://stackoverflow.com/questions/8165216/what-is-the-use-of-memoryfile-in-android
                                        // http://osdir.com/ml/Android-Developers/2013-01/msg00793.html
                                    };
                                }

                                new Button(this).AttachTo(ll).WithText(xFileDescriptor_SourceField + new { xvalue }.ToString());
                            }
                        );


                    }
                    else
                    {
                        new Button(this).AttachTo(ll).WithText(new { SourceField, value }.ToString());
                    }

                }
            );
            #endregion

            new Button(activity).WithText("Patch n Read").AttachTo(ll).AtClick(
               delegate
               {
                   patch();

                   //this.finish();

                   //E/AndroidRuntime( 7526): Caused by: java.lang.NoSuchMethodError: android.app.Activity.finishAndRemoveTask
                   //E/AndroidRuntime( 7526):        at TestMultiProcMemoryFile.Activities.SecondaryActivity._onCreate_b__0(SecondaryActivity.java:66)

                   //this.finishAndRemoveTask();

                   try { m.readBytes(buffer, 0, 0, 0x07); }
                   catch { }
                   buffer0 = buffer[0];


                   this.setTitle(
                        new { m_descriptor, buffer0 }.ToString()
                   );



               }
            );

            this.setContentView(sv);

        }
    }
}
