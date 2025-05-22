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

    public void blinkinge_effect(Text text)　　　　//点滅エフェクト
    {
        text.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 少しずつ黒くするエフェクト
    /// </summary>
    /// <param name="obj">色を変わる対象</param>
    /// <param name="nextscene">シーンの名前</param>
    public void turnblack(Image obj, string nextscene)  
    {
        obj.gameObject.SetActive(true);
        obj.DOFade(1, 0.5f).OnComplete(() =>
        {
            obj.color = new Color(0, 0, 0, 0);
            
            SceneManager.LoadScene(nextscene, LoadSceneMode.Single);
            obj.gameObject.SetActive(false);

        });
    }  
    


}
