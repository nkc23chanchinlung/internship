using UnityEngine;

public class M4 : Gun
{
    [SerializeField]int range;
    [SerializeField] GameObject Weapon;
    void OnEnable()
    {
      
        GameManager.OnGameStart += OnGameStart;
    }
    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
    private void Awake()
    {
        cooldown = 1f;
        Magazine = 8;
        MaxMagazine = 8;
        MaxCooldown = 1;

        Damage = 50;
        Pow = 5;
        Repair = 1;
        weaponnum = 1; //ïêäÌî‘çÜ
    }
    void OnGameStart()
    {
       

        bulletprefab = Resources.Load("bullet") as UnityEngine.GameObject;
        uiManager = GameObject.Find("-----UIManager-----").GetComponent<UIManager>();
       
        audioSource = Weapon.GetComponent<AudioSource>();
        

        if (playerController != null)
        {

            uiManager.SearchMagazine();
            uiManager.SetMagazine(Magazine, MaxMagazine);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameStop) return;

        if (playerController != null)
        {
            Shoot();
            if (Input.GetKeyDown(KeyCode.R) && Magazine != MaxMagazine)
            {
                StartCoroutine(Reload(ReloadTime));
            }
        }

    }
    protected override void Shoot()//éÀåÇ
    {
        base.Shoot();

        if (Input.GetMouseButton(0) &&
            !IsReloading &&
            cooldown <= 0 && 
            Magazine > 0 &&
            !playerController.IsCreate)
        {
           audioSource.clip = Resources.Load<AudioClip>("Sound/SE/M4_Shot");
            audioSource.PlayOneShot(audioSource.clip);
            

           
            //èàóù  
            for (int i = -range; i < range; i++)
            {

                GameObject bullet = Instantiate(bulletprefab, 
                    transform.position + (-transform.forward)+transform.right*(i*0.2f),
                    transform.rotation * Quaternion.Euler(0, 180-(15*i), 0));

                Bullet M4bullet = bullet.GetComponent<Bullet>();
                M4bullet.damage = Damage;
                bullet.tag = "PlayerAtk";
            }
            Magazine--;
            cooldown = MaxCooldown;

        }
    }
}
