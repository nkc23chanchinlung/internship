using UnityEngine;

public class AK47 : Gun
{
    DataManager ak47data;
    private void OnEnable()
    {
        if (playerController != null)
            uiManager.SearchMagazine();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        try
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        catch
        {
            Debug.LogError("PlayerController not found");
        }
        ak47data =new DataManager();

    }

    void Start()
    {
        cooldown = 0.2f;
        Magazine = 30;
        MaxMagazine = 30;
        MaxCooldown = 0.2f;
        IsReloading = false;
        ReloadTime = 1f;
        Damage=10;
        Pow = 2;
        Repair = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null)
        {
            uiManager.SetMagazine(Magazine, MaxMagazine);
            Shoot();
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload(ReloadTime));
            }
        }
    }
    public override void Shoot() //éÀåÇ
    {
        cooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) &&
            !IsReloading &&
            cooldown <= 0 &&
            Magazine > 0 && 
            !playerController.IsCreate)
        {
            //èàóù
            GameObject bullet= Instantiate(bulletprefab, 
                transform.position + (-transform.forward), 
                transform.rotation * Quaternion.Euler(0, 180, 0));
           

            Bullet ak47bullet = bullet.GetComponent<Bullet>();
            ak47bullet.damage = Damage;
            bullet.tag = "PlayerBullet";
            Magazine--;
            cooldown = MaxCooldown;

        }
    }



}
