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

    public Operation operation;
    public Color color;

    [Range(.1f, 100)]
    public float blendFactor;
    public ShapeDimensions dimensions;
    public void SetDimensionArray(RaymarchRenderer.Shape shape, vector12 _dimensions)
    {
        switch (shape)
        {
            case RaymarchRenderer.Shape.Cylinder:
                dimensions.cylH = _dimensions.a;
                dimensions.cylR = _dimensions.b;
                break;
            case RaymarchRenderer.Shape.Frustrum:
                dimensions.capConeR1 = _dimensions.a;
                dimensions.capConeR2 = _dimensions.b;
                dimensions.capConeH = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.CappedCone:
                dimensions.capConeR1 = _dimensions.a;
                dimensions.capConeR2 = _dimensions.b;
                dimensions.capConeH = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.Shpere:
                dimensions.sphereRadius = _dimensions.a;
                break;
            case RaymarchRenderer.Shape.Torus:
                dimensions.torusThickness = new Vector2(_dimensions.a, _dimensions.b);
                break;
            case RaymarchRenderer.Shape.CappedTorus:
                dimensions.cappedTorusRo = _dimensions.a;
                dimensions.cappedTorusRi = _dimensions.b;
                dimensions.cappedTorusThickness = new Vector2(_dimensions.c, _dimensions.d);
                break;
            case RaymarchRenderer.Shape.Link:
                dimensions.linkSeparation = _dimensions.a;
                dimensions.linkRadius = _dimensions.b;
                dimensions.linkThickness = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.Cone:
                dimensions.coneTan = new Vector2(_dimensions.a, _dimensions.b);
                dimensions.coneHeight = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.InfCone:
                dimensions.infConeTan = new Vector2(_dimensions.a, _dimensions.b);
                break;
            case RaymarchRenderer.Shape.Plane:
                dimensions.planeNormal = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                dimensions.planeDistance = _dimensions.d;
                break;
            case RaymarchRenderer.Shape.HexPrism:
                dimensions.hexPrismH = new Vector2(_dimensions.a, _dimensions.b);
                break;
            case RaymarchRenderer.Shape.TriPrism:
                dimensions.triPrismH = new Vector2(_dimensions.a, _dimensions.b);
                break;
            case RaymarchRenderer.Shape.Capsule:
                dimensions.capsuleA = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                dimensions.capsuleB = new Vector3(_dimensions.d, _dimensions.e, _dimensions.f);
                dimensions.capsuleR = _dimensions.g;
                break;
            case RaymarchRenderer.Shape.InfiniteCylinder:
                dimensions.infCylC = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                break;
            case RaymarchRenderer.Shape.Box:
                dimensions.boxSize = _dimensions.a;
                break;
            case RaymarchRenderer.Shape.RoundBox:
                dimensions.roundBoxSize = _dimensions.a;
                dimensions.roundBoxRoundFactor = _dimensions.b;
                break;
            case RaymarchRenderer.Shape.RoundedCylinder:
                dimensions.roundCylRa = _dimensions.a;
                dimensions.roundCylRb = _dimensions.b;
                dimensions.roundCylH = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.BoxFrame:
                dimensions.boxFrameSize = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                dimensions.boxFrameCavity = _dimensions.d;
                break;
            case RaymarchRenderer.Shape.SolidAngle:
                dimensions.solidAngleC = new Vector2(_dimensions.a, _dimensions.b);
                dimensions.solidAngleRa = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.CutSphere:
                dimensions.cutSphereR = _dimensions.a;
                dimensions.cutSphereH = _dimensions.b;
                break;
            case RaymarchRenderer.Shape.CutHollowSphere:
                dimensions.hollowSphereR = _dimensions.a;
                dimensions.hollowSphereH = _dimensions.b;
                dimensions.hollowSphereT = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.DeathStar:
                dimensions.deathStarRa = _dimensions.a;
                dimensions.deathStarRb = _dimensions.b;
                dimensions.deathStarD = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.RoundCone:
                dimensions.roundConeR1 = _dimensions.a;
                dimensions.roundConeR2 = _dimensions.b;
                dimensions.roundConeH = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.Ellipsoid:
                dimensions.ellipsoidRadius = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                break;
            case RaymarchRenderer.Shape.Rhombus:
                dimensions.rhombusLa = _dimensions.a;
                dimensions.rhombusLb = _dimensions.b;
                dimensions.rhombusH = _dimensions.c;
                dimensions.rhombusRa = _dimensions.d;
                break;
            case RaymarchRenderer.Shape.Octahedron:
                dimensions.octahedronSize = _dimensions.a;
                break;
            case RaymarchRenderer.Shape.Pyramid:
                dimensions.pyramidSize = _dimensions.a;
                break;
            case RaymarchRenderer.Shape.Triangle:
                dimensions.triangleSideA = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                dimensions.triangleSideB = new Vector3(_dimensions.d, _dimensions.e, _dimensions.f);
                dimensions.triangleSideC = new Vector3(_dimensions.g, _dimensions.h, _dimensions.i);
                break;
            case RaymarchRenderer.Shape.Quad:
                dimensions.quadSideA = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                dimensions.quadSideB = new Vector3(_dimensions.d, _dimensions.e, _dimensions.f);
                dimensions.quadSideC = new Vector3(_dimensions.g, _dimensions.h, _dimensions.i);
                dimensions.quadSideD = new Vector3(_dimensions.j, _dimensions.k, _dimensions.l);
                break;
            case RaymarchRenderer.Shape.Fractal:
                dimensions.fractalI = _dimensions.a;
                dimensions.fractalS = _dimensions.b;
                dimensions.fractalO = _dimensions.c;
                break;
            case RaymarchRenderer.Shape.Tesseract:
                dimensions.tesseractSize = new Vector3(_dimensions.a, _dimensions.b, _dimensions.c);
                break;

        }
    }
    public vector12 GetDimensionVectors(RaymarchRenderer.Shape shape)
    {
        vector12 dim = new vector12();

        switch (shape)
        {
            case RaymarchRenderer.Shape.Cylinder:
                dim = new vector12(dimensions.cylH, dimensions.cylR, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CappedCone:
                dim = new vector12(dimensions.capConeH, dimensions.capConeR1, dimensions.capConeH, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Frustrum:
                dim = new vector12(dimensions.capConeH, dimensions.capConeR1, dimensions.capConeH, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Shpere:
                dim = new vector12(dimensions.sphereRadius, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Torus:
                dim = new vector12(dimensions.torusThickness.x, dimensions.torusThickness.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CappedTorus:
                dim = new vector12(dimensions.cappedTorusRo, dimensions.cappedTorusRi, dimensions.cappedTorusThickness.x, dimensions.cappedTorusThickness.y, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Link:
                dim = new vector12(dimensions.linkSeparation, dimensions.linkRadius, dimensions.linkThickness, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Cone:
                dim = new vector12(dimensions.coneTan.x, dimensions.coneTan.y, dimensions.coneHeight, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.InfCone:
                dim = new vector12(dimensions.infConeTan.x, dimensions.infConeTan.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Plane:
                dim = new vector12(dimensions.planeNormal.x, dimensions.planeNormal.y, dimensions.planeNormal.z, dimensions.planeDistance, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.HexPrism:
                dim = new vector12(dimensions.hexPrismH.x, dimensions.hexPrismH.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.TriPrism:
                dim = new vector12(dimensions.triPrismH.x, dimensions.triPrismH.y, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Capsule:
                dim = new vector12(dimensions.capsuleA.x, dimensions.capsuleA.y, dimensions.capsuleA.z, dimensions.capsuleB.x, dimensions.capsuleB.y, dimensions.capsuleB.z, dimensions.capsuleR, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.InfiniteCylinder:
                dim = new vector12(dimensions.infCylC.x, dimensions.infCylC.y, dimensions.infCylC.z, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Box:
                dim = new vector12(dimensions.boxSize, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.RoundBox:
                dim = new vector12(dimensions.roundBoxSize, dimensions.roundBoxRoundFactor, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.RoundedCylinder:
                dim = new vector12(dimensions.roundCylRa, dimensions.roundCylRb, dimensions.roundCylH, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.BoxFrame:
                dim = new vector12(dimensions.boxFrameSize.x, dimensions.boxFrameSize.y, dimensions.boxFrameSize.z, dimensions.boxFrameCavity, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.SolidAngle:
                dim = new vector12(dimensions.solidAngleC.x, dimensions.solidAngleC.y, dimensions.solidAngleRa, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CutSphere:
                dim = new vector12(dimensions.cutSphereR, dimensions.cutSphereH, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.CutHollowSphere:
                dim = new vector12(dimensions.hollowSphereR, dimensions.hollowSphereH, dimensions.hollowSphereT, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.DeathStar:
                dim = new vector12(dimensions.deathStarRa, dimensions.deathStarRb, dimensions.deathStarD, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.RoundCone:
                dim = new vector12(dimensions.roundConeR1, dimensions.roundConeR2, dimensions.roundConeH, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Ellipsoid:
                dim = new vector12(dimensions.ellipsoidRadius.x, dimensions.ellipsoidRadius.y, dimensions.ellipsoidRadius.z, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Rhombus:
                dim = new vector12(dimensions.rhombusLa, dimensions.rhombusLb, dimensions.rhombusH, dimensions.rhombusRa, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Octahedron:
                dim = new vector12(dimensions.octahedronSize, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Pyramid:
                dim = new vector12(dimensions.pyramidSize, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Triangle:
                dim = new vector12(dimensions.triangleSideA.x, dimensions.triangleSideA.y, dimensions.triangleSideA.z, dimensions.triangleSideB.x, dimensions.triangleSideB.y, dimensions.triangleSideB.z, dimensions.triangleSideC.x, dimensions.triangleSideC.y, dimensions.triangleSideC.z, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Quad:
                dim = new vector12(dimensions.quadSideA.x, dimensions.quadSideA.y, dimensions.quadSideA.z, dimensions.quadSideB.x, dimensions.quadSideB.y, dimensions.quadSideB.z, dimensions.quadSideC.x, dimensions.quadSideC.y, dimensions.quadSideC.z, dimensions.quadSideD.x, dimensions.quadSideD.y, dimensions.quadSideD.z);
                return dim;

            case RaymarchRenderer.Shape.Fractal:
                dim = new vector12(dimensions.fractalI, dimensions.fractalS, dimensions.fractalO, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

            case RaymarchRenderer.Shape.Tesseract:
                dim = new vector12(dimensions.tesseractSize.x, dimensions.tesseractSize.y, dimensions.tesseractSize.z, dimensions.tesseractSize.w, 0, 0, 0, 0, 0, 0, 0, 0);
                return dim;

        }

        return dim;

    }
    public void InitializeShapeDimensions()
    {
        if (dimensions == null)
        {
            dimensions = CreateShapeDimensionsAsset();
        }
    }
    public Shape shape
    {
        get { return _shape; }
        set
        {
            _shape = value;
            InitializeShapeDimensions();
        }
    }
    private Shape _shape;
    ShapeDimensions CreateShapeDimensionsAsset()
    {
        ShapeDimensions asset = ScriptableObject.CreateInstance<ShapeDimensions>();

        string folderPath = "Assets/ScriptableObjects";

        if (!System.IO.Directory.Exists(folderPath))
        {
            AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
        }

        // Enhancing asset naming for better identification
        string timestamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        string assetName = "ShapeDimensions_" + timestamp;

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(folderPath + "/" + assetName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;

        return asset;
    }

}
