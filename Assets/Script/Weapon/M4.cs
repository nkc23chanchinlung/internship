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
        bulletprefab = Resources.Load("bullet") as UnityEngine.GameObject;
       
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Reload();
        
    }
    public override void Shoot()
    {
        uiManager.SetMagazine(Magazine, MaxMagazine);
        cooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) && cooldown <= 0 && Magazine > 0 && !playerController.IsCreate)
        {
           
            for (int i = -range; i < range; i++)
            {
                
                Instantiate(bulletprefab, transform.position + (-transform.forward)+transform.right*(i*0.2f), transform.rotation * Quaternion.Euler(0, 180-(15*i), 0));
                
            }
            Magazine--;
            cooldown = MaxCooldown;

        }
    }
}
