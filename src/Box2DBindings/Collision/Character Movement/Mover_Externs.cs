using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

static partial class Mover
{
#if NET9_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2, CollisionPlane*, int, PlaneSolverResult> b2SolvePlanes;
    private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2, CollisionPlane*, int, Vec2> b2ClipVector;
        
    static unsafe Mover()
    {
        nint lib = nativeLibrary;

        nint ptr;
        NativeLibrary.TryGetExport(lib, "b2SolvePlanes", out ptr);
        b2SolvePlanes = (delegate* unmanaged[Cdecl]<Vec2, CollisionPlane*, int, PlaneSolverResult>)ptr;
        NativeLibrary.TryGetExport(lib, "b2ClipVector", out ptr);
        b2ClipVector = (delegate* unmanaged[Cdecl]<Vec2, CollisionPlane*, int, Vec2>)ptr;
    }
#else
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SolvePlanes")]
        private unsafe static extern PlaneSolverResult b2SolvePlanes(Vec2 position, CollisionPlane* planes, int count);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ClipVector")]
        private unsafe static extern Vec2 b2ClipVector(Vec2 vector, CollisionPlane* planes, int count);
#endif

}