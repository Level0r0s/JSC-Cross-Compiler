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
using HebrewGenesis;
using HebrewGenesis.Design;
using HebrewGenesis.HTML.Pages;
using System.Collections;

namespace HebrewGenesis
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
            // http://www.freetype.org/ttfautohint/#works

            // http://www.1001fonts.com/cabin-font.html#license
            // http://www.bibleworks.com/fonts.html

            // http://www-01.sil.org/computing/fonts/silhebrew/index.htm

            // if we add a ttf font to Font what will jsc build for AssetsLibrary?

            // http://www.w3schools.com/cssref/pr_font_font-family.asp
            // http://www.w3schools.com/cssref/css_websafe_fonts.asp

            // ttf not web friendly?
            // lowercase needed?
            // SILEZRA_.TTF

            // nope
            //new IHTMLPre { "hello world SHALOOLD ויהי אור", new Fonts.SHALOOLD { } }.AttachToDocument();
            //new IHTMLPre { "hello world Bsthebre ויהי אור", new Fonts.Bsthebre { } }.AttachToDocument();
            //new IHTMLPre { "hello world HEBREW ויהי אור", new Fonts.HEBREW { } }.AttachToDocument();
            //new IHTMLPre { "hello world SILEZRA_   ויהי אור", new Fonts.SILEZRA_ { } }.AttachToDocument();
            //new IHTMLPre { "hello world cabin_regular  ויהי אור", new Fonts.cabin_regular { } }.AttachToDocument();
            //new IHTMLPre { "hello world  ויהי אור", new Fonts.bwhebb { } }.AttachToDocument();
            //new IHTMLPre { "hello world  ויהי אור", new Fonts.bwhebl { } }.AttachToDocument();

            // http://biblehub.com/text/genesis/1-1.htm
            // http://www.bible-researcher.com/sblhebrew.html
            // http://www.sbl-site.org/educational/BiblicalFonts_SBLHebrew.aspx

            // Ezra SIL, SBL Hebrew, Palatino Linotype, Palatino, Century Schoolbook L, Times New Roman, Cardo, Arial, Helvetica, Sans-serif
            // SBL Hebrew

            new IStyle(Native.css[IHTMLElement.HTMLElementEnum.body])
            {
                width = "50%",
                textAlign = IStyle.TextAlignEnum.right
            };


            Native.css[IHTMLElement.HTMLElementEnum.pre].before.contentXAttribute = new XAttribute("title", "");

            new IStyle(Native.css[IHTMLElement.HTMLElementEnum.pre].before)
            {
                color = "red",

                position = IStyle.PositionEnum.absolute,
                left = "50%",

                paddingLeft = "2em"
            };

            Native.document.title = "Genesis 1:1";

            new IStyle(IHTMLElement.HTMLElementEnum.head).display = IStyle.DisplayEnum.block;
            new IStyle(IHTMLElement.HTMLElementEnum.title)
            {
                display = IStyle.DisplayEnum.block,
                textAlign = IStyle.TextAlignEnum.center,
                padding = "2em",

                //color = "red",
                borderBottom = "1px dotted gray"
            };


            // what about shadow elements?
            new[] {
                new IHTMLPre { "בְּרֵאשִׁ֖ית", new Fonts.SBL_Hbrw { }, new XAttribute("title", "In the beginning") }.AttachToDocument(),
                new IHTMLPre { "בָּרָ֣א", new Fonts.SBL_Hbrw { }, new XAttribute("title", "created")}.AttachToDocument(),
                new IHTMLPre { "אֱלֹהִ֑ים", new Fonts.SBL_Hbrw { }, new XAttribute("title", "God") }.AttachToDocument(),
                new IHTMLPre { "אֵ֥ת", new Fonts.SBL_Hbrw { }, new XAttribute("title", "-") }.AttachToDocument(),
                new IHTMLPre { "הַשָּׁמַ֖יִם", new Fonts.SBL_Hbrw { }, new XAttribute("title", "the heavens")  }.AttachToDocument(),
                new IHTMLPre { "וְאֵ֥ת", new Fonts.SBL_Hbrw { }, new XAttribute("title", "and")  }.AttachToDocument(),
                new IHTMLPre { "הָאָֽרֶץ׃", new Fonts.SBL_Hbrw { }, new XAttribute("title", "the earth")  }.AttachToDocument()
            }.With(
                async lines =>
                {
                    //while (await Native.document.documentElement.async.onclick)
                    //{
                    //    foreach (var item in lines)
                    //    {
                    //        var x = new { item.title, item.innerText };

                    //        item.title = x.innerText;
                    //        item.innerText = x.title;
                    //    }
                    //}
                }
            );


            // can we have a second element?
            //new IHTMLElement("title", "Genesis 2:1").AttachToDocument();
            // http://biblehub.com/text/genesis/1-2.htm


            new IHTMLElement("title", "Genesis 1:2").AttachTo(Native.document.documentElement);
            new AddStringString(
                (x, y) => new IHTMLPre { x, new Fonts.SBL_Hbrw { }, new XAttribute("title", y) }.AttachTo(new IHTMLBody { }.AttachTo(Native.document.documentElement))
            )
            {
                {"וְהָאָ֗רֶץ", "And the earth"},
                {"הָיְתָ֥ה", "was"},
                {"תֹ֙הוּ֙", "without form"},
                {"וָבֹ֔הוּ", "and void"},
                {"וְחֹ֖שֶׁךְ", "and darkness"},
                {"עַל־", "[was] on"},
                {"פְּנֵ֣י", "the face"},
                {"תְה֑וֹם", "of the deep"},
                {"וְר֣וּחַ", "And the Spirit"},
                {"אֱלֹהִ֔ים", "of God"},
                {"מְרַחֶ֖פֶת", "moved"},
                {"עַל־", "on"},
                {"פְּנֵ֥י", "the face"},
                {"הַמָּֽיִם׃", "of the waters"},
            };


            new IHTMLElement("title", "Genesis 1:3").AttachTo(Native.document.documentElement);
            new AddStringString(
                (x, y) => new IHTMLPre { x, new Fonts.SBL_Hbrw { }, new XAttribute("title", y) }.AttachTo(new IHTMLBody { }.AttachTo(Native.document.documentElement))
            )
            {
                {"וַיֹּ֥אמֶר", "And said"},
                {"אֱלֹהִ֖ים", "God"},
                {"יְהִ֣י", "Let there be"},
                {"א֑וֹר", "light"},
                {"וַֽיְהִי־", "and there was"},
                {"אֽוֹר׃", "light"},
               
            };

        }

    }

    class AddStringString : IEnumerable
    {
        // Error	5	Cannot initialize type 'HebrewGenesis.AddStringString' with a collection initializer because it does not implement 'System.Collections.IEnumerable'	Z:\jsc.svn\examples\javascript\css\ttf\HebrewGenesis\Application.cs	136	13	HebrewGenesis


        public AddStringString(Action<string, string> vAdd)
        {
            this.vAdd = vAdd;
        }

        Action<string, string> vAdd;

        public void Add(string x, string y)
        {
            vAdd(x, y);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
