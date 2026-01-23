using UnityEngine;
using UnityEngine.SceneManagement;
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

    int butnum = 1;
    int imgsize = 250;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            loadingScript.NextScene();
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
        for (int i = 0; i < button.Length; i++)
        {

            var buttonpos = button[i].transform.position;
            if (mousepos.x > buttonpos.x - imgsize / 2 && mousepos.x < buttonpos.x + imgsize / 2 && mousepos.y > buttonpos.y - imgsize / 2f && mousepos.y < buttonpos.y + imgsize / 2)
            {

                if (butnum != i + 1)
                {
                    //seaudio.PlayOneShot(seclip);
                    butnum = i + 1;
                }


            }
        }

    }
}
