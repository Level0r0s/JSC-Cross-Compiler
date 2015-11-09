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
using DropLESTToDisplay;
using DropLESTToDisplay.Design;
using DropLESTToDisplay.HTML.Pages;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TestSwitchToServiceContextAsync;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using DropLESTToDisplay.HTML.Images.FromAssets;

namespace DropLESTToDisplay
{
    #region HopToThreadPoolAwaitable
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // simple awaitable that allows for hopping to the thread pool
    //struct HopToThreadPoolAwaitable : System.Runtime.CompilerServices.INotifyCompletion
    struct HopToWorker : System.Runtime.CompilerServices.INotifyCompletion
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201504/20150401
        // X:\jsc.svn\examples\javascript\Test\TestHopToThreadPoolAwaitable\TestHopToThreadPoolAwaitable\Application.cs

        public HopToWorker GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }
        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }
        //public void OnCompleted(Action continuation) { Task.Run(continuation); }
        public void GetResult() { }
    }
    #endregion

    #region HopToUIAwaitable
    // Z:\jsc.svn\examples\javascript\async\AsyncHopToUIFromWorker\AsyncHopToUIFromWorker\Application.cs
    //public struct HopToUIAwaitable : System.Runtime.CompilerServices.INotifyCompletion
    public struct HopToUI : System.Runtime.CompilerServices.INotifyCompletion
    {
        // basically we have to hibernate the current state to resume
        public HopToUI GetAwaiter() { return this; }
        public bool IsCompleted { get { return false; } }

        public static Action<Action> VirtualOnCompleted;
        public void OnCompleted(Action continuation) { VirtualOnCompleted(continuation); }

        public void GetResult() { }
    }
    #endregion

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        static Func<string, string> DecoratedString = x => x.Replace("-", "_").Replace("+", "_").Replace("<", "_").Replace(">", "_");


        static Application()
        {
            #region document
            if (Native.document != null)
            {
                // patch the awaiter..
                //Console.SetOut(new xConsole());


                // also. all workers we will be creating will need to start to expect switch commands...
                // how can we tap in them?

                //__worker_onfirstmessage: {{ 
                // ManagedThreadId = 10, href = https://192.168.1.196:13946/view-source#worker, 
                // MethodTargetTypeIndex = type$PgZysaxv_bTu4GEkwmJdJrQ, 
                // MethodTargetType = ___ctor_b__1_5_d, 
                // MethodToken = jwsABpdwBjGQu09dvBXjxw, 
                // MethodType = FuncOfObjectToObject, 
                // stateTypeHandleIndex = null, 
                // stateType = null, 
                // state = [object Object], 
                // IsIProgress = false }}
                // 

                // can we start a task, yet also have special access for the posted messages?

                // what about threads that did not hop to background?
                // dont they want to hop to background?
                HopToWorker.VirtualOnCompleted = continuation =>
                {
                    Action<ShadowIAsyncStateMachine> MoveNext0 = null;

                    // async dont like ref?
                    var shadowstate = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext0);
                    var MoveNext = MoveNext0;

                    Console.WriteLine("enter HopToWorker " + new { shadowstate.state });

                    // post a message to the document 
                    var xx = new __Task.InternalTaskExtensionsScope { InternalTaskExtensionsScope_function = continuation };


                    var x = new __Task<object>();

                    // reusing thread init for generic task start, although, resuming needs special implementation..
                    x.InternalInitializeInlineWorker(
                        new Action(xx.f),
                        //action,
                        default(object),
                        default(CancellationToken),
                        default(TaskCreationOptions),
                        TaskScheduler.Default,

                        yield: (worker, e) =>
                        {
                            // like operator for JSON?

                            //Console.WriteLine("enter HopToThreadPoolAwaitable InternalInitializeInlineWorker yield");

                            var data = new
                            {
                                vSetTitle = default(string),

                                HopToUIAwaitable = new
                                {
                                    // state to hop back

                                    shadowstate = default(ShadowIAsyncStateMachine)
                                }
                            };

                            data = (dynamic)e.data;

                            //if (data.HopToUIAwaitable )

                            if (data.vSetTitle != null)
                            {
                                Console.Title = data.vSetTitle;
                            }

                            if (data.HopToUIAwaitable == null)
                                return;


                            // time to hop back on continuation?

                            var that = (TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine)data.HopToUIAwaitable.shadowstate;


                            //Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("work about to hop into ui " + new { that.state });
                            //Console.ForegroundColor = ConsoleColor.Black;


                            //enter HopToThreadPoolAwaitable yield HopToUIAwaitable
                            //worker Task Run function has returned {{ value_Task = null, value_TaskOfT = null }}
                            //__Task.InternalStart inner complete {{ yield = {{ value = null }} }}

                            // the worker should be in a suspended state, as we may want to jump back?

                            // 
                            //MoveNext(data.HopToUIAwaitable.shadowstate);



                            #region xAsyncStateMachineType
                            var xAsyncStateMachineType = typeof(Application).Assembly.GetTypes().FirstOrDefault(
                                  item =>
                                  {
                                      // safety check 1

                                      //Console.WriteLine(new { sw.ElapsedMilliseconds, item.FullName });

                                      var xisIAsyncStateMachine = typeof(IAsyncStateMachine).IsAssignableFrom(item);
                                      if (xisIAsyncStateMachine)
                                      {
                                          //Console.WriteLine(new { item.FullName, isIAsyncStateMachine });

                                          return item.FullName == that.TypeName;
                                      }

                                      return false;
                                  }
                              );
                            #endregion


                            var NewStateMachine = FormatterServices.GetUninitializedObject(xAsyncStateMachineType);
                            var isIAsyncStateMachine = NewStateMachine is IAsyncStateMachine;

                            var NewStateMachineI = (IAsyncStateMachine)NewStateMachine;

                            #region 1__state
                            xAsyncStateMachineType.GetFields(
                                        System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                                    ).WithEach(
                                     AsyncStateMachineSourceField =>
                                     {

                                         Console.WriteLine(new { AsyncStateMachineSourceField });

                                         if (AsyncStateMachineSourceField.Name.EndsWith("1__state"))
                                         {
                                             AsyncStateMachineSourceField.SetValue(
                                                 NewStateMachineI,
                                                 that.state
                                              );
                                         }

                                         var xStringField = that.StringFields.AsEnumerable().FirstOrDefault(
                                             f => DecoratedString(f.FieldName) == DecoratedString(AsyncStateMachineSourceField.Name)
                                         );

                                         if (xStringField != null)
                                         {
                                             // once we are to go back to client. we need to reverse it?

                                             AsyncStateMachineSourceField.SetValue(
                                                 NewStateMachineI,
                                                 xStringField.value
                                              );
                                             // next xml?
                                             // before lets send our strings back with the new state!
                                             // what about exceptions?
                                         }
                                     }
                                );
                            #endregion

                            //new IHTMLPre {
                            //            "inside iframe invoke state"
                            //        }.AttachToDocument();

                            NewStateMachineI.MoveNext();

                        }
                    );


                    x.Start();


                };

                return;
            }
            #endregion


            #region worker
            if (Native.worker != null)
            {
                Console.WriteLine("about to enable HopToUI...");


                Native.worker.onfirstmessage += e =>
                {
                    Console.WriteLine("enter onfirstmessage");

                    ScriptCoreLib.JavaScript.BCLImplementation.System.__Console.vSetTitle = value =>
                    {
                        e.postMessage(
                          new
                          {
                              vSetTitle = value
                          }
                        );
                    };


                    HopToUI.VirtualOnCompleted = continuation =>
                    {
                        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201511/20151102/hoptoui

                        // first jump out?
                        InternalInlineWorker.InternalOverrideTaskOfT = new TaskCompletionSource<object>().Task;

                        // post a message to the document 

                        // um. how can we signal that we are not done?


                        //Action<ShadowIAsyncStateMachine> MoveNext = null;

                        // async dont like ref?
                        //var shadowstate = ShadowIAsyncStateMachine.FromContinuation(continuation, ref MoveNext);
                        var r = TestSwitchToServiceContextAsync.ShadowIAsyncStateMachine.ResumeableFromContinuation(continuation);

                        Console.WriteLine("enter HopToUI, postMessage " + new { r.shadowstate.state });


                        // postMessageAsync ? if ui wants to return, instead of restaring this thread?
                        e.postMessage(
                            new
                            {
                                HopToUIAwaitable = new
                                {
                                    // state to hop back

                                    r.shadowstate
                                }
                            }
                        );

                    };
                };


                return;
            }
            #endregion

        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // user gets to drop a csv
            // with X and Y fields.
            // where is our csv parser? assetslibary?

            // show title?


            #region test
            new { }.With(
                async delegate
                {
                    { fixup: await Task.CompletedTask; }

                    var bytes = new byte[] { 0xcc };

                    await new IHTMLButton { "test worker " + new { Thread.CurrentThread.ManagedThreadId, bytes = bytes.ToHexString() } }.AttachToDocument().async.onclick;

                    await default(HopToWorker);

                    var output = "hello " + new { Thread.CurrentThread.ManagedThreadId, bytes = bytes.ToHexString() };

                    Console.Title = "progress!";

                    await Task.Delay(500);

                    new { }.With(
                        async delegate
                        {
                            { fixup: await Task.CompletedTask; }

                            // is this supposed to work?
                            var output2 = "hello " + new { Thread.CurrentThread.ManagedThreadId };
                            await default(HopToUI);

                            new IHTMLPre { "progress... " + new { Thread.CurrentThread.ManagedThreadId, output2 } }.AttachToDocument();
                        }
                    );



                    await Task.Delay(500);



                    await default(HopToUI);

                    new IHTMLPre { "ready " + new { Thread.CurrentThread.ManagedThreadId, output } }.AttachToDocument();

                    // ready { ManagedThreadId = 1, output = hello { ManagedThreadId = 10 } }
                }
            );
            #endregion


            Native.css[IHTMLElement.HTMLElementEnum.head].style.display = IStyle.DisplayEnum.block;
            Native.css[IHTMLElement.HTMLElementEnum.title].style.display = IStyle.DisplayEnum.block;

            new { }.With(
                async delegate
                {
                    { fixup: await Task.CompletedTask; }

                    Native.document.documentElement.css.hover.style.backgroundColor = "pink";
                    //Native.document.documentElement.css.dragover
                    // while await ondrop ?
                    Native.document.documentElement.ondragover += ee =>
                    {
                        Native.document.documentElement.style.backgroundColor = "cyan";
                        // await leave? unless ondrop?
                    };

                    Native.document.documentElement.ondragleave += delegate
                    {
                        Native.document.documentElement.style.backgroundColor = "";
                    };

                    new IHTMLPre { "drop a file" }.AttachToDocument();

                    //Native.document.documentElement.ondrop += e =>
                    //var e = await Native.document.documentElement.async.ondrop;
                    var f = await Native.document.documentElement.async.ondropfile;

                    Native.document.documentElement.style.backgroundColor = "yellow";

                    new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, f.name, f.size } }.AttachToDocument();
                    // { name = download.csv, size = 20851425 }

                    var sw = Stopwatch.StartNew();
                    // if the drop is from a network share it wil take a while..

                    //{ ManagedThreadId = 1, name = data.csv, size = 110777754 }
                    //{ ManagedThreadId = 1, ElapsedMilliseconds = 43418 }

                    var bytes = await f.readAsBytes();

                    new IHTMLPre { new { Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds } }.AttachToDocument();

                    // nuget google
                    await google.maps.api;

                    #region map
                    var div = new IHTMLDiv
                    {
                    }.AttachToDocument();

                    div.style.border = "1px dashed red";
                    div.style.height = "300px";
                    div.style.left = "0px";
                    div.style.right = "0px";



                    // https://developers.google.com/maps/documentation/javascript/reference?csw=1#MapOptions
                    map = new google.maps.Map(div,
                        new
                        {
                            disableDefaultUI = true,

                            center = new { lat = 59.4329527, lng = 24.7023564 },
                            zoom = 6.0
                        }
                    );


                    new IHTMLPre { () => new { zoom = map.getZoom() } }.AttachToDocument();


                    var marker0 = new google.maps.Marker(
                         new
                         {
                             position = new
                             {
                                 lat = 59.4329527,
                                 lng = 24.7023564
                             },
                             //label = "T",
                             //title = "Tallinn",
                             map
                         }
                      );

                    #endregion


                    await new IHTMLButton { "go" }.AttachToDocument().async.onclick;
                    var bytes1 = bytes;
                    await default(HopToWorker);

                    // start the static line reader.

                    WorkerReader = new StreamReader(new MemoryStream(bytes1));

                    // now before we jump back to ui. lets start reading the lines...
                    // working... { ManagedThreadId = 1, output = hello { ManagedThreadId = 11, Length = 20851425, WorkerReaderLineCount = 0 } }

                    new { }.With(
                        async delegate
                        {
                            { fixup: await Task.CompletedTask; }

                            var output = "hello " + new { Thread.CurrentThread.ManagedThreadId };
                            await default(HopToUI);
                            WorkerUI = new IHTMLPre { "working... " + new { Thread.CurrentThread.ManagedThreadId, output } }.AttachToDocument();

                            // cant jump back can we?
                        }
                    );

                    var header = WorkerReader.ReadLine();

                    #region rows
                    // Jkn;Kohanimi;Keel;Kohanime staatus;Kohanime olek;Nimeobjekti liik;Lisainfo;Maakond,omavalitsus,asustusüksus;X;Y; 

                    //EIC;X;Y;ID;IK3MAX A;POSTOFFICE;ADDRESS
                    //00117987-Y;6590203.66;534216.50;6250743;2916.98;Tallinn Harju maakond;Tallinn, Nooda tee, 7

                    //var headerX = header.Split(';').TakeWhile(x => x != "X").Count();
                    //var headerY = header.Split(';').TakeWhile(x => x != "Y").Count();

                    var line1 = WorkerReader.ReadLine();

                    var sw1 = Stopwatch.StartNew();

                    WorkerReaderLineCount = 0;

                    var HasLine = !string.IsNullOrEmpty(line1);

                    // URI malformed at decodeURIComponent

                    while (HasLine)
                    {
                        WorkerReaderLineCount++;

                        //await Task.Delay(33);

                        //Console.WriteLine("520 " + new { WorkerReaderLineCount });

                        var x = new CSVHeaderLookup { header = header, line = line1 }["X"];
                        var y = new CSVHeaderLookup { header = header, line = line1 }["Y"];

                        //Console.WriteLine("525 " + new { WorkerReaderLineCount });

                        // hop supports strings for now..
                        var lat = "" + (double)LEST97.lest_function_vba.lest_geo(x, y, 0);
                        var lng = "" + (double)LEST97.lest_function_vba.lest_geo(x, y, 1);

                        //Console.WriteLine("531 " + new { WorkerReaderLineCount });

                        #region progress
                        //if (WorkerReaderLineCount % 500 == 1)
                        {
                            Console.Title = WorkerReaderLineCount + " in " + sw1.ElapsedMilliseconds + "ms " + new { WorkerReader.BaseStream.Position, WorkerReader.BaseStream.Length };
                        }


                        if (WorkerReaderLineCount % 300 == 1)
                        {
                            new { }.With(
                                async delegate
                                {
                                    { fixup: await Task.CompletedTask; }

                                    var lng1 = lng;
                                    var lat1 = lat;
                                    var title1 = line1;

                                    var output = "working... " + WorkerReaderLineCount + " in " + sw1.ElapsedMilliseconds + "ms " + new { x, y, lat, lng };
                                    await default(HopToUI);
                                    WorkerUI.innerText = output;

                                    Console.WriteLine(new { lng1, lat1 });

                                    var position = new
                                             {
                                                 // InvalidValueError: setPosition: not a LatLng or LatLngLiteral: in property lat: not a number

                                                 lat = Convert.ToDouble(lat1),
                                                 lng = Convert.ToDouble(lng1)
                                             };

                                    Console.WriteLine(new { position });

                                    // http://stackoverflow.com/questions/20044308/google-maps-api-3-show-hide-markers-depending-on-zoom-level
                                    // http://stackoverflow.com/questions/19304574/center-set-zoom-of-map-to-cover-all-markers-visible-markers

                                    var marker = new google.maps.Marker(
                                         new
                                         {
                                             // https://developers.google.com/maps/documentation/javascript/examples/marker-symbol-predefined
                                             // https://developers.google.com/maps/documentation/javascript/markers
                                             //icon = new marker().src,

                                             // can we have two zoom levels?
                                             icon = new markersmall().src,

                                             position,
                                             //label = "T",

                                             title = title1,

                                             map
                                         }
                                      );

                                    // http://stackoverflow.com/questions/8198635/change-marker-icon-on-mouseover-google-maps-v3
                                    AddZoomAwareMarker(map, marker, new markersmall().src, new markerxsmall().src);


                                    while (await marker.async.onmouseover)
                                    {

                                        Console.Title = title1;
                                    }

                                }
                            );



                            //await Task.Delay(300);
                        }
                        #endregion

                        //Console.WriteLine("606 " + new { WorkerReaderLineCount });

                        //await Task.Delay(3);

                        line1 = WorkerReader.ReadLine();
                        HasLine = !string.IsNullOrEmpty(line1);

                        //Console.WriteLine("611 " + new { WorkerReaderLineCount });
                    }
                    #endregion

                    Console.WriteLine("done at " + new { WorkerReaderLineCount });

                    // done... { ManagedThreadId = 1, output = done { ManagedThreadId = 10, WorkerReaderLineCount = 153411, ElapsedMilliseconds = 99979 } }
                    // done... { ManagedThreadId = 1, output = done { ManagedThreadId = 10, WorkerReaderLineCount = 153411, ElapsedMilliseconds = 260865 } }
                    {

                        var output = "done " + new { Thread.CurrentThread.ManagedThreadId, WorkerReaderLineCount, sw1.ElapsedMilliseconds };
                        await default(HopToUI);
                        WorkerUI.innerText = "done... " + new { Thread.CurrentThread.ManagedThreadId, output };

                        // done... { ManagedThreadId = 1, output = done { ManagedThreadId = 10, WorkerReaderLineCount = 153411 } }
                    }




                }
            );

        }

        static async void AddZoomAwareMarker(google.maps.Map map, google.maps.Marker marker, string small, string xsmall)
        {

            do
            {
                var z = map.getZoom();

                //if (z < 10.0)
                if (z < 13.0)
                    marker.setIcon(xsmall);
                else
                    marker.setIcon(small);


                await map.async.onzoomchanged;

            }
            while (true);

        }

        static google.maps.Map map;

        static IHTMLPre WorkerUI;
        static StreamReader WorkerReader;
        static int WorkerReaderLineCount;
    }

    class CSVHeaderLookup
    {
        public string header;
        public string line;

        public string this[string column]
        {
            get
            {
                var h = header.Split(';');

                if (!h.Contains(column))
                    return "";

                var headerX = h.TakeWhile(x => x != column).Count();


                return line.Split(';')[headerX];
            }
        }
    }
}
