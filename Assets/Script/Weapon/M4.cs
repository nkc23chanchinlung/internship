using UnityEngine;

public class M4 : Gun
{
    [SerializeField]int range;

    void OnEnable()
    {
        uiManager.SearchMagazine();
        uiManager.SetMagazine(Magazine, MaxMagazine);
    }
    
    void Start()
    {
       
        MaxCooldown = 1f;
        cooldown = 1f;
        Magazine = 8;
        MaxMagazine = 8;
        ReloadTime = 1f;
        bulletprefab = Resources.Load("bullet") as UnityEngine.GameObject;
        Damage = 25;
       
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload(ReloadTime));
        }

    }
    public override void Shoot()//éÀåÇ
    {
        uiManager.SetMagazine(Magazine, MaxMagazine);
        cooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) &&
            !IsReloading &&
            cooldown <= 0 && 
            Magazine > 0 &&
            !playerController.IsCreate)
        {
            //èàóù  
            for (int i = -range; i < range; i++)
            {

                GameObject bullet = Instantiate(bulletprefab, 
                    transform.position + (-transform.forward)+transform.right*(i*0.2f),
                    transform.rotation * Quaternion.Euler(0, 180-(15*i), 0));

                Bullet M4bullet = bullet.GetComponent<Bullet>();
                M4bullet.damage = Damage;
                bullet.tag = "PlayerBullet";
            }
            Magazine--;
            cooldown = MaxCooldown;

        }
    }
}
