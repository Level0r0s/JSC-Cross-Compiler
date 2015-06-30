using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

namespace TestThreadManager
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// The static content defined in the HTML file will be update to the dynamic content once application is running.
        /// </summary>
        public XElement Header = new XElement(@"h1", @"JSC - The .NET crosscompiler for web platforms. ready.");

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            // https://cloud.google.com/appengine/docs/java/javadoc/com/google/appengine/api/ThreadManager#currentRequestThreadFactory()

            // X:\jsc.svn\core\ScriptCoreLibJava.AppEngine\ScriptCoreLibJava.AppEngine\com\google\appengine\api\ThreadManager.cs

            // current version does not show it?
            Console.WriteLine("before ThreadManager.createThreadForCurrentRequest " + new { Thread.CurrentThread.ManagedThreadId });
            // before ThreadManager.createThreadForCurrentRequest { ManagedThreadId = 6 }


            //before ThreadManager.createThreadForCurrentRequest { { ManagedThreadId = 14 } }
            //at ThreadManager.createThreadForCurrentRequest { { ManagedThreadId = 17 } }
            //after ThreadManager.createThreadForCurrentRequest { { ManagedThreadId = 14 } }

            // X:\jsc.svn\core\ScriptCoreLibJava.AppEngine\ScriptCoreLibJava.AppEngine\Extensions\ThreadManagerExtensions.cs
            // 

            var t = com.google.appengine.api.ThreadManager.createThreadForCurrentRequest(new yy
            {

                y = delegate
                {
                    Console.WriteLine("at ThreadManager.createThreadForCurrentRequest " + new { Thread.CurrentThread.ManagedThreadId });

                }
            }
            );

            // before ThreadManager.createThreadForCurrentRequest {{ ManagedThreadId = 10 }}
            //t.start();
            t.start();

            new Thread(
                delegate ()
            {
                Console.WriteLine("at new Thread " + new { Thread.CurrentThread.ManagedThreadId });
            }
            ).Start();

            Thread.Sleep(500);
            Console.WriteLine("after ThreadManager.createThreadForCurrentRequest " + new { Thread.CurrentThread.ManagedThreadId });

            // Send it back to the caller.
            y(e);
        }

        class yy : java.lang.Runnable
        {
            public Action y;

            public void run()
            {
                y();
            }
        }


    }
}


//X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe  -encoding UTF-8 -cp Y:\TestThreadManager.ApplicationWebService\staging.java\web\release;x:\util\appengine-java-sdk-1.9.15\lib\impl\*;x:\util\appengine-java-sdk-1.9.15\lib\shared\* -d "Y:\TestThreadManager.ApplicationWebService\staging.java\web\release" @"Y:\TestThreadManager.ApplicationWebService\staging.java\web\files"

//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Web\__HttpRequest.java:5: error: package javax.servlet.http does not exist
//import javax.servlet.http.Cookie;
//                         ^
//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Web\__HttpRequest.java:6: error: package javax.servlet.http does not exist


//X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe  -encoding UTF-8 -cp Y:\TestThreadManager.ApplicationWebService\staging.java\web\release;x:\util\appengine-java-sdk-1.9.22\lib\impl\*;x:\util\appengine-java-sdk-1.9.22\lib\shared\* -d "Y:\TestThreadManager.ApplicationWebService\staging.java\web\release" @"Y:\TestThreadManager.ApplicationWebService\staging.java\web\files"

//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Extensions\LinqExtensions.java:63: error: incompatible types
//                    if ((enumerator_13))
//                         ^
//  required: boolean
//  found:    __IEnumerator_1<T>
//  where T is a type-variable:
//    T extends Object declared in method <T>WithEachIndex(__IEnumerable_1<T>,__Action_2<T,Integer>)

