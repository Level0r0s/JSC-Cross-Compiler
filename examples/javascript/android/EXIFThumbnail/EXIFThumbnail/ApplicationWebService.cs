using com.drew.imaging;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLibJava.Extensions;
using System.Collections.Generic;
using System.IO;

namespace EXIFThumbnail
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150722/assetslibrary
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" connect 192.168.1.126:5555
        // "x:\util\android-sdk-windows\platform-tools\adb.exe" shell am start -n EXIFThumbnail.Activities/EXIFThumbnail.Activities.ApplicationWebServiceActivity
        // x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity" "AndroidRuntime"


        // struct dict?
        static Dictionary<string, Action> __inspect = new Dictionary<string, Action> { };

        // can we serialize and encrypt a continuation to the client side without static memory?
        public void inspect(string name)
        {
            Console.WriteLine(new { name });

            __inspect[name]();

        }

        public void WebMethod2(string e, Action<string, string> y)
        {
            // https://github.com/drewnoakes/metadata-extractor.git
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150722

            // jsc, on android how do I get the exif thumbnail of the pictures?
            // firs let make sure we have .android referenced


            var DIRECTORY_DCIM = global::android.os.Environment.DIRECTORY_DCIM;

            var path = global::android.os.Environment.getExternalStoragePublicDirectory(DIRECTORY_DCIM).getAbsolutePath();
            path += "/Camera";

            var s = new Stopwatch();

            s.Start();


            var files0 =
                from fname in Directory.GetFiles(path)
                let f = new FileInfo(fname)
                orderby f.LastWriteTime descending
                select f;


            var dir = new java.io.File(path);

            Console.WriteLine("before listFiles " + new { s.Elapsed });


            var files = files0.Take(350).ToArray();



            Console.WriteLine("after listFiles " + new { s.Elapsed });

            var sw = Stopwatch.StartNew();

            files.WithEach(
                ff =>
                {
                    //y(f.getName());

                    //I/System.Console(20875): 518b:ab3a { Name = /storage/emulated/0/DCIM/Camera/20150430_183822.jpg }
                    //I/System.Console(20875): 518b:ab3a { Name = /storage/emulated/0/DCIM/Camera/20150430_183825.mp4 }


                    var f = new java.io.File(ff.FullName);


                    Console.WriteLine(new { sw.ElapsedMilliseconds, ff.Name, ff.Length });

                    if (ff.Extension != ".jpg")
                        //if (!ff.Name.EndsWith(".jpg"))
                        if (!ff.Name.EndsWith(".png"))
                            return;


                    //               I/System.Console(31633): 7b91:b252 { Name = /storage/emulated/0/DCIM/Camera/20150721_143000.jpg, dir = JPEG, tag = Image Height }
                    //I/System.Console(31633): 7b91:b252 { Name = /storage/emulated/0/DCIM/Camera/20150721_143000.jpg, dir = JPEG, tag = Image Width }



                    try
                    {
                        // http://stackoverflow.com/questions/8578441/can-the-android-sdk-work-with-jdk-1-7
                        // http://stackoverflow.com/questions/15848332/does-adt-support-java-7-api
                        // https://code.google.com/p/android/issues/detail?id=22970
                        //                        [dx] trouble processing:
                        //[dx] bad class file magic (cafebabe) or version (0033.0000)
                        //[dx] ...while parsing com/drew/imaging/bmp/BmpMetadataReader.class
                        //[dx] ...while processing com/drew/imaging/bmp/BmpMetadataReader.class

                        //{ tag = [Exif Thumbnail] Thumbnail Compression - JPEG (old-style) }
                        //{ tag = [Exif Thumbnail] Orientation - Top, left side (Horizontal / normal) }
                        //{ tag = [Exif Thumbnail] X Resolution - 72 dots per inch }
                        //{ tag = [Exif Thumbnail] Y Resolution - 72 dots per inch }
                        //{ tag = [Exif Thumbnail] Resolution Unit - Inch }
                        //{ tag = [Exif Thumbnail] Thumbnail Offset - 1292 bytes }
                        //{ tag = [Exif Thumbnail] Thumbnail Length - 9092 bytes }

                        // http://drewnoakes.com/code/exif/
                        var m = ImageMetadataReader.readMetadata(f);

                        // http://stackoverflow.com/questions/10166373/metadata-extraction-java

                        var src = default(string);
                        var ButtonTitle = ff.Name;
                        int w = 0;
                        int h = 0;

                        // I/System.Console(  814): 032e:b35d { Name = /storage/emulated/0/DCIM/Camera/20150708_105032.jpg, dir = Xmp, tag = XMP Value Count }

                        var cJpegDirectory = typeof(com.drew.metadata.jpeg.JpegDirectory).ToClass();
                        if (m.containsDirectoryOfType(cJpegDirectory))
                        {
                            var x = (com.drew.metadata.jpeg.JpegDirectory)m.getFirstDirectoryOfType(cJpegDirectory);

                            w = x.getImageWidth();
                            h = x.getImageHeight();

                            ButtonTitle += " " + w + ":" + h;

                            //if (w < 6000)
                            //    return;

                            // http://exsight360.com/blog/how-to-upload-non-android-360-panoramas-to-google-maps/
                            var cXmpDirectory = typeof(com.drew.metadata.xmp.XmpDirectory).ToClass();
                            if (m.containsDirectoryOfType(cXmpDirectory))
                            {
                                var xmp = (com.drew.metadata.xmp.XmpDirectory)m.getFirstDirectoryOfType(cXmpDirectory);

                                var p = xmp.getXmpProperties();

                                var equirectangular = ((string)p.get("GPano:ProjectionType")) == "equirectangular";

                                if (equirectangular)
                                {
                                    // link it into vr..
                                    // http://androidwarzone.blogspot.com/2012/03/creating-symbolic-links-on-android-from.html

                                    // or just copy?
                                    //ff.CopyTo("sdcard/oculus/360Photos/" + ff.Name, true);

                                    //java.nio.file.Fi

                                    //java.io.File.cop
                                    // http://stackoverflow.com/questions/106770/standard-concise-way-to-copy-a-file-in-java
                                    // http://docs.oracle.com/javase/7/docs/api/java/nio/file/Files.html

                                    var copysw = Stopwatch.StartNew();

                                    //  Caused by: java.lang.RuntimeException: /sdcard/oculus/360Photos/storage/emulated/0/DCIM/Camera/20150710_202301.jpg: open failed: ENOENT (No such file or directory)

                                    //I/System.Console(17759): 455f:bd8c InternalReadAllBytes { path = /storage/emulated/0/DCIM/Camera/20150624_120258.jpg }
                                    //I/System.Console(17759): 455f:bd8c { copysw = 00:00:00.19.0, target = /sdcard/oculus/360Photos//storage/emulated/0/DCIM/Camera/20150624_120258.jpg }


                                    // I/System.Console(20294): 4f46:bf53 GetFileName /storage/emulated/0/DCIM/Camera/20150624_120258.jpg

                                    //Console.WriteLine("GetFileName " + Path.GetFileName(ff.FullName));

                                    var target = "/sdcard/oculus/360Photos/" + ff.Name;

                                    // 20ms
                                    var bytes = File.ReadAllBytes(ff.FullName);

                                    File.WriteAllBytes(target, bytes);

                                    Console.WriteLine(new { copysw, target });


                                    //f.co.CopyTo("sdcard/oculus/360Photos/" + ff.Name, true);

                                }

                                ButtonTitle += new { equirectangular };

                                Console.WriteLine(new { ButtonTitle });

                                // http://search.cpan.org/dist/Image-ExifTool-9.76/lib/Image/ExifTool/TagNames.pod#XMP_GPano_Tags
                                // http://www.panotwins.de/technical/how-to-add-mandatory-photo-sphere-meta-data-to-an-equirectangular-image/

                                //                            public virtual XMPMeta getXMPMeta();
                                //public virtual Map getXmpProperties();

                                //var xmpmeta = xmp.getXMPMeta();
                                //xmpmeta.dumpObject();

                                var pk = p.keySet();

                                var pka = pk.toArray();

                                for (int i = 0; i < pka.Length; i++)
                                {
                                    var key = pka[i];

                                    Console.WriteLine(key + ": " + p.get(key));

                                }

                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 0, key = GPano:ProjectionType, value = equirectangular }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 1, key = GPano:LargestValidInteriorRectTop, value = 0 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 2, key = GPano:CroppedAreaLeftPixels, value = 0 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 3, key = GPano:CroppedAreaTopPixels, value = 0 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 4, key = GPano:FullPanoHeightPixels, value = 3290 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 5, key = GPano:CroppedAreaImageHeightPixels, value = 3290 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 6, key = GPano:LargestValidInteriorRectLeft, value = 0 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 7, key = GPano:FullPanoWidthPixels, value = 6582 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 8, key = GPano:LargestValidInteriorRectHeight, value = 3290 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 9, key = GPano:FirstPhotoDate, value = 2015-07-08T08:04:44.035Z }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 10, key = GPano:CroppedAreaImageWidthPixels, value = 6582 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 11, key = GPano:UsePanoramaViewer, value = True }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 12, key = GPano:LargestValidInteriorRectWidth, value = 6582 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 13, key = GPano:LastPhotoDate, value = 2015-07-08T08:06:40.751Z }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 14, key = GPano:PoseHeadingDegrees, value = 300.4 }
                                //I/System.Console(10680): 29b8:b8f0 { ButtonTitle = /storage/emulated/0/DCIM/Camera/20150708_110444.jpg 6582:3290, i = 15, key = GPano:SourcePhotosCount, value = 64 }
                                //I/System.Console(10680): 29b8:b8f0 { ElapsedMilliseconds = 2442, Name = /storage/emulated/0/DCIM/Camera/20150708_110137.jpg, Length = 5759881 }
                            }


                            // NDK implement JDK implement CLR?
                            #region ExifThumbnailDirectory
                            var cExifThumbnailDirectory = typeof(com.drew.metadata.exif.ExifThumbnailDirectory).ToClass();
                            var containsDirectoryOfType = m.containsDirectoryOfType(cExifThumbnailDirectory);
                            if (containsDirectoryOfType)
                            {
                                var xExifThumbnailDirectory = (com.drew.metadata.exif.ExifThumbnailDirectory)m.getFirstDirectoryOfType(cExifThumbnailDirectory);

                                // in 3sec load thumbnails...
                                if (sw.ElapsedMilliseconds < 3000)
                                {
                                    //Console.WriteLine(
                                    //   f.getName()
                                    //);

                                    var data = (byte[])(object)xExifThumbnailDirectory.getThumbnailData();

                                    //Console.WriteLine(new { data });

                                    if (data != null)
                                    {
                                        src = "data:image/jpg;base64," +
                                           Convert.ToBase64String(
                                               data
                                           );



                                    }
                                }
                            }
                            #endregion


                            y(
                                ButtonTitle,
                                src
                            );
                        }

                        // we have static memory. on android..
                        __inspect[ButtonTitle] = delegate
                        {

                            //var m = ImageMetadataReader.readMetadata(f);

                            // http://stackoverflow.com/questions/10166373/metadata-extraction-java

                            //var t = typeof(com.drew.metadata.exif.ExifThumbnailDirectory).ToClass();



                            //com.drew.metadata.exif.ExifThumbnailDirectory.

                            var i = m.getDirectories().iterator();

                            while (i.hasNext())
                            {
                                var directory = (com.drew.metadata.Directory)i.next();
                                var tags = directory.getTags().toArray();

                                foreach (com.drew.metadata.Tag tag in tags)
                                {
                                    // https://developers.google.com/photo-sphere/metadata/

                                    // GPano:ProjectionType

                                    // -xmp:UsePanoramaViewer=True

                                    Console.WriteLine(new { ff.Name, dir = directory.getName(), tag = tag.getTagName() });
                                    // I/System.Console( 7296): 1c80:b6bc { Name = /storage/emulated/0/DCIM/Camera/20150708_133445.jpg, dir = Xmp, tag = XMP Value Count }

                                    if (directory.getName() == "Xmp")
                                    {
                                        //directory as Xmp
                                    }

                                    //tag.
                                    //y(new { tag }.ToString());


                                }



                            }
                        };



                    }
                    catch
                    {
                        //  Caused by: java.lang.RuntimeException: File format is not supported
                        throw;
                    }
                }
            );

            s.Stop();

            Console.WriteLine(
                new { s.Elapsed }
            );
        }

    }
}

//System.IO.FileNotFoundException: Could not find file 'W:/EXIFThumbnail.ApplicationWebService.AssetsLibrary.dll'.
//File name: 'W:/EXIFThumbnail.ApplicationWebService.AssetsLibrary.dll'
//   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
//   at System.IO.File.InternalCopy(String sourceFileName, String destFileName, Boolean overwrite, Boolean checkHost)
//   at System.IO.File.Copy(String sourceFileName, String destFileName)