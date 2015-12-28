using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ioxor
{
    //358612821 read in 260861
    //reading... done in 35

    //Unhandled Exception: System.OperationCanceledException: The operation was canceled.
    //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
    //   at System.IO.FileStream.ReadCore(Byte[] buffer, Int32 offset, Int32 count)
    //   at System.IO.FileStream.Read(Byte[] array, Int32 offset, Int32 count)
    //   at ioxor.Program.Main(String[] args) in X:\jsc.svn\examples\merge\ioxor\ioxor\Program.cs:line 152



    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // http://discutils.codeplex.com/SourceControl/latest#src/Vhd/Footer.cs

            // can we switch wifi channels?

            // need bit 1,2,3,4,5,7,8,9X histogram

            // https://ffmpeg.org/pipermail/ffmpeg-user/2011-August/002051.html
            // 60.1 KB (61,574 bytes)
            // 60.2 KB (61,652 bytes)
            // 78bytes

            // SIGINT
            // opencv

            // http://stackoverflow.com/questions/11986279/can-ffmpeg-convert-audio-from-raw-pcm-to-wav
            // header includes the format, sample rate, and number of channels.
            // http://www.topherlee.com/software/pcm-tut-wavformat.html






            // https://www.youtube.com/watch?v=2xZgCVG_Bzk
            // Audacity's "import Raw Data" feature.
            // Invalid PCM packet, data has size 2 but at least a size of 4 was expected
            // "R:\util\ffmpeg-20150609-git-7c9fcdf-win64-static\ffmpeg-20150609-git-7c9fcdf-win64-static\bin\ffmpeg.exe" -f s16le -ar 44.1k -ac 2 -i file.pcm file.wav
            // ffmpeg -f u16le -ar 44100 -ac 1 -i input.raw
            // ffmpeg -f s16le -ar 44.1k -ac 2 -i file.pcm file.wav
            // http://stackoverflow.com/questions/11986279/can-ffmpeg-convert-audio-from-raw-pcm-to-wav

            // http://www.jsresources.org/examples/RawAudioDataConverter.html
            // http://www.mathworks.com/matlabcentral/answers/88840-how-to-open-a-headerless-wav-file

            // https://code.google.com/p/binvis/downloads/detail?name=BinVis_Binary_Release.zip&can=2&q=
            // https://code.google.com/p/cassia/
            // http://fragged.info/tag/remoteapp/

            File.WriteAllText("TerminalServerSession", "" + System.Windows.Forms.SystemInformation.TerminalServerSession);


            // send to RemoteApp
            Console.WriteLine(new { System.Windows.Forms.SystemInformation.TerminalServerSession });
            Console.WriteLine(new { Environment.CurrentDirectory });
            //Console.WriteLine(Environment.ter);

            // 683671226
            var CrashManagerLastWriteback = File.Exists("LastWriteback") ? File.ReadAllText("LastWriteback") : "0";
            var CrashManagerLastWriteback64 = long.Parse(CrashManagerLastWriteback);

            Console.WriteLine(new { CrashManagerLastWriteback });


            var f2 = new Form2();

            // or should we jump from RED to android ui?
            if (f2.ShowDialog() != DialogResult.OK)
            {
                // no seed
                return;
            }

            var f2seedCount = int.Parse(f2.textBox1.Text);

            Console.WriteLine(new { f2seedCount });


            var seed3 = Enumerable.Take(

                         from ff in Directory.GetFiles("x:/media/")
                         let fff = new FileInfo(ff)
                         // cutoff by date?
                         orderby fff.Length descending

                         //select fff, 8
                         //select fff, 16
                         select fff, f2seedCount
                     ).ToArray();

            foreach (var i in seed3)
            {
                Console.WriteLine(new { i.LastWriteTimeUtc.Date, i.Name, i.Length });
            }

            foreach (var item in args)
            {
                after_tsdiscon:;


                var exists = Directory.Exists(item);

                Console.WriteLine(new { item, exists });

                if (!exists)
                {
                    // http://weblogs.asp.net/jeffwids/remote-desktop-to-console-session-on-windows-server-2003
                    //Microsoft.Win32.SystemEvents.SessionEnding

                    if (MessageBox.Show("folder not available/ reconect? check drivestoredirect. disconnect to reconnect. May cause session to be closed after Preparing Your Desktop.", "?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // https://technet.microsoft.com/en-us/library/bb490805.aspx
                        // https://books.google.ee/books?id=GJoBXKrdG7gC&pg=PA208&lpg=PA208&dq=tskill+disconnect&source=bl&ots=i3Qj6Lb7ye&sig=WFAI7mmC209Due3U7YI9mZxiZqs&hl=en&sa=X&ved=0CB8Q6AEwAGoVChMI9v--uq2yyAIVATYaCh0NEgpG#v=onepage&q=tskill%20disconnect&f=false


                        // If no session ID or session name is specified, tsdiscon disconnects the current session.
                        Process.Start("tsdiscon").WaitForExit();

			    // await reconnect?
			    // http://stackoverflow.com/questions/12759567/remote-desktop-connection-c-sharp-events
                        if (MessageBox.Show("retry?", "?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            goto after_tsdiscon;
                    }

                    Environment.Exit(1);
                }
                else
                    foreach (var f in Directory.GetFiles(item))
                    {
                        if (File.Exists(f))
                        {
                            //Console.WriteLine(f);

                            var zf = new FileInfo(f);
                            var Length = zf.Length;

                            Console.WriteLine(new { f, Length, zf.LastWriteTimeUtc });




                            // { Length = 1 073 742 336 }
                            if (MessageBox.Show(

                                f, "update seed?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                var sw = Stopwatch.StartNew();


                                var r = File.Open(f, FileMode.Open, FileAccess.ReadWrite);


                                // would it work?
                                if (CrashManagerLastWriteback64 > 0)
                                    if (CrashManagerLastWriteback64 < r.Length)
                                        if (MessageBox.Show("resume at " + new { CrashManagerLastWriteback64 }, "crash?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                            r.Position = CrashManagerLastWriteback64;

                                var seed3r = Enumerable.ToArray(
                                    from SourceSeed in seed3
                                    let SourceSeedStream = SourceSeed.OpenRead()
                                    // http://www.slideshare.net/DefCamp/defcamp-2013-doctrackr
                                    // SIGINT 55
                                    // offset by, ask UDP device

                                    let SourceSeedStreamOffset = SourceSeed.Length / 5 + r.Position
                                    //let peekdata = new byte[peek]
                                    //let x = fr.Read(peekdata, 0, peek)

                                    let p = SourceSeedStream.Position = SourceSeedStreamOffset

                                    select SourceSeedStream
                                );

                                Console.WriteLine(new { sw.ElapsedMilliseconds });



                                var c = 0;

                                Action yield = delegate { };


                                do
                                {
                                    var Position0 = r.Position;
                                    var data = new byte[0x1fffff];

                                    GC.KeepAlive(data);

                                    var c0 = 0;

                                    #region data <- read
                                    var readreopen = false;

                                    reread:
                                    try
                                    {
                                        if (readreopen)
                                        {
                                            readreopen = false;
                                            r = File.Open(f, FileMode.Open, FileAccess.ReadWrite);
                                            r.Position = Position0;
                                        }

                                        // a hybrid app needs to be resillien while lengthy data sync may be interupted
                                        c0 = r.Read(data, 0, data.Length);
                                        c = c0;
                                    }
                                    catch (Exception readfault)
                                    {
                                        Console.WriteLine(new { readfault });

                                        //                                        reading...done in 163
                                        //{
                                        //                                            readfault = System.OperationCanceledException: The operation was canceled.
                                        //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
                                        //   at System.IO.FileStream.ReadCore(Byte[] buffer, Int32 offset, Int32 count)
                                        //   at System.IO.FileStream.Read(Byte[] array, Int32 offset, Int32 count)
                                        //   at ioxor.Program.Main(String[] args) in Z:\jsc.svn\examples\merge\ioxor\ioxor\Program.cs:line 172 }
                                        //                                        {
                                        //                                            readfault = System.IO.IOException: An unexpected network error occurred.

                                        //                                             at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
                                        //                                           at System.IO.FileStream.ReadCore(Byte[] buffer, Int32 offset, Int32 count)
                                        //                                           at System.IO.FileStream.Read(Byte[] array, Int32 offset, Int32 count)
                                        //                                           at ioxor.Program.Main(String[] args) in Z:\jsc.svn\examples\merge\ioxor\ioxor\Program.cs:line 172 }

                                        MessageBox.Show("reread?");

                                        readreopen = true;


                                        goto reread;
                                    }
                                    #endregion

                                    // 255 read in 14
                                    // 65535 read in 15
                                    // 1048575 read in 104
                                    // 16777215 read in 1650

                                    Console.Title = (int)(100 * ((double)r.Position / (double)Length)) + "%";
                                    Console.WriteLine(r.Position + " read in " + sw.ElapsedMilliseconds);

                                    var task0 = Task.Run(
                                        delegate
                                        {
                                            var sw0 = Stopwatch.StartNew();

                                            //Console.WriteLine("reading...");

                                            if (Position0 == 0)
                                            {
                                                // from
                                                var Position0bytes8 = Encoding.ASCII.GetString(data, 0, 8);
                                                Console.WriteLine(new { Position0bytes8 });
                                            }

                                            foreach (var seed0 in seed3r)
                                            {
                                                var data0 = new byte[data.Length];

                                                // apply entropy offset here?
                                                var c00 = seed0.Read(data0, 0, data0.Length);

                                                for (int j = 0; j < data.Length; j++)
                                                {
                                                    data[j] ^= data0[j];
                                                }

                                                //Console.WriteLine("reading... " + c0 + " in " + sw0.ElapsedMilliseconds);
                                                // 11534325 read in 1370
                                                //reading...
                                                //reading... 1048575 in 1
                                                //reading... 1048575 in 1
                                                //reading... 1048575 in 2
                                            }

                                            for (int j = 0; j < data.Length; j++)
                                            {
                                                data[j] ^= 0x55;
                                            }

                                            Console.WriteLine("reading... done in " + sw0.ElapsedMilliseconds);

                                            if (Position0 == 0)
                                            {
                                                // to
                                                var Position0bytes8 = Encoding.ASCII.GetString(data, 0, 8);


                                                Console.WriteLine(new { f2seedCount, Position0bytes8 });
                                                if (
                                                        MessageBox.Show(new { Position0bytes8 }.ToString(), "sky is blue?", MessageBoxButtons.OKCancel) == DialogResult.Cancel
                                                )
                                                {
                                                    Environment.Exit(1);
                                                }


                                            }

                                            File.WriteAllText("ReadyForWriteback", "" + Position0);

                                            #region write
                                            yield += delegate
                                            {

                                                //                                             Unhandled Exception: System.OperationCanceledException: The operation was canceled.
                                                //at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
                                                //at System.IO.FileStream.WriteCore(Byte[] buffer, Int32 offset, Int32 count)
                                                //at System.IO.FileStream.Write(Byte[] array, Int32 offset, Int32 count)
                                                //at ioxor.Program.<> c__DisplayClass0_1.< Main > b__10() in X:\jsc.svn\examples\merge\ioxor\ioxor\Program.cs:line 191
                                                //at System.Action.Invoke()
                                                //at ioxor.Program.Main(String[] args) in X:\jsc.svn\examples\merge\ioxor\ioxor\Program.cs:line 233



                                                // data corruption...
                                                var reopen = false;
                                                retry:
                                                try
                                                {
                                                    if (reopen)
                                                    {
                                                        r = File.Open(f, FileMode.Open, FileAccess.ReadWrite);
                                                        reopen = false;
                                                    }

                                                    r.Position = Position0;

                                                    r.Write(data, 0, c0);

                                                    // is it possible we crashed but dont know it?


                                                    r.Flush();

                                                    //                                                0 written in 0
                                                    //5242879 written in 0
                                                    //10485758 written in 0
                                                    //15728637 written in 0
                                                    //18342170 written in 1

                                                    //Console.WriteLine("critical writeback " + new { Position0 });

                                                    Console.Title = (int)(100 * ((double)Position0 / (double)Length)) + "%";
                                                    Console.WriteLine(r.Position + " written in " + sw.ElapsedMilliseconds);


                                                    // if we crash we should fast forward to thisone?
                                                    File.WriteAllText("LastWriteback", "" + Position0);
                                                }
                                                catch (Exception err)
                                                {
                                                    //379584331 written in 319822
                                                    //{ err = System.OperationCanceledException: The operation was canceled.
                                                    //   at System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
                                                    //   at System.IO.FileStream.WriteCore(Byte[] buffer, Int32 offset, Int32 count)
                                                    //   at System.IO.FileStream.Write(Byte[] array, Int32 offset, Int32 count)
                                                    //   at ioxor.Program.<>c__DisplayClass0_1.<Main>b__9() in Z:\jsc.svn\examples\merge\ioxor\ioxor\Program.cs:line 245 }
                                                    //{ err = System.IO.IOException: An unexpected network error occurred.
                                                    Console.WriteLine(new { err });
                                                    Thread.Sleep(10000);
                                                    if (MessageBox.Show("retry at " + new { Position0 }, "crash?", MessageBoxButtons.YesNo) == DialogResult.Yes)

                                                    // r needs to be recreated?
                                                    {
                                                        reopen = true;

                                                        goto retry;
                                                    }

                                                }
                                           
                                            };
                                            #endregion


                                        }
                                    );


                                    if (Position0 == 0)
                                    {
                                        Console.WriteLine(
                                            "ask the xor thread to do a status check."
                                        );


                                        task0.Wait();
                                    }

                                    Thread.Yield();

                                }
                                while (c > 0);





                                // 1073742336 read in 414878
                                // 1 073 742 336 read in 414 878

                                //MessageBox.Show("do critical writeback?");

                                Thread.Yield();

                                for (int i = 10; i > 0; i--)
                                {
                                    Console.WriteLine("vsync. ready to ioxor " + i);
                                    Thread.Sleep(1000);
                                }

                                Thread.Sleep(300);

                                sw.Restart();
                                yield();

                                // disable resume..
                                File.WriteAllText("LastWriteback", "" + 0);

                            }

                        }
                    }


            }

            //new Form1().ShowDialog();
            // crash cleanup"
            MessageBox.Show("done. disconnect. keep in touch. reconnect.");
            // http://stackoverflow.com/questions/5207506/logoff-interactive-users-in-windows-from-a-service
            // http://microsoft.public.windows.terminal-services.narkive.com/14i4dvmL/programatically-reset-all-ts-sessions

        }
    }
}

//[Window Title]
//RemoteApp Error

//[Content]
//Couldn’t open this program or file.Either there was a problem with "X:\jsc.svn\examples\merge\ioxor\ioxor\bin\Debug\ioxor.exe" or the file you’re trying to open couldn’t be accessed.
//For assistance, contact your system administrator.

//[OK]