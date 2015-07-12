extern alias core;

using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.DOM
{
    // X:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\JavaScript\DOM\NavigatorInfo.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\NavigatorInfo.cs

    public class NavigatorInfo
    {
        //public void webkitGetUserMedia(object constraints, IFunction successCallback, IFunction errorCallback);

        [Script(DefineAsStatic = true)]
        public void getUserMedia(
            Action<LocalMediaStream> successCallback,
            bool video = true,
            bool audio = false,
            Action<NavigatorUserMediaError> errorCallback = null)
        {
            // http://www.guanotronic.com/~serge/papers/chi15-webcam.pdf
            // X:\jsc.svn\examples\javascript\async\test\TestGetUserMedia\TestGetUserMedia\Application.cs

            // http://www.html5rocks.com/en/tutorials/getusermedia/intro/

            var fgetUserMedia = (IFunction)new IFunction(
                "return navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;"
            ).apply(null);

            // Failed to execute 'webkitGetUserMedia' on 'Navigator': The callback provided as parameter 3 is not a function.


            if (errorCallback == null)
            {
                errorCallback =
                    err =>
                    {
                        // 93ms getUserMedia error { code = , err = [object NavigatorUserMediaError] }
                        // 
                        // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs
                        Console.WriteLine("getUserMedia error " + new { err.code, err.message, err.name });

                        // how about returning null?
                    };
            }

            // https://w3c.github.io/mediacapture-main/getusermedia.html#idl-def-MediaTrackConstraints
            // https://code.google.com/p/chromium/issues/detail?id=160337

            // https://gumcameraresolutions.azurewebsites.net/


            dynamic __video = video;

            if (video)
            {
                // https://github.com/GoogleChrome/chrome-app-samples/blob/master/samples/camera-capture/app.js

                // HD please.
                // TypeError: Failed to execute 'webkitGetUserMedia' on 'Navigator': Malformed constraints object.
                // https://chromium.googlesource.com/chromium/blink/+/master/Source/modules/mediastream/MediaConstraintsImpl.cpp

                //__video = new { height = 720 };
                // https://gumcameraresolutions.azurewebsites.net/js/quickscan.js

                // MediaTrackConstraints 
                //__video = new
                //{
                //    mandatory = new
                //    {
                //        maxWidth = 1280,
                //        minWidth = 1280,
                //        minHeight = 720,
                //        maxHeight = 720
                //    }
                //};

                //__video = new object();
                //__video.mandatory = new object();
                //__video.mandatory.maxWidth = 1280;
                //__video.mandatory.minWidth = 1280;
                //__video.mandatory.minHeight = 720;
                //__video.mandatory.maxHeight = 720;

            }

            var constraints = new { video = __video, audio };

            Console.WriteLine(new { constraints });

            fgetUserMedia.apply(Native.window.navigator,
              constraints,
              IFunction.OfDelegate(
                  successCallback
              ),
              IFunction.OfDelegate(
                  errorCallback
              )
            );

            //navigator.getUserMedia_ = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
        }

        // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs

        #region async
        [Script]
        public new class Tasks
        // <TElement> where TElement : IHTMLElement
        {
            internal NavigatorInfo that;

            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public virtual Task<IHTMLVideo> onvideo
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IHTMLVideo>();

                    // tested by
                    // X:\jsc.svn\market\javascript\Abstractatech.JavaScript.Avatar\Abstractatech.JavaScript.Avatar\Application.cs
                    // X:\jsc.svn\examples\javascript\async\Test\TestGetUserMedia\TestGetUserMedia\Application.cs

                    that.getUserMedia(
                        successCallback:
                         stream =>
                         {
                             var src = stream.ToObjectURL();

                             var v = new IHTMLVideo { src = src };
                             x.SetResult(v);

                             // http://stackoverflow.com/questions/11642926/stop-close-webcam-which-is-opened-by-navigator-getusermedia

                             var w = (core::ScriptCoreLib.JavaScript.DOM.IWindow)(object)Native.window;

                             w.onframe +=
                                 delegate
                                 {
                                     if (stream == null)
                                         return;

                                     if (v.src == src)
                                         return;

                                     // how else could media steam be used, webrtc?
                                     stream.stop();
                                     stream = null;
                                 };
                         }
                       );

                    return x.Task;
                }
            }


        }

        [System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks { that = this };
            }
        }
        #endregion
    }

}
