﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using jsc.meta.Commands.Rewrite;
using jsc.meta.Library;
using jsc.meta.Tools;
using Microsoft.CSharp;
using ScriptCoreLib;
using System.Reflection.Emit;
namespace jsc.meta.Commands.Reference
{
	[Description("Referencing source code in the cloud as components has never been that easy!")]
	public class ReferenceWebSource
	{
		// this could be extended to support zip files / tar.gz files
		// and have it download multiple sources at once
		// ScriptCoreLib.Archive needs to implement the algorithm then...

		const string WebSource = "WebSource";

		public FileInfo ProjectFileName;


		public void Invoke()
		{
			var csproj = XDocument.Load(ProjectFileName.FullName);
			var csproj_dirty = false;



			/*

<Project ToolsVersion="3.5" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
	<RootNamespace>AutoGeneratedReferences</RootNamespace>

  <ItemGroup>
	<Reference Include="System" />

  <ItemGroup>
	<None Include="Components\JohDoe.TextComponent" />
			*/

			XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
			var nsItemGroup = ns + "ItemGroup";
			var nsRootNamespace = ns + "RootNamespace";
			var nsPropertyGroup = ns + "PropertyGroup";
			var nsNone = ns + "None";
			var nsContent = ns + "Content";
			var nsDependentUpon = ns + "DependentUpon";
			var nsReference = ns + "Reference";
			var nsHintPath = ns + "HintPath";
			var nsAssemblyName = ns + "AssemblyName";

			var SourceAssemblyName = Enumerable.First(
				 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
				 from AssemblyName in PropertyGroup.Elements(nsAssemblyName)
				 select AssemblyName.Value
			);

			var DefaultNamespace = Enumerable.First(
				 from PropertyGroup in csproj.Root.Elements(nsPropertyGroup)
				 from RootNamespace in PropertyGroup.Elements(nsRootNamespace)
				 select RootNamespace.Value
			);

			// bin is assumed to being ignored by svn
			// we need to stage it
			var Staging = this.ProjectFileName.Directory.CreateSubdirectory("bin/" + WebSource + ".staging");

			// fixme: no caching as of yet
			//var Cache = Staging.CreateSubdirectory("cache");

			#region AddReference
			Action<FileInfo, AssemblyName> AddReference =
				(AssemblyFile, Name) =>
				{

					/* add reference
<Reference Include="AutoGeneratedReferences.Components.JohDoe.TextComponent, Version=0.0.0.0, Culture=neutral, processorArchitecture=MSIL">
  <SpecificVersion>False</SpecificVersion>
  <HintPath>bin\staging\AutoGeneratedReferences.Components.JohDoe.TextComponent.dll</HintPath>
</Reference>
					*/

					var TargetHintPath = AssemblyFile.FullName.Substring(ProjectFileName.Directory.FullName.Length + 1);

					if (!Enumerable.Any(
						 from ItemGroup in csproj.Root.Elements(nsItemGroup)
						 from Reference in ItemGroup.Elements(nsReference)
						 from HintPath in Reference.Elements(nsHintPath)
						 where TargetHintPath == HintPath.Value
						 select new { HintPath, Reference, ItemGroup }
						))
					{
						var TargetItemGroup = Enumerable.First(
							from ItemGroup in csproj.Root.Elements(nsItemGroup)
							from Reference in ItemGroup.Elements(nsReference)
							select ItemGroup
						);

						TargetItemGroup.Add(
							new XElement(nsReference,
								new XAttribute("Include", Name.ToString()),
								new XElement(nsHintPath, TargetHintPath)
							)
						);

						csproj_dirty = true;

					}
				};
			#endregion


			var Targets =
			  from ItemGroup in csproj.Root.Elements(nsItemGroup)
			  from None in ItemGroup.Elements(nsNone).Concat(ItemGroup.Elements(nsContent))
			  let Include = None.Attribute("Include").Value
			  let Directory = Path.GetDirectoryName(Include)
			  where Directory == WebSource || Directory.EndsWith("." + WebSource)

			  let TargetName = DefaultNamespace + "." + Directory.Replace("/", ".").Replace("\\", ".")
			  let Target = new FileInfo(Path.Combine(Staging.FullName, TargetName.Substring(DefaultNamespace.Length + 1) + ".dll"))

			  let File = new FileInfo(Path.Combine(ProjectFileName.Directory.FullName, Include))
			  group new { ItemGroup, None, Include, File, Directory, TargetName, Target } by Directory;

			var References = Enumerable.Distinct(
				from k in Targets
				from f in k
				// should we restrict us to single file or allow multiple files to
				// enable grouping?
				where f.File.Name == "references.txt"
				from r in File.ReadAllLines(f.File.FullName)
				select r
			);

			// http://support.microsoft.com/kb/304655
			var Sources = DownloadWebSource(References).ToArray();

			var Sources_CSharp = Sources.Where(k => k.Reference.EndsWith(".cs")).ToArray();
			var Sources_VB = Sources.Where(k => k.Reference.EndsWith(".vb")).ToArray();

			{
				var Assemblies = Compile(Sources_CSharp, Sources_VB).ToArray();
				var PrimaryTypes = Assemblies.SelectMany(k =>


					from kt in k.GetTypes()
					#region except that VB magic namespace
					where !(kt.Namespace != null && (
							kt.Namespace == "My" ||
							kt.Namespace.EndsWith(".My") ||
							kt.Namespace.EndsWith(".My.Resources")
						))
					#endregion
					select kt

				).ToArray();

				var Product = DefaultNamespace + "." + WebSource;

				// great now we need to merge them
				var r = new RewriteToAssembly
				{
					staging = Staging,
					PrimaryTypes = PrimaryTypes,
					product = Product,

					#region if we are going to inject code from jsc we need to copy it
					rename = new RewriteToAssembly.NamespaceRenameInstructions[] {
					    "jsc.meta->" +  DefaultNamespace,
					    "jsc->" +  DefaultNamespace,
					},

					merge = new RewriteToAssembly.MergeInstruction[] {
					    "jsc.meta",
					    "jsc"
					},
					#endregion

					PostRewrite =
						a =>
						{
							// at this point we are free to add any additional code here
							// maybe we should infer some cool classes?

							
							a.Assembly.DefineAttribute<ObfuscationAttribute>(
								new
								{
									Feature = "script",
								}
							);


						
						}
				};

				r.Invoke();

				AddReference(r.Output, new AssemblyName(Product));
			}

			if (csproj_dirty)
				csproj.Save(this.ProjectFileName.FullName);
		}

