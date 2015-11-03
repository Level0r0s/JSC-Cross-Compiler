using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Shared;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace TestWebServiceRSA
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        // non fOAEP?
        //var MaxData = (RSA.KeySize - 384) / 8 + 37;

        // fOAEP needs the last byte?
        public static readonly Func<int, int> dwKeySizeFromMaxData = (int MaxData) => 384 + 8 * (MaxData - 7);
        // { ElapsedMilliseconds = 0, KeySize = 392, maxidata = 7, maxodata = 48, odata = 49, itext = 7 }
        public static readonly Func<int, int> MaxDataFromdwKeySize = (int dwKeySize) => (dwKeySize - 384) / 8 + 7;

        public static readonly Func<int, int> dwKeySizeToEncryptedDataLength = (int dwKeySize) => MaxDataFromdwKeySize(dwKeySize) + 41;

        public static void Main(string[] args)
        {
            // Z:\jsc.svn\examples\javascript\android\Test\TestAndroidWebCryptoKeyImport\TestAndroidWebCryptoKeyImport\Application.cs


            // jvm wants 512bits keys
            // jvmMinKeySize = 23
            var jvmMinKeySize = MaxDataFromdwKeySize(512);

            var keygensw = Stopwatch.StartNew();

            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(4096));
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(2048));
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(256));
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(1024));
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(64)); // 105
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(65)); // 106
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(49)); // 90
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(39)); // 80

            var MaxData = 7;
            var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(MaxData)); // 50
            //var rsa = new RSACryptoServiceProvider(dwKeySize: dwKeySizeFromMaxData(4)); // error

            // Additional information: Invalid flags specified.
            var struct_rsaprivate = rsa.ExportParameters(includePrivateParameters: true);
            var bytes_rsaprivate = rsa.ExportCspBlob(includePrivateParameters: true);

            keygensw.Stop();

            // { ElapsedMilliseconds = 33, KeySize = 840 }
            // { ElapsedMilliseconds = 103558, KeySize = 8520 }
            // { ElapsedMilliseconds = 35335, KeySize = 8520 }
            Console.WriteLine(new { keygensw.ElapsedMilliseconds, rsa.KeySize, maxdata = MaxDataFromdwKeySize(rsa.KeySize) });

            var fOAEP = true;

            // Additional information: Bad Length.
            //var data = rsa.Encrypt(Encoding.UTF8.GetBytes("".PadLeft(37, '?')), fOAEP: fOAEP);
            var data = rsa.Encrypt(Encoding.UTF8.GetBytes("".PadLeft(MaxData - 1, '?')), fOAEP: fOAEP);
            var datadecrypt = rsa.Decrypt(data, fOAEP: fOAEP);

            // http://stackoverflow.com/questions/1199058/how-to-use-rsa-to-encrypt-files-huge-data-in-c-sharp

            // data = {byte[256]}

            var rsa2 = new RSACryptoServiceProvider();
            //rsa2.ImportParameters(struct_rsaprivate);
            rsa2.ImportCspBlob(bytes_rsaprivate);

            var decryptsw = Stopwatch.StartNew();
            //var data2 = rsa2.Decrypt(data, fOAEP: false);

            // Additional information: Error occurred while decoding OAEP padding.
            var data2 = rsa2.Decrypt(data, fOAEP: fOAEP);
            var text2 = Encoding.UTF8.GetString(data2);
            decryptsw.Stop();


            // { ElapsedMilliseconds = 0, Length = 105, text2 = hello world }
            Console.WriteLine(new
            {
                decryptsw.ElapsedMilliseconds,
                rsa.KeySize,
                maxidata = MaxDataFromdwKeySize(rsa.KeySize),
                maxodata = dwKeySizeToEncryptedDataLength(rsa.KeySize),
                odata = data.Length,
                itext = text2.Length
            });
            // rsa cannot encrypt too much data huh?
            // unless we have a bigger key?

            //{ ElapsedMilliseconds = 0, KeySize = 384, maxidata = 7, maxodata = 48, odata = 48, itext = 37 }




            // this is how we rsa encrypt with small key
            var s = new RSACryptoStream(rsa2);

            var sdata = s.EncryptString("hello world".PadRight(128, ' '));
            var stext = s.DecryptString(sdata);

            // now how do we sign it?
            // http://blogs.msdn.com/b/alejacma/archive/2008/06/25/how-to-sign-and-verify-the-signature-with-net-and-a-certificate-c.aspx

            // https://msdn.microsoft.com/en-us/library/9tsc5d0z(v=vs.110).aspx

            var sign3 = rsa2.SignData(data2, new SHA1CryptoServiceProvider());


            var stextu = stext.ToUpper();
            var verify3 = rsa2.VerifyData(Encoding.UTF8.GetBytes(stextu), new SHA1CryptoServiceProvider(), sign3);

            //verify3 = false
            // verify3 = true

            // Z:\jsc.svn\examples\java\hybrid\JVMCLRRSA\JVMCLRRSA\Program.cs

            // +		InnerException	{"Common Language Runtime detected an invalid program."}	System.Exception {System.InvalidProgramException}

            //var rsa3 = new RSACryptoStream(NamedKeyPairs.Key1PrivateKey.CSPBlob);
            var rsa3 = new RSACryptoStream(NamedKeyPairs.Key1PrivateKey.RSAParameters);

            Console.WriteLine(new { rsa3.KeySize, rsa3.EncryptedDataChunkSize });

            var rsa3e = rsa3.EncryptString(("hello world " + new { rsa3.rsa.KeySize }).PadRight(1024));
            var rsa3t = rsa3.DecryptString(rsa3e);

            // Z:\jsc.svn\examples\javascript\Test\TestWebCrypto\TestWebCrypto\Application.cs

            //at System.Security.Cryptography.CryptographicException.ThrowCryptographicException(Int32 hr)
            //at System.Security.Cryptography.Utils.SignValue(SafeKeyHandle hKey, Int32 keyNumber, Int32 calgKey, Int32 calgHash, Byte[] hash, Int32 cbHash, ObjectHandleOnStack retSignature)
            //at System.Security.Cryptography.Utils.SignValue(SafeKeyHandle hKey, Int32 keyNumber, Int32 calgKey, Int32 calgHash, Byte[] hash)
            //at System.Security.Cryptography.RSACryptoServiceProvider.SignHash(Byte[] rgbHash, Int32 calgHash)
            //at System.Security.Cryptography.RSACryptoServiceProvider.SignData(Byte[] buffer, Object halg)
            //at TestWebServiceRSA.Program.Main(String[] args) in z:\jsc.svn\examples\javascript\crypto\test\TestWebServiceRSA\TestWebServiceRSA\Program.cs:line 131

            var rsa3sig = rsa3.rsa.SignData(
                Encoding.UTF8.GetBytes(rsa3t),
                new SHA1CryptoServiceProvider()
            );

            var rsa3verify = rsa3.rsa.VerifyData(
                Encoding.UTF8.GetBytes(rsa3t),
                new SHA1CryptoServiceProvider(),
                rsa3sig
            );

            //rsa3verify = true


            var rsa3tu = rsa3t.ToUpper();

            var rsa3uverify = rsa3.rsa.VerifyData(
               Encoding.UTF8.GetBytes(rsa3tu),
               new SHA1CryptoServiceProvider(),
               rsa3sig
           );


            var rsa3tusig = rsa3.SignString(rsa3tu);
            var rsa3tuverify = rsa3.VerifyString(rsa3tu, rsa3tusig);

            // rsa3sig = {byte[48]}

            // rsa3uverify = false

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));

        }

    }

}
