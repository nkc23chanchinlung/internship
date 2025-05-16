
using UnityEngine;
using System.Collections.Generic;

public class EquipSystem : MonoBehaviour
{
   [SerializeField] GameObject[] weaponsPrefabs;
    List<UnityEngine.GameObject> equippedweapons = new List<UnityEngine.GameObject>();
    int eqyuippedIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i < weaponsPrefabs.Length; i++)
        {
            equippedweapons.Add(weaponsPrefabs[i]);
        }

        equippedweapons[0].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        WeaponChange();
      
    }
    void WeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Q) && eqyuippedIndex == 0)
            eqyuippedIndex = 1;
        else if (Input.GetKeyDown(KeyCode.Q) && eqyuippedIndex == 1)
            eqyuippedIndex = 0;
       
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
