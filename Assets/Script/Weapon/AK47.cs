using UnityEngine;

public class AK47 : MonoBehaviour
{
    [SerializeField] GameObject bulletprefab;
    UIManager uiManager;
    float cooldown = 0.2f;
    int Magazine = 30;
    int MaxMagazine = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        uiManager = new UIManager();
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.Magazine_UI(Magazine, MaxMagazine);
        cooldown -= Time.deltaTime;
        if (Input.GetMouseButton(0)&&cooldown<=0&&Magazine>0)
        {
           
            Instantiate(bulletprefab,transform.position+(-transform.forward), transform.rotation*Quaternion.Euler(0,180,0));
            Magazine--;
            cooldown = 0.2f;
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (Magazine < MaxMagazine)
            {
                Magazine=MaxMagazine;
            }
        }
    }
    
    
}
