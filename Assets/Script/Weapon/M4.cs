using UnityEngine;
using UnityEngine.Audio;

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
        CoolDown = 1f;
        Magazine = 8;
        MaxMagazine = 8;
        MaxCooldown = 1;
        Se = Resources.Load<AudioClip>("Sound/SE/M4_Shot");
        Damage = 50;
       
        Repair = 1;
        weaponnum = 1; //ïêäÌî‘çÜ
        Pow = DataManager.Instance.GunDatabase[weaponnum].WeaponPower;
    }
    //void OnGameStart()
    //{


    //    bulletprefab = Resources.Load("bullet") as UnityEngine.GameObject;
    //    uiManager = GameObject.Find("-----UIManager-----").GetComponent<UIManager>();

    //    audioSource = Weapon.GetComponent<AudioSource>();


    //    if (playerController != null)
    //    {

    //        uiManager.SearchMagazine();
    //        uiManager.SetMagazine(Magazine, MaxMagazine);
    //    }

    //}
   

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameStop) return;

        if (PlayerController != null)
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
            CoolDown <= 0 && 
            Magazine > 0 &&
            !PlayerController.IsCreate)
        {
          
            AudioManager.Instance.PlaySE(Se);



            //èàóù  
            for (int i = -range; i < range; i++)
            {

                GameObject bullet = Instantiate(BulletPrefab, 
                    transform.position + (-transform.forward)+transform.right*(i*0.2f),
                    transform.rotation * Quaternion.Euler(0, 180-(15*i), 0));

                Bullet M4bullet = bullet.GetComponent<Bullet>();
                M4bullet.damage = Damage;
                bullet.tag = "PlayerAtk";
            }
            Magazine--;
            CoolDown = MaxCooldown;

        }
    }
}
