using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GunMan : Enemy
{
    bool _isInit = false;//初期化フラグ
    bool _isHit = false;
    bool _isAim= false;
    bool _isShoot = false;
    bool _isAtk = false;
    bool _isReload = false;
    bool _isFireAccpt = false;
    [SerializeField] Transform _gun;
    AudioSource _audioSource;


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

    public override void Init()
    {
        base.Init(); //親クラスの初期化を呼び出す

        _audioSource = GetComponent<AudioSource>();
        MaxHp = 200; //敵の最大HP
        Hp = MaxHp;
        //Attack = 10;
        //defense = 10;
        Sponpoint = transform.position; //スポーン位置を設定
                                        //target=GameObject.Find("House").transform; //初期ターゲットを家に設定

        lifebar.SetActive(false);
        agent.stoppingDistance = 1f;
        status = Status.Idle; //初期状態を敵対に設定
        enemyAnimetor = new ObjAnimetor(1f, gameObject); //敵のアニメーションを管理するクラスの初期化
        mat = GetComponentInChildren<Renderer>().material;
        _isInit = true;
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
            //gethit = true;


        }
    }

    void Update()
    {
        if (_isInit == false) return;
        if (GameManager.Instance.GameStop)
        {
            
            GameStop();
            return;

        }
        else
        {
            
            GameContinue();


        }

        //if (bossRoomManager) Debug.Log("bossRoomManager is true");
        _isHit = false;

        if (isDead) return; //死亡している場合は処理を中断

        if (GameManager.Instance.GameStop || GameManager.Instance.IsOpenMoviePlaying) return; //ゲームが停止している場合は処理を中断
        if (Hp <= 0)
        {
            //ボスルームマネージャーが存在する場合、敵のカウントを減らす
            //if (bossRoomManager != null)
            //{
            //    bossRoomManager.enemyCount--;
            //    Debug.Log(bossRoomManager.enemyCount);

            //}
            Die(); //HPが0以下なら死亡処理
        }

        if (target != null)
            agent.SetDestination(target.position);
        Setlifebar(lifebar, Hp, MaxHp); //ライフバーの更新
        angerprocess();

        if (target != null)
        {

            Movement(5);
        }
        visibility();


        Vector3 velocity = agent.velocity;  //NavMeshAgentの速度を取得
        speed = velocity.magnitude;         //速度の大きさを取得
        //アニメーションの実行
        enemyAnimetor.Animetor(_isAtk, _isAim,false,speed);
    }
    /// <summary>
    /// 行動関数
    /// </summary>
    /// <param name="threshold">//閾値</param>
    protected override void Movement(float threshold)
    {
        base.Movement(threshold);
       
        //状態による行動の切り替え
        switch (status)
        {
            case Status.Hostile:
                _isAtk = false;
                if (target != null)
                {
                    agent.isStopped = false;
                    if (agent.isStopped == false)
                        agent.SetDestination(target.position);
                }
                else
                {
                    agent.isStopped = true;
                }
                break;

            case Status.Attack:                                                       //攻撃制御

                _isAtk = true;//transform.LookAt(target);
                agent.isStopped = true;
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

                if (!shooting)
                {
                    //弾を撃つ処理
                    StartCoroutine(Shoot(bulletprefab,Attack, 0.3f,  new Vector3(0, 1.5f, 0),_audioSource));
                   
                }
                break;

            case Status.Idle:
                _isAtk = false;
                Idle(Sponpoint);                                                      //スポーン位置に戻る
                break;

        }

    }
   

    public void GetFireAccpt()
    {

    }


}
