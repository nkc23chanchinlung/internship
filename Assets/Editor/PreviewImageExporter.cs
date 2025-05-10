using UnityEditor;
using UnityEngine;
using System.IO;

public class PreviewImageExporter : EditorWindow
{
    private Object targetObject;
    private string savePath = "Assets/PreviewImages";

    [MenuItem("Tools/Preview Image Exporter")]
    public static void ShowWindow()
    {
        GetWindow<PreviewImageExporter>("Preview Image Exporter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Export Object Preview as Image", EditorStyles.boldLabel);
        targetObject = EditorGUILayout.ObjectField("Target Object", targetObject, typeof(Object), true);
        savePath = EditorGUILayout.TextField("Save Path", savePath);

        if (GUILayout.Button("Export Preview") && targetObject != null)
        {
            ExportPreviewImage();
        }
    }

    private void ExportPreviewImage()
    {
        Texture2D previewTexture = AssetPreview.GetAssetPreview(targetObject);

        if (previewTexture == null)
        {
            Debug.LogError("Preview not available for this object.");
            return;
        }

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        byte[] bytes = previewTexture.EncodeToPNG();
        string filePath = Path.Combine(savePath, targetObject.name + "_Preview.png");

        File.WriteAllBytes(filePath, bytes);
        AssetDatabase.Refresh();

        Debug.Log("Preview image exported to: " + filePath);
    }
}
