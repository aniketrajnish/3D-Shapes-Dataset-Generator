using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using AnotherFileBrowser.Windows;
using System.Diagnostics;

public class MenuScript : MonoBehaviour
{
    RaymarchRenderer[] rrs;
    [SerializeField] GameObject shapes;
    [SerializeField] ShapeBatch shapeBatch;
    [SerializeField] TMP_InputField max_shapes, dataset_size, save_path;
    void Start()
    {
        rrs = GetComponentsInChildren<RaymarchRenderer>();

        float rand_float = Random.Range(1, 2);
        vector12 rand_dim = new vector12(rand_float,0,0,0,0,0,0,0,0,0,0,0);

        foreach (RaymarchRenderer rr in rrs)
        {
            rr.SetDimensionArray(rr.shape, rand_dim);
            rr.color = Random.ColorHSV(0,1);
        }
    }
    public void Generate()
    {
       shapeBatch.max_shapes = int.Parse(max_shapes.text);
       shapeBatch.dataset_size = int.Parse(dataset_size.text);
       shapeBatch.save_path = save_path.text;

       shapes.SetActive(false);

       Process.Start("explorer.exe", save_path.text);
       StartCoroutine(shapeBatch.RenderShapes());
    }
    public void OpenFileBrowser()
    {/*
        string directory = EditorUtility.OpenFolderPanel("Select Directory", "", "");
        save_path.text = directory;*/
        var bp = new BrowserProperties();
        bp.title = "Choose Path";
        new FileBrowser().OpenFolderBrowser(bp, path =>
        {
            SetPath(path);
        });
    }
    public void SetPath(string path)
    {
        save_path.text = path;
    }
    public void ToggleVCA()
    {
        shapeBatch.varying_angles = !shapeBatch.varying_angles;
    }
    public void ToggleVO()
    {
        shapeBatch.varying_orientation = !shapeBatch.varying_orientation;
    }
    public void ToggleRSC()
    {
        shapeBatch.randomize_shape_count = !shapeBatch.randomize_shape_count;
    }
    public void ShapeToggle(int index)
    {
        RaymarchRenderer.Shape shape = (RaymarchRenderer.Shape)index;

        if (shapeBatch.exclude_shapes.Contains(shape))
            shapeBatch.exclude_shapes.Remove(shape);
        else
            shapeBatch.exclude_shapes.Add(shape);
    }
    public void OperationToggle(int index)
    {
        RaymarchRenderer.Operation operation = (RaymarchRenderer.Operation)index;

        if (shapeBatch.exclude_operations.Contains(operation))
            shapeBatch.exclude_operations.Remove(operation);
        else
            shapeBatch.exclude_operations.Add(operation);
    }
}
