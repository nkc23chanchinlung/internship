using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    List<GameObject> weapons = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapons.Add(Resources.LoadAll<GameObject>("Prefabs/Weapon")[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
