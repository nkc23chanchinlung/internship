using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Loading : MonoBehaviour
{
    AsyncOperation async;
    [SerializeField]GameObject loadingUI;
    
    [SerializeField] GameObject _anyKey_Text;

    [SerializeField] GameObject effect;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
       Text _anyKey=_anyKey_Text.GetComponent<Text>();
        _anyKey.DOFade(endValue: 0f, duration: 1f).SetLoops(-1,LoopType.Yoyo);

        effect.transform.DOMoveX(2000,4).SetLoops(-1,LoopType.Restart);
    }
   
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
            
            if (async.progress >= 0.9f)
            {
                async.allowSceneActivation = false;
                _anyKey_Text.SetActive(true);
                if(Input.anyKey)
                    async.allowSceneActivation= true;
            }
            yield return null;
        }
       
    }
}
