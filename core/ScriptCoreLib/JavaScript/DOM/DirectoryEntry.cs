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
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeWriteFiles\ChromeWriteFiles\Application.cs

        //void getFile([TreatUndefinedAs=NullString] DOMString? path, optional FileSystemFlags options, optional EntryCallback successCallback, optional ErrorCallback errorCallback);
        public void getFile(string path, object options, Action<FileEntry> successCallback)
        {
            // can chrome do vhd?
        }
    }


    [Script]
    public static class DirectoryEntryExtensions
    {
        public static Task WriteAllBytes(this DirectoryEntry that, string filename, Blob bytes)
        {
            var c = new TaskCompletionSource<DirectoryEntry>();


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
                            w.write(bytes);


                            //w.write()


                            // ready?

                            c.SetResult(that);
                        }
                    );

                }
            );


            return c.Task;

        }
    }
}
