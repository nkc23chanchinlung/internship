using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// UIのエフェクトを管理するクラス
/// </summary>
public class UIEffect : MonoBehaviour
{

    //[SerializeField] GameObject Damage_text;

    public void DamageEffect(GameObject obj)
    {
        var sequence = DOTween.Sequence();
        int random = Random.Range(-100, 101);

        sequence.Append(obj.transform.DOMove(obj.transform.position + new Vector3(random, 100, 0), 1f).SetEase(Ease.OutBounce));
        sequence.Join(obj.transform.DOScale(Vector3.one * 1.2f, 1f));

        Text text = obj.GetComponent<Text>();
        text.DOFade(0, 1f).OnComplete(() =>
        {
            Destroy(obj);
        });

    }

    public  void Blinkinge_Effect(Image image)　　　　//点滅エフェクト
    {
        image.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    public void Blinkinge_Effect(Text text)      　//点滅エフェクトオーバーロード
    {
        text.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    public void RotateEffect()
    {

    }

    /// <summary>
    /// 表示するエフェクト
    /// </summary>
    /// <param name="obj">色を変わる対象</param>
    /// <param name="nextscene">シーンの名前</param>
    public void displayeffect(Image obj, string nextscene,float displaytimer)  
    {
        obj.gameObject.SetActive(true);
        obj.DOFade(1, displaytimer).OnComplete(() =>
        {
            //obj.color = new Color(0, 0, 0, 0);
            if(nextscene!=null)
            SceneManager.LoadScene(nextscene, LoadSceneMode.Single);
            //obj.gameObject.SetActive(false);

        });
    }  
    public void hideeffect(Image obj,float hidetimer)
         //非表示するエフェクト
    {
        if(!obj.gameObject.activeSelf)
        obj.gameObject.SetActive(true);
        Color c = obj.color;
        c.a = 1;
        obj.color = c;
        
        obj.DOFade(0, hidetimer).OnComplete(() =>
        {
            obj.gameObject.SetActive(false);
        });
    }
    public void expansioneffect(GameObject obj,Vector3 size)   //拡大エフェクト
    {
        obj.transform.DOScale(size *1f, 0.2f).SetEase(Ease.InElastic);
    }
    public void ereductioneffect(GameObject obj, Vector3 size)　//縮小エフェクト 

    {
        obj.transform.DOScale(size * 0f, 0.2f).OnComplete(() =>
        {
            obj.SetActive(false);
        });
    }



}
