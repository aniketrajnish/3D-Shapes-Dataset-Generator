using System;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class RaymarchRenderer : MonoBehaviour
{ 
    public enum Shape
    {
        Cylinder,
        Frustrum,
        Shpere,
        Torus,
        CappedTorus,
        Link,
        Cone,
        InfCone,
        Plane,
        HexPrism,
        TriPrism,
        Capsule,
        InfiniteCylinder,
        Box,
        RoundBox,
        RoundedCylinder,
        CappedCone,
        BoxFrame,
        SolidAngle,
        CutSphere,
        CutHollowSphere,
        DeathStar,
        RoundCone,
        Ellipsoid,
        Rhombus,
        Octahedron,
        Pyramid,
        Triangle,
        Quad,
        Fractal,
        Tesseract
    };     
    public enum Operation
    {
        Union,        
        Inrersect,
        Subtract
    };

    public Shape shape;
    public Operation operation;
    public Color color;

    [Range(.1f, 100)]
    public float blendFactor;
    public void SetDimensionArray(RaymarchRenderer.Shape shape, vector12 dimensions)
    {
        switch(shape)
        {
            case RaymarchRenderer.Shape.Cylinder:
                CylinderDimensions.h = dimensions.a;
                CylinderDimensions.r = dimensions.b;
                break;
            case RaymarchRenderer.Shape.Frustrum:
                CappedConeDimensions.r1 = dimensions.a;
                CappedConeDimensions.r2 = dimensions.b;
                CappedConeDimensions.h = dimensions.c;
                break;
            case RaymarchRenderer.Shape.CappedCone:
                CappedConeDimensions.r1 = dimensions.a;
                CappedConeDimensions.r2 = dimensions.b;
                CappedConeDimensions.h = dimensions.c;
                break;
            case RaymarchRenderer.Shape.Shpere:
                SphereDimensions.radius = dimensions.a;
                break;
            case RaymarchRenderer.Shape.Torus:
                TorusDimensions.thickness = new Vector2(dimensions.a, dimensions.b);
                break;
            case RaymarchRenderer.Shape.CappedTorus:
                CappedTorusDimensions.ro = dimensions.a;
                CappedTorusDimensions.ri = dimensions.b;
                CappedTorusDimensions.thickness = new Vector2(dimensions.c, dimensions.d);
                break;
            case RaymarchRenderer.Shape.Link:
                LinkDimensions.separation = dimensions.a;
                LinkDimensions.radius = dimensions.b;
                LinkDimensions.thickness = dimensions.c;
                break;
            case RaymarchRenderer.Shape.Cone:
                ConeDimensions.tan = new Vector2(dimensions.a, dimensions.b);
                ConeDimensions.height = dimensions.c;
                break;
            case RaymarchRenderer.Shape.InfCone:
                InfiniteConeDimensions.tan = new Vector2(dimensions.a, dimensions.b);
                break;
            case RaymarchRenderer.Shape.Plane:
                PlaneDimensions.normal = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                PlaneDimensions.distance = dimensions.d;
                break;
            case RaymarchRenderer.Shape.HexPrism:
                HexagonalPrismDimensions.h = new Vector2(dimensions.a, dimensions.b);
                break;
            case RaymarchRenderer.Shape.TriPrism:
                TriangularPrismDimensions.h = new Vector2(dimensions.a, dimensions.b);
                break;
            case RaymarchRenderer.Shape.Capsule:
                CapsuleDimensions.a = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                CapsuleDimensions.b = new Vector3(dimensions.d, dimensions.e, dimensions.f);
                CapsuleDimensions.r = dimensions.g;
                break;
            case RaymarchRenderer.Shape.InfiniteCylinder:
                InfiniteCylinderDimensions.c = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                break;
            case RaymarchRenderer.Shape.Box:
                BoxDimensions.size = dimensions.a;
                break;
            case RaymarchRenderer.Shape.RoundBox:
                RoundBoxDimensions.size = dimensions.a;
                RoundBoxDimensions.roundFactor = dimensions.b;
                break;
            case RaymarchRenderer.Shape.RoundedCylinder:
                RoundedCylinderDimensions.ra = dimensions.a;
                RoundedCylinderDimensions.rb = dimensions.b;
                RoundedCylinderDimensions.h = dimensions.c;
                break;
            case RaymarchRenderer.Shape.BoxFrame:
                BoxFrameDimensions.size = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                BoxFrameDimensions.cavity = dimensions.d;
                break;
            case RaymarchRenderer.Shape.SolidAngle:
                SolidAngleDimensions.c = new Vector2(dimensions.a, dimensions.b);
                SolidAngleDimensions.ra = dimensions.c;
                break;
            case RaymarchRenderer.Shape.CutSphere:
                CutSphereDimensions.r = dimensions.a;
                CutSphereDimensions.h = dimensions.b;
                break;
            case RaymarchRenderer.Shape.CutHollowSphere:
                HollowSphereDimensions.r = dimensions.a;
                HollowSphereDimensions.h = dimensions.b;
                HollowSphereDimensions.t = dimensions.c;
                break;
            case RaymarchRenderer.Shape.DeathStar:
                DeathStarDimensions.ra = dimensions.a;
                DeathStarDimensions.rb = dimensions.b;
                DeathStarDimensions.d = dimensions.c;
                break;
            case RaymarchRenderer.Shape.RoundCone:
                RoundConeDimensions.r1 = dimensions.a;
                RoundConeDimensions.r2 = dimensions.b;
                RoundConeDimensions.h = dimensions.c; 
                break;
            case RaymarchRenderer.Shape.Ellipsoid:
                EllipsoidDimensions.Radius = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                break;
            case RaymarchRenderer.Shape.Rhombus:
                RhombusDimensions.la = dimensions.a;
                RhombusDimensions.lb = dimensions.b;
                RhombusDimensions.h = dimensions.c;
                RhombusDimensions.ra = dimensions.d;
                break;
            case RaymarchRenderer.Shape.Octahedron:
                OctahedronDimensions.size = dimensions.a;
                break;
            case RaymarchRenderer.Shape.Pyramid:
                PyramidDimensions.size = dimensions.a;
                break;
            case RaymarchRenderer.Shape.Triangle:
                TriangleDimensions.sideA = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                TriangleDimensions.sideB = new Vector3(dimensions.d, dimensions.e, dimensions.f);
                TriangleDimensions.sideC = new Vector3(dimensions.g, dimensions.h, dimensions.i);
                break;
            case RaymarchRenderer.Shape.Quad:
                QuadDimensions.sideA = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                QuadDimensions.sideB = new Vector3(dimensions.d, dimensions.e, dimensions.f);
                QuadDimensions.sideC = new Vector3(dimensions.g, dimensions.h, dimensions.i);
                QuadDimensions.sideD = new Vector3(dimensions.j, dimensions.k, dimensions.l);
                break;
            case RaymarchRenderer.Shape.Fractal:
                FractalDimenisons.i = dimensions.a;
                FractalDimenisons.s = dimensions.b;
                FractalDimenisons.o = dimensions.c;
                break;
            case RaymarchRenderer.Shape.Tesseract:
                TesseractDimensions.size = new Vector3(dimensions.a, dimensions.b, dimensions.c);
                break;

        }
    }
    public vector12 GetDimensionVectors(RaymarchRenderer.Shape shape)
    {
        vector12 dim = new vector12();

        switch(shape)
        {
            case RaymarchRenderer.Shape.Cylinder:
                dim =  new vector12(CylinderDimensions.h, CylinderDimensions.r, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CappedCone:
                dim = new vector12(CappedConeDimensions.r1, CappedConeDimensions.r2, CappedConeDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Frustrum:
                dim = new vector12(CappedConeDimensions.r1, CappedConeDimensions.r2, CappedConeDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Shpere:
                dim = new vector12(SphereDimensions.radius, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Torus:
                dim = new vector12(TorusDimensions.thickness.x, TorusDimensions.thickness.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CappedTorus:
                dim = new vector12(CappedTorusDimensions.ro, CappedTorusDimensions.ri, CappedTorusDimensions.thickness.x, CappedTorusDimensions.thickness.y, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Link:
                dim = new vector12(LinkDimensions.separation, LinkDimensions.radius, LinkDimensions.thickness, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Cone:
                dim = new vector12(ConeDimensions.tan.x, ConeDimensions.tan.y, ConeDimensions.height, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.InfCone:
                dim = new vector12(InfiniteConeDimensions.tan.x, InfiniteConeDimensions.tan.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Plane:
                dim = new vector12(PlaneDimensions.normal.x, PlaneDimensions.normal.y, PlaneDimensions.normal.z, PlaneDimensions.distance, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.HexPrism:
                dim = new vector12(HexagonalPrismDimensions.h.x, HexagonalPrismDimensions.h.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.TriPrism:
                dim = new vector12(TriangularPrismDimensions.h.x, TriangularPrismDimensions.h.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Capsule:
                dim = new vector12(CapsuleDimensions.a.x, CapsuleDimensions.a.y, CapsuleDimensions.a.z, CapsuleDimensions.b.x, CapsuleDimensions.b.y, CapsuleDimensions.b.z, CapsuleDimensions.r,0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.InfiniteCylinder:
                dim = new vector12(InfiniteCylinderDimensions.c.x, InfiniteCylinderDimensions.c.y, InfiniteCylinderDimensions.c.z, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Box:
                dim = new vector12(BoxDimensions.size, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.RoundBox:
                dim = new vector12(RoundBoxDimensions.size, RoundBoxDimensions.roundFactor, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.RoundedCylinder:
                dim = new vector12(RoundedCylinderDimensions.ra, RoundedCylinderDimensions.rb, RoundedCylinderDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.BoxFrame:
                dim = new vector12(BoxFrameDimensions.size.x, BoxFrameDimensions.size.y, BoxFrameDimensions.size.z, BoxFrameDimensions.cavity, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.SolidAngle:
                dim = new vector12(SolidAngleDimensions.c.x, SolidAngleDimensions.c.y, SolidAngleDimensions.ra, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CutSphere:
                dim = new vector12(CutSphereDimensions.r, CutSphereDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CutHollowSphere:
                dim = new vector12(HollowSphereDimensions.r, HollowSphereDimensions.h, HollowSphereDimensions.t, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.DeathStar:
                dim = new vector12(DeathStarDimensions.ra, DeathStarDimensions.rb, DeathStarDimensions.d, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.RoundCone:
                dim = new vector12(RoundConeDimensions.r1, RoundConeDimensions.r2, RoundConeDimensions.h, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Ellipsoid:
                dim = new vector12(EllipsoidDimensions.Radius.x, EllipsoidDimensions.Radius.y, EllipsoidDimensions.Radius.z, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Rhombus:
                dim = new vector12(RhombusDimensions.la, RhombusDimensions.lb, RhombusDimensions.h, RhombusDimensions.ra, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Octahedron:
                dim = new vector12(OctahedronDimensions.size, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Pyramid:
                dim = new vector12(PyramidDimensions.size, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Triangle:
                dim = new vector12(TriangleDimensions.sideA.x, TriangleDimensions.sideA.y, TriangleDimensions.sideA.z, TriangleDimensions.sideB.x, TriangleDimensions.sideB.y, TriangleDimensions.sideB.z, TriangleDimensions.sideC.x, TriangleDimensions.sideC.y, TriangleDimensions.sideC.z, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Quad:
                dim = new vector12(QuadDimensions.sideA.x, QuadDimensions.sideA.y, QuadDimensions.sideA.z, QuadDimensions.sideB.x, QuadDimensions.sideB.y, QuadDimensions.sideB.z, QuadDimensions.sideC.x, QuadDimensions.sideC.y, QuadDimensions.sideC.z, QuadDimensions.sideD.x, QuadDimensions.sideD.y, QuadDimensions.sideD.z);
                return dim;

            case RaymarchRenderer.Shape.Fractal:
                dim = new vector12(FractalDimenisons.i, FractalDimenisons.s, FractalDimenisons.o, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Tesseract:
                dim = new vector12(TesseractDimensions.size.x, TesseractDimensions.size.y, TesseractDimensions.size.z, TesseractDimensions.size.w, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

        }

        return dim;       
        
    }
    
}

public struct CylinderDimensions
{
    public static float r;
    public static float h;
};
public struct CappedConeDimensions
{
    public static float h;
    public static float r1;
    public static float r2;
};

public struct SphereDimensions
{
    public static float radius;
};
public struct TorusDimensions
{
    public static Vector2 thickness;
};
public struct CappedTorusDimensions
{
    public static float ro;
    public static float ri;
    public static Vector2 thickness;
};
public struct LinkDimensions
{
    public static float separation;
    public static float radius;
    public static float thickness;
};
public struct ConeDimensions
{
    public static Vector2 tan;
    public static float height;
};
public struct InfiniteConeDimensions
{
    public static Vector2 tan;
};
public struct PlaneDimensions
{
    public static Vector3 normal;
    public static float distance;
};
public struct HexagonalPrismDimensions
{
    public static Vector2 h;
};
public struct TriangularPrismDimensions
{
    public static Vector2 h;
};
public struct CapsuleDimensions
{
    public static Vector3 a;
    public static Vector3 b;
    public static float r;
};
public struct InfiniteCylinderDimensions
{
    public static Vector3 c;
};
public struct BoxDimensions
{
    public static float size;
};
public struct RoundBoxDimensions
{
    public static float size;
    public static float roundFactor;
};
public struct RoundedCylinderDimensions
{
    public static float ra;
    public static float rb;
    public static float h;
};
public struct BoxFrameDimensions
{
    public static Vector3 size;
    public static float cavity;
};
public struct SolidAngleDimensions
{
    public static Vector2 c;
    public static float ra;
};
public struct CutSphereDimensions
{
    public static float r;
    public static float h;
};
public struct HollowSphereDimensions
{
    public static float r;
    public static float h;
    public static float t;
};
public struct DeathStarDimensions
{
    public static float ra;
    public static float rb;
    public static float d;
};
public struct RoundConeDimensions
{
    public static float r1;
    public static float r2;
    public static float h;
};
public struct EllipsoidDimensions
{
    public static Vector3 Radius;
};
public struct RhombusDimensions
{
    public static float la;
    public static float lb;
    public static float h;
    public static float ra;
};
public struct OctahedronDimensions
{
    public static float size;
};
public struct PyramidDimensions
{
    public static float size;
};
public struct TriangleDimensions
{
    public static Vector3 sideA;
    public static Vector3 sideB;
    public static Vector3 sideC;
};
public struct QuadDimensions
{
    public static Vector3 sideA;
    public static Vector3 sideB;
    public static Vector3 sideC;
    public static Vector3 sideD;
};

public struct FractalDimenisons
{
    public static float i;
    public static float s;
    public static float o;
};
public struct TesseractDimensions
{
    public static Vector4 size;
};
