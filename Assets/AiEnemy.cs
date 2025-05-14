using UnityEngine;
using UnityEngine.AI;

public class AiEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    NavMeshAgent agent;
    [SerializeField]Transform player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        
       
    }
}
