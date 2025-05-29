using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : PreViewController
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
          for(int i=0;i<Weapon.Length; i++)
        {
            previewobj_dic.Add(Weapon[i], i);
        }


    }

    // Update is called once per frame
    void Update()
    {
       

        for (int i = 0; i < Powgague.Length; i++)
        {
            Powgague[i].SetActive(false);
        }


        if (ItemChoose() != null)
        {
            ItemInfo(ItemChoose());
            ItemInfoPanel.SetActive(true);
        }
        else ItemInfoPanel.SetActive(false);


    }
    void ItemInfo(GameObject target)
    {
        ItemName.text = target.name;
        
    }
    GameObject ItemChoose()
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            Weapon[i].GetComponent<Outline>().enabled = false;
        }
        Vector3 mospos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mospos);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);    //Debugóp************

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {

           
            hit.collider.gameObject.GetComponent<Outline>().enabled = true;
            GetInfo(hit.collider.gameObject);
            SetPreviewNum(hit.collider.gameObject);
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

    }
    //ñ¢äÆê¨ÅAîzóÒÇÃÇ«Ç±ÇÎÇÕïœ
    void InfoValuegague(int pow,int repair)
    {
        for(int i = 0; i < pow; i++)
        {
            Powgague[i].SetActive(true);
            Debug.Log("pow");
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("GameScene");
    }
   
}
