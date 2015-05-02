using chrome;
using ChromeTCPServerWithFrameNone;
using ChromeTCPServerWithFrameNone.Design;
using ChromeTCPServerWithFrameNone.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ChromeTCPServer
{
	// http://www.snip2code.com/Snippet/19734/Visual-studio-intellisense-file-for-chro
	[Script(HasNoPrototype = true)]
	class xPointerLockPermissionRequest
	{
		// https://developer.chrome.com/apps/tags/webview#type-PointerLockPermissionRequest

		// tested by ?
		public void allow()
		{
		}
	}

	public static class TheServerWithStyledForm
	{
		public static void Invoke(
			string AppSource,
			int DefaultWidth = 640,
			int DefaultHeight = 480,
			Action<FormStyler> AtFormCreated = null,

			// X:\jsc.svn\examples\javascript\chrome\apps\ChomeAlphaAppWindow\ChomeAlphaAppWindow\Application.cs
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeEarth\ChromeEarth\Application.cs
			bool transparentBackground = false,
			bool resizable = true,



			Action<Form> AtFormConstructor = null
			)
		{
			Console.WriteLine("enter TheServerWithStyledForm.Invoke " + new { AtFormConstructor });




			#region  AtFormCreated
			if (AtFormCreated == null)
				AtFormCreated = AtFormCreated = s =>
				{
					// X:\jsc.svn\examples\javascript\IsometricTycoonViewWithToolbar\IsometricTycoonViewWithToolbar\Application.cs
					// X:\jsc.internal.svn\core\com.abstractatech.web\com.abstractatech.web\Domains\discover.xavalon.net\discover_xavalon_net.cs

					// browser popup will use this color

					// FormBackgroundColor
					//((__Form)s.Context).HTMLTargetContainerRef.style.backgroundColor = JSColor.FromRGB(0, 0, 0);

					s.Caption.style.backgroundColor = JSColor.FromRGB(0, 0, 0);
					s.TargetOuterBorder.style.boxShadow = "rgba(0, 0, 0, 0.3) 0px 0px 6px 3px";
					s.TargetOuterBorder.style.borderColor = JSColor.FromRGB(0, 0, 0);

					s.TargetInnerBorder.style.borderWidth = "0px";

					s.CloseButton.style.color = JSColor.White;
					s.CloseButton.style.backgroundColor = JSColor.None;
					s.CloseButton.style.borderWidth = "0px";
					s.CloseButtonContent.style.borderWidth = "0px";

					s.TargetResizerPadding.style.left = "0px";
					s.TargetResizerPadding.style.top = "0px";
					s.TargetResizerPadding.style.right = "0px";
					s.TargetResizerPadding.style.bottom = "0px";

				};


			FormStyler.AtFormCreated = AtFormCreated;
			#endregion



			var windowsForm = new Dictionary<Form, AppWindow>();
			var windows = new List<AppWindow>();

			#region __Form
			{


				__Form.InternalHTMLTargetAttachToDocument =
				   async (that, yield) =>
				   {
					   // --enable-logging --v=1
					   // http://stackoverflow.com/questions/12219058/how-to-debug-a-chrome-browser-extension-which-is-crashing
					   // "C:\Users\Arvo\AppData\Local\Google\Chrome SxS\Application\debug.log"
					   // "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"


					   // X:\jsc.svn\examples\javascript\chrome\apps\ChomeAlphaAppWindow\ChomeAlphaAppWindow\Application.cs
					   // X:\jsc.svn\examples\javascript\android\com.abstractatech.gamification.craft\com.abstractatech.gamification.craft\Application.cs

					   Console.WriteLine("enter InternalHTMLTargetAttachToDocument");

					   //Error in event handler for app.runtime.onLaunched: Error: Invalid value for argument 2. Property 'transparentBackground': Expected 'boolean' but got 'integer'.
					   //var transparentBackground = true;


					   #region options
					   // jsc does not use bool literals correctly
					   var ztransparentBackground = false;



					   object options = new
					   {
						   frame = "none",
					   };

					   if (transparentBackground)
					   {
						   ztransparentBackground = true;
						   options = new
						   {
							   frame = "none",
							   //transparentBackground = ztransparentBackground

							   // X:\jsc.svn\examples\javascript\chrome\apps\ChomeAlphaAppWindow\ChomeAlphaAppWindow\Application.cs

							   // even if false, permission is checked. why?
							   alphaEnabled = ztransparentBackground

						   };

					   }

					   Console.WriteLine(new { options });
					   #endregion

					   // http://src.chromium.org/viewvc/chrome/trunk/src/chrome/common/extensions/api/app_window.idl
					   var xappwindow = await chrome.app.window.create(
							 Native.document.location.pathname,
							options
						);


					   // webview needs special attention later!
					   windowsForm[that] = xappwindow;

					   // Uncaught TypeError: Cannot read property 'contentWindow' of undefined 

					   //Console.WriteLine("appwindow loading... " + new { xappwindow });
					   //Console.WriteLine("appwindow loading... " + new { xappwindow.contentWindow });

					   // our window frame non client area plus inner body margin

					   #region resizeTo
					   if (that.FormBorderStyle == FormBorderStyle.None)
					   {
						   xappwindow.resizeTo(
							  DefaultWidth,
							  DefaultHeight
							 );
					   }
					   else
					   {
						   xappwindow.resizeTo(
							DefaultWidth + 32,
							DefaultHeight + 64
						   );
					   }
					   #endregion




					   await xappwindow.contentWindow.async.onload;

					   #region onload
					   var c = that;
					   var f = (Form)that;
					   var ff = c;

					   windows.Add(xappwindow);

					   // http://sandipchitale.blogspot.com/2013/03/tip-webkit-app-region-css-property.html

					   (ff.CaptionForeground.style as dynamic).webkitAppRegion = "drag";

					   //(ff.ResizeGripElement.style as dynamic).webkitAppRegion = "drag";
					   // cant have it yet
					   //ff.ResizeGripElement.Orphanize();

					   f.StartPosition = FormStartPosition.Manual;


					   f.Left = 0;
					   f.Top = 0;

					   #region FormClosing
					   f.FormClosing +=
						   delegate
					   {
						   Console.WriteLine("FormClosing");
						   xappwindow.close();
					   };
					   #endregion



					   // jsc can you generate instance events too?
					   #region  onRestored
					   xappwindow.onRestored.addListener(
						   new Action(
							   delegate
					   {
						   that.CaptionShadow.Hide();

					   }
						   )
					   );
					   #endregion


					   #region onMaximized
					   xappwindow.onMaximized.addListener(
					   new Action(
							   delegate
					   {
						   that.CaptionShadow.Show();

					   }
					   )
					   );
					   #endregion


					   #region onClosed
					   xappwindow.onClosed.addListener(
								new Action(
									delegate
					   {
						   Console.WriteLine("onClosed");
						   windows.Remove(xappwindow);

						   f.Close();
					   }
							)
							);
					   #endregion

					   // wont fire yet
					   //appwindow.contentWindow.onbeforeunload +=
					   //    delegate
					   //    {
					   //        Console.WriteLine("onbeforeunload");
					   //    };

					   //appwindow.onBoundsChanged.addListener(
					   //        new Action(
					   //        delegate
					   //        {
					   //            Console.WriteLine("appwindow.onBoundsChanged");

					   //            f.SizeTo(
					   //                appwindow.contentWindow.Width,
					   //                appwindow.contentWindow.Height
					   //            );
					   //        }
					   //    )
					   //);

					   f.SizeChanged +=
					   delegate
					   {
						   // who is changing our size?
						   Console.WriteLine(
							   "SizeChanged  " + new { f.Width, f.Height }
							   );
					   };


					   Action SizeFormToAppWindow = delegate
					   {
						   // not called?
						   // why not use appwindow resize/bounds instead?
						   Console.WriteLine("SizeFormToAppWindow " + new { transparentBackground, xappwindow.contentWindow.Width, xappwindow.contentWindow.Height });
						   // transparentBackground

						   // x:\jsc.svn\examples\javascript\chrome\apps\chromenexus7\chromenexus7\application.cs
						   // do we need to show our own shadow?
						   // how will it play with resize

						   if (transparentBackground)
						   {
							   // how much does the shadow need?

							   // if we ale in fullscreen this would not be the right thing to do
							   // also, when can we have resizeable working correctly on chrome?

							   f.Width = xappwindow.contentWindow.Width - 8;
							   f.Height = xappwindow.contentWindow.Height - 8;
						   }
						   else
						   {
							   f.Width = xappwindow.contentWindow.Width;
							   f.Height = xappwindow.contentWindow.Height;
						   }
					   };

					   #region resize
					   xappwindow.contentWindow.onresize +=
									delegate
					   {

						   //Console.WriteLine("appwindow.contentWindow.onresize SizeTo " +
						   //    new
						   //    {
						   //        appwindow.contentWindow.Width,
						   //        appwindow.contentWindow.Height
						   //    }
						   //    );


						   SizeFormToAppWindow();

					   }
							;
					   #endregion


					   SizeFormToAppWindow();



					   Console.WriteLine("appwindow contentWindow onload");


					   that.HTMLTarget.AttachTo(
						   xappwindow.contentWindow.document.body
					   );


					   if (transparentBackground)
					   {
						   // seems like windows7 dwm wont provide fadein animation for us
						   // the shadowdom flip does not yet work for us
						   // can we reuse the css transition instead?

						   // x:\jsc.svn\examples\javascript\chrome\apps\chromenexus7\chromenexus7\application.cs

						   // cant see it. cpu not idle enough to animate?
						   // or animation block another by trigger?
						   yield(true);
					   }
					   else
					   {
						   yield(false);
					   }

					   //Console.WriteLine("appwindow contentWindow onload done");
					   #endregion



					   Console.WriteLine("exit InternalHTMLTargetAttachToDocument");



				   };


			}
			#endregion

			#region __WebBrowser
			{
				// X:\jsc.svn\examples\javascript\chrome\ChromeFormsWebBrowserExperiment\ChromeFormsWebBrowserExperiment\Application.cs
				__WebBrowser.InitializeInternalElement = that =>
				{
					// the other option is to have Native to point the AppWindow this is supposed to run in?


					Console.WriteLine("[413165] new webview for current AppWindow " + new { windows.Count });


					// since we do not have a ref to the new AppWindow
					// lets create a placeholder until wo do know.
					that.InternalElement = (IHTMLIFrame)(object)Native.document.createElement("no-webview");

					// <no-webview class=" WebBrowser" style="position: absolute; left: 0px; top: 0px; width: 640px; height: 480px;"></no-webview>

					// what is supposed to trigger it?
					that.InternalAtAfterVisibleChanged +=
						delegate
					{
						var form = that.FindForm();

						Console.WriteLine(" __WebBrowser.InternalAtAfterVisibleChanged " + new
						{
							windows.Count,
							windowsForm = windowsForm.Count,
							form
						});

						// 9:16907ms  __WebBrowser.InternalAtAfterVisibleChanged { Count = 1, f = <Namespace>.Form }
						var w = windowsForm[form];

						//  9:5642ms  __WebBrowser.InternalAtAfterVisibleChanged { w = [object Object] }
						//Console.WriteLine(" __WebBrowser.InternalAtAfterVisibleChanged " + new { w });

						// time to replace it?

						//that.InternalElement = (IHTMLIFrame)(object)webview;


						// https://code.google.com/p/chromium/issues/detail?id=413165#c2

						// X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\webkit\WebView.cs

						//var webview = Native.document.createElement("webview");


						// which AppWindow?
						// 9:4271ms [413165] new webview for current AppWindow { Count = 0 }


						var webview = w.contentWindow.document.createElement("webview");

						// copy old attributes to the new webview element! src  n friends 
						that.InternalElement.attributes.WithEach(
							a =>
								{
									Console.WriteLine(" __WebBrowser" + new { a.name, a.value });

									webview.setAttribute(
										a.name,
										a.value
									);

								}
						);

						// 9:4866ms  __WebBrowser{ name = class, value =  WebBrowser }
						//view-source:42999 9:4870ms  __WebBrowser{ name = style, value = position: absolute; left: 0px; top: 0px; width: 512px; height: 384px; }



						// You do not have permission to use <webview> tag. Be sure to declare 'webview' permission in your manifest. 
						webview.setAttribute("partition", "p1");
						webview.setAttribute("allowtransparency", "true");

						// 0xA26D41

						webview.setAttribute("allowfullscreen", "true");


						// ?
						webview.style.Opacity = 0.0;


						//webview.style.display = IStyle.DisplayEnum.none;

						// none wont start loading.. empty will..
						//webview.style.display = IStyle.DisplayEnum.empty;

						// https://developer.chrome.com/apps/tags/webview#event-contentload
						webview.addEventListener("contentload",
							e =>
								{
									Console.WriteLine("contentload");
									// prevent showing white while loading...
									//webview.style.display = IStyle.DisplayEnum.block;
								}
						);


						webview.addEventListener("loadstop",
						 async
						 e =>
							 {
								 Console.WriteLine("loadstop");
								 // prevent showing white while loading...

								 await Task.Delay(100);

								 //webview.style.display = IStyle.DisplayEnum.block;
								 webview.style.Opacity = 1.0;
							 }
						 );

						#region permissionrequest
						// https://github.com/GoogleChrome/chromium-webview-samples
						// permissionrequest
						// https://developer.chrome.com/apps/tags/webview#type-WebRequestEventInteface
						webview.addEventListener("permissionrequest",
							(e) =>
								{
									// https://code.google.com/p/chromium/issues/detail?id=141198

									//% c9:176376ms permissionrequest { { permission = pointerLock } }
									//Uncaught TypeError: Cannot read property 'allow' of undefined
									//< webview >: The permission request for "pointerLock" has been denied.

									// X:\jsc.internal.git\market\chrome\ChromeMyJscSolutionsNet\ChromeMyJscSolutionsNet\Application.cs

									// https://chromium.googlesource.com/chromium/src/+/git-svn/chrome/common/extensions/api/webview_tag.json
									// https://bugzilla.mozilla.org/show_bug.cgi?id=896143
									// https://developer.chrome.com/apps/tags/webview#event-permissionrequest
									// https://code.google.com/p/chromium/issues/detail?id=153540

									//  The permission request for "pointerLock" has been denied.
									// http://stackoverflow.com/questions/16302627/geolocation-in-a-webview-inside-a-chrome-packaged-app
									// http://git.chromium.org/gitweb/?p=chromium.git;a=commitdiff;h=e1d226c0ea739adaed36cc4b617f7a387d44eca0

									string permission = (e as dynamic).permission;
									xPointerLockPermissionRequest e_request = (e as dynamic).request;

									Console.WriteLine("permissionrequest " + new
									{
										permission,
										e,
										e_request
									});
									//% c9:167409ms permissionrequest { { permission = pointerLock } }
									//Uncaught TypeError: Cannot read property 'allow' of undefined

									e.preventDefault();


									//9:122010ms permissionrequest { { permission = pointerLock, e = [object Event], e_request = [object Object] } }
									//9:122028ms delay permissionrequest { { permission = pointerLock, e = [object Event], delay_e_request = [object Object] } }
									//Uncaught Error: < webview >: Permission has already been decided for this "permissionrequest" event. 

									//Expando.

									if (e_request != null)
										e_request.allow();

									//Task.Delay(1).ContinueWith(
									//    delegate
									//{
									//    xPointerLockPermissionRequest delay_e_request = (e as dynamic).request;

									//    Console.WriteLine("delay permissionrequest " + new { permission, e, delay_e_request });


									//    if (delay_e_request != null)
									//        delay_e_request.allow();
									//}
									//);
								}
						);
						#endregion



						// X:\jsc.svn\examples\javascript\WebGL\WebGLYomotsuTPS\WebGLYomotsuTPS\Application.cs
						// http://src.chromium.org/viewvc/chrome/trunk/src/chrome/test/data/extensions/platform_apps/web_view/pointer_lock/main.js








						that.InternalElement.parentNode.replaceChild(webview, that.InternalElement);

						that.InternalElement = (IHTMLIFrame)(object)webview;

						// src was not copied for some reason. force it.
						that.Size = that.Size;
						that.Refresh();


						Console.WriteLine("[413165] new webview ready " + new { that.Url });

					};

					return;


				};

			}
			#endregion


			Console.WriteLine("will enter TheServer.InvokeAsync");
			ChromeTCPServer.TheServer.InvokeAsync(AppSource,

				open:

				async uri =>
				{
					// x:\jsc.svn\examples\javascript\webgl\heatZeekerrts\heatZeekerrts\application.cs
					Console.WriteLine("enter TheServer.InvokeAsync open");

					var f = new Form
					{

						Text = chrome.Notification.DefaultTitle,
						ShowIcon = false
					};


					if (AtFormConstructor != null)
						AtFormConstructor(f);

					//f.BackColor = System.Drawing.Color.Yellow;


					//Refused to frame 'http://192.168.43.252:8877/' because it violates the following Content Security Policy directive: "frame-src 'self' data: chrome-extension-resource:"

					var w = new WebBrowser { }.AttachTo(f);



					#region SizeChanged
					f.SizeChanged +=
						delegate
					{
						//Console.WriteLine("SizeChanged");

						var ClientSize = f.ClientSize;


						w.Width = ClientSize.Width;
						w.Height = ClientSize.Height;

					};
					#endregion


					w.Navigate(uri);

					f.Load +=
						delegate
					{
						Console.WriteLine("f.Load, // InternalAtAfterVisibleChanged ?");
					};

					f.Shown +=
						delegate
					{
						Console.WriteLine("f.Shown, // InternalAtAfterVisibleChanged ?");

					};

					Console.WriteLine(" f.Show(), will it trigger // InternalAtAfterVisibleChanged ?");

					f.Show();

					var x = new TaskCompletionSource<object>();

					f.FormClosed +=
						delegate
					{
						x.SetResult(f);
					};

					await x.Task;

					// Error	8	Since 'System.Func<string,System.Threading.Tasks.Task>' is an async method that returns 'Task', a return keyword must not be followed by an object expression. Did you intend to return 'Task<T>'?	X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs	311	21	ChromeTCPServerWithFrameNone
				}


			);

		}
	}
}

namespace ChromeTCPServerWithFrameNone
{
	/// <summary>
	/// Your client side code running inside a web browser as JavaScript.
	/// </summary>
	public sealed class Application
	{
		public readonly ApplicationWebService service = new ApplicationWebService();

		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IApp page)
		{


			dynamic self = Native.self;
			dynamic self_chrome = self.chrome;
			object self_chrome_socket = self_chrome.socket;

			if (self_chrome_socket != null)
			{
				chrome.Notification.DefaultTitle = "ChromeTCPServerWithFrameNone";

				//Action<string> open =
				//      async uri =>
				//      {
				//                // Error	25	Cannot await 'chrome.Notification'	X:\jsc.svn\examples\javascript\chrome\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs	202	25	ChromeTCPServerWithFrameNone
				//                //Error	26	Only assignment, call, increment, decrement, await, and new object expressions can be used as a statement	X:\jsc.svn\examples\javascript\chrome\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs	204	25	ChromeTCPServerWithFrameNone

				//                await (Task)"Make me a window!".ToNotification();

				//                open(uri);
				//      };



				ChromeTCPServer.TheServerWithStyledForm.Invoke(AppSource.Text);



				return;
			}

		}

	}
}
