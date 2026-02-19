using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class BottonManager : MonoBehaviour
{
   [SerializeField] TitleManager titleManager;
    [SerializeField]UIEffect uIEffect;
    [SerializeField] GameObject SettingPanel;
    [SerializeField] PlayableDirector director;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _SelectSe;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GameStart()
    {
        
      
        _audioSource.PlayOneShot(_SelectSe);
        director.Play();
        director.stopped += gamestart;
        
        


    }
    void gamestart(PlayableDirector aDirector)
    {
        if (aDirector == director)
        {
            titleManager._IsStart = true;
        }
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void Setting()
    {
        _audioSource.PlayOneShot(_SelectSe);
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
