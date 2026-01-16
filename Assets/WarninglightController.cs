using System.Collections;
using UnityEngine;

public class WarninglightController : MonoBehaviour
{
    [SerializeField]
    int rotationSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        gameObject.transform.Rotate(0, rotationSpeed, 0);


    }

    // Update is called once per frame
    
    
}
