using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField]GameObject boss;
    [SerializeField] UIManager UIManager;
    [SerializeField] GameObject wall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (boss != null)
            {
                boss.SetActive(true);
                wall.SetActive(true);
                UIManager.WarningImg(2f);
                gameObject.SetActive(false);
            }
        }
    }
}
