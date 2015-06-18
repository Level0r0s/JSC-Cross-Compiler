using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
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
using android.net;
//using java.io;

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
        // X:\jsc.svn\examples\java\android\synergy\OVRVrCubeWorldSurfaceView\OVRVrCubeWorldSurfaceViewXNDK\VrCubeWorld.Renderer.cs

        // http://mattias.niklewski.com/2014/03/binder.html


        MemoryFile m = default(MemoryFile);


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

            var m_descriptor = 0;
            var m_fd = default(java.io.FileDescriptor);
            var pid = android.os.Process.myPid();
            var uid = android.os.Process.myUid();

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
                        m_fd = xFileDescriptor;

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
                                    m_descriptor = (int)xvalue;
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

            //var pfd = default(android.os.ParcelFileDescriptor);

            //try
            //{
            //    pfd = android.os.ParcelFileDescriptor.dup(
            //        m_fd
            //    );
            //}
            //catch
            //{
            //}

            new Button(activity).WithText("Next \n " + new
            {
                pid,
                uid
                //,
                //pfd = pfd.getFd(),
                //size = pfd.getStatSize()
            }).AttachTo(ll).AtClick(
                delegate
                {
                    Intent intent = new Intent(activity, typeof(SecondaryActivity).ToClass());
                    intent.setFlags(Intent.FLAG_ACTIVITY_NO_HISTORY);

                    // share scope
                    intent.putExtra("m_descriptor", m_descriptor);
                    intent.putExtra("pid", pid);

                    //intent.putExtra("pfd", pfd);

                    //intent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TASK);

                    try
                    {
                        Console.WriteLine("new LocalServerSocket");
                        var ss = new LocalServerSocket("MemoryFileDescriptor0");

                        // cached backgroun process?
                        // switching to another process.. easy...
                        activity.startActivity(intent);

                        Console.WriteLine("before LocalServerSocket accept");


                        // http://alvinalexander.com/java/jwarehouse/android/core/tests/coretests/src/android/net/LocalSocketTest.java.shtml
                        var ls = ss.accept();
                        Console.WriteLine("after LocalServerSocket accept");

                        ls.setFileDescriptorsForSend(new[] { m_fd });
                        ls.getOutputStream().write(42);
                    }
                    catch
                    {
                        throw;
                    }

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
            // http://unix.stackexchange.com/questions/10050/proc-pid-fd-x-link-number
            // http://stackoverflow.com/questions/21955273/sharing-shared-memory-file-descriptors-across-android-app-processes-using-binder
            // http://permalink.gmane.org/gmane.comp.handhelds.android.porting/10904

            // http://www.mkyong.com/android/android-activity-from-one-screen-to-another-screen/
            // https://groups.google.com/forum/#!topic/android-ndk/sjIiMsLkHCM

            base.onCreate(savedInstanceState);


            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var activity = this;

            // does it work for us?
            var fs = default(java.io.FileDescriptor);

            try
            {
                var ls = new LocalSocket();
                ls.connect(new LocalSocketAddress("MemoryFileDescriptor0"));

                var i8 = ls.getInputStream().read();

                fs = ls.getAncillaryFileDescriptors().FirstOrDefault();

                new Button(this).AttachTo(ll).WithText(new { i8, fs }.ToString());


                //var memory0 = new FileInputStream(ls_fd).read();

                //new Button(this).AttachTo(ll).WithText(new { memory0 }.ToString());
            }
            catch { }

            var m = default(MemoryFile);

            //try { m = new MemoryFile(default(string), 0); }
            try { m = new MemoryFile("name1", 0x07); }
            catch { throw; }
            var m_descriptor = this.getIntent().getIntExtra("m_descriptor", 0);
            var parentpid = this.getIntent().getIntExtra("pid", 0);

            //var pfd = (ParcelFileDescriptor)this.getIntent().getParcelableExtra("pfd");

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
            var buffer = new byte[0x07];
            //X:\jsc.svn\examples\java\android\Test\TestNewByteArray7\TestNewByteArray7\Class1.cs
            //try { m.readBytes(buffer, 0, 0, 0x07); }
            //catch { }

            //try
            //{
            //    //buffer0 = new java.io.FileInputStream(fs).read();
            //    available = new java.io.FileInputStream(fs).available();
            //}
            //catch
            //{ }


            //var parentpidfd = "/proc/" + parentpid + "/fd/" + m_descriptor;

            //var parentpidfdx = global::System.IO.File.Exists(parentpidfd);

            //var parentpidf = new java.io.File(parentpidfd);


            //new Button(this).AttachTo(ll).WithText(new { parentpidfdx }.ToString());
            //new Button(this).AttachTo(ll).WithText(new { parentpidfd }.ToString());
            //new Button(this).AttachTo(ll).WithText(new { size = parentpidf.length() }.ToString());


            //var parentfd = ParcelFileDescriptor.open(parentpidf, ParcelFileDescriptor.MODE_READ_WRITE);

            var pid = android.os.Process.myPid();
            var uid = android.os.Process.myUid();

            this.setTitle(
                 new
                 {
                     pid,
                     uid

                     //m_descriptor,
                     //parentpid,
                     ////pfg = pfd.getFd(),
                     ////size = pfd.getStatSize()
                     //parentpidfdx,
                     //parentpidfd
                 }.ToString()
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
                                        Console.WriteLine("enter patch " + new { SourceField });

                                        //mAddress = native_mmap(mFD, length, modeToProt(mode));
                                        //mOwnsRegion = false;

                                        SourceField.SetValue(m, fs);
                                        //xFileDescriptor_SourceField.SetValue(value, m_descriptor);
                                        value = SourceField.GetValue(m);
                                        xvalue = xFileDescriptor_SourceField.GetValue(value);

                                        var field_mAddress = fields.FirstOrDefault(xx => xx.Name == "mAddress");

                                        Console.WriteLine("enter patch " + new { xvalue } + " invoke mmap");


                                        var PROT_READ = 0x1;

                                        //E/AndroidRuntime( 8047): Caused by: java.lang.IllegalAccessException: access to method denied
                                        //E/AndroidRuntime( 8047):        at java.lang.reflect.Method.invokeNative(Native Method)
                                        //E/AndroidRuntime( 8047):        at java.lang.reflect.Method.invoke(Method.java:507)
                                        //E/AndroidRuntime( 8047):        at ScriptCoreLibJava.BCLImplementation.System.Reflection.__MethodInfo.InternalInvoke(__MethodInfo.java:93)

                                        //I/System.Console( 8648): 21c8:0001 enter patch { field_mAddress = int mAddress } invoke native_mmap

                                        //I/System.Console( 8936): 22e8:0001 GetFields { Length = 5, IsPublic = false, IsNonPublic = true, IsStatic = fal
                                        //I/System.Console( 8936): 22e8:0001 enter patch
                                        //I/System.Console( 8936): 22e8:0001 enter patch { field_mAddress = int mAddress, m_descriptor = 39 } invoke mmap
                                        //I/System.Console( 8936): 22e8:0001 lib: libs/armeabi_v7a/libTestNDKAsAsset.so
                                        //I/System.Console( 8936): 22e8:0001 loadLibrary: TestNDKAsAsset
                                        //I/System.Console( 8936): 22e8:0001 exit patch { mAddress = -1 }

                                        //var z = ScriptCoreLibNative.SystemHeaders.sys.mman_h.mmap(
                                        //    null,
                                        //    length,
                                        //    PROT_READ | PROT_WRITE,
                                        //    MAP_SHARED,
                                        //    fd,
                                        //    0
                                        //    );

                                        //                                        I/System.Console( 8016): 1f50:0001 enter patch { SourceField = java.io.FileDescriptor mFD }
                                        //I/System.Console( 8016): 1f50:0001 enter patch { field_mAddress = long mAddress, xvalue = 32 } invoke mmap
                                        //I/System.Console( 8016): 1f50:0001 lib: libs/armeabi_v7a/libTestNDKAsAsset.so
                                        //I/System.Console( 8016): 1f50:0001 loadLibrary: TestNDKAsAsset
                                        //I/System.Console( 8016): 1f50:0001 exit patch { mAddress = -1 }


                                        //I/System.Console( 9792): 2640:0001 enter patch { SourceField = java.io.FileDescriptor mFD }
                                        //I/System.Console( 9792): 2640:0001 enter patch { xvalue = 32 } invoke mmap
                                        //I/xNativeActivity( 9792): x:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\Program.cs:139 mmap -1 errno: 13 Permission denied
                                        //I/System.Console( 9792): 2640:0001 exit patch { mAddress = -1 }
                                        //E/audit   ( 5152): type=1400 msg=audit(1433328685.131:566): avc:  denied  { mmap_zero } for  pid=9792 comm="Activities:foo1" scontext=u:r:untrusted_app:s0 tcontext=u:r:untrusted_app:s0 tclass=memprotect permissive=0
                                        //E/audit   ( 5152):  SEPF_SM-G925F_5.0.2_0009
                                        //E/audit   ( 5152): type=1300 msg=audit(1433328685.131:566): arch=40000028 syscall=192 success=no exit=-13 a0=0 a1=7 a2=3 a3=10 items=0 ppid=2962 ppcomm=main pid=9792 auid=4294967295 uid=10315 gid=10315 euid=10315 suid=10315 fsuid=10315 egid=10315 sgid=10315 fsgid=10315 ses=4294967295 tty=(none) comm="Activities:foo1" exe="/system/bin/app_process32" subj=u:r:untrusted_app:s0 key=(null)
                                        //E/audit   ( 5152): type=1320 msg=audit(1433328685.131:566):
                                        //D/SSRM:n  ( 3468): SIOP:: AP = 290, PST = 300, CP = 395, CUR = 268

                                        // https://github.com/realm/realm-java/issues/1037

                                        var mAddress = (int)
                                            // ???
                                            TestNDKAsAsset.xActivity.mmap(
                                            (int)xvalue,
                                            0x07
                                           );

                                        // https://android.googlesource.com/platform/development/+/858086e/ndk/sources/android/libportable/arch-mips/mmap.c
                                        // https://groups.google.com/forum/#!msg/android-ndk/tNYpTsHNQEY/8S7VS2j8f_8J

                                        // jint result = (jint)mmap(NULL, length, prot, MAP_SHARED, fd, 0);

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

            new Button(activity).WithText("Patch n Read!").AttachTo(ll).AtClick(
               delegate
               {
                   patch();
                   //return;

                   //this.finish();

                   //E/AndroidRuntime( 7526): Caused by: java.lang.NoSuchMethodError: android.app.Activity.finishAndRemoveTask
                   //E/AndroidRuntime( 7526):        at TestMultiProcMemoryFile.Activities.SecondaryActivity._onCreate_b__0(SecondaryActivity.java:66)

                   //this.finishAndRemoveTask();

                   //try { m.readBytes(buffer, 0, 0, 0x07); }
                   //catch { }
                   //var buffer0 = buffer[0];


                   //this.setTitle(
                   //     new { m_descriptor, buffer0 }.ToString()
                   //);



               }
            );

            this.setContentView(sv);

        }
    }
}

