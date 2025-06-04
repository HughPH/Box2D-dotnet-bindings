# Box2D-dotnet-bindings
Box2D 3.x Bindings for dotnet (C#, F#, VB, ...)

[Doxygen documentation](https://hughph.github.io/Box2D-dotnet-bindings/namespaceBox2D.html)

### Includes native binaries for:

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

Please [submit a github issue](https://github.com/HughPH/Box2D-dotnet-bindings/issues) if you try an untested platform and it works or doesn't work.

## What is it?
This is a "link" from dotnet to Box2D 3.x, with an API that should be more familiar and comfortable to dotnet users. World has a Bodies property, Body has a Shapes property and a Joints property, and so on. Delegates are fully formed, rather than being vague IntPtrs. All methods and most properties are PascalCased and have XML documentation.

## How is this better than Box2D.NetStandard/another port of Box2D 2.x?
Box2D 3.x contains significant efficiency improvements that make use of SIMD intrinsics.
While it's not impossible to implement in C# - intrinsics have been available since dotnet 6 - it's probably unlikely that Box2D 3.x will be ported into Box2D.NetStandard: one of the challenges with Box2D.NetStandard has always been keeping it up-to-date with changes to Box2D 2.x, and the performance was never on par. Since Box2D 3.x builds to shared libraries, it makes much more sense all round to simply write bindings to that library than to put time into porting it. It also means I can target .net standard 2.1 instead of dotnet 6 or above.

## How is this better than Hexa.NET.Box2D or Box2D.NET?
### Hexa.NET.Box2D & Box2D.NET.Release (/Box2D.NET.Debug)
These are auto-generated with different code generators, and are each a direct mapping of Box2D's "flat" C API. That means they require you to use pointers and unsafe code, and they may not have the same level of documentation as this library.
### Box2D.NET (not Box2D.NET.Release)
Box2D.NET is a copy-paste code port of Box2D even down to the Tracy integration. The project is well documented, exposes no pointers and has generic delegates, but it allocates on the heap, which causes GC pressure and potentially delays while GC runs.
### Box2D-dotnet-bindings (this project/package)
By contrast, this is a hand-crafted API that is designed to be more idiomatic to dotnet coders. These bindings also have full XmlDoc comments and fully defined delegates, and overloads that take generic type arguments and consume and deliver Spans of data instead of pointers. Heap allocation is consciously kept to a minimum. The world can be browsed in the debugger.

"Better" is probably subjective: this library is designed to bring quality of life improvements. *This API is complete up to 3.1.1 and will be upgraded as new versions of Box2D are released.* 

## Getting things working
Just install this package. It should work. If not, *please* [raise an issue in the github repository](https://github.com/HughPH/Box2D-dotnet-bindings/issues)

## Known limitations / quirks
- The native binaries are built from Erin's Box2D latest release tag.
- Extensions to the Box2D API won't appear automagically. Please feel free to submit a PR if you want to add something.

