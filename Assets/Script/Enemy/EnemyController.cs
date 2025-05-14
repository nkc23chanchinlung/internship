using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class EnemyController : MonoBehaviour
{
    public int objnum { get; set; } = 1; //�G�̃I�u�W�F�N�g�ԍ�
    enum Status { Idle, Doubt, Hostile,Attack, num };//�G�̏��
    Status status = Status.Hostile;
    [Header("���G�͈�")]
    [Tooltip("�G�̍��G�͈�")]
    [Range(1, 10)] //Inspector��ł̕\��
    [SerializeField] int Enemies;
    [SerializeField] int speed;
    [SerializeField] int leagth;
   
    [SerializeField] Transform target;

    [Header("�G�̃X�e�[�^�X")]
    [SerializeField] public int MaxHp { get; private set; } = 100; //�G��HP
    [SerializeField]public int Hp { get; set; } = 100; //�G��HP   
    [SerializeField] int Attack;
    

    private const float intervalX = 0.1f;
    private const float intervalY = 0.1f;
    float cooldown = 3f;
    float angervalue;//�G�̓{��l
    float dinstance;
    float targetedge;
    NavMeshAgent agent;
    [SerializeField] UnityEngine.GameObject bulletprefab;
   
    Collider targetcol;
    Vector3 targetsize;//�ڕW�̑傫��

    [SerializeField] Text Debug_Status;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = UnityEngine.GameObject.Find("House").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 3f;

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



        visibility();
        movement();
       
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Debug_text()
    {
        Debug_Status.text = "Status:" + status.ToString() + "\n" +
            "Hp:" + Hp.ToString() + "\n" +
            "Dinstance:" + dinstance.ToString() + "\n" +
            "Target:" + target.ToString() + "\n" +
            "TargetEdge:" + targetedge.ToString() + "\n" +
            "AngerValue:" + angervalue.ToString();
    }
    /// <summary>
    /// ���G�̊֐�
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
    /// �s���̊֐�
    /// </summary>
    void movement()
    {

        //��Ԑ؂�ւ�
        dinstance = Vector3.Distance(target.position, transform.position);

        if(dinstance<=targetedge+2) status = Status.Attack;
        
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

        // �N�[���_�E���̎��Ԃ����炷
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

    // �e�̐������܂Ƃ߂��֐�
    void Shoot()
    {
        
        Instantiate(
            bulletprefab,
            transform.position + transform.forward,
            transform.rotation
        );
    }
    //Hostile��Ԃ̏���
    void Hostile(RaycastHit hit)
    {
        target = hit.collider.transform;
        status = Status.Hostile;
        if (hit.collider.tag == "Player")
        {
            targetcol = hit.collider.GetComponent<CapsuleCollider>();
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
        target = UnityEngine.GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("GetDamage");

    }
}
    




    

   
