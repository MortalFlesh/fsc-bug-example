FSCS Bug Example
================

[![Check](https://github.com/MortalFlesh/fscs-bug-example/actions/workflows/checks.yaml/badge.svg)](https://github.com/MortalFlesh/fscs-bug-example/actions/workflows/checks.yaml)

## Normal behavior

- `dotnet run`
- `./build.sh -t run`
- `./build.sh -t watch`

All of above yield a successful result of:

```
====================
FSCS.BugExample

ProjectOptions: { ProjectFileName = "domain/domain.fsx.fsproj"
  ProjectId = None
  SourceFiles = [|"domain/domain.fsx"|]
  OtherOptions =
   [|"--noframework"; "--warn:3";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Numerics.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/netstandard.dll";
     "-r:/Users/chromecp/fsharp/fscs-bug-example/bin/Debug/net5.0/FSharp.Core.dll";
     "-r:/usr/local/share/dotnet/shared/Microsoft.NETCore.App/5.0.9/../../../packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Numerics.dll";
     "-r:/usr/local/share/dotnet/shared/Microsoft.NETCore.App/5.0.9/../../../packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.dll";
     "-r:/usr/local/share/dotnet/shared/Microsoft.NETCore.App/5.0.9/../../../packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/netstandard.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/mscorlib.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Xml.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Data.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Drawing.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Core.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Configuration.dll";
     "-r:/usr/local/share/dotnet/shared/Microsoft.NETCore.App/5.0.9/System.ValueTuple.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Runtime.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Linq.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Reflection.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Linq.Expressions.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Threading.Tasks.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.IO.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Net.Requests.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Collections.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Runtime.Numerics.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Threading.dll";
     "-r:/usr/local/share/dotnet/packs/Microsoft.NETCore.App.Ref/5.0.0/ref/net5.0/System.Web.dll"|]
  ReferencedProjects = [||]
  IsIncompleteTypeCheckEnvironment = false
  UseScriptResolutionRules = true
  LoadTime = 31/12/9999 23:59:59
  UnresolvedReferences = Some FSharpUnresolvedReferencesSet []
  OriginalLoadReferences = []
  Stamp = None }
====================
```

## A Bug?

But when I `publish` the code

- `dotnet publish -c Release /p:PublishSingleFile=true -o dist/osx-x64 --self-contained -r osx-x64 ./BugExample.fsproj`
- `./build.sh -t release`

and run it

- `dist/osx-x64/BugExample`

it ends with:

```
====================
FSCS.BugExample

GetProjectOptionsFromScript.Error: System.TypeInitializationException: The type initializer for '<StartupCode$FSharp-Compiler-Service>.$FSharpCheckerResults' threw an exception.
 ---> System.NullReferenceException: Object reference not set to an instance of an object.
   at <StartupCode$FSharp-Compiler-Service>.$FSharpCheckerResults..cctor()
   --- End of inner exception stack trace ---
   at FSharp.Compiler.CodeAnalysis.FSharpCheckerResultsSettings.get_defaultFSharpBinariesDir()
   at <StartupCode$FSharp-Compiler-Service>.$Service.e@822-10(BackgroundCompiler _, FSharpOption`1 useSdkRefs, FSharpOption`1 useFsiAuxLib, ISourceText sourceText, FSharpOption`1 sdkDirOverride, FSharpOption`1 previewEnabled, FSharpOption`1 otherFlags, FSharpOption`1 optionsStamp, FSharpOption`1 loadedTimeStamp, String filename, FSharpOption`1 assumeDotNetFramework, CompilationThreadToken ctok, ErrorScope _arg36)
   at <StartupCode$FSharp-Compiler-Service>.$Service.f@826-139(BackgroundCompiler _, FSharpOption`1 useSdkRefs, FSharpOption`1 useFsiAuxLib, ISourceText sourceText, FSharpOption`1 sdkDirOverride, FSharpOption`1 previewEnabled, FSharpOption`1 otherFlags, FSharpOption`1 optionsStamp, FSharpOption`1 loadedTimeStamp, String filename, FSharpOption`1 assumeDotNetFramework, CompilationThreadToken ctok, Unit unitVar)
   at <StartupCode$FSharp-Compiler-Service>.$Service.GetProjectOptionsFromScript@871-1.Invoke(CancellationToken ct)
   at <StartupCode$FSharp-Compiler-Service>.$Reactor.EnqueueAndAwaitOpAsync@220-2.Invoke(CompilationThreadToken ctok)
--- End of stack trace from previous location ---
   at Microsoft.FSharp.Control.AsyncResult`1.Commit()
   at Microsoft.FSharp.Control.AsyncPrimitives.RunSynchronouslyInAnotherThread[a](CancellationToken token, FSharpAsync`1 computation, FSharpOption`1 timeout)
   at Microsoft.FSharp.Control.AsyncPrimitives.RunSynchronously[T](CancellationToken cancellationToken, FSharpAsync`1 computation, FSharpOption`1 timeout)
   at Microsoft.FSharp.Control.FSharpAsync.RunSynchronously[T](FSharpAsync`1 computation, FSharpOption`1 timeout, FSharpOption`1 cancellationToken)
   at Program.DomainResolverLibrary.parseAndCheck(FSharpChecker checker, String file, String contents)
====================
```

---

## My environment:

- `dotnet --info`

```
.NET SDK (reflecting any global.json):
 Version:   5.0.400
 Commit:    d61950f9bf

Runtime Environment:
 OS Name:     Mac OS X
 OS Version:  11.0
 OS Platform: Darwin
 RID:         osx.11.0-x64
 Base Path:   /usr/local/share/dotnet/sdk/5.0.400/

Host (useful for support):
  Version: 6.0.0-preview.4.21253.7
  Commit:  bfd6048a60

.NET SDKs installed:
  5.0.103 [/usr/local/share/dotnet/sdk]
  5.0.200 [/usr/local/share/dotnet/sdk]
  5.0.300 [/usr/local/share/dotnet/sdk]
  5.0.301 [/usr/local/share/dotnet/sdk]
  5.0.302 [/usr/local/share/dotnet/sdk]
  5.0.400 [/usr/local/share/dotnet/sdk]
  6.0.100-preview.4.21255.9 [/usr/local/share/dotnet/sdk]

.NET runtimes installed:
  Microsoft.AspNetCore.App 5.0.3 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 5.0.6 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 5.0.7 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 5.0.8 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 5.0.9 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 6.0.0-preview.4.21253.5 [/usr/local/share/dotnet/shared/Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 5.0.3 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 5.0.6 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 5.0.7 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 5.0.8 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 5.0.9 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]
  Microsoft.NETCore.App 6.0.0-preview.4.21253.7 [/usr/local/share/dotnet/shared/Microsoft.NETCore.App]

To install additional .NET runtimes or SDKs:
  https://aka.ms/dotnet-download
```
