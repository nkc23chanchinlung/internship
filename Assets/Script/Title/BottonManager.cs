using UnityEngine;
using UnityEngine.SceneManagement;


public class BottonManager : MonoBehaviour
{
   [SerializeField] TitleManager titleManager;
   
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
}
