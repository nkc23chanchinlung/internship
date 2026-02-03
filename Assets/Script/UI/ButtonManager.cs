using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : UIEffect
{
    [SerializeField] GameObject AccaptPanel;
    public void Setting()
    {

    }
    public void Exit()
    {
        AccaptPanel.SetActive(true);
        
        expansioneffect(AccaptPanel, new Vector3(0.3f, 0.3f, 0.3f));
        
    }
    public void Yes()                                     //Exitの確認YESボタン
    {
        GameManager.instance.GameStop = false;
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
    }
    public void No()　　　　　　　　　　　　　　　　　　　//Exitの確認NOボタン
    {
        ereductioneffect(AccaptPanel, new Vector3(0.3f, 0.3f, 0.3f));
    }

}
