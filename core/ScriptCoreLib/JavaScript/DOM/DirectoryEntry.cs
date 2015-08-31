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

        static Blob bytes;

        //public static Task WriteAllBytes(this DirectoryEntry that, string filename, WebGL.WebGLRenderingContext gl)
        //{
        //}

        public static Task WriteAllBytes(this DirectoryEntry that, string filename, IHTMLCanvas canvas)
        {
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
            canvas = null;

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


                              }
                          );

                            w.write(bytes);
                        }
                    );

                }
            );


            return c.Task;

        }

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
