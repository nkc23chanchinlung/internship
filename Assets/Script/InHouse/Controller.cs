using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField]LayerMask layerMask;
    [SerializeField] GameObject[] Weapon;
    [SerializeField] GameObject ItemInfoPanel;
    [SerializeField]
    GameObject[] Powgague;
    [SerializeField] Text ItemName;
    [SerializeField] Text Info;
    GameObject target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
            Weapon=GameObject.FindGameObjectsWithTag("Weapon");
           // Powgague= GameObject.FindGameObjectsWithTag("Powgague");
          


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
        if (ItemChoose().gameObject == Weapon[0]) Debug.Log("AK47");
        else if (ItemChoose().gameObject == Weapon[1]) Debug.Log("M4");
        else if (ItemChoose().gameObject == Weapon[2]) Debug.Log("grad");
    }
    GameObject ItemChoose()
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            Weapon[i].GetComponent<Outline>().enabled = false;
        }
        Vector3 mospos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mospos);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);    //Debug—p************

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

    }
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
