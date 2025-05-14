using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    List<UnityEngine.GameObject> weapons = new List<UnityEngine.GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapons.Add(Resources.LoadAll<UnityEngine.GameObject>("Prefabs/Weapon")[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
