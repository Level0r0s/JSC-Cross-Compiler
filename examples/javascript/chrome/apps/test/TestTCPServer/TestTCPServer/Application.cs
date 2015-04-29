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
using TestTCPServer;
using TestTCPServer.Design;
using TestTCPServer.HTML.Pages;

namespace TestTCPServer
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

			#region ChromeTCPServer
			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				//chrome.Notification.DefaultTitle = "Heat Zeeker";
				//chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Promotion3D_iso1_tiltshift_128().src;

				//#region AtFormCreated
				//FormStyler.AtFormCreated =
				//	 s =>
				//	 {
				//		 s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

				//		 // this is working?
				//		 var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
				//	 };
				//#endregion

				Console.WriteLine("will enter TheServerWithStyledForm.Invoke");
				ChromeTCPServer.TheServer.Invoke(
					AppSource.Text
				);

				return;
			}
			#endregion
		}

	}
}
