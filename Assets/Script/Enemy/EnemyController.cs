using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;




public class EnemyController : EnemyMovement, IEnemyMovement
{
    public int objnum { get; set; } = 1; //“G‚ÌƒIƒuƒWƒFƒNƒg”Ô†
    [SerializeField]UIManager uimanager;
    enum Status { Idle, Doubt, Hostile,Attack, num };//“G‚Ìó‘Ô
    Status status = Status.Hostile;
    [Header("õ“G”ÍˆÍ")]
    [Tooltip("“G‚Ìõ“G”ÍˆÍ")]
    [Range(1, 10)] //Inspectorã‚Å‚Ì•\¦
    [SerializeField] int Enemies;
    [SerializeField] int speed;
    [SerializeField] int leagth;
   
    [SerializeField] Transform target;
    Transform Player;
    Transform House;

    [Header("“G‚ÌƒXƒe[ƒ^ƒX")]
    [SerializeField] public int MaxHp { get; private set; } = 100; //“G‚ÌHP
    [SerializeField]public int Hp { get; set; } = 100; //“G‚ÌHP   
    [SerializeField] int Attack;
    

    private const float intervalX = 0.1f;
    private const float intervalY = 0.1f;
   
    private float angervalue;//“G‚Ì“{‚è’l
    private float dinstance;
    private float targetedge;
    NavMeshAgent agent;
    [SerializeField]GameObject bulletprefab;
    [SerializeField] GameObject Damageprefeb;
   
    Collider targetcol;
    Vector3 targetsize;//–Ú•W‚Ì‘å‚«‚³
   

    [SerializeField] Text Debug_Status;


    private void Awake()
    {
        uimanager = GameObject.Find("-----UI-----").GetComponent<UIManager>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        House = GameObject.Find("House").transform;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        target = House;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 3f;

        //debug—p‚ÌƒeƒLƒXƒg•\¦
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
        else if(angervalue == 0) target = House; //“{‚è’l‚ª0‚Ì‚Í‰Æ‚ğƒ^[ƒQƒbƒg‚É‚·‚é
        
    }
    /// <summary>
    /// õ“G‚ÌŠÖ”
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
    /// s“®‚ÌŠÖ”
    /// </summary>
    private void movement()
    {

        //ó‘ÔØ‚è‘Ö‚¦
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
                if (!cooldown)
                {
                    agent.isStopped = true;
                StartCoroutine(Shoot(1,bulletprefab));
                }
                break;
          
        }

      
    }
   
    // ’e‚Ì¶¬‚ğ‚Ü‚Æ‚ß‚½ŠÖ”

    //“G‘Î‚Ìó‘Ô‚Ìˆ—
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

    public void GetDamage(int damage)
    {
        
        angervalue += 60;
        uimanager.Damagevalue(transform, damage);
        Hp -= damage;
        
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet playerbullet = collision.gameObject.GetComponent<Bullet>();
            Destroy(collision.gameObject);
            int damage = playerbullet.damage;
            GetDamage(damage);

        }

    }

}
    




    

   
