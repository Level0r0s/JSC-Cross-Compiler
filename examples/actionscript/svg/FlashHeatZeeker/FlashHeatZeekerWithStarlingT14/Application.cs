using FlashHeatZeekerWithStarlingT14.Design;
using FlashHeatZeekerWithStarlingT14.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.ActionScript.flash.display;
using Abstractatech.ConsoleFormPackage.Library;
using System.Windows.Forms;

namespace FlashHeatZeekerWithStarlingT14
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            sprite.wmode();

            sprite.AttachSpriteToDocument().With(
                   embed =>
                   {
                       embed.style.SetLocation(0, 0);
                       embed.style.SetSize(Native.Window.Width, Native.Window.Height);

                       Native.Window.onresize +=
                           delegate
                           {
                               embed.style.SetSize(Native.Window.Width, Native.Window.Height);
                           };
                   }
               );


            #region con
            var con = new ConsoleForm();

            con.InitializeConsoleFormWriter();

            con.Show();

            con.Left = Native.Window.Width - con.Width;
            con.Top = 0;

            Native.Window.onresize +=
                  delegate
                  {
                      con.Left = Native.Window.Width - con.Width;
                      con.Top = 0;
                  };


            con.Opacity = 0.6;


            sprite.InitializeConsoleFormWriter(
                       Console.Write,
                       Console.WriteLine
                   );
            #endregion



            sprite.fps +=
                fps =>
                {
                    con.Text = new { fps }.ToString();
                };


            sprite.context_new_remotegame +=
                remotegame =>
                {
                    var remotegame_con = new ConsoleForm();

                    remotegame_con.Show();
                    remotegame_con.Left = 0;
                    remotegame_con.Top = Native.Window.Height - remotegame_con.Height;

                    remotegame_con.Opacity = 0.5;

                    remotegame.AtTitleChange +=
                        e => remotegame_con.Text = e;

                    remotegame.AtWriteLine +=
                        e =>
                        {
                            remotegame_con.textBox1.AppendText(e + Environment.NewLine);
                            remotegame_con.textBox1.ScrollToCaret();
                        };


                };

            {
                int c = 2000;

                Action<MessageEvent> window_onmessage =
                     e =>
                     {
                         var xml = XElement.Parse((string)e.data);

                         c++;
                         //Console.WriteLine(c + " window -> sprite " + xml);

                         sprite.game_postMessage(xml);
                     };


                Console.WriteLine("add window_onmessage");
                Native.Window.onmessage += window_onmessage;


            }

            Action<XElement> sprite_context_onmessage = delegate { };

            int ccc = 0;
            sprite.context_onmessage +=
                e =>
                {
                    ccc++;
                    //Console.WriteLine(ccc + " sprite ->  " + e);
                    sprite_context_onmessage(e);
                };

            if (Native.Window.opener != null)
            {
                // opener closes, we close. 
                Native.Window.opener.onbeforeunload +=
                    delegate
                    {
                        Native.Window.close();
                    };

                sprite_context_onmessage +=
                    e =>
                    {
                        Native.Window.opener.postMessage(e.ToString());
                    };
            }
            else
            {
                new Button { Text = "Secondary View" }.With(
                    connect =>
                    {
                        connect.AttachTo(con);

                        connect.Left = 8;
                        connect.Top = 8;

                        connect.Click +=
                            delegate
                            {
                                var w = Native.Window.open(Native.Document.location.href, "_blank", 600, 600, false);


                                w.onload +=
                                    delegate
                                    {
                                        Console.WriteLine("loaded: " + w.document.location.href);

                                        if (w.document.location.href == "about:blank")
                                        {
                                            // fck you blank:P 4h of debugging for this.

                                            return;
                                        }

                                        //Native.Window.onmessage +=
                                        //     e =>
                                        //     {
                                        //         if (e.source == w)
                                        //             return;

                                        //         // relay, not echo
                                        //         w.postMessage(e.data);
                                        //     };

                                        var w_closed = false;

                                        w.onbeforeunload +=
                                            delegate
                                            {
                                                w_closed = true;
                                            };

                                        var xcc = 0;
                                        Action<XElement> __sprite_context_onmessage = e =>
                                        {
                                            if (w_closed)
                                                return;

                                            xcc++;
                                            //Console.WriteLine(xcc + " to child ->  " + e);
                                            w.postMessage(e.ToString());
                                        };


                                        sprite_context_onmessage += __sprite_context_onmessage;

                                    };
                            };
                    }
                );
            }
        }

    }


    public static class XX
    {
        public static void wmode(this Sprite s, string value = "direct")
        {
            var x = s.ToHTMLElement();

            var p = x.parentNode;
            if (p != null)
            {
                // if we continue, element will be reloaded!
                return;
            }

            x.setAttribute("wmode", value);


        }
    }
}
