# Box2D-dotnet-bindings
Box2D 3.x Bindings for dotnet (C#, F#, VB, ...)

### The NuGet package includes native binaries for:

| Platform  | Architecture | Tested |
|-----------| ------------ |-------|
| Windows   | x64          | Yes   |
| Windows   | x86          | No    |
| Windows   | arm64        | No    |
| Linux     | x64          | Yes   |
| Linux     | x86          | No    |
| Linux     | arm64        | No    |
| MacOS     | x64          | No    |
| MacOS     | arm64        | Yes   |

Please [submit an issue](https://github.com/HughPH/Box2D-dotnet-bindings/issues) if you try an untested platform and it works or doesn't work.

## What is it?
This is a "link" from dotnet to Box2D 3.x, with an API that should be more familiar and comfortable to dotnet users.

## How is this better than Box2D.NetStandard?
Box2D 3.x contains significant efficiency improvements that make use of SIMD intrinsics.
While it's not impossible to implement in C# - intrinsics have been available since dotnet 6 - it's probably unlikely that Box2D 3.x will be ported into Box2D.NetStandard: one of the challenges with Box2D.NetStandard has always been keeping it up-to-date with changes to Box2D 2.x, and the performance was never on par. Since Box2D 3.x builds to shared libraries, it makes much more sense all round to simply write bindings to that library than to put time into porting it. It also means I can target .net standard 2.1 instead of dotnet 6 or above.

## How is this better than Hexa.NET.Box2D or Box2D.NET?
### Hexa.NET.Box2D & Box2D.NET.Release (/Box2D.NET.Debug)
These are auto-generated with different code generators, and are each a direct mapping of Box2D's "flat" C API. That means they require you to use pointers and unsafe code, and they may not have the same level of documentation as this library.
### Box2D.NET (not Box2D.NET.Release)
Box2D.NET is a copy-paste code port of Box2D even down to the Tracy integration. The project is well documented, exposes no pointers and has generic delegates, but it allocates on the heap, which causes GC pressure and potentially delays while GC runs.
### Box2D-dotnet-bindings (this project/package)
By contrast, this is a hand-crafted API that is designed to be more idiomatic to dotnet coders. These bindings also have full XmlDoc comments and fully defined delegates, and overloads that take generic type arguments and consume and deliver Spans of data instead of pointers. Heap allocation is consciously kept to a minimum. The world can be browsed in the debugger.

"Better" is probably subjective: this library is designed to bring quality of life improvements. *This API is complete up to 3.1.0 and will be upgraded as new versions of Box2D are released.*

### This library includes:
Full XML documentation

<img src="media/Intellisense.png" alt="An image demonstrating the XMLDoc."/>

Fully defined delegates

<img src="media/Delegates.png" alt="An image demonstrating two delegates, one which is a direct assignment of a method, and the other which is a lambda."/>

PascalCased properties that populate from the underlying Box2D API

<img src="media/BodyProperties.png" alt="An image demonstrating the PascalCased properties of a Body object showing detailed information about the Body."/>


## Raising Issues

If you think you've found a bug, it's best if you can provide code that proves the bug. That code can then go towards a unit test to initially prove the bug, and later prevent regression. Sometimes bugs or queries may be referred to the original Box2D project.

## Contributing

If you want to add code, please keep it consistent with the rest of the codebase. Consistency within a codebase helps others recognise repeated patterns. Your IDE may automatically detect and use the .editorconfig file in the root of the project.

## Getting things working

If you want to just use it, get the NuGet package HughPH.Box2D using your IDE. Package info: https://www.nuget.org/packages/HughPH.Box2D/

If you want to contribute:

1. Clone this repo, then either build it or add a DLL reference, or copy it into your solution and add a project reference, or configure it as a submodule.
- (If this is too cryptic, you should probably just use the NuGet package - which *should* include the native libraries - if this doesn't work, please raies an Issue)
2. Clone the main branch of Erin Catto's incredible Box2D project from https://github.com/erincatto/box2d
3. Build Box2D shared library:
- CD into the box2d repo
- Execute the commands in build.sh or build.bat, but for the first `cmake` command, add `-DBOX2D_SAMPLES=OFF -DBUILD_SHARED_LIBS=ON` before the `..`
- (If this is too cryptic, this might not be the project for you.)
4. You fill find the shared object or DLL in ./build/src on Linux and .\build\bin\Debug on Windows
5. Make sure that file gets into your output dir
