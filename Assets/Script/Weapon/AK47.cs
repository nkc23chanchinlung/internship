using UnityEngine;

public class AK47 : Gun
{

    private void OnEnable()
    {
        uiManager.SearchMagazine();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerController =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    void Start()
    {
        cooldown = 0.2f;
        Magazine = 30;
        MaxMagazine = 30;
        MaxCooldown = 0.2f;
        IsReloading = false;
        ReloadTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        uiManager.SetMagazine(Magazine, MaxMagazine);
        Shoot();
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload(ReloadTime));
        }
    }
    public override void Shoot() //éÀåÇ
    {
        cooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) &&
            cooldown <= 0 &&
            Magazine > 0 && 
            !playerController.IsCreate)
        {
            //èàóù
            GameObject bullet= Instantiate(bulletprefab, 
                transform.position + (-transform.forward), 
                transform.rotation * Quaternion.Euler(0, 180, 0));
            Magazine--;
            bullet.tag = "PlayerBullet";
            cooldown = MaxCooldown;

        }
    }



}
