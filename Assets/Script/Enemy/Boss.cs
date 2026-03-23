using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class Boss : Enemy
{
    //値
    float waittimer = 0f;
    [SerializeField] float Atkdistance;

    //Collider
    BoxCollider hitBox;
    [SerializeField] private SphereCollider searchArea;

    //status
    private int lastAttackType = 0; // 0: なし, 1: Melee, 2: Long_range
    enum BossStatus { Walk, Melee, Long_range, LazerAttack, wave2, num };
    BossStatus bossstatus = BossStatus.Walk;
    private BossStatus previousBossStatus = BossStatus.Walk;

    //Script
    [SerializeField] BossRoomManager bossRoomManager;

    //Object
    [SerializeField] GameObject redcircle;
    [SerializeField] GameObject Lazer;
    [SerializeField] GameObject[] EnemySpawner;
    [SerializeField] GameObject Gun;
    [SerializeField] GameObject SparklesEffect;


    //Timeline
    PlayableDirector director;
    [SerializeField] PlayableAsset wave2timeline;
    [SerializeField] PlayableAsset returntimeline;

    //Sound Effects
    [SerializeField] AudioClip[] AttackSE;
    [SerializeField] AudioClip struckSE;
    AudioSource audioSource;

    //Animation
    Animator bossani;
    Animation anim;

    //flag
    bool wave2ended = true;
    bool Isbattle = false;
    bool returned = false;
    public bool wave2flag { get; private set; } = false;
    bool idle = false;
    public bool wave2clear { get; set; }= false;
    public bool hasHit { get; set; }

    //UI
    [SerializeField] GameObject _GameClearPanel;

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
        director = GetComponent<PlayableDirector>();
        bossani = GetComponent<Animator>();
        anim = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        if (!wave2clear)
        {
            enemyAnimetor = new ObjAnimetor(1f, gameObject);
            Init();
            Isbattle = true;
            Attack = 30;
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameStop) return;

        if (!Isbattle) return;

        Vector3 velocity = agent.velocity;
        speed = velocity.magnitude;
    }

    private void Update()
    {
        if(GameManager.Instance.GameStop) return;

        Debug.Log("wave2clear:"+wave2clear);

        if (target == null) return;
        if (((float)Hp / (float)MaxHp) * 100 <= 50 && !wave2flag&&!wave2clear) //50%以下で第2形態へ
        {
            agent.enabled = false; // NavMeshAgentを一時的に無効化
            bossstatus = BossStatus.wave2;

        }
        if (wave2clear&&!returned) StartCoroutine(Wave2Clear());

        uimanager.BossHpBar(Hp, MaxHp);

        if (Hp <= 0) Die();
        if (agent.enabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
        Vector3 velocity = agent.velocity;
        speed = velocity.magnitude;

        // 攻撃中（近距離or遠距離）はステータス変更しない
        if (!meleeing && !shooting && !wave2flag)
        {
            if (agent.enabled && agent.isOnNavMesh)
            {
                StatusChange(3f);
            }
        }
        // 第2形態クリア後の戻り処理
        if (returned)
        {
            agent.enabled = true;
            if (agent.enabled && agent.isOnNavMesh)
            {
                
                agent.isStopped = false;
                StatusChange(2f); // 第2形態クリア後は少し早めに攻撃
                
                Debug.Log("戻り中");
            }
           
        }

        StatusInfo();

        enemyAnimetor.Animetor(false, false, shooting, meleeing, idle, speed * 5);

        previousBossStatus = bossstatus; // 次のフレームで進入判定に使用
    }
    private IEnumerator Wave2(GameObject SpawnObj, Transform pos, int spawmamount)
    {

        wave2ended = false;
        wave2flag = true;
        agent.isStopped = true;
        director.playableAsset = wave2timeline;
        director.Play();
       

        for (int i = 0; i < spawmamount; i++)
        {

            GameObject moster = Instantiate(SpawnObj, pos.position+new Vector3(i,pos.position.y,i), pos.rotation);
            bossRoomManager.enemyCount++;
            Swordmen enemy = moster.GetComponent<Swordmen>();
            enemy.bossRoomManager = bossRoomManager;
            enemy.Init();
            enemy.target = Player;

        }


        yield return null;

    }
    IEnumerator  Wave2Clear()
    {
        director.playableAsset = returntimeline;
        director.Play();
        returned = true;
        wave2ended = true;
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
        else if (waittimer >= waittime && !meleeing && !shooting && wave2ended)
        {
            int chosen = Random.Range(1, (int)BossStatus.num - 1);

            // 連続同じ攻撃を回避
            while (lastAttackType != 0 && chosen == lastAttackType)
            {
                chosen = Random.Range(1, (int)BossStatus.num - 1);
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
            Instantiate(SparklesEffect, other.transform.position,other.transform.rotation* Quaternion.Euler(90, 0, 0));
            audioSource.PlayOneShot(struckSE);
            Destroy(other.gameObject);
            int damage = (int)PlayerAtk.damage;
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
                if (!wave2flag)
                {
                    agent.isStopped = true;
                    StartCoroutine(Wave2(EnemySpawner[0], transform, 5));
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
        if (agent.isOnNavMesh) agent.isStopped = false;
        
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
            targetPos.y = 6f;  

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
        _GameClearPanel.SetActive(true);

        //SceneManager.LoadScene("GameClearScene");
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
#if true //アニメションイベント用関数グループ
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
    public void wave2erase()
    {
        gameObject.SetActive(false);
        agent.isStopped = true;
    }

    public void TimelinePlayflag()
    {
        agent.isStopped= false;
    }
#endif



}