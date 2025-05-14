using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Accapt : MonoBehaviour
{
    Transform playerpos;
    public bool isIndoor = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerpos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerpos.position + new Vector3(0, 1.5f, 0);
        transform.rotation=Camera.main.transform.rotation;
        if (isIndoor)
        {
            door();
        }
    }
    void door()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("InHouse");
        }
    }
    
}
