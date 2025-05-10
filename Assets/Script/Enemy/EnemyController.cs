using UnityEngine;

public class EnemyController : MonoBehaviour
{

    enum State { Idle, Doubt, Hostile, num };//�G�̏��
    State state = State.Idle;
    [Header("���G�͈�")]
    [Tooltip("�G�̍��G�͈�")]
    [Range(1, 10)] //Inspector��ł̕\��
    [SerializeField] int Enemies;
    [SerializeField] int speed;
    [SerializeField] int leagth;
    private const float intervalX=0.1f;
    private const float intervalY=0.1f;
    [SerializeField] Transform target;
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
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.tag == "Player")
                    {
                        target = hit.collider.transform;
                        Debug.Log("�G�̏��" + state);
                        break;
                    }
                    else
                    {
                        state = State.Idle;
                        Debug.Log("�G�̏��" + state);
                    }
                }
                Debug.DrawRay(transform.position, (transform.forward+new Vector3(intervalX * j, intervalY*i, 0))* leagth, Color.red);
               
            }
        }

    }
    void movement()
    {
        transform.position+=transform.forward * Time.deltaTime*speed;
    }
}
