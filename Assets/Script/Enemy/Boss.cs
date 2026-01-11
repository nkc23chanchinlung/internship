using DG.Tweening;
using System.Collections;
using System.Threading;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Boss : Enemy
{
    bool Isbattle = false;
    [SerializeField] private SphereCollider searchArea;
    [SerializeField] GameObject Gun;
    bool idle = false;

    BoxCollider hitBox;
    float long_timer = 0f;
    float waittimer = 0f;
    int attackType;
    [SerializeField] float Atkdistance;
    
    Animator bossanimator;
    enum BossStatus { Walk, Melee, Long_range, num };
    BossStatus bossstatus = BossStatus.Walk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Init()
    {
        base.Init();
        bossanimator = GetComponent<Animator>();
        target = Player;
        shooting = false;
        meleeing = false;
        hitBox = Hand.GetComponent<BoxCollider>();
        hitBox.enabled = false;
        


    }
    private void OnEnable()
    {
        
        enemyAnimetor = new ObjAnimetor(1f, gameObject); //敵のアニメーションを管理するクラスの初期化
        Init();
        Isbattle=true;
        Attack = 30;
    }
    private void FixedUpdate()
    {
        if (!Isbattle) return;
        
        Vector3 velocity = agent.velocity;                                          //NavMeshAgentの速度を取得
        speed = velocity.magnitude;
        
    }

    private void Update()
    {
       

        if (target == null) return;

        uimanager.BossHpbar(Hp, MaxHp);
        
        if (Hp<=0) Die();                      //HPが0以下なら死亡処理

        agent.SetDestination(target.position);
        Vector3 velocity = agent.velocity;      //NavMeshAgentの速度を取得
        speed = velocity.magnitude;
        if (!meleeing || !shooting)
        {
            StatusChange(3f);
        }
        StatusInfo();
        enemyAnimetor.Animetor(false, false, shooting, meleeing, idle, speed * 5); //アニメーションの実行

    }
    //状態変化条件メソッド
    void StatusChange(float waittime)
    {
        Debug.Log(bossstatus);
        float distance = Vector3.Distance(transform.position, target.position);
        


        waittimer += Time.deltaTime;
        if(distance>= Atkdistance) bossstatus= BossStatus.Walk;
        else if (waittimer >= waittime&&!meleeing&&!shooting)
        {
            bossstatus= (BossStatus)Random.Range(1, (int)BossStatus.num);
            
            waittimer = 0;
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerAtk")
        {
            Bullet PlayerAtk = other.gameObject.GetComponent<Bullet>();
            Destroy(other.gameObject);
            int damage = PlayerAtk.damage;
            GetDamage(damage - defense, 2.0f);
        }
    }
    //状態情報メソッド
    void StatusInfo()
    {
        switch (bossstatus)
        {
            //case BossStatus.Idle:
            //    Idle();
            //    break;
            case BossStatus.Walk:
                Walk();
                break;
            case BossStatus.Melee:
                
                    agent.isStopped = true;
                    Meleeattack(5f);
                


                break;
            case BossStatus.Long_range:
                
                    agent.isStopped = true;
                shooting = true;
                Long_range(3f);
                
                

                break;

        }
    }

    void Idle()
    {
       if (!Isbattle) return;
        agent.isStopped = true;
        idle = true;
    }
    //移動メソッド
    void Walk()
    {
        idle = false;
        shooting = false;
        meleeing = false;
        agent.isStopped = false;
        
    }
    
       
    
    //遠距離攻撃メソッド
    void Long_range(float time)
    {
        transform.Rotate(0f, 360f * Time.deltaTime, 0f);
        StartCoroutine(Shoot(bulletprefab, 3f, Gun, new Vector3(0f, 0f, 2f),"Enemy"));
        long_timer += Time.deltaTime;
        if (long_timer >= time)
        {
        long_timer = 0;
        }
    }
    public void HitOn()
    {
        hitBox.enabled = true;
    }

    public void HitOff()
    {
        hitBox.enabled = false;
    }
 
    public void MelleAtkStart()
    {
        meleeing = true;
    }
    public void MelleAtkComplete()
    {
        meleeing = false;

    }

}
