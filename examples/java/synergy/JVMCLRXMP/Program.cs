using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using com.drew.imaging;
using System.IO;
using ScriptCoreLib.Shared.IO;
using System.Diagnostics;

namespace JVMCLRXMP
{


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140908/rsa-assetslibrary
            // start /MIN /WAIT cmd /C C:\util\jsc\bin\jsc.meta.exe ReferenceAssetsLibrary /ProjectFileName:"$(ProjectPath)" /NamedKeyPairs:JVMToCLR /NamedKeyPairs:CLRToJVM

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            // how do we use it?
            // first lets show the api is useable.

            // are we a GUI java app?
            //var zImageMetadataReader = typeof(com.drew.imaging.ImageMetadataReader);
            //// { zImageMetadataReader = com.drew.imaging.ImageMetadataReader }

            //Console.WriteLine(new { zImageMetadataReader });

            //<package id="AndroidMetadataExtractor" version="1.0.0.0" targetFramework="net40" />
            //var m = ImageMetadataReader.readMetadata(new File(filepath));

            var f = @"x:\vr\androidlensblur\IMG_20151205_150024.vr.jpg";
            //var f = @"x:\vr\androidlensblur\IMG_20151205_152513.vr.jpg";
            try
            {
                // could jsc have ctors act as implict operators yet?

                Action<com.adobe.xmp.XMPMeta> AtXMPMeta = XMPMeta =>
                {
                    try
                    {
                        var XMPMetai = XMPMeta.iterator();
                        //Console.WriteLine(new { getXMPMeta = XMPMeta.size() });

                        Console.WriteLine();

                        while (XMPMetai.hasNext())
                        {
                            var xXMPPropertyInfo = XMPMetai.next() as com.adobe.xmp.properties.XMPPropertyInfo;

                            //string getNamespace();
                            //     PropertyOptions getOptions();
                            //     string getPath();
                            //     string getValue();

                            var Namespace = xXMPPropertyInfo.getNamespace();
                            var Path = xXMPPropertyInfo.getPath();
                            var Value = xXMPPropertyInfo.getValue();


                            //{ Namespace = http://ns.google.com/photos/1.0/image/, Path = , Value =  }
                            //{ Namespace = http://ns.google.com/photos/1.0/image/, Path = GImage:Data, Value = /9j/4AAQS

                            Console.WriteLine(new { Namespace, Path, Value });


                            if (Path == "GImage:Data")
                            {
                                //   GImage:Mime="image/jpeg"
                                var GImageDataBytes = Convert.FromBase64String(Value);


                                // image broken?
                                File.WriteAllBytes("GImageDataBytes.jpg", GImageDataBytes);

                            }


                            // http://stackoverflow.com/questions/1650983/streaming-aac-audio-with-android
                            if (Path == "GAudio:Data")
                            {
                                //  GAudio:Mime="audio/mp4a-latm"
                                var GAudioDataBytes = Convert.FromBase64String(Value);

                                // whats the format?
                                //File.WriteAllBytes("GAudioDataBytes.mp3", GAudioDataBytes);

                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }

                };


                var bytes = System.IO.File.ReadAllBytes(f);
                var s = new SmartStreamReader(new MemoryStream(bytes));
                var s2 = new SmartStreamReader(new MemoryStream(bytes));


                var segmentindex = 0;

                var xmpmeta_open = "<x:xmpmeta";
                var xmpmeta_close = "</x:xmpmeta>";

                var ok = true;
                while (ok)
                {
                    ok = false;

                    //Console.WriteLine("before ReadToBoundary");
                    var segment1 = s.ReadToBoundary(xmpmeta_open);
                    //Console.WriteLine("after ReadToBoundary");
                    //Console.WriteLine(new { segment1 });

                    if (segment1 != null)
                    {
                        var segment1close = s.ReadToBoundary(xmpmeta_close);
                        //Console.WriteLine(new { segment1close });

                        if (segment1close != null)
                        {
                            Console.WriteLine("segment1 " + new { segment1close.Length });

                            ok = segment1close.Length > 0;

                            if (ok)
                            {
                                var xmpmeta_close_bytes = Encoding.UTF8.GetBytes(xmpmeta_close);

                                segment1close.Write(xmpmeta_close_bytes, 0, xmpmeta_close_bytes.Length);

                                //var xmpmeta_bytes = segment1close.ToArray().Concat(Encoding.UTF8.GetBytes(xmpmeta_close)).ToArray();
                                var xmpmeta_bytes = segment1close.ToArray();
                                //var xmpmeta_string = xmpmeta_open + Encoding.UTF8.GetString(segment1close.ToArray()) + xmpmeta_close;
                                //var xmpmeta_string = Encoding.UTF8.GetString(segment1close.ToArray()) + xmpmeta_close;
                                var xmpmeta_string = Encoding.UTF8.GetString(xmpmeta_bytes);

                                //File.WriteAllText("xmpmeta." + segmentindex + ".xml", xmpmeta_string);
                                //File.WriteAllBytes("xmpmeta." + segmentindex + ".xml", xmpmeta_bytes);
                                // its in chunks.

                                //Console.WriteLine(new { segmentindex, xmpmeta_string });
                                //Console.WriteLine(new { segmentindex });


                                //com.adobe.xmp.XMPMeta meta = com.adobe.xmp.XMPMetaFactory.parseFromBuffer(
                                //    (sbyte[])(object)xmpmeta_bytes
                                //);

                                //Console.WriteLine(new { segmentindex, meta });


                                //AtXMPMeta(meta);

                                //{ fault = System.Xml.XmlException: '.', hexadecimal value 0x00, is an invalid character. Line 6, position 65122.
                                //   at System.Xml.XmlTextReaderImpl.Throw(Exception e)
                                //   at System.Xml.XmlTextReaderImpl.Throw(String res, String[] args)
                                //   at System.Xml.XmlTextReaderImpl.ThrowInvalidChar(Char[] data, Int32 length, Int32 invCharPos)
                                //   at System.Xml.XmlTextReaderImpl.ParseAttributeValueSlow(Int32 curPos, Char quoteChar, NodeData attr)


                                var xmpmeta = XElement.Parse(xmpmeta_string);

                                var HasExtendedXMP = xmpmeta.Elements().First().Elements().First().Attributes().FirstOrDefault(x => x.Name.LocalName == "HasExtendedXMP");


                                // Console.WriteLine(new { xmpmeta });


                                //                                Image cotains XMP section, 65460 bytes long
                                //??http://ns.adobe.com/xmp/extension/?E7161367CA4C162658BE105E785E9D37?U??????<x:xmpmeta xmlns:x="ado
                                //be:ns:meta/" x:xmptk="Adobe XMP Core 5.1.0-jc003">

                                // http://ns.adobe.com/xmp/extension/?E
                                //var boundary = "http://ns.adobe.com/xmp/extension/?" + HasExtendedXMP.Value;
                                //var boundary = "http://ns.adobe.com/xmp/extension/";
                                var boundary = HasExtendedXMP.Value;


                                // 475348120A63AE3C4D312022600647AA

                                var skip0 = s.ReadToBoundary(boundary);



                                var chunkstream = new MemoryStream { };

                                do
                                {
                                    var take0 = s.ReadToBoundary(boundary);

                                    //Console.WriteLine(new { take0.Length });

                                    // kStdXMPLimit 

                                    var offset1 = boundary.Length + 8;


                                    if (take0.Length > 65462)
                                    {
                                        // last chunk?

                                        // 		take0.ReadToEnd()	'System.IO.MemoryStream' does not contain a definition for 'ReadToEnd' and no extension method 'ReadToEnd' accepting a first argument of type 'System.IO.MemoryStream' could be found (are you missing a using directive or an assembly reference?)	

                                        //var x = take0.ReadToEnd();

                                        var s3 = new SmartStreamReader(new MemoryStream(take0.ToArray()));
                                        var s3x = s3.ReadToBoundary(xmpmeta_close);


                                        //var xs = Encoding.UTF8.GetString(
                                        //    take0.ToArray(),
                                        //    boundary.Length + 8,
                                        //    (int)take0.Length - (boundary.Length + 8)
                                        //    );

                                        chunkstream.Write(
                                           s3x.ToArray(),
                                           offset1,
                                           (int)s3x.Length - offset1
                                       );

                                        break;
                                    }

                                    if (take0.Length == 0)
                                        break;

                                    // 		(char)take0.ToArray()[boundary.Length + 8]	60 '<'	char
                                    // 		(char)take0.ToArray()[take0.Length - "http://ns.adobe.com/xmp/extension/".Length - 6]	74 'J'	char


                                    var offset2 = (int)take0.Length - "http://ns.adobe.com/xmp/extension/".Length - 6;

                                    chunkstream.Write(
                                        take0.ToArray(),
                                        offset1,
                                        offset2 - offset1 + 1
                                    );



                                }
                                while (true);

                                var xmpmeta2_string = Encoding.UTF8.GetString(chunkstream.ToArray()) + xmpmeta_close;
                                //var xmpmeta = XElement.Parse(xmpmeta_string);
                                var xmpmeta2 = XElement.Parse(xmpmeta2_string);

                                //File.WriteAllBytes("xmpmeta." + segmentindex + ".jpg", chunkstream.tos);

                                //xmpmeta2.Elements().First().Elements().First().Attributes().ToArray()[3].Name.LocalName	"Data"	string
                                //xmpmeta2.Elements().First().Elements().First().Attributes().ToArray()[3].Name.NamespaceName	"http://ns.google.com/photos/1.0/image/"	string

                                var GAudioData = xmpmeta2.Elements().First().Elements().First().Attributes().ToArray()[4];
                                var GAudioDataBytes = Convert.FromBase64String(GAudioData.Value);

                                File.WriteAllBytes(new FileInfo(f).Name + ".GAudioDataBytes.mp4", GAudioDataBytes);

                                var GImageData = xmpmeta2.Elements().First().Elements().First().Attributes().ToArray()[3];
                                var GImageDataBytes = Convert.FromBase64String(GImageData.Value);

                                File.WriteAllBytes(new FileInfo(f).Name + ".GImageDataBytes.jpg", GImageDataBytes);

                                // http://www.adobe.com/content/dam/Adobe/en/devnet/xmp/pdfs/XMPSpecificationPart3.pdf
                                // keep only 1 section thanks.
                                ok = false;
                                Debugger.Break();
                            }

                            //java.lang.Object, rt
                            //{ zImageMetadataReader = com.drew.imaging.ImageMetadataReader }
                            //{ segment1 = ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream@19b1de }
                            //{ segment1close = ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream@1d10a5c }
                            //segment1 { Length = 860 }
                            //{ segment1 = ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream@50988 }
                            //{ segment1close = ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream@49d67c }
                            //segment1 { Length = 2218768 }
                            //{ segment1 = ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream@1a42792 }
                            //{ segment1close = ScriptCoreLibJava.BCLImplementation.System.IO.__MemoryStream@2200d5 }
                            //segment1 { Length = 0 }
                            //{ DirectoryCount = 6 }

                        }
                    }

                    segmentindex++;
                }


                // segment 2 ?
                //var segment2 = s.ReadToBoundary("<x:xmpmeta");
                //Console.WriteLine("segmen1 " + new { segment2 });


#if !DEBUG

                var m = ImageMetadataReader.readMetadata(new java.io.File(f));

                // now what?
                // can we find the additional data feed ?

                var DirectoryCount = m.getDirectoryCount();

                Console.WriteLine(new { DirectoryCount });

                var i = m.getDirectories();
                var ii = i.iterator();

                //{ zImageMetadataReader = com.drew.imaging.ImageMetadataReader }
                //{ DirectoryCount = 6 }
                //{ current = JPEG Directory (8 tags) }
                //{ current = Exif IFD0 Directory (8 tags) }
                //{ current = GPS Directory (2 tags) }
                //{ current = Exif SubIFD Directory (2 tags) }
                //{ current = Xmp Directory (1 tag) }
                //{ current = File Directory (3 tags) }
                //done


                //                java.lang.Object, rt
                //{ zImageMetadataReader = com.drew.imaging.ImageMetadataReader }
                //{ DirectoryCount = 6 }
                //{ xDirectory = JPEG Directory (8 tags) }
                //{ TagCount = 8 }
                //{ xDirectory = Exif IFD0 Directory (8 tags) }
                //{ TagCount = 8 }
                //{ xDirectory = GPS Directory (2 tags) }
                //{ TagCount = 2 }
                //{ xDirectory = Exif SubIFD Directory (2 tags) }
                //{ TagCount = 2 }

                //{ xDirectory = Xmp Directory (1 tag) }
                //{ TagCount = 1 }
                //{ xXmpDirectory = Xmp Directory (1 tag) }

                //{ xDirectory = File Directory (3 tags) }
                //{ TagCount = 3 }
                //done





                while (ii.hasNext())
                {
                    var current = ii.next();
                    var xDirectory = current as com.drew.metadata.Directory;

                    Console.WriteLine();
                    Console.WriteLine(new { xDirectory });


                    var TagCount = xDirectory.getTagCount();
                    Console.WriteLine(new { TagCount });

                    var taga = xDirectory.getTags();
                    var tagii = taga.iterator();


                    while (tagii.hasNext())
                    {
                        var tagcurrent = tagii.next();
                        //var tagcurrentt = typeof(tagcurrent);
                        //var tagcurrentt = Type.GetTypeFromHandle(tagcurrent);
                        var tagcurrentt = tagcurrent.GetType();



                        var xTag = tagcurrent as com.drew.metadata.Tag;

                        //Console.WriteLine(new { tagcurrent, tagcurrentt });
                        Console.WriteLine(new { tagcurrent, xTag });
                    }

                    var xXmpDirectory = xDirectory as com.drew.metadata.xmp.XmpDirectory;
                    if (xXmpDirectory != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine(new { xXmpDirectory });


                        // should see mime tags?

                        //public virtual Map getXmpProperties();

                #region XmpProperties
                        var XmpProperties = xXmpDirectory.getXmpProperties();

                        Console.WriteLine(new { XmpProperties = XmpProperties.size() });

                        //for (int i = 0; i < XmpProperties.size(); i++)

                        var ksi = XmpProperties.keySet().iterator();
                        while (ksi.hasNext())
                        {
                            var ckey = ksi.next();
                            var cvalue = XmpProperties.get(ckey);

                            Console.WriteLine(new { ckey, cvalue });
                        }
                #endregion

                        // this is only the first one? there is more?
                        // http://stackoverflow.com/questions/23253281/reading-jpg-files-xmp-metadata
                        var XMPMeta = xXmpDirectory.getXMPMeta();

                        AtXMPMeta(XMPMeta);
                    }
                }


#endif
            }
            catch (Exception fault)
            {

                Console.WriteLine(new { fault });
                //throw;
            }

            Console.WriteLine("done");
            Console.ReadLine();

            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );

            //// how shall we join two private keys into a nice encryption channel?
            //var z = NamedKeyPairs.JVMToCLR.PublicKey.Encrypt("data");

            //// decrypt in clr?
            //var zz = NamedKeyPairs.CLRToJVM.PrivateKey

            MessageBox.Show("click to close");

        }
    }


}
