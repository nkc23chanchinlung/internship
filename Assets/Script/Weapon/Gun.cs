using System.Collections;
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
    public bool IsReloading;
    public float ReloadTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public virtual void Shoot() { }
   
    public IEnumerator Reload(float ReloadTime)
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
