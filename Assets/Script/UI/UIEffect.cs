using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// UIのエフェクトを管理するクラス
/// </summary>
public class UIEffect : MonoBehaviour
{
    [SerializeField] GameObject Damage_text;

    public void DamageEffect(GameObject obj)
    {
        Debug.Log("DamageEffect");
        obj.transform.DOMove(obj.transform.position+new Vector3(0, 100, 0), 1f).SetEase(Ease.OutBounce);
        Text text = obj.GetComponent<Text>();
        text.DOFade(0, 1f).OnComplete(() =>
        {
            Destroy(obj);
        });

    }
    


}
