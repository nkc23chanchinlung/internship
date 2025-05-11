using UnityEngine;
using UnityEngine.SceneManagement;


public class BottonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void GameExit()
    {
        Application.Quit();
    }
}
