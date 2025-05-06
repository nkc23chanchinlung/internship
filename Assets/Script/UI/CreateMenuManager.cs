
using UnityEngine;



public class CreateMenuManager : MonoBehaviour
{
    [SerializeField] GameObject testcube;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CT_cube()
    {
        gameObject.SetActive(false);

        Instantiate(testcube);


    }
}
