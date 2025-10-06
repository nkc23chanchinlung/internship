using DG.Tweening;
using UnityEngine;

/// <summary>
/// コイン制御クラス
/// </summary>
public class Coin : MonoBehaviour
{
    [SerializeField] Transform CoinUI;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

   
    void Start()
    {
        CoinUI =GameObject.Find("Coin_UI").transform;

        this.transform.DOMove(CoinUI.position,1f).SetDelay(1f).OnComplete(() =>
        {
            Destroy(this.gameObject);
            GameManager.Coin += 1; //コインを増やす

        });
    }

  
}
