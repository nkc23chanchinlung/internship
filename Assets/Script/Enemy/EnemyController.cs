using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public enum enemylist
{
    shooter,
    swordman,
    secrordboss,
    boss,
    num,
    
}


public class EnemyController : EnemyMovement
{
    [SerializeField]
    enemylist enemylist; //敵の種類を列挙型で定義
    [SerializeField]UIManager uimanager;
    enum Status { Idle, Doubt, Hostile,Attack, num };            //敵の状態
    Status status = Status.Hostile;
    [Header("索敵範囲")]
    [Tooltip("敵の索敵範囲")]
    [Range(1, 10)]                                                //Inspector上での表示
    [SerializeField] int Enemies;
    [SerializeField] int speed;
    [SerializeField] int leagth;
   

   
    [SerializeField] Transform target;
    Transform Player;
    Transform House;

    [Header("敵のステータス")]
    protected int MaxHp;   //敵の最大HP
    public int Hp;      //敵のHP   
    public int Attack;  //攻撃力
    public int defense; //防御力



    private const float intervalX = 0.1f;
    private const float intervalY = 0.1f;
   
    private float angervalue;                                       //敵の怒り値
    private float dinstance;
    private float targetedge;

    [SerializeField]GameObject lifebar;
    NavMeshAgent agent;
    [SerializeField]GameObject bulletprefab;
    [SerializeField] GameObject Damageprefeb;
   
    Collider targetcol;
    Vector3 targetsize;                                              //目標の大きさ
   

    [SerializeField] Text Debug_Status;

    [SerializeField] GameObject Hand;
    Rigidbody rb;

    ObjAnimetor enemyAnimetor; //敵のアニメーションを管理するクラス

    private void Awake()
    {
        uimanager = GameObject.Find("-----UIManager-----").GetComponent<UIManager>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        House = GameObject.Find("House").transform;
        lifebar=GetComponentInChildren<Lifebar>().gameObject;
        enemyAnimetor = new ObjAnimetor(1f, gameObject); //敵のアニメーションを管理するクラスの初期化
        rb = GetComponent<Rigidbody>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifebar.SetActive(false);
        target = House;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 3f;

        //debug用のテキスト表示
        try
        {
            Debug_Status = GetComponentInChildren<Text>();
        }
        catch 
        {
            Debug_Status = null;
            Debug.LogError("Debug_Status is not assigned in the inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Debug_Status != null) Debug_text();

        angerprocess();
        visibility();
        movement();
       
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
        
    }
    protected void Debug_text() //*****Debug用のテキスト表示関数*****
    {
        Debug_Status.text = "Status:" + status.ToString() + "\n" +
            "target:" + target.gameObject.name.ToString() + "\n" +
            "anger:" + angervalue.ToString() + "\n";
    }
    protected void angerprocess()
    {
        if (angervalue >= 90) target = Player;
        else if(angervalue <= 0) target = House; //怒り値が0の時は家をターゲットにする
        
    }
    /// <summary>
    /// 索敵の関数
    /// </summary>
    protected void visibility()
    {
        for (int i=-Enemies; i< Enemies; i++)
        {

            for (int j = -Enemies; j < Enemies; j++)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, (transform.forward + new Vector3(intervalX * i, 0, 0)), out hit, leagth);
                if (hit.collider != null)
                {
                   
                    if (hit.collider.tag == "Player")
                    {
                       Hostile(hit);
                    }
                    else if (hit.collider.tag == "GameObj")
                    {
                        Hostile(hit);

                    }
                    
                }
                
                Debug.DrawRay(transform.position, (transform.forward+new Vector3(intervalX * j, intervalY*i, 0))* leagth, Color.red);
               
            }
        }

    }
    /// <summary>
    /// 行動の関数
    /// </summary>
    protected void movement()
    {
        
        if(enemylist==enemylist.swordman)
        enemyAnimetor.Animetor(false, agent.speed*5,false, false, false,atking, false); //アニメーションの実行
        //状態切り替え
        dinstance = Vector3.Distance(target.position, transform.position);
        if (dinstance >= 5) angervalue--;
        angervalue = Mathf.Clamp(angervalue, 0, 100);

        if (dinstance<=targetedge+2) status = Status.Attack;
        
        if (agent == null || !agent.isOnNavMesh)
        {
            Debug.LogWarning("Agent is not on NavMesh or is missing.");
            return;
        }

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
                    if(enemylist == enemylist.shooter)
                    {
                        StartCoroutine(Shoot(bulletprefab, 0.5f));
                    }
                    else if (enemylist == enemylist.swordman)
                    {
                        Meleeattack(1.0f);
                    }
                    
                }
                break;
          
        }

      
    }

    // 弾の生成をまとめた関数

    //敵対の状態の処理
    protected void Hostile(RaycastHit hit)
    {
        target = hit.collider.transform;
        status = Status.Hostile;
        if (hit.collider.tag == "Player")
        {
            targetcol = hit.collider.GetComponent<CapsuleCollider>();
            angervalue += 5;
        }
        else if (hit.collider.tag == "GameObj")
        {
            targetcol = hit.collider.GetComponent<BoxCollider>();
        }
      
        targetsize = targetcol.bounds.size;
      
        targetedge = targetsize.magnitude;
    }

    protected void GetDamage(int damage,float hidetime)    //敵がダメージを受ける関数
    {
        lifebar.SetActive(true);
        angervalue += 60;
        uimanager.Damagevalue(transform, damage);
        Hp -= damage;
        uimanager.displayeffect(lifebar.GetComponent<Image>(), null, 1.0f);
        Invoke("hidelifebar", hidetime);
    }

        IEnumerator hidelifebar()     　　　　　　　//ライフバーを非表示にするコルーチン
    {
        uimanager.hideeffect(lifebar.GetComponent<Image>(), 1.0f);
        return null;
    }


    protected void Meleeattack(float cooldowntime)  　　　　　//アニメーションイベントから呼び出される攻撃関数
    {
        BoxCollider col =Hand.GetComponentInChildren<BoxCollider>();
       
        StartCoroutine(meleeattack(col, cooldowntime));

    }

}
    




    

   
