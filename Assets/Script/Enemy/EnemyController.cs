using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class EnemyController : MonoBehaviour
{
    public int objnum { get; set; } = 1; //敵のオブジェクト番号
    enum Status { Idle, Doubt, Hostile,Attack, num };//敵の状態
    Status status = Status.Hostile;
    [Header("索敵範囲")]
    [Tooltip("敵の索敵範囲")]
    [Range(1, 10)] //Inspector上での表示
    [SerializeField] int Enemies;
    [SerializeField] int speed;
    [SerializeField] int leagth;
   
    [SerializeField] Transform target;
    Transform Player;
    Transform House;

    [Header("敵のステータス")]
    [SerializeField] public int MaxHp { get; private set; } = 100; //敵のHP
    [SerializeField]public int Hp { get; set; } = 100; //敵のHP   
    [SerializeField] int Attack;
    

    private const float intervalX = 0.1f;
    private const float intervalY = 0.1f;
    private float cooldown = 3f;
    private float angervalue;//敵の怒り値
    private float dinstance;
    private float targetedge;
    NavMeshAgent agent;
    [SerializeField]GameObject bulletprefab;
   
    Collider targetcol;
    Vector3 targetsize;//目標の大きさ
   

    [SerializeField] Text Debug_Status;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        House = GameObject.Find("House").transform;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    private void Debug_text()
    {
        Debug_Status.text = "Status:" + status.ToString() + "\n" +
            "target:" + target.gameObject.name.ToString() + "\n" +
            "anger:" + angervalue.ToString() + "\n";
    }
    void angerprocess()
    {
        if (angervalue == 100) target = Player;
        else if(angervalue == 0) target = House; //怒り値が0の時は家をターゲットにする
        
    }
    /// <summary>
    /// 索敵の関数
    /// </summary>
    void visibility()
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
    private void movement()
    {

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

        switch (status)
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

            case Status.Attack:
                transform.LookAt(target);
                if (cooldown <= 0)
                {
                    agent.isStopped = true;
                    Shoot();
                cooldown = 3f;
                }
                break;
          
        }

        // クールダウンの時間を減らす
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    // 弾の生成をまとめた関数
    void Shoot()
    {
        
        Instantiate(
            bulletprefab,
            transform.position + transform.forward,
            transform.rotation
        );
    }
    //敵対の状態の処理
    void Hostile(RaycastHit hit)
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

    public void GetDamage()
    {
        
        angervalue += 60;
        Debug.Log("GetDamage");

    }
}
    




    

   
