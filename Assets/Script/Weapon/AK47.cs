using UnityEngine;

public class AK47 : Gun
{

    private void OnEnable()
    {
        uiManager.SearchMagazine();
        
        Debug.Log("AK47");
    }
   


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    void Start()
    {
        cooldown = 0.2f;
        Magazine = 30;
        MaxMagazine = 30;
        MaxCooldown = 0.2f;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        uiManager.SetMagazine(Magazine, MaxMagazine);
        Shoot();
        Reload();
    }
    
    
}
