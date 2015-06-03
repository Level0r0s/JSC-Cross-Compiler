using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[assembly: Script()]
//[assembly: ScriptCoreLib.Shared.ScriptResourcesAttribute("libs/armeabi-v7a")]
[assembly: ScriptCoreLib.Shared.ScriptResourcesAttribute("libs/armeabi_v7a")]

namespace TestNDKAsAsset
{
    // [armeabi-v7a] Install        : libTestNDKAsAsset.so => libs/armeabi-v7a/libTestNDKAsAsset.so
    // "X:\jsc.svn\examples\c\android\Test\TestNDKAsAsset\TestNDKAsAsset\bin\Debug\staging\libs\armeabi-v7a\libTestNDKAsAsset.so"

    public static partial class xActivity
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201505/20150525

        // ConfigurationCreateNuGetPackage 

        // Embedded Resource
        // libs\armeabi-v7a\libTestNDKAsAsset.so

        static xActivity()
        {
            // if we name the asset, will merge rewriter keep it around?
            //var lib = @"libs\armeabi-v7a\libTestNDKAsAsset.so";
            //var lib = @"libs/armeabi-v7a/libTestNDKAsAsset.so";
            var lib = @"libs/armeabi_v7a/libTestNDKAsAsset.so";

            // visual studio mangles the name?

            // 2015 RC for java
            //[javac] W:\src\__AnonymousTypes__AndroidNDKNugetExperiment_AndroidActivity\__f__AnonymousType_34__1_0_1.java:34: error: reference to Format is ambiguous, both method Format(String,Object,Object) in __String and method Format(__IFormatProvider,String,Object[]) in __String match
            //[javac]         return __String.Format(null, "{{ lib = {0} }}", objectArray2);
            //Console.WriteLine(new { lib });

            Console.WriteLine("lib: " + lib);
            Console.WriteLine("loadLibrary: TestNDKAsAsset");

            java.lang.System.loadLibrary("TestNDKAsAsset");
        }

        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static string stringFromJNI() { return default(string); }

        [Script(IsPInvoke = true)]
        //private long find(string lib, string fname) { return default(long); }
        public static long mmap(int fd,
            int length) { return default(long); }
        // "X:\jsc.svn\examples\java\android\AndroidNDKNugetExperiment\AndroidNDKNugetExperiment\bin\Debug\staging\clr\AndroidNDKNugetExperiment.AndroidActivity.dll"
    }
}

//at System.Collections.Immutable.ImmutableArray`1.Builder.get_Item(Int32 index)
//   at Microsoft.CodeAnalysis.CSharp.OverloadResolution.IsApplicable(Symbol candidate, EffectiveParameters parameters, AnalyzedArguments arguments, ImmutableArray`1 argsToParameters, Boolean isVararg, Boolean hasAnyRefOmittedArgument, Boolean ignoreOpenTypes, HashSet`1& useSiteDiagnostics)
//   at Microsoft.CodeAnalysis.CSharp.OverloadResolution.IsApplicable[TMember](TMember member, TMember leastOverriddenMember, ArrayBuilder`1 typeArgumentsBuilder, AnalyzedArguments arguments, EffectiveParameters originalEffectiveParameters, EffectiveParameters constructedEffectiveParameters, ImmutableArray`1 argsToParamsMap, Boolean hasAnyRefOmittedArgument, HashSet`1& useSiteDiagnostics, Boolean inferWithDynamic)
//   at Microsoft.CodeAnalysis.CSharp.OverloadResolution.IsMemberApplicableInNormalForm[TMember](TMember member, TMember leastOverriddenMember, ArrayBuilder`1 typeArguments, AnalyzedArguments arguments, Boolean isMethodGroupConversion, Boolean allowRefOmittedArguments, Boolean inferWithDynamic, HashSet`1& useSiteDiagnostics)
//   at Microsoft.CodeAnalysis.CSharp.OverloadResolution.AddMemberToCandidateSet[TMember](TMember member, ArrayBuilder`1 results, ArrayBuilder`1 members, ArrayBuilder`1 typeArguments, AnalyzedArguments arguments, Boolean completeResults, Boolean isMethodGroupConversion, Boolean allowRefOmittedArguments, Dictionary`2 containingTypeMapOpt, Boolean inferWithDynamic, HashSet`1& useSiteDiagnostics, Boolean allowUnexpandedForm)
//   at Microsoft.CodeAnalysis.CSharp.OverloadResolution.PerformMemberOverloadResolution[TMember](ArrayBuilder`1 results, ArrayBuilder`1 members, ArrayBuilder`1 typeArguments, AnalyzedArguments arguments, Boolean completeResults, Boolean isMethodGroupConversion, Boolean allowRefOmittedArguments, HashSet`1& useSiteDiagnostics, Boolean inferWithDynamic, Boolean allowUnexpandedForm)
//   at Microsoft.CodeAnalysis.CSharp.OverloadResolution.MethodOrPropertyOverloadResolution[TMember](ArrayBuilder`1 members, ArrayBuilder`1 typeArguments, AnalyzedArguments arguments, OverloadResolutionResult`1 result, Boolean isMethodGroupConversion, Boolean allowRefOmittedArguments, HashSet`1& useSiteDiagnostics, Boolean inferWithDynamic, Boolean allowUnexpandedForm)
//   at Microsoft.CodeAnalysis.CSharp.Binder.ResolveDefaultMethodGroup(BoundMethodGroup node, AnalyzedArguments analyzedArguments, Boolean isMethodGroupConversion, HashSet`1& useSiteDiagnostics, Boolean inferWithDynamic, Boolean allowUnexpandedForm)
