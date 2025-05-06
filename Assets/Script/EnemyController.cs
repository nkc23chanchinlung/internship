using UnityEngine;

public class EnemyController : MonoBehaviour
{

    enum State { Idle, Doubt, Hostile, num };//ìGÇÃèÛë‘
    State state = State.Idle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        visibility();
    }
    void visibility()
    {

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, 100);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
        }
        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);


    }
}
