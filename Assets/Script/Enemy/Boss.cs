using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Boss : Enemy
{
    bool Isbattle = false;
    [SerializeField] private SphereCollider searchArea;
    [SerializeField] GameObject Gun;
    bool idle = false;
    public bool hasHit { get; set; }

    bool twowave = false;

    BoxCollider hitBox;
    float waittimer = 0f;
    [SerializeField] float Atkdistance;
    [SerializeField] GameObject redcircle;
    [SerializeField] GameObject Lazer;

    [SerializeField] AudioClip[] AttackSE;
    AudioSource audioSource;


    enum BossStatus { Walk, Melee, Long_range,LazerAttack,wave2 ,num };
    BossStatus bossstatus = BossStatus.Walk;
    private BossStatus previousBossStatus = BossStatus.Walk;
    private int lastAttackType = 0; // 0: なし, 1: Melee, 2: Long_range

    PlayableDirector director;
    [SerializeField] PlayableAsset wave2;
    [SerializeField]GameObject[] EnemySpawner;
    bool wave2ended = true;

    Animator bossani;
    Animation anim;


    public override void Init()
    {
        base.Init();
        target = Player;
        shooting = false;
        meleeing = false;
        hitBox = Hand.GetComponent<BoxCollider>();
        hitBox.enabled = false;
        previousBossStatus = BossStatus.Walk;
        lastAttackType = 0;
        audioSource = GetComponent<AudioSource>();
        director=GetComponent<PlayableDirector>();
        bossani = GetComponent<Animator>();
        anim=GetComponent<Animation>();
    }

    private void OnEnable()
    {
        enemyAnimetor = new ObjAnimetor(1f, gameObject);
        Init();
        Isbattle = true;
        Attack = 30;
        
    }

    private void FixedUpdate()
    {
        if (!Isbattle) return;

        Vector3 velocity = agent.velocity;
        speed = velocity.magnitude;
    }

    private void Update()
    {

        Debug.Log(twowave);

        if (target == null) return;
        if (((float)Hp / (float)MaxHp) * 100 <= 50 && !twowave) //50%以下で第2形態へ
        {
            bossstatus = BossStatus.wave2;

        }

        uimanager.BossHpbar(Hp, MaxHp);

        if (Hp <= 0) Die();

        agent.SetDestination(target.position);
        Vector3 velocity = agent.velocity;
        speed = velocity.magnitude;

        // 攻撃中（近距離or遠距離）はステータス変更しない
        if (!meleeing && !shooting&&!twowave)
        {
            StatusChange(3f);
        }

        StatusInfo();

        enemyAnimetor.Animetor(false, false, shooting, meleeing, idle, speed * 5);

        previousBossStatus = bossstatus; // 次のフレームで進入判定に使用
    }
     private IEnumerator FinalAttack(GameObject　SpawnObj, Transform pos,int spawmamount)
    {
        Debug.Log("wave2");
        
        wave2ended = false;
        twowave = true;
        director.playableAsset = wave2;
        director.Play();
        
            GameObject moster = Instantiate(SpawnObj, pos.position, pos.rotation);
            Swordmen enemy = moster.GetComponent<Swordmen>();
            enemy.target = Player;
            enemy.Init();

        
        yield return null;

    }
    void StatusChange(float waittime)
    {
        Debug.Log(bossstatus);

        float distance = Vector3.Distance(transform.position, target.position);

        waittimer += Time.deltaTime;

        
       
        if (distance >= Atkdistance)
        {
            bossstatus = BossStatus.Walk;
            waittimer = 0f; // 離れたら待機タイマーもリセット
        }
        else if (waittimer >= waittime && !meleeing && !shooting&&wave2ended)
        {
            int chosen = Random.Range(1, (int)BossStatus.num-1);

            // 連続同じ攻撃を回避
            while(lastAttackType != 0 && chosen == lastAttackType)
            {
                chosen = Random.Range(1, (int)BossStatus.num-1);
            }

            lastAttackType = chosen;
            bossstatus = (BossStatus)chosen;
            waittimer = 0f;
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

    void StatusInfo()
    {
        switch (bossstatus)
        {
            case BossStatus.Walk:
                Walk();
                break;

            case BossStatus.Melee:
                agent.isStopped = true;
                Meleeattack(5f);
                if (meleeing)
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
               
                break;

            case BossStatus.Long_range:
                agent.isStopped = true;

                // ステータスがLong_rangeに変わった瞬間だけコルーチン開始
                if (previousBossStatus != bossstatus)
                {
                    audioSource.PlayOneShot(AttackSE[1]);
                    StartCoroutine(LongRangeAttackCoroutine());
                }
                break;
                case BossStatus.LazerAttack:
                agent.isStopped = true;
                if (previousBossStatus != bossstatus)
                {
                    StartCoroutine(LazeAttack());
                }
                break;
            case BossStatus.wave2:
                if (!twowave)
                {
                    agent.isStopped = true;
                    StartCoroutine(FinalAttack(EnemySpawner[0], transform, 5));
                }
                
                break;
        }
    }

    void Idle()
    {
        if (!Isbattle) return;
        agent.isStopped = true;
        idle = true;
    }

    void Walk()
    {
        idle = false;
        shooting = false;
        meleeing = false;
        agent.isStopped = false;
    }
    IEnumerator LazeAttack()
    {
        // 赤い円を生成
        Vector3 spawnPos = new Vector3(transform.position.x, 6f, transform.position.z);
        GameObject red = Instantiate(redcircle, spawnPos, Quaternion.Euler(90, 0, 0));

        float duration = 2.0f;
        float elapsed = 0f;

        //プレイヤーを追跡
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // ターゲット（プレイヤー）の現在位置に追従させつつ、Yは6固定
            Vector3 targetPos = target.transform.position;
            targetPos.y = 6f;  // ← これが重要！

            red.transform.position = targetPos;

            yield return null;
        }

        // 追跡終了後、0.5秒待機
        yield return new WaitForSeconds(1f);

        // レーザーを赤い円の現在位置に生成
        Instantiate(Lazer, red.transform.position, Quaternion.Euler(0, 0, 0));

        // 必要に応じて赤い円を消す
        Destroy(red);
    }

    protected override void Die()
    {
        base.Die();
        SceneManager.LoadScene("GameClearScene");
    }

    public void HitOn()
    {
        hasHit = false;
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
    public void AttackSound()
    {
        audioSource.PlayOneShot(AttackSE[0]);
    }
    
    // 遠距離攻撃専用コルーチン
    private IEnumerator LongRangeAttackCoroutine()
    {
        shooting = true;

        float duration = 2.0f;     // 攻撃持続時間    
        float elapsed = 0f;
        float shootInterval = 0.05f; // 射撃間隔
        float shootTimer = 0f;

        while (elapsed < duration)
        {
            // 回る
            transform.Rotate(0f, 360f * Time.deltaTime, 0f);

            elapsed += Time.deltaTime;
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootInterval)
            {
                shootTimer -= shootInterval;
                // 射撃
                StartCoroutine(Shoot(bulletprefab, 0f, Gun, new Vector3(0f, 0f, 2f), "EnemyAtk"));
            }

            yield return null;
        }

        shooting = false;
        bossstatus = BossStatus.Walk; // 攻撃終了後は歩きに戻す
    }
   
}