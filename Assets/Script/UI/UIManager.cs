using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StorePanel;
    [SerializeField] GameObject WeaponPanel;
    Text Magazine_Text;
    Image Magazine_Image;
    PlayerController playerController;
    bool PanelOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
}