//0001 02000042 TestThreadManager.ApplicationWebService::<module>.SHA14cdcc3a4041f43cf15b17f195067126f523efbb1@1634151512
//Y:\TestThreadManager.ApplicationWebService\staging.java\web\files
//X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe  -encoding UTF-8 -cp Y:\TestThreadManager.ApplicationWebService\staging.java\web\release;x:\util\appengine-java-sdk-1.9.22\lib\impl

//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Ultra\WebService\InternalGlobalExtensions___c__DisplayClass0_0.java:96: error: incompatible types
//        for (num3 = 0; num3; num3++)
//                       ^
//  required: boolean


//X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe  -encoding UTF-8 -cp Y:\TestThreadManager.ApplicationWebService\staging.java\web\release;x:\util\appengine-java-sdk-1.9.2

//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Data\SQLite\__MySQLCommand.java:454: error: incompatible types
//        return ((((Object)_017e) instanceof  Integer) ? (Integer)((Object)_017e) : (Integer)null);
//                                                      ^
//  required: __Int32
//  found:    Integer


//0001 02000042 TestThreadManager.ApplicationWebService::<module>.SHA14cdcc3a4041f43cf15b17f195067126f523efbb1@161959109
//Y:\TestThreadManager.ApplicationWebService\staging.java\web\files
//X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe  -encoding UTF-8 -cp Y:\TestThreadManager.ApplicationWebService\staging.java\web\release;x:\util\appengine-java-sdk-1.9.22\lib\impl\*;x:\util\appengine-java-sdk-1.9

//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\__TaskExtensions.java:34: error: cannot find symbol
//        task.ContinueWith_0600018f(new __Action_1<__Task_1<__Task_1<TResult>>>(class2_10,
//            ^
//  symbol:   method ContinueWith_0600018f(__Action_1<__Task_1<__Task_1<TResult>>>)
//  location: variable task of type __Task_1<__Task_1<TResult>>
//  where TResult is a type-variable:
//    TResult extends Object declared in method <TResult>Unwrap_06000b5a(__Task_1<__Task_1<TResult>>,String)
//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\__TaskExtensions.java:59: error: cannot find symbol
//        task.ContinueWith_0600018f(new __Action_1<__Task_1<__Task>>(class60,
//            ^
//  symbol:   method ContinueWith_0600018f(__Action_1<__Task_1<__Task>>)
//  location: variable task of type __Task_1<__Task>
//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Threading\Tasks\__TaskExtensions___c__DisplayClass2_1.java:38: error: cannot find symbol
//        task_10.ContinueWith_0600018f(new __Action_1<__Task_1<TResult>>(this,
//               ^
//  symbol:   method ContinueWith_0600018f(__Action_1<__Task_1<TResult>>)
//  location: variable task_10 of type __Task_1<TResult>
//  where TResult is a type-variable:
//    TResult extends Object declared in class __TaskExtensions___c__DisplayClass2_1
//Note: Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\__Thread.java uses or overrides a deprecated API.


//0001 02000042 TestThreadManager.ApplicationWebService::<module>.SHA14cdcc3a4041f43cf15b17f195067126f523efbb1@1498165865
//Y:\TestThreadManager.ApplicationWebService\staging.java\web\files
//X:\Program Files (x86)\Java\jdk1.7.0_79\bin\javac.exe  -encoding UTF-8 -cp Y:\TestThreadManager.ApplicationWebService\staging.java\web\release;x:\util\appengine-java-sdk-1.9

//Y:\TestThreadManager.ApplicationWebService\staging.java\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:286: error: cannot find symbol
//        return  __TaskExtensions.<TResult>Unwrap_06000b59(__Task.get_InternalFactory().<__Task_1<TResult>>StartNew(function));
//                                ^
//  symbol:   method <TResult>Unwrap_06000b59(__Task_1<__Task_1<TResult>>)
//  location: class __TaskExtensions
//  where TResult is a type-variable:
//    TResult extends Object declared in method <TResult>Run(__Func_1<__Task_1<TResult>>)
