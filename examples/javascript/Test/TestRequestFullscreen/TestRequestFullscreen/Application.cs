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
using TestRequestFullscreen;
using TestRequestFullscreen.Design;
using TestRequestFullscreen.HTML.Pages;

namespace TestRequestFullscreen
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
			// http://updates.html5rocks.com/2014/11/Support-for-theme-color-in-Chrome-39-for-Android

			//Native.document.onful

			new IHTMLButton { "go fullscreen" }.AttachToDocument().With(
				async button =>
				{
					while (await button.async.onclick)
					{

						// NaN on windows!
						new IHTMLPre { new { Native.window.orientation } }.AttachToDocument();

						// Failed to execute 'requestFullScreen' on 'Element': API can only be initiated by a user gesture.

						//var infullscreen = new { Native.window.orientation };

						// using
						Native.document.documentElement.requestFullscreen();

						//new IHTMLPre { "to exit or reenter fullscreen, reorient device." }.AttachToDocument();

						//await Native.window.onorientationchange;
						await Native.window.async.onorientationchange;
						new IHTMLPre { new { Native.window.orientation } }.AttachToDocument();


						Native.document.exitFullscreen();
					}

				}
			);
		}

	}
}
