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
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs

    internal partial class __Task<TResult>
    {
        public __Task()
        { 
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201508/20150817
        // Z:\jsc.svn\examples\java\hybrid\async\Test\JVMCLRFactoryRun\JVMCLRFactoryRun\Program.cs

        public __Task(Func<TResult> function)
        {
            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\Task\Task.Run.cs

            // unstarted

            this.Status = TaskStatus.Created;

            this.vStart = delegate
            {
                this.Status = TaskStatus.Running;

                new Thread(
                    delegate()
                    {
                        // in java it we can keep our call refs.

                        var result = function();

                        this.SetResult(result);
                    }
                ).Start();
            };
        }
    }
}
