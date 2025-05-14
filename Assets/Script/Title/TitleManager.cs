using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    [SerializeField]Image titleframe;
    UnityEngine.GameObject titleframe_Obj;
    [Header("Alpha")]
    [Tooltip("0-100")]
    [Range(0, 100)]
    [SerializeField]float alpha = 0;
    public bool isStart { get; set; } = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleframe_Obj = titleframe.gameObject;
        titleframe_Obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        titleframe.color = new Color(0, 0, 0, alpha/100);
        if (isStart)
        {
            GameStart_EF();
        }


    }
    void GameStart_EF()
    {
        titleframe_Obj.SetActive(true);
        alpha++;
        if (alpha >= 100)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
