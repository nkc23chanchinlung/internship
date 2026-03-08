using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class SaveManager : MonoBehaviour
{
    static public SaveManager instance;
    string savePath;
    string _fileName="Data.json";
    [SerializeField] Text text;



    void Awake()
    {
        savePath = Application.dataPath + "/" + _fileName;
        if (!File.Exists(savePath))
        {
            Save("1");
        }
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            Save("1");
            Debug.Log("Save");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //text.text=Load("savePath").ToString();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Save(string data)
    {
        
            string json = JsonUtility.ToJson(data);
            StreamWriter writer = new StreamWriter(savePath, false);
            writer.Write(json);
            writer.Close();
        
    }

    //string Load(string path)
    //{
    //    StreamReader rd = new StreamReader(path);               // ファイル読み込み指定
    //    string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
    //    rd.Close();                                             // ファイル閉じる

    //    return JsonUtility.FromJson<DataManager>(json);            // jsonファイルを型に戻して返す
    //}



}
