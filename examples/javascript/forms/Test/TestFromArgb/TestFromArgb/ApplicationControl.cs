using TestFromArgb;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestFromArgb
{
	public partial class ApplicationControl : UserControl
	{
		public ApplicationControl()
		{
			this.InitializeComponent();
		}

		private void ApplicationControl_Load(object sender, System.EventArgs e)
		{
			this.BackColor = System.Drawing.Color.FromArgb(0xA26D41);

		}
	}
}