// http://stackoverflow.com/questions/8888342/how-to-pass-socket-file-descriptor-to-other-application-in-android
//  it is not possible to pass a ParcelFileDescriptor through an Intent.
// http://stackoverflow.com/questions/25777338/sending-an-ashmem-file-descriptor-via-an-intent

//E/AndroidRuntime(19607): Caused by: java.lang.RuntimeException: Not allowed to write file descriptors here
//E/AndroidRuntime(19607):        at android.os.Parcel.nativeWriteFileDescriptor(Native Method)
//E/AndroidRuntime(19607):        at android.os.Parcel.writeFileDescriptor(Parcel.java:575)
//E/AndroidRuntime(19607):        at android.os.ParcelFileDescriptor.writeToParcel(ParcelFileDescriptor.java:891)
//E/AndroidRuntime(19607):        at android.os.Parcel.writeParcelable(Parcel.java:1357)
//E/AndroidRuntime(19607):        at android.os.Parcel.writeValue(Parcel.java:1262)
//E/AndroidRuntime(19607):        at android.os.Parcel.writeArrayMapInternal(Parcel.java:638)
//E/AndroidRuntime(19607):        at android.os.BaseBundle.writeToParcelInner(BaseBundle.java:1313)
//E/AndroidRuntime(19607):        at android.os.Bundle.writeToParcel(Bundle.java:1096)
//E/AndroidRuntime(19607):        at android.os.Parcel.writeBundle(Parcel.java:663)
//E/AndroidRuntime(19607):        at android.content.Intent.writeToParcel(Intent.java:7910)
//E/AndroidRuntime(19607):        at android.app.ActivityManagerProxy.startActivity(ActivityManagerNative.java:2528)
//E/AndroidRuntime(19607):        at android.app.Instrumentation.execStartActivity(Instrumentation.java:1494)
//E/AndroidRuntime(19607):        at android.app.Activity.startActivityForResult(Activity.java:3954)
//E/AndroidRuntime(19607):        at android.app.Activity.startActivityForResult(Activity.java:3901)
//E/AndroidRuntime(19607):        at android.app.Activity.startActivity(Activity.java:4225)