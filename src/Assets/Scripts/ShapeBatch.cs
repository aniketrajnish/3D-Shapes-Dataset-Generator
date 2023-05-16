using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class ShapeBatch : MonoBehaviour
{
    public int max_shapes, dataset_size, resolution_x, resolution_y, seed;    
    public string save_path;
    public RaymarchRenderer.Shape[] shapes;
    public List<RaymarchRenderer.Shape> exclude_shapes;
    public List<RaymarchRenderer.Operation> exclude_operations;
    public bool randomize_shape_count, varying_angles, varying_orientation, varying_position, should_seed;
    public GameObject generate_btn;

    RaymarchRenderer.Operation[] operations;
    Camera _cam;
    int batch_size = 1;    
    private void Start()
    {
        _cam = Camera.main;

        //default exclusion
        exclude_shapes.Add(RaymarchRenderer.Shape.Fractal);
        exclude_shapes.Add(RaymarchRenderer.Shape.CappedCone);
        exclude_shapes.Add(RaymarchRenderer.Shape.Tesseract);
        exclude_shapes.Add(RaymarchRenderer.Shape.Rhombus);
        exclude_shapes.Add(RaymarchRenderer.Shape.Triangle);
        exclude_shapes.Add(RaymarchRenderer.Shape.Quad);
        exclude_shapes.Add(RaymarchRenderer.Shape.CappedTorus);
        exclude_shapes.Add(RaymarchRenderer.Shape.InfCone);
        exclude_shapes.Add(RaymarchRenderer.Shape.Plane);
        exclude_shapes.Add(RaymarchRenderer.Shape.SolidAngle);
        exclude_shapes.Add(RaymarchRenderer.Shape.CutSphere);
        exclude_shapes.Add(RaymarchRenderer.Shape.DeathStar);
        exclude_shapes.Add(RaymarchRenderer.Shape.CutHollowSphere);
        exclude_shapes.Add(RaymarchRenderer.Shape.InfiniteCylinder);

        Camera.main.transform.LookAt(this.transform);
    }

    public IEnumerator RenderShapes()
    {
        if (!should_seed)
            seed = Random.Range(1,9999999);

        Random.InitState(seed);        

        string folderName = "dataset_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        save_path = Path.Combine(save_path, folderName);
        Directory.CreateDirectory(save_path);
        string image_path = Path.Combine(save_path, "images");
        Directory.CreateDirectory(image_path);
        System.Diagnostics.Process.Start("explorer.exe", image_path);

        string randomValuesPath = Path.Combine(save_path, "random_seed.txt");
        StreamWriter randomValuesWriter = new StreamWriter(randomValuesPath);
        randomValuesWriter.WriteLine("Seed: " + seed);
        randomValuesWriter.Close();

        string csvPath = Path.Combine(save_path, "dataset.csv");
        StreamWriter csvWriter = new StreamWriter(csvPath);

        csvWriter.WriteLine("filename,shape,operation,a,b,c,d,e,f,g,h,i,j,k,l,hue,sat,val,rot_x,rot_y,rot_z,pos_x,pos_y,pos_z");

        generate_btn.GetComponent<Button>().enabled = false;

        RenderTexture rt_zero = RenderTexture.GetTemporary(resolution_x, resolution_y, 24);
        List<string> file_names_zero = new List<string>();
        file_names_zero.Add("shape_0.png");
        yield return StartCoroutine(SaveTexturesToFile(rt_zero, image_path, file_names_zero));
        RenderTexture.ReleaseTemporary(rt_zero);
        file_names_zero.Clear();

        for (int i = 0; i < dataset_size; i += batch_size)
        {
            int shape_count = 0;

            if (randomize_shape_count)
                shape_count = Random.Range(1, max_shapes + 1);
            else
                shape_count = max_shapes;

            shapes = (RaymarchRenderer.Shape[])System.Enum.GetValues(typeof(RaymarchRenderer.Shape));            

            operations = (RaymarchRenderer.Operation[])System.Enum.GetValues(typeof(RaymarchRenderer.Operation));

            RenderTexture rt = RenderTexture.GetTemporary(resolution_x, resolution_y, 24);
            List<string> file_names = new List<string>();

            string out_name = "";

            for (int j = 0; j < shape_count; j++)
            {
                RaymarchRenderer.Shape shape;
                RaymarchRenderer.Operation operation;

                do                
                    shape = shapes[Random.Range(0, shapes.Length)];
                while (exclude_shapes.Contains(shape));
                
                do
                    operation = operations[Random.Range(0, operations.Length)];
                while (exclude_operations.Contains(operation));                

                GameObject go = new GameObject();

                if (varying_orientation)
                    go.transform.rotation = Random.rotation;

                if (varying_position)
                {
                    float rand_float = Random.Range(-1f, 1f);
                    Vector3 rand_pos = new Vector3(rand_float, rand_float, rand_float);
                    go.transform.position = rand_pos;
                }

                /*if (varying_angles)
                    _cam.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));*/

                go.AddComponent<RaymarchRenderer>();

                go.GetComponent<RaymarchRenderer>().shape = shape;
                go.GetComponent<RaymarchRenderer>().operation = operation;

                vector12 dimensions = GetRandomDimensions(shape);
                go.GetComponent<RaymarchRenderer>().SetDimensionArray(shape, dimensions);

                Color col = Random.ColorHSV(0, 1, 1, 1, 1, 1);
                go.GetComponent<RaymarchRenderer>().color = col;
                Color.RGBToHSV(col, out float H, out float S, out float V);                

                csvWriter.WriteLine("shape_" + i + ".png" + "," + (int)shape + "," + (int)operation + "," +
                dimensions.a + "," + dimensions.b + "," + dimensions.c + "," + dimensions.d + "," +
                dimensions.e + "," + dimensions.f + "," + dimensions.g + "," + dimensions.h + "," +
                dimensions.i + "," + dimensions.j + "," + dimensions.k + "," + dimensions.l + "," +
                H + "," + S + "," + V + "," +
                go.transform.rotation.eulerAngles.x + "," + go.transform.rotation.eulerAngles.y + "," + go.transform.rotation.eulerAngles.z + "," +
                go.transform.position.x + "," + go.transform.position.y + "," + go.transform.position.z);

                RenderToTexture(go, rt);
                Destroy(go);    
            }

            float progress_percent_float = (float)i / dataset_size * 100;
            int progress_percent = (int)System.Math.Round(progress_percent_float, System.MidpointRounding.AwayFromZero);
            string progress_display = progress_percent.ToString() + " %";
            generate_btn.GetComponentInChildren<TextMeshProUGUI>().text = progress_display;

            file_names.Add("shape_" + i + out_name + ".png");
            yield return StartCoroutine(SaveTexturesToFile(rt, image_path, file_names));
            RenderTexture.ReleaseTemporary(rt);
            file_names.Clear();
        }

        csvWriter.Close();
        generate_btn.GetComponentInChildren<TextMeshProUGUI>().text = "Generated!";
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    vector12 GetRandomDimensions(RaymarchRenderer.Shape shape)
    {
        vector12 rand_dimensions = new vector12();        

        switch (shape)
        {
            case RaymarchRenderer.Shape.Cylinder:
                rand_dimensions.a = Random.Range(1f, 3.5f);
                rand_dimensions.b = Random.Range(1f, 3.5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CappedCone:
                rand_dimensions.a = Random.Range(1f, 3f);
                rand_dimensions.b = Random.Range(1f, 5f);
                rand_dimensions.c = Random.Range(1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Frustrum:
                rand_dimensions.a = Random.Range(1f, 3f);
                rand_dimensions.b = Random.Range(1f, 5f);
                rand_dimensions.c = Random.Range(1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Shpere:
                rand_dimensions.a = Random.Range(2f, 4f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Torus:
                rand_dimensions.a = Random.Range(1f, 4f);
                rand_dimensions.b = Random.Range(.1f, 1.5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CappedTorus:
                rand_dimensions.a = Random.Range(.1f, 3f);
                rand_dimensions.b = Random.Range(.1f, 3f);
                rand_dimensions.c = Random.Range(.1f, 5f);
                rand_dimensions.d = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Link:
                rand_dimensions.a = Random.Range(1f, 2f);
                rand_dimensions.b = Random.Range(1f, 3f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Cone:
                rand_dimensions.a = Random.Range(.1f, .2f);
                rand_dimensions.b = Random.Range(.1f, .2f);
                rand_dimensions.c = Random.Range(2f, 4f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.InfCone:
                rand_dimensions.a = Random.Range(-1f, 1f);
                rand_dimensions.b = Random.Range(-1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Plane:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                rand_dimensions.d = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.HexPrism:
                rand_dimensions.a = Random.Range(2f, 5f);
                rand_dimensions.b = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.TriPrism:
                rand_dimensions.a = Random.Range(2f, 5f);
                rand_dimensions.b = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Capsule:
                rand_dimensions.a = 0;
                rand_dimensions.b = Random.Range(0, 3f);
                rand_dimensions.c = 0;
                rand_dimensions.d = 0;
                rand_dimensions.e = Random.Range(-3f, 0);
                rand_dimensions.f = 0;
                rand_dimensions.g = Random.Range(1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.InfiniteCylinder:
                rand_dimensions.a = 0;
                rand_dimensions.b = 0;
                rand_dimensions.c = Random.Range(.1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Box:
                rand_dimensions.a = Random.Range(1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.RoundBox:
                rand_dimensions.a = Random.Range(1f, 2.5f);
                rand_dimensions.b = Random.Range(.1f, 2f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.RoundedCylinder:
                rand_dimensions.a = Random.Range(1f, 2.5f);
                rand_dimensions.b = Random.Range(.1f, 2f);
                rand_dimensions.c = Random.Range(1f, 2.5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.BoxFrame:
                rand_dimensions.a = Random.Range(1f, 4f);
                rand_dimensions.b = Random.Range(1f, 4f);
                rand_dimensions.c = Random.Range(1f, 4f);
                rand_dimensions.d = Random.Range(.1f, .75f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.SolidAngle:
                rand_dimensions.a = Random.Range(-1f, 1f);
                rand_dimensions.b = Random.Range(-1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CutSphere:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.CutHollowSphere:
                rand_dimensions.a = Random.Range(.1f, 5f);
                rand_dimensions.b = Random.Range(.1f, 4f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.DeathStar:
                rand_dimensions.a = Random.Range(.1f, 10f);
                rand_dimensions.b = Random.Range(.1f, 10f);
                rand_dimensions.c = Random.Range(.1f, 10f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.RoundCone:
                rand_dimensions.a = Random.Range(.5f, 3f);
                rand_dimensions.b = Random.Range(.5f, 3f);
                rand_dimensions.c = Random.Range(3f, 6f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Ellipsoid:
                rand_dimensions.a = Random.Range(2f, 5f);
                rand_dimensions.b = Random.Range(2f, 5f);
                rand_dimensions.c = Random.Range(2f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Rhombus:
                rand_dimensions.a = Random.Range(1f, 3f);
                rand_dimensions.b = Random.Range(1f, 3f);
                rand_dimensions.c = Random.Range(1f, 3f);
                rand_dimensions.d = Random.Range(1f, 3f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Octahedron:
                rand_dimensions.a = Random.Range(2f, 5f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Pyramid:
                rand_dimensions.a = Random.Range(3f, 12f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Triangle:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                rand_dimensions.d = Random.Range(.1f, 1f);
                rand_dimensions.e = Random.Range(.1f, 1f);
                rand_dimensions.f = Random.Range(.1f, 1f);
                rand_dimensions.g = Random.Range(.1f, 1f);
                rand_dimensions.h = Random.Range(.1f, 1f);
                rand_dimensions.i = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Quad:
                rand_dimensions.a = Random.Range(-10f, 10f);
                rand_dimensions.b = Random.Range(-10f, 10f);
                rand_dimensions.c = Random.Range(-10f, 10f);
                rand_dimensions.d = Random.Range(-10f, 10f);
                rand_dimensions.e = Random.Range(-10f, 10f);
                rand_dimensions.f = Random.Range(-10f, 10f);
                rand_dimensions.g = Random.Range(-10f, 10f);
                rand_dimensions.h = Random.Range(-10f, 10f);
                rand_dimensions.i = Random.Range(-10f, 10f);
                rand_dimensions.j = Random.Range(-10f, 10f);
                rand_dimensions.k = Random.Range(-10f, 10f);
                rand_dimensions.l = Random.Range(-10f, 10f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Fractal:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

            case RaymarchRenderer.Shape.Tesseract:
                rand_dimensions.a = Random.Range(.1f, 1f);
                rand_dimensions.b = Random.Range(.1f, 1f);
                rand_dimensions.c = Random.Range(.1f, 1f);
                return rand_dimensions;

        }

        return rand_dimensions;
    }
    private RenderTexture RenderToTexture(GameObject go, RenderTexture rt)
    {
        RenderTexture previousTarget = _cam.targetTexture;
        _cam.targetTexture = rt;
        _cam.Render();  
        _cam.targetTexture = previousTarget;

        return rt;
    }
    private IEnumerator SaveTexturesToFile(RenderTexture rt, string savePath, List<string> fileNames)
    {

        for (int i = 0; i < fileNames.Count; i++)
        {
            Texture2D texture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

            RenderTexture.active = rt;
            texture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            RenderTexture.active = null;

            byte[] bytes = texture.EncodeToPNG();

            string filePath = System.IO.Path.Combine(savePath, fileNames[i]);
            File.WriteAllBytes(filePath, bytes);

            Destroy(texture);
            yield return null;
        }
    }
}
