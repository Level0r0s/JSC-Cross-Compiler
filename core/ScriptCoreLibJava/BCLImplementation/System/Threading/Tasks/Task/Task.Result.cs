using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Runtime.CompilerServices;
using ScriptCoreLibJava.BCLImplementation.System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading.Tasks
{

    internal partial class __Task<TResult>
    {
        public TResult InternalResult;
        public TResult Result
        {
            get
            {

                // in js we cannot wait, throw instead?
                this.Wait();
                return this.InternalResult;
            }
            set { this.InternalResult = value; }
        }

        public void SetResult(TResult result)
        {
            //Console.WriteLine("enter __Task SetResult, WaitEvent.Set " + new { __Environment.CurrentManagedThreadId });


            // do we have to pay attention to threads?

            this.Result = result;
            this.Status = TaskStatus.RanToCompletion;
            this.IsCompleted = true;

            if (InvokeWhenCompleteLater != null)
            {
                InvokeWhenCompleteLater();
                InvokeWhenCompleteLater = null;
            }

            // does Wait event get raised after descendant actions are done or just started?

            //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task_1.java:121: error: cannot find symbol
            //        this.WaitEvent.Set();
            //                      ^
            //  symbol:   method Set()
            //  location: variable WaitEvent of type __AutoResetEvent

            this.WaitEvent.Set();
        }
    }
}
