using Sydewa;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    float Gametime;
    public int Day { get; set; } = 1;
    [SerializeField]Text Day_Text;
    [SerializeField] Text DayMessage_Text;
    int CurrentDay;
    [SerializeField] GameObject DayMessage;
    Color color;
    float Timer= 0f;
    //チュートリアル
    [SerializeField] Image Tr_move;
    [SerializeField] Image Tr_shoot;
    [SerializeField] Image Tr_reload;
    [SerializeField] Image Tr_create;
    [SerializeField] Text Tr_move_text;
    [SerializeField] Text Tr_shoot_text;
    [SerializeField] Text Tr_reload_text;
    [SerializeField] Text Tr_create_text;
    bool onlyonce = true;
    int layer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentDay = Day;
        color = DayMessage_Text.color;

        color.a = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        Gametime += Time.deltaTime;
        Day_Text.text = "Day" + Day.ToString();
        tutorial();
        ShowDayMessage();
        if (Gametime >= 2f && onlyonce)
        {
            layer = 1;
            onlyonce = false;
        }




        }
    void ShowDayMessage()
    {
        DayMessage_Text.color = color;
        if (CurrentDay != Day)
        {
            DayMessage_Text.text = "Day" + Day.ToString();
            DayMessage.SetActive(true);
            Timer += Time.deltaTime;
            Debug.Log(Timer);
            color.a += 0.1f;
            if (Timer >= 2f)
            {
                CurrentDay = Day;
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
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    Tr_move.gameObject.SetActive(false);
                    Tr_move_text.gameObject.SetActive(false);
                    layer = 2;
                }
                break;
            case 2:
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
                Tr_reload.gameObject.SetActive(true);
                Tr_reload_text.gameObject.SetActive(true);
                Tr_reload.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                Tr_reload_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Tr_reload.gameObject.SetActive(false);
                    Tr_reload_text.gameObject.SetActive(false);
                    layer = 4;
                }
                break;
            case 4:
                Tr_create.gameObject.SetActive(true);
                Tr_create_text.gameObject.SetActive(true);
                Tr_create.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                Tr_create_text.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
                if (Input.GetKeyDown(KeyCode.B))
                {
                    Tr_create.gameObject.SetActive(false);
                    Tr_create_text.gameObject.SetActive(false);
                    layer = 5;
                }
                break;
        }
        


    }
}
