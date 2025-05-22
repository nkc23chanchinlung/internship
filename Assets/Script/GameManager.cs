using UnityEngine;

public class GameManager : MonoBehaviour
{
   static bool GameManagerExist = false;
    bool IsStarted;
    public int Day { get; set; } = 3;
    int Enemyvalue;
    
    bool clear;
   
    

    
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
}
