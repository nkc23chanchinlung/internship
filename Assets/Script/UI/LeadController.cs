using UnityEngine;

public class LeadController : MonoBehaviour
{
    [SerializeField]Transform homepos;
    Quaternion targetRotation;
    [SerializeField] Transform Playerpos;
    [SerializeField] float distance = 5f; // –Ú•W‚Ü‚Å‚Ì‹——£

    private Quaternion fixedRotationOffset = Quaternion.Euler(90, 0, 90);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Playerpos=GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Playerpos.position+transform.forward * distance + Playerpos.up * distance;

        Vector3 direction = (homepos.position - transform.position).normalized;
        

        // –Ú•W•ûŒü‚ÉŠî‚Ã‚¢‚½‰ñ“]‚ðŒvŽZ
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            targetRotation = Quaternion.Slerp(transform.rotation, lookRotation * fixedRotationOffset, Time.deltaTime * 2f);
            transform.rotation = targetRotation;
        }

        
    }
}