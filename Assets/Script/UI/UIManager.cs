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
    /// �p�l�����J���Ə���
    /// </summary>
    /// <param name="panel">�p�l��</param>
    public void Panel_Open(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}
