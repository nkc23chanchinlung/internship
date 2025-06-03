using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{
     Image lifebar;
    Enemy enemyController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyController=GetComponentInParent<Enemy>();
        lifebar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
