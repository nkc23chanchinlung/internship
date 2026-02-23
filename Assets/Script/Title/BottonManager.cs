using DG.Tweening;
using System.Collections;
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
    [SerializeField] Transform TitlePanel;
    [SerializeField] GameObject _titleImage;
   

    private void Awake()
    {

            StartCoroutine(TitleImg_EF());
          
        
    }
    IEnumerator TitleImg_EF()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject titleimg = Instantiate(_titleImage, TitlePanel);
            titleimg.transform.position = new Vector3(500, -200, 0); // ŠJŽnˆÊ’u‚ðŒÅ’è

            titleimg.transform.DOMoveY(1300, 100)
                .SetSpeedBased()
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);

           

            yield return new WaitForSeconds(3f); // 1•bŠÔŠu‚ÅŽŸ‚Ì‰æ‘œ‚ð¶¬

        }
    }


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
