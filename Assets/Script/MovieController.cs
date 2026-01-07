using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DG.Tweening;
public class MovieController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] PlayableDirector director;
    [SerializeField]GameObject player;
    [SerializeField] Transform OutLine1;
    [SerializeField] Transform OutLine2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        gameManager = GameManager.instance;
        OpeningMovie();
        

    }

    // Update is called once per frame
   
    void OpeningMovie()
    {
        director.Play();
        director.stopped += OnTimelineStopped;
        gameManager.IsOpenMoviePlaying = true;


    }
    void OnTimelineStopped(PlayableDirector aDirector)
    {
        if (aDirector == director)
        {
            
            player.SetActive(true);
            gameManager.IsOpenMoviePlaying = false;
            MoveUIController();
            gameManager.StartGame();
            director.gameObject.SetActive(false);
            
        }
    }
    void MoveUIController()
    {   
        //UIÇï\é¶Ç≥ÇπÇÈ
        OutLine1.gameObject.SetActive(true);
        OutLine2.gameObject.SetActive(true);

        //UIÇè„â∫Ç…à⁄ìÆÇ≥ÇπÇÈ
        OutLine1.DOMoveY(OutLine1.transform.position.y + 100, 1f).SetEase(Ease.InOutSine);
        OutLine2.DOMoveY(OutLine2.transform.position.y -100, 1f).SetEase(Ease.InOutSine);

    }
}
