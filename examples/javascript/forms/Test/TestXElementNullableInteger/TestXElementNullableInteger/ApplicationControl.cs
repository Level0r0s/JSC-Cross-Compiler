using TestXElementNullableInteger;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System;

namespace TestXElementNullableInteger
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var i_null = (int?)XElement.Parse("<root></root>").Element("i");
            var i = (int?)XElement.Parse("<root><i>3</i></root>").Element("i");

            //{ i_null = , i = 3 }
            //ApplicationForm.Load

            Console.WriteLine(new { i_null = i_null == null, i });


        }
    }
}
