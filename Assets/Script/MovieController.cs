using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using DG.Tweening;
using Unity.VisualScripting;
//ムービー制御
public class MovieController : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] PlayableDirector director;
    [SerializeField] GameObject player;
    [SerializeField] Transform OutLine1; //ムービー再生黒枠
    [SerializeField] Transform OutLine2;
    [SerializeField] GameObject[] shopExitpos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        gameManager = GameManager.Instance;
        OpeningMovie();
        

    }
    private void Update()
    {
        if (gameManager.enterShop != 0)
        {
            director.Stop();
        }
        //ムービースキップ
        if (Input.GetMouseButtonDown(0))
        {
            director.Stop();
        }
    }

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
            //ショップから出るキャラの出現場所が変更処理
            if (gameManager.enterShop == 1)
            {
                player.transform.position = shopExitpos[0].transform.position;
            }
            else if (gameManager.enterShop == 2)
            {
                player.transform.position = shopExitpos[1].transform.position;
            }
                gameManager.IsOpenMoviePlaying = false;
            MoveUIController();
            gameManager.StartGame();
            director.gameObject.SetActive(false);
            Destroy(this.gameObject);

        }
        
    }
    void MoveUIController()
    {   
        //UIを表示させる
        OutLine1.gameObject.SetActive(true);
        OutLine2.gameObject.SetActive(true);

        //UIを上下に移動させる
        OutLine1.DOMoveY(OutLine1.transform.position.y + 100, 1f).SetEase(Ease.InOutSine);
        OutLine2.DOMoveY(OutLine2.transform.position.y -100, 1f).SetEase(Ease.InOutSine);

    }
}
