using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Swordmen : Enemy
{
    bool inited = false;


    void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }
    
    void OnGameStart()
    {
        
        Init();//初期化
        
       


    }
    private void OnTriggerEnter(Collider other) // updated to Collider from Collision
    {
       
        if (other.gameObject.tag == "PlayerAtk")
        {
            Returnmat(0.5f, "MutantMesh");
            Bullet PlayerAtk = other.gameObject.GetComponent<Bullet>();
            Destroy(other.gameObject);
            int damage = PlayerAtk.damage;
            GetDamage(damage - defense, 2.0f);
            

        }
    }
    public override void Init()
    {
        base.Init(); //親クラスの初期化を呼び出す
        
        
       
        MaxHp = 200; //敵の最大HP
        Hp = MaxHp;
        Attack = 10;
        defense = 10;
        Sponpoint = transform.position; //スポーン位置を設定
                                        //target=GameObject.Find("House").transform; //初期ターゲットを家に設定

        lifebar.SetActive(false);
        agent.stoppingDistance = 1f;
        status = Status.Idle; //初期状態を敵対に設定
        enemyAnimetor = new ObjAnimetor(1f, gameObject); //敵のアニメーションを管理するクラスの初期化
        mat = GetComponentInChildren<Renderer>().material;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (isDead) return; //死亡している場合は処理を中断

        if (GameManager.instance.GameStop || GameManager.instance.IsOpenMoviePlaying) return; //ゲームが停止している場合は処理を中断
        Debug.Log("Swordmen Update");
        if (Hp <= 0) Die(); //HPが0以下なら死亡処理

        if (target != null)
            agent.SetDestination(target.position);
        Setlifebar(lifebar, Hp, MaxHp); //ライフバーの更新
        angerprocess();

        if (target != null)
        {
           //Debug_text ();
            movement();
        }
        visibility();
        

        Vector3 velocity = agent.velocity;  //NavMeshAgentの速度を取得
        speed = velocity.magnitude;         //速度の大きさを取得
        //アニメーションの実行
        enemyAnimetor.Animetor(false, speed * 5, false, false, false, meleeing, false); 
    }

    protected override void movement()
    {
        base.movement();
        //状態による行動の切り替え
        switch (status)                                                                
        {
            case Status.Hostile:
                if (target != null)
                {
                    agent.isStopped = false;
                    if(agent.isStopped==false)
                    agent.SetDestination(target.position);
                }
                else
                {
                    agent.isStopped = true;
                }
                break;

            case Status.Attack:                                                       //攻撃制御
                //transform.LookAt(target);
                if (!meleeing)
                {
                  agent.isStopped = true;
                 
                    
                  Meleeattack(1.0f);
                    

                }
                break;

             case Status.Idle:
                Idle(Sponpoint);                                                      //スポーン位置に戻る
                break;

        }
    }

   

}
