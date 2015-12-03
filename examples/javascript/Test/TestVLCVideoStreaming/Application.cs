using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestVLCVideoStreaming;
using TestVLCVideoStreaming.Design;
using TestVLCVideoStreaming.HTML.Pages;

namespace TestVLCVideoStreaming
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            Native.document.body.Clear();
            Native.document.body.style.margin = "0px";
            Native.document.body.style.padding = "0px";

            // http://stackoverflow.com/questions/11294836/streaming-mp4-with-vlc-to-html-browser

            // In XHTML, attribute minimization is forbidden, and the controls attribute must be defined as <video controls="controls">.

            // ogg wont show up on gearvr?

            //var v = new IHTMLVideo { src = "http://192.168.1.12:8080/x.ogg", controls = true }.AttachToDocument();

            // chrome displays 1 still and crashes on webm play. vlc shows black.
            // gearvr stays white.
            //var v = new IHTMLVideo { src = "http://192.168.1.12:8080/x.webm", controls = true }.AttachToDocument();

            // chrome stays black.
            var v = new IHTMLVideo { src = "http://192.168.1.12:8080/x.mp4", controls = true }.AttachToDocument();

            // this works in gearvr!
            //var v = new IHTMLVideo { src = "https://broken-links.com/tests/media/BigBuck.webm", controls = true }.AttachToDocument();
            //var v = new IHTMLVideo { src = "https://broken-links.com/tests/media/BigBuck.theora.ogv", controls = true }.AttachToDocument();

            // run in console session?
            // vlc goes crazy!
            // "C:\Program Files (x86)\VideoLAN\VLC\vlc.exe"  –no-sse2 –no-sse41 -vvv "X:\media\Abraham Hicks - Is There Value In Being Vulnerable Or Open (2014).mp4" --sout '#transcode{vcodec=VP80,vb=800,scale=1,acodec=vorbis,ab=128,channels=2}:standard{access=http,mux="ffmpeg{mux=webm}",dst=192.168.1.12:8080/x.webm}' 
            // "X:\util\vlc-2.2.1-win32\vlc-2.2.1\vlc.exe" --intf dummy --verbose  -vvv "X:\media\Abraham Hicks - Is There Value In Being Vulnerable Or Open (2014).mp4" vlc://quit --sout '#transcode{vcodec=VP80,vb=800,scale=1,acodec=vorbis,ab=128,channels=2}:standard{access=http,mux="ffmpeg{mux=webm}",dst=192.168.1.12:8080/x.webm}' 
            // "X:\util\vlc-2.2.1-win32\vlc-2.2.1\vlc.exe" --no-loop --intf dummy --verbose  -vvv "X:\media\Abraham Hicks - Is There Value In Being Vulnerable Or Open (2014).mp4" --sout '#transcode{vcodec=VP80,vb=800,scale=1,acodec=vorbis,ab=128,channels=2}:standard{access=http,mux="ffmpeg{mux=webm}",dst=192.168.1.12:8080/x.webm}' 



            // http://stackoverflow.com/questions/5087687/howto-encode-webm-using-command-line-vlc

            // [0276ac3c] core input error: cannot start stream output instance, aborting
            //[006a75f4] stream_out_standard stream out error: no mux specified or found by extension

            // cvlc my_first_video.avi  –sout “#transcode{vcodec=VP80,vb=800,scale=1,acodec=vorbis,ab=128,channels=2}:std{access=file,mux=”ffmpeg{mux=webm}”,dst=my_first_video.webm}”
            //(note:


            //[02130d24] dummy interface: VLC media player - 2.2.1 Terry Pratchett (Weatherwax)
            //[02130d24] dummy interface: Copyright © 1996-2015 the VideoLAN team
            //[02130d24] dummy interface:
            //Warning: if you cannot access the GUI anymore, open a command-line window, go to the directory where you installed VLC and run "vlc -I qt"

            //[02130d24] dummy interface: using the dummy interface module...
            //[0218d6a4] stream_out_standard stream out error: no mux specified or found by extension
            //[0218d60c] core stream output error: stream chain failed for `standard{mux="",access="'#transcode{vcodec=VP80,vb=800,scale=1,acodec=vorbis,ab=128,channels=2}",dst="standard{access=http,mux=ffmpeg{mux=webm},dst=192.168.1.12:8080/x.webm}'"}'
            //[0213cc3c] core input error: cannot start stream output instance, aborting



            // https://www.maketecheasier.com/mastering-vlc-via-the-command-line-linux/
            // http://alien.slackbook.org/blog/vlc-and-creating-webm-video/

            //What the VLC graphical interface can not yet do, is allow you to encode WebM video. Lucky for us, VLC has a command-line interface as well, with a humongous amount of options whose learning curve is even steeper than that of vi 😉

            //The VLC command-line allows to encode/transcode WebM video! Want to try it out?


            // view-source:https://broken-links.com/tests/video/
            //            <video id="video" autobuffer height="240" poster="../images/bbb_poster-360x240.jpg" width="360">
            //        <source src="../media/BigBuck.m4v">
            //        <source src="../media/BigBuck.webm" type="video/webm">
            //        <source src="../media/BigBuck.theora.ogv" type="video/ogg">
            //</video>



            //            Streaming / Transcoding failed:
            //VLC could not open the mp4a audio encoder.
            // https://www.broken-links.com/2010/07/30/encoding-video-for-android/


            // https://broken-links.com/tests/video/comparison.html


            //new IStyle(v)
            //{
            //    position = IStyle.PositionEnum.@fixed,
            //    left = "0px",
            //    top = "0px",
            //    right = "0px",
            //    bottom = "0px",
            //};

            new IStyle(v)
            {
                position = IStyle.PositionEnum.absolute,
                left = "0px",
                top = "0px",
                width = "100%",
                height = "100%",
            };


            // would jsc be able to be the overlay buffer?
        }

    }
}
