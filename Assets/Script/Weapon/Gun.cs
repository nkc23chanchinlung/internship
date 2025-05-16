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

    public virtual void Shoot() { }

    
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
