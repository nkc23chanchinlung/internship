using UnityEngine;

public class AK47 : Gun
{
    WeaponDatabase ak47data;
    [SerializeField] GameObject Weapon;
    private void OnEnable()
    {
        if (playerController != null)
            uiManager.SearchMagazine();

        audioSource = Weapon.GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Sound/SE/AK47_Shot");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        cooldown = 0.2f;
        Magazine = 30;
        MaxMagazine = 30;
        MaxCooldown = 0.2f;

        Damage = 40;
        Pow = 2;
        Repair = 1;
        weaponnum = 0; //ïêäÌî‘çÜ
        try
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        if (GameManager.instance.gameStop) return;

        if (playerController != null)
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
            cooldown <= 0 &&
            Magazine > 0 && 
            !playerController.IsCreate)
        {
            //èàóù
            GameObject bullet= Instantiate(bulletprefab, 
                transform.position + (-transform.forward), 
                transform.rotation * Quaternion.Euler(0, 180, 0));

            audioSource.PlayOneShot(audioSource.clip);


            Bullet ak47bullet = bullet.GetComponent<Bullet>();
            ak47bullet.damage = Damage;
            bullet.tag = "PlayerAtk";
            Magazine--;
            cooldown = MaxCooldown;

        }
    }



}
