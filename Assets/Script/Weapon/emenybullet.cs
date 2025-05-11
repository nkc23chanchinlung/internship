using UnityEngine;

public class emenybullet : MonoBehaviour
{
    int speed=20;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        Destroy(gameObject, 3f);
       
       
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Destroy(gameObject);
            
            player.GetDamage();
            
        }
       
    }
}