		private static IEnumerable<Assembly> Compile(SourceFile[] Sources_CSharp, SourceFile[] Sources_VB)
		{
			#region Sources_CSharp
			if (Sources_CSharp.Any())
			{
				// http://www.eggheadcafe.com/software/aspnet/30935490/codedomprovidercreatecom.aspx
				// http://social.msdn.microsoft.com/forums/en-US/netfxbcl/thread/512d9fdd-61af-4a0c-b78a-2f88738e651a/
				var cp = new Microsoft.CSharp.CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });

				var parameters = new CompilerParameters
				{
					GenerateExecutable = false,
					GenerateInMemory = true,
					IncludeDebugInformation = false,
				};

				// Include referenced assemblies
				parameters.ReferencedAssemblies.Add("mscorlib.dll");
				parameters.ReferencedAssemblies.Add("System.dll");
				parameters.ReferencedAssemblies.Add("System.Core.dll");

				var results = cp.CompileAssemblyFromSource(parameters, Sources_CSharp.Select(k => k.Content).ToArray());

				yield return results.CompiledAssembly;
			}
			#endregion

			#region Sources_VB
			if (Sources_VB.Any())
			{
				// http://msdn.microsoft.com/en-us/library/bb470844.aspx
				var cp = new Microsoft.VisualBasic.VBCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });

				var parameters = new CompilerParameters
				{
					GenerateExecutable = false,
					GenerateInMemory = true,
					IncludeDebugInformation = false,
				};

				// Include referenced assemblies
				parameters.ReferencedAssemblies.Add("mscorlib.dll");
				parameters.ReferencedAssemblies.Add("System.dll");
				parameters.ReferencedAssemblies.Add("System.Core.dll");

				var results = cp.CompileAssemblyFromSource(parameters, Sources_VB.Select(k => k.Content).ToArray());

				yield return results.CompiledAssembly;
			}
			#endregion
		}

		public class SourceFile
		{
			public string Reference;
			public string Content;
		}

		private static IEnumerable<SourceFile> DownloadWebSource(IEnumerable<string> References)
		{
			foreach (var Reference in References)
			{
				Console.WriteLine("downloading: " + Reference);

				var c = (HttpWebRequest)HttpWebRequest.Create(Reference);

				// http://code.logos.com/blog/2009/06/using_if-modified-since_in_http_requests.html
				// http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.ifmodifiedsince.aspx
				// http://www.acmebinary.com/blog/archive/2006/09/05/252.aspx

				var r = (HttpWebResponse)c.GetResponse();

				try
				{
					if (r.StatusCode == HttpStatusCode.OK)
					{
						var Content = new StreamReader(r.GetResponseStream()).ReadToEnd();

						yield return new SourceFile { Content = Content, Reference = Reference };
					}

				}
				finally
				{
					r.Close();
				}

			}
		}
	}
}
