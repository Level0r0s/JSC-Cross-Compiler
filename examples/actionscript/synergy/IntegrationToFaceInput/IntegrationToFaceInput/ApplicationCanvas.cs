using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace IntegrationToFaceInput
{
    public class ApplicationCanvas : Canvas
    {
        public readonly Rectangle r = new Rectangle();
        public readonly Rectangle f = new Rectangle();

        public TextBox t;

        public ApplicationCanvas()
        {
         

            r.Fill = Brushes.Black;
            r.AttachTo(this);
            r.MoveTo(8, 8);
            this.SizeChanged += (s, e) => r.SizeTo(this.Width - 16.0, this.Height - 16.0);

            this.t = new TextBox
            {
                Text = "no camera"
            }.AttachTo(this);


            f.Fill = Brushes.Yellow;
            f.AttachTo(this);
            f.SizeTo(16, 16);
        }

    }
}

// "X:\util\air17_sdk_sa_win"
//x:\util\air16_sdk_win\bin\compc.bat
//  -include-sources "." -load-config x:\util\air16_sdk_win/frameworks/airmobile-config.xml -output "X:\jsc.svn\examples\actionscri
//System.ComponentModel.Win32Exception (0x80004005): The system cannot find the file specified


//Y:\com\mrdoob\tools\threedoob\renderers\CommonRenderer.as:75
//Warning: Assignment within conditional.  Did you mean == instead of =?
//                                                if (v.visible = (focus + v.sz > 0))
//                                                    ^

//Y:\com\mrdoob\tools\threedoob\renderers\CommonRenderer.as:186
//Warning: Assignment within conditional.  Did you mean == instead of =?
//                                        if (object.container.visible = (focus + object.sz > 0))
//                                            ^

//Y:\com\mrdoob\tools\threedoob\renderers\LayerRenderer.as:99
//Warning: Assignment within conditional.  Did you mean == instead of =?
//                                                if (v.visible = (focus + v.sz > 0))
//                                                    ^

//Y:\com\mrdoob\tools\threedoob\renderers\LayerRenderer.as:210
//Warning: Assignment within conditional.  Did you mean == instead of =?
//                                        if (object.container.visible = (focus + object.sz > 0))
//                                            ^

//Y:\com\mrdoob\tools\threedoob\renderers\SimpleBitmapRenderer.as:65
//Warning: Assignment within conditional.  Did you mean == instead of =?
//                                        if (object.container.visible = (focus + object.sz > 0))
//                                            ^

