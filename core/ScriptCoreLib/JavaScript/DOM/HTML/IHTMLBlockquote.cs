using ScriptCoreLib;

namespace ScriptCoreLib.JavaScript.DOM.HTML
{
    // https://developer.mozilla.org/en/docs/Web/API/HTMLQuoteElement
    // https://developer.mozilla.org/en-US/docs/Web/HTML/Element/blockquote

    [Script(InternalConstructor = true)]
    public class IHTMLBlockquote : IHTMLElement
    {


        #region Constructor

        public IHTMLBlockquote()
        {
            // InternalConstructor
        }

        static IHTMLBlockquote InternalConstructor()
        {
            return (IHTMLBlockquote)((object)new IHTMLElement(HTMLElementEnum.blockquote));
        }

        #endregion
    }
}
