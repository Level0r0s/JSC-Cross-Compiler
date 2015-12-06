using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestPostWithoutLength
{
    class Program
    {
        static void Main(string[] args)
        {
            //var x = new IXMLHttpRequest(ScriptCoreLib.Shared.HTTPMethodEnum.POST, "/upload", true);

            var doubleDash = "--";
            var boundary = "12345678901234567890";


            var wRequestPayload = new StringBuilder();

            wRequestPayload.Append(@"--12345678901234567890
Content-Type: text/xml; charset=UTF-8


<xml1>hello</xml1>

--12345678901234567890
Content-type: text/xml
e-x: 34
e-y: pp

<xml1>world</xml1>
--12345678901234567890--"
             );


            //var data = wRequestPayload.ToString();

            //x.setRequestHeader("Content-Type", "multipart/related; boundary=" + boundary);

            //// java stream hangs if we try to read past data?
            ////x.setRequestHeader("Content-Length", "" + data.Length);



            //x.send(data);


//            var u = @"POST /upload HTTP/1.1
//Host: 192.168.1.12:6552
//Connection: close
//Accept: */*
//Content-Type: multipart/related; boundary=12345678901234567890
//Content-Length: " + wRequestPayload.Length + @"
//
//" + wRequestPayload.ToString();

            var u = @"POST /upload HTTP/1.1
Host: 192.168.1.12:6552
Connection: close
Accept: */*
Content-Type: multipart/related; boundary=12345678901234567890

" + wRequestPayload.ToString();


            var t = new TcpClient();

            // whats the port?
            // 8741 
            //t.Connect("127.0.0.1", 8741);
            t.Connect("192.168.1.189", 8741);
            // 192.168.1.12:6552

            var data = Encoding.UTF8.GetBytes(u);
            t.GetStream().Write(data, 0, data.Length);


            var rx = new byte[0xffff];
            var c = t.GetStream().Read(rx, 0, rx.Length);
            // Additional information: Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host.


            var rxstring = Encoding.UTF8.GetString(rx, 0, c);

            t.Close();
       
            Console.WriteLine("did we get a response? " + new { rxstring });

        }
    }
}
