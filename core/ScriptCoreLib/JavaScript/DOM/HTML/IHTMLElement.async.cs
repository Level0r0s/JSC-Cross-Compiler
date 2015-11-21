using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ScriptCoreLib.JavaScript.DOM.SVG;


namespace ScriptCoreLib.JavaScript.DOM.HTML
{

    public /* abstract */ partial class IHTMLElement :
        IElement,

        // Error	17	'ScriptCoreLib.JavaScript.DOM.INode' does not implement interface member 'ScriptCoreLib.JavaScript.Extensions.INodeConvertible<ScriptCoreLib.JavaScript.DOM.INode>.ToNode()'	X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\INode.cs	14	15	ScriptCoreLib
        // circular ref?
        INodeConvertible<IHTMLElement>
    {
        // see also
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLElementGrouping.cs


        #region async
        [Script]
        public new class Tasks<TElement> where TElement : IHTMLElement
        {
            // X:\jsc.svn\core\ScriptCoreLib\ActionScript\Extensions\flash\display\InteractiveObject.cs

            internal TElement that;



            // Z:\jsc.svn\examples\javascript\audio\DropAudioFile\Application.cs
            public Task<File> ondropfile
            {
                get
                {
                    var x = new TaskCompletionSource<File>();

                    Native.document.documentElement.ondragover += ee =>
                    {
                        if (x == null)
                            return;

                        //if (ee.dataTransfer.files.length > 0)
                        {
                            ee.stopPropagation();
                            ee.preventDefault();
                            ee.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.
                        }

                        // await leave? unless ondrop?
                    };

                    Native.document.documentElement.ondrop += e =>
                    {
                        if (x == null)
                            return;

                        if (e.dataTransfer.files.length > 0)
                        {
                            e.stopPropagation();
                            e.preventDefault();

                            x.SetResult(e.dataTransfer.files[0]);

                            x = null;
                        }
                    };

                    return x.Task;
                }
            }


            // Z:\jsc.svn\examples\javascript\Test\TestDropText\Application.cs
            public Task<string> ondroptext
            {
                get
                {
                    var x = new TaskCompletionSource<string>();

                    Native.document.documentElement.ondragover += ee =>
                    {
                        if (x == null)
                            return;


                        ee.stopPropagation();
                        ee.preventDefault();
                        ee.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                        // await leave? unless ondrop?
                    };

                    Native.document.documentElement.ondrop += e =>
                    {
                        if (x == null)
                            return;

                        e.stopPropagation();
                        e.preventDefault();

                        x.SetResult(e.text);

                        x = null;
                    };

                    return x.Task;
                }
            }


            // Z:\jsc.svn\examples\javascript\io\DropLESTToDisplay\DropLESTToDisplay\Application.cs
            public Task<DragEvent> ondrop
            {
                get
                {
                    var x = new TaskCompletionSource<DragEvent>();

                    Native.document.documentElement.ondragover += ee =>
                    {
                        if (x == null)
                            return;


                        ee.stopPropagation();
                        ee.preventDefault();
                        ee.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.

                        // await leave? unless ondrop?
                    };

                    Native.document.documentElement.ondrop += e =>
                    {
                        if (x == null)
                            return;

                        e.stopPropagation();
                        e.preventDefault();

                        x.SetResult(e);

                        x = null;
                    };

                    return x.Task;
                }
            }

            public virtual Task<IEvent> onmutation
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // X:\jsc.svn\examples\javascript\xml\FindByClassAndObserve\FindByClassAndObserve\Application.cs
                    // X:\jsc.svn\examples\javascript\svg\SVGFromHTMLDivObservable\SVGFromHTMLDivObservable\Application.cs

                    var x = new TaskCompletionSource<IEvent>();
                    //that.onmouseover += x.SetResult;

                    new MutationObserver(
                                new MutationCallback(
                                    (MutationRecord[] mutations, MutationObserver observer) =>
                                    {
                                        //Console.WriteLine("Mutations len " + mutations.Length);


                                        // mutationevent?
                                        //x.SetResult(null);


                                        // non null will yield to await
                                        x.SetResult((IEvent)new object());

                                        observer.disconnect();
                                    }
                                )
                            ).observe(that,
                                new
                                {
                                    // Set to true if mutations to target's children are to be observed.
                                    childList = true,
                                    // Set to true if mutations to target's attributes are to be observed. Can be omitted if attributeOldValue and/or attributeFilter is specified.
                                    attributes = true,
                                    // Set to true if mutations to target's data are to be observed. Can be omitted if characterDataOldValue is specified.
                                    characterData = true,
                                    // Set to true if mutations to not just target, but also target's descendants are to be observed.
                                    subtree = true,
                                }
                            );

                    return x.Task;
                }
            }

            #region onclick
            [System.Obsolete("should jsc expose events as async tasks until C# chooses to allow that?")]
            public virtual Task<IEvent> onclick
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();

