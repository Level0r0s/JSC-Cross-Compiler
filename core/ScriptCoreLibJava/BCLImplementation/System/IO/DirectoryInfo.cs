using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.DirectoryInfo))]
    internal class __DirectoryInfo : __FileSystemInfo
    {
        readonly string InternalPath;

        public __DirectoryInfo(string path)
        {
            this.InternalPath = path;
        }

        public override string Name
        {
            get { return Path.GetFileName(InternalPath); }
        }

        public override string FullName
        {
            get
            {
                return __Directory.__GetFullPath(InternalPath);
            }
        }

        public override bool Exists
        {
            get { return __File.Exists(FullName); }
        }

        public DirectoryInfo CreateSubdirectory(string path)
        {
            var f = new DirectoryInfo(Path.Combine(this.FullName, path));

            f.Create();

            return f;
        }

        public void Create()
        {
            Directory.CreateDirectory(this.FullName);
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public FileInfo[] GetFiles()
        {
            // Z:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosHUD\ApplicationActivity.cs
            return Directory.GetFiles(this.InternalPath).Select(x => new FileInfo(x)).ToArray();
        }


        public override string ToString()
        {
            return new { this.FullName, this.Exists }.ToString();
        }
    }
}
