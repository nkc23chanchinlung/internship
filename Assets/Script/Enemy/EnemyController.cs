using UnityEngine;

public class EnemyController : MonoBehaviour
{

    enum State { Idle, Doubt, Hostile, num };//“G‚Ìó‘Ô
    State state = State.Idle;
    [Header("õ“G”ÍˆÍ")]
    [Tooltip("“G‚Ìõ“G”ÍˆÍ")]
    [Range(1, 10)] //Inspectorã‚Å‚Ì•\¦
    [SerializeField] int Enemies;
    [SerializeField] int speed;
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
                Physics.Raycast(transform.position, (transform.forward + new Vector3(intervalX * i, 0, 0)), out hit, 100);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
                Debug.DrawRay(transform.position, (transform.forward+new Vector3(intervalX * j, intervalY*i, 0))*100, Color.red);
                
            }
        }

    }
    void movement()
    {
        transform.position+=transform.forward * Time.deltaTime*speed;
    }
}
