using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
//âπê∫ä÷òAä«óùÉNÉâÉX
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField]public AudioSource BGMPlayer { get;private set; }
    [SerializeField]public AudioSource SEPlayer { get;private set; }


    public float BgmVolue { get; set; }
    public float SeVolue { get; set; }

    
    [SerializeField] AudioSource[] TitleSEPlayerarray = { };
    [SerializeField] AudioSource[] GaneSEPlayerarray = { };

    List<AudioSource> SEPlayerlist = new List<AudioSource>();

    int SceneNum;


    [SerializeField] AudioClip[] BGMCliplist = { };
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        CheakGameManagerExist();
        TitleSEPlayerarray = Object
    .FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

//        SEPlayerlarray = Object
//.FindObjectsByType<AudioSource>(FindObjectsSortMode.None)
//.Where(a => a.gameObject.tag != "BGMPlayer")
//.ToArray();
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    
    private void FixedUpdate()
    {
       
        VolueControll();
        
 



        //SEPlayer = GameObject.FindGameObjectWithTag("SEPlayer").GetComponent<AudioSource>();
    }
    public void PlayBGM(AudioClip Clip)
    {
        BGMPlayer = GameObject.FindGameObjectWithTag("BGMPlayer").GetComponent<AudioSource>();
        BGMPlayer = GameObject
    .FindGameObjectWithTag("BGMPlayer")
    ?.GetComponent<AudioSource>();

    }
    public void AddSE(AudioClip Clip)
    {
        SEPlayer = GameObject.FindGameObjectWithTag("SEPlayer").GetComponent<AudioSource>();
        
        SEPlayer.PlayOneShot(Clip);
    }
   
   private void CheakGameManagerExist()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    //    void BGMChanger()
    //    {
    //        if(SceneManager.GetActiveScene().name=="TitleScene"&&SceneNum!=1)
    //        {
    //            Debug.Log("BGMChange");
    //            SEPlayerarray = Object
    //.FindObjectsByType<AudioSource>(FindObjectsSortMode.None)
    //.Where(a => a.gameObject.tag != "BGMPlayer")
    //.ToArray();
    //            SceneNum = 1;
    //            SEPlayerlist.Clear();

    //            BGMPlayer.clip = BGMCliplist[0];
    //            SEPlayerlist.AddRange(SEPlayerarray);
    //        }
    //        else if (SceneManager.GetActiveScene().name == "GameScene" && SceneNum != 2)
    //        {
    //            Debug.Log("BGMChange");
    //            SEPlayerarray = Object
    //.FindObjectsByType<AudioSource>(FindObjectsSortMode.None)
    //.Where(a => a.gameObject.tag != "BGMPlayer")
    //.ToArray();
    //            SceneNum = 2;
    //            SEPlayerlist.Clear();
    //            BGMPlayer.clip = BGMCliplist[1];
    //            SEPlayerlist.AddRange(SEPlayerarray);
    //        }
    //    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("BGMChange");

        TitleSEPlayerarray = Object
     .FindObjectsByType<AudioSource>(FindObjectsSortMode.None)
     .Where(a =>
         a.gameObject.tag != "BGMPlayer" &&
         a.gameObject.scene == scene
     )
     .ToArray();


        SEPlayerlist.Clear();
       

        if (scene.name == "TitleScene")
        {
            TitleSEPlayerarray = Object
    .FindObjectsByType<AudioSource>(FindObjectsSortMode.None)
    .Where(a => a.gameObject.tag != "BGMPlayer")
    .ToArray();
            SceneNum = 1;
            SEPlayerlist.AddRange(TitleSEPlayerarray);
            BGMPlayer.clip = BGMCliplist[0];

        }
        else if (scene.name == "GameScene")
        {
            GaneSEPlayerarray = Object
    .FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            SceneNum = 2;
            SEPlayerlist.AddRange(GaneSEPlayerarray);
            BGMPlayer.clip = BGMCliplist[1];
        }
    }
    void VolueControll()
    {
       
            BGMPlayer.volume = BgmVolue;
        
        for (int i = 0; i < SEPlayerlist.Count; i++)
        {
           

            SEPlayerlist[i].volume = SeVolue;
        }
    }


}
