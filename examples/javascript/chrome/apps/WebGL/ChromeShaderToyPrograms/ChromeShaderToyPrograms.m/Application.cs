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
using ChromeShaderToyPrograms.m;
using ChromeShaderToyPrograms.m.Design;
using ChromeShaderToyPrograms.m.HTML.Pages;
using ScriptCoreLib.JavaScript.WebGL;
using System.Diagnostics;
using ChromeShaderToyColumns.Library;

namespace ChromeShaderToyPrograms.m
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // could assetslibrary auto byref those projects ?
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150818
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150809/chrome-filesystem

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {




            //Native.document.documentElement.style.overflow = IStyle.OverflowEnum.auto;


            // chrome by default has no scrollbar, bowser does
            Native.document.documentElement.style.overflow = IStyle.OverflowEnum.hidden;
            Native.body.style.margin = "0px";
            Native.body.Clear();

            // ipad?
            Native.window.onerror +=
                e =>
                {
                    new IHTMLPre {
                        "error " + new { e.error }
                    }.AttachToDocument();
                };

            // https://www.youtube.com/watch?v=tnS8K0yhmZU
            // http://www.reddit.com/r/oculus/comments/2sv5lk/new_release_of_shadertoy_vr/
            // https://www.shadertoy.com/view/lsSGRz

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150309
            // https://zproxy.wordpress.com/2015/03/09/project-windstorm/
            // https://github.com/jimbo00000/RiftRay


            #region += Launched chrome.app.window
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                if (!(Native.window.opener == null && Native.window.parent == Native.window.self))
                {
                    Console.WriteLine("chrome.app.window.create, is that you?");

                    // pass thru
                }
                else
                {
                    // should jsc send a copresence udp message?
                    chrome.runtime.UpdateAvailable += delegate
                    {
                        new chrome.Notification(title: "UpdateAvailable");

                    };

                    chrome.app.runtime.Launched += async delegate
                    {
                        // 0:12094ms chrome.app.window.create {{ href = chrome-extension://aemlnmcokphbneegoefdckonejmknohh/_generated_background_page.html }}
                        Console.WriteLine("chrome.app.window.create " + new { Native.document.location.href });

                        new chrome.Notification(title: "ChromeUDPSendAsync");

                        var xappwindow = await chrome.app.window.create(
                               Native.document.location.pathname, options: null
                        );

                        //xappwindow.setAlwaysOnTop

                        xappwindow.show();

                        await xappwindow.contentWindow.async.onload;

                        Console.WriteLine("chrome.app.window loaded!");
                    };


                    return;
                }
            }
            #endregion



            Console.WriteLine("create WebGLRenderingContext");

            var gl = new WebGLRenderingContext(alpha: true);

            if (gl == null)
            {
                Native.body.style.backgroundColor = "yellow";

                new IHTMLPre {
                    // https://code.google.com/p/chromium/issues/detail?id=294207
                    "Rats! WebGL hit a snag.",

                    //new IHTMLAnchor { href = "about:gpu", innerText = "about:gpu" }
                }.AttachToDocument();
                return;
            }


            Native.body.style.backgroundColor = "blue";

            gl.oncontextlost += async e =>
            {
                //  The GPU process hung. Terminating after 10000 ms.

                // GpuProcessHostUIShim: The GPU process crashed!

                Native.body.style.backgroundColor = "red";

                Native.body.Clear();

                new IHTMLPre {
                    // https://code.google.com/p/chromium/issues/detail?id=294207
                    //"The GPU process crashed! (or did the graphics driver crash?)",
                    "The GPU process crashed! (or did the display driver crash?)",

                    //new IHTMLAnchor { href = "about:gpu", innerText = "about:gpu" }
                }.AttachToDocument();

                // reload?

                //if (Native.window.confirm("oncontextlost, retry or refresh later?"))
                //{
                //    Native.body.style.backgroundColor = "yellow";

                //    //e.
                //}

                await new IHTMLButton { "blacklist " + new { Native.document.location.hash } + " shader,  reload tab to reload gpu. (or restart browser)" }.AttachToDocument().async.onclick;

                Native.window.localStorage[new { Native.document.location.hash }] = "blacklisted";

                // reload gpu?
                Native.document.location.reload();
            };

            //gl.canvas.async.oncont

            var combo = new IHTMLSelect().AttachToDocument();

            combo.style.position = IStyle.PositionEnum.absolute;
            combo.style.left = "0px";
            combo.style.top = "0px";
            //combo.style.right = "0px";
            combo.style.width = "100%";

            combo.style.backgroundColor = "rgba(255,255,255,0.5)";
            //combo.style.backgroundColor = "rgba(255,255,0,0.5)";
            //combo.style.background = "linear-gradient(to bottom, rgba(255,255,255,0.5 0%,rgba(255,255,255,0.0 100%))";
            combo.style.border = "0px solid transparent";
            combo.style.fontSize = "large";
            combo.style.paddingLeft = "1em";
            combo.style.fontFamily = IStyle.FontFamilyEnum.Verdana;
            combo.style.cursor = IStyle.CursorEnum.pointer;



            //var mAudioContext = new AudioContext();


            var c = gl.canvas.AttachToDocument();

            #region onresize
            new { }.With(
                async delegate
                {
                    do
                    {
                        c.width = Native.window.Width;
                        c.height = Native.window.Height;
                        c.style.SetSize(c.width, c.height);
                    }
                    while (await Native.window.async.onresize);
                }
            );
            #endregion



            #region CaptureMouse
            var mMouseOriX = 0;
            var mMouseOriY = 0;
            var mMousePosX = 0;
            var mMousePosY = 0;

            c.onmousedown += async ev =>
            {
                mMouseOriX = ev.CursorX;
                //mMouseOriY = ev.CursorY;
                mMouseOriY = c.height - ev.CursorY;

                mMousePosX = mMouseOriX;
                mMousePosY = mMouseOriY;

                // why aint it canvas?
                //ev.Element
                //ev.CaptureMouse();

                // using ?
                ev.Element.requestPointerLock();
                await ev.Element.async.onmouseup;
                Native.document.exitPointerLock();

                mMouseOriX = -Math.Abs(mMouseOriX);
                mMouseOriY = -Math.Abs(mMouseOriY);
            };

            //c.ontouchmove += 

            c.onmousemove += ev =>
            {
                if (ev.MouseButton == IEvent.MouseButtonEnum.Left)
                {
                    mMousePosX += ev.movementX;
                    mMousePosY += ev.movementY;
                }
            };

            c.onmousewheel += ev =>
            {
                ev.preventDefault();
                ev.stopPropagation();

                mMousePosY += 3 * ev.WheelDirection;
            };

            #endregion



            // http://www.wufoo.com/html5/attributes/05-list.html
            // http://www.w3schools.com/tags/att_input_list.asp
            //uiauto.datalist1.EnsureID();
            //uiauto.search.list = uiauto.datalist1.id;
            //uiauto.datalist1.id = "datalist1";
            //uiauto.search.list = "datalist1";
            //new IHTMLPre { new { uiauto.search.list, uiauto.datalist1.id } }.AttachToDocument();

            var sw = Stopwatch.StartNew();


            //new IHTMLOption { value = "", innerText = $"{References.programs.Count} shaders available" }.AttachTo(combo);
            new IHTMLOption { value = "", innerText = m.programs.Count + " shaders available" }.AttachTo(combo);


            // should bind the selection to uri and reload if gpu crashes.

            #region can we have a next button?
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20150807/shadertoy
            new IHTMLButton { "next" }.AttachToDocument().With(
                next =>
                {
                    new IStyle(next)
                    {
                        position = IStyle.PositionEnum.absolute,
                        right = "1em",
                        top = "2em",
                        bottom = "1em",
                        padding = "4em"
                    };

                    next.onclick += delegate
                    {
                        var n = combo.selectedIndex + 1;
                        Console.WriteLine(new { n });
                        combo.selectedIndex = n;

                        // if we are on a laptop, by clicking the button perhaps now you want to click right arrow?
                        combo.focus();
                    };
                }
            );
            #endregion

            // do not add byref if bypackage is available!

            //            Severity Code    Description Project File Line
            //Error CS0433  The type 'ShaderToy' exists in both 'ChromeShaderToyColumns, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' and 'WebGL ShaderToy, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'    ChromeShaderToyPrograms.m Z:\jsc.svn\examples\javascript\chrome\apps\WebGL\ChromeShaderToyPrograms\ChromeShaderToyPrograms.m\Application.cs   283

            ShaderToy.EffectPass pip = null;

            // http://stackoverflow.com/questions/25289390/html-how-to-make-input-type-list-only-accept-a-list-choice
            //References.programs.Keys.WithEachIndex(
            m.programs.Keys.WithEachIndex(
                async (key, index) =>
                {
                    //var text = (1 + index) + " of " + References.programs.Count + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");
                    var text = (1 + index) + " of " + m.programs.Count + " " + key.SkipUntilIfAny("ChromeShaderToy").Replace("By", " by ");

                    var blacklisted = Native.window.localStorage[new { hash = "#" + key }] == "blacklisted";
                    if (blacklisted)
                    {
                        var option0 = new IHTMLOption { value = key, innerText = "blacklisted " + text }.AttachTo(combo);
                        return;
                    }


                    var option = new IHTMLOption { value = key, innerText = text }.AttachTo(combo);

                    await Native.window.async.onframe;

                    // um should we preselect it? did we come from a reload? did gpu crash?
                    if (Native.document.location.hash == "#" + key)
                    {
                        // 0 is the header.. should it be a optionheader instead?
                        combo.selectedIndex = 1 + index;
                    }


                    // we are about to create 100 objects. does it have any impact to UI?
                    //var frag = References.programs[key]();
                    var frag = m.programs[key]();

                    // 0ms Error: {{ infoLog = WARNING: 0:13: '=' : global variable initializers should be constant expressions (uniforms and globals are allowed in global initializers for legacy compatibility)
                    // can we detect errors correctly?

                    var len = frag.ToString().Length;

                    option.innerText = text + " " + new
                    {
                        //frame,
                        //load = load.ElapsedMilliseconds + "ms ",

                        frag = len + "bytes ",
                        // a telemetry to track while running on actual hardware
                        //fragGPU = pass0.xCreateShader.fsTranslatedShaderSource.Length + " bytes"
                    };

                    // cant we get it if we manually set it?
                    await option.async.onselect;

                    // first time select. before we try to load the shader, lets make sure we remember what causes the gpu crash
                    // 2278ms select {{ key = InvadersByIapafoto, index = 10, hash = #InputTimeByIq }}
                    await Native.document.location.replace("#" + key);

                    // need two frames to see a change in hash?
                    //await Native.window.async.onframe;
                    //await Native.window.async.onframe;


                    // unless from previous refresh it crashed gpu?
                    Console.WriteLine("select " + new { key, index, Native.document.location.hash });

                    var load = Stopwatch.StartNew();

                    // defined at?
                    // "C:\util\jsc\nuget\WebGL.ShaderToy.1.0.0.0.nupkg"
                    var pass0 = new ShaderToy.EffectPass(
                        gl: gl,
                        precission: ShaderToy.DetermineShaderPrecission(gl),
                        supportDerivatives: gl.getExtension("OES_standard_derivatives") != null
                    );
                    pass0.MakeHeader_Image();
                    pass0.NewShader_Image(frag);

                    load.Stop();

                    new { }.With(
                        async delegate
                        {
                            while (await option.async.ondeselect)
                            {
                                pip = pass0;

                                await option.async.onselect;
                            }
                        }
                    );

                    var framesInSecond = 0;
                    var theSecond = Stopwatch.StartNew();

                    var frame = 0;
                    do
                    {
                        // can we change uri so a refresh would reload the same program?
                        // perhaps its time to review historic api?
                        Native.document.location.replace("#" + key);


                        frame++;
                        framesInSecond++;

                        if (theSecond.ElapsedMilliseconds >= 1000)
                        {
                            //option.innerText = key + new { frame };
                            option.innerText = text + " " + new
                            {
                                //frame,
                                framesInSecond,
                                load = load.ElapsedMilliseconds + "ms ",

                                frag = len + "bytes ",
                                // a telemetry to track while running on actual hardware
                                fragGPU = pass0.xCreateShader.fsTranslatedShaderSource.Length + " bytes"
                            };

                            framesInSecond = 0;
                            //theSecond.Restart();
                            theSecond = Stopwatch.StartNew();
                        }


                        // can we scale?
                        pass0.Paint_Image(
                            sw.ElapsedMilliseconds / 1000.0f,

                            mMouseOriX,
                            mMouseOriY,
                            mMousePosX,
                            mMousePosY,

                            zoom: 1.0f
                        );

                        if (pip != null)
                        {
                            // can we scale?
                            pip.Paint_Image(
                                sw.ElapsedMilliseconds / 1000.0f,

                                mMouseOriX,
                                mMouseOriY,
                                mMousePosX,
                                mMousePosY,

                                zoom: 0.10f
                            );

                        }

                        // what does it do?
                        gl.flush();

                        // wither we are selected or we are pip?
                        await option.async.selected;
                    }
                    while (await Native.window.async.onframe);

                }
            );





        }

    }
}
