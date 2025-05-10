using UnityEngine;

public class EnemyController : MonoBehaviour
{

    enum Status { Idle, Doubt, Hostile,Attack, num };//“G‚Ìó‘Ô
    Status status = Status.Hostile;
    [Header("õ“G”ÍˆÍ")]
    [Tooltip("“G‚Ìõ“G”ÍˆÍ")]
    [Range(1, 10)] //Inspectorã‚Å‚Ì•\¦
    [SerializeField] int Enemies;
    [SerializeField] int speed;
    [SerializeField] int leagth;
    private const float intervalX=0.1f;
    private const float intervalY=0.1f;
    [SerializeField] Transform target;

    [Header("“G‚ÌƒXƒe[ƒ^ƒX")]
    [SerializeField]public int Hp { get; set; } = 100; //“G‚ÌHP   
    [SerializeField] int Attack;
    [SerializeField] public int MaxHp { get; private set; } = 100; //“G‚ÌHP
    float dinstance;

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
                        Debug.Log("“G‚Ìó‘Ô" + status);
                        break;
                    }
                    else
                    {
                        status = Status.Idle;
                        Debug.Log("“G‚Ìó‘Ô" + status);
                    }
                }
                Debug.DrawRay(transform.position, (transform.forward+new Vector3(intervalX * j, intervalY*i, 0))* leagth, Color.red);
               
            }
        }

    }
    void movement()
    {
        dinstance = Vector3.Distance(target.position, transform.position);
        if (dinstance < 3f)
        {
            status = Status.Attack;
            Debug.Log("Attack");
        }
        else
        {
            status = Status.Hostile;
            Debug.Log("Hostile");
        }
        if (status == Status.Hostile)
            transform.position += transform.forward * Time.deltaTime * speed;
    }
}
