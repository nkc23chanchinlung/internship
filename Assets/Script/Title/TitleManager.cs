using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    [SerializeField]Image titleframe;
    GameObject titleframe_Obj;
    [Header("Alpha")]
    [Tooltip("0-100")]
    [Range(0, 100)]
    [SerializeField]float alpha = 100;
    public bool isStart { get; set; } = false;
    [SerializeField]Loading loadingScript;
    bool once = false;

    [SerializeField]
    GameObject[] button = { };
    [SerializeField]
    Text[] buttontext = { };
    [SerializeField]
    Image[] buttonimg = { };
    [SerializeField]
    GameObject[] selectionimg = { };

    [SerializeField] Texture2D cursor;
    [SerializeField] AudioClip seclip;
    AudioSource seaudio;

    int butnum = 0;
    int imgsizeX = 300;
    int imgsizeY = 60;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seaudio = GetComponent<AudioSource>();
        titleframe_Obj = titleframe.gameObject;
        titleframe_Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TitleMos();
        Debug.Log(butnum);

        if (alpha >=0&&!isStart)
        {
            TitleStart_EF();

        }

        titleframe.color = new Color(0, 0, 0, alpha/100);
        if (isStart)
        {
            GameStart_EF();
        }


    }
    void GameStart_EF()
    {
        titleframe_Obj.SetActive(true);
        alpha++;
        if (alpha >= 100)
        {
            if (!once) { 
            loadingScript.NextScene("GameScene");
            once = true;
        }
        }
    }
    void TitleStart_EF()
    {

        titleframe_Obj.SetActive(true);
        alpha--;
        
        if (alpha <= 0)
        {
            alpha = 0;
            titleframe_Obj.SetActive(false);
        }
    }
  

    /// <summary>
    /// É}ÉEÉXÇ≈ëÄçÏÇ∑ÇÈÇ∆Ç´ÇÃä÷êî
    /// </summary>
    public void TitleMos()
    {
        Vector3 mousepos = Input.mousePosition;
        selectionimg[butnum].SetActive(true);
        for (int i = 0; i < button.Length; i++)
        {
            if (i != butnum) selectionimg[i].SetActive(false);
            var buttonpos = button[i].transform.position;
            if (mousepos.x > buttonpos.x - imgsizeX / 2 
                && mousepos.x < buttonpos.x + imgsizeX / 2
                && mousepos.y > buttonpos.y - imgsizeY / 2
                && mousepos.y < buttonpos.y + imgsizeY / 2)
            {

                

                if (butnum != i)
                {
                    
                    Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
                    seaudio.PlayOneShot(seclip);
                    butnum = i;
                }
                
               


            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }


        }

    }
}
