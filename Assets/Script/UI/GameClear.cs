using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

//ゲームクリア決算クラス
public class GameClear : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Text _time_Text;
    [SerializeField] GameObject _assessment;
    [SerializeField] Sprite[] _assessment_Image;
    [SerializeField] Text _deathCount_Text;

    private void OnEnable()
    {
        gameManager=GameManager.Instance;
        StartCoroutine(ShowClear(_time_Text, _deathCount_Text));
        _assessment.gameObject.transform.localScale = Vector3.one * 4;
        _assessment.SetActive(false);
    }

    IEnumerator ShowClear(Text timeText,Text deathCountText)
    {
        float timer = 0f;

        while (timer < 1.0f)
        {
            timeText.text = Random.Range(0.0f, 100f).ToString("F2")+"s";
            timer += Time.deltaTime;
            yield return null;
        }

       

        float deathTimer = 0f;
        while (deathTimer < 1.0f) {
            deathCountText.text = Random.Range(0, 100).ToString() + "回";
            deathTimer += Time.deltaTime;
            yield return null;
        }
        deathCountText.text=gameManager.DeathCount.ToString() + "回";
        _assessment.SetActive(true);
        _assessment.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        if (gameManager.GetTime() < 120f)
        {
            _assessment.GetComponent<Image>().sprite = _assessment_Image[3];
        }
        else if (gameManager.GetTime() < 180f)
        {
            _assessment.GetComponent<Image>().sprite = _assessment_Image[2];
        }
        else if (gameManager.GetTime() < 240f)
        {

            _assessment.GetComponent<Image>().sprite = _assessment_Image[1];
        }
        else
        {
            _assessment.GetComponent<Image>().sprite = _assessment_Image[0];
        }
        timeText.text = gameManager.GetTime().ToString("F2") + "s";
    }
    public void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
