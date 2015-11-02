using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Ultra.WebService
{
    public static class InternalCassiniClientCertificateLoader
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20151102
        // set by?
        // Z:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\TcpListenerExtensions.cs

        public static void Invoke(InternalGlobal g)
        {
            // did we ask the user for PIN ?


            var path = new FileInfo("ClientCertificate.crt");
            if (path.Exists)
            {
                try
                {
                    Console.WriteLine("enter InternalCassiniClientCertificateLoader "
                        + new { path.FullName }
                        //+ new { Environment.CurrentDirectory, typeof(InternalCassiniClientCertificateLoader).Assembly.Location }
                    );

                    // 'System.Security.Cryptography.X509Certificates.X509Certificate' to type 'System.Security.Cryptography.X509Certificates.X509Certificate2'.


                    var crt = new System.Security.Cryptography.X509Certificates.X509Certificate2();

                    crt.Import(File.ReadAllBytes(path.FullName));

                    g.ClientCertificate = crt;

                }
                catch (Exception err)
                {
                    Console.WriteLine(new { err });

                    throw;
                }
                finally
                {
                    File.Delete(path.FullName);
                }
            }

        }
    }
}
