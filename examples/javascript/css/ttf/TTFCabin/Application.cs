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
using TTFCabin;
using TTFCabin.Design;
using TTFCabin.HTML.Pages;

namespace TTFCabin
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
            // https://github.com/typekit/webfontloader
            // font-family: 'CabinRegular'

            var r = IStyleSheet.all.AddFontFaceRule(
              "'Blokk <dynamic>'",
               Fonts.Cabin_Regular_TTF.GetSource()
          );


            new IHTMLParagraph { 
                "Liitumiskeskkonna eesmärk on anda isikutele, kes on huvitatud taastuvenergia seadme paigaldamisest kinnistule, kiire esialgne hinnang võrguga liitumise kohta ning muud kõikvõimalikku informatsiooni taastuvenergia mikrotootjaks hakkamise osas.", 
                
                //new Fonts.Cabin_Regular_TTF("CabinRegular") { } 
            }.AttachToDocument();

            new IHTMLParagraph { 
                "Liitumiskeskkonna eesmärk on anda isikutele, kes on huvitatud taastuvenergia seadme paigaldamisest kinnistule, kiire esialgne hinnang võrguga liitumise kohta ning muud kõikvõimalikku informatsiooni taastuvenergia mikrotootjaks hakkamise osas.", 
                
                new Fonts.Cabin_Regular_TTF("CabinRegular") { } 
            }.AttachToDocument();


            new IHTMLParagraph { 
                "Liitumiskeskkonna eesmärk on anda isikutele, kes on huvitatud taastuvenergia seadme paigaldamisest kinnistule, kiire esialgne hinnang võrguga liitumise kohta ning muud kõikvõimalikku informatsiooni taastuvenergia mikrotootjaks hakkamise osas.", 
                
                new Fonts.Cantarell_Regular { } 
            }.AttachToDocument();

        }

    }
}
