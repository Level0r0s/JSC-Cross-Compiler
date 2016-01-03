using android.graphics;
using android.media;
using android.view;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScriptCoreLib.Android.Extensions;
using System.Net.NetworkInformation;
using System.Net.Sockets;

using ScriptCoreLib.Extensions;
using System.Net;


namespace x360video.Activities
{
    partial class ApplicationActivity
    {

        //x:\util\android-sdk-windows\platform-tools\adb.exe shell am force-stop x360video.Activities
        //x:\util\android-sdk-windows\platform-tools\adb.exe shell am start -n x360video.Activities/x360video.Activities.ApplicationActivity

        //  x:\util\android-sdk-windows\platform-tools\adb.exe logcat -s "xNativeActivity" "System.Console" "DEBUG" "PlatformActivity"


        #region NDK
        //        virtual eMsgStatus  OnEvent_Impl( OvrGuiSys & guiSys, VrFrame const & vrFrame,
        //    VRMenuObject * self, VRMenuEvent const & event )

        //    OVR_ASSERT( event.EventType == VRMENU_EVENT_TOUCH_UP );
        //    if ( OvrFolderBrowser * folderBrowser = static_cast< OvrFolderBrowser * >( Menu ) )
        //        folderBrowser->OnPanelUp( guiSys, Data );


        //        void OvrFolderBrowser::OnPanelUp( OvrGuiSys & guiSys, const OvrMetaDatum * data )
        //    if ( AllowPanelTouchUp )
        //        OnPanelActivated( guiSys, data );



        //        void VideoBrowser::OnPanelActivated( OvrGuiSys & guiSys, const OvrMetaDatum * panelData )
        //    Videos.OnVideoActivated( panelData );

        //void Oculus360Videos::OnVideoActivated( const OvrMetaDatum * videoData )
        //    ActiveVideo = videoData;

        // void Oculus360Videos::StartVideo( const double nowTime )
        // X:\opensource\ovr_sdk_mobile_1.0.0.0\VrSamples\Native\Oculus360VideosSDK\Src\Oculus360Videos.cpp
        // 		jmethodID startMovieMethodId = app->GetJava()->Env->GetMethodID( MainActivityClass,
        //	"startMovieFromNative", "(Ljava/lang/String;)V" );
        #endregion

        public void startMovieFromUDP(string pathName)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/startmoviefromudp
            Console.WriteLine("startMovieFromUDP " + new { pathName });

            // need to activate a panel?
            // how do we do that?


            //xMarshal.startMovieFromUDP(new { this.__ptr, pathName });

            // java anonymous types should use plain fields like we do in js...
            xMarshal.startMovieFromUDP(new startMovieFromUDPArguments { __ptr = this.__ptr, pathName = pathName });
        }

        public void startMovieFromNative(string pathName)
        {
            // hop to UI


            // NDK or UDP or UI?
            //Console.WriteLine("startMovieFromNative " + new { pathName, Environment.StackTrace });

            //Log.d( TAG, "startMovieFromNative" );
            this.runOnUiThread(
                delegate
                {
                    //Log.d( TAG, "startMovieFromNative" );
                    startMovie(pathName);
                }
            );
        }


        // by nativePrepareNewVideo
        SurfaceTexture movieTexture = null;

        // how do we get our videos on it?

        static Dictionary<string, string> startMovieLookup = new Dictionary<string, string> { };

