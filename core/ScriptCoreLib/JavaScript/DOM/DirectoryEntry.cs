using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.WebGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/FileEntry.idl
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/DirectoryEntry.idl

    // .WebFileSystem
    // .IOFileSystem ?
    [Script(HasNoPrototype = true, ExternalTarget = "")]
    public class DirectoryEntry : Entry
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\Application.cs

        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\Application.cs

        //void getFile([TreatUndefinedAs=NullString] DOMString? path, optional FileSystemFlags options, optional EntryCallback successCallback, optional ErrorCallback errorCallback);
        public void getFile(string path, object options, Action<FileEntry> successCallback)
        {
            // can chrome do vhd?
        }

        //void getDirectory([TreatUndefinedAs=NullString] DOMString? path, optional FileSystemFlags options, optional EntryCallback successCallback, optional ErrorCallback errorCallback);
        public void getDirectory(string path, object options, Action<DirectoryEntry> successCallback)
        {
            // can chrome do vhd?
        }


        public DirectoryReader createReader()
        {
            return null;
        }
    }


    [Script]
    public static class DirectoryEntryExtensions
    {
        //public static Task WriteAllBytes(this DirectoryEntry that, string filename, IHTMLCanvas canvas)
        //{
        //    //var data = canvas.toDataURL(quality: 0.1);
        //    var data = canvas.toDataURL(quality: 0.1);

        //    //var fileBytes = System.Convert.FromBase64String(data.SkipUntilOrEmpty("base64,"));

        //    var prefix = "base64,";



        //    var fileBytes = System.Convert.FromBase64String(
        //        data.Substring(data.IndexOf(prefix) + prefix.Length));

        //    Blob blob = fileBytes;

        //    var c = new TaskCompletionSource<DirectoryEntry>();


        //    that.getFile(
        //        //"0000.jpg",
        //        filename,
        //        new
        //        {
        //            create = true,
        //            exclusive = false
        //        },
        //        fentry =>
        //        {
        //            // {{ fentry = [object FileEntry] }}
        //            //new IHTMLPre { new { fentry } }.AttachToDocument();


        //            fentry.createWriter(
        //                w =>
        //                {

        //                    //new IHTMLPre { new { w } }.AttachToDocument();

        //                    // new Blob([document.getElementById("HTMLFile").value],
        //                    //{ type: 'text/plain'}

        //                    //var blob = new Blob(
        //                    //    blobParts: new ArrayBufferView[] { fileBytes },
        //                    //    options: new { type = "application/octet-stream" }
        //                    //);

        //                    // http://stackoverflow.com/questions/12168909/blob-from-dataurl
        //                    //w.write(fileBytes);

        //                    w.onwriteend = new Action(
        //                        delegate
        //                        {
        //                            blob.close();
        //                            blob = null;

        //                            w.truncate(w.position);
        //                            w = null;



        //                            c.SetResult(that);

        //                        }
        //                    );

        //                    w.write(blob);


        //                    //w.write()


        //                    // ready?


        //                }
        //            );

        //        }
        //    );


        //    return c.Task;
        //}





        // Refcount: 1
        // chrome://blob-internals/
        static Blob bytes;

        //public static Task WriteAllBytes(this DirectoryEntry that, string filename, WebGL.WebGLRenderingContext gl)
        //{
        //}

        public static Task WriteAllBytes(this DirectoryEntry that, string filename, IHTMLCanvas canvas)
        {
            //            754986ms WriteAllBytes { filename = 00272.png, position = 3101246, size = 3101246 }
            //view-source:63706 FileError is deprecated. Please use the 'name' or 'message' attributes of DOMError rather than 'code'.
            //view-source:54105 757622ms WriteAllBytes onerror { code = 7, message = An operation that depends on state cached in an interface object was made but the state had changed since it was read from disk., error = [object FileError] }
            //view-source:54105 757623ms out of files?
            //view-source:54105 757624ms WriteAllBytes { filename = 00273.png, position = 0, size = 3143207 }
            //view-source:54105 757624ms what happened? retry?
            //view-source:54105 760625ms what happened? retry!
            //view-source:54105 760626ms what happened? retry! retry7?
            //view-source:38832 Uncaught TypeError: Cannot read property 'toDataURL' of null

            var data = canvas.toDataURL();
            //var data = canvas.toDataURL(quality: 0.1);

            var prefix = "base64,";



            var fileBytes = System.Convert.FromBase64String(
                data.Substring(data.IndexOf(prefix) + prefix.Length));

            // https://groups.google.com/a/chromium.org/forum/#!topic/blink-dev/jcrEI_jfYFs
            // this blob wont delete?
            bytes = new Blob(
                blobParts: new ArrayBufferView[] { fileBytes },
                options: new { type = "application/octet-stream;" + filename }
            );

            fileBytes = null;
            data = null;
            //canvas = null;

            var c = new TaskCompletionSource<DirectoryEntry>();


            // can do up to 120 files?
            // not disposing blob?


            //Blob blob = bytes;

            that.getFile(
                //"0000.jpg",
                filename,
                new
                {
                    create = true,
                    exclusive = false
                },
                fentry =>
                {
                    // {{ fentry = [object FileEntry] }}
                    //new IHTMLPre { new { fentry } }.AttachToDocument();


                    fentry.createWriter(
                        w =>
                        {

                            //new IHTMLPre { new { w } }.AttachToDocument();

                            // new Blob([document.getElementById("HTMLFile").value],
                            //{ type: 'text/plain'}

                            //var blob = new Blob(
                            //    blobParts: new ArrayBufferView[] { fileBytes },
                            //    options: new { type = "application/octet-stream" }
                            //);

                            // http://stackoverflow.com/questions/12168909/blob-from-dataurl
                            //w.write(fileBytes);
                            //w.write(bytes);


                            ////w.write()


                            //// ready?


                            var retry7 = false;

                            //c.SetResult(that);
                            //WriteAllBytes onerror { error = [object FileError], code = 7 }
                            w.onerror = new Action(
                                delegate
                                {
                                    // https://developer.mozilla.org/en-US/docs/Web/API/FileError

                                    Console.WriteLine("WriteAllBytes onerror " + new { w.error.code, w.error.message, w.error });

                                    if (w.error.code == 7)
                                    {
                                        Console.WriteLine("out of files? " + new { filename });
                                        //https://code.google.com/p/chromium/issues/detail?id=83736
                                        retry7 = true;
                                    }
                                }
                            );



                            //1063359ms WriteAllBytes onerror { code = 7, message = An operation that depends on state cached in an interface object was made but the state had changed since it was read from disk., error = [object FileError] }
                            //view-source:54104 1063360ms out of files?
                            //view-source:54104 1063361ms WriteAllBytes { filename = 00265.png, position = 0, size = 3058562 }
                            //view-source:54104 1063361ms what happened? retry?
                            //view-source:54104 1066363ms what happened? retry!


                            w.onwriteend = new Action(
                              delegate
                              {
                                  var position = w.position;

                                  Console.WriteLine("onwriteend WriteAllBytes " + new { filename, position, bytes.size });

                                  if (position < (long)bytes.size)
                                  {


                                      Console.WriteLine("what happened? retry? " + new { filename });

                                      if (position == 0)
                                      {



                                          Native.setTimeout(
                                              delegate
                                              {

                                                  if (retry7)
                                                  {
                                                      Console.WriteLine("what happened? retry7? " + new { filename });


                                                      WriteAllBytes(that, filename, canvas).ContinueWith(
                                                          delegate
                                                          {
                                                              Console.WriteLine("what happened? retry7! " + new { filename });
                                                              c.SetResult(that);
                                                              fentry = null;
                                                          }
                                                      );


                                                      return;
                                                  }
                                                  Console.WriteLine("what happened? retry! " + new { filename });

                                                  w.write(bytes);
                                              }, 5000
                                            );
                                      }

                                      return;
                                  }

                                  // https://groups.google.com/a/chromium.org/forum/#!topic/chromium-html5/6Behx6zrbCI
                                  //Console.WriteLine("WriteAllBytes Blob close");

                                  // https://code.google.com/p/chromium/issues/detail?id=404301

                                  // chrome://blob-internals/

                                  //bytes.close();
                                  bytes = null;

                                  //w.truncate(w.position);
                                  w = null;


                                  // need it?
                                  //Native.window.requestAnimationFrame += delegate


                                  //Console.WriteLine("WriteAllBytes yield");
                                  c.SetResult(that);

                                  fentry = null;
                              }
                          );

                            w.write(bytes);
                        }
                    );

                }
            );


            return c.Task;

        }

        [Obsolete("leak?")]
        public static Task WriteAllBytes(this DirectoryEntry that, string filename, Blob bytes)
        {
            // can do up to 120 files?
            // not disposing blob?

            var c = new TaskCompletionSource<DirectoryEntry>();

            //Blob blob = bytes;

            that.getFile(
                //"0000.jpg",
                filename,
                new
                {
                    create = true,
                    exclusive = false
                },
                fentry =>
                {
                    // {{ fentry = [object FileEntry] }}
                    //new IHTMLPre { new { fentry } }.AttachToDocument();


                    fentry.createWriter(
                        w =>
                        {

                            //new IHTMLPre { new { w } }.AttachToDocument();

                            // new Blob([document.getElementById("HTMLFile").value],
                            //{ type: 'text/plain'}

                            //var blob = new Blob(
                            //    blobParts: new ArrayBufferView[] { fileBytes },
                            //    options: new { type = "application/octet-stream" }
                            //);

                            // http://stackoverflow.com/questions/12168909/blob-from-dataurl
                            //w.write(fileBytes);
                            //w.write(bytes);


                            ////w.write()


                            //// ready?

                            //c.SetResult(that);

                            w.onerror = new Action(
                                delegate
                                {
                                    // https://developer.mozilla.org/en-US/docs/Web/API/FileError

                                    Console.WriteLine("WriteAllBytes onerror " + new { w.error, w.error.code });

                                }
                            );

                            w.onwriteend = new Action(
                              delegate
                              {
                                  var position = w.position;

                                  Console.WriteLine("WriteAllBytes " + new { filename, position, bytes.size });

                                  if (position < (long)bytes.size)
                                  {
                                      Console.WriteLine("what happened? retry?");

                                      if (position == 0)
                                      {
                                          Native.setTimeout(
                                              delegate
                                              {
                                                  Console.WriteLine("what happened? retry!");

                                                  w.write(bytes);
                                              }, 3000
                                            );
                                      }

                                      return;
                                  }

                                  // https://groups.google.com/a/chromium.org/forum/#!topic/chromium-html5/6Behx6zrbCI
                                  Console.WriteLine("WriteAllBytes Blob close");

                                  // https://code.google.com/p/chromium/issues/detail?id=404301

                                  // chrome://blob-internals/

                                  //bytes.close();
                                  bytes = null;

                                  //w.truncate(w.position);
                                  w = null;


                                  // need it?
                                  //Native.window.requestAnimationFrame += delegate


                                  Console.WriteLine("WriteAllBytes yield");
                                  c.SetResult(that);


                              }
                          );

                            w.write(bytes);
                        }
                    );

                }
            );


            return c.Task;

        }
    }
}


