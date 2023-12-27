using UnityEngine;
[CreateAssetMenu(fileName = "Shape Dimensions", menuName = "Makra/Shape Dimensions")]
public class ShapeDimensions : ScriptableObject
{
    public float cylH = 1.0f;
    public float cylR = 1.0f;
    public float sphereRadius = .5f;
    public Vector2 torusThickness = new Vector2(.4f, .1f);
    public float cappedTorusRo = .25f;
    public float cappedTorusRi = .1f;
    public Vector2 cappedTorusThickness = new Vector2(.1f, .1f);
    public float linkSeparation = .13f;
    public float linkRadius = .2f;
    public float linkThickness = .09f;
    public Vector2 coneTan = new Vector2(1, 2);
    public float coneHeight = 1f;
    public Vector2 infConeTan = new Vector2(.1f, .1f);
    public Vector3 planeNormal = new Vector3(0, .5f, .5f);
    public float planeDistance = 1f;
    public Vector2 hexPrismH = new Vector2(.25f, .25f);
    public Vector2 triPrismH = new Vector2(.25f, .25f);
    public Vector3 capsuleA = new Vector3(.25f, .1f, .25f);
    public Vector3 capsuleB = new Vector3(.1f, .25f, .25f);
    public float capsuleR = .25f;
    public Vector3 infCylC = new Vector3(0, .25f, .25f);
    public float boxSize = .25f;
    public float roundBoxSize = .3f;
    public float roundBoxRoundFactor = .1f;
    public float roundCylRa = .25f;
    public float roundCylRb = .1f;
    public float roundCylH = .25f;
    public float capConeH = .5f;
    public float capConeR1 = .5f;
    public float capConeR2 = .2f;
    public Vector3 boxFrameSize = new Vector3(.5f, .3f, .2f);
    public float boxFrameCavity = .1f;
    public Vector2 solidAngleC = new Vector2(.25f, .25f);
    public float solidAngleRa = .5f;
    public float cutSphereR = .25f;
    public float cutSphereH = .1f;
    public float hollowSphereR = .35f;
    public float hollowSphereH = .05f;
    public float hollowSphereT = .05f;
    public float deathStarRa = .5f;
    public float deathStarRb = .35f;
    public float deathStarD = .5f;
    public float roundConeR1 = .1f;
    public float roundConeR2 = .25f;
    public float roundConeH = .4f;
    public Vector3 ellipsoidRadius = new Vector3(.18f, .3f, .1f);
    public float rhombusLa = .6f;
    public float rhombusLb = .2f;
    public float rhombusH = .02f;
    public float rhombusRa = .02f;
    public float octahedronSize = .5f;
    public float pyramidSize = .5f;
    public Vector3 triangleSideA = new Vector3(.3f, .5f, .15f);
    public Vector3 triangleSideB = new Vector3(.8f, .2f, .1f);
    public Vector3 triangleSideC = new Vector3(.7f, .3f, .5f);
    public Vector3 quadSideA = new Vector3(.3f, .5f, .15f);
    public Vector3 quadSideB = new Vector3(.8f, .2f, 0);
    public Vector3 quadSideC = new Vector3(.9f, .3f, .5f);
    public Vector3 quadSideD = new Vector3(.1f, .2f, .5f);
    public float fractalI = 10f;
    public float fractalS = 1.25f;
    public float fractalO = 2f;
    public Vector4 tesseractSize = new Vector4(.25f, .25f, .25f, .25f);
    public float hyperSphereRadius = .5f;
    public Vector2 duoCylR1R2 = new Vector2(.5f, .5f);
    public float vertCapsuleH = .5f;
    public float vertCapsuleR = .5f;
    public Vector4 fiveCellA = new Vector4(.5f, .5f, .5f, .5f);
    public float sixteenCellS = .5f;
}
