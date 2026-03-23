using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameClear : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] Text _time_Text;
    [SerializeField] GameObject _assessment;
    [SerializeField] Sprite[] _assessment_Image;

    private void OnEnable()
    {
        gameManager=GameManager.Instance;
        StartCoroutine(ShowClear(_time_Text));
        _assessment.gameObject.transform.localScale = Vector3.one * 4;
        _assessment.SetActive(false);
    }

    IEnumerator ShowClear(Text text)
    {
        float timer = 0f;

        while (timer < 1.0f)
        {
            text.text = Random.Range(0.0f, 100f).ToString("F2")+"s";
            timer += Time.deltaTime;
            yield return null;
        }

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
        text.text =  gameManager.GetTime().ToString("F2") + "s";

    }
    public void ReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