        public void startMovie(string pathName)
        {
            var f = new FileInfo(pathName);

            // let zmovies know we started a video. could we stream it to chrome?
            Console.WriteLine("startMovie " + new { f.Name });

            // lets shell and do a ls to figure out we do have the thumbnail there...
            var mp4_jpg = (
              from pf in new DirectoryInfo("/sdcard/oculus/360Photos/").GetFiles()
              //where pf.Extension.ToLower() == ".jpg"
              //  Z:\jsc.svn\examples\rewrite\GearVR360VideoPush\GearVR360VideoPush\Program.cs
              where pf.Name == System.IO.Path.ChangeExtension(f.Name, ".jpg")
              select pf

              // if we change it. can we hotswap the code already rnning in vr?
            ).FirstOrDefault();

            ///System.Console(30077): 757d:0001 startMovie { Name = 360 3D Towering Hoodoos in Bryce Canyon in 3d,  -degree video. by Visit_Utah.mp3._TB.mp4 }
            ///System.Console(30077): 757d:0001 startMovie { mp4_jpg = ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable__WhereIterator_d__0_1@6d1507a }

            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/x360videoui
            Console.WriteLine("startMovie " + new { mp4_jpg });


            // upstream it!

            #region udp
            if (mp4_jpg != null)
            {

                Task.Run(
                    delegate
                    {

                        //args.filesize = mp4_jpg.Length;

                        // we are not on ui thread.
                        // HUD thread can freeze...
                        // mmap?
                        var sw = System.Diagnostics.Stopwatch.StartNew();
                        var bytes = File.ReadAllBytes(mp4_jpg.FullName);

                        // why slo slow???
                        // can jsc do it in NDK?
                        // http://stackoverflow.com/questions/32269305/android-play-pcm-byte-array-from-converted-from-base64-string-slow-sounds
                        var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(bytes);
                        var md5string = md5.ToHexString();

                        Console.WriteLine("startMovie " + new { f.Name, md5string, sw.ElapsedMilliseconds });


                        //I/System.Console( 4098): 1002:0001 startMovie { Name = 360 3D [3D  VR] __________(HAC) ______ __(Dance) A_ by _____________Verest__360_VR.mp3._TB.mp4 }
                        //I/System.Console( 4098): 1002:0001 startMovie { mp4_jpg = { FullName = /storage/emulated/legacy/oculus/360Photos/360 3D [3D  VR] __________(HAC) ______ __(Dance) A_ by _____________Verest__360_VR.mp3._TB.jpg, Exists = true } }
                        //I/System.Console( 4098): 1002:06a2 startMovie { Name = 360 3D [3D  VR] __________(HAC) ______ __(Dance) A_ by _____________Verest__360_VR.mp3._TB.mp4, md5string = 8bebab806331b078b385e33e5e069393, ElapsedMilliseconds = 6579 }


                        if (startMovieLookup.ContainsKey(md5string))
                        {
                            // already uploaded.

                            return;

                        }

                        startMovieLookup[md5string] = pathName;

                        // await for callback. lookup. transaction




                        // now broadcast. at 500KBps in segments.
                        // 8MB is 16 segments then.

                        if (bytes.Length > 0)
                            NetworkInterface.GetAllNetworkInterfaces().WithEach(
                                  n =>
                                  {
                                      // X:\jsc.svn\examples\java\android\forms\FormsUDPJoinGroup\FormsUDPJoinGroup\ApplicationControl.cs
                                      // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Net\NetworkInformation\NetworkInterface.cs

                                      var IPProperties = n.GetIPProperties();
                                      var PhysicalAddress = n.GetPhysicalAddress();



                                      foreach (var ip in IPProperties.UnicastAddresses)
                                      {
                                          // ipv4
                                          if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                          {
                                              if (!IPAddress.IsLoopback(ip.Address))
                                                  if (n.SupportsMulticast)
                                                  {
                                                      //fWASDC(ip.Address);
                                                      //fParallax(ip.Address);
                                                      //fvertexTransform(ip.Address);
                                                      //sendTracking(ip.Address);

                                                      var port = new Random().Next(16000, 40000);

                                                      //new IHTMLPre { "about to bind... " + new { port } }.AttachToDocument();

                                                      // where is bind async?
                                                      var socket = new UdpClient(
                                                           new IPEndPoint(ip.Address, port)
                                                          );


                                                      //// who is on the other end?
                                                      //var nmessage = args.x + ":" + args.y + ":" + args.z + ":0:" + args.filename;

                                                      //var data = Encoding.UTF8.GetBytes(nmessage);      //creates a variable b of type byte

                                                      // http://stackoverflow.com/questions/25841/maximum-buffer-length-for-sendto

                                                      new { }.With(
                                                          async delegate
                                                          {
                                                              // reached too far?
                                                              if (bytes.Length == 0)
                                                                  return;

                                                              //var current0 = current;

                                                              var r = new MemoryStream(bytes);
                                                              var uploadLength = r.Length;

                                                              var data = new byte[65507];

                                                          next:

                                                              //if (current0 != current)
                                                              //    return;

                                                              var cc = r.Read(data, 0, data.Length);

                                                              var uploadPosition = r.Position;

                                                              if (cc <= 0)
                                                                  return;

                                                              //new IHTMLPre { "about to send... " + new { data.Length } }.AttachToDocument();

                                                              // X:\jsc.svn\examples\javascript\chrome\apps\ChromeUDPNotification\ChromeUDPNotification\Application.cs
                                                              //Console.WriteLine("about to Send");
                                                              // X:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeEquirectangularPanorama\ChromeEquirectangularPanorama\Application.cs
                                                              await socket.SendAsync(
                                                                   data,
                                                                   cc,
                                                                   hostname: "239.1.2.3",
                                                                   port: 49000
                                                               );

                                                              //await Task.Delay(1000 / 15);
                                                              //await Task.Delay(1000 / 30);

                                                              // no corruption
                                                              await Task.Delay(1000 / 20);

                                                              goto next;

                                                          }
                                                      );

                                                      //socket.Close();
                                                  }
                                          }
                                      }




                                  }
                              );
                    }
                );

            }
            #endregion





            // Request audio focus
            requestAudioFocus();

            // Have native code pause any playing movie,
            // allocate a new external texture,
            // and create a surfaceTexture with it.
            movieTexture = com.oculus.oculus360videossdk.MainActivity.nativePrepareNewVideo(base_getAppPtr());
            movieTexture.setOnFrameAvailableListener(this);
            movieSurface = new Surface(movieTexture);

            if (mediaPlayer != null)
            {
                mediaPlayer.release();
            }

            //Log.v(TAG, "MediaPlayer.create");

            //synchronized (this) {
            mediaPlayer = new MediaPlayer();
            //}


            mediaPlayer.setOnVideoSizeChangedListener(this);
            mediaPlayer.setOnCompletionListener(this);

            // if only webview had setSurface method?
            mediaPlayer.setSurface(movieSurface);

            try
            {
                //Log.v(TAG, "mediaPlayer.setDataSource()");
                mediaPlayer.setDataSource(pathName);
            }
            catch //(IOException t) 
            {
                //Log.e(TAG, "mediaPlayer.setDataSource failed");
            }

            try
            {
                //Log.v(TAG, "mediaPlayer.prepare");
                mediaPlayer.prepare();
            }
            catch //(IOException t) 
            {
                //Log.e(TAG, "mediaPlayer.prepare failed:" + t.getMessage());
            }
            //Log.v(TAG, "mediaPlayer.start");

            // If this movie has a saved position, seek there before starting
            // This seems to make movie switching crashier.
            int seekPos = getPreferences(MODE_PRIVATE).getInt(pathName + "_pos", 0);
            if (seekPos > 0)
            {
                try
                {
                    mediaPlayer.seekTo(seekPos);
                }
                catch //( IllegalStateException ise ) 
                {
                    //Log.d( TAG, "mediaPlayer.seekTo(): Caught illegalStateException: " + ise.toString() );
                }
            }

            mediaPlayer.setLooping(false);

            try
            {
                mediaPlayer.start();
            }
            catch //( IllegalStateException ise ) 
            {
                //Log.d( TAG, "mediaPlayer.start(): Caught illegalStateException: " + ise.toString() );
            }

            mediaPlayer.setVolume(1.0f, 1.0f);

            // Save the current movie now that it was successfully started
            var edit = getPreferences(MODE_PRIVATE).edit();
            edit.putString("currentMovie", pathName);
            edit.commit();

            //Log.v(TAG, "returning");
        }
    }
}
