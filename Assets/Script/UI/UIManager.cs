using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StorePanel;
    Text Magazine_Text;
    Image Magazine_Image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UIManager()
    {
        Magazine_Text = GameObject.Find("magazine").GetComponent<Text>();
    }
    void Start()
    {
        StorePanel.SetActive(false);
        Magazine_Text = GameObject.Find("magazine").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) Panel_Open(StorePanel);
    }
   
    /// <summary>
    /// パネルを開くと消す
    /// </summary>
    /// <param name="panel">パネル</param>
    public void Panel_Open(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    public void Magazine_UI(int Magazine,int MaxMagazine)
    {
       Magazine_Text.text=Magazine.ToString() + "/" + MaxMagazine.ToString();
        Magazine_Image = GameObject.Find("magazinebar").GetComponent<Image>();
        Magazine_Image.fillAmount = (float)Magazine / (float)MaxMagazine;
    }
}
