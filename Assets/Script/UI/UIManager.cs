using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]GameObject StorePanel;
    [SerializeField] GameObject WeaponPanel;
    [SerializeField] GameObject StoryPanel;
    [SerializeField] GameObject Lead;
    [SerializeField] Image Lifebar;
    [SerializeField] Image House_Hpbar;
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
        if (WeaponPanel.activeSelf||StorePanel.activeSelf) //パネルが開いているか
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





    }
   public void SearchMagazine()
    {
        Magazine_Text = UnityEngine.GameObject.Find("magazine").GetComponent<Text>();
    }
   
    /// <summary>
    /// パネルを開くと消す
    /// </summary>
    /// <param name="panel">パネル</param>
    public void Panel_Open(UnityEngine.GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    public void SetMagazine(int Magazine,int MaxMagazine)
    {
       Magazine_Text.text=Magazine.ToString() + "/" + MaxMagazine.ToString();
        Magazine_Image = UnityEngine.GameObject.Find("magazinebar").GetComponent<Image>();
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
}