//644242ms WriteAllBytes { filename = 00251.png, position = 3239430, size = 3239430 }
//view-source:63705 FileError is deprecated. Please use the 'name' or 'message' attributes of DOMError rather than 'code'.
//view-source:54104 646748ms WriteAllBytes onerror { code = 7, message = An operation that depends on state cached in an interface object was made but the state had changed since it was read from disk., error = [object FileError] }
//view-source:54104 646748ms out of files?
//view-source:54104 646749ms WriteAllBytes { filename = 00252.png, position = 0, size = 3259041 }
//view-source:54104 646750ms what happened? retry?
//view-source:54104 649751ms what happened? retry!
//view-source:54104 649751ms what happened? retry! retry7?
//view-source:54104 651462ms WriteAllBytes onerror { code = 7, message = An operation that depends on state cached in an interface object was made but the state had changed since it was read from disk., error = [object FileError] }
//view-source:54104 651462ms out of files?
//view-source:54104 651463ms WriteAllBytes { filename = 00252.png, position = 0, size = 3259041 }
//view-source:54104 651464ms what happened? retry?
//view-source:54104 651465ms WriteAllBytes onerror { code = 7, message = An operation that depends on state cached in an interface object was made but the state had changed since it was read from disk., error = [object FileError] }
//view-source:54104 651465ms out of files?
//view-source:54104 651466ms WriteAllBytes { filename = 00252.png, position = 0, size = 3259041 }
//view-source:54104 651466ms what happened? retry?
//view-source:54104 654464ms what happened? retry!
//view-source:54104 654465ms what happened? retry! retry7?
//view-source:54104 656158ms what happened? retry!
//view-source:54104 656159ms what happened? retry! retry7?
//view-source:54104 657867ms WriteAllBytes onerror { code = 7, message = An operation that depends on state cached in an interface object was made but the state had changed since it was read from disk., error = [object FileError] }
//view-source:54104 657868ms out of files?
//view-source:54104 657869ms WriteAllBytes { filename = 00252.png, position = 0, size = 3259041 }
//view-source:54104 657870ms what happened? retry?
//view-source:54104 657871ms WriteAllBytes { filename = 00252.png, position = 3259041, size = 3259041 }
//view-source:54104 657872ms what happened? retry! retry7!
//view-source:54104 660393ms WriteAllBytes { filename = 00253.png, position = 3244947, size = 3244947 }
//view-source:54104 663195ms what happened? retry!
//view-source:54104 663196ms what happened? retry! retry7?
//view-source:54104 664777ms WriteAllBytes { filename = 00252.png, position = 3218882, size = 3218882 }