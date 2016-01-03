using ScriptCoreLib;
using ScriptCoreLibAndroidNDK.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLibAndroidNDK.BCLImplementation.System.Runtime.CompilerServices
{
    // z:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
    // z:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
    // z:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
    // z:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs


    [Obsolete("can we move it into Shared just yet?")]
    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite))]
    internal class __CallSite
    {
        // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/15-dualvr/20160103/ndktype

        // Z:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Runtime\CompilerServices\CallSiteBinder.cs

        public CallSiteBinder Binder { get; set; }
    }

    [Script(Implements = typeof(global::System.Runtime.CompilerServices.CallSite<>))]
    internal class __CallSite<T> : __CallSite
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\CompilerServices\CallSite.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127



        // used by
        //     ScriptCoreLib_Shared_BCLImplementation_System___Action_2_Invoke((void*)OVROculus360PhotosNDK_xNativeActivity__future_o__SiteContainer4___p__Site5->Target, (LPScriptCoreLibAndroidNDK_BCLImplementation_System_Runtime_CompilerServices___CallSite)OVROculus360PhotosNDK_xNativeActivity__future_o__SiteContainer4___p__Site5, activity);
        public T Target;

        // used by
        //        infoArray0[0] = ScriptCoreLib_Shared_BCLImplementation_Microsoft_CSharp___CSharpArgumentInfo_Create((int)0, NULL);
        public static __CallSite<T> Create(CallSiteBinder binder)
        {
            ConsoleExtensions.trace("__CallSite Create");

            // Z:\jsc.svn\examples\java\android\synergy\OVROculus360PhotosNDK\OVROculus360PhotosNDK\xNativeActivity.cs


            return null;
        }
    }
}
