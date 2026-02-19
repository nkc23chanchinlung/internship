using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : UIEffect
{
    [SerializeField] GameObject AccaptPanel;
    [SerializeField] GameObject settingPanel;
    
    public void Setting()
    {
        settingPanel.SetActive(true);
    }
    public void SettingExit()
    {
        settingPanel.SetActive(false); 
    }
    public void Exit()
    {
        AccaptPanel.SetActive(true);
        GameManager.Instance.enterShop = 0;//キャラの出現場所をリセット
        expansioneffect(AccaptPanel, new Vector3(0.3f, 0.3f, 0.3f));
        
    }
    public void Yes()                                     //Exitの確認YESボタン
    {
        GameManager.Instance.GameStop = false;
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
    public void No()　　　　　　　　　　　　　　　　　　　//Exitの確認NOボタン
    {
        ereductioneffect(AccaptPanel, new Vector3(0.3f, 0.3f, 0.3f));
    }

}
