using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float damageInterval;   // 何秒ごとにダメージ
    [SerializeField] private int damageAmount;         // 1回あたりのダメージ量
    float duration = 2.0f;
    float timer = 0f;
    float Maxscale = 3f;
    private float nextDamageTime = 0f;                      // 次にダメージを与えていい時間
    private PlayerController playerInRange = null;                    // 現在範囲内にいるプレイヤー（1人前提）

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                playerInRange = player;
                // 入った瞬間にすぐダメージを与えてもOKならここで1回与える
                playerInRange.GetDamage(damageAmount);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = null;
        }
    }

    private void Update()
    {
        transform.localScale=new Vector3(Maxscale-timer,transform.localScale.y , Maxscale - timer);

        if (timer < duration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
        if (playerInRange == null) return;

        // 現在時刻が「次にダメージを与えていい時間」を超えていたら
        if (Time.time >= nextDamageTime)
        {
            playerInRange.GetDamage(damageAmount);
            nextDamageTime = Time.time + damageInterval;  
        }
       
    }
}
