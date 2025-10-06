using System.Collections;
using UnityEngine;

/// <summary>
/// èeÇÃäÓíÍÉNÉâÉX
/// </summary>
public class Gun :  MonoBehaviour

{
    public GameObject bulletprefab;
    public PlayerController playerController;
    public UIManager uiManager;
    public float cooldown;
    public int Magazine;
    public int MaxMagazine;
    public float MaxCooldown;
    public bool IsReloading;
    public float ReloadTime;
    public int Damage;
    public int Pow;
    public int Repair;
    public int weaponnum;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected virtual void Shoot()
    {
        uiManager.SetMagazine(Magazine, MaxMagazine);
        cooldown -= Time.deltaTime;
        
        }

    protected IEnumerator Reload(float ReloadTime)
    {
        
        if (!IsReloading)
        {
            IsReloading = true;
            yield return new WaitForSeconds(ReloadTime);
            Magazine = MaxMagazine;
            IsReloading = false;
        }
    }
}
