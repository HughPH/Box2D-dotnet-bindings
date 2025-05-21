using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

static partial class Mover
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2, CollisionPlane[], int, PlaneSolverResult> b2SolvePlanes;
    private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2, CollisionPlane[], int, Vec2> b2ClipVector;
        
    static unsafe Mover()
    {
        nint lib = Core.NativeLibHandle;

        NativeLibrary.TryGetExport(lib, "b2SolvePlanes", out var solvePtr);
        NativeLibrary.TryGetExport(lib, "b2ClipVector", out var clipPtr);

        b2SolvePlanes = (delegate* unmanaged[Cdecl]<Vec2, CollisionPlane[], int, PlaneSolverResult>)solvePtr;
        b2ClipVector = (delegate* unmanaged[Cdecl]<Vec2, CollisionPlane[], int, Vec2>)clipPtr;
    }
#else
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SolvePlanes")]
        private static extern PlaneSolverResult b2SolvePlanes(Vec2 position, [In] CollisionPlane[] planes, int count);

        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ClipVector")]
        private static extern Vec2 b2ClipVector(Vec2 vector, [In] CollisionPlane[] planes, int count);
#endif

}