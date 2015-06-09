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
using BinauralAudioExperiment;
using BinauralAudioExperiment.Design;
using BinauralAudioExperiment.HTML.Pages;

namespace BinauralAudioExperiment
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
            // http://www.theverge.com/2015/2/12/8021733/3d-audio-3dio-binaural-immersive-vr-sound-times-square-new-york

            new IStyle(IHTMLElement.HTMLElementEnum.button)
            {
                width = "5em",
                height = "5em",
            };

            new IHTMLButton { "front-left" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.front_left().play());
            new IHTMLButton { "front" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.front().play());
            new IHTMLButton { "front-right" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.front_right().play());
            new IHTMLHorizontalRule { }.AttachToDocument();

            new IHTMLButton { "left" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.left().play());
            new IHTMLButton { "?" }.AttachToDocument();
            new IHTMLButton { "right" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.right().play());
            new IHTMLHorizontalRule { }.AttachToDocument();

            new IHTMLButton { "back-left" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.back_left().play());
            new IHTMLButton { "back" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.back().play());
            new IHTMLButton { "back-right" }.AttachToDocument().WhenClicked(x => new BinauralAudioExperiment.HTML.Audio.FromAssets.back_right().play());
            new IHTMLHorizontalRule { }.AttachToDocument();
        }

    }
}

//---------------------------
//Microsoft Visual Studio
//---------------------------
//Project 'BinauralAudioExperiment' could not be opened because the Visual C# 2015 RC compiler could not be created. 'BinauralAudioExperiment' is already part of the workspace.
//---------------------------
//OK   
//---------------------------
