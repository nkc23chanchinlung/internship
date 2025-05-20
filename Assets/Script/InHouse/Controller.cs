using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]LayerMask layerMask;
    [SerializeField] GameObject[] Weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
            Weapon=GameObject.FindGameObjectsWithTag("Weapon");


    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Weapon.Length; i++)
        {
            Weapon[i].GetComponent<Outline>().enabled = false;
        }
        Vector3 mospos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mospos);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);    //Debug—p************

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f,layerMask))
        {
            
           Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.GetComponent<Outline>().enabled = true;

        }
        
    }
}
