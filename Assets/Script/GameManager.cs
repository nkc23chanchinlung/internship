using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全シーンで存在し続けるゲーム全体を管理するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    static public GameManager instance { get; private set; }

    public static event Action OnGameStart; //ゲーム開始時のイベント
    public bool GameStop = false;  //ゲームを停止するかどうか
    static public bool GameManagerExist = false;//GameManagerが存在フラグ
    public bool IsOpenMoviePlaying { get; set; } //ムービー再生中かどうか
    
    bool IsStarted;
    public int Day { get; set; }
    int Enemyvalue;
    
    bool clear;

    public void StartGame()
    {
        GameStop = false;
        IsOpenMoviePlaying = false;
        OnGameStart?.Invoke();
    }

    static public int Coin { get; set; } = 0;  //所持金



    private void Awake()
    {
        CheakGameManagerExist();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
       
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //GameManagerの重複を防ぐ
    void CheakGameManagerExist()
    {
       if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

   
    void Setdata(int day)
    {
        Day = day;
    }
}
