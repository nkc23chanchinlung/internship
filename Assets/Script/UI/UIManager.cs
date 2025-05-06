using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StorePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StorePanel.SetActive(false);
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
}
