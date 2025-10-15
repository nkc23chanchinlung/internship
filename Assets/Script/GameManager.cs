using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全シーンで存在し続けるゲーム全体を管理するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    static public bool GameStop = false;  //ゲームを停止するかどうか
    static public bool GameManagerExist = false;
    bool IsStarted;
    public int Day { get; set; }
    int Enemyvalue;
    
    bool clear;

    static public int Coin { get; set; } = 0;  //所持金



    //private void Awake()
    //{
    //    Application.targetFrameRate = 60;
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        CheakGameManagerExist();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheakGameManagerExist()
    {
       
        if (!GameManagerExist)
        {
            GameManagerExist = true;
            //IsStarted = false;
            //Enemyvalue = 0;
            //Day = 0;
            //clear = false;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Setdata(int day)
    {
        Day = day;
    }
}
