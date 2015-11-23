using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ScriptCoreLib.Shared
{
    // Z:\jsc.svn\examples\javascript\crypto\test\TestWebServiceRSA\TestWebServiceRSA\ApplicationWebService.cs


    public class RSACryptoStream
    {
        // not correct for small datas?

        //// fOAEP needs the last byte?
        //public static readonly Func<int, int> dwKeySizeFromMaxData = (int MaxData) => 384 + 8 * (MaxData - 7);
        //public static readonly Func<int, int> MaxDataFromdwKeySize = (int dwKeySize) => (dwKeySize - 384) / 8 + 7;

        //// keysize for 9 databytes means output is 50 bytes
        //public static readonly Func<int, int> dwKeySizeToEncryptedDataLength = (int dwKeySize) => MaxDataFromdwKeySize(dwKeySize) + 41;


        //// fOAEP needs the last byte?
        //public static readonly Func<int, int> dwKeySizeFromMaxData = (int MaxData) => 384 + 8 * (MaxData - 7);
        //public static readonly Func<int, int> MaxDataFromdwKeySize = (int dwKeySize) => (dwKeySize - 384) / 8 + 7;

        //// keysize for 9 databytes means output is 50 bytes
        //public static readonly Func<int, int> dwKeySizeToEncryptedDataLength = (int dwKeySize) => MaxDataFromdwKeySize(dwKeySize) + 41;


        public int KeySize
        {
            get { return this.rsa.KeySize; }
        }

        public int EncryptedDataChunkSize
        {
            get
            {
                return
                    (this.rsa.KeySize - 384) / 8 + 7 + 41;
                //dwKeySizeToEncryptedDataLength(this.rsa.KeySize); 
            }
        }

        
        public readonly RSACryptoServiceProvider rsa;


        public RSACryptoStream(RSAParameters p)
        {
            this.rsa = new RSACryptoServiceProvider();
            this.rsa.ImportParameters(p);
        }

        //public RSACryptoStream(byte[] CSPBlob)
        //{
        //    this.rsa = new RSACryptoServiceProvider();
        //    this.rsa.ImportCspBlob(CSPBlob);
        //}

        public RSACryptoStream(RSACryptoServiceProvider rsa)
        {
            this.rsa = rsa;

        }




        public bool VerifyString(string e, byte[] signature)
        {
            return this.rsa.VerifyData(
                 Encoding.UTF8.GetBytes(e),
                //new SHA256CryptoServiceProvider(),
                 new SHA1CryptoServiceProvider(),
                 signature
             );
        }

        public byte[] SignString(string e)
        {
            return this.rsa.SignData(
             Encoding.UTF8.GetBytes(e),
             new SHA1CryptoServiceProvider()
         );
        }

        public string DecryptString(byte[] data)
        {
            var bytes = ReadAllBytes(new MemoryStream(data));
            return Encoding.UTF8.GetString(bytes);
        }

        public byte[] EncryptString(string e)
        {
            var m = new MemoryStream();
            WriteTo(Encoding.UTF8.GetBytes(e), m);
            return m.ToArray();
        }

        const bool fOAEP = true;

        public void WriteTo(byte[] bytes, Stream stream)
        {
            Console.WriteLine("enter WriteTo " + new { stream });
            // we need to d a chuncked write

            var m = new MemoryStream(bytes);

            var data = new byte[EncryptedDataChunkSize];
            var datalength = m.Read(data, 0, EncryptedDataChunkSize);
            Console.WriteLine("WriteTo " + new { datalength });

            while (datalength > 0)
            {
                var rgb = new byte[datalength];
                Array.Copy(data, rgb, datalength);

                var unclear = rsa.Encrypt(rgb, fOAEP: fOAEP);
                Console.WriteLine("WriteTo " + new { unclear });

                stream.Write(unclear, 0, unclear.Length);

                datalength = m.Read(data, 0, EncryptedDataChunkSize);
                Console.WriteLine("WriteTo " + new { datalength });
            }
        }

        public byte[] ReadAllBytes(Stream stream)
        {
            // tested by?
            Console.WriteLine("enter ReadAllBytes " + new { stream });

            var m = new MemoryStream();

            var data = new byte[EncryptedDataChunkSize];
            var datalength = stream.Read(data, 0, data.Length);

            while (datalength > 0)
            {
                var rgb = new byte[datalength];

                Array.Copy(data, rgb, datalength);
                var clear = rsa.Decrypt(rgb, fOAEP: fOAEP);

                m.Write(clear, 0, clear.Length);

                datalength = stream.Read(data, 0, data.Length);
            }

            return m.ToArray();
        }
    }

}
