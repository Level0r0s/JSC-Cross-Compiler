using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/modules/filesystem/DirectoryReader.idl

    [Script(HasNoPrototype = true, ExternalTarget = "DirectoryReader")]
    public class DirectoryReader : IEventTarget
    {
        // Z:\jsc.svn\examples\javascript\chrome\apps\ChromeReadFiles\Application.cs

        //void readEntries(EntriesCallback successCallback, optional ErrorCallback errorCallback);
        //public void readEntries(Action<object> successCallback) { }
        public void readEntries(Action<Entry[]> successCallback) { }

        // extended by
        // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\DOM\FileExtensions.cs
    }

    //    [object DirectoryReader]
    //readEntries 0
    //readEntries 0 done 
    //readEntries 0 done {{ item = [object FileEntry] }}
    //readEntries 0 done {{ item = [object FileEntry] }}
    //readEntries 1
    //readEntries 1 done {{ entries1isnull = false }}
    //readEntries 1 done {{ Length = 0 }}


    [Script]
    public static class DirectoryReaderExtensions
    {
        public static Task<IEnumerable<FileEntry>> readFileEntries(this DirectoryReader that)
        {
            var a = new List<FileEntry>();

            var c = new TaskCompletionSource<IEnumerable<FileEntry>>();


            Action yield = null;


            yield = delegate
            {
                that.readEntries(
                    entries =>
                    {
                        if (entries.Length == 0)
                        {
                            c.SetResult(a);
                            return;
                        }

                        //a.AddRange(entries.AsEnumerable());

                        foreach (var item in entries)
                        {
                            if (item.isFile)
                            {
                                var f = (FileEntry)item;

                                a.Add(f);
                            }
                        }

                        yield();
                    }
                );
            };

            yield();

            return c.Task;
        }
    }
}
