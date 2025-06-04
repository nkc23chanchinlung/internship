using UnityEngine;
using UnityEngine.UI;
public class Swordmen : Enemy
{
    
    private void Awake()
    {
        

        
       

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();//初期化
        lifebar.SetActive(false);
        agent.stoppingDistance =1f;
        status = Status.Hostile; //初期状態を敵対に設定
        enemyAnimetor = new ObjAnimetor(1f, gameObject); //敵のアニメーションを管理するクラスの初期化
        

    }
    protected override void Init()
    {
        base.Init(); //親クラスの初期化を呼び出す
        
        MaxHp = 200; //敵の最大HP
        Hp = MaxHp;
        Attack = 20;
        defense = 10;
        target = Player; //ターゲットをプレイヤーに設定
        agent.SetDestination(target.position);

    }

    // Update is called once per frame
    void Update()
    {
       
        Setlifebar(lifebar, Hp, MaxHp); //ライフバーの更新
        movement();
        visibility();
        Debug_text();

        Vector3 velocity = agent.velocity; //NavMeshAgentの速度を取得
        speed = velocity.magnitude; //速度の大きさを取得
        enemyAnimetor.Animetor(false, speed * 5, false, false, false, atking, false); //アニメーションの実行
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerAtk")
        {
            Bullet PlayerAtk = collision.gameObject.GetComponent<Bullet>();
            Destroy(collision.gameObject);
            int damage = PlayerAtk.damage;
            GetDamage(damage-defense, 2.0f);

        }

    }
    protected override void movement()
    {
        base.movement();
       
        switch (status)                  //状態による行動の切り替え
        {
            case Status.Hostile:
                if (target != null)
                {
                    agent.isStopped = false;
                    agent.SetDestination(target.position);
                }
                else
                {
                    agent.isStopped = true;
                }
                break;

            case Status.Attack:            //攻撃制御
                transform.LookAt(target);
                if (!atking)
                {
                  agent.isStopped = true;
                 
                    
                  Meleeattack(1.0f);
                    

                }
                break;

        }
    }
}
