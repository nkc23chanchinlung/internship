using UnityEditor;
using UnityEngine;


public partial class ProjectEditor : EditorWindow
{
    private string text = "";
    private string log = "";
    [MenuItem("Memo/Project Memo")]
    static void Init()
    {
        ProjectEditor window = (ProjectEditor)EditorWindow.GetWindow(typeof(ProjectEditor));
        window.Show();
    }

    void OnGUI()
    {
        text = EditorGUILayout.TextArea(text, GUILayout.Height(100));
        GUILayout.Label(log, EditorStyles.boldLabel);
        if (GUILayout.Button("Create"))
        {
            string[] lines = text.Split('\n');
            string path = "Assets/Editor/Memo.txt";
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true);


            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            log = sr.ReadToEnd();


        }


    }
}