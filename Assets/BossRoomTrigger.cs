using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField]GameObject boss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (boss != null)
            {
                boss.SetActive(true);
            }
        }
    }
}
