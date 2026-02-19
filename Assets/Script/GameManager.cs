using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 全シーンで存在し続けるゲーム全体を管理するクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }

    public static event Action OnGameStart; //ゲーム開始時のイベント
    public static event Action OnGameStop;
    public static event Action OnContinue;
    public bool GameStop { get; set; } = false;  //ゲームを停止するかどうか
    static public bool GameManagerExist = false;//GameManagerが存在フラグ
    public bool IsOpenMoviePlaying { get; set; } //ムービー再生中かどうか

    [SerializeField]AudioManager audioManager;
    [SerializeField] AudioClip gameBGM;
    public int enterShop { get; set; } = 0;
    


    bool _isStarted;
    public int Stage { get; set; }
    int _enemyValue;
    
    bool _clear;
    int _savePoint;

    [SerializeField] Texture2D cursorTexture;

    public void StartGame()
    {
        GameStop = false;
        IsOpenMoviePlaying = false;
        OnGameStart?.Invoke();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
       // audioManager.PlayBGM("GameScene");

    }
    public void StopGame()
    {
     GameStop = true;
     OnGameStop?.Invoke();
     Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
     

    }
    public void ContinueGame()
    {
        GameStop = false;
        OnContinue?.Invoke();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        
    }

    static public int Coin { get; set; }  //所持金


    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        CheakGameManagerExist();
    }
    //GameManagerの重複を防ぐ
    void CheakGameManagerExist()
    {
       if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

   
    void Setdata(int day)
    {
        Stage = day;
    }

    void Savepoint(int savepoint)
    {
        
    }
}
