using TestExpressionBodiedFunction;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestExpressionBodiedFunction
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            //this.ParentForm.Text = this.ToString();
            this.ParentForm.Text = this.goo;
        }

        public override string ToString() => "hello world";

        public string goo => "goo: " + this.ToString();
    }
}
