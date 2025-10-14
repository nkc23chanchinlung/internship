using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UIを管理するクラス
/// </summary>
[System.Serializable]
public class UIManager :UIEffect
{
    

    [Header("UI")]
    [Header("GameObject")]
    [SerializeField] GameObject GameCanvas;
    [SerializeField] GameObject StorePanel;
    [SerializeField] GameObject WeaponPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject StoryPanel;
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject Lead;
    [SerializeField] GameObject Damagevalueprefeb;
    [SerializeField] GameObject Coinprefeb;
   
    
    [Header("Image")]
    [SerializeField] Image House_Hpbar;
    [SerializeField] Image Fade;
    [SerializeField]Image Lifebar;


    [Header("Text")]
    [SerializeField] Text Reloading_text;
    [SerializeField] EquipSystem equipSystem;
    [SerializeField] Text Magazine_Text;
    [SerializeField] Text Coin_Text;
    [SerializeField] Text Lifebar_Text;
    Image Magazine_Image;
    [SerializeField]PlayerController playerController;
    House house;
    bool PanelOpen = false;
    float displaylife=100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        playerController =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        try
        {
            house = GameObject.Find("House").GetComponent<House>();
        }
        catch
        {
            house = null;
        }
        StorePanel.SetActive(false);
        blinkinge_effect(Reloading_text);
        MapPanel.transform.localScale = Vector3.zero;
        MenuPanel.transform.localScale = Vector3.zero;

        hideeffect(Fade, 1f);  　　　　　　　　　　　　　//Fade処理
        


    }

    // Update is called once per frame
    void Update()
    {
       
        playerController.IsCreate = PanelOpen;

        if (Input.GetKeyDown(KeyCode.B)&&!PanelOpen)
        {

            Panel_Open(StorePanel);

            if (WeaponPanel.activeSelf)
            {
                WeaponPanel.SetActive(false);
            }
        }
        else if (PanelOpen && Input.GetKeyDown(KeyCode.B))
        {
            StorePanel.SetActive(false);
            WeaponPanel.SetActive(false);
        }

        if (WeaponPanel.activeSelf||StorePanel.activeSelf)　　　　　　　　　　　　　　　　　　　　              //パネルが開いているか
        {
            PanelOpen = true;
        }
        else PanelOpen = false;

        Hpbar();
        if (house != null)                                                                                       //家のHPバーがあるか
            HouseHpbar();


        PanelOpen = PanelControll(Lead, KeyCode.T, new Vector3(0.02f, 0.02f, 0.02f));                            //リードのパネルを開くか閉じるか
        PanelOpen = PanelControll(MapPanel, KeyCode.Tab, new Vector3(0.5f, 0.7f, 0.7f));                         //マップのパネルを開くか閉じるか
        GameManager.GameStop= PanelControll(MenuPanel, KeyCode.Escape, new Vector3(0.3f, 0.5f, 0.5f));           //メニューのパネルを開くか閉じるか
       
        SetCoin(GameManager.Coin);                                                                 //コインの数を更新                                 




        if (equipSystem.IsReloading)
        {
            Show_Reloading_text();
        }
        else
        {
            Reloading_text.gameObject.SetActive(false);
            
        }

    }
   public void SearchMagazine()
    {
        Magazine_Text = GameObject.Find("magazine").GetComponent<Text>();
    }
   
    /// <summary>
    /// パネルを開くと消す
    /// </summary>
    /// <param name="panel">パネル</param>
    public void Panel_Open(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    /// <summary>
    /// マガジンのUI
    /// </summary>
    /// <param name="Magazine">マガジン</param>
    /// <param name="MaxMagazine">最大のマガジン</param>
    public void SetMagazine(int Magazine,int MaxMagazine)
    {
       Magazine_Text.text=Magazine.ToString() + "/" + MaxMagazine.ToString();
        //Magazine_Image = GameObject.Find("magazinebar").GetComponent<Image>();
        //Magazine_Image.fillAmount = (float)Magazine / (float)MaxMagazine;
    }
    public void memo()
    {
        StorePanel.SetActive(true);
    }
    void Hpbar( )                                                                          //プレイヤーのHPバー
    {
       
        Lifebar.fillAmount = (float)playerController.Hp / (float)playerController.MaxHp;
        var life = ((float)playerController.Hp / (float)playerController.MaxHp)*100;
        if (displaylife != life) displaylife--;
            Lifebar_Text.text = displaylife.ToString("")+"%";
    }
    void HouseHpbar()                                                                     //家のHPバー
    {
       House_Hpbar.fillAmount = (float)house.Hp / (float)house.MaxHp;
    }
    void Show_Reloading_text()
    {
        Reloading_text.gameObject.SetActive(true);
        Reloading_text.text = "Reloading...";
        
    }
    public void Damagevalue( Transform obj,int damage)　　　　　　　　　　　　　　　　　　　　//ダメージ表記
    {
        Text Damage_text = Damagevalueprefeb.GetComponent<Text>();
        Damage_text.text = damage.ToString();
        var center = 0.5f * new Vector3(Screen.width, Screen.height);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.position)-center;

        GameObject damageInstance = Instantiate(
        Damagevalueprefeb,
        screenPos,
        Quaternion.identity
        );
       
        damageInstance.transform.SetParent(GameCanvas.transform, false);
        DamageEffect(damageInstance);
        
    }
    /// <summary>
    /// ドロップしたコインの処理
    /// </summary>
    /// <param name="obj"></param>
    public void Coin(Transform obj)　　　　　　　　　　　　　　　　　　　　//コイン
    {
        var center = 0.5f * new Vector3(Screen.width, Screen.height);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.position) - center;

        GameObject CoinInstance = Instantiate(
        Coinprefeb,
        screenPos,
        Quaternion.identity
        );

       //コイン生成した処理
        CoinInstance.transform.SetParent(GameCanvas.transform, false);
        AudioSource audioSource =CoinInstance.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
        DamageEffect(CoinInstance);
        Transform CoinUI = GameObject.Find("Coin_UI").transform;
        CoinInstance.transform.DOMove(CoinUI.position, 1f).SetDelay(1f).OnComplete(() =>
        {
            Destroy(CoinInstance);
            GameManager.Coin += 1; //コインを増やす

        });

    }

    public void SetCoin(int coin)　　　　　　　　　　　　　　　　　　　　//コインの数
    {
        Coin_Text.text = coin.ToString("D2");
    }
    public void FadeControl(string nextscene)
    {
        displayeffect(Fade,nextscene,0.5f);
    }
    /// <summary>
    /// パネルの表示と非表示を制御するメソッド
    /// </summary>
    /// <param name="Obj">対象のパネル</param>
    /// <param name="key">表示するボタン</param>
    bool PanelControll(GameObject Obj,KeyCode key,Vector3 size)
    {
         if(Obj.activeSelf&& Input.GetKeyDown(key))
        {
            ereductioneffect(Obj, size);
        }
        else if(!Obj.activeSelf && Input.GetKeyDown(key))
        {
            Obj.SetActive(true);
            expansioneffect(Obj, size);
        }
        return Obj.activeSelf;    //パネルが開いているかどうかを返す

    }
   
}
