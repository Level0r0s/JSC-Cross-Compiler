

using System;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestScriptIfIfReturn
{
    public class TaskCompletionSource<T>
    {
        static TaskCompletionSource<object> vsync0ambient;
        static TaskCompletionSource<object> vsync1renderman;


        static void nop() { }



        public bool IsCompleted { get { return true; } }
        public TaskCompletionSource<T> Task;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public TaskCompletionSource(object page)
        {
            // Z:\jsc.svn\examples\javascript\test\TestDelegateIfIfReturn\Application.cs

            // let render man know..

            var flag1 = vsync1renderman != null;
            if (flag1)
            // this if block is not detected?
            {
                // whats going on  with if clauses?
                nop();

                // wtf???
                var flag0 = vsync1renderman.Task.IsCompleted;
                if (flag0)
                    return;
            }
            if (vsync0ambient != null)
                if (vsync0ambient.Task.IsCompleted)
                    return;

            WriteLine("Native.window.onframe ");

            return;
        }

        private void WriteLine(string v)
        {
        }
    }
}

// its the rewriter thats gona mess it up for roslyn??

//type$kVoWvfl4zj2OZvNhNEsPvw.AwAABvl4zj2OZvNhNEsPvw = function(b)
//{
//    this.constructor.apply(this);
//    var a = [this], c, d, e, f, g, h;

//    c = AgAABPl4zj2OZvNhNEsPvw != null;
//    d = c;

//    if (d)
//    {
//        AQAABvl4zj2OZvNhNEsPvw();
//        e = AgAABPl4zj2OZvNhNEsPvw.Task.AgAABvl4zj2OZvNhNEsPvw();
//        f = e;

//        if (f)
//        {
//            return;
//        }

//    }

//    g = AQAABPl4zj2OZvNhNEsPvw != null;

//    if (g)
//    {
//        h = AQAABPl4zj2OZvNhNEsPvw.Task.AgAABvl4zj2OZvNhNEsPvw();

//        if (h)
//        {
//            return;
//        }

//    }

//    a[0].BAAABvl4zj2OZvNhNEsPvw('Native.window.onframe ');
//};