                    // tested by
                    // X:\jsc.svn\examples\javascript\android\TextToSpeechExperiment\TextToSpeechExperiment\Application.cs
                    that.onclick +=
                        e =>
                        {
                            x.SetResult(e);
                        };

                    // tested by?
                    ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.InternalTaskNameLookup[x.Task] = "onclick";

                    return x.Task;
                }
            }
            #endregion

            // while this is a script library, could we mark some classes to be for merge?
            public virtual Task<IEvent> ondblclick
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();

                    // tested by
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowUDPPointerLock\ChromeAppWindowUDPPointerLock\Application.cs
                    that.ondblclick +=
                        e =>
                        {
                            x.SetResult(e);
                        };

                    // tested by?
                    ScriptCoreLib.JavaScript.DOM.CSSStyleRuleMonkier.InternalTaskNameLookup[x.Task] = "ondblclick";

                    return x.Task;
                }
            }


            public virtual Task<IEvent> onchange
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeHTMLTextToGLSLBytes\ChromeHTMLTextToGLSLBytes\Application.cs

                    var x = new TaskCompletionSource<IEvent>();
                    that.onchange += x.SetResult;
                    return x.Task;
                }
            }



            // X:\jsc.svn\examples\javascript\async\Test\TestAsyncMouseOver\TestAsyncMouseOver\Application.cs
            public virtual Task<IEvent> onmouseover
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmouseover += x.SetResult;
                    return x.Task;
                }
            }

            public virtual Task<IEvent> onmouseout
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmouseout += x.SetResult;
                    return x.Task;
                }
            }

            public virtual Task<IEvent> onmousedown
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmousedown += x.SetResult;
                    return x.Task;
                }
            }


            public virtual Task<IEvent> onmousemove
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowUDPPointerLock\ChromeAppWindowUDPPointerLock\Application.cs

                    var x = new TaskCompletionSource<IEvent>();
                    that.onmousemove += x.SetResult;
                    return x.Task;
                }
            }



            // there can be only one UI mouse capture 
            static Action oncapturedmousemove_release;

            // Z:\jsc.svn\examples\javascript\async\Test\TestMouseCaptureWhileMove\Application.cs
            public virtual Task<IEvent> oncapturedmousemove
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeAppWindowUDPPointerLock\ChromeAppWindowUDPPointerLock\Application.cs

                    if (oncapturedmousemove_release == null)
                    {
                        oncapturedmousemove_release = that.CaptureMouse();
                    }

                    var xx = new TaskCompletionSource<IEvent>();

                    this.onmouseup.ContinueWith(
                        x =>
                        {
                            // fires twice?
                            if (oncapturedmousemove_release == null)
                                return;

                            x.Result.preventDefault();
                            x.Result.stopPropagation();


                            oncapturedmousemove_release();
                            oncapturedmousemove_release = null;

                            // yield null
                        }
                    );


                    oncapturedmousemove_release += delegate
                    {
                        if (xx == null)
                            return;

                        xx.SetResult(null);
                        xx = null;
                    };

                    this.onmousemove.ContinueWith(
                        x =>
                        {
                            if (xx == null)
                                return;

                            // yield value
                            x.Result.preventDefault();
                            x.Result.stopPropagation();

                            xx.SetResult(x.Result);
                            xx = null;
                        }
                    );

                    return xx.Task;
                }
            }


            public virtual Task<IEvent> onmouseup
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onmouseup += x.SetResult;
                    return x.Task;
                }
            }

            public virtual Task<IEvent> onkeyup
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    var x = new TaskCompletionSource<IEvent>();
                    that.onkeyup += x.SetResult;
                    return x.Task;
                }
            }




            #region onscrollToBottom
            [Obsolete("how to name this?")]
            public Task<IEvent> onscrollToBottom
            {
                [Script(DefineAsStatic = true)]
                get
                {
                    // X:\jsc.svn\examples\javascript\android\com.abstractatech.dcimgalleryapp\com.abstractatech.dcimgalleryapp\Application.cs
                    // X:\jsc.svn\examples\javascript\UIAutomationEvents\UIAutomationEvents\Application.cs

                    var x = new TaskCompletionSource<IEvent>();

                    that.onscroll +=
                          e =>
                          {
                              if (x == null)
                                  return;



                              if (that.scrollHeight - 1 <= that.clientHeight + that.scrollTop)
                              {
                                  x.SetResult(e);
                                  x = null;
                              }
                          };

                    return x.Task;
                }
            }
            #endregion
        }


        // https://visualstudio.uservoice.com/forums/121579-visual-studio-2015/suggestions/5342192-awaitable-events
        //[System.Obsolete("is this the best way to expose events as async?")]
        public new Tasks<IHTMLElement> async
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return new Tasks<IHTMLElement> { that = this };
            }
        }
        #endregion
    }
}
