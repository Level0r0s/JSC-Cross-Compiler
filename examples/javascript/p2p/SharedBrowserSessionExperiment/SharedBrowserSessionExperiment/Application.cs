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
using SharedBrowserSessionExperiment;
using SharedBrowserSessionExperiment.Design;
using SharedBrowserSessionExperiment.HTML.Pages;
using SharedBrowserSessionExperiment.DataLayer.Data;
using System.Data;
using ScriptCoreLib.Lambda;
using System.Windows.Forms;

namespace SharedBrowserSessionExperiment
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
            // X:\jsc.svn\examples\javascript\forms\Test\TestDynamicBindingSourceForDataTable\TestDynamicBindingSourceForDataTable\Application.cs

            //NavigationOrdersNavigateBindingSource.CreateDataSource.With(
            //    CreateDataSource =>
            //    {
            //        NavigationOrdersNavigateBindingSource.CreateDataSource =
            //             delegate
            //             {
            //                 Console.WriteLine("Application NavigationOrdersNavigateBindingSource CreateDataSource");

            //                 return (CreateDataSource() as DataTable).With(
            //                     async asDataTable =>
            //                     {
            //                         while (true)
            //                         {

            //                             // send all rows that do not have keys to the server for rubber stamping.

            //                             // Error	5	The type arguments for method 'System.Linq.Enumerable.Select<TSource,TResult>(System.Collections.Generic.IEnumerable<TSource>, System.Func<TSource,TResult>)' cannot be inferred from the usage. Try specifying the type arguments explicitly.	X:\jsc.svn\examples\javascript\p2p\SharedBrowserSessionExperiment\SharedBrowserSessionExperiment\Application.cs	56	65	SharedBrowserSessionExperiment


            //                             base.RowsWithoutKeys = asDataTable.Rows.AsEnumerable().Select(
            //                                 (DataRow zr) =>
            //                                 {
            //                                     NavigationOrdersNavigateRow rr = zr;

            //                                     // ?

            //                                     return rr;
            //                                 }
            //                             ).ToArray();



            //                             await this.BindingSourceSynchonization();

            //                             await base.Retry;

            //                         }
            //                     }
            //                 );
            //             };
            //    }
            //);


            // does this work on nexus?
            // and also jsc store?


            // is this the first step to explore
            // js p2p
            // first step to allow youtube to be inspected
            // and recorder from android?

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140412

            // by installing chrome styler
            // should we have the means to autostart?
            #region auto link : Abstractatech.JavaScript.Forms.ChromeStyler

            // otherwise we have to know what we type?
            // what about reusing the IComponent model to have access
            // and communications with the auto linked types and services?

            // we have to be .4.5!
            // !!! haha. we have two different styles in use here:
            //FormStyler.AtFormCreated += System.Windows.Forms.FormStylerLikeChrome.LikeChrome;

            FormStyler.AtFormCreated = System.Windows.Forms.FormStylerLikeChrome.LikeChrome;

            // what about ordering of such auto linkers?
            // this will be a security issue, as no manual inspection
            // can be done on those linked nugets.
            // jsc needs to start scanning third party code
            // even if it only impacts client side code.
            // next we will also want it to have impact on the server!

            // shall jsc ask for consent on the first compile?
            // or shall it ask consent as the license you install the nuget?
            #endregion

            //new TheBrowserTab().With(
            new PositionsWatchdog().With(
                async w =>
                {
                    // um do xlsx
                    // already support
                    // urlString as uri

                    // when will C# support 

                    // Refused to display 'https://www.youtube.com/watch?v=Jr-yMOJRTo4' in a frame because it set 'X-Frame-Options' to 'SAMEORIGIN'.

                    //<iframe width="420" height="315" src="//www.youtube.com/embed/Jr-yMOJRTo4" frameborder="0" allowfullscreen></iframe>

                    //tab.webBrowser1.Navigate(
                    //    //"https://www.youtube.com/watch?v=Jr-yMOJRTo4"
                    //    "https://www.youtube.com/embed/Jr-yMOJRTo4"
                    //);

                    //tab.Show();

                    w.Opacity = 0.5;

                    w.StartPosition = FormStartPosition.Manual;
                    w.Left = 0;

                    await w.ShowAsync();

                    // reload ?

                    Native.document.location.reload();
                }
               );


        }

    }
}