using UnityEngine;

public class AK47 : Gun
{
    WeaponDatabase ak47data;
    [SerializeField] GameObject Weapon;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {

        GameManager.OnGameStart += OnGameStart;
        //if (PlayerController != null)
        //    UiManager.SearchMagazine();

        //audioSource = GetComponent<AudioSource>();
       
    }
    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
   
    private void Awake()
    {
        Se = Resources.Load<AudioClip>("Sound/SE/AK47_Shot");
        CoolDown = 0.2f;
        Magazine = 30;
        MaxMagazine = 30;
        MaxCooldown = 0.2f;

        Damage = 40;
        
        Repair = 1;
        weaponnum = 0; //ïêäÌî‘çÜ
        Pow = DataManager.Instance.GunDatabase[weaponnum].WeaponPower;
        try
        {
            PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        catch
        {
            Debug.LogError("PlayerController not found");
        }
        ak47data =new WeaponDatabase();

    }

    void Start()
    {
       

        //IsReloading = false;
        //ReloadTime = 1f;


    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameStop) return;

        if (PlayerController != null)
        {
            
            Shoot();
            if (Input.GetKeyDown(KeyCode.R)&&Magazine!=MaxMagazine)
            {
                StartCoroutine(Reload(ReloadTime));
            }
        }
    }
    protected override void Shoot() //éÀåÇ
    {

        base.Shoot();
        if (Input.GetMouseButton(0) &&
            !IsReloading &&
            CoolDown <= 0 &&
            Magazine > 0 && 
            !PlayerController.IsCreate)
        {
            //èàóù
            GameObject bullet= Instantiate(BulletPrefab, 
                transform.position + (-transform.forward), 
                transform.rotation * Quaternion.Euler(0, 180, 0));

            //audioSource.PlayOneShot(audioSource.clip);
            AudioManager.Instance.PlaySE(Se);


            Bullet ak47bullet = bullet.GetComponent<Bullet>();
            ak47bullet.damage = (float)Damage*((float)Pow*0.7f);
            bullet.tag = "PlayerAtk";
            Magazine--;
            CoolDown = MaxCooldown;

        }
    }



}
