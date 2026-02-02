using UnityEngine;
using UnityEngine.SceneManagement;


public class BottonManager : MonoBehaviour
{
   [SerializeField] TitleManager titleManager;
    [SerializeField]UIEffect uIEffect;
    [SerializeField] GameObject SettingPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GameStart()
    {
       Debug.Log("GameStart");
        titleManager.isStart = true;
        
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void Setting()
    {
        SettingPanel.SetActive(true);
        uIEffect.expansioneffect(SettingPanel, new Vector3(1, 1, 1));
    }
    public void SettingClose()
    {
        if(SettingPanel.activeSelf)
        {
            uIEffect.ereductioneffect(SettingPanel, new Vector3(1, 1, 1));
        }
    }
}
