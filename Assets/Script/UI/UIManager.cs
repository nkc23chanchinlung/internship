using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject StoryPanel;
    [SerializeField] GameObject Lead;
    [SerializeField] GameObject Damagevalueprefeb;
    
    [Header("Image")]
    [SerializeField] Image Lifebar;
    [SerializeField] Image House_Hpbar;
    [SerializeField] Image Fade;


    [Header("Text")]
    [SerializeField] Text Reloading_text;
    [SerializeField] EquipSystem equipSystem;
    Text Magazine_Text;
    Image Magazine_Image;
    PlayerController playerController;
    House house;
    bool PanelOpen = false;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        playerController =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        house =GameObject.Find("House").GetComponent<House>();
        StorePanel.SetActive(false);
        blinkinge_effect(Reloading_text);
       


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

        if (WeaponPanel.activeSelf||StorePanel.activeSelf)　　　　　　　　　　　　　　　　　　　　 //パネルが開いているか
        {
            PanelOpen = true;
        }
        else PanelOpen = false;

        Hpbar();
        HouseHpbar();

        if (Lead.activeSelf&&Input.GetKeyDown(KeyCode.T))
        {
            Lead.SetActive(false);
        }
        else if (!Lead.activeSelf && Input.GetKeyDown(KeyCode.T))
        {
            Lead.SetActive(true);
        }
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
    public void SetMagazine(int Magazine,int MaxMagazine)
    {
       Magazine_Text.text=Magazine.ToString() + "/" + MaxMagazine.ToString();
        Magazine_Image = GameObject.Find("magazinebar").GetComponent<Image>();
        Magazine_Image.fillAmount = (float)Magazine / (float)MaxMagazine;
    }
    public void memo()
    {
        StorePanel.SetActive(true);
    }
    void Hpbar()
    {
        Lifebar.fillAmount = (float)playerController.Hp / (float)playerController.MaxHp;
    }
    void HouseHpbar()
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
    public void FadeControl(string nextscene)
    {
        turnblack(Fade,nextscene);
    }
}
