using UnityEngine;

public class AK47 : MonoBehaviour
{
    [SerializeField] GameObject bulletprefab;
    PlayerController playerController;
    UIManager uiManager;
    float cooldown = 0.2f;
    int Magazine = 30;
    int MaxMagazine = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        uiManager = new UIManager();
        uiManager.SearchMagazine();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.SetMagazine(Magazine, MaxMagazine);
        cooldown -= Time.deltaTime;
        if (Input.GetMouseButton(0)&&cooldown<=0&&Magazine>0&&!playerController.IsCreate)
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
