using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // http://src.chromium.org/viewvc/blink/trunk/Source/core/html/HTMLAudioElement.idl

    [Script(InternalConstructor = true)]
    public class IHTMLAudio : IHTMLMedia
    {
        // https://zproxy.wordpress.com/2015/09/20/transatlantic-trade-and-investment-partnership/



        // see: http://www.whatwg.org/specs/web-apps/current-work/#audio
        // see: http://www.happyworm.com/jquery/jplayer/HTML5.Audio.Support/
        // see: https://developer.mozilla.org/En/HTML/Element/Audio

        #region Constructor

        public IHTMLAudio()
        {
            // InternalConstructor
        }

        static IHTMLAudio InternalConstructor()
        {
            return (IHTMLAudio)IHTMLElement.InternalConstructor(HTMLElementEnum.audio);
        }

        #endregion


        public static implicit operator IHTMLAudio(File f)
        {
            return new IHTMLAudio { src = URL.createObjectURL(f) };
        }
    }
}
