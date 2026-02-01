using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Loading : MonoBehaviour
{
    AsyncOperation async;
    [SerializeField]GameObject loadingUI;
    [SerializeField] Image loadingbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    public void NextScene(string SceneName)
    {
        loadingUI.SetActive(true);
        StartCoroutine(LoadScene(SceneName));
      

    }
   
    IEnumerator LoadScene(string SceneName)
    {
        async=SceneManager.LoadSceneAsync(SceneName);

        while (!async.isDone)
        {
            loadingbar.fillAmount = async.progress;
            yield return null;
        }
    }
}
