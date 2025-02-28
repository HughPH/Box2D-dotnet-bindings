# Box2D-dotnet-bindings
Box2D 3.x Bindings for dotnet (C#, F#, VB, ...)

## What is it?
This is a "link" from dotnet to Box2D 3.x, with an API that should be more familiar and comfortable to dotnet users. World has a Bodies property, Body has a Shapes property and a Joints property, and so on. Delegates are fully formed, rather than being vague IntPtrs. All methods and most properties are PascalCased and have XML documentation.

## How is this better than Box2D.NetStandard/another port of Box2D 2.x?
Box2D 3.x contains significant efficiency improvements that make use of SIMD intrinsics.
While it's not impossible to implement in C# - intrinsics have been available since dotnet 6 - it's probably unlikely that Box2D 3.x will be ported into Box2D.NetStandard: one of the challenges with Box2D.NetStandard has always been keeping it up-to-date with changes to Box2D 2.x, and the performance was never on par. Since Box2D 3.x builds to shared libraries, it makes much more sense all round to simply write bindings to that library than to put time into porting it. It also means I can target .net standard 2.1 instead of dotnet 6 or above.

## How is this better than Hexa.NET.Box2D or Box2D.NET?
Hexa.NET.Box2D and Box2D.NET are auto-generated with code generators, and are direct mappings of the Box2D API. This, by contrast, is a hand-crafted API that is designed to be more idiomatic to dotnet coders.
These bindings also have full XmlDoc comments and fully defined delegates.
In this case "better" is probably subjective: Hexa.NET.Box2D and Box2D.NET are more likely to be API-complete with the Box2D version that they target, while this library brings quality of life improvements. 

## Getting things working
Just install this package. It should work. If not, *please* raise an issue in the github repository: https://github.com/HughPH/Box2D-dotnet-bindings/issues

## Known limitations / quirks
- The native binaries are built from Erin's Box2D mainline, not the latest release tag. This will capture any latest bugfixes, but *could* introduce bugs - in which case, revert to the previous version.
- Extensions to the Box2D API won't appear automagically. Please feel free to submit a PR if you want to add something.
- The native binaries are built for Windows x64 and Linux x64 only.
