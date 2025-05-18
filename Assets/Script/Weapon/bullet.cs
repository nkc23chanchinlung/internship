using UnityEngine;

/// <summary>
/// ’e‚ÌŠî’êƒNƒ‰ƒX
/// </summary>
public class Bullet : MonoBehaviour
{
    int speed=20;
    public int damage { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
            Bulletprocess();
    }
    public void Bulletprocess()
    {
            transform.position += transform.forward * speed * Time.deltaTime;
    }
   
}
