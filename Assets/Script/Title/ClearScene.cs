using UnityEngine;

public class ClearScene : MonoBehaviour
{
   public void GoTitle()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
