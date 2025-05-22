using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// UI�̃G�t�F�N�g���Ǘ�����N���X
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

    public void blinkinge_effect(Text text)�@�@�@�@//�_�ŃG�t�F�N�g
    {
        text.DOFade(0, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// ��������������G�t�F�N�g
    /// </summary>
    /// <param name="obj">�F��ς��Ώ�</param>
    /// <param name="nextscene">�V�[���̖��O</param>
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
