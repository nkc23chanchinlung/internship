using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// ストアのコントローラークラス
/// </summary>
public class StoreManager : MonoBehaviour
{
    [SerializeField]LayerMask layerMask;
    [SerializeField] GameObject[] Weapon;
    [SerializeField] GameObject ItemInfoPanel;
    [SerializeField]
    GameObject[] _powGague;
    [SerializeField]
    GameObject[] _weaponreveiw_PowGague;
    [SerializeField] Text ItemName;
    [SerializeField] Text Info;
    GameObject target;
    GameManager gameManager;
    [SerializeField] PreViewController preViewController;
    [SerializeField] GameObject skillPanel;
    GameObject _chooseGun;

    [Header("value")]
    [SerializeField] int coin;

    [Header("UI")]
    [SerializeField] Text CoinText;

    [SerializeField] GameObject _msgPanel;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        catch (System.Exception e)
        {
            Debug.LogError("GameManager not found: " + e.Message);
        }
       // Weapon =GameObject.FindGameObjectsWithTag("Weapon");
        // Powgague= GameObject.FindGameObjectsWithTag("Powgague");
       
        coin = GameManager.Coin;
        Reset();


    }

    // Update is called once per frame
    void Update()
    {

        Coin_Text(coin);

        for (int i = 0; i < _powGague.Length; i++)
        {
            _powGague[i].SetActive(false);
            
            
        }

        if (!preViewController.IsPreviewing)
        {
            if (ItemChoose() != null&&ItemChoose().gameObject.name!= "Magic Book")
            {
                ItemInfo(ItemChoose());
                ItemInfoPanel.SetActive(true);
            }
            else ItemInfoPanel.SetActive(false);
        }
      


    }
    void ItemInfo(GameObject target)
    {
        ItemName.text = target.name;
        
    }
    //ItemChoose()はRaycastでアイテムを選択する関数
    GameObject ItemChoose()
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            Weapon[i].GetComponent<Outline>().enabled = false;
        }
        Vector3 mospos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mospos);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);    //Debug用************

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {

           
            hit.collider.gameObject.GetComponent<Outline>().enabled = true;
            GetInfo(hit.collider.gameObject);
           
            return hit.collider.gameObject;

        }
        else
        {
            return null;
        }
    }
    //GetInfo()は選択したアイテムの情報を表示する関数
    void GetInfo(GameObject target)
    {
       Debug.Log(target.name);
        Gun Guninfo = target.GetComponent(typeof(Gun)) as Gun;
        if (Input.GetMouseButtonDown(0))
        {
            if (target.name == "Magic Book" && Input.GetMouseButtonDown(0))
            {
                skillPanel.SetActive(true);
            }
            else
            {
                preViewController.Showpreview(Guninfo.weaponnum, target.name);
                
                int pow = Guninfo.Pow;
                _chooseGun = target;
                Debug.Log("_chooseGun: " + _chooseGun.name);
                Debug.Log("pow: " + pow);
                for (int i = 0; i < pow; i++)
                {
                    _weaponreveiw_PowGague[i].SetActive(true);
                }

            }
        }
        else
        {
            if (Guninfo != null)
                InfoValuegague(Guninfo.Pow, Guninfo.Repair);
        }

    }
    //InfoValuegague()は選択したアイテムのPowとRepairをゲージで表示する関数
    void InfoValuegague(int pow,int repair)
    {
        
        Debug.Log("pow: " + pow + " repair: " + repair);
        for (int i = 0; i < pow; i++)
        {
           _powGague[i].SetActive(true);
            
            Debug.Log(pow);
        }
    }
    void Coin_Text(int coin)
    {
        CoinText.text = coin.ToString("D2");
    }

    public void Exit()
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            if (Weapon[i].GetComponent<Gun>() != null) {
                Gun info = Weapon[i].GetComponent(typeof(Gun)) as Gun;
                DataManager.Instance.GunDatabase[i].WeaponPower = info.Pow;
                Debug.Log("Saving Gun " + i + ": Pow = " + info.Pow);
            }
            
          
        }
        SceneManager.LoadScene("GameScene");
    }
    public void SkillPanelExit()
    {
        if (skillPanel.activeSelf)
        {
            skillPanel.SetActive(false);
        }
    }
    //パネルを閉じる時にゲージをリセットする関数
    public void Reset()
    {
        for (int i = 0; i < _weaponreveiw_PowGague.Length; i++)
        {
            _weaponreveiw_PowGague [i].SetActive(false);
        }
    }
  
    //強化する関数
    public void reinforce()
    {
        Gun guninfo = _chooseGun.GetComponent(typeof(Gun)) as Gun;
        if (guninfo.Pow >= 7)
        {
            _msgPanel.SetActive(true);


            _ = WaitForAsync(0.5f, () =>
            {
                _msgPanel.SetActive(false);
            });

        }
        else
        {
            guninfo.Pow += 1;
        }
        for (int i = 0; i < guninfo.Pow; i++)
        {
            _weaponreveiw_PowGague[i].SetActive(true);
            
        }
       
    }

    private async Task WaitForAsync(float seconds, Action action)
    {
        await Task.Delay(TimeSpan.FromSeconds(seconds));
        action();
    }
}
