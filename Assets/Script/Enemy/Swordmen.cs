using UnityEngine;

public class Swordmen : EnemyController
{
    private void Awake()
    {
        Init();//èâä˙âª
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Init()
    {
        MaxHp = 200; //ìGÇÃç≈ëÂHP
        Hp = 200;
        Attack = 20;
        defense = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet playerbullet = collision.gameObject.GetComponent<Bullet>();
            Destroy(collision.gameObject);
            int damage = playerbullet.damage;
            GetDamage(damage-defense, 2.0f);

        }

    }
}
