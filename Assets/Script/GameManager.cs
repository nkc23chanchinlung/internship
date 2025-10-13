using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public bool GameStop = false;  //ÉQÅ[ÉÄÇí‚é~Ç∑ÇÈÇ©Ç«Ç§Ç©
    static public bool GameManagerExist = false;
    bool IsStarted;
    public int Day { get; set; }
    int Enemyvalue;
    
    bool clear;

    static public int Coin { get; set; } = 0;  //èäéùã‡



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
