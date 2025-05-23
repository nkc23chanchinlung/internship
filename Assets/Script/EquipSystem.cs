
using UnityEngine;
using System.Collections.Generic;

public class EquipSystem : MonoBehaviour
{
   [SerializeField] GameObject[] weaponsPrefabs;
    List<GameObject> equippedweapons = new List<GameObject>();
    Gun gun;
    int eqyuippedIndex = 0;
    public bool IsReloading { get; private set; } = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i < weaponsPrefabs.Length; i++)
        {
            equippedweapons.Add(weaponsPrefabs[i]);
        }

        equippedweapons[0].SetActive(true);
        
       gun = equippedweapons[0].GetComponent(typeof(Gun)) as Gun;

    }

    // Update is called once per frame
    void Update()
    {
        WeaponChange();
        IsReloading = gun.IsReloading;


    }
    void WeaponChange()
    {
        if (!gun.IsReloading)
        {
            if (Input.GetKeyDown(KeyCode.Q) && eqyuippedIndex == 0)
            {
                eqyuippedIndex = 1;
                gun = equippedweapons[1].GetComponent(typeof(Gun)) as Gun;         //武器切り替えるとき情報取得

            }

            else if (Input.GetKeyDown(KeyCode.Q) && eqyuippedIndex == 1)
            {
                eqyuippedIndex = 0;
                gun = equippedweapons[0].GetComponent(typeof(Gun)) as Gun;         //武器切り替えるとき情報取得


            }

            if (eqyuippedIndex == 0)
            {
                equippedweapons[0].SetActive(true);
                equippedweapons[1].SetActive(false);
            }
            else if (eqyuippedIndex == 1)
            {
                equippedweapons[0].SetActive(false);
                equippedweapons[1].SetActive(true);
            }
        }
    }
}
