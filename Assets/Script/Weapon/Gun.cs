using System.Collections;
using UnityEngine;

/// <summary>
/// 銃の基底クラス
/// </summary>
public class Gun :  MonoBehaviour

{
    [Header("銃ステータス")]
    public GameObject BulletPrefab;
    public PlayerController PlayerController;
    public UIManager UiManager;
    public int weaponnum;
    public int Damage;
    public int Pow;
    public int Repair;

    [Header("弾")]
    public int Magazine;
    public int MaxMagazine;
    public float ReloadTime;
    public float MaxCooldown;
    public float CoolDown;
    public bool IsReloading;

    [Header("効果音")]
    public AudioClip Se;
    //public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public  void OnGameStart()
    {


        BulletPrefab = Resources.Load("bullet") as UnityEngine.GameObject;
        UiManager = GameObject.Find("-----UIManager-----").GetComponent<UIManager>();

        //audioSource = GetComponent<AudioSource>();
        if (PlayerController != null)
            UiManager.SearchMagazine();

        if (PlayerController != null)
        {

            UiManager.SearchMagazine();
            UiManager.SetMagazine(Magazine, MaxMagazine);
            Debug.Log("Magazine: " + Magazine + "/" + MaxMagazine);
        }

    }
    protected virtual void Shoot()
    {
        UiManager.SetMagazine(Magazine, MaxMagazine);
        CoolDown -= Time.deltaTime;
        
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
