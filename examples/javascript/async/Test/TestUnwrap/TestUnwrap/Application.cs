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
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestUnwrap;
using TestUnwrap.Design;
using TestUnwrap.HTML.Pages;

namespace TestUnwrap
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
			// X:\jsc.svn\examples\javascript\async\test\TestUnwrap\TestUnwrap\Application.cs
			var yt = Task.Run(
				  delegate
				  {
					  return (Task<string>)Task.FromResult(
						  "async value " + new { Thread.CurrentThread.ManagedThreadId }
					  );
				  }
			);

			// async value { ManagedThreadId = 10 }
			new IHTMLPre { yt }.AttachToDocument();


			var tt = Task.Run(
				  delegate
				  {
					  return (Task)Task.FromResult(
						  "async value " + new { Thread.CurrentThread.ManagedThreadId }
					  );
				  }
			);

			// tested?
			tt.ContinueWith(
				task =>
				{
					new IHTMLPre { "Task Run complete" }.AttachToDocument();
				}
			);
		}
	}
}
