
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//追加コンテンツのイベント管理
public class EventManager : MonoBehaviour
{
    float Gametime;
    GameManager gameManager;
    bool gamestarted = false;
    int Stage;
    [SerializeField]Text Day_Text;
    [SerializeField] Text DayMessage_Text;
    int CurrentDay;
    [SerializeField] GameObject DayMessage;
    Color color;
    float Timer= 0f;
    //チュートリアル
    [SerializeField]
    Dictionary<Text, Image> image_dic = new Dictionary<Text,Image>();
    [SerializeField]
    Image[] Tr_Imagesarray;
    [SerializeField]
    Text[] Tr_Textarray;
    [SerializeField] Image Tr_move;
    [SerializeField] Image Tr_shoot;
    [SerializeField] Image Tr_reload;
    [SerializeField] Image Tr_rolling;
    [SerializeField] Text Tr_move_text;
    [SerializeField] Text Tr_shoot_text;
    [SerializeField] Text Tr_reload_text;
    [SerializeField] Text Tr_rolling_text;
    [SerializeField] Image Tr_change;
    [SerializeField] Text Tr_change_text;
    bool onlyonce = true;
    int layer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Stage = gameManager.Stage;
        DayMessage.SetActive(false);
        foreach (Image image in Tr_Imagesarray)
        {
            image_dic.Add(Tr_Textarray[0], image);
        }
        
    }
    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
    }
    void OnGameStart()
    {
        gamestarted = true;
    }

    void Start()
    {
        CurrentDay = Stage;
        color = DayMessage_Text.color;

        color.a = 0f;

     foreach(Image image in Tr_Imagesarray){
       
           
        }
     
      
      
       

    }

    // Update is called once per frame
    void Update()
    {
        if (!gamestarted) return;
        Gametime += Time.deltaTime;
        Day_Text.text = "Stage" + Stage.ToString();
        tutorial();
        ShowDayMessage();
        if ((int)Gametime%10==0&&layer<=6&&onlyonce)
        {
            layer ++;
            onlyonce = false;

        }
        else if ((int)Gametime % 10 != 0)
        
            onlyonce = true;




        }
    void ShowDayMessage()
    {
        DayMessage_Text.color = color;
        if (CurrentDay != Stage)
        {
            DayMessage_Text.text = "Stage" + Stage.ToString();
            DayMessage.SetActive(true);
            Timer += Time.deltaTime;
            
            color.a += 0.1f;
            if (Timer >= 2f)
            {
                CurrentDay = Stage;
                Timer = 0f;
            }
        }
        else
        {
            color.a -= 0.1f;
            if(color.a <= 0f)
            {
                DayMessage.SetActive(false);
                color.a = 0f;
            }
        }
    }
    void tutorial()
    {

        switch (layer)
        {


        
            case 1:
                Tr_move.gameObject.SetActive(true);
                Tr_move_text.gameObject.SetActive(true);
                Tr_move.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                Tr_move_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                if (Input.GetKeyDown(KeyCode.W) || 
                    Input.GetKeyDown(KeyCode.A) ||
                    Input.GetKeyDown(KeyCode.S) || 
                    Input.GetKeyDown(KeyCode.D))
                {
                    Tr_move.gameObject.SetActive(false);
                    Tr_move_text.gameObject.SetActive(false);
                    layer = 2;
                }
                break;
            case 2:
                Tr_move.gameObject.SetActive(false);
                Tr_move_text.gameObject.SetActive(false);
                Tr_shoot.gameObject.SetActive(true);
                Tr_shoot_text.gameObject.SetActive(true);
                Tr_shoot.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                Tr_shoot_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                if (Input.GetMouseButtonDown(0))
                {
                    Tr_shoot.gameObject.SetActive(false);
                    Tr_shoot_text.gameObject.SetActive(false);
                    layer = 3;
                }
                break;
            case 3:
                Tr_shoot.gameObject.SetActive(false);
                Tr_shoot_text.gameObject.SetActive(false);
                Tr_change.gameObject.SetActive(true);
                Tr_change_text.gameObject.SetActive(true);
                Tr_change.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                Tr_change_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Tr_change.gameObject.SetActive(false);
                    Tr_change_text.gameObject.SetActive(false);
                    layer = 4;
                }
                break;
            case 4:
                Tr_change.gameObject.SetActive(false);
                Tr_change_text.gameObject.SetActive(false);
                Tr_reload.gameObject.SetActive(true);
                Tr_reload_text.gameObject.SetActive(true);
                Tr_reload.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                Tr_reload_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Tr_reload.gameObject.SetActive(false);
                    Tr_reload_text.gameObject.SetActive(false);
                    layer = 5;
                }
                break;
            case 5:
                Tr_reload.gameObject.SetActive(false);
                Tr_reload_text.gameObject.SetActive(false);
                Tr_rolling.gameObject.SetActive(true);
                Tr_rolling_text.gameObject.SetActive(true);
                Tr_rolling.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                Tr_rolling_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Tr_rolling.gameObject.SetActive(false);
                    Tr_rolling_text.gameObject.SetActive(false);
                    layer = 6;
                }
                break;
            case 6:
                Tr_rolling.gameObject.SetActive(false);
                Tr_rolling_text.gameObject.SetActive(false);
                layer = 7;
                break;


            }
        }
    void tutorialshow(Image image, Text text)
    {
        image.gameObject.SetActive(true);
        Tr_move_text.gameObject.SetActive(true);
        Tr_move.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
        Tr_move_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));

    }
}

          



    

