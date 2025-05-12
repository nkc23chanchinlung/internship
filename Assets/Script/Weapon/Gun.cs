using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletprefab;
    public PlayerController playerController;
    public UIManager uiManager;
    public float cooldown;
    public int Magazine;
    public int MaxMagazine;
    public float MaxCooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    public virtual void Shoot()
    {
        
        cooldown -= Time.deltaTime;
        
        if (Input.GetMouseButton(0) && cooldown <= 0 && Magazine > 0 && !playerController.IsCreate)
        {

            Instantiate(bulletprefab, transform.position + (-transform.forward), transform.rotation * Quaternion.Euler(0, 180, 0));
            Magazine--;
            cooldown = MaxCooldown;

        }
      
    }
    public virtual void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Magazine < MaxMagazine)
            {
                Magazine = MaxMagazine;
            }
        }
    }
}
