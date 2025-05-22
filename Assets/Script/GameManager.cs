using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool IsStarted;

    int Enemyvalue;
    int Day;
    bool clear;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Debug.Log("GameManager Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
