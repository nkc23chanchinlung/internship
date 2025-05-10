using UnityEngine;

public class AK47 : MonoBehaviour
{
    [SerializeField] GameObject bulletprefab;
    
    float cooldown = 0.2f;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        cooldown -= Time.deltaTime;
        if (Input.GetMouseButton(0)&&cooldown<=0)
        {
           
            Instantiate(bulletprefab,transform.position+(-transform.forward), transform.rotation*Quaternion.Euler(0,180,0));
            cooldown = 0.2f;
        }
    }
    
    
}
