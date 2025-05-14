using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public int objnum { get; set; } = 1; //�G�̃I�u�W�F�N�g�ԍ�
    enum Status { Idle, Doubt, Hostile,Attack,Attackbuld, num };//�G�̏��
    Status status = Status.Hostile;
    [Header("���G�͈�")]
    [Tooltip("�G�̍��G�͈�")]
    [Range(1, 10)] //Inspector��ł̕\��
    [SerializeField] int Enemies;
    [SerializeField] int speed;
    [SerializeField] int leagth;
    private const float intervalX=0.1f;
    private const float intervalY=0.1f;
    [SerializeField] Transform target;

    [Header("�G�̃X�e�[�^�X")]
    [SerializeField]public int Hp { get; set; } = 100; //�G��HP   
    [SerializeField] int Attack;
    [SerializeField] public int MaxHp { get; private set; } = 100; //�G��HP
    float angervalue;//�G�̓{��l
    float dinstance;
    [SerializeField] GameObject bulletprefab;
    float cooldown = 3f;
    BoxCollider targetcol;
    Vector3 targetsize;
    float targetedge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("House").transform;
    }

    // Update is called once per frame
    void Update()
    {
        visibility();
        movement();
        transform.LookAt(target);
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
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
                        target = hit.collider.transform;

                        break;
                    }
                    else if (hit.collider.tag == "GameObj")
                    {
                       
                        target = hit.collider.transform;
                        targetcol = hit.collider.GetComponent<BoxCollider>();
                        targetsize  = targetcol.size;
                        targetedge = targetsize.magnitude/2;
                        
                    }
                    else 
                    {
                        status = Status.Idle;

                    }
                }
                Debug.DrawRay(transform.position, (transform.forward+new Vector3(intervalX * j, intervalY*i, 0))* leagth, Color.red);
               
            }
        }

    }
    void movement()
    {
        //��Ԑ؂�ւ�
        dinstance = Vector3.Distance(target.position, transform.position);
        
        if (dinstance < 3f)
        {
            status = Status.Attack;

        }
        else if (dinstance <= targetedge)
        {
            status = Status.Attackbuld;
        }
        else
        {
            status = Status.Hostile;

        }
        //��Ԃɉ������s��
        cooldown -= Time.deltaTime;
        if (status == Status.Hostile)
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            
        }

        else if (status == Status.Attack && cooldown <= 0)
        {
            
            Instantiate(bulletprefab, transform.position + (transform.forward), transform.rotation * Quaternion.Euler(0, 0, 0));

            cooldown = 3f;
        }
        else if (status == Status.Attackbuld && cooldown <= 0)
        {
            
            Instantiate(bulletprefab, transform.position + (transform.forward), transform.rotation * Quaternion.Euler(0, 0, 0));
            cooldown = 2f;
        }
    }
    public void GetDamage()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
}
