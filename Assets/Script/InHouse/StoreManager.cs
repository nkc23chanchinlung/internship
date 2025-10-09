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
    GameObject[] Powgague;
    [SerializeField] Text ItemName;
    [SerializeField] Text Info;
    GameObject target;
    GameManager gameManager;
    [SerializeField] PreViewController preViewController;


    [Header("value")]
    [SerializeField] int coin;

    [Header("UI")]
    [SerializeField] Text CoinText;

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
        Weapon =GameObject.FindGameObjectsWithTag("Weapon");
        // Powgague= GameObject.FindGameObjectsWithTag("Powgague");
       
        coin = GameManager.Coin;



    }

    // Update is called once per frame
    void Update()
    {
        Coin_Text(coin);

        for (int i = 0; i < Powgague.Length; i++)
        {
            Powgague[i].SetActive(false);
        }

        if (!preViewController.IsPreviewing)
        {
            if (ItemChoose() != null)
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
    void GetInfo(GameObject target)
    {
        Gun Guninfo=target.GetComponent(typeof(Gun))as Gun;
       
        InfoValuegague(Guninfo.Pow,Guninfo.Repair);
        if (Guninfo != null && Input.GetMouseButtonDown(0))
        {
          
            preViewController.Showpreview(Guninfo.weaponnum,target.name);
        }

    }
    
    void InfoValuegague(int pow,int repair)
    {
        for(int i = 0; i < pow; i++)
        {
            Powgague[i].SetActive(true);
            Debug.Log("pow");
        }
    }
    void Coin_Text(int coin)
    {
        CoinText.text = coin.ToString("D2");
    }

    public void Exit()
    {
        SceneManager.LoadScene("GameScene");
    }
   
}